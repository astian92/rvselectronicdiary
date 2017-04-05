using System;
using System.Linq;
using System.Globalization;
using RED.Models.ReportGeneration.EPPlus;

namespace RED.Models.FileModels.RequestList
{
    public class RequestListReport : EPPlusBaseGeneric<RequestListModel>
    {
        public RequestListReport(ReportModel model)
            : base(model, "RequestTemplate.xlsx")
        {

        }

        protected override void FillContent()
        {
            //Get The First Table Data
            var diaryNumber = ReportModel.ReportParameters["RequestNumber"] as string;
            var testingPeriod = ReportModel.ReportParameters["TestingPeriod"] as int? ?? 0;
            DateTime? requestDate = ReportModel.ReportParameters["Date"] as DateTime?;

            int row = 6;

            if (reportData.Any(rd => rd.ProductTests.Any(pt => pt.TestType == TestTypes.MKB)))
            {
                CreateTableInTemplate("7.1 МИКРОБИОЛОГИЧНО ИЗПИТВАНЕ", ref row, diaryNumber, testingPeriod, requestDate, TestTypes.MKB);
            }

            row++;

            //Insert only MKB Tests
            if (reportData.Any(rd => rd.ProductTests.Any(pt => pt.TestType == TestTypes.FZH)))
            {
                CreateTableInTemplate("7.2 ФИЗИКОХИМИЧНО И ОРГАНОЛЕПТИЧНО ИЗПИТВАНЕ", ref row, diaryNumber, testingPeriod, requestDate, TestTypes.FZH);
            }
        }

        private void CreateTableInTemplate(string title, ref int row, string diaryNumber, int testingPeriod, DateTime? requestDate, string testType)
        {
            //1 the title
            var head = Cells["A" + row + ":F" + row];
            head.Merge = true;
            head.Value = string.Format("ЗАЯВКА № {0} / Дата {1}", diaryNumber, requestDate?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)); // / Час #TIME					
            //style the head
            FormatTitleRow(row);
             
            row += 2;

            Cells["A" + row].Value = title;
            FormatCategoryRow(row);

            //style title
            row++;

            Cells["A" + row].Value = "Вх. №";
            Cells["B" + row].Value = "Вид на пробата";
            Cells["C" + row].Value = "Показател";
            Cells["D" + row].Value = "Метод";
            Cells["E" + row].Value = "Допуск";
            Cells["F" + row].Value = "Забележка";
            FormatHeaderRow(row);

            //style table header cells
            row += 1;

            //PopulateData
            PopulateData(ref row, testType, diaryNumber);

            row += 1;

            var days = Cells["A" + row + ":C" + row];
            days.Merge = true;
            days.Value = string.Format("Срок за изпитване: {0} дни", testingPeriod);

            var tester = Cells["D" + row + ":F" + row];
            tester.Merge = true;
            tester.Value = "Приел пробата......";

            FormatFooterRow(row);

            row++;
        }

        private void PopulateData(ref int row, string testType, string diaryNumber)
        {
            var products = reportData.Where(r => r.ProductTests.Any(pt => pt.TestType == testType));

            Cells["A" + row].Value = diaryNumber;
            //no row increment ! products start on the same row

            foreach (var product in products)
            {
                Cells["B" + row].Value = product.ProductNumber + ". " + product.ProductName;

                var tests = product.ProductTests.Where(pt => pt.TestType == testType);
                foreach (var test in tests)
                {
                    Cells["C" + row].Value = test.TestName;
                    Cells["D" + row].Value = test.Method;
                    Cells["E" + row].Value = test.MethodValue;
                    Cells["F" + row].Value = test.Remark;

                    FormatDataRow(row);

                    row++;
                }
            }
        }

        private void FormatTitleRow(int row)
        {
            Cells["A" + row].Style.Font.Size = 14;
            Cells["A" + row].Style.Font.Name = "Times New Roman";
            Cells["A" + row].Style.Font.Bold = true;
            Cells["A" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }

        private void FormatCategoryRow(int row)
        {
            Cells["A" + row].Style.Font.Size = 14;
            Cells["A" + row].Style.Font.Name = "Times New Roman";
            Cells["A" + row].Style.Font.Bold = true;
            Cells["A" + row].Style.Indent = 2;
        }

        private void FormatHeaderRow(int row)
        {
            Cells["A" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["A" + row].Style.Font.Size = 12;
            Cells["A" + row].Style.Font.Name = "Times New Roman";
            Cells["A" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["B" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["B" + row].Style.Font.Size = 12;
            Cells["B" + row].Style.Font.Name = "Times New Roman";
            Cells["B" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["C" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["C" + row].Style.Font.Size = 12;
            Cells["C" + row].Style.Font.Name = "Times New Roman";
            Cells["C" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["D" + row].Style.Font.Size = 12;
            Cells["D" + row].Style.Font.Name = "Times New Roman";
            Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["E" + row].Style.Font.Size = 12;
            Cells["E" + row].Style.Font.Name = "Times New Roman";
            Cells["E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["F" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["F" + row].Style.Font.Size = 12;
            Cells["F" + row].Style.Font.Name = "Times New Roman";
            Cells["F" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }

        private void FormatDataRow(int row)
        {
            Cells["A" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["A" + row].Style.WrapText = true;
            Cells["A" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Cells["B" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["B" + row].Style.WrapText = true;
            Cells["B" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Cells["C" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["C" + row].Style.WrapText = true;
            Cells["C" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["D" + row].Style.WrapText = true;
            Cells["D" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["E" + row].Style.WrapText = true;
            Cells["E" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            Cells["F" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["F" + row].Style.WrapText = true;
            Cells["F" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            Cells["B" + row + ":E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["B" + row + ":E" + row].Style.Font.Size = 12;
            Cells["B" + row + ":E" + row].Style.Font.Name = "Times New Roman";
        }

        private void FormatFooterRow(int row)
        {
            Cells["A" + row].Style.Indent = 3;
            Cells["D" + row].Style.Indent = 3;
        }

    }
}