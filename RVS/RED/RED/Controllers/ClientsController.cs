using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RED.Filters;
using RED.Models;
using RED.Models.ControllerBases;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Clients;
using RED.Repositories.Abstract;
using RED.Helpers;
using Newtonsoft.Json;

namespace RED.Controllers
{
    [RoleFilter(FeaturesCollection.ViewClients)]
    public class ClientsController : BaseController
    {
        private readonly IClientsRepository _rep;

        public ClientsController(IClientsRepository clientsRepo)
        {
            _rep = clientsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public string GetClients()
        {
            //get the parameters from the Datatable
            var dtParams = new DtParameters(Request);

            var entities = _rep.GetClients();
            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.Name.ToLower().Contains(dtParams.SearchValue) ||
                                        (e.Mobile != null ? e.Mobile.ToLower().Contains(dtParams.SearchValue) : false));
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

            var json = JsonConvert.SerializeObject(jsonResult);
            return json;
        }

        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult Create(ClientW client)
        {
            if (ModelState.IsValid)
            {
                if (!_rep.IsExisting(client))
                {
                    _rep.Add(client);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("ErrorExists", "Клиент с това име вече съществува. Моля опитайте друго име.");
            }

            return View(client);
        }

        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientW client = _rep.GetClient(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult Edit(ClientW client)
        {
            if (ModelState.IsValid)
            {
                if (!_rep.IsExisting(client))
                {
                    _rep.Edit(client);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("ErrorExists", "Клиент с това име вече съществува. Моля опитайте друго име.");
            }

            return View(client);
        }

        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientW client = _rep.GetClient(id.Value);
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
        [RoleFilter(FeaturesCollection.ModifyClients)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = _rep.Delete(id);

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