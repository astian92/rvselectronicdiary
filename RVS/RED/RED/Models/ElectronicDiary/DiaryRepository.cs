using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;
using RED.Models.ElectronicDiary.Tests;
using RED.Models.RepositoryBases;
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
            
            diary.Products.Clear();
            foreach (var item in diaryW.Products)
            {
                item.Id = Guid.NewGuid();
                item.DiaryId = diary.Id;
                item.Test = db.Tests.FirstOrDefault(x => x.Id == item.TestId);
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
            diary.Number = db.Diaries.Count() + 1;
            diary.AcceptanceDateAndTime = DateTime.Now.ToUniversalTime();

            foreach (var item in diary.Products)
            {
                item.Id = Guid.NewGuid();
                item.DiaryId = diary.Id;
                item.Test = db.Tests.FirstOrDefault(x => x.Id == item.TestId);
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
    }
}