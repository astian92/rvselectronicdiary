using System.Web.Mvc;
using RED.Filters;

namespace RED.Models.ControllerBases
{
    [BombFilter]
    [RoleAuthorizationFilter]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}