﻿using System.Collections.Generic;
using System.Linq;
using RED.Models.Account;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Models.Logs;
using RED.Repositories.Abstract;

namespace RED.Repositories.Concrete
{
    public class LogRepository : ILogRepository
    {
        private readonly RvsDbContext Db;

        public LogRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public IEnumerable<ActionLogW> GetAllActionLogs(int page = 1, int pageSize = 10)
        {
            var logs = Db.ActionLogs.Where(x => x.UserId != RvsPrincipal.MasterGuid).OrderByDescending(x => x.On).Skip((page - 1) * pageSize).Take(pageSize);
            return logs.ToList().Select(l => new ActionLogW(l, Db.Users.FirstOrDefault(x => x.Id == l.UserId)));
        }
    }
}