using System;
using System.Collections.Generic;
using System.Linq;
using RED.Mappings;
using RED.Models.Account;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Models.ElectronicDiary.Requests;
using RED.Repositories.Abstract;

namespace RED.Repositories.Concrete
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly RvsDbContext Db;

        public RequestsRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public RequestW GetRequest(Guid id)
        {
            var requestW = Db.Requests.Single(r => r.Id == id).ToRequestWrapper();
            return requestW;
        }

        public IEnumerable<RequestW> GetNotAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number))
                                      .Where(d => (from != null ? d.Date >= from.Value : true) && (to != null ? d.Date <= to.Value : true))
                                      .Where(r => r.AcceptedBy == null && r.IsAccepted == false);

            //Order and paging
            var notAcceptedRequests = requests.OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize).Select(RequestMappings.ToRequestW);
            return notAcceptedRequests;
        }

        public IEnumerable<RequestW> GetAcceptedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number))
                                      .Where(d => (from != null ? d.Date >= from.Value : true) && (to != null ? d.Date <= to.Value : true))
                                      .Where(r => r.AcceptedBy != null && r.IsAccepted == true && r.Protocols.Any() == false);

            //Order and paging
            var acceptedRequests = requests.OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize).Select(RequestMappings.ToRequestW);
            return acceptedRequests;
        }

        public IEnumerable<RequestW> GetMyRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number))
                                      .Where(d => (from != null ? d.Date >= from.Value : true) && (to != null ? d.Date <= to.Value : true))
                                      .Where(r => r.IsAccepted == true && r.AcceptedBy == RvsPrincipal.User.Id && r.Protocols.Any() == false); //that were not completed
            //Order and paging
            var myRequests = requests.OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize).Select(RequestMappings.ToRequestW);
            return myRequests;
        }

        public IEnumerable<RequestW> GetCompletedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.Requests.Where(d => d.Diary.Number == (number == -1 ? d.Diary.Number : number))
                                      .Where(d => (from != null ? d.Date >= from.Value : true) && (to != null ? d.Date <= to.Value : true))
                                      .Where(r => r.Protocols.Any() == true);

            //Order and paging
            var completedRequests = requests.OrderByDescending(r => r.Date).Skip((page - 1) * pageSize).Take(pageSize).Select(RequestMappings.ToRequestW);
            return completedRequests;
        }

        public IEnumerable<ArchivedRequest> GetArchivedRequests(int page = 1, int pageSize = 10, int number = -1, DateTime? from = null, DateTime? to = null)
        {
            //Filter
            var requests = Db.ArchivedDiaries.Where(d => d.Number == (number == -1 ? d.Number : number))
                                             .Where(d => (from != null ? d.RequestDate >= from.Value : true)
                                                      && (to != null ? d.RequestDate <= to.Value : true));

            //Order and paging
            var pagedRequests = requests.OrderByDescending(r => r.RequestDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedRequests.Select(ad => new ArchivedRequest(ad));
        }

        public bool AcceptRequest(Guid requestId)
        {
            var issSuccess = false;
            try
            {
                var request = Db.Requests.Single(r => r.Id == requestId);

                request.AcceptedBy = RvsPrincipal.User.Id;
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