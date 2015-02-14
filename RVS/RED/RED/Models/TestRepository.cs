using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models
{
    public class TestRepository : RepositoryBase
    {
        public string GetUser()
        {
            return db.Users.First().Username;
        }
    }
}