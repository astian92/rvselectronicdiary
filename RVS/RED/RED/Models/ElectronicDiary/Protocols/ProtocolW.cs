using System;
using System.Collections.Generic;
using System.Linq;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Protocols
{
    public class ProtocolW
    {
        public ProtocolW()
        {
            ProtocolResults = new HashSet<ProtocolResult>();
            ProtocolsRemarksA = new HashSet<ProtocolsRemark>();
            ProtocolsRemarksB = new HashSet<ProtocolsRemark>();
        }

        public Guid Id { get; set; }

        public Guid RequestId { get; set; }

        public DateTime IssuedDate { get; set; }

        public string TesterMKB { get; set; }

        public string TesterFZH { get; set; }

        public string LabLeader { get; set; }

        public Request Request { get; set; }

        public ICollection<ProtocolResult> ProtocolResults { get; set; }

        public IEnumerable<ProtocolsRemark> ProtocolsRemarksA { get; set; }

        public IEnumerable<ProtocolsRemark> ProtocolsRemarksB { get; set; }

        public string IssuedTime
        {
            get
            {
                return IssuedDate.ToString("HH:mm");
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var arguments = value.Split(':');
                    var hours = int.Parse(arguments[0]);
                    var minutes = int.Parse(arguments[1]);

                    IssuedDate = IssuedDate.AddHours(hours);
                    IssuedDate = IssuedDate.AddMinutes(minutes);
                }
            }
        }

        public Protocol ToBase()
        {
            var protocol = new Protocol();
            protocol.Id = Id;
            protocol.RequestId = RequestId;
            protocol.IssuedDate = IssuedDate;
            protocol.TesterMKB = TesterMKB;
            protocol.TesterFZH = TesterFZH;
            protocol.LabLeader = LabLeader;

            protocol.ProtocolResults = ProtocolResults;
            protocol.Request = Request;
            protocol.ProtocolsRemarks = ProtocolsRemarksA.Union(ProtocolsRemarksB).ToList();

            return protocol;
        }
    }
}