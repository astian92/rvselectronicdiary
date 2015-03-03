using RED.Models.DataContext;
using System;
using System.Collections.Generic;
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
        public string Contractor { get; set; }
        public Guid ClientId { get; set; }
        public Nullable<DateTime> ProtocolCreationDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<DiaryProduct> DiaryProducts { get; set; }
        public virtual ICollection<DiarySampleAcceptor> DiarySampleAcceptors { get; set; }
        public virtual ICollection<DiaryTest> DiaryTests { get; set; }

        public DiaryW()
        {
            this.DiaryProducts = new List<DiaryProduct>();
            this.DiarySampleAcceptors = new List<DiarySampleAcceptor>();
            this.DiaryTests = new List<DiaryTest>();
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