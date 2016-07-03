using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    [RoleFilter("0e161082-3d84-4887-8bef-968e1ca53256")]
    public class TestsController : ControllerBase<TestsRepository>
    {
        public ActionResult Categories()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            var categories = Rep.GetCategories();
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
                if (!Rep.IsExisting(category))
                {
                    Rep.AddCategory(category);
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
            TestCategoryW category = Rep.GetCategory(id.Value);
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
                if (!Rep.IsExisting(category))
                {
                    Rep.EditCategory(category);
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

            TestCategoryW category = Rep.GetCategory(id.Value);
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
            bool isdeleted = Rep.DeleteCategory(id);

            if(isdeleted)
                return RedirectToAction("Categories");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Tests/Categories" });
        }

        public ActionResult Index()
        {
            var tests = Rep.GetTests();
            return View(tests);
        }

        public JsonResult GetTests()
        {
            var tests = Rep.GetTests();

            var jsonData = tests.Select(t => new
            {
                TestType = t.TestType.ShortName,
                Name = t.Name,
                //TestMethods = t.TestMethods,
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
            ViewBag.TestCategoryId = new SelectList(Rep.GetCategories(), "Id", "Name"); 
            ViewBag.AcredetationLevelId = new SelectList(Rep.GetAcredetationLevels(), "Id", "Level");
            ViewBag.TypeId = new SelectList(Rep.GetTestTypes(), "Id", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Create(TestW test)
        {
            if (ModelState.IsValid)
            {
                Rep.Add(test);
                return RedirectToAction("Index");

                ModelState.AddModelError("ErrorExists", "Изследване с това име вече съществува. Моля опитайте друго име.");
            }

            ViewBag.TestCategoryId = new SelectList(Rep.GetCategories(), "Id", "Name");
            ViewBag.AcredetationLevelId = new SelectList(Rep.GetAcredetationLevels(), "Id", "Level");
            ViewBag.TypeId = new SelectList(Rep.GetTestTypes(), "Id", "Type");

            return View(test);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TestW test = Rep.GetTest(id.Value);
            if (test == null)
            {
                return HttpNotFound();
            }

            ViewBag.TestCategoryId = new SelectList(Rep.GetCategories(), "Id", "Name", test.TestCategoryId);
            ViewBag.AcredetationLevelId = new SelectList(Rep.GetAcredetationLevels(), "Id", "Level", test.AcredetationLevelId);
            ViewBag.TypeId = new SelectList(Rep.GetTestTypes(), "Id", "Type", test.TypeId);

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Edit(TestW test)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(test);
                return RedirectToAction("Index");

                ModelState.AddModelError("ErrorExists", "Изследване с това име вече съществува. Моля опитайте друго име.");
            }

            ViewBag.TestCategoryId = new SelectList(Rep.GetCategories(), "Id", "Name", test.TestCategoryId);
            ViewBag.AcredetationLevelId = new SelectList(Rep.GetAcredetationLevels(), "Id", "Level", test.AcredetationLevelId);
            ViewBag.TypeId = new SelectList(Rep.GetTestTypes(), "Id", "Type", test.TypeId);

            return View(test);
        }

        [RoleFilter("e8d6d039-d94d-4465-9302-c2f6fde5d330")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TestW test = Rep.GetTest(id.Value);
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
            bool isdeleted = Rep.Delete(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Tests/Index" });
        }
    }
}