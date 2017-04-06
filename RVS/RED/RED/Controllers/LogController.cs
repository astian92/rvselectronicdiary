using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    [RoleFilter("77d2cc10-dc68-4fbf-8c3d-9128df7c1a09")]
    public class LogController : BaseController
    {
        private readonly ILogRepository _rep;

        public LogController(ILogRepository logRepo)
        {
            _rep = logRepo;
        }

        public ActionResult Index(int page, int pageSize)
        {
            var logs = _rep.GetAllActionLogs(page, pageSize);

            if (Request.IsAjaxRequest())
            {
                return PartialView(logs);
            }

            return View(logs);
        }
    }
}