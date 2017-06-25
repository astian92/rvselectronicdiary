using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Tests;
using RED.Repositories.Abstract;
using RED.Helpers;
using RED.Models;
using RED.Models.DataContext;
using Newtonsoft.Json;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewTests)]
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

        [RoleFilter(FeaturesCollection.ModifyTests)]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        [RoleFilter(FeaturesCollection.ModifyTests)]
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
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        [RoleFilter(FeaturesCollection.ModifyTests)]
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
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        public string GetTests()
        {
            //get the parameters from the Datatable
            var dtParams = new DtParameters(Request);

            var entities = _rep.GetTests();
            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.Name.ToLower().Contains(dtParams.SearchValue) ||
                                          e.TestType.ShortName.ToLower().Contains(dtParams.SearchValue) || 
                                          e.TestCategory.Name.ToLower().Contains(dtParams.SearchValue));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else
            {
                //defaultOrder
                entities = entities.OrderBy(c => c.TestCategory.Name)
                    .ThenBy(c => c.TestType.ShortName)
                    .ThenBy(c => c.Name)
                    .ThenBy(c => c.AcredetationLevel.Level);
            }

            var data = entities.Skip(dtParams.Skip).Take(dtParams.PageSize)
                .ToList().Select(t => new TestVM
                {
                    TestType = t.TestType.ShortName,
                    Name = t.Name,
                    Level = t.AcredetationLevel.Level,
                    UnitName = t.UnitName,
                    Temperature = t.Temperature,
                    Category = t.TestCategory.Name,
                    Id = t.Id
                });

            var jsonResult = new JqueryListResult<TestVM>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords);

            var json = JsonConvert.SerializeObject(jsonResult);
            return json;
        }

        [RoleFilter(FeaturesCollection.ModifyTests)]
        public ActionResult Create()
        {
            ViewBag.TestCategoryId = new SelectList(_rep.GetCategories(), "Id", "Name");
            ViewBag.AcredetationLevelId = new SelectList(_rep.GetAcredetationLevels(), "Id", "Level");
            ViewBag.TypeId = new SelectList(_rep.GetTestTypes(), "Id", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        [RoleFilter(FeaturesCollection.ModifyTests)]
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
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        [RoleFilter(FeaturesCollection.ModifyTests)]
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
        [RoleFilter(FeaturesCollection.ModifyTests)]
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

        private IQueryable<Test> Filter(IQueryable<Test> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenBy(e => e.TestType.ShortName);
                    }
                    else
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).OrderByDescending(e => e.TestType.ShortName);
                    }

                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenBy(e => e.Name);
                    }
                    else
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenByDescending(e => e.Name);
                    }

                    break;
                case 2:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenBy(e => e.AcredetationLevel.Level);
                    }
                    else
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenByDescending(e => e.AcredetationLevel.Level);
                    }

                    break;
                case 3:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenBy(e => e.UnitName);
                    }
                    else
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenByDescending(e => e.UnitName);
                    }

                    break;
                case 4:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenBy(e => e.Temperature);
                    }
                    else
                    {
                        entities = entities.OrderBy(c => c.TestCategory.Name).ThenByDescending(e => e.Temperature);
                    }

                    break;
                default:
                    break;
            }

            return entities;
        }
    }
}