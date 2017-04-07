using System.Collections.Generic;

namespace RED.Models.ReportGeneration.EPPlus
{
    public class ReportModel
    {
        public ReportModel()
        {
            this.ReportParameters = new Dictionary<string, object>();
        }

        public Dictionary<string, object> ReportParameters { get; set; }
        
        public IEnumerable<IReportable> ReportItems { get; set; }
    }
}