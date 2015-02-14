﻿using RED.Models;
using RED.Models.ControllerBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class TestController : ControllerBase<TestRepository>
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Content(Rep.GetUser());
        }

        public ActionResult Exception()
        {
            throw new Exception("EXCEPTION TEXT");
        }
    }
}