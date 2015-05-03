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
        public IEnumerable<ProtocolW> GetProtocols()
        {
            var protocols = db.Protocols.ToList();
            var result = protocols.Select(p => new ProtocolW(p));

            return result;
        }

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