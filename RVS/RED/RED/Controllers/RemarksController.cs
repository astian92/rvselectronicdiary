using System;
using System.Web.Mvc;
using RED.Models.ElectronicDiary.Remarks;
using RED.Models.ControllerBases;
using RED.Filters;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("54471a7f-866f-4ccd-8501-ec6e08c7f052")]
    public class RemarksController : BaseController
    {
        private readonly IRemarksRepository _rep;

        public RemarksController(IRemarksRepository remarksRepo)
        {
            _rep = remarksRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRemarks()
        {
            var remarks = _rep.GetRemarks();
            return Json(new { data = remarks });
        }

        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Create(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _rep.Create(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Edit(Guid id)
        {
            var remark = _rep.GetRemark(id);
            return View(remark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Edit(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isEdited = _rep.Edit(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Delete(Guid id)
        {
            var remark = _rep.GetRemark(id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(remark);
            }

            return View(remark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isDeleted = _rep.Delete(id);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Remarks/Index" });
        }
    }
}
