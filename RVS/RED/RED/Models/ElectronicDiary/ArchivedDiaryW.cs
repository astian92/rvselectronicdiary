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

        [Required]
        public string Number { get; set; }

        public DateTime AcceptanceDateAndTime { get; set; }

        [Required(ErrorMessage = "Номерът на писмото е задължителен")]
        [Range(0, int.MaxValue, ErrorMessage = "Невалиден номер")]
        [Display(Name = "Писмо №")]
        public string LetterNumber { get; set; }

        [Required(ErrorMessage = "Датата на писмото е задължителна")]
        [Display(Name = "Писмо дата")]
        public DateTime LetterDate { get; set; }

        [Required(ErrorMessage = "Възложителя е задължителен")]
        [Display(Name = "Възложител")]
        public string Contractor { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public string Client { get; set; }

        [Display(Name = "Бележка")]
        public string Comment { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequestAcceptedBy { get; set; }

        public DateTime ProtocolIssuedDate { get; set; }

        public string Remark { get; set; }

        [Display(Name = "Продукти")]
        public virtual ICollection<ArchivedProduct> ArchivedProducts { get; set; }

        public virtual ICollection<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }

        public string LetterInfo
        {
            get
            {
                return "Писмо №" + this.LetterNumber + " от " + this.LetterDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
        }

        public ArchivedDiaryW(ArchivedDiary diary)
        {
            this.Id = diary.Id;
            this.Number = diary.Number;
            this.LetterNumber = diary.LetterNumber;
            this.LetterDate = diary.LetterDate;
            this.AcceptanceDateAndTime = diary.AcceptanceDateAndTime;
            this.Contractor = diary.Contractor;
            this.Client = diary.Client;
            this.Comment = diary.Comment;
            this.RequestDate = diary.RequestDate;
            this.RequestAcceptedBy = diary.RequestAcceptedBy;
            this.ProtocolIssuedDate = diary.ProtocolIssuedDate;
            this.Remark = diary.Remark;

            this.ArchivedProducts = diary.ArchivedProducts;
            this.ArchivedProtocolResults = diary.ArchivedProtocolResults;
        }

        public ArchivedDiary ToBase()
        {
            ArchivedDiary diary = new ArchivedDiary();

            diary.Id = this.Id;
            diary.Number = this.Number;
            diary.LetterNumber = this.LetterNumber;
            diary.LetterDate = this.LetterDate;
            diary.AcceptanceDateAndTime = this.AcceptanceDateAndTime;
            diary.Contractor = this.Contractor;
            diary.Client = this.Client;
            diary.Comment = this.Comment;
            diary.RequestDate = this.RequestDate;
            diary.RequestAcceptedBy = this.RequestAcceptedBy;
            diary.ProtocolIssuedDate = this.ProtocolIssuedDate;
            diary.Remark = this.Remark;

            diary.ArchivedProducts = this.ArchivedProducts;
            diary.ArchivedProtocolResults = this.ArchivedProtocolResults;

            return diary;
        }
    }
}