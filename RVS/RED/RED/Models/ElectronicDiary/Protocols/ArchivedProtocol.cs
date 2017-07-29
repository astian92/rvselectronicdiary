using System;
using System.Collections.Generic;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Protocols
{
    public class ArchivedProtocol
    {
        public ArchivedProtocol(ArchivedDiary adiary)
        {
            ArchivedDiary = adiary;
            DiaryId = adiary.Id;
            IssuedDate = adiary.ProtocolIssuedDate;
            ProtocolResults = adiary.ArchivedProtocolResults;
            Tester = adiary.ProtocolTester;
            LabLeader = adiary.ProtocolLabLeader;
        }

        public ArchivedDiary ArchivedDiary { get; set; }

        public Guid DiaryId { get; set; }

        public DateTime IssuedDate { get; set; }

        public string Tester { get; set; }

        public string LabLeader { get; set; }

        public ICollection<ArchivedProtocolResult> ProtocolResults { get; set; }
    }
}