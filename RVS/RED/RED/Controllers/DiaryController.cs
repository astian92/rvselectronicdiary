using RED.Models.ControllerBases;
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
            return View();
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
    }
}