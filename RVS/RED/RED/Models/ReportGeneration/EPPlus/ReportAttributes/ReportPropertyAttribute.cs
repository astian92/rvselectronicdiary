using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ReportGeneration.EPPlus.ReportAttributes
{
    public class ReportPropertyAttribute : Attribute
    {
        public string column;
        public string lastColumn;

        public bool merged
        {
            get { return !string.IsNullOrEmpty(lastColumn); }
        }

        public ReportPropertyAttribute(string column, string lastColumn = null)
        {
            this.column = column;
            this.lastColumn = lastColumn;
        }
    }
}