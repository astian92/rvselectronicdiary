using System.Configuration;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style.XmlAccess;

namespace RED.Models.ReportGeneration.EPPlus
{
    public abstract class EPPlusExcelBase
    {
        private string mappedHomePath;
        private string fileName;
        private ExcelPackage package;
        private ExcelWorkbook workbook;
        private ExcelWorksheet worksheet;

        public EPPlusExcelBase()
        {
            Initialize();
        }

        public EPPlusExcelBase(string fileName)
        {
            this.fileName = fileName;
            this.mappedHomePath = ConfigurationManager.AppSettings["TemplatesSourcePath"];
            Initialize();
        }

        public bool NoTemplate
        {
            get { return string.IsNullOrEmpty(fileName); }
        }

        public ExcelPackage Package
        {
            get { return this.package; }
        }

        public ExcelWorkbook Workbook
        {
            get { return this.workbook; }
        }

        public ExcelWorksheet Worksheet
        {
            get { return this.worksheet; }
        }

        public ExcelRange Cells
        {
            get { return this.Worksheet.Cells; }
        }

        public ExcelStyles Styles
        {
            get { return this.workbook.Styles; }
        }

        public byte[] GenerateReport()
        {
            FillContent();

            return this.package.GetAsByteArray();
        }

        //functionality wrappers: *************
        public ExcelRow InsertRow(int rowPosition)
        {
            this.Worksheet.InsertRow(rowPosition, 1);
            return Worksheet.Row(rowPosition);
        }

        public ExcelRow InsertRow(int rowPosition, int copyStylesFromRow)
        {
            this.Worksheet.InsertRow(rowPosition, 1, copyStylesFromRow);
            return Worksheet.Row(rowPosition);
        }

        public ExcelRow InsertRow(int rowPosition, ExcelRow copyStylesFromRow)
        {
            this.Worksheet.InsertRow(rowPosition, 1, copyStylesFromRow.Row);
            return Worksheet.Row(rowPosition);
        }

        public ExcelRow InsertRow(int rowPosition, string namedStyle)
        {
            ExcelRow row = this.InsertRow(rowPosition);
            row.StyleName = namedStyle;
            return row;
        }

        public ExcelRow InsertRow(int rowPosition, ExcelNamedStyleXml namedStyle)
        {
            return InsertRow(rowPosition, namedStyle.Name);
        }

        public ExcelNamedStyleXml CreateNamedStyle(string name)
        {
            return this.Styles.CreateNamedStyle(name);
        }

        public ExcelNamedStyleXml GetStyle(string name)
        {
            return this.Styles.NamedStyles.First(s => s.Name == name);
        }

        /// <summary>
        /// Override to write content in the excel file.
        /// </summary>
        protected abstract void FillContent();

        private void Initialize()
        {
            if (NoTemplate)
            {
                this.package = new ExcelPackage();
            }
            else
            {
                FileInfo templateFile = new FileInfo(mappedHomePath + fileName);
                this.package = new ExcelPackage(templateFile);
            }

            this.workbook = this.Package.Workbook;

            if (workbook.Worksheets.Count > 0)
            {
                this.worksheet = this.Workbook.Worksheets.First();
            }
            else
            {
                this.worksheet = this.workbook.Worksheets.Add("Content");
            }
        }
    }
}