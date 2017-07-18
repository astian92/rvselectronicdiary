using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Requests
{
    public class RequestW
    {
        public RequestW()
        {
        }

        public Guid Id { get; set; }

        public Guid DiaryId { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Дата")]
        public string DateStr 
        { 
            get
            {
                return Date.ToString("dd.MM.yyyy");
            }
        }

        [Display(Name = "Час")]
        public string HourStr
        {
            get
            {
                return Date.ToString("hh:mm");
            }
        }

        public Guid? AcceptedBy { get; set; }

        public bool IsAccepted { get; set; }

        public virtual Diary Diary { get; set; }

        [Display(Name = "Номер")]
        public int Number 
        { 
            get
            {
                return Diary.Number;
            }
        }

        public virtual User User { get; set; } //Acceptor

        [Display(Name = "Приета от")]
        public string Acceptor
        {
            get
            {
                if (User != null)
                {
                    return User.FirstName.Substring(0, 1) + ". " + User.LastName;
                }

                return string.Empty;
            }
        }

        [Display(Name = "Срок на изпитване (дни)")]
        public int? TestingPeriod { get; set; }

        public ICollection<Protocol> Protocols { get; set; }

        public Request ToBase()
        {
            var request = new Request();
            request.Id = Id;
            request.DiaryId = DiaryId;
            request.Date = Date;
            request.AcceptedBy = AcceptedBy;
            request.IsAccepted = IsAccepted;
            request.TestingPeriod = TestingPeriod;

            request.Diary = Diary;
            request.User = User;
            request.Protocols = Protocols;

            return request;
        }
    }
}