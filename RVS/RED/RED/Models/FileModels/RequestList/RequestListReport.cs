using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RED.Models.ReportGeneration;
using RED.Models.ReportGeneration.EPPlus;
using OfficeOpenXml;
using System.Globalization;

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

            //Insert only FZH Tests


            ReplaceInTitle(); //in order to make changes after the table headers are generated

            //foreach (var item in reportData)
            //{
            //    Cells["B" + row].Value = item.Number;
            //    Cells["C" + row].Value = item.SampleType;
            //    Cells["D" + row].Value = item.Quantity;

            //    foreach (var test in item.TestNames)
            //    {
            //        Cells["E" + row].Value = test;
            //        FormatRow(row);
            //        row++;
            //        InsertRow(row);
            //    }

            //    if (item.TestNames.Length == 0)
            //    {
            //        FormatRow(row);
            //        row++;
            //        InsertRow(row);
            //    }
            //}

            ReplaceInFooter(row);
        }

        private void CreateTableInTemplate(string title, ref int row, string diaryNumber, int testingPeriod, DateTime? requestDate, string testType)
        {
            //1 the title
            var head = Cells["A" + row + ":F" + row];
            head.Merge = true;
            head.Value = string.Format("ЗАЯВКА № {0} / Дата {1}", diaryNumber, requestDate?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)); // / Час #TIME					
            //style the head
            row += 2;

            Cells["A" + row].Value = title;
            //style title
            row++;

            Cells["A" + row].Value = "Вх. №";
            Cells["B" + row].Value = "Вид на  пробата";
            Cells["C" + row].Value = "Показател";
            Cells["D" + row].Value = "Метод";
            Cells["E" + row].Value = "Допуск";
            Cells["F" + row].Value = "Забележка";
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

                    row++;
                }
            }
        }


        private void ReplaceInTitle()
        {
            var queryNumber = ReportModel.ReportParameters["RequestNumber"];
            var date = ReportModel.ReportParameters["Date"] as DateTime?;

            //var title = Cells["A5"].Value;
            //Cells["A5"].Value = title.ToString()
            //    .Replace("#NUMBER", queryNumber.ToString())
            //    .Replace("#DATE", date == null ? "" : date.Value.ToLocalTime().ToString("d.MM.yyyy г"))
            //    .Replace("#TIME", date == null ? "" : date.Value.ToLocalTime().ToString("HH:mm"));
        }

        private void FormatRow(int row)
        {
            Cells["B" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["C" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

            Cells["B" + row + ":E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            Cells["B" + row + ":E" + row].Style.Font.Size = 12;
            Cells["B" + row + ":E" + row].Style.Font.Name = "Times New Roman";
        }

        private void ReplaceInFooter(int row)
        {
            var timeLength = ReportModel.ReportParameters["TestingPeriod"].ToString();

            //var footer = Cells["B" + (row + 2)].Value;
            //Cells["B" + (row + 2)].Value = footer.ToString().Replace("#D", timeLength);
        }
    }
}