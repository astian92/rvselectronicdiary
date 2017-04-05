using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;
using RED.Models.ElectronicDiary.Tests;
using RED.Models.FileModels;
using RED.Models.RepositoryBases;
using RED.Models.Responses;

namespace RED.Models.ElectronicDiary
{
    public class DiaryRepository : RepositoryBase
    {
        public IEnumerable<DiaryW> GetDiaryEntries(int page = 1, int pageSize = 10, string number = "-1", int diaryNumber = -1, Guid client = default(Guid), DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var diaryEntries = Db.Diaries.Where(d => d.Number == (diaryNumber == -1 ? d.Number : diaryNumber)); 
            diaryEntries = diaryEntries.Where(d => d.LetterNumber == (number == "-1" ? d.LetterNumber : number));
            diaryEntries = diaryEntries.Where(d => d.ClientId == (client == Guid.Empty ? d.ClientId : client));
            diaryEntries = diaryEntries.Where(d => d.LetterDate >= (from == null ? d.LetterDate : from.Value) &&
                                                   d.LetterDate <= (to == null ? d.LetterDate : to.Value));

            //Order and paging
            diaryEntries = diaryEntries.OrderByDescending(d => d.Number);
            var paged = diaryEntries.Skip((page - 1) * pageSize).Take(pageSize);
            var wrapped = paged.ToList().Select(d => new DiaryW(d));
            
            return wrapped;
        }

        public IEnumerable<ArchivedDiaryW> GetArchivedDiaryEntries(int page = 1, int pageSize = 10, int number = -1, int diaryNumber = -1, string client = "Всички", DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var adiaries = Db.ArchivedDiaries.Where(d => d.Number == (diaryNumber == -1 ? d.Number : diaryNumber)); 
            adiaries = adiaries.Where(d => d.LetterNumber == (number == -1 ? d.LetterNumber : number.ToString()));
            adiaries = adiaries.Where(d => d.Client == (client == "Всички" ? d.Client : client));
            adiaries = adiaries.Where(d => d.LetterDate >= (from == null ? d.LetterDate : from.Value) &&
                                                   d.LetterDate <= (to == null ? d.LetterDate : to.Value));

            //Order and paging
            adiaries = adiaries.OrderByDescending(d => d.Number);
            var paged = adiaries.Skip((page - 1) * pageSize).Take(pageSize);
            var wrapped = paged.ToList().Select(d => new ArchivedDiaryW(d));

            return wrapped;
        }

        public DiaryW GetDiary(Guid diaryId)
        {
            var diary = Db.Diaries.Single(c => c.Id == diaryId);
            return new DiaryW(diary);
        }

        public ArchivedDiaryW GetArchivedDiary(Guid adiaryId)
        {
            var adiary = Db.ArchivedDiaries.Single(c => c.Id == adiaryId);
            return new ArchivedDiaryW(adiary);
        }

        public void Edit(DiaryW diaryW)
        {
            var diary = Db.Diaries.Single(c => c.Id == diaryW.Id);
            diary.LetterNumber = diaryW.LetterNumber;
            diary.LetterDate = diaryW.LetterDate;
            diary.Contractor = diaryW.Contractor;
            diary.ClientId = diaryW.ClientId;
            
            foreach (var item in diary.Products)
            {
                var pts = Db.ProductTests.Where(pt => pt.ProductId == item.Id);
                Db.ProductTests.RemoveRange(pts);
            }

            Db.Products.RemoveRange(diary.Products);

            int i = 1;
            foreach (var item in diaryW.Products)
            {
                item.Id = Guid.NewGuid();
                item.DiaryId = diary.Id;
                item.Number = i;
                i++;

                foreach (var test in item.ProductTests)
                {
                    test.Id = Guid.NewGuid();
                    test.ProductId = item.Id;
                }

                diary.Products.Add(item);
            }

            Db.SaveChanges();
            
            var request = diary.Requests.FirstOrDefault();
            if (request != null)
            {
                var filesRepository = new FilesRepository();
                string charGenerated = filesRepository.GenerateRequestListReport(diary.Id, request.Date, request.TestingPeriod ?? 0);
            }
        }

        public bool Delete(Guid diaryId)
        {
            var diary = Db.Diaries.Single(c => c.Id == diaryId);
            Db.Diaries.Remove(diary);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void AddLetter(DiaryW diary)
        {
            diary.Id = Guid.NewGuid();

            int activeMax = 0;
            if (Db.Diaries.Any())
            {
                activeMax = Db.Diaries.Max(d => d.Number);
            }

            int archivedMax = 0;
            if (Db.ArchivedDiaries.Any())
            {
                archivedMax = Db.ArchivedDiaries.Max(ad => ad.Number);
            }

            diary.Number = activeMax > archivedMax ? activeMax + 1 : archivedMax + 1;
            diary.AcceptanceDateAndTime = DateTime.UtcNow;

            int i = 1;
            foreach (var item in diary.Products)
            {
                item.Id = Guid.NewGuid();
                item.Number = i;
                item.DiaryId = diary.Id;
                i++;

                foreach (var pt in item.ProductTests)
                {
                    pt.Id = Guid.NewGuid();
                    pt.ProductId = item.Id;
                }
            }

            Db.Diaries.Add(diary.ToBase());
            Db.SaveChanges();
        }

        public IEnumerable<ClientW> GetClients()
        {
            return Db.Clients.ToList().Select(r => new ClientW(r));
        }
        
        public IEnumerable<Client> GetSelectListClients(bool allValue = true)
        {
            var selectList = new List<Client>();

            if (allValue)
            {
                var nullable = new Client();
                nullable.Id = Guid.Empty;
                nullable.Name = "Всички";
                selectList.Insert(0, nullable);
            }

            selectList.AddRange(Db.Clients);

            return selectList;
        }

        public IEnumerable<TestW> GetTests()
        {
            var tests = Db.Tests.ToList();
            return tests.Select(x => new TestW(x)).OrderBy(x => x.TestCategory.Name);
        }

        public IEnumerable<TestW> GetSelectListTests()
        {
            var selectList = Db.Tests.OrderBy(x => x.TestCategory.Name).Select(x => new TestW
                                                                {
                                                                    FullName = x.Name + " - " + x.TestCategory.Name,
                                                                    FullValue = x.TestType.ShortName + "_" + x.Id
                                                                });
            return selectList;
        }

        public IEnumerable<TestMethodW> GetTestMethods(Guid testId)
        {
            var methods = Db.TestMethods.Where(x => x.TestId == testId).ToList().Select(x => new TestMethodW(x));
            return methods;
        }

        public string GenerateRequest(Guid diaryId, int testingPeriod)
        {
            if (Db.Diaries.Any(x => x.Id == diaryId))
            {
                Request request = new Request();
                request.Id = Guid.NewGuid();
                var date = DateTime.Now.ToUniversalTime();
                request.Date = date;
                request.DiaryId = diaryId;
                request.TestingPeriod = testingPeriod;

                try
                {
                    Db.Requests.Add(request);
                    var filesRepository = new FilesRepository();
                    string charGenerated = filesRepository.GenerateRequestListReport(diaryId, date, testingPeriod);
                    Db.SaveChanges();

                    return charGenerated;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            return string.Empty;
        }

        public bool DeleteRequest(Guid diaryId)
        {
            try
            {
                if (Db.Diaries.Any(x => x.Id == diaryId))
                {
                    var request = Db.Requests.FirstOrDefault(r => r.DiaryId == diaryId);
                    if (request != null)
                    {
                        Db.Requests.Remove(request);
                        Db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return false;
        }

        public bool AddComment(Guid diaryId, string comment)
        {
            var diary = Db.Diaries.FirstOrDefault(x => x.Id == diaryId);
            if (diary != null)
            {
                diary.Comment = comment;

                try
                {
                    Db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            return false;
        }

        public ActionResponse ArchiveDiary(Guid diaryId)
        {
            var res = new ActionResponse();

            try
            {
                var diary = Db.Diaries.Single(d => d.Id == diaryId);

                var archivedDiary = new ArchivedDiary();
                archivedDiary.Id = Guid.NewGuid();
                archivedDiary.Number = diary.Number;
                archivedDiary.AcceptanceDateAndTime = diary.AcceptanceDateAndTime;
                archivedDiary.LetterNumber = diary.LetterNumber?.ToString();
                archivedDiary.LetterDate = diary.LetterDate;
                archivedDiary.Contractor = diary.Contractor;
                archivedDiary.Client = diary.Client.Name;
                archivedDiary.ClientMobile = diary.Client.Mobile;
                archivedDiary.Comment = diary.Comment;

                var request = diary.Requests.First();
                archivedDiary.RequestDate = request.Date;
                archivedDiary.RequestAcceptedBy = request.User.FirstName.Substring(0, 1) + ". " + request.User.LastName;
                archivedDiary.RequestTestingPeriod = request.TestingPeriod;
                archivedDiary.Remark = new DiaryW(diary).Remark;

                var protocol = request.Protocols.First();
                archivedDiary.ProtocolIssuedDate = protocol.IssuedDate;
                archivedDiary.ProtocolTesterMKB = protocol.TesterMKB;
                archivedDiary.ProtocolTesterFZH = protocol.TesterFZH;
                archivedDiary.ProtocolLabLeader = protocol.LabLeader;

                foreach (var remark in protocol.ProtocolsRemarks)
                {
                    ArchivedProtocolRemark aremark = new ArchivedProtocolRemark();
                    aremark.Id = Guid.NewGuid();
                    aremark.ArchivedDiaryId = archivedDiary.Id;
                    aremark.Remark = remark.Remark.Text;
                    aremark.AcredetationLevel = remark.AcredetationLevel.Level;
                    aremark.Number = remark.Number;

                    Db.ArchivedProtocolRemarks.Add(aremark);
                }

                Db.ProtocolsRemarks.RemoveRange(protocol.ProtocolsRemarks);

                var products = diary.Products;
                foreach (var product in products)
                {
                    ArchivedProduct aproduct = new ArchivedProduct();

                    aproduct.Id = Guid.NewGuid();
                    aproduct.Name = product.Name;
                    aproduct.Number = product.Number;
                    aproduct.Quantity = product.Quantity;

                    var productTests = product.ProductTests;
                    foreach (var ptest in productTests)
                    {
                        ArchivedProductTest aptest = new ArchivedProductTest();

                        aptest.Id = Guid.NewGuid();
                        aptest.TestName = ptest.Test.Name;
                        aptest.TestUnitName = ptest.Test.UnitName;
                        aptest.TestMethods = ptest.TestMethod.Method;
                        aptest.TestAcredetationLevel = ptest.Test.AcredetationLevel.Level;
                        aptest.TestTemperature = ptest.Test.Temperature;
                        aptest.TestCategory = ptest.Test.TestCategory.Name;
                        aptest.TestType = ptest.Test.TestType.Type;
                        aptest.TestTypeShortName = ptest.Test.TestType.ShortName;
                        aptest.MethodValue = ptest.MethodValue;
                        aptest.Remark = ptest.Remark;

                        var protocolResults = ptest.ProtocolResults;
                        foreach (var presult in protocolResults)
                        {
                            ArchivedProtocolResult apresult = new ArchivedProtocolResult();

                            apresult.Id = Guid.NewGuid();
                            apresult.Results = presult.Results;
                            apresult.ResultNumber = presult.ResultNumber;
                            apresult.ArchivedDiaryId = archivedDiary.Id;

                            aptest.ArchivedProtocolResults.Add(apresult);
                        }

                        Db.ProtocolResults.RemoveRange(ptest.ProtocolResults);
                        aproduct.ArchivedProductTests.Add(aptest);
                    }

                    Db.ProductTests.RemoveRange(product.ProductTests);
                    archivedDiary.ArchivedProducts.Add(aproduct);
                }

                Db.Products.RemoveRange(diary.Products);
                Db.ArchivedDiaries.Add(archivedDiary);

                Db.Protocols.RemoveRange(request.Protocols);
                Db.Requests.Remove(request);

                Db.Diaries.Remove(diary);

                Db.SaveChanges();

                res.IsSuccess = true;
                res.ResponseObject = archivedDiary.Id;
                res.SuccessMsg = "Архивирането на дневник: " + diary.Number + " премина успешно.";
            }
            catch (DbEntityValidationException sqlEx)
            {
                foreach (var validationErrors in sqlEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                res.Error = ErrorFactory.UnableToArchiveDiary;
            }

            return res;
        }

        public ActionResponse RegenerateArchivedProtocol(ArchivedDiaryW adiary)
        {
            var response = new ActionResponse();

            try
            {
                var filesRep = new FilesRepository();
                filesRep.RegenerateProtocolReport(adiary);

                response.IsSuccess = true;
                response.SuccessMsg = "Успешно опресняване на архивирания протокол!";
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                response.Error = ErrorFactory.UnableToRefreshArchivedProtocol;
            }

            return response;
        }
    }
}