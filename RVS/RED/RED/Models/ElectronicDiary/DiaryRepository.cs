using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;
using RED.Models.ElectronicDiary.Tests;
using RED.Models.RepositoryBases;
using RED.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class DiaryRepository : RepositoryBase
    {
        public IEnumerable<DiaryW> GetDiaryEntries(int page = 1, int pageSize = 10)
        {
            var diaryEntries = db.Diaries.OrderByDescending(d => d.AcceptanceDateAndTime);
            var paged = diaryEntries.Skip((page - 1) * pageSize).Take(pageSize);
            var wrapped = paged.ToList().Select(d => new DiaryW(d)).ToList();
            
            return wrapped;
        }

        public IEnumerable<ArchivedDiaryW> GetArchivedDiaryEntries(int page = 1, int pageSize = 10)
        {
            var adiaries = db.ArchivedDiaries.OrderByDescending(d => d.AcceptanceDateAndTime);
            var paged = adiaries.Skip((page - 1) * pageSize).Take(pageSize);
            var wrapped = paged.ToList().Select(d => new ArchivedDiaryW(d)).ToList();

            return wrapped;
        }

        public DiaryW GetDiary(Guid diaryId)
        {
            var diary = db.Diaries.Single(c => c.Id == diaryId);
            return new DiaryW(diary);
        }

        public void Edit(DiaryW diaryW)
        {
            var diary = db.Diaries.Single(c => c.Id == diaryW.Id);
            diary.LetterNumber = diaryW.LetterNumber;
            diary.LetterDate = diaryW.LetterDate;
            diary.Contractor = diaryW.Contractor;
            diary.ClientId = diaryW.ClientId;
            
            //diary.Products.Clear();
            db.Products.RemoveRange(diary.Products);
            int i = 1;
            foreach (var item in diaryW.Products)
            {
                item.Id = Guid.NewGuid();
                item.DiaryId = diary.Id;
                item.Number = i;
                //item.Test = db.Tests.FirstOrDefault(x => x.Id == item.TestId);
                i++;

                foreach (var test in item.ProductTests)
                {
                    test.Id = Guid.NewGuid();
                }

                diary.Products.Add(item);
            }

            db.SaveChanges();
        }

        public void Delete(Guid diaryId)
        {
            var diary = db.Diaries.Single(c => c.Id == diaryId);
            db.Diaries.Remove(diary);

            db.SaveChanges();
        }

        public void AddLetter(DiaryW diary)
        {
            diary.Id = Guid.NewGuid();
            diary.Number = db.Diaries.Max(d => d.Number) + 1;
            diary.AcceptanceDateAndTime = DateTime.Now.ToUniversalTime();

            int i = 1;
            foreach (var item in diary.Products)
            {
                item.Id = Guid.NewGuid();
                item.Number = i;
                item.DiaryId = diary.Id;
                //item.Test = db.Tests.FirstOrDefault(x => x.Id == item.TestId);
                i++;

                foreach (var pt in item.ProductTests)
                {
                    pt.Id = Guid.NewGuid();
                }
            }

            db.Diaries.Add(diary.ToBase());
            db.SaveChanges();
        }

        public IEnumerable<ClientW> GetClients()
        {
            var roles = db.Clients.ToList();
            return roles.Select(r => new ClientW(r));
        }
        
        public IEnumerable<TestW> GetTests()
        {
            var tests = db.Tests.ToList();
            return tests.Select(x => new TestW(x));
        }

        public bool GenerateRequest(Guid diaryId)
        {
            if(db.Diaries.Any(x => x.Id == diaryId))
            {
                Request request = new Request();
                request.Id = Guid.NewGuid();
                request.Date = DateTime.Now.ToUniversalTime();
                request.DiaryId = diaryId;

                try
                {
                    db.Requests.Add(request);
                    db.SaveChanges();

                    return true;
                }
                catch(Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            return false;
        }

        public bool AddComment(Guid diaryId, string comment)
        {
            var diary = db.Diaries.FirstOrDefault(x => x.Id == diaryId);
            if(diary != null)
            {
                diary.Comment = comment;

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            return false;
        }

        public ActionResponse ArchiveDiary(Guid diaryId)
        {
            ActionResponse response = new ActionResponse();

            try
            {
                var diary = db.Diaries.Single(d => d.Id == diaryId);

                ArchivedDiary archivedDiary = new ArchivedDiary();

                archivedDiary.Id = Guid.NewGuid();
                archivedDiary.Number = diary.Number.ToString();
                archivedDiary.AcceptanceDateAndTime = diary.AcceptanceDateAndTime;
                archivedDiary.LetterNumber = diary.LetterNumber.ToString();
                archivedDiary.LetterDate = diary.LetterDate;
                archivedDiary.Contractor = diary.Contractor;
                archivedDiary.Client = diary.Client.Name;
                archivedDiary.Comment = diary.Comment;
                var request = diary.Requests.First();
                archivedDiary.RequestDate = request.Date;
                archivedDiary.RequestAcceptedBy = request.User.FirstName.Substring(0,1) + ". " + request.User.LastName;
                archivedDiary.Remark = new DiaryW(diary).Remark;
                archivedDiary.ProtocolIssuedDate = request.Protocols.First().IssuedDate;

                var products = diary.Products;
                foreach (var product in products)
                {
                    ArchivedProduct aproduct = new ArchivedProduct();

                    aproduct.Id = Guid.NewGuid();
                    aproduct.Name = product.Name;
                    aproduct.Number = product.Number.ToString();
                    aproduct.Quantity = product.Quantity;

                    var productTests = product.ProductTests;
                    foreach (var ptest in productTests)
                    {
                        ArchivedProductTest aptest = new ArchivedProductTest();

                        aptest.Id = Guid.NewGuid();
                        aptest.TestName = ptest.Test.Name;
                        aptest.TestUnitName = ptest.Test.UnitName;
                        aptest.TestMethods = ptest.Test.TestMethods;
                        aptest.TestAcredetationLevel = ptest.Test.AcredetationLevel.Level;
                        aptest.TestTemperature = ptest.Test.Temperature;
                        aptest.TestCategory = ptest.Test.TestCategory.Name;
                        aptest.Units = ptest.Units;

                        var protocolResults = ptest.ProtocolResults;
                        foreach (var presult in protocolResults)
                        {
                            ArchivedProtocolResult apresult = new ArchivedProtocolResult();

                            apresult.Id = Guid.NewGuid();
                            apresult.Results = presult.Results;
                            apresult.MethodValue = presult.MethodValue;
                            apresult.ResultNumber = presult.ResultNumber;
                            apresult.ArchivedDiaryId = archivedDiary.Id;

                            aptest.ArchivedProtocolResults.Add(apresult);
                        }

                        db.ProtocolResults.RemoveRange(ptest.ProtocolResults);
                        aproduct.ArchivedProductTests.Add(aptest);
                    }

                    db.ProductTests.RemoveRange(product.ProductTests);
                    archivedDiary.ArchivedProducts.Add(aproduct);
                }

                db.Products.RemoveRange(diary.Products);
                db.ArchivedDiaries.Add(archivedDiary);

                db.Protocols.RemoveRange(request.Protocols);
                db.Requests.Remove(request);

                db.Diaries.Remove(diary);

                db.SaveChanges();

                response.IsSuccess = true;
                response.SuccessMsg = "Архивирането на дневник: " + diary.Number + " премина успешно.";
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                response.Error = ErrorFactory.UnableToArchiveDiary;
            }

            return response;
        }


    }
}