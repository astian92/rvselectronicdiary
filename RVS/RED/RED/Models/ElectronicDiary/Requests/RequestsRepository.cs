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
        public RequestW GetRequest(Guid id)
        {
            var request = db.Requests.Single(r => r.Id == id);
            return new RequestW(request);
        }

        public IEnumerable<RequestW> GetNotAcceptedRequests()
        {
            var notAccepted = db.Requests.Where(r => r.AcceptedBy == null && r.IsAccepted == false)
                .OrderByDescending(r => r.Date)
                .ToList();
            return notAccepted.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetAcceptedRequests()
        {
            var accepted = db.Requests.Where(r => r.AcceptedBy != null && r.IsAccepted == true && r.Protocols.Any() == false)
                .OrderByDescending(r => r.Date)
                .ToList();
            return accepted.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetMyRequests()
        {
            var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

            var myRequests = db.Requests.Where(r => r.IsAccepted == true &&
                        r.AcceptedBy == userId &&
                        r.Protocols.Any() == false) //that were not completed
                .OrderByDescending(r => r.Date)
                .ToList(); 
            return myRequests.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetCompletedRequests()
        {
            var requests = db.Requests.Where(r => r.Protocols.Any() == true)
                .OrderByDescending(r => r.Date)
                .ToList();
            return requests.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetArchivedRequests()
        {
            throw new NotImplementedException("Archived Requests are not yet implemented!");
        }

        public bool AcceptRequest(Guid requestId)
        {
            var issSuccess = false;
            try
            {
                var request = db.Requests.Single(r => r.Id == requestId);
                var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

                request.AcceptedBy = userId;
                request.IsAccepted = true;
                db.SaveChanges();

                issSuccess = true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                issSuccess = false;
            }

            return issSuccess;
        }

        public bool DenyRequest(Guid requestId)
        {
            var issSuccess = false;
            try
            {
                var request = db.Requests.Single(r => r.Id == requestId);
                //var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

                request.AcceptedBy = null;
                request.IsAccepted = false;
                db.SaveChanges();

                issSuccess = true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                issSuccess = false;
            }

            return issSuccess;
        }
    }
}