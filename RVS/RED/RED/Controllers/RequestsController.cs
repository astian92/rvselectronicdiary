using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class RequestsController : ControllerBase<RequestsRepository>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNotAcceptedRequests()
        {
            ViewBag.Label = "notAccepted";

            var requests = Rep.GetNotAcceptedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult FilterNotAcceptedRequests(int page, int pageSize,
            int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "notAccepted";

            var requests = Rep.GetNotAcceptedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult GetAcceptedRequests()
        {
            ViewBag.Label = "accepted";

            var requests = Rep.GetAcceptedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult FilterAcceptedRequests(int page, int pageSize,
            int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "accepted";

            if(toDate != null)
                toDate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 23, 59, 59);

            var requests = Rep.GetAcceptedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult GetMyRequests()
        {
            ViewBag.Mine = true;
            ViewBag.Label = "mine";

            var requests = Rep.GetMyRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult FilterMyRequests(int page, int pageSize,
            int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Mine = true;
            ViewBag.Label = "mine";

            var requests = Rep.GetMyRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult GetCompletedRequests()
        {
            ViewBag.Label = "completed";

            var requests = Rep.GetCompletedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult FilterCompletedRequests(int page, int pageSize,
            int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "completed";

            var requests = Rep.GetCompletedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public ActionResult GetArchivedRequests()
        {
            ViewBag.Label = "archived";

            var requests = Rep.GetArchivedRequests();
            return PartialView("ArchivedRequests", requests);
        }

        public ActionResult FilterArchivedRequests(int page, int pageSize,
            int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "archived";

            var requests = Rep.GetArchivedRequests(page, pageSize, number, fromDate, toDate);
            return PartialView("Requests", requests);
        }

        public bool AcceptRequest(Guid requestId)
        {
            var result = Rep.AcceptRequest(requestId);
            return result;
        }

        [HttpGet]
        public PartialViewResult ConfirmDenyRequest(Guid requestId)
        {
            var request = Rep.GetRequest(requestId);
            return PartialView("Delete", request);
        }

        [HttpPost]
        public bool DenyRequest(Guid requestId)
        {
            var result = Rep.DenyRequest(requestId);
            return result;
        }
    }
}