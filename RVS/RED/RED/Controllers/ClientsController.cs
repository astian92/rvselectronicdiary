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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientW client)
        {
            if (ModelState.IsValid)
            {
                Rep.Add(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

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
        public ActionResult Edit(ClientW client)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

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
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.Delete(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Clients/Index" });
        }
    }
}