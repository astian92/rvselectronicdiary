using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Requests;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Protocols
{
    public class ProtocolsRepository : RepositoryBase
    {
        public RequestW GetRequest(Guid id)
        {
            var request = db.Requests.Single(r => r.Id == id);
            return new RequestW(request);
        }

        public void Create(ProtocolW protocolW)
        {
            var protocol = protocolW.ToBase();
            protocol.IssuedDate = DateTime.Now.ToUniversalTime();
            db.Protocols.Add(protocol);

            db.SaveChanges();
        }
    }
}