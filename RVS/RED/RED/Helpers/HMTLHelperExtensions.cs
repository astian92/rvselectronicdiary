using System.Web;
using System.Web.Mvc;
using RED.Models.Account;
using RED.Models.DataContext.Concrete;

namespace RED.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static MvcHtmlString GetProfile(this HtmlHelper html)
        {
            var principal = new RvsPrincipal(HttpContext.Current.User.Identity, new RvsContextFactory());
            var user = principal.GetUserData();
            var profileHtml = "<span class=\"block m-t-xs\">" +
                            "<strong class=\"font-bold\" style=\"color: white\">" + user.FirstName + " " + user.LastName + "</strong>" +
                       "</span><span class=\"text-muted text-xs block\">" + user.Position + "</span>";

            return MvcHtmlString.Create(profileHtml);
        }

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }

            if (string.IsNullOrEmpty(action))
            {
                action = currentAction;
            }

            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static bool IsAuthorized(this HtmlHelper html, string featureId)
        {
            var principal = new RvsPrincipal(HttpContext.Current.User.Identity, new RvsContextFactory());
            return principal.IsAuthorize(featureId);
        }

        public static bool IsSuperUser(this HtmlHelper html)
        {
            var principal = new RvsPrincipal(HttpContext.Current.User.Identity, new RvsContextFactory());
            return principal.IsSuperUser();
        }
    }
}
