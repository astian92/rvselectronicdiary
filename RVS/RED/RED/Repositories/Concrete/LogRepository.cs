﻿using System.Collections.Generic;
using System.Linq;
using RED.Models.Logs;
using RED.Repositories.Abstract;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Models.Account;

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
            var logs = Db.ActionLogs.Where(x => x.UserId.ToString() != RvsPrincipal.MasterId).OrderByDescending(x => x.On).Skip((page - 1) * pageSize).Take(pageSize);
            return logs.ToList().Select(l => new ActionLogW(l, Db.Users.FirstOrDefault(x => x.Id == l.UserId)));
        }
    }
}