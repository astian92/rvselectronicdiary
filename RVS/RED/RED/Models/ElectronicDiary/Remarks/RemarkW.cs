using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED.Models.ElectronicDiary.Remarks
{
    public class RemarkW
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public ICollection<ProtocolsRemark> ProtocolsRemarks { get; set; }

        public RemarkW()
        {
            this.ProtocolsRemarks = new List<ProtocolsRemark>();
        }

        public RemarkW(Remark remark)
        {
            this.Id = remark.Id;
            this.Text = remark.Text;

            this.ProtocolsRemarks = remark.ProtocolsRemarks;
        }

        public Remark ToBase()
        {
            var remark = new Remark();
            remark.Id = this.Id;
            remark.Text = this.Text;

            remark.ProtocolsRemarks = remark.ProtocolsRemarks;

            return remark;
        }
    }
}
