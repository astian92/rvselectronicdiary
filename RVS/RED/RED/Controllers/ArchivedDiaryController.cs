using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.ArchivedWrappers;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [UserFilter]
    public class ArchivedDiaryController : BaseController
    {
        private readonly IArchivedDiaryRepository _rep;

        public ArchivedDiaryController(IArchivedDiaryRepository archivedDiaryRepo)
        {
            _rep = archivedDiaryRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var archivedDiary = _rep.GetArchivedDiaryW(id);
            return View(archivedDiary);
        }

        [HttpPost]
        public ActionResult Edit(ArchivedDiaryW adiary)
        {
            if (ModelState.IsValid)
            {
                _rep.Edit(adiary);
                return RedirectToAction("Index", "Diary");
            }

            return View(adiary);
        }

        public ActionResult ProductsIndex(Guid archivedDiaryId)
        {
            var adiary = _rep.GetArchivedDiary(archivedDiaryId);
            ViewBag.ADiaryNumber = adiary.Number;
            ViewBag.ArchivedDiaryId = archivedDiaryId;
            return View();
        }

        public JsonResult GetProducts(Guid archivedDiaryId)
        {
            var products = _rep.GetProducts(archivedDiaryId).OrderBy(p => p.Number);
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
                _rep.AddProduct(aproduct);
                return RedirectToAction("ProductsIndex", new { archivedDiaryId = aproduct.ArchivedDiaryId });
            }

            return View(aproduct);
        }

        [HttpGet]
        public ActionResult EditProduct(Guid id)
        {
            var aproduct = _rep.GetArchivedProductW(id);
            ViewBag.ADiaryId = aproduct.ArchivedDiaryId;
            return View(aproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ArchivedProductW aproduct)
        {
            if (ModelState.IsValid)
            {
                _rep.EditProduct(aproduct);
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

            var aproduct = _rep.GetArchivedProductW(id.Value);
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
            var product = _rep.GetArchivedProduct(id);
            bool isdeleted = _rep.DeleteProduct(id);

            if (isdeleted)
            {
                return RedirectToAction("ProductsIndex", new { archivedDiaryId = product.ArchivedDiaryId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProductsIndex?archivedDiaryId=" + product.ArchivedDiaryId });
        }

        public ActionResult ProductTestsIndex(Guid aproductId)
        {
            var aproduct = _rep.GetArchivedProductW(aproductId);
            ViewBag.ArchivedProductId = aproductId;
            ViewBag.AProductName = aproduct.Name;

            return View();
        }

        public JsonResult GetProductTests(Guid aproductId)
        {
            var products = _rep.GetProductTests(aproductId);
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateProductTest(Guid aproductId)
        {
            ViewBag.TestAcredetationLevel = new SelectList(_rep.GetPossibleAcredetationLevels(), "Level", "Level");
            ViewBag.AProductId = aproductId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductTest(ArchivedProductTestW aproductTest)
        {
            if (ModelState.IsValid)
            {
                _rep.AddProductTest(aproductTest);
                return RedirectToAction("ProductTestsIndex", new { aproductId = aproductTest.ArchivedProductId });
            }

            return View(aproductTest);
        }

        [HttpGet]
        public ActionResult EditProductTest(Guid aproductTestId)
        {
            var aProductTest = _rep.GetArchivedProductTestW(aproductTestId);
            ViewBag.TestAcredetationLevel = new SelectList(_rep.GetPossibleAcredetationLevels(), "Level", "Level", aProductTest.TestAcredetationLevel);
            ViewBag.AProductId = aProductTest.ArchivedProductId;
            return View(aProductTest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductTest(ArchivedProductTestW aproductTest)
        {
            if (ModelState.IsValid)
            {
                _rep.EditProductTest(aproductTest);
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

            var aproductTest = _rep.GetArchivedProductTestW(id.Value);
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
            var productTest = _rep.GetArchivedProductTest(id);
            bool isdeleted = _rep.DeleteProductTest(id);

            if (isdeleted)
            {
                return RedirectToAction("ProductTestsIndex", new { aproductId = productTest.ArchivedProductId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProductTestsIndex?aproductId=" + productTest.ArchivedProductId });
        }

        public ActionResult ProtocolResultsIndex(Guid aproductTestId)
        {
            var aproductTest = _rep.GetArchivedProductTestW(aproductTestId);
            ViewBag.ArchivedProductTestId = aproductTestId;
            ViewBag.AProductTestName = aproductTest.TestName;

            return View();
        }

        public JsonResult GetProtocolResults(Guid aproductTestId)
        {
            var products = _rep.GetProtocolResults(aproductTestId);
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProtocolResult(Guid aproductTestId)
        {
            var productTest = _rep.GetArchivedProductTestW(aproductTestId);
            var product = _rep.GetArchivedProductW(productTest.ArchivedProductId);

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
                _rep.AddProtocolResult(aprotocolResult);
                return RedirectToAction("ProtocolResultsIndex", new { aproductTestId = aprotocolResult.ArchivedProductTestId });
            }

            return View(aprotocolResult);
        }

        [HttpGet]
        public ActionResult EditProtocolResult(Guid aprotocolResultId)
        {
            var aprotocolResult = _rep.GetArchivedProtocolResultW(aprotocolResultId);
            ViewBag.AProductTestId = aprotocolResult.ArchivedProductTestId;
            return View(aprotocolResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProtocolResult(ArchivedProtocolResultW aprotocolResult)
        {
            if (ModelState.IsValid)
            {
                _rep.EditProtocolResult(aprotocolResult);
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

            var aprotocolResult = _rep.GetArchivedProtocolResultW(id.Value);
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
            var protocolResult = _rep.GetArchivedProtocolResult(id);
            bool isdeleted = _rep.DeleteProtocolResult(id);

            if (isdeleted)
            {
                return RedirectToAction("ProtocolResultsIndex", new { aproductTestId = protocolResult.ArchivedProductTestId });
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/ArchivedDiary/ProtocolResultsIndex?aproductTestId=" + protocolResult.ArchivedProductTestId });
        }
    }
}