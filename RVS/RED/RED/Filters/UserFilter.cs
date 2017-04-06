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
            RvsPrincipal principal = new RvsPrincipal(HttpContext.Current.User.Identity, new RvsContextFactory());
            var user = principal.GetUserData();

            if (user.Id != Guid.Parse("0f68da69-5c82-480b-9474-54c133439b0c") && user.Id != Guid.Parse("613b0faa-8828-44a9-8bbe-09ba68cc33ae"))
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