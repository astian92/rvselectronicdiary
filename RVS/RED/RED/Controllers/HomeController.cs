using RED.Models.ControllerBases;
using RED.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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