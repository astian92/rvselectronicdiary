using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;
using RED.Models.ElectronicDiary;

namespace RED.Controllers
{
    [Authorize]
    public class MiscController : BaseController
    {
        private readonly IMiscRepository _rep;

        public MiscController(IMiscRepository miscRepo)
        {
            _rep = miscRepo;
        }

        [HttpGet]
        public ActionResult AcreditationRegistry()
        {
            var acreditationRegistry = _rep.GetAcreditationRegistry();
            return View(acreditationRegistry);
        }

        [HttpPost]
        public ActionResult AcreditationRegistry(AcreditationMetaW model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Невалидни данни");
            }
            else
            {
                _rep.SaveAcreditationRegistry(model);
                ViewBag.Success = "Датите бяха успешно променени";
            }

            return View(model);
        }
    }
}