﻿using System;
using System.Collections.Generic;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.RepositoryBases;

namespace RED.Models.ElectronicDiary.Remarks
{
    public class RemarksRepository : RepositoryBase
    {
        public RemarksRepository()
        {
            this.Db = new RvsDbContext();
        }

        public IEnumerable<RemarkW> GetRemarks()
        {
            var remarks = Db.Remarks.ToList();
            return remarks.Select(r => new RemarkW(r));
        }

        public RemarkW GetRemark(Guid remarkId)
        {
            return new RemarkW(Db.Remarks.Single(r => r.Id == remarkId));
        }

        public bool Create(RemarkW remark)
        {
            remark.Id = Guid.NewGuid();
            Db.Remarks.Add(remark.ToBase());
            Db.SaveChanges();

            return true;
        }

        public bool Edit(RemarkW remark)
        {
            var r = Db.Remarks.Find(remark.Id);
            r.Text = remark.Text;
            Db.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            var remark = Db.Remarks.Find(id);
            Db.Remarks.Remove(remark);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                return false;
            }

            return true;
        }
    }
}
