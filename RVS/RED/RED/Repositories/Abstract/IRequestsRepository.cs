using System;
using System.Collections.Generic;
using RED.Models.ElectronicDiary.Requests;

namespace RED.Repositories.Abstract
{
    public interface IRequestsRepository
    {
        bool AcceptRequest(Guid requestId);

        bool DenyRequest(Guid requestId);

        IEnumerable<RequestW> GetAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<ArchivedRequest> GetArchivedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<RequestW> GetCompletedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<RequestW> GetMyRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<RequestW> GetNotAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        RequestW GetRequest(Guid id);
    }
}