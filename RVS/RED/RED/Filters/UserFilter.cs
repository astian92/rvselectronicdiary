using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RED.Models.Account;
using RED.Models.DataContext.Concrete;

namespace RED.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var principal = new RvsPrincipal(HttpContext.Current.User.Identity, new RvsContextFactory());
            if (!principal.IsSuperUser())
            {
                filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                                {
                                    { "controller", "Home" },
                                    { "action", "Index" }
                                });
            }
        }
    }
}