using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED.Models.Dashboard
{
    public class DashboardRepository : RepositoryBase
    {
        public DashboardW GetDashboard()
        {
            DashboardW dash = new DashboardW();
            dash.clientsNum = db.Clients.Count();
            dash.diariesNum = db.Diaries.Count();
            dash.acceptedRequestsNum = db.Requests.Where(r => r.AcceptedBy != null).Count();
            dash.completedRequestsNum = db.Requests.Where(r => r.Protocols.Count > 0).Count();

            return dash;
        }

        public Dictionary<string, IGrouping<Guid, ProductTest>> GetTestsReference(int type)
        {
            switch (type)
            {
                case 0:
                    return db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.Product.Diary.AcceptanceDateAndTime.Month == DateTime.Now.Month
                                            && x.Product.Diary.AcceptanceDateAndTime.Day == DateTime.Now.Day)
                                            .GroupBy(x => x.TestId).ToDictionary(x => x.FirstOrDefault().Test.Name);
                case 1:
                    return db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.Product.Diary.AcceptanceDateAndTime.Month == DateTime.Now.Month)
                                            .GroupBy(x => x.TestId).ToDictionary(x => x.FirstOrDefault().Test.Name);
                case 2:
                    return db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year)
                                            .GroupBy(x => x.TestId).ToDictionary(x => x.FirstOrDefault().Test.Name);
            }

            return new Dictionary<string, IGrouping<Guid, ProductTest>>();
        }
    }
}
