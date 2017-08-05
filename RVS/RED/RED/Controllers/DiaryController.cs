using System;
using System.Linq;
using System.Web.Mvc;
using RED.Filters;
using RED.Helpers;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewDiary)]
    public class DiaryController : BaseController
    {
        private readonly IDiaryRepository _rep;

        public DiaryController(IDiaryRepository diaryRepo)
        {
            _rep = diaryRepo;
        }

        public ActionResult Index(Guid? IdToOpen, bool isArchived = false)
        {
            ViewBag.ClientId = new SelectList(_rep.GetSelectListClients(), "Id", "Name");
            ViewBag.IdToOpen = IdToOpen;
            ViewBag.IsArchived = isArchived;

            return View();
        }

        public ActionResult Details(Guid Id)
        {
            var diaryW = _rep.GetDiary(Id);
            return PartialView(diaryW);
        }

        public ActionResult ActiveDiaries()
        {
            var diaryEntries = _rep.GetDiaryEntries();
            ViewBag.page = 1;
            return PartialView(diaryEntries);
        }

        public ActionResult FilterActiveDiaries(int page, int pageSize, string number, int diaryNumber, Guid client, DateTime? fromDate, DateTime? toDate)
        {
            var diaryEntries = _rep.GetDiaryEntries(page, pageSize, number, diaryNumber, client, fromDate, toDate);
            ViewBag.page = page;
            return PartialView("ActiveDiaries", diaryEntries);
        }

        public ActionResult ArchivedDiaries()
        {
            var archivedDiaries = _rep.GetArchivedDiaryEntries();
            return PartialView(archivedDiaries);
        }

        public ActionResult FilterArchivedDiaries(int page, int pageSize, int number, int diaryNumber, string client, DateTime? fromDate, DateTime? toDate)
        {
            var archivedDiaries = _rep.GetArchivedDiaryEntries(page, pageSize, number, diaryNumber, client, fromDate, toDate);
            ViewBag.page = page;
            return PartialView("ArchivedDiaries", archivedDiaries);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(_rep.GetSelectListClients(false), "Id", "Name");
            var diary = new DiaryW();
            diary.AcceptanceDateAndTime = DateTime.Now;
            diary.LetterDate = DateTime.Now;
            return View(diary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Create(DiaryW diary)
        {
            ModelState.Remove("LetterNumber");
            if (ModelState.IsValid && diary.Products.Count > 0)
            {
                _rep.AddLetter(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(_rep.GetSelectListClients(false), "Id", "Name", diary.ClientId);

            return View(diary);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Edit(Guid id)
        {
            var diary = _rep.GetDiary(id);
            if (diary == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientId = new SelectList(_rep.GetSelectListClients(false), "Id", "Name", diary.ClientId);
            ViewBag.Tests = new SelectList(_rep.GetSelectListTests(), "Id", "Name");

            return View(diary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Edit(DiaryW diary)
        {
            ModelState.Remove("LetterNumber");
            if (ModelState.IsValid)
            {
                _rep.Edit(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(_rep.GetSelectListClients(false), "Id", "Name", diary.ClientId);
            ViewBag.Tests = new SelectList(_rep.GetSelectListTests(), "Id", "Name");

            return View(diary);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Delete(Guid id)
        {
            var client = _rep.GetDiary(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(client);
            }

            return View(client);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult Archive(Guid id)
        {
            var diary = _rep.GetDiary(id);
            if (diary == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(diary);
            }

            return View(diary);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = _rep.Delete(id);

            if (isdeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Diary/Index" });
        }

        public ActionResult GetAllDiaryEntries()
        {
            //All diary entries - problem is that to get them as DiaryW (wrapped) I have to enumerate them!
            var diaryEntries = _rep.GetDiaryEntries();

            return Json(diaryEntries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPagedEntries()
        {
            //therefore, I implemented the paging before the enumeration.
            //here we have 10 records per page and get the second page
            var pagedEntries = _rep.GetDiaryEntries(2, 10);

            return Json(pagedEntries, JsonRequestBehavior.AllowGet);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public JsonResult GenerateRequest(Guid? diaryId, int testingPeriod)
        {
            if (diaryId != null && testingPeriod > 0 && testingPeriod <= 365)
            {
                string charGenerated = _rep.GenerateRequest(diaryId.Value, testingPeriod);

                if (!string.IsNullOrEmpty(charGenerated))
                {
                    return Json(charGenerated, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public JsonResult DeleteRequest(Guid? diaryId)
        {
            if (diaryId != null)
            {
                bool isDeleted = _rep.DeleteRequest(diaryId.Value);

                if (isDeleted)
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public JsonResult AddComment(Guid? diaryId, string comment)
        {
            if (diaryId != null && comment != null)
            {
                bool isSaved = _rep.AddComment(diaryId.Value, comment);

                if (isSaved)
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductsTests(SimpleProduct[] products)
        {
            ViewBag.Tests = new SelectList(_rep.GetSelectListTests(), "FullValue", "FullName");
            return PartialView(products);
        }

        public ActionResult AddTests()
        {
            ViewBag.Tests = new SelectList(_rep.GetSelectListTests(), "FullValue", "FullName");
            return PartialView();
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public JsonResult GetTestMethods(Guid testId)
        {
            return Json(_rep.GetTestMethods(testId), JsonRequestBehavior.AllowGet);
        }

        [RoleFilter(FeaturesCollection.ModifyDiary)]
        public JsonResult ArchiveDiary(Guid diaryId)
        {
            var response = _rep.ArchiveDiary(diaryId);
            return Json(response);
        }

        [UserFilter]
        public JsonResult RefreshArchivedProtocol(Guid adiaryId)
        {
            var adiary = _rep.GetArchivedDiary(adiaryId);
            var response = _rep.RegenerateArchivedProtocol(adiary);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public string GetMethodValueForTest(Guid testId)
        {
            var tests = _rep.GetTests();
            if (tests.Any(t => t.Id == testId))
            {
                var test = tests.Single(t => t.Id == testId);

                //because FZH is forbidden to take MethodValue just return the method value directly
                return test.MethodValue;
            }

            return string.Empty;
        }
    }
}