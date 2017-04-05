using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Models.DataContext;
using RED.Filters;

namespace RED.Controllers
{
    [RoleFilter()]
    public class FeaturesController : Controller
    {
        private RvsDbContext db = DbContextFactory.GetDbContext();

        public ActionResult Index()
        {
            return View(db.Features.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                feature.Id = Guid.NewGuid();
                db.Features.Add(feature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayName")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Features/Index" });
            }

            return RedirectToAction("Index");
        }
    }
}
