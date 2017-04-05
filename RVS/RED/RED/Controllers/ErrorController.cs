using System.Web.Mvc;

namespace RED.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult DeleteConflicted(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        public ActionResult ProductExpired()
        {
            return View();
        }
    }
}