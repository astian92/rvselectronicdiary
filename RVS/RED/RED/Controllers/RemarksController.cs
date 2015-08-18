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

namespace RED.Controllers
{
    public class RemarksController : ControllerBase<RemarksRepository>
    {
        public ActionResult Index()
        {
            var remarks = Rep.GetRemarks();
            return View(remarks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isCreated = Rep.Create(remark);
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        public ActionResult Edit(Guid id)
        {
            var remark = Rep.GetRemark(id);
            return View(remark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RemarkW remark)
        {
            if (ModelState.IsValid)
            {
                var isEdited = Rep.Edit(remark);
                return RedirectToAction("Index");
            }
            return View(remark);
        }

        public ActionResult Delete(Guid id)
        {
            var remark = Rep.GetRemark(id);

            if (Request.IsAjaxRequest())
                return PartialView(remark);
            return View(remark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var isDeleted = Rep.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
