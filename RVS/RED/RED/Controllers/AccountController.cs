using System;
using System.Web.Mvc;
using System.Web.Security;
using RED.Models;
using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _rep;

        public AccountController(IAccountRepository accRepo)
        {
            _rep = accRepo;
        }

        public AccountController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var response = _rep.Authenticate(model.Username, model.Password);
                if (response.IsSuccess)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                   ModelState.AddModelError("Error", response.Error.ErrorText);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile(Guid id)
        {
            var db = new RedDataEntities();
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "DisplayName", user.RoleId);

            return View(user);
        }
    }
}