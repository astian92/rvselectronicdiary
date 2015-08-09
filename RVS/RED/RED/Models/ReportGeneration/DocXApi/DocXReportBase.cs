using RED.Models.ReportGeneration.EPPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ReportGeneration.DocXApi
{
    public abstract class DocXReportBase : DocXBase
    {
        protected ReportModel ReportModel { get; set; }

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
    }
}