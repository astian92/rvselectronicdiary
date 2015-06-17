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
            //change later after we decide wether or not the requests will disappear after resolution
            var requests = Rep.GetCompletedRequests();
            return View(requests);
        }

        public ActionResult GetNotAcceptedRequests()
        {
            ViewBag.Label = "notAccepted";

            var requests = Rep.GetNotAcceptedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult GetAcceptedRequests()
        {
            ViewBag.Label = "accepted";

            var requests = Rep.GetAcceptedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult GetMyRequests()
        {
            ViewBag.Mine = true;
            ViewBag.Label = "mine";

            var requests = Rep.GetMyRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult GetCompletedRequests()
        {
            ViewBag.Label = "completed";

            var requests = Rep.GetCompletedRequests();
            return PartialView("Requests", requests);
        }

        public ActionResult GetArchivedRequests()
        {
            ViewBag.Label = "archived";

            var requests = Rep.GetArchivedRequests();
            return PartialView("ArchivedRequests", requests);
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