using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class DiaryController : ControllerBase<DiaryRepository>
    {
        // GET: Diary
        public ActionResult Index()
        {
            var diaryEntries = Rep.GetDiaryEntries();
            return View(diaryEntries);
        }

        // GET: Diary/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(Rep.GetClients(), "Id", "Name");
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");
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
            ViewBag.Tests = new SelectList(Rep.GetTests(), "Id", "Name");

            return View(diary);
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
        public JsonResult AddComment(Guid diaryId, string comment)
        {
            if (comment != "")
            {
                bool isSaved = Rep.AddComment(diaryId, comment);

                if (isSaved)
                    return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}