using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class ProtocolsController : ControllerBase<ProtocolsRepository>
    {
        public ActionResult Index()
        {
            var protocols = Rep.GetProtocols();
            return View(protocols);
        }

        [HttpGet]
        public ActionResult Create(Guid requestId)
        {
            var request = Rep.GetRequest(requestId);
            ViewBag.request = request;
            
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProtocolW protocol)
        {
            Rep.Create(protocol);
            return RedirectToAction("Index", "Requests");
        }
    }
}