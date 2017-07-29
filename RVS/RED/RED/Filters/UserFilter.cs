using System.Web.Mvc;
using System.Web.Routing;
using RED.Models.Account;

namespace RED.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!RvsPrincipal.IsSuperUser())
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