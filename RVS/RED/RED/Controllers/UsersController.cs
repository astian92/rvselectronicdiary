﻿using System;
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
    [RoleFilter("132fb592-e0de-4f7b-89dd-e11b4aacc4ff")]
    public class UsersController : ControllerBase<AdminRepository>
    {
        public ActionResult Index()
        {
            //var users = Rep.GetUsers();
            return View();
        }

        public JsonResult GetUsers()
        {
            var users = Rep.GetUsers();

            var usersUnwrapped = users.Select(u => new
            {
                Username = u.Username,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Position = u.Position,
                RoleName = u.Role.DisplayName,
                Id = u.Id
            });

            return Json(new { data = usersUnwrapped });
        }

        // GET: Users/Create
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
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
        [RoleFilter("5696d246-25db-4d59-bcf6-139cd303f2f4")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bool isdeleted = Rep.DeleteUser(id);

            if(isdeleted)
                return RedirectToAction("Index");

            return RedirectToAction("DeleteConflicted", "Error", new { returnUrl = "/Users/Index" });
        }
    }
}