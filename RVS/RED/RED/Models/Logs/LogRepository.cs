using System.Collections.Generic;
using System.Linq;
using RED.Models.RepositoryBases;

namespace RED.Models.Logs
{
    public class LogRepository : RepositoryBase
    {
        public IEnumerable<ActionLogW> GetAllActionLogs(int page = 1, int pageSize = 10)
        {
            var logs = Db.ActionLogs.Where(x => x.UserId.ToString() != "613b0faa-8828-44a9-8bbe-09ba68cc33ae").OrderByDescending(x => x.On).Skip((page - 1) * pageSize).Take(pageSize);
            return logs.ToList().Select(l => new ActionLogW(l, Db.Users.FirstOrDefault(x => x.Id == l.UserId)));
        }
    }
}