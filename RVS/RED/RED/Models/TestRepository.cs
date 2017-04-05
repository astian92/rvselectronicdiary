using System.Linq;
using RED.Models.RepositoryBases;

namespace RED.Models
{
    public class TestRepository : RepositoryBase
    {
        public string GetUser()
        {
            return Db.Users.First().Username;
        }
    }
}