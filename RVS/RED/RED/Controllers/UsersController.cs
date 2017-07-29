using System;
using System.Web.Mvc;
using RED.Filters;
using RED.Helpers;
using RED.Models.Admin.Users;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewAdminsNRoles)]
    public class UsersController : BaseController
    {
        private readonly IAdminRepository _rep;

        public UsersController(IAdminRepository adminRepo)
        {
            _rep = adminRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var users = _rep.GetUsers();
            return Json(new { data = users });
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Create([Bind(Include = "Id,Username,Password,FirstName,MiddleName,LastName,Position,RoleId")] UserW user)
        {
            if (ModelState.IsValid)
            {
                _rep.AddUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Edit(Guid id)
        {
            var user = _rep.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,FirstName,MiddleName,LastName,Position,RoleId")] UserW user)
        {
            if (ModelState.IsValid)
            {
                _rep.EditUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult Delete(Guid id)
        {
            var user = _rep.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(user);
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyAdminsNRoles)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = _rep.DeleteUser(id);

            if (isdeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Users/Index" });
        }
    }
}