using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.RepositoryBases
{
    public abstract class RepositoryBase
    {
        protected RedDataEntities db { get; set; }

        public RepositoryBase()
        {
            db = DbContextFactory.GetDbContext();
        }
    }
}