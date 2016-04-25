using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RED.Models.ReportGeneration;
using RED.Models.ReportGeneration.EPPlus;
using OfficeOpenXml;

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
            int row = 6;

            if (reportData.Any(rd => rd.ProductTests.Any(pt => pt.TestType == TestTypes.MKB)))
            {
                CreateTableInTemplate("7.1 МИКРОБИОЛОГИЧНО ИЗПИТВАНЕ", ref row);
            }

            row++;
            //Insert only MKB Tests

            if (reportData.Any(rd => rd.ProductTests.Any(pt => pt.TestType == TestTypes.FZH)))
            {
                CreateTableInTemplate("7.2 ФИЗИКОХИМИЧНО И ОРГАНОЛЕПТИЧНО ИЗПИТВАНЕ", ref row);
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

        private void CreateTableInTemplate(string title, ref int row)
        {
            //1 the title
            var head = Cells["A" + row + ":F" + row];
            head.Merge = true;
            head.Value = "ЗАЯВКА № #NUMBER / Дата #DATE"; // / Час #TIME					
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
            row += 2;

            var days = Cells["A" + row + ":C" + row];
            days.Merge = true;
            days.Value = "Срок за изпитване: #D дни";

            var tester = Cells["D" + row + ":F" + row];
            tester.Merge = true;
            tester.Value = "Приел пробата......";

            row++;
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