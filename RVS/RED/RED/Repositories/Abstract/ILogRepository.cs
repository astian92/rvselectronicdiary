using System.Collections.Generic;
using RED.Models.Logs;

namespace RED.Repositories.Abstract
{
    public interface ILogRepository
    {
        IEnumerable<ActionLogW> GetAllActionLogs(int page = 1, int pageSize = 10);
    }
}