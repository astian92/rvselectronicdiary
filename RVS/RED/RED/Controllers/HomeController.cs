using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDashboardRepository _rep;

        public HomeController(IDashboardRepository dashboardRepo)
        {
            _rep = dashboardRepo;
        }

        public ActionResult Index()
        {
            return View(_rep.GetDashboard());
        }

        public ActionResult TestsReference(int type)
        {
            return PartialView(_rep.GetTestsReference(type));
        }
    }
}