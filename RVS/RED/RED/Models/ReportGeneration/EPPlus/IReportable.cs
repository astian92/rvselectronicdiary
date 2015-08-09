using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ReportGeneration.EPPlus
{
    public interface IReportable
    {
        //Common for All items that can be populated by a generic EPPlus Report
        //REMEMBER TO MARK PROPERTIES FOR THE REPORT WITH ATTRIBUTE FOR COLUMN!
    }
}