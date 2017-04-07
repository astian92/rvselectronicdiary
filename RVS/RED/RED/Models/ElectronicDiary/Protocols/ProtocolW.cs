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
            this.ProtocolResults = new List<ProtocolResult>();
            this.ProtocolsRemarksA = new List<ProtocolsRemark>();
            this.ProtocolsRemarksB = new List<ProtocolsRemark>();
        }

        public ProtocolW(Protocol protocol)
        {
            this.Id = protocol.Id;
            this.RequestId = protocol.Id;
            this.IssuedDate = protocol.IssuedDate.ToLocalTime();
            this.TesterMKB = protocol.TesterMKB;
            this.TesterFZH = protocol.TesterFZH;
            this.LabLeader = protocol.LabLeader;

            this.ProtocolResults = protocol.ProtocolResults;
            this.Request = protocol.Request;
            this.ProtocolsRemarksA = protocol.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.Acredited).ToList();
            this.ProtocolsRemarksB = protocol.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.NotAcredited).ToList();
        }

        public Guid Id { get; set; }

        public Guid RequestId { get; set; }

        public DateTime IssuedDate { get; set; }

        public string TesterMKB { get; set; }

        public string TesterFZH { get; set; }

        public string LabLeader { get; set; }

        public ICollection<ProtocolResult> ProtocolResults { get; set; }

        public Request Request { get; set; }

        public ICollection<ProtocolsRemark> ProtocolsRemarksA { get; set; }

        public ICollection<ProtocolsRemark> ProtocolsRemarksB { get; set; }

        public string IssuedTime
        {
            get
            {
                return this.IssuedDate.ToString("HH:mm");
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var arguments = value.Split(':');
                    var hours = int.Parse(arguments[0]);
                    var minutes = int.Parse(arguments[1]);

                    this.IssuedDate = this.IssuedDate.AddHours(hours);
                    this.IssuedDate = this.IssuedDate.AddMinutes(minutes);
                }
            }
        }

        public Protocol ToBase()
        {
            var protocol = new Protocol();

            protocol.Id = this.Id;
            protocol.RequestId = this.RequestId;
            protocol.IssuedDate = this.IssuedDate.ToUniversalTime();
            protocol.TesterMKB = this.TesterMKB;
            protocol.TesterFZH = this.TesterFZH;
            protocol.LabLeader = this.LabLeader;

            protocol.ProtocolResults = this.ProtocolResults;
            protocol.Request = this.Request;
            protocol.ProtocolsRemarks = this.ProtocolsRemarksA.Union(this.ProtocolsRemarksB).ToList();

            return protocol;
        }
    }
}