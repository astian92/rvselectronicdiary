using Novacode;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ReportGeneration.DocXApi;
using RED.Models.ReportGeneration.EPPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RED.Models.FileModels.ProtocolFiles
{
    public class ProtocolReport : DocXReportBase
    {
        public ProtocolReport(ReportModel model) 
            : base(model, "ProtocolTemplate.docx")
        {
            
        }

        protected override void FillContent()
        {
            ReplaceItems();
            InsertMethodsAndQuantities();
            InsertLists();
            InsertTable();
            InsertRemarks();
        }

        private void ReplaceItems()
        {
            var protocolNumber = ReportModel.ReportParameters["ProtocolNumber"] as string;
            var protocolIssuedDate = (DateTime)ReportModel.ReportParameters["ProtocolIssuedDate"];
            var contractor = ReportModel.ReportParameters["Contractor"] as string;
            var client = ReportModel.ReportParameters["Client"] as string;
            var letterNumber = ReportModel.ReportParameters["LetterNumber"] as int?;
            var letterDate = (DateTime)ReportModel.ReportParameters["LetterDate"];
            var requestDate = (DateTime)ReportModel.ReportParameters["RequestDate"];
            var labLeader = ReportModel.ReportParameters["LabLeader"] as string;
            var tester = ReportModel.ReportParameters["Tester"] as string;

            string acredetationString = "";
            if (ReportModel.ReportParameters.ContainsKey("AcredetationString"))
            {
                acredetationString = ReportModel.ReportParameters["AcredetationString"] as string;
            }

            Document.ReplaceText("#PROTOCOLNUMBER", protocolNumber);
            Document.ReplaceText("#PROTOCOLISSUEDDATE", protocolIssuedDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#CONTRACTOR", contractor);
            Document.ReplaceText("#CLIENT", client);
            Document.ReplaceText("#LETTERNUMBER", letterNumber.HasValue ? "№" + letterNumber.ToString() : "");
            Document.ReplaceText("#LETTERDATE", letterDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQUESTDATE", requestDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQHOUR", requestDate.Hour.ToString());
            Document.ReplaceText("#REQMIN", requestDate.Minute.ToString());
            Document.ReplaceText("#LABLEADER", labLeader);
            Document.ReplaceText("#TESTER", tester);

            Document.ReplaceText("#ACREDETATIONSTRING", acredetationString);
        }

        private void InsertMethodsAndQuantities()
        {
            var methods = ReportModel.ReportParameters["Methods"] as IEnumerable<string>;
            var methodsString = string.Join("; ", methods);

            Document.ReplaceText("#TESTMETHODSLIST", methodsString);

            var quantities = ReportModel.ReportParameters["Quantities"] as IEnumerable<string>;
            var quantitiesString = string.Join("; ", quantities);

            Document.ReplaceText("#QUANTITIESLIST", quantitiesString);
        }

        private void InsertLists()
        {
            var products = ReportModel.ReportParameters["Products"] as IEnumerable<Product>;
            products = products.OrderBy(p => p.Number);

            var categories = products.SelectMany(p => p.ProductTests.Select(pt => pt.Test.TestCategory.Name)).Distinct();

            var catProds = new List<CategoryProducts>();
            foreach (var cat in categories)
            {
                var inProducts = products.Where(p => p.ProductTests.Any(pt => pt.Test.TestCategory.Name == cat));
                var catProdItem = new CategoryProducts();
                catProdItem.Category = cat;
                catProdItem.Products = inProducts.Select(p => new NumberNamePair() { Name = p.Name, Number = p.Number }).ToArray();
                catProds.Add(catProdItem);
            }

            //order the damn categories
            var orderedCategories = new List<CategoryProducts>();
            var number = 1;
            while (catProds.Count > 0)
            {
                var cats = catProds.Where(c => c.Products.Any(p => p.Number == number));
                if (cats.Count() > 0)
                {
                    orderedCategories.AddRange(cats);
                    catProds.RemoveAll(c => cats.Contains(c));
                    //foreach (var cat in cats)
                    //{
                    //    catProds.Remove(cat);
                    //}
                }
                number++;
            }

            //set styles
            var categoryStyle = new Formatting();
            categoryStyle.Size = 14;
            categoryStyle.Bold = true;

            var productsStyle = new Formatting();
            productsStyle.Size = 14;

            var listItem = Document.Lists[0].Items[0];
            bool isLabelInserted = false;
            string label = @"/ Наименование на пробата – тип, марка, вид и др. /";
            string afterCategoryIntervals = "";
            foreach (var cat in orderedCategories)
            {
                listItem.InsertText(cat.Category + ":" + Environment.NewLine + afterCategoryIntervals, false, categoryStyle);

                if (!isLabelInserted)
                {
                    listItem.InsertText(label + Environment.NewLine + "     ");
                    isLabelInserted = true;
                    afterCategoryIntervals = "    ";
                }

                var catProducts = string.Join("\n    ", cat.Products.OrderBy(p => p.Number).Select(p => p.Concatenated));
                listItem.InsertText(catProducts + Environment.NewLine, false, productsStyle);
            }
        }

        private void InsertTable()
        {
            var cellStyle0 = new Formatting();
            cellStyle0.Size = 12;

            var cellStyle1 = new Formatting();
            cellStyle1.Size = 12;
            cellStyle1.Italic = true;

            var cellStyle3 = new Formatting();
            cellStyle3.Size = 10;

            var protocolResults = ReportModel.ReportParameters["ProtocolResults"] as IOrderedEnumerable<ProtocolResult>;
            var table = Document.Tables.First();
            int rowIndex = 2;
            foreach (var result in protocolResults)
            {
                if (rowIndex > 2)
                {
                    var row = table.InsertRow();
                    row.Cells[0].Width = 0.83;
                    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[1].Width = 2.99;
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].Width = 1.74;
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[3].Width = 4;
                    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[4].Width = 1.97;
                    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[5].Width = 4.75;
                    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[6].Width = 2.25;
                    row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[7].Width = 2;
                    row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
                }

                table.Rows[rowIndex].Cells[0].Paragraphs[0].InsertText((rowIndex - 1) + ".", false, cellStyle0);
                table.Rows[rowIndex].Cells[0].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[1].Paragraphs[0].InsertText(result.ProductTest.Test.Name, false, cellStyle1);
                table.Rows[rowIndex].Cells[1].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[2].Paragraphs[0].InsertText(result.ProductTest.Test.UnitName, false, cellStyle0);
                table.Rows[rowIndex].Cells[2].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[3].Paragraphs[0].InsertText(result.ProductTest.Test.TestMethods, false, cellStyle0);
                table.Rows[rowIndex].Cells[3].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[4].Paragraphs[0].InsertText(result.ResultNumber, false, cellStyle0);
                table.Rows[rowIndex].Cells[4].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[5].Paragraphs[0].InsertText(result.Results, false, cellStyle0);
                table.Rows[rowIndex].Cells[5].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[6].Paragraphs[0].InsertText(result.MethodValue, false, cellStyle0);
                table.Rows[rowIndex].Cells[6].Paragraphs[0].Alignment = Alignment.center;

                table.Rows[rowIndex].Cells[7].Paragraphs[0].InsertText(result.ProductTest.Test.Temperature, false, cellStyle3);
                table.Rows[rowIndex].Cells[7].Paragraphs[0].Alignment = Alignment.center;

                rowIndex++;
            }
        }

        private void InsertRemarks()
        {
            var remarks = ReportModel.ReportParameters["Remarks"] as IEnumerable<ProtocolsRemark>;

            StringBuilder remarksText = new StringBuilder();

            foreach (var remark in remarks.OrderBy(r => r.Number))
            {
                if(remark.Remark != null)
                    remarksText.Append("\rЗабележка " + remark.Number + ": " + remark.Remark.Text + Environment.NewLine + Environment.NewLine);
            }

            Document.ReplaceText("#REMARKSLIST", remarksText.ToString());
        }
    }
}