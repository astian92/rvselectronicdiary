using System;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Helpers;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Protocols;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewProtocols)]
    public class ProtocolsController : BaseController
    {
        private readonly IProtocolsRepository _rep;

        public ProtocolsController(IProtocolsRepository protocolsRepo)
        {
            _rep = protocolsRepo;
        }

        public ActionResult Index(Guid? idToOpen, bool IsArchived = false)
        {
            ViewBag.IsArchived = IsArchived;
            ViewBag.IdToOpen = idToOpen;
            return View();
        }

        public ActionResult FilterActiveProtocols(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "active-protocols";
            ViewBag.page = page;

            var protocols = _rep.GetActiveProtocols(page, pageSize, number, fromDate, toDate);
            return PartialView("ActiveProtocols", protocols);
        }

        public ActionResult FilterArchivedProtocols(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "archived-protocols";
            ViewBag.page = page;

            var protocols = _rep.GetArchivedProtocols(page, pageSize, number, fromDate, toDate);
            return PartialView("ArchivedProtocols", protocols);
        }

        [HttpGet]
        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult Create(Guid requestId)
        {
            var request = _rep.GetRequest(requestId);

            ViewBag.RemarkId = new SelectList(_rep.GetRemarks(), "Id", "Text");

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult Create(ProtocolW protocol)
        {
            _rep.Create(protocol);
            return RedirectToAction("Index", "Requests");
        }

        [HttpGet]
        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult Edit(Guid protocolId)
        {
            var protocol = _rep.GetProtocol(protocolId);
            ViewBag.RemarkId = new SelectList(_rep.GetRemarks(), "Id", "Text");
            return View(protocol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult Edit(ProtocolW protocol)
        {
            if (ModelState.IsValid)
            {
                _rep.EditProtocol(protocol);
                return RedirectToAction("Index");
            }

            ViewBag.RemarkId = new SelectList(_rep.GetRemarks(), "Id", "Text");
            return View(protocol);
        }

        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProtocolW protocol = _rep.GetProtocol(id.Value);
            if (Request.IsAjaxRequest())
            {
                return PartialView(protocol);
            }

            return View(protocol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyProtocols)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdelete = _rep.Delete(id);

            if (isdelete)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Protocols/Index" });
        }
    }
}