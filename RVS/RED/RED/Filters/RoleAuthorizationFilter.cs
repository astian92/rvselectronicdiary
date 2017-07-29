using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using RED.Models.Admin.Users;
using RED.Models.DataContext.Abstract;

namespace RED.Filters
{
    public class RoleAuthorizationFilter : AuthorizeAttribute
    {
        public IRvsContextFactory Factory { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            var user = JsonConvert.DeserializeObject<UserW>(ticket.UserData);

            var rvsContext = Factory.CreateConcrete();

            var role = rvsContext.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            user.Role = role;

            HttpContext.Current.Session["user"] = user;
            return true;
        }
    }
}