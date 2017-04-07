using System;
using System.Web.Mvc;
using RED.Models.ElectronicDiary.Remarks;
using RED.Models.ControllerBases;
using RED.Filters;
using RED.Repositories.Abstract;
using RED.Helpers;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewRemarks)]
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

        [RoleFilter(FeaturesCollection.ModifyRemarks)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyRemarks)]
        public ActionResult Create(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _rep.Create(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        [RoleFilter(FeaturesCollection.ModifyRemarks)]
        public ActionResult Edit(Guid id)
        {
            var remark = _rep.GetRemark(id);
            return View(remark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyRemarks)]
        public ActionResult Edit(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isEdited = _rep.Edit(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        [RoleFilter(FeaturesCollection.ModifyRemarks)]
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
        [RoleFilter(FeaturesCollection.ModifyRemarks)]
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
