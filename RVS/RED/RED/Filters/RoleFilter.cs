using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RED.Models.Account;   

namespace RED.Filters
{
    public class RoleFilter : ActionFilterAttribute
    {
        private string featureId;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //TODO
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = (RvsPrincipal)HttpContext.Current.User;
            if (!currentUser.IsAuthorize(this.featureId))
            {
                filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary    
                                {
                                    { "controller", "Home" },
                                    { "action" , "Index" }
                                });
            }
        }

        public RoleFilter()
        {
        }

        public RoleFilter(string featureId)
        {
            this.featureId = featureId;
        }
    }
}