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
        public string Tester { get; set; }
        public string LabLeader { get; set; }

        public ICollection<ArchivedProtocolResult> ProtocolResults { get; set; }

        public ArchivedProtocol(ArchivedDiary adiary)
        {
            this.ArchivedDiary = adiary;
            this.DiaryId = adiary.Id;
            this.IssuedDate = adiary.ProtocolIssuedDate;
            this.ProtocolResults = adiary.ArchivedProtocolResults;
            this.Tester = adiary.ProtocolTester;
            this.LabLeader = adiary.ProtocolLabLeader;
        }

    }
}