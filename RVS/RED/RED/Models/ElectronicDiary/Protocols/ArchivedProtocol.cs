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
            TesterMKB = adiary.ProtocolTesterMKB;
            TesterFZH = adiary.ProtocolTesterFZH;
            LabLeader = adiary.ProtocolLabLeader;
        }

        public ArchivedDiary ArchivedDiary { get; set; }

        public Guid DiaryId { get; set; }

        public DateTime IssuedDate { get; set; }

        public string TesterMKB { get; set; }

        public string TesterFZH { get; set; }

        public string LabLeader { get; set; }

        public ICollection<ArchivedProtocolResult> ProtocolResults { get; set; }
    }
}