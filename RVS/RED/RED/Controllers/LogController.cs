using RED.Models.ControllerBases;
using RED.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class LogController : ControllerBase<LogRepository>
    {
        // GET: Log
        public ActionResult Index()
        {
            var logs = Rep.GetAllActionLogs();

            if (Request.IsAjaxRequest())
                return PartialView(logs.OrderByDescending(x => x.On).ToList());

            return View(logs.OrderByDescending(x => x.On).ToList());
        }
    }
}