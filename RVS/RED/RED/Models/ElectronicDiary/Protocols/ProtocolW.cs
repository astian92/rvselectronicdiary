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

        public ICollection<ProtocolResult> ProtocolResults { get; set; }
        public Request Request { get; set; }

        public ProtocolW()
        {
            this.ProtocolResults = new List<ProtocolResult>();
        }

        public ProtocolW(Protocol protocol)
        {
            this.Id = protocol.Id;
            this.RequestId = protocol.Id;
            this.IssuedDate = protocol.IssuedDate;

            this.ProtocolResults = protocol.ProtocolResults;
            this.Request = protocol.Request;
        }

        public Protocol ToBase()
        {
            var protocol = new Protocol();

            protocol.Id = this.Id;
            protocol.RequestId = this.RequestId;
            protocol.IssuedDate = this.IssuedDate;

            protocol.ProtocolResults = this.ProtocolResults;
            protocol.Request = this.Request;

            return protocol;
        }
    }
}