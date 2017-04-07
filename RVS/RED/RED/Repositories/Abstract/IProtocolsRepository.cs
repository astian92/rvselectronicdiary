using System;
using System.Collections.Generic;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Remarks;
using RED.Models.ElectronicDiary.Requests;
using RED.Models.ElectronicDiary.Protocols;

namespace RED.Repositories.Abstract
{
    public interface IProtocolsRepository
    {
        void Create(ProtocolW protocolW);

        bool Delete(Guid protocolId);

        void EditProtocol(ProtocolW protocolW);

        void GeneratePorotocolReport(Protocol protocol, Request request = null);

        IEnumerable<ProtocolW> GetActiveProtocols(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<ArchivedProtocol> GetArchivedProtocols(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        ProtocolW GetProtocol(Guid protocolId);

        IEnumerable<RemarkW> GetRemarks();

        RequestW GetRequest(Guid id);
    }
}