using System;

namespace RED.Models.ReportGeneration.EPPlus.ReportAttributes
{
    public class ReportHeaderPropertyAttribute : Attribute
    {   
        public string Column;
        public string LastColumn;
        public string Value;

        public ReportHeaderPropertyAttribute(string value, string column, string lastColumn = null)
        {
            this.Value = value;
            this.Column = column;
            this.LastColumn = lastColumn;
        }

        public bool Merged
        {
            get { return !string.IsNullOrEmpty(LastColumn); }
        }
    }
}