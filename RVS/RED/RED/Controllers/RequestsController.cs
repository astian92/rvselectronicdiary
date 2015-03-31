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
            var requests = Rep.GetRequests();
            return View(requests);
        }
    }
}