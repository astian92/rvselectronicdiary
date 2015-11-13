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
            ReplaceInTitle();

            int row = 8;

            foreach (var item in reportData)
            {
                Cells["B" + row].Value = item.Number;
                Cells["C" + row].Value = item.SampleType;
                Cells["D" + row].Value = item.Quantity;

                foreach (var test in item.TestNames)
                {
                    Cells["E" + row].Value = test;
                    FormatRow(row);
                    row++;
                    InsertRow(row);
                }

                if (item.TestNames.Length == 0)
                {
                    FormatRow(row);
                    row++;
                    InsertRow(row);
                }
            }

            ReplaceInFooter(row);
        }

        private void ReplaceInTitle()
        {
            var queryNumber = ReportModel.ReportParameters["RequestNumber"];
            var date = ReportModel.ReportParameters["Date"] as DateTime?;

            var title = Cells["A5"].Value;
            Cells["A5"].Value = title.ToString()
                .Replace("#NUMBER", queryNumber.ToString())
                .Replace("#DATE", date == null ? "" : date.Value.ToLocalTime().ToString("d.MM.yyyy г"))
                .Replace("#TIME", date == null ? "" : date.Value.ToLocalTime().ToString("HH:mm"));
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

            var footer = Cells["B" + (row + 2)].Value;
            Cells["B" + (row + 2)].Value = footer.ToString().Replace("#D", timeLength);
        }
    }
}