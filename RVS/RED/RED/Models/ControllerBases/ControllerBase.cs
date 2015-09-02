using RED.Filters;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Models.ControllerBases
{
    [BombFilter]
    public class ControllerBase<T> : Controller
        where T : RepositoryBase, new()
    {
        protected T Rep { get; set; }

        public ControllerBase()
        {
            this.Rep = new T();
        }
    }
}