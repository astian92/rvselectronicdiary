﻿using RED.Models.ControllerBases;
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
    public class DiaryController : ControllerBase<DiaryRepository>
    {
        // GET: Diary
        public ActionResult Index()
        {
            var clients = Rep.GetClients();
            var selectList = clients.ToList();

            Client nullable = new Client();
            nullable.Id = Guid.Empty;
            nullable.Name = "Всички";
            selectList.Insert(0, new ClientW(nullable));
            ViewBag.ClientId = new SelectList(selectList, "Id", "Name");
            return View();
        }

        public ActionResult ActiveDiaries()
        {
            var diaryEntries = Rep.GetDiaryEntries();
            return PartialView(diaryEntries);
        }

        public ActionResult FilterActiveDiaries(int page, int pageSize,
            int number, Guid client, DateTime? fromDate, DateTime? toDate)
        {
            var diaryEntries = Rep.GetDiaryEntries(page, pageSize, number, client, fromDate, toDate);
            return PartialView("ActiveDiaries", diaryEntries);
        }

        public ActionResult ArchivedDiaries()
        {
            var archivedDiaries = Rep.GetArchivedDiaryEntries();
            return PartialView(archivedDiaries);
        }

        public ActionResult FilterArchivedDiaries(int page, int pageSize,
            int number, string client, DateTime? fromDate, DateTime? toDate)
        {
            var archivedDiaries = Rep.GetArchivedDiaryEntries(page, pageSize, number, client, fromDate, toDate);
            return PartialView("ArchivedDiaries", archivedDiaries);
        }

        // GET: Diary/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name");
            //ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");
            return View();
        }

        // POST: Diary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiaryW diary)
        {
            if (ModelState.IsValid && diary.Products.Count > 0)
            {
                Rep.AddLetter(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name", diary.ClientId);

            return View(diary);
        }

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
        public ActionResult Edit(DiaryW diary)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(diary);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name", diary.ClientId);
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");

            return View(diary);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        public JsonResult GenerateRequest(Guid? diaryId)
        {
            if(diaryId != null)
            {
                bool isGenerated = Rep.GenerateRequest(diaryId.Value);

                if(isGenerated)
                    return Json("Ok", JsonRequestBehavior.AllowGet);  
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);  
        }

        [HttpPost]
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

        public JsonResult ArchiveDiary(Guid diaryId)
        {
            var response = Rep.ArchiveDiary(diaryId);
            return Json(response);
        }
    }
}