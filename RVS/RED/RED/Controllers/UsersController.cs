using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Models.Admin.Users;
using RED.Filters;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("132fb592-e0de-4f7b-89dd-e11b4aacc4ff")]
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

            var usersUnwrapped = users.Select(u => new
            {
                Username = u.Username,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Position = u.Position,
                RoleName = u.Role.DisplayName,
                Id = u.Id
            });

            return Json(new { data = usersUnwrapped });
        }

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserW user = _rep.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(_rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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

        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserW user = _rep.GetUser(id.Value);
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
