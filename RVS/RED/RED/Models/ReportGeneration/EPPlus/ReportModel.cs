using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ReportGeneration.EPPlus
{
    public class ReportModel
    {
        public Dictionary<string, object> ReportParameters { get; set; }
        
        public IEnumerable<IReportable> reportItems { get; set; }

        public ReportModel()
        {
            this.ReportParameters = new Dictionary<string, object>();
        }
    }
}