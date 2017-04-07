using System;

namespace RED.Models.ReportGeneration.EPPlus.ReportAttributes
{
    public class ReportPropertyAttribute : Attribute
    {
        public string Column;
        public string LastColumn;

        public ReportPropertyAttribute(string column, string lastColumn = null)
        {
            this.Column = column;
            this.LastColumn = lastColumn;
        }

        public bool Merged
        {
            get { return !string.IsNullOrEmpty(LastColumn); }
        }
    }
}