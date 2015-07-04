using RED.Filters;
using RED.Models.ControllerBases;
using RED.Models.ElectronicDiary.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    [RoleFilter("4177b39a-ddce-46ad-812b-55d5935012ed")]
    public class ClientsController : ControllerBase<ClientsRepository>
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetClients()
        {
            var clients = Rep.GetClients();

            var jsonData = clients.Select(c => new
            {
                Name = c.Name,
                Id = c.Id
            });

            return Json(new { data = jsonData });
        }

        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult Create(ClientW client)
        {
            if (ModelState.IsValid)
            {
                Rep.Add(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientW client = Rep.GetClient(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult Edit(ClientW client)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientW client = Rep.GetClient(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(client);
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter("a896caa3-43eb-452a-a0ce-4691290f2a19")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.Delete(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Clients/Index" });
        }
    }
}