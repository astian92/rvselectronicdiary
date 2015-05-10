using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class DiaryW
    {
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }

        public DateTime AcceptanceDateAndTime { get; set; }

        [Required(ErrorMessage = "Номерът на писмото е задължителен")]
        [Range(0, int.MaxValue, ErrorMessage = "Невалиден номер")]
        [Display(Name = "Писмо №")]
        public int LetterNumber { get; set; }

        [Required(ErrorMessage = "Датата на писмото е задължителна")]
        [Display(Name = "Писмо дата")]
        public DateTime LetterDate { get; set; }

        [Required(ErrorMessage = "Възложителя е задължителен")]
        [Display(Name="Възложител")]
        public string Contractor { get; set; }

        [Display(Name = "Бележка")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public Guid ClientId { get; set; }

        [Display(Name = "Клиент")]
        public virtual Client Client { get; set; }

        [Display(Name = "Заявка")]
        public virtual Request Request { get; set; }

        [Display(Name = "Продукти")]
        public virtual ICollection<Product> Products { get; set; }

        public DiaryW()
        {
            this.Products = new List<Product>();
        }

        public string LetterInfo
        {
            get
            {
                return "Писмо №" + this.LetterNumber + " от " + this.LetterDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string Remark
        {
            get
            {
                StringBuilder remark = new StringBuilder();

                //if (this.Products.Any(p => p.Test.AcredetationLevel.Level.Trim() == AcredetationLevels.Acredited))
                //{
                //    remark.Append("A");
                //}

                //if (this.Products.Any(p => p.Test.AcredetationLevel.Level.Trim() == AcredetationLevels.NotAcredited))
                //{
                //    if (remark.Length > 0)
                //    {
                //        remark.Append("/");
                //    }
                //    remark.Append("B");
                //}

                return remark.ToString();
            }
        }

        public DiaryW(Diary diary)
        {
            this.Id = diary.Id;
            this.Number = diary.Number;
            this.LetterNumber = diary.LetterNumber;
            this.LetterDate = diary.LetterDate;
            this.AcceptanceDateAndTime = diary.AcceptanceDateAndTime;
            this.Contractor = diary.Contractor;
            this.ClientId = diary.ClientId;
            this.Comment = diary.Comment;

            this.Client = diary.Client;
            this.Request = diary.Requests.FirstOrDefault();
            this.Products = diary.Products;
        }

        public Diary ToBase()
        {
            Diary diary = new Diary();

            diary.Id = this.Id;
            diary.Number = this.Number;
            diary.LetterNumber = this.LetterNumber;
            diary.LetterDate = this.LetterDate;
            diary.AcceptanceDateAndTime = this.AcceptanceDateAndTime;
            diary.Contractor = this.Contractor;
            diary.ClientId = this.ClientId;
            diary.Comment = this.Comment;

            diary.Client = this.Client;
            diary.Requests.Add(this.Request);
            diary.Products = this.Products;

            return diary;
        }
    }
}