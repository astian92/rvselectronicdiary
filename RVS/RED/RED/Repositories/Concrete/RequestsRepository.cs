using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RED.Models.Account;
using RED.Models.RepositoryBases;
using RED.Models.ElectronicDiary.Requests;
using RED.Repositories.Abstract;

namespace RED.Repositories.Concrete
{
    public class RequestsRepository : RepositoryBase, IRequestsRepository
    {
        public RequestW GetRequest(Guid id)
        {
            var request = Db.Requests.Single(r => r.Id == id);
            return new RequestW(request);
        }

        public IEnumerable<RequestW> GetNotAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number));
            requests = requests.Where(d => d.Date >= (from == null ? d.Date : from.Value) &&
                                                   d.Date <= (to == null ? d.Date : to.Value));

            //Order and paging
            var notAccepted = requests.Where(r => r.AcceptedBy == null && r.IsAccepted == false)
                .OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList();
            return notAccepted.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number));
            requests = requests.Where(d => d.Date >= (from == null ? d.Date : from.Value) &&
                                                   d.Date <= (to == null ? d.Date : to.Value));

            //Order and paging
            var accepted = requests.Where(r => r.AcceptedBy != null && r.IsAccepted == true && r.Protocols.Any() == false)
                .OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList();
            return accepted.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetMyRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number));
            requests = requests.Where(d => d.Date >= (from == null ? d.Date : from.Value) &&
                                                   d.Date <= (to == null ? d.Date : to.Value));

            //Order and paging
            var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

            var myRequests = requests.Where(r => r.IsAccepted == true &&
                        r.AcceptedBy == userId &&
                        r.Protocols.Any() == false) //that were not completed
                .OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList(); 
            return myRequests.Select(r => new RequestW(r));
        }

        public IEnumerable<RequestW> GetCompletedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number));
            requests = requests.Where(d => d.Date >= (from == null ? d.Date : from.Value) &&
                                                   d.Date <= (to == null ? d.Date : to.Value));

            //Order and paging
            var completed = requests.Where(r => r.Protocols.Any() == true)
                .OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize)
                .ToList();
            return completed.Select(r => new RequestW(r));
        }

        public IEnumerable<ArchivedRequest> GetArchivedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.ArchivedDiaries.Where(d => d.Number == (number == -1 ? d.Number : number));
            requests = requests.Where(d => d.RequestDate >= (from == null ? d.RequestDate : from.Value) &&
                                                   d.RequestDate <= (to == null ? d.RequestDate : to.Value));

            //Order and paging
            var archivedDiaries = requests.OrderByDescending(r => r.RequestDate).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return archivedDiaries.Select(ad => new ArchivedRequest(ad));
        }

        public bool AcceptRequest(Guid requestId)
        {
            var issSuccess = false;
            try
            {
                var request = Db.Requests.Single(r => r.Id == requestId);
                var userId = ((RvsPrincipal)HttpContext.Current.User).GetId();

                request.AcceptedBy = userId;
                request.IsAccepted = true;
                Db.SaveChanges();

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
                var request = Db.Requests.Single(r => r.Id == requestId);

                request.AcceptedBy = null;
                request.IsAccepted = false;
                Db.SaveChanges();

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