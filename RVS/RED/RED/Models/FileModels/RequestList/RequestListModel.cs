using RED.Models.ReportGeneration.EPPlus;
using RED.Models.ReportGeneration.EPPlus.ReportAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.FileModels.RequestList
{
    public class RequestListModel : IReportable
    {
        public int ProductNumber { get; set; }

        public string ProductName { get; set; }

        public IList<SubListModel> ProductTests { get; set; }
    }

    public class SubListModel : IReportable //tests
    {
        public string TestType { get; set; }

        public string TestName { get; set; }

        public string Method { get; set; }

        public string MethodValue { get; set; }

        public string Remark { get; set; }
    }
}