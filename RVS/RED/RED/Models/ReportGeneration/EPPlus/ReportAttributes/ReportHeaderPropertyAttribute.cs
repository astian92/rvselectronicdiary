using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ReportGeneration.EPPlus.ReportAttributes
{
    public class ReportHeaderPropertyAttribute : Attribute
    {   
        public string column;
        public string lastColumn;
        public string Value;

        public bool merged
        {
            get { return !string.IsNullOrEmpty(lastColumn); }
        }

        public ReportHeaderPropertyAttribute(string value, string column, string lastColumn = null)
        {
            this.Value = value;
            this.column = column;
            this.lastColumn = lastColumn;
        }
    }
}