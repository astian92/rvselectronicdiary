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
            //extensions test
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

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProductsIndex?archivedDiaryId=" + product.ArchivedDiaryId });
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

        [HttpGet]
        public ActionResult CreateProductTest(Guid aproductId)
        {
            ViewBag.TestAcredetationLevel = new SelectList(Rep.GetPossibleAcredetationLevels(), "Level", "Level");
            ViewBag.AProductId = aproductId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductTest(ArchivedProductTestW aproductTest)
        {
            if (ModelState.IsValid)
            {
                Rep.AddProductTest(aproductTest);
                return RedirectToAction("ProductTestsIndex", new { aproductId = aproductTest.ArchivedProductId });
            }

            return View(aproductTest);
        }

        [HttpGet]
        public ActionResult EditProductTest(Guid aproductTestId)
        {
            var aProductTest = Rep.GetArchivedProductTestW(aproductTestId);
            ViewBag.TestAcredetationLevel = new SelectList(Rep.GetPossibleAcredetationLevels(), "Level", "Level", aProductTest.TestAcredetationLevel);
            ViewBag.AProductId = aProductTest.ArchivedProductId;
            return View(aProductTest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductTest(ArchivedProductTestW aproductTest)
        {
            if (ModelState.IsValid)
            {
                Rep.EditProductTest(aproductTest);
                return RedirectToAction("ProductTestsIndex", new { aproductId = aproductTest.ArchivedProductId });
            }

            return View(aproductTest);
        }

        [HttpGet]
        public ActionResult DeleteProductTest(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aproductTest = Rep.GetArchivedProductTestW(id.Value);
            if (aproductTest == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(aproductTest);
            }

            return View(aproductTest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductTest(Guid id)
        {
            var productTest = Rep.GetArchivedProductTest(id);
            bool isdeleted = Rep.DeleteProductTest(id);

            if (isdeleted)
            {
                return RedirectToAction("ProductTestsIndex", new { aproductId = productTest.ArchivedProductId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProductTestsIndex?aproductId=" + productTest.ArchivedProductId });
        }

        public ActionResult ProtocolResultsIndex(Guid aproductTestId)
        {
            var aproductTest = Rep.GetArchivedProductTestW(aproductTestId);
            ViewBag.ArchivedProductTestId = aproductTestId;
            ViewBag.AProductTestName = aproductTest.TestName;

            return View();
        }

        public JsonResult GetProtocolResults(Guid aproductTestId)
        {
            var products = Rep.GetProtocolResults(aproductTestId);
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProtocolResult(Guid aproductTestId)
        {
            var productTest = Rep.GetArchivedProductTestW(aproductTestId);
            var product = Rep.GetArchivedProductW(productTest.ArchivedProductId);

            ViewBag.ADiaryId = product.ArchivedDiaryId;
            ViewBag.AProductTestId = aproductTestId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProtocolResult(ArchivedProtocolResultW aprotocolResult)
        {
            if (ModelState.IsValid)
            {
                Rep.AddProtocolResult(aprotocolResult);
                return RedirectToAction("ProtocolResultsIndex", new { aproductTestId = aprotocolResult.ArchivedProductTestId });
            }

            return View(aprotocolResult);
        }
        
        [HttpGet]    
        public ActionResult EditProtocolResult(Guid aprotocolResultId)
        {
            var aprotocolResult = Rep.GetArchivedProtocolResultW(aprotocolResultId);
            ViewBag.AProductTestId = aprotocolResult.ArchivedProductTestId;
            return View(aprotocolResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProtocolResult(ArchivedProtocolResultW aprotocolResult)
        {
            if (ModelState.IsValid)
            {
                Rep.EditProtocolResult(aprotocolResult);
                return RedirectToAction("ProtocolResultsIndex", new { aproductTestId = aprotocolResult.ArchivedProductTestId });
            }

            return View(aprotocolResult);
        }

        [HttpGet]
        public ActionResult DeleteProtocolResult(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aprotocolResult = Rep.GetArchivedProtocolResultW(id.Value);
            if (aprotocolResult == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(aprotocolResult);
            }

            return View(aprotocolResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProtocolResult(Guid id)
        {
            var protocolResult = Rep.GetArchivedProtocolResult(id);
            bool isdeleted = Rep.DeleteProtocolResult(id);

            if (isdeleted)
            {
                return RedirectToAction("ProtocolResultsIndex", new { aproductTestId = protocolResult.ArchivedProductTestId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProtocolResultsIndex?aproductTestId=" + protocolResult.ArchivedProductTestId });
        }
    }
}