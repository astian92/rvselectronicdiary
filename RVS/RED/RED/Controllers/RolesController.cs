using System;
using System.Net;
using System.Web.Mvc;
using RED.Models.Admin;
using RED.Models.ControllerBases;
using RED.Models.Admin.Roles;
using RED.Filters;

namespace RED.Controllers
{
    [RoleFilter("132fb592-e0de-4f7b-89dd-e11b4aacc4ff")]
    public class RolesController : ControllerBase<AdminRepository>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoles()
        {
            var roles = Rep.GetRoles();
            return Json(new { data = roles });
        }

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Create()
        {
            ViewBag.Features = Rep.GetFeatures();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Create([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.AddRole(role, features);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleW role = Rep.GetRole(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.Features = Rep.GetFeatures();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Edit([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.EditRole(role, features);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleW role = Rep.GetRole(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
                return PartialView(role);
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.DeleteRole(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Roles/Index" });
        }
    }
}
