using System.Web.Mvc;
using System.Web.Routing;
using RED.Models.Account;

namespace RED.Filters
{
    public class RoleFilter : ActionFilterAttribute
    {
        private readonly string _featureId;

        public RoleFilter()
        {
        }

        public RoleFilter(string featureId)
        {
            _featureId = featureId;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!RvsPrincipal.IsAuthorize(_featureId))
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