using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class DiaryW
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime AcceptanceDateAndTime { get; set; }
        public string TypeNumberDate { get; set; }
        [Required]
        [Display(Name="Възложител")]
        public string Contractor { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public Guid ClientId { get; set; }
        public Nullable<DateTime> ProtocolCreationDate { get; set; }

        public virtual Client Client { get; set; }
        [Display(Name = "Продукти")]
        public virtual ICollection<DiaryProduct> DiaryProducts { get; set; }
        public virtual ICollection<DiarySampleAcceptor> DiarySampleAcceptors { get; set; }
        public virtual ICollection<DiaryTest> DiaryTests { get; set; }

        public DiaryW()
        {
            this.DiaryProducts = new List<DiaryProduct>();
            this.DiarySampleAcceptors = new List<DiarySampleAcceptor>();
            this.DiaryTests = new List<DiaryTest>();
        }

        public string QueryNumber
        {
            get
            {
                return this.Number + "/" + this.AcceptanceDateAndTime.ToString("dd.MM.yy", CultureInfo.InvariantCulture);
            }
        }

        public string ProtocolNumberCreationDate
        {
            get
            {
                string result = this.Number + "/";
                if (this.ProtocolCreationDate.HasValue)
                {
                    result += this.ProtocolCreationDate.Value.ToString("dd.MM.yy", CultureInfo.InvariantCulture);
                }

                return result;
            }
        }

        public string Remark
        {
            get
            {
                var acreditedTests = this.DiaryTests.Where(t => t.Test.AcredetationLevel.Level == ((char)AcredetationLevels.Acredited).ToString());

                var notAcreditedTests = this.DiaryTests.Where(t => t.Test.AcredetationLevel.Level == ((char)AcredetationLevels.NotAcredited).ToString());

                if (acreditedTests.Count() > 0 && notAcreditedTests.Count() > 0)
                {
                    return "A/B";
                }
                else if (acreditedTests.Count() > 0)
                {
                    return "A";
                }
                else if (notAcreditedTests.Count() > 0)
                {
                    return "B";
                }

                return "";
            }
        }

        public DiaryW(Diary diary)
        {
            this.Id = diary.Id;
            this.Number = diary.Number;
            this.AcceptanceDateAndTime = diary.AcceptanceDateAndTime;
            this.TypeNumberDate = diary.TypeNumberDate;
            this.Contractor = diary.Contractor;
            this.ClientId = diary.ClientId;
            this.ProtocolCreationDate = diary.ProtocolCreationDate;

            this.Client = diary.Client;
            this.DiaryProducts = diary.DiaryProducts;
            this.DiarySampleAcceptors = diary.DiarySampleAcceptors;
            this.DiaryTests = diary.DiaryTests;
        }

        public Diary ToBase()
        {
            Diary diary = new Diary();

            diary.Id = this.Id;
            diary.Number = this.Number;
            diary.AcceptanceDateAndTime = this.AcceptanceDateAndTime;
            diary.TypeNumberDate = this.TypeNumberDate;
            diary.Contractor = this.Contractor;
            diary.ClientId = this.ClientId;
            diary.ProtocolCreationDate = this.ProtocolCreationDate;

            diary.Client = this.Client;
            diary.DiaryProducts = this.DiaryProducts;
            diary.DiarySampleAcceptors = this.DiarySampleAcceptors;
            diary.DiaryTests = this.DiaryTests;

            return diary;
        }
    }
}