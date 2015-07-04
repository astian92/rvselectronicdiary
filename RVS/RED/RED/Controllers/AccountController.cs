using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RED.Models;
using RED.Models.Account;
using RED.Models.ControllerBases;
using System.Web.Security;
using RED.Models.Responses;
using System.Net;
using RED.Models.DataContext;

namespace RED.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase<AccountRepository>
    {
        public AccountController()
        {
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ActionResponse response = Rep.Authenticate(model.Username, model.Password);

                if(response.IsSuccess)
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

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    if (result.Succeeded)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //        // Send an email with this link
            //        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //        return RedirectToAction("Index", "Home");
            //    }
            //    AddErrors(result);
            //}

             //If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile(Guid id)
        {
            RedDataEntities db = new RedDataEntities();
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