using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class ArchivedDiaryW
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage="Полето \"Номер\" е задължително!")]
        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Полето \"Дата на приемане\" е задължително!")]
        [Display(Name = "Дата на приемане")]
        public DateTime AcceptanceDateAndTime { get; set; }

        [Display(Name = "Писмо №")]
        public string LetterNumber { get; set; }

        [Required(ErrorMessage = "Полето \"Дата на писмото\" е задължително!")]
        [Display(Name = "Писмо дата")]
        public DateTime LetterDate { get; set; }

        [Required(ErrorMessage = "Полето \"Възложител\" е задължително!")]
        [Display(Name = "Възложител")]
        public string Contractor { get; set; }

        [Required(ErrorMessage = "Полето \"Клиент\" е задължително!")]
        [Display(Name = "Клиент")]
        public string Client { get; set; }

        [MaxLength(30, ErrorMessage = "Телефонът на клиента трябва да е не по-дълъг от 30 символа.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Телефонът трябва да бъде във формат +359123456789.")]
        [Display(Name = "Телефон")]
        public string ClientMobile { get; set; }

        [Display(Name = "Бележка")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Полето \"Дата на създаване на заявката\" е задължително!")]
        [Display(Name = "Дата на създаване на заявката")]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Полето \"Приел заявката\" е задължително!")]
        [Display(Name = "Приел заявката")]
        public string RequestAcceptedBy { get; set; }

        [Required(ErrorMessage = "Полето \"Протокол създаден на\" е задължителнo!")]
        [Display(Name = "Протокол създаден на")]
        public DateTime ProtocolIssuedDate { get; set; }

        [Required(ErrorMessage = "Полето \"Протокол създаден на\" е задължителнo!")]
        [Display(Name = "Извършил изпитването")]
        public string ProtocolTester { get; set; }

        [Required(ErrorMessage = "Полето \"Протокол създаден на\" е задължителнo!")]
        [Display(Name = "Ръководител")]
        public string ProtocolLabLeader { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = "Полето \"Срок на изпитването\" е задължителнo!")]
        [Display(Name = "Срок на изпитването")]
        public Nullable<int> RequestTestingPeriod { get; set; }

        [Display(Name = "Продукти")]
        public virtual ICollection<ArchivedProduct> ArchivedProducts { get; set; }

        public virtual ICollection<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }

        public virtual ICollection<ArchivedProtocolRemark> ArchivedProtocolRemarks { get; set; }

        public string LetterInfo
        {
            get
            {
                if(this.LetterNumber != "")
                    return "Писмо №" + this.LetterNumber + " от " + this.LetterDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                else
                    return "Писмо от " + this.LetterDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string RequestDateTime 
        {
            get
            {
                return this.RequestDate.ToString("HH:mm");
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var arguments = value.Split(':');
                    var hours = int.Parse(arguments[0]);
                    var minutes = int.Parse(arguments[1]);

                    this.RequestDate = this.RequestDate.AddHours(hours);
                    this.RequestDate = this.RequestDate.AddMinutes(minutes);
                }
            }
        }

        public ArchivedDiaryW()
        {
        }

        public ArchivedDiaryW(ArchivedDiary diary)
        {
            this.Id = diary.Id;
            this.Number = diary.Number;
            this.LetterNumber = diary.LetterNumber;
            this.LetterDate = diary.LetterDate.ToLocalTime();
            this.AcceptanceDateAndTime = diary.AcceptanceDateAndTime.ToLocalTime();
            this.Contractor = diary.Contractor;
            this.Client = diary.Client;
            this.ClientMobile = diary.ClientMobile;
            this.Comment = diary.Comment;
            this.RequestDate = diary.RequestDate.ToLocalTime();
            this.RequestAcceptedBy = diary.RequestAcceptedBy;
            this.ProtocolIssuedDate = diary.ProtocolIssuedDate.ToLocalTime();
            this.ProtocolTester = diary.ProtocolTester;
            this.ProtocolLabLeader = diary.ProtocolLabLeader;
            this.Remark = diary.Remark;
            this.RequestTestingPeriod = diary.RequestTestingPeriod;

            this.ArchivedProducts = diary.ArchivedProducts;
            this.ArchivedProtocolResults = diary.ArchivedProtocolResults;
            this.ArchivedProtocolRemarks = diary.ArchivedProtocolRemarks;
        }

        public ArchivedDiary ToBase()
        {
            ArchivedDiary diary = new ArchivedDiary();

            diary.Id = this.Id;
            diary.Number = this.Number;
            diary.LetterNumber = this.LetterNumber;
            diary.LetterDate = this.LetterDate.ToUniversalTime();
            diary.AcceptanceDateAndTime = this.AcceptanceDateAndTime.ToUniversalTime();
            diary.Contractor = this.Contractor;
            diary.Client = this.Client;
            diary.ClientMobile = this.ClientMobile;
            diary.Comment = this.Comment;
            diary.RequestDate = this.RequestDate.ToUniversalTime();
            diary.RequestAcceptedBy = this.RequestAcceptedBy;
            diary.ProtocolIssuedDate = this.ProtocolIssuedDate.ToUniversalTime();
            diary.ProtocolTester = this.ProtocolTester;
            diary.ProtocolLabLeader = this.ProtocolLabLeader;
            diary.Remark = this.Remark;
            diary.RequestTestingPeriod = this.RequestTestingPeriod;

            diary.ArchivedProducts = this.ArchivedProducts;
            diary.ArchivedProtocolResults = this.ArchivedProtocolResults;
            diary.ArchivedProtocolRemarks = this.ArchivedProtocolRemarks;

            return diary;
        }
    }
}