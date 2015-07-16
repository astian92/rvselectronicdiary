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
        public IEnumerable<ProtocolW> GetActiveProtocols(int page = 1, int pageSize = 10,
            int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var protocols = db.Protocols.Where(d => d.Request.Diary.Number == (number == -1 ? d.Request.Diary.Number : number));
            protocols = protocols.Where(d => d.IssuedDate >= (from == null ? d.IssuedDate : from.Value) &&
                                                   d.IssuedDate <= (to == null ? d.IssuedDate : to.Value));

            //Order and paging
            var activeProtocols = protocols
                .OrderByDescending(p => p.IssuedDate).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList();

            var result = activeProtocols.Select(p => new ProtocolW(p));

            return result;
        }

        public IEnumerable<ArchivedProtocol> GetArchivedProtocols(int page = 1, int pageSize = 10,
            int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var protocols = db.ArchivedDiaries.Where(d => d.Number == (number == -1 ? d.Number : number));
            protocols = protocols.Where(d => d.ProtocolIssuedDate >= (from == null ? d.ProtocolIssuedDate : from.Value) &&
                                                   d.ProtocolIssuedDate <= (to == null ? d.ProtocolIssuedDate : to.Value));

            //Order and paging
            var adiaries = protocols
                .OrderByDescending(p => p.ProtocolIssuedDate).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList();
            var result = adiaries.Select(ad => new ArchivedProtocol(ad));

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

        public bool Delete(Guid protocolId)
        {
            var protocol = db.Protocols.Single(p => p.Id == protocolId);
            db.Protocols.Remove(protocol);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}