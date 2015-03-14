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
            Rep.DeleteCategory(id);
            return RedirectToAction("Categories");
        }

        public ActionResult Index()
        {
            var tests = Rep.GetTests();
            return View(tests);
        }


    }
}