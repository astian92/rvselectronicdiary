using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Requests
{
    public class RequestW
    {
        public Guid Id { get; set; }

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

        public Nullable<Guid> AcceptedBy { get; set; }

        public bool IsAccepted { get; set; }

        public virtual Diary Diary { get; set; }

        [Display(Name = "Номер")]
        public int Number 
        { 
            get
            {
                return this.Diary.Number;
            }
        }

        public virtual User User { get; set; } //Acceptor

        [Display(Name = "Приета от")]
        public string Acceptor
        {
            get
            {
                if (this.User != null)
                {
                    return this.User.FirstName.Substring(0, 1) + ". " + this.User.LastName;
                }

                return "";
            }
        }

        [Display(Name = "Срок на изпитване (дни)")]
        public int? TestingPeriod { get; set; }

        public ICollection<Protocol> Protocols { get; set; }

        public RequestW()
        {
        }

        public RequestW(Request request)
        {
            this.Id = request.Id;
            this.DiaryId = request.DiaryId;
            this.Date = request.Date;
            this.AcceptedBy = request.AcceptedBy;
            this.IsAccepted = request.IsAccepted;
            this.TestingPeriod = request.TestingPeriod;

            this.Diary = request.Diary;
            this.User = request.User;
            this.Protocols = request.Protocols;
        }

        public Request ToBase()
        {
            Request request = new Request();

            request.Id = this.Id;
            request.DiaryId = this.DiaryId;
            request.Date = this.Date;
            request.AcceptedBy = this.AcceptedBy;
            request.IsAccepted = this.IsAccepted;
            request.TestingPeriod = this.TestingPeriod;

            request.Diary = this.Diary;
            request.User = this.User;
            request.Protocols = this.Protocols;

            return request;
        }
    }
}