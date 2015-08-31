using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    [RoleFilter("fd53f97f-8bec-42ae-b17a-80cc7fee522f")]
    public class DiaryController : ControllerBase<DiaryRepository>
    {
        // GET: Diary
        public ActionResult Index(Guid? IdToOpen, bool isArchived = false)
        {
            var clients = Rep.GetClients();
            var selectList = clients.ToList();

            Client nullable = new Client();
            nullable.Id = Guid.Empty;
            nullable.Name = "Всички";
            selectList.Insert(0, new ClientW(nullable));
            ViewBag.ClientId = new SelectList(selectList, "Id", "Name");
            ViewBag.IdToOpen = IdToOpen;
            ViewBag.IsArchived = isArchived;
            return View();
        }

        public ActionResult ActiveDiaries()
        {
            var diaryEntries = Rep.GetDiaryEntries();
            ViewBag.page = 1;
            return PartialView(diaryEntries);
        }

        public ActionResult FilterActiveDiaries(int page, int pageSize,
            int number, int diaryNumber, Guid client, DateTime? fromDate, DateTime? toDate)
        {
            var diaryEntries = Rep.GetDiaryEntries(page, pageSize, number, diaryNumber, client, fromDate, toDate);
            ViewBag.page = page;
            return PartialView("ActiveDiaries", diaryEntries);
        }

        public ActionResult ArchivedDiaries()
        {
            var archivedDiaries = Rep.GetArchivedDiaryEntries();
            return PartialView(archivedDiaries);
        }

        public ActionResult FilterArchivedDiaries(int page, int pageSize,
            int number, int diaryNumber, string client, DateTime? fromDate, DateTime? toDate)
        {
            var archivedDiaries = Rep.GetArchivedDiaryEntries(page, pageSize, number, diaryNumber, client, fromDate, toDate);
            ViewBag.page = page;
            return PartialView("ArchivedDiaries", archivedDiaries);
        }

        // GET: Diary/Create
        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name");
            //ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");
            return View();
        }

        // POST: Diary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Create(DiaryW diary)
        {
            ModelState.Remove("LetterNumber");
            if (ModelState.IsValid && diary.Products.Count > 0)
            {
                Rep.AddLetter(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name", diary.ClientId);

            return View(diary);
        }

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            DiaryW diary = Rep.GetDiary(id.Value);
            if (diary == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name", diary.ClientId);
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");

            return View(diary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Edit(DiaryW diary)
        {
            ModelState.Remove("LetterNumber");
            if (ModelState.IsValid)
            {
                Rep.Edit(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name", diary.ClientId);
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");

            return View(diary);
        }

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DiaryW client = Rep.GetDiary(id.Value);
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

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult Archive(Guid id)
        {
            DiaryW client = Rep.GetDiary(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
                return PartialView(client);

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.Delete(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Diary/Index" });
        }

        public ActionResult GetAllDiaryEntries()
        {
            //All diary entries - problem is that to get them as DiaryW (wrapped) I have to enumerate them!
            var diaryEntries = Rep.GetDiaryEntries();

            return Json(diaryEntries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPagedEntries()
        {
            //therefore, I implemented the paging before the enumeration.
            var pagedEntries = Rep.GetDiaryEntries(2, 10);
            //here we have 10 records per page and get the second page

            return Json(pagedEntries, JsonRequestBehavior.AllowGet);
        }

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public JsonResult GenerateRequest(Guid? diaryId, int testingPeriod)
        {
            if(diaryId != null && testingPeriod > 0 && testingPeriod <= 365)
            {
                bool isGenerated = Rep.GenerateRequest(diaryId.Value, testingPeriod);

                if(isGenerated)
                    return Json("Ok", JsonRequestBehavior.AllowGet);  
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);  
        }

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public JsonResult DeleteRequest(Guid? diaryId)
        {
            if (diaryId != null)
            {
                bool isDeleted = Rep.DeleteRequest(diaryId.Value);

                if (isDeleted)
                    return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public JsonResult AddComment(Guid? diaryId, string comment)
        {
            if (diaryId != null && comment != null && comment != "")
            {
                bool isSaved = Rep.AddComment(diaryId.Value, comment);

                if (isSaved)
                    return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductsTests(SimpleProduct[] products)
        {
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");
            return PartialView(products);
        }

        [RoleFilter("6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a")]
        public JsonResult ArchiveDiary(Guid diaryId)
        {
            var response = Rep.ArchiveDiary(diaryId);
            return Json(response);
        }
    }
}