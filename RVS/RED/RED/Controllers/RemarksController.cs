using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Remarks;
using RED.Models.ControllerBases;
using RED.Filters;

namespace RED.Controllers
{
    [RoleFilter("54471a7f-866f-4ccd-8501-ec6e08c7f052")]
    public class RemarksController : ControllerBase<RemarksRepository>
    {
        public ActionResult Index()
        {
            var remarks = Rep.GetRemarks();
            return View(remarks);
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
                var isCreated = Rep.Create(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Edit(Guid id)
        {
            var remark = Rep.GetRemark(id);
            return View(remark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Edit(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isEdited = Rep.Edit(remark);
                return RedirectToAction("Index");
            }
            return View(remark);
        }

        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult Delete(Guid id)
        {
            var remark = Rep.GetRemark(id);

            if (Request.IsAjaxRequest())
                return PartialView(remark);
            return View(remark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("95342ca3-d105-4e5e-9b37-d7205afd463e")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var isDeleted = Rep.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
