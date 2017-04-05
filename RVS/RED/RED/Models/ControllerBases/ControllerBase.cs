using System.Web.Mvc;
using RED.Filters;
using RED.Models.RepositoryBases;

namespace RED.Models.ControllerBases
{
    [BombFilter]
    public class ControllerBase<T> : Controller
        where T : RepositoryBase, new()
    {
        public ControllerBase()
        {
            this.Rep = new T();
        }

        protected T Rep { get; set; }
    }
}