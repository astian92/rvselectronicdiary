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
        public int Number { get; set; }

        public string SampleType { get; set; }

        public string Quantity { get; set; }

        public string[] TestNames { get; set; }
    }
}