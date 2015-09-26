using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Remarks;
using RED.Models.ElectronicDiary.Requests;
using RED.Models.FileModels;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
            var acreditedLevel = db.AcredetationLevels.Single(al => al.Level.Trim() == AcredetationLevels.Acredited);
            foreach (var remark in protocolW.ProtocolsRemarksA)
            {
                remark.AcredetationLevelId = acreditedLevel.Id;
                remark.Remark = db.Remarks.Single(r => r.Id == remark.RemarkId);
            }
            var notAcreditedLevel = db.AcredetationLevels.Single(al => al.Level.Trim() == AcredetationLevels.NotAcredited);
            foreach (var remark in protocolW.ProtocolsRemarksB)
            {
                remark.AcredetationLevelId = notAcreditedLevel.Id;
                remark.Remark = db.Remarks.Single(r => r.Id == remark.RemarkId);
            }

            var protocol = protocolW.ToBase();
            protocol.IssuedDate = DateTime.Now.ToUniversalTime();
            db.Protocols.Add(protocol);
            var request = db.Requests.Single(r => r.Id == protocol.RequestId);
            GeneratePorotocolReport(protocol, request);

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
            db.ProtocolsRemarks.RemoveRange(protocol.ProtocolsRemarks);
            protocol.ProtocolsRemarks.Clear();
            var acreditedLevel = db.AcredetationLevels.Single(al => al.Level.Trim() == AcredetationLevels.Acredited);
            foreach (var item in protocolW.ProtocolsRemarksA)
            {
                item.AcredetationLevelId = acreditedLevel.Id;
                //for some reason the saveChanges doesnt populate the Remark object in the ProtocolsRemark and its
                //needed inside the protocol generating
                item.Remark = db.Remarks.Single(r => r.Id == item.RemarkId); 
                protocol.ProtocolsRemarks.Add(item);
            }
            var notAcreditedLevel = db.AcredetationLevels.Single(al => al.Level.Trim() == AcredetationLevels.NotAcredited);
            foreach (var item in protocolW.ProtocolsRemarksB)
            {
                item.AcredetationLevelId = notAcreditedLevel.Id;
                //for some reason the saveChanges doesnt populate the Remark object in the ProtocolsRemark and its
                //needed inside the protocol generating
                item.Remark = db.Remarks.Single(r => r.Id == item.RemarkId);
                protocol.ProtocolsRemarks.Add(item);
            }

            protocol.Tester = protocolW.Tester;
            protocol.LabLeader = protocolW.LabLeader;

            using (TransactionScope scope = new TransactionScope())
            {
                db.SaveChanges();
                GeneratePorotocolReport(protocol);

                scope.Complete();
            }
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

        public void GeneratePorotocolReport(Protocol protocol, Request request = null)
        {
            var filesRep = new FilesRepository();
            filesRep.GenerateProtocolReport(protocol, request);
        }

        public IEnumerable<RemarkW> GetRemarks()
        {
            RemarksRepository RRep = new RemarksRepository();
            return RRep.GetRemarks();
        }
    }
}