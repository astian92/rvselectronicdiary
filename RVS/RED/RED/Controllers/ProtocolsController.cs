using System;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Protocols;

namespace RED.Controllers
{
    [RoleFilter("93b1ccf0-c462-464a-9294-524e5088b93b")]
    public class ProtocolsController : ControllerBase<ProtocolsRepository>
    {
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

            var protocols = Rep.GetActiveProtocols(page, pageSize, number, fromDate, toDate);
            return PartialView("ActiveProtocols", protocols);
        }

        public ActionResult FilterArchivedProtocols(int page, int pageSize, int number, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.Label = "archived-protocols";
            ViewBag.page = page;

            var protocols = Rep.GetArchivedProtocols(page, pageSize, number, fromDate, toDate);
            return PartialView("ArchivedProtocols", protocols);
        }

        [HttpGet]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Create(Guid requestId)
        {
            var request = Rep.GetRequest(requestId);

            ViewBag.RemarkId = new SelectList(Rep.GetRemarks(), "Id", "Text");

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Create(ProtocolW protocol)
        {
            Rep.Create(protocol);
            return RedirectToAction("Index", "Requests");
        }

        [HttpGet]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Edit(Guid protocolId)
        {
            var protocol = Rep.GetProtocol(protocolId);
            ViewBag.RemarkId = new SelectList(Rep.GetRemarks(), "Id", "Text");
            return View(protocol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult Edit(ProtocolW protocol)
        {
            if (ModelState.IsValid)
            {
                Rep.EditProtocol(protocol);
                return RedirectToAction("Index");
            }

            ViewBag.RemarkId = new SelectList(Rep.GetRemarks(), "Id", "Text");
            return View(protocol);
        }

        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
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
        [RoleFilter("b3a0ca2d-428d-4f12-8b93-fc227350fc2c")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdelete = Rep.Delete(id);

            if (isdelete)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Protocols/Index" });
        }
    }
}