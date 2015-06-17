﻿using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            //ViewBag.request = request;
            
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProtocolW protocol)
        {
            Rep.Create(protocol);
            return RedirectToAction("Index", "Requests");
        }

        [HttpGet]
        public ActionResult Edit(Guid protocolId)
        {
            var protocol = Rep.GetProtocol(protocolId);
            
            return View(protocol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProtocolW protocol)
        {
            if (ModelState.IsValid)
            {
                Rep.EditProtocol(protocol);
                return RedirectToAction("Index");
            }

            return View(protocol);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProtocolW protocol = Rep.GetProtocol(id.Value);
            if (Request.IsAjaxRequest())
            {
                return PartialView(protocol);
            }

            return View(protocol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Rep.Delete(id);
            return RedirectToAction("Index");
        }
    }
}