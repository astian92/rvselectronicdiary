using System;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Protocols;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("93b1ccf0-c462-464a-9294-524e5088b93b")]
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
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Create(Guid requestId)
        {
            var request = _rep.GetRequest(requestId);

            ViewBag.RemarkId = new SelectList(_rep.GetRemarks(), "Id", "Text");

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Create(ProtocolW protocol)
        {
            _rep.Create(protocol);
            return RedirectToAction("Index", "Requests");
        }

        [HttpGet]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Edit(Guid protocolId)
        {
            var protocol = _rep.GetProtocol(protocolId);
            ViewBag.RemarkId = new SelectList(_rep.GetRemarks(), "Id", "Text");
            return View(protocol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
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

        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
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
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
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