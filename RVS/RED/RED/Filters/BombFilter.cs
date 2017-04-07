using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace RED.Filters
{
    public class BombFilter : ActionFilterAttribute
    {
        private DateTime trialExpireDate = new DateTime(2015, 11, 01);
        private string privateKey = "c11418e7-df8e-4dfc-90e8-695f79246726";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string publickey = ConfigurationManager.AppSettings["PublicKey"];

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (privateKey != publickey && trialExpireDate < DateTime.Now)
            {
                if (controllerName != "Error" && actionName != "ProductExpired")
                {
                    filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                                    {
                                        { "controller", "Error" },
                                        { "action", "ProductExpired" }
                                    });
                }
            }
        }
    }
}
