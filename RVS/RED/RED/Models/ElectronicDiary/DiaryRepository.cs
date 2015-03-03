using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
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
    }
}