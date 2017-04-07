using System.Web.Mvc;
using RED.Filters;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;
using RED.Helpers;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewUserActions)]
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