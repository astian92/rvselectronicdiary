using System;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Remarks
{
    public class RemarkW
    {
        public RemarkW()
        {
        }

        public RemarkW(Remark remark)
        {
            this.Id = remark.Id;
            this.Text = remark.Text;
        }

        public Guid Id { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        public Remark ToBase()
        {
            var remark = new Remark();
            remark.Id = this.Id;
            remark.Text = this.Text;

            return remark;
        }
    }
}