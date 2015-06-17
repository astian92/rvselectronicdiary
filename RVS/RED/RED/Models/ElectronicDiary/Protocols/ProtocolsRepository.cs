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

        public ProtocolW GetProtocol(Guid protocolId)
        {
            var protocol = db.Protocols.Single(x => x.Id == protocolId);
            return new ProtocolW(protocol);
        }

        public void EditProtocol(ProtocolW protocolW)
        {
            var protocol = db.Protocols.Single(p => p.Id == protocolW.Id);

            db.ProtocolResults.RemoveRange(protocol.ProtocolResults);
            protocol.ProtocolResults.Clear();
            foreach (var item in protocolW.ProtocolResults)
            {
                protocol.ProtocolResults.Add(item);
            }

            db.SaveChanges();
        }

        public void Delete(Guid protocolId)
        {
            var protocol = db.Protocols.Single(p => p.Id == protocolId);
            db.Protocols.Remove(protocol);

            db.SaveChanges();
        }
    }
}