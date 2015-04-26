﻿using RED.Models.ControllerBases;
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

        public ActionResult GetAllRequests()
        {
            ViewBag.Label = "all";

            var requests = Rep.GetAllRequests();
            return PartialView("Requests", requests);
        }

        public bool AcceptRequest(Guid requestId)
        {
            var result = Rep.AcceptRequest(requestId);
            return result;
        }
    }
}