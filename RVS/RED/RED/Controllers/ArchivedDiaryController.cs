using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class ArchivedDiaryController : ControllerBase<ArchivedDiaryRepository>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var archivedDiary = Rep.GetArchivedDiaryW(id);
            return View(archivedDiary);
        }

        [HttpPost]
        public ActionResult Edit(ArchivedDiaryW adiary)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(adiary);
                return RedirectToAction("Index", "Diary");
            }

            return View(adiary);
        }
	}
}