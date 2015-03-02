using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RED.Models.DataContext;
using RED.Models.Admin;
using RED.Models.ControllerBases;
using RED.Models.Admin.Users;
using RED.Filters;

namespace RED.Controllers
{
    [RoleFilter("fd76464d-8e9c-4176-ab40-e372084d79ad")]
    public class UsersController : ControllerBase<AdminRepository>
    {
        public ActionResult Index()
        {
            var users = Rep.GetUsers();
            return View(users);
        }
        
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(Rep.GetRoles(), "Id", "DisplayName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,FirstName,MiddleName,LastName,Position,RoleId")] UserW user)
        {
            if (ModelState.IsValid)
            {
                Rep.AddUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(Rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserW user = Rep.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(Rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,FirstName,MiddleName,LastName,Position,RoleId")] UserW user)
        {
            if (ModelState.IsValid)
            {
                Rep.EditUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(Rep.GetRoles(), "Id", "DisplayName", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserW user = Rep.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
                return PartialView(user);
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Rep.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
