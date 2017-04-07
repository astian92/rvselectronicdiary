using RED.Models.ReportGeneration.EPPlus;

namespace RED.Models.ReportGeneration.DocXApi
{
    public abstract class DocXReportBase : DocXBase
    {
        public DocXReportBase(ReportModel model)
            : base()
        {
            this.ReportModel = model;
        }

        public DocXReportBase(ReportModel model, string fileName)
            : base(fileName)
        {
            this.ReportModel = model;
        }

        protected ReportModel ReportModel { get; set; }
    }
}