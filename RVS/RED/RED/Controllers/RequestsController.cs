using System;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("b93941bf-aa40-490e-9764-5aea1841de32")]
    public class RequestsController : BaseController
    {
        private readonly IRequestsRepository _rep;

        public RequestsController(IRequestsRepository requestsRepo)
        {
            _rep = requestsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilterNotAcceptedRequests(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "notAccepted";
            ViewBag.page = page;

            var requests = _rep.GetNotAcceptedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult FilterAcceptedRequests(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "accepted";
            ViewBag.page = page;

            if (toDate != null)
            {
                toDate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 23, 59, 59);
            }

            var requests = _rep.GetAcceptedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult FilterMyRequests(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Mine = true;
            ViewBag.Label = "mine";
            ViewBag.page = page;

            var requests = _rep.GetMyRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }
        
        public ActionResult FilterCompletedRequests(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "completed";
            ViewBag.page = page;

            var requests = _rep.GetCompletedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult FilterArchivedRequests(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "archived";
            ViewBag.page = page;

            var requests = _rep.GetArchivedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("ArchivedRequests", requests);
        }

        [RoleFilter("4a6fd1e4-7720-4385-841a-d33a58c3130a")]
        public bool AcceptRequest(Guid requestId)
        {
            var result = _rep.AcceptRequest(requestId);
            return result;
        }

        [HttpGet]
        [RoleFilter("4a6fd1e4-7720-4385-841a-d33a58c3130a")]
        public PartialViewResult ConfirmDenyRequest(Guid requestId)
        {
            var request = _rep.GetRequest(requestId);
            return PartialView("Delete", request);
        }

        [HttpPost]
        [RoleFilter("4a6fd1e4-7720-4385-841a-d33a58c3130a")]
        public bool DenyRequest(Guid requestId)
        {
            var result = _rep.DenyRequest(requestId);
            return result;
        }
    }
}