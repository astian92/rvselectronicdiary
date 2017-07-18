using System;
using System.Linq;
using System.Linq.Expressions;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.Protocols;

namespace RED.Mappings
{
    public static class ProtocolMappings
    {
        public static readonly Expression<Func<Protocol, ProtocolW>> ToProtocolW =
            entity => new ProtocolW()
            {
                Id = entity.Id,
                RequestId = entity.Id,
                IssuedDate = entity.IssuedDate,
                TesterMKB = entity.TesterMKB,
                TesterFZH = entity.TesterFZH,
                LabLeader = entity.LabLeader,
                ProtocolResults = entity.ProtocolResults,
                Request = entity.Request,
                ProtocolsRemarksA = entity.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.Acredited),
                ProtocolsRemarksB = entity.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.NotAcredited)
            };

        public static ProtocolW ToProtocolWrapper(this Protocol entity)
        {
            var protocolW = new ProtocolW()
            {
                Id = entity.Id,
                RequestId = entity.Id,
                IssuedDate = entity.IssuedDate,
                TesterMKB = entity.TesterMKB,
                TesterFZH = entity.TesterFZH,
                LabLeader = entity.LabLeader,
                ProtocolResults = entity.ProtocolResults,
                Request = entity.Request,
                ProtocolsRemarksA = entity.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.Acredited),
                ProtocolsRemarksB = entity.ProtocolsRemarks.Where(pr => pr.AcredetationLevel.Level.Trim() == AcreditationLevels.NotAcredited)
            };

            return protocolW;
        }
    }
}