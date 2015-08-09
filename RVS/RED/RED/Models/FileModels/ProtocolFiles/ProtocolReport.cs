using RED.Models.ReportGeneration.DocXApi;
using RED.Models.ReportGeneration.EPPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.FileModels.ProtocolFiles
{
    public class ProtocolReport : DocXReportBase
    {
        public ProtocolReport(ReportModel model) 
            : base(model, "ProtocolTemplate.docx")
        {
            
        }

        protected override void FillContent()
        {
            
        }
    }
}