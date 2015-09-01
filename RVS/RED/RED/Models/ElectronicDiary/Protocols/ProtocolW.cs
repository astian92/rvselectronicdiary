using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Protocols
{
    public class ProtocolW
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Tester { get; set; }
        public string LabLeader { get; set; }

        public ICollection<ProtocolResult> ProtocolResults { get; set; }
        public Request Request { get; set; }
        //public ICollection<ProtocolsRemark> ProtocolsRemarks { get; set; }
        public ICollection<ProtocolsRemark> ProtocolsRemarksA { get; set; }
        public ICollection<ProtocolsRemark> ProtocolsRemarksB { get; set; }

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
            this.IssuedDate = protocol.IssuedDate;
            this.Tester = protocol.Tester;
            this.LabLeader = protocol.LabLeader;

            this.ProtocolResults = protocol.ProtocolResults;
            this.Request = protocol.Request;
            this.ProtocolsRemarksA = protocol.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcredetationLevels.Acredited).ToList();
            this.ProtocolsRemarksB = protocol.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcredetationLevels.NotAcredited).ToList();
        }

        public Protocol ToBase()
        {
            var protocol = new Protocol();

            protocol.Id = this.Id;
            protocol.RequestId = this.RequestId;
            protocol.IssuedDate = this.IssuedDate;
            protocol.Tester = this.Tester;
            protocol.LabLeader = this.LabLeader;

            protocol.ProtocolResults = this.ProtocolResults;
            protocol.Request = this.Request;
            protocol.ProtocolsRemarks = this.ProtocolsRemarksA.Union(this.ProtocolsRemarksB).ToList();

            return protocol;
        }
    }
}