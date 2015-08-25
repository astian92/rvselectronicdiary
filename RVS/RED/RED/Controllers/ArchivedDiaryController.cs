using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.ArchivedWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class ArchivedDiaryController : ControllerBase<ArchivedDiaryRepository>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var archivedDiary = Rep.GetArchivedDiaryW(id);
            return View(archivedDiary);
        }

        [HttpPost]
        public ActionResult Edit(ArchivedDiaryW adiary)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(adiary);
                return RedirectToAction("Index", "Diary");
            }

            return View(adiary);
        }

        public ActionResult ProductsIndex(Guid archivedDiaryId)
        {
            var adiary = Rep.GetArchivedDiary(archivedDiaryId);
            ViewBag.ADiaryNumber = adiary.Number;
            ViewBag.ArchivedDiaryId = archivedDiaryId;
            return View();
        }

        public JsonResult GetProducts(Guid archivedDiaryId)
        {
            var products = Rep.GetProducts(archivedDiaryId).OrderBy(p => p.Number);
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateProduct(Guid archivedDiaryId)
        {
            ViewBag.ADiaryId = archivedDiaryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ArchivedProductW aproduct)
        {
            if (ModelState.IsValid)
            {
                Rep.AddProduct(aproduct);
                return RedirectToAction("ProductsIndex", new { archivedDiaryId = aproduct.ArchivedDiaryId });
            }

            //ModelState.AddModelError("ErrorExists", "Клиент с това име вече съществува. Моля опитайте друго име.");

            return View(aproduct);
        }

        [HttpGet]
        public ActionResult EditProduct(Guid id)
        {
            var aproduct = Rep.GetArchivedProductW(id);
            ViewBag.ADiaryId = aproduct.ArchivedDiaryId;
            return View(aproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ArchivedProductW aproduct)
        {
            if (ModelState.IsValid)
            {
                Rep.EditProduct(aproduct);
                return RedirectToAction("ProductsIndex", new { archivedDiaryId = aproduct.ArchivedDiaryId });
            }

            return View(aproduct);
        }

        [HttpGet]
        public ActionResult DeleteProduct(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aproduct = Rep.GetArchivedProductW(id.Value);
            if (aproduct == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(aproduct);
            }

            return View(aproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(Guid id)
        {
            var product = Rep.GetArchivedProduct(id);
            bool isdeleted = Rep.DeleteProduct(id);

            if (isdeleted)
            {
                return RedirectToAction("ProductsIndex", new { archivedDiaryId = product.ArchivedDiaryId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProductsIndex" });
        }

        public ActionResult ProductTestsIndex(Guid aproductId)
        {
            var aproduct = Rep.GetArchivedProductW(aproductId);
            ViewBag.ArchivedProductId = aproductId;
            ViewBag.AProductName = aproduct.Name;

            return View();
        }

        public JsonResult GetProductTests(Guid aproductId)
        {
            var products = Rep.GetProductTests(aproductId);
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

    }
}