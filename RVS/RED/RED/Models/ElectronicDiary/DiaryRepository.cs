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

        public void AddLetter(DiaryW diary)
        {
            diary.Id = Guid.NewGuid();
            diary.Number = db.Diaries.Count() + 1;
            diary.AcceptanceDateAndTime = DateTime.Now.ToUniversalTime();
           
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
    }
}