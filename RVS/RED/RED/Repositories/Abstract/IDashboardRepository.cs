using System.Collections.Generic;
using System.Linq;
using RED.Models.Dashboard;

namespace RED.Repositories.Abstract
{
    public interface IDashboardRepository
    {
        DashboardW GetDashboard();

        Dictionary<string, IGrouping<string, TestsReferenceW>> GetTestsReference(int type);
    }
}