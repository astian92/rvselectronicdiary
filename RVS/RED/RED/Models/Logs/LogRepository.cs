using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.Logs
{
    public class LogRepository : RepositoryBase
    {
        public List<ActionLogW> GetAllActionLogs()
        {
            List<ActionLogW> logsWrapper = new List<ActionLogW>();

            try
            {
                var context = DbContextFactory.GetDbContext();
                var logs = context.ActionLogs.ToList();
                foreach (var item in logs)
                {
                    ActionLogW log = new ActionLogW(item);

                    var user = context.Users.FirstOrDefault(x => x.Id == item.UserId);
                    if(user != null)
                    {
                        log.UserName = user.Username;
                        logsWrapper.Add(log);
                    }
                }
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return logsWrapper;
        }
    }
}