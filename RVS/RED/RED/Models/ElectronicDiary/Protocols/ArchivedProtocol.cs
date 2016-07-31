using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Protocols
{
    public class ArchivedProtocol
    {
        public ArchivedDiary ArchivedDiary { get; set; }
        public Guid DiaryId { get; set; }
        public DateTime IssuedDate { get; set; }
        public string TesterMKB { get; set; }
        public string TesterFZH { get; set; }
        public string LabLeader { get; set; }

        public ICollection<ArchivedProtocolResult> ProtocolResults { get; set; }

        public ArchivedProtocol(ArchivedDiary adiary)
        {
            this.ArchivedDiary = adiary;
            this.DiaryId = adiary.Id;
            this.IssuedDate = adiary.ProtocolIssuedDate;
            this.ProtocolResults = adiary.ArchivedProtocolResults;
            this.TesterMKB = adiary.ProtocolTesterMKB;
            this.TesterFZH = adiary.ProtocolTesterFZH;
            this.LabLeader = adiary.ProtocolLabLeader;
        }

    }
}