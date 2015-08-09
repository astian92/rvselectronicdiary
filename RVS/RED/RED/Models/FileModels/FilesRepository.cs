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
                string newDestination = fileProps.FullPath.Substring(0, fileProps.FullPath.Length - 5) + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".xlsx";
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
                WriteProtocolReport(protocol, diaryNumber, "A", acreditedProducts);
            }

            if (notAcreditedProducts.Count() > 0)
            {
                WriteProtocolReport(protocol, diaryNumber, "B", notAcreditedProducts);
            }
        }

        private void WriteProtocolReport(Protocol protocol, int diaryNumber, string category, IEnumerable<Product> products)
        {
            var model = new ReportModel();
            //... Populate model filtering on the USED PRODUCTS for this acredetetion
            var report = new ProtocolReport(model);
            var data = report.GenerateReport();

            CheckAndGenerateDirectories(diaryNumber);

            var fileProps = GetFileProperties(diaryNumber, FileNames.Protocol, category);

            if (File.Exists(fileProps.FullPath))
            {
                string newDestination = fileProps.FullPath.Substring(0, fileProps.FullPath.Length - 5) + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".docx";
                File.Move(fileProps.FullPath, newDestination);
            }

            var file = File.Create(fileProps.FullPath);
            file.Write(data, 0, data.Length);
            file.Close();
        }
    }
}