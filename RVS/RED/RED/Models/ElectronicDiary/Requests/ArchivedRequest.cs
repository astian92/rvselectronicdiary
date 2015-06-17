using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Requests
{
    public class ArchivedRequest
    {
        public Guid DiaryId { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Дата")]
        public string DateStr 
        { 
            get
            {
                return this.Date.ToLocalTime().ToString("dd.MM.yyyy");
            }
        }

        [Display(Name = "Час")]
        public string HourStr
        {
            get
            {
                return this.Date.ToLocalTime().ToString("hh:mm");
            }
        }

        public string AcceptedBy { get; set; }

        public bool IsAccepted { get; set; }

        public virtual ArchivedDiary Diary { get; set; }

        [Display(Name = "Номер")]
        public string Number 
        { 
            get
            {
                return this.Diary.Number;
            }
        }

        [Display(Name = "Приета от")]
        public string Acceptor
        {
            get
            {
                return this.AcceptedBy;
            }
        }

        //public ICollection<Protocol> Protocols { get; set; }

        public ArchivedRequest(ArchivedDiary adiary)
        {
            this.DiaryId = adiary.Id;
            this.Date = adiary.RequestDate;
            this.AcceptedBy = adiary.RequestAcceptedBy;
            this.IsAccepted = true;

            this.Diary = adiary;
        }
    }
}