using System;
using System.Web.Mvc;
using RED.Filters;
using RED.Helpers;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewRequests)]
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

        [RoleFilter(FeaturesCollection.ModifyRequests)]
        public bool AcceptRequest(Guid requestId)
        {
            var result = _rep.AcceptRequest(requestId);
            return result;
        }

        [HttpGet]
        [RoleFilter(FeaturesCollection.ModifyRequests)]
        public PartialViewResult ConfirmDenyRequest(Guid requestId)
        {
            var request = _rep.GetRequest(requestId);
            return PartialView("Delete", request);
        }

        [HttpPost]
        [RoleFilter(FeaturesCollection.ModifyRequests)]
        public bool DenyRequest(Guid requestId)
        {
            var result = _rep.DenyRequest(requestId);
            return result;
        }
    }
}