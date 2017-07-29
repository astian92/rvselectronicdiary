using System;
using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    public class FeaturesController : BaseController
    {
        private readonly IFeaturesRepository _rep;

        public FeaturesController(IFeaturesRepository featuresRepo)
        {
            _rep = featuresRepo;
        }

        public ActionResult Index()
        {
            return View(_rep.GetAll());
        }

        public ActionResult Details(Guid id)
        {
            var feature = _rep.Get(id);
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
                _rep.Create(feature);
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        public ActionResult Edit(Guid id)
        {
            var feature = _rep.Get(id);
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
                _rep.Edit(feature);
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        public ActionResult Delete(Guid id)
        {
            var feature = _rep.Get(id);
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
            var isDeleted = _rep.Delete(id);
            if (!isDeleted)
            {
                return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Features/Index" });
            }

            return RedirectToAction("Index");
        }
    }
}