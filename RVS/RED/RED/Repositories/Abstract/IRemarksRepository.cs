using System;
using System.Collections.Generic;
using RED.Models.ElectronicDiary.Remarks;

namespace RED.Repositories.Abstract
{
    public interface IRemarksRepository
    {
        bool Create(RemarkW remark);

        bool Delete(Guid id);

        bool Edit(RemarkW remark);

        RemarkW GetRemark(Guid remarkId);

        IEnumerable<RemarkW> GetRemarks();
    }
}