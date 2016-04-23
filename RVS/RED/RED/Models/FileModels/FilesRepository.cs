using RED.Models.FileModels.RequestList;
using RED.Models.ReportGeneration.EPPlus;
using RED.Models.RepositoryBases;
using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using RED.Models.FileModels.ProtocolFiles;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.Converters;

namespace RED.Models.FileModels
{
    public class FilesRepository : RepositoryBase
    {
        public string FileTreePath { get; set; }

        public FilesRepository()
        {
            this.FileTreePath = ConfigurationManager.AppSettings["FilesDestination"];
        }

        private FileProperties GetFileProperties(int diaryNumber, string reportName, string category = "")
        {
            var fileProp = new FileProperties();

            var directoryName = DirUtility.CalculateDirectoryName(diaryNumber);

            fileProp.Path = this.FileTreePath + directoryName + @"\" + diaryNumber;
            fileProp.FileName = category + diaryNumber + reportName;

            return fileProp;
        }

        private void CheckAndGenerateDirectories(int diaryNumber)
        {
            var fileProps = GetFileProperties(diaryNumber, "doesnt matter");
            if (!Directory.Exists(fileProps.Path))
            {
                Directory.CreateDirectory(fileProps.Path);
            }
        }

        public void GenerateRequestListReport(Guid diaryId, DateTime date, int testingPeriod)
        {
            var diary = db.Diaries.Single(d => d.Id == diaryId);
            var model = new ReportModel();
            //var request = diary.Requests.First();

            model.ReportParameters.Add("RequestNumber", diary.Number);
            model.ReportParameters.Add("TestingPeriod", testingPeriod);
            model.ReportParameters.Add("Date", date);

            var items = new List<RequestListModel>();
            foreach (var product in diary.Products.OrderBy(dp => dp.Number))
            {
                var item = new RequestListModel();

                item.Number = product.Number;
                item.SampleType = product.Name;
                item.Quantity = product.Quantity;
                item.TestNames = product.ProductTests.Select(t => t.Test.Name).ToArray();

                items.Add(item);
            }

            model.reportItems = items;
            var report = new RequestListReport(model);

            var data = report.GenerateReport();

            //this is supposed to create all the necessary directories for the file.
            CheckAndGenerateDirectories(diary.Number);

            var fileProps = GetFileProperties(diary.Number, FileNames.RequestListReport);

            //if the file already exists override it BUT keep the old with current date
            if (File.Exists(fileProps.FullPath))
            {
                string newDestination = fileProps.FullPath.Substring(0, fileProps.FullPath.Length - 5) + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx";
                File.Move(fileProps.FullPath, newDestination);
            }

            var file = File.Create(fileProps.FullPath);
            file.Write(data, 0, data.Length);
            file.Close();
        }

        public byte[] GetRequestListReport(Guid diaryId, out string fileName)
        {
            var diary = db.Diaries.Single(d => d.Id == diaryId);

            var fileProp = GetFileProperties(diary.Number, FileNames.RequestListReport);
            
            if (Directory.Exists(fileProp.Path))
            {
                var file = File.ReadAllBytes(fileProp.FullPath);

                fileName = fileProp.FileName;
                return file;
            }

            fileName = "unknown.xlsx";
            return new byte[0];
        }

        public void GenerateProtocolReport(Protocol protocol, Request request)
        {
            var protocolRequest = request ?? protocol.Request;
            var diaryNumber = protocolRequest.Diary.Number;
            //We need to identify if there will be more than 1 protocol (one for A and one for B)
            var acreditedProducts = protocolRequest.Diary.Products.Where(p => p.ProductTests.Any(pt => pt.Test.AcredetationLevel.Level.Trim() == AcredetationLevels.Acredited));

            var notAcreditedProducts = protocolRequest.Diary.Products.Where(p => p.ProductTests.Any(pt => pt.Test.AcredetationLevel.Level.Trim() == AcredetationLevels.NotAcredited));

            if (acreditedProducts.Count() > 0)
            {
                WriteProtocolReport(protocol, diaryNumber, "A", acreditedProducts, protocolRequest);
            }

            if (notAcreditedProducts.Count() > 0)
            {
                WriteProtocolReport(protocol, diaryNumber, "B", notAcreditedProducts, protocolRequest);
            }
        }

        private void WriteProtocolReport(Protocol protocol, int diaryNumber, string category, IEnumerable<Product> products, Request request)
        {
            var model = new ReportModel();

            model.ReportParameters.Add("AcredetationLevel", category);

            model.ReportParameters.Add("ProtocolNumber", category + diaryNumber);
            model.ReportParameters.Add("ProtocolIssuedDate", protocol.IssuedDate);
            
            model.ReportParameters.Add("Products", products);
            var methods = products.SelectMany(p => p.ProductTests.Where(pt => pt.Test.AcredetationLevel.Level.Trim() == category).Select(pt => pt.Test.TestMethods)).Distinct();
            model.ReportParameters.Add("Methods", methods);
            var quantities = products.OrderBy(p => p.Number).Select(p => p.Quantity);
            model.ReportParameters.Add("Quantities", quantities);

            var protocolResults = protocol.ProtocolResults.Where(pr =>
                pr.ProductTest.Test.AcredetationLevel.Level.Trim() == category)
                .OrderBy(x => x.ProductTest.Product.Number).ThenBy(x => x.ProductTest.Test.Name).ThenBy(x => x.ResultNumber);
            model.ReportParameters.Add("ProtocolResults", protocolResults);

            model.ReportParameters.Add("Contractor", request.Diary.Contractor);
            model.ReportParameters.Add("Client", request.Diary.Client.Name);
            model.ReportParameters.Add("LetterNumber", request.Diary.LetterNumber);
            model.ReportParameters.Add("LetterDate", request.Diary.LetterDate);
            model.ReportParameters.Add("RequestDate", request.Date.ToLocalTime());
            model.ReportParameters.Add("LabLeader", protocol.LabLeader);
            model.ReportParameters.Add("Tester", protocol.TesterMKB);

            var remarks = protocol.ProtocolsRemarks.Where(r => r.AcredetationLevel.Level.Trim() == category);
            model.ReportParameters.Add("Remarks", remarks);

            if (category == "A")
            {
                string acredetationString = @"АКРЕДИТИРАНА СЪГЛАСНО БДС EN ISO/IEC 17025:2006
СЕРТИФИКАТ №55 ЛИ ОТ 08.04.2015 г./ ИА „БСА”
С ВАЛИДНОСТ НА АКРЕДИТАЦИЯТА ДО 31.03.2016 г.
";
                model.ReportParameters.Add("AcredetationString", acredetationString);
            }

            var report = new ProtocolReport(model);
            var data = report.GenerateReport();

            CheckAndGenerateDirectories(diaryNumber);

            var fileProps = GetFileProperties(diaryNumber, FileNames.Protocol, category);

            if (File.Exists(fileProps.FullPath))
            {
                string newDestination = fileProps.FullPath.Substring(0, fileProps.FullPath.Length - 5) + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".docx";
                File.Move(fileProps.FullPath, newDestination);
            }

            var file = File.Create(fileProps.FullPath);
            file.Write(data, 0, data.Length);
            file.Close();
        }

        public byte[] GetProtocolReport(Guid protocolId, string category, out string fileName)
        {
            var diary = db.Protocols.Single(p => p.Id == protocolId).Request.Diary;

            var fileProp = GetFileProperties(diary.Number, FileNames.Protocol, category);

            if (Directory.Exists(fileProp.Path))
            {
                var file = File.ReadAllBytes(fileProp.FullPath);

                fileName = fileProp.FileName;
                return file;
            }

            fileName = "unknown";
            return new byte[0];
        }

        public byte[] GetArchivedProtocolReport(Guid archivedDiaryId, string category, out string fileName)
        {
            var diary = db.ArchivedDiaries.Single(ad => ad.Id == archivedDiaryId);

            var fileProp = GetFileProperties(diary.Number, FileNames.Protocol, category);

            if (Directory.Exists(fileProp.Path))
            {
                var file = File.ReadAllBytes(fileProp.FullPath);

                fileName = fileProp.FileName;
                return file;
            }

            fileName = "unknown.xlsx";
            return new byte[0];
        }

        //Archived shit
        public void RegenerateProtocolReport(ArchivedDiaryW adiary)
        {
            var acreditedProducts = adiary.ArchivedProducts.Where(p => p.ArchivedProductTests.Any(apt => apt.TestAcredetationLevel.Trim() == AcredetationLevels.Acredited));
            var notAcreditedProducts = adiary.ArchivedProducts.Where(p => p.ArchivedProductTests.Any(apt => apt.TestAcredetationLevel.Trim() == AcredetationLevels.NotAcredited));

            if (acreditedProducts.Count() > 0)
            {
                RewriteProtocolReport(adiary, "A", acreditedProducts);
            }
            if (notAcreditedProducts.Count() > 0)
            {
                RewriteProtocolReport(adiary, "B", notAcreditedProducts);
            }
        }

        private void RewriteProtocolReport(ArchivedDiaryW adiary, string category, IEnumerable<ArchivedProduct> products)
        {
            var model = new ReportModel();

            model.ReportParameters.Add("AcredetationLevel", category);
            model.ReportParameters.Add("ProtocolNumber", category + adiary.Number);
            model.ReportParameters.Add("ProtocolIssuedDate", adiary.ProtocolIssuedDate);

            var convertedProducts = new List<Product>();
            var productConverter = new ProductConverter();
            foreach (var pr in products)
            {
                var product = productConverter.ConvertFromArchived(pr);
                convertedProducts.Add(product);
            }

            model.ReportParameters.Add("Products", convertedProducts);
            var methods = convertedProducts.SelectMany(p => p.ProductTests.Where(pt => pt.Test.AcredetationLevel.Level.Trim() == category).Select(pt => pt.Test.TestMethods)).Distinct();
            model.ReportParameters.Add("Methods", methods);
            var quantities = convertedProducts.OrderBy(p => p.Number).Select(p => p.Quantity);
            model.ReportParameters.Add("Quantities", quantities);

            var protocolResultsConverter = new ProtocolResultsConverter();
            var theProtocolResults = adiary.ArchivedProtocolResults.Select(apr => protocolResultsConverter.ConvertFromArchived(apr));

            var protocolResults = theProtocolResults.Where(pr =>
                pr.ProductTest.Test.AcredetationLevel.Level.Trim() == category)
                .OrderBy(x => x.ProductTest.Product.Number).ThenBy(x => x.ProductTest.Test.Name).ThenBy(x => x.ResultNumber);
            model.ReportParameters.Add("ProtocolResults", protocolResults);

            model.ReportParameters.Add("Contractor", adiary.Contractor);
            model.ReportParameters.Add("Client", adiary.Client);
            model.ReportParameters.Add("LetterNumber", adiary.LetterNumber);
            model.ReportParameters.Add("LetterDate", adiary.LetterDate);
            model.ReportParameters.Add("RequestDate", adiary.RequestDate.ToLocalTime());
            model.ReportParameters.Add("LabLeader", adiary.ProtocolLabLeader);
            model.ReportParameters.Add("Tester", adiary.ProtocolTester);

            var remarksConverter = new RemarksConverter();
            var remarks = adiary.ArchivedProtocolRemarks.Where(r => r.AcredetationLevel.Trim() == category).
                Select(r => new ProtocolsRemark()
                {
                    Remark = remarksConverter.ConvertFromArchived(r),
                    Number = r.Number
                });

            model.ReportParameters.Add("Remarks", remarks);

            if (category == "A")
            {
                string acredetationString = @"АКРЕДИТИРАНА СЪГЛАСНО БДС EN ISO/IEC 17025:2006
СЕРТИФИКАТ №55 ЛИ ОТ 08.04.2015 г./ ИА „БСА”
С ВАЛИДНОСТ НА АКРЕДИТАЦИЯТА ДО 31.03.2016 г.
";
                model.ReportParameters.Add("AcredetationString", acredetationString);
            }

            var report = new ProtocolReport(model);
            var data = report.GenerateReport();

            CheckAndGenerateDirectories(adiary.Number);

            var fileProps = GetFileProperties(adiary.Number, FileNames.Protocol, category);

            if (File.Exists(fileProps.FullPath))
            {
                string newDestination = fileProps.FullPath.Substring(0, fileProps.FullPath.Length - 5) + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".docx";
                File.Move(fileProps.FullPath, newDestination);
            }

            var file = File.Create(fileProps.FullPath);
            file.Write(data, 0, data.Length);
            file.Close();
        }
    }
}