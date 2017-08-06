using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Novacode;
using RED.Models.DataContext;
using RED.Models.ReportGeneration.DocXApi;
using RED.Models.ReportGeneration.EPPlus;

namespace RED.Models.FileModels.ProtocolFiles
{
    public class ProtocolReport : DocXReportBase
    {
        private IOrderedEnumerable<ProtocolResult> modelItems;

        public ProtocolReport(ReportModel model)
            : base(model, "ProtocolTemplate.docx")
        {
            this.modelItems = ReportModel.ReportParameters["ProtocolResults"] as IOrderedEnumerable<ProtocolResult>;
        }
        
        protected override void FillContent()
        {
            ReplaceItems();
            InsertMethodsAndQuantities();
            InsertLists();

            InsertResultsInTable();

            ReplaceRemarks();
            ReplaceTester();
        }

        private void ReplaceTester()
        {
            var tester = ReportModel.ReportParameters["Tester"] as string;
            Document.ReplaceText("#TESTER", tester);
        }

        private void InsertResultsInTable()
        {
            var textStyle = new Formatting();
            textStyle.Size = 9;
            textStyle.FontFamily = new FontFamily("Times New Roman");

            var table = Document.Tables.First();

            var items = modelItems
                .OrderBy(mi => mi.ProductTest.Product.Number)
                .ThenBy(mi => mi.ProductTest.Test.TestType.SortOrder);

            int i = 1;
            foreach (var item in items)
            {
                var row = table.InsertRow();

                row.Cells[0].Paragraphs[0].InsertText(i.ToString() + ".", false, textStyle);
                row.Cells[0].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[0].Width = 40;
                row.Cells[1].Paragraphs[0].InsertText(item.ProductTest.Test.Name, false, textStyle);
                row.Cells[1].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[1].Width = 210;
                row.Cells[2].Paragraphs[0].InsertText(item.ResultNumber, false, textStyle);
                row.Cells[2].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[2].Width = 150;
                row.Cells[3].Paragraphs[0].InsertText(item.ProductTest.TestMethod.Method, false, textStyle);
                row.Cells[3].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[3].Width = 170;
                row.Cells[4].Paragraphs[0].InsertText(item.Results, false, textStyle);
                row.Cells[4].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[4].Width = 290;
                row.Cells[5].Paragraphs[0].InsertText(item.ProductTest.MethodValue, false, textStyle);
                row.Cells[5].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[6].Paragraphs[0].InsertText(item.ProductTest.Test.UnitName, false, textStyle);
                row.Cells[6].Paragraphs[0].Alignment = Alignment.center;
                row.Cells[7].Paragraphs[0].InsertText(item.ProductTest.Test.Temperature, false, textStyle);
                row.Cells[7].Paragraphs[0].Alignment = Alignment.center;
                i++;
            }
        }

        private void ReplaceItems()
        {
            var acreditationRegisteredDate = (DateTime)ReportModel.ReportParameters["AcreditationRegisteredDate"];
            var acreditationValidToDate = (DateTime)ReportModel.ReportParameters["AcreditationValidToDate"];
            var protocolNumber = ReportModel.ReportParameters["ProtocolNumber"] as string;
            var protocolIssuedDate = (DateTime)ReportModel.ReportParameters["ProtocolIssuedDate"];
            var contractor = ReportModel.ReportParameters["Contractor"] as string;
            var client = ReportModel.ReportParameters["Client"] as string;
            var letterNumber = ReportModel.ReportParameters["LetterNumber"] as int?;
            var letterDate = (DateTime)ReportModel.ReportParameters["LetterDate"];
            var requestDate = (DateTime)ReportModel.ReportParameters["RequestDate"];
            var labLeader = ReportModel.ReportParameters["LabLeader"] as string;

            string acredetationString = string.Empty;
            if (ReportModel.ReportParameters.ContainsKey("AcredetationString"))
            {
                acredetationString = ReportModel.ReportParameters["AcredetationString"] as string;
            }

            Document.ReplaceText("#REGDATE", acreditationRegisteredDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#VALIDDATE", acreditationValidToDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#PROTOCOLNUMBER", protocolNumber);
            Document.ReplaceText("#PROTOCOLISSUEDDATE", protocolIssuedDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#CONTRACTOR", contractor);
            Document.ReplaceText("#CLIENT", client);
            Document.ReplaceText("#LETTERNUMBER", letterNumber.HasValue ? "№" + letterNumber.ToString() + " " : string.Empty);
            Document.ReplaceText("#LETTERDATE", letterDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQUESTDATE", requestDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQHOUR", requestDate.Hour.ToString());
            Document.ReplaceText("#REQMIN", requestDate.Minute.ToString());
            Document.ReplaceText("#LABLEADER", labLeader);

            Document.ReplaceText("#ACREDETATIONSTRING", acredetationString);
        }

        private void InsertMethodsAndQuantities()
        {
            var methods = ReportModel.ReportParameters["Methods"] as IEnumerable<MethodsModel>;
            var methodsString = new StringBuilder();

            int i = 1;
            foreach (var item in methods)
            {
                methodsString.AppendLine(i + ". " + item.TestName + " - " + item.TestMethod);
                i++;
            }

            Document.ReplaceText("#TESTMETHODSLIST", methodsString.ToString());

            var quantities = ReportModel.ReportParameters["Quantities"] as IEnumerable<string>;
            var quantitiesString = string.Join("; ", quantities);

            Document.ReplaceText("#QUANTITIESLIST", quantitiesString);
        }

        private void InsertLists()
        {
            var products = ReportModel.ReportParameters["Products"] as IEnumerable<Product>;
            products = products.OrderBy(p => p.Number);

            //set styles
            var productsStyle = new Formatting();
            productsStyle.Size = 14;
            var acreditationLevel = ReportModel.ReportParameters["category"].ToString();

            var tests = products.SelectMany(p => p.ProductTests);
            var categories = tests.Select(t => t.Test.TestCategory);

            var builder = new StringBuilder();

            foreach (var category in categories.Distinct())
            {
                builder.AppendLine(category.Name);

                var pr = products.Where(p => p.ProductTests.Any(pt => pt.Test.TestCategoryId == category.Id))
                    .OrderBy(p => p.Number);

                foreach (var product in pr)
                {
                    builder.AppendLine(acreditationLevel + product.Diary.Number + "-" + product.Number + " - " + product.Name);
                }
            }

            Document.ReplaceText("#PRODUCTSLIST", builder.ToString());
        }

        private void ReplaceRemarks()
        {
            var remarks = ReportModel.ReportParameters["Remarks"] as IEnumerable<ProtocolsRemark>;

            StringBuilder remarksText = new StringBuilder();

            foreach (var remark in remarks.OrderBy(r => r.Number))
            {
                if (remark.Remark != null)
                {
                    remarksText.Append("\rЗабележка " + remark.Number + ": " + remark.Remark.Text + Environment.NewLine + Environment.NewLine);
                }
            }

            Document.ReplaceText("#REMARKS", remarksText.ToString());
        }
    }
}