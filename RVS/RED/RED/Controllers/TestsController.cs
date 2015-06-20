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
    public class TestsController : ControllerBase<TestsRepository>
    {
        public ActionResult Categories()
        {
            var categories = Rep.GetCategories();
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(TestCategoryW category)
        {
            if (ModelState.IsValid)
            {
                Rep.AddCategory(category);
                return RedirectToAction("Categories");
            }

            return View(category);
        }

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
        public ActionResult EditCategory(TestCategoryW category)
        {
            if (ModelState.IsValid)
            {
                Rep.EditCategory(category);
                return RedirectToAction("Categories");
            }

            return View(category);
        }

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

        public ActionResult Create()
        {
            ViewBag.TestCategoryId = new SelectList(Rep.GetCategories(), "Id", "Name"); 
            ViewBag.AcredetationLevelId = new SelectList(Rep.GetAcredetationLevels(), "Id", "Level");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestW test)
        {
            if (ModelState.IsValid)
            {
                Rep.Add(test);
                return RedirectToAction("Index");
            }

            return View(test);
        }

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
            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestW test)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(test);
                return RedirectToAction("Index");
            }

            return View(test);
        }

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
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.Delete(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Tests/Index" });
        }
    }
}