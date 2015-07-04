using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    [RoleFilter("77d2cc10-dc68-4fbf-8c3d-9128df7c1a09")]
    public class LogController : ControllerBase<LogRepository>
    {
        // GET: Log
        public ActionResult Index(int page, int pageSize)
        {
            var logs = Rep.GetAllActionLogs(page, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView(logs);

            return View(logs);
        }
    }
}