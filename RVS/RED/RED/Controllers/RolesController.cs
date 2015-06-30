using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RED.Models.DataContext;
using RED.Models.Admin;
using RED.Models.ControllerBases;
using RED.Models.Admin.Roles;
using RED.Filters;

namespace RED.Controllers
{
    [RoleFilter("fd76464d-8e9c-4176-ab40-e372084d79ad")]
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

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.Features = Rep.GetFeatures();
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.AddRole(role, features);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
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

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayName")] RoleW role, string[] features)
        {
            if (ModelState.IsValid)
            {
                Rep.EditRole(role, features);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
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

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.DeleteRole(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Roles/Index" });
        }
    }
}
