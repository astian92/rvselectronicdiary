using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Tests;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("0e161082-3d84-4887-8bef-968e1ca53256")]
    public class TestsController : BaseController
    {
        private readonly ITestsRepository _rep;

        public TestsController(ITestsRepository testsRepo)
        {
            _rep = testsRepo;
        }

        public ActionResult Categories()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            var categories = _rep.GetCategories();
            return Json(new { data = categories });
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult CreateCategory(TestCategoryW category)
        {
            if (ModelState.IsValid)
            {
                if (!_rep.IsExisting(category))
                {
                    _rep.AddCategory(category);
                    return RedirectToAction("Categories");
                }

                ModelState.AddModelError("ErrorExists", "Категория с това име вече съществува. Моля опитайте друго име.");
            }

            return View(category);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult EditCategory(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TestCategoryW category = _rep.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult EditCategory(TestCategoryW category)
        {
            if (ModelState.IsValid)
            {
                if (!_rep.IsExisting(category))
                {
                    _rep.EditCategory(category);
                    return RedirectToAction("Categories");
                }

                ModelState.AddModelError("ErrorExists", "Категория с това име вече съществува. Моля опитайте друго име.");
            }

            return View(category);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult DeleteCategory(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TestCategoryW category = _rep.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(category);
            }

            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult DeleteCategoryConfirmed(Guid id)
        {
            bool isdeleted = _rep.DeleteCategory(id);

            if (isdeleted)
            {
                return RedirectToAction("Categories");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Tests/Categories" });
        }

        public ActionResult Index()
        {
            var tests = _rep.GetTests();
            return View(tests);
        }

        public JsonResult GetTests()
        {
            var tests = _rep.GetTests();

            var jsonData = tests.Select(t => new
            {
                TestType = t.TestType.ShortName,
                Name = t.Name,
                Level = t.AcredetationLevel.Level,
                UnitName = t.UnitName,
                Temperature = t.Temperature,
                Category = t.TestCategory.Name,
                Id = t.Id
            });

            return Json(new { data = jsonData });
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Create()
        {
            ViewBag.TestCategoryId = new SelectList(_rep.GetCategories(), "Id", "Name"); 
            ViewBag.AcredetationLevelId = new SelectList(_rep.GetAcredetationLevels(), "Id", "Level");
            ViewBag.TypeId = new SelectList(_rep.GetTestTypes(), "Id", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Create(TestW test)
        {
            if (ModelState.IsValid)
            {
                _rep.Add(test);
                return RedirectToAction("Index");
            }

            ViewBag.TestCategoryId = new SelectList(_rep.GetCategories(), "Id", "Name");
            ViewBag.AcredetationLevelId = new SelectList(_rep.GetAcredetationLevels(), "Id", "Level");
            ViewBag.TypeId = new SelectList(_rep.GetTestTypes(), "Id", "Type");

            return View(test);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TestW test = _rep.GetTest(id.Value);
            if (test == null)
            {
                return HttpNotFound();
            }

            ViewBag.TestCategoryId = new SelectList(_rep.GetCategories(), "Id", "Name", test.TestCategoryId);
            ViewBag.AcredetationLevelId = new SelectList(_rep.GetAcredetationLevels(), "Id", "Level", test.AcredetationLevelId);
            ViewBag.TypeId = new SelectList(_rep.GetTestTypes(), "Id", "Type", test.TypeId);

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Edit(TestW test)
        {
            if (ModelState.IsValid)
            {
                var response = _rep.Edit(test);

                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ErrorExists", response.Error.ErrorText);
                }
            }

            ViewBag.TestCategoryId = new SelectList(_rep.GetCategories(), "Id", "Name", test.TestCategoryId);
            ViewBag.AcredetationLevelId = new SelectList(_rep.GetAcredetationLevels(), "Id", "Level", test.AcredetationLevelId);
            ViewBag.TypeId = new SelectList(_rep.GetTestTypes(), "Id", "Type", test.TypeId);

            return View(test);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TestW test = _rep.GetTest(id.Value);
            if (test == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(test);
            }

            return View(test);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = _rep.Delete(id);

            if (isdeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Tests/Index" });
        }

        public string GetTestTypeFromId(Guid testId)
        {
            var testType = _rep.GetTestTypes();

            if (testType.Any(t => t.Id == testId))
            {
                return testType.Single(t => t.Id == testId).ShortName;
            }

            return "Unknown";
        }
    }
}