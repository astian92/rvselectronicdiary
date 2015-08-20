using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED.Models.ElectronicDiary.Remarks
{
    public class RemarksRepository : RepositoryBase
    {
        public RemarksRepository()
        {
            this.db = new RvsDbContext();
        }

        public IEnumerable<RemarkW> GetRemarks()
        {
            var remarks = db.Remarks.ToList();
            return remarks.Select(r => new RemarkW(r));
        }

        public RemarkW GetRemark(Guid remarkId)
        {
            return new RemarkW(db.Remarks.Single(r => r.Id == remarkId));
        }

        public bool Create(RemarkW remark)
        {
            remark.Id = Guid.NewGuid();
            db.Remarks.Add(remark.ToBase());
            db.SaveChanges();

            return true;
        }

        public bool Edit(RemarkW remark)
        {
            var r = db.Remarks.Find(remark.Id);
            r.Text = remark.Text;
            db.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            var remark = db.Remarks.Find(id);
            db.Remarks.Remove(remark);
            db.SaveChanges();
            return true;
        }
    }
}
