using System;
using System.Net;
using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Models.Admin.Roles;
using RED.Filters;
using RED.Repositories.Abstract;
using RED.Helpers;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewAdminsNRoles)]
    public class RolesController : BaseController
    {
        private readonly IAdminRepository Rep;

        public RolesController(IAdminRepository adminRepo)
        {
            Rep = adminRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoles()
        {
            var roles = Rep.GetRoles();
            return Json(new { data = roles });
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Create()
        {
            ViewBag.Features = Rep.GetFeatures();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Create([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.AddRole(role, features);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
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
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Edit([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.EditRole(role, features);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
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
            {
                return PartialView(role);
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.DeleteRole(id);

            if (isdeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Roles/Index" });
        }
    }
}
