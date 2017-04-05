using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models;
using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;

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
            var dtParams = new DtParameters(Request); //get the parameters from the Datatable

            var entities = Rep.GetClients();
            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.Name.ToLower().Contains(dtParams.SearchValue) ||
                                               (e.Mobile != null ?
                                                    e.Mobile.ToLower().Contains(dtParams.SearchValue) : 
                                                    false));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else
            {
                //defaultOrder
                entities = entities.OrderBy(e => e.Name);
            }

            var data = entities.Skip(dtParams.Skip).Take(dtParams.PageSize)
                .ToList().Select(c => new ClientW
                {
                    Name = c.Name,
                    Mobile = c.Mobile,
                    Id = c.Id
                });

            var jsonResult = new JqueryListResult<ClientW>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords);

            return Json(jsonResult);
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
                if (!Rep.IsExisting(client))
                {
                    Rep.Add(client);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("ErrorExists", "Клиент с това име вече съществува. Моля опитайте друго име.");
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
                if (!Rep.IsExisting(client))
                {
                    Rep.Edit(client);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("ErrorExists", "Клиент с това име вече съществува. Моля опитайте друго име.");
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

            if (isdeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Clients/Index" });
        }

        private IQueryable<Client> Filter(IQueryable<Client> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Name);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Name);
                    }

                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Mobile);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Mobile);
                    }

                    break;
                default:
                    break;
            }

            return entities;
        }
    }
}