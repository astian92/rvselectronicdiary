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
            dash.diariesNum = db.Diaries.Count() + db.ArchivedDiaries.Count();
            dash.acceptedRequestsNum = db.Requests.Where(r => r.AcceptedBy != null && r.Protocols.Count == 0).Count();
            dash.completedRequestsNum = db.Requests.Where(r => r.Protocols.Count > 0).Count() + db.ArchivedDiaries.Count();

            return dash;
        }

        public Dictionary<string, IGrouping<string, TestsReferenceW>> GetTestsReference(int type)
        {
            
            switch (type)
            {
                case 0:
                    var tests = db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.Product.Diary.AcceptanceDateAndTime.Month == DateTime.Now.Month
                                            && x.Product.Diary.AcceptanceDateAndTime.Day == DateTime.Now.Day)
                                            .ToList().Select(x => new TestsReferenceW(x)).ToList();
                    tests.AddRange(db.ArchivedProductTests.Where(x => x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Month == DateTime.Now.Month
                                            && x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Day == DateTime.Now.Day)
                                             .ToList().Select(x => new TestsReferenceW(x)));
                                            
                    return tests.GroupBy(x => x.TestName).ToDictionary(x => x.FirstOrDefault().TestName);
                case 1:
                    tests = db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.Product.Diary.AcceptanceDateAndTime.Month == DateTime.Now.Month)
                                            .ToList().Select(x => new TestsReferenceW(x)).ToList();
                    tests.AddRange(db.ArchivedProductTests.Where(x => x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Year == DateTime.Now.Year
                                            && x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Month == DateTime.Now.Month)
                                             .ToList().Select(x => new TestsReferenceW(x)));

                    return tests.GroupBy(x => x.TestName).ToDictionary(x => x.FirstOrDefault().TestName);
                case 2:
                    tests = db.ProductTests.Where(x => x.Product.Diary.AcceptanceDateAndTime.Year == DateTime.Now.Year)
                                            .ToList().Select(x => new TestsReferenceW(x)).ToList();
                    tests.AddRange(db.ArchivedProductTests.Where(x => x.ArchivedProduct.ArchivedDiary.AcceptanceDateAndTime.Year == DateTime.Now.Year)
                                             .ToList().Select(x => new TestsReferenceW(x)));

                    return tests.GroupBy(x => x.TestName).ToDictionary(x => x.FirstOrDefault().TestName);
            }

            return new Dictionary<string, IGrouping<string, TestsReferenceW>>();
        }
    }
}
