using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Models.Dashboard;

namespace RED.Controllers
{
    public class HomeController : ControllerBase<DashboardRepository>
    {
        public ActionResult Index()
        {
            return View(Rep.GetDashboard());
        }

        public ActionResult TestsReference(int type)
        {
            return PartialView(Rep.GetTestsReference(type));
        }
    }
}