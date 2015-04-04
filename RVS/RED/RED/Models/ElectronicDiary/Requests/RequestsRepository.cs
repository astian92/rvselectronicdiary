using RED.Models.Account;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Requests
{
    public class RequestsRepository : RepositoryBase
    {
        public RequestW GetRequst(Guid id)
        {
            var request = db.Requests.Single(r => r.Id == id);
            return new RequestW(request);
        }

        public IEnumerable<RequestW> GetNotAcceptedRequests()
        {
            var notAccepted = db.Requests.Where(r => r.AcceptedBy == null && r.IsAccepted == false).ToList();
            return notAccepted.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetMyRequests()
        {
            var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

            var myRequests = db.Requests.Where(r => r.IsAccepted == true &&
                r.AcceptedBy == userId &&
                r.Protocols.Any() == false).ToList(); //that were not completed
            return myRequests.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetCompletedRequests()
        {
            var requests = db.Requests.Where(r => r.Protocols.Any() == true).ToList();
            return requests.Select(r => new RequestW(r));
        }
    }
}