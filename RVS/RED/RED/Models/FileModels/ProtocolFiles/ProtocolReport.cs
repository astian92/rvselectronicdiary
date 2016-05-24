using Novacode;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ReportGeneration.DocXApi;
using RED.Models.ReportGeneration.EPPlus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace RED.Models.FileModels.ProtocolFiles
{
    public class ProtocolReport : DocXReportBase
    {
        private IOrderedEnumerable<ProtocolResult> modelItems;
        private double[] cellsWidth = { 2d, 2d, 2d, 2d, 2d, 2d, 2d, 2d, 2d };

        private bool hasMKB;
        private bool hasFZH;
        private bool HasBoth
        {
            get
            {
                return this.hasMKB && this.hasFZH;
            }
        }

        public ProtocolReport(ReportModel model) 
            : base(model, "ProtocolTemplate.docx")
        {
            this.modelItems = ReportModel.ReportParameters["ProtocolResults"] as IOrderedEnumerable<ProtocolResult>;
            this.hasMKB = this.modelItems.Any(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.MKB);
            this.hasFZH = this.modelItems.Any(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.FZH);
        }

        protected override void FillContent()
        {
            ReplaceItems();
            InsertMethodsAndQuantities();
            InsertLists();

            var secondDocument = CreateLandscapePart();
            var protocolResults = ReportModel.ReportParameters["ProtocolResults"] as IOrderedEnumerable<ProtocolResult>;

            string tableTitle = string.Empty;
            if (this.hasMKB)
            {
                tableTitle = "7.1 РЕЗУЛТАТИ ОТ МИКРОБИОЛОГИЧНО ИЗПИТВАНЕ:";
                var data = protocolResults.Where(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.MKB);
                CreateResultsTable(secondDocument, tableTitle, data);
            }
            if (this.hasFZH)
            {
                string number = "1";
                if (this.HasBoth)
                {
                    number = "2";
                }

                tableTitle = "7." + number + " РЕЗУЛТАТИ ОТ ФИЗИКОХИМИЧНО И ОРГАНОЛЕПТИЧНО ИЗПИТВАНЕ:";
                var data = protocolResults.Where(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.FZH);
                CreateResultsTable(secondDocument, tableTitle, data);
            }
            
            secondDocument.Save();

            this.Document.InsertSection();
            this.Document.InsertDocument(secondDocument);


            //InsertTable();
            //InsertRemarks();
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
            Document.ReplaceText("#LETTERNUMBER", letterNumber.HasValue ? "№" + letterNumber.ToString() + " " : "");
            Document.ReplaceText("#LETTERDATE", letterDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQUESTDATE", requestDate.ToString("dd.MM.yyyy"));
            Document.ReplaceText("#REQHOUR", requestDate.Hour.ToString());
            Document.ReplaceText("#REQMIN", requestDate.Minute.ToString());
            Document.ReplaceText("#LABLEADER", labLeader); //-LATER ON SECOND PAGE
            //Document.ReplaceText("#TESTER", tester);

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

            var builder = new StringBuilder();

            foreach (var product in products.OrderBy(p => p.Number))
            {
                builder.AppendLine(product.Number + ". " + product.Name); //continue with " - " product category ? (not one category, but many ...) TALK WITH IVO
            }

            Document.ReplaceText("#PRODUCTSLIST", builder.ToString());
        }

        public DocX CreateLandscapePart()
        {
            System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
            DocX document2 = DocX.Create(ms2);
            document2.PageLayout.Orientation = Novacode.Orientation.Landscape;

            var titleStyle = new Formatting();
            titleStyle.Size = 12;
            titleStyle.Bold = true;
            titleStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            Paragraph p = document2.InsertParagraph("7. РЕЗУЛТАТИ ОТ ИЗПИТВАНЕ", false, titleStyle);
            p.IndentationBefore = 2;

            return document2;
        }

        private void CreateResultsTable(DocX document2, string tableTitle, IEnumerable<ProtocolResult> data) //add items later
        {
            var titleStyle = new Formatting();
            titleStyle.Size = 12;
            titleStyle.Bold = true;
            titleStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var title = document2.InsertParagraph(tableTitle, false, titleStyle);
            title.IndentationBefore = 2;
            title.InsertText(Environment.NewLine);

            var table = document2.InsertTable(1, 9);
            //table.AutoFit = AutoFit.Window;

            InsertTableHeader(table);
            InsertTableNumerationRow(table);

            PopulateTable(table, data);

            PrepareCells(table);
        }

        private void InsertTableHeader(Table table)
        {
            var textStyle = new Formatting();
            textStyle.Size = 9;
            textStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var row = table.Rows[0];

            row.Cells[0].Paragraphs[0].InsertText("№ по ред", false, textStyle);
            row.Cells[1].Paragraphs[0].InsertText("№ на образеца по вх/изх. дневник", false, textStyle);
            row.Cells[2].Paragraphs[0].InsertText("Продукт", false, textStyle);
            row.Cells[3].Paragraphs[0].InsertText("Изпитван показател", false, textStyle);
            row.Cells[4].Paragraphs[0].InsertText("Единица на величина", false, textStyle);
            row.Cells[5].Paragraphs[0].InsertText("Метод на изследване", false, textStyle);
            row.Cells[6].Paragraphs[0].InsertText("Резултат от изпитването", false, textStyle);
            row.Cells[7].Paragraphs[0].InsertText("Стойност и допуск на показателя", false, textStyle);
            row.Cells[8].Paragraphs[0].InsertText("Условия на изпитването", false, textStyle);
        }

        private void InsertTableNumerationRow(Table table)
        {
            var textStyle = new Formatting();
            textStyle.Size = 9;
            textStyle.Bold = true;
            textStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var row = table.InsertRow();

            for (int i = 0; i < table.ColumnCount; i++)
            {
                row.Cells[i].Paragraphs[0].InsertText((i + 1).ToString(), false, textStyle);
            }
        }

        private void PopulateTable(Table table, IEnumerable<ProtocolResult> data)
        {
            var textStyle = new Formatting();
            textStyle.Size = 9;
            textStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            int productIndex = 1;
            
            foreach (var item in data)
            {
                var row = table.InsertRow();

                row.Cells[0].Paragraphs[0].InsertText(productIndex + ".", false, textStyle);
                row.Cells[1].Paragraphs[0].InsertText(item.ResultNumber, false, textStyle);
                row.Cells[2].Paragraphs[0].InsertText(item.ProductTest.Product.Name, false, textStyle);
                row.Cells[3].Paragraphs[0].InsertText(item.ProductTest.Test.Name, false, textStyle);
                row.Cells[4].Paragraphs[0].InsertText(item.ProductTest.Test.UnitName, false, textStyle);
                row.Cells[5].Paragraphs[0].InsertText(item.ProductTest.Test.TestMethods, false, textStyle);
                row.Cells[6].Paragraphs[0].InsertText(item.Results, false, textStyle);
                row.Cells[7].Paragraphs[0].InsertText(item.ProductTest.MethodValue, false, textStyle);
                row.Cells[8].Paragraphs[0].InsertText(item.ProductTest.Test.Temperature, false, textStyle);
                //rowIndex++;
            }
        }

        private void PrepareCells(Table table)
        {
            foreach (var row in table.Rows)
            {
                for (int i = 0; i < table.ColumnCount; i++)
                {
                    var cell = row.Cells[i];
                    cell.Width = cellsWidth[i];
                    cell.Paragraphs[0].Alignment = Alignment.center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                }

                SetBorder(row);
            }
        }

        private void SetBorder(Row row)
        {
            foreach (var cell in row.Cells)
            {
                SetBorder(cell);
            }
        }

        private void SetBorder(Cell cell)
        {
            cell.SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black));
            cell.SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black));
            cell.SetBorder(TableCellBorderType.Left, new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black));
            cell.SetBorder(TableCellBorderType.Right, new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black));
        }











        //private void InsertTable()
        //{
        //    var cellStyle0 = new Formatting();
        //    cellStyle0.Size = 12;

        //    var cellStyle1 = new Formatting();
        //    cellStyle1.Size = 12;
        //    cellStyle1.Italic = true;

        //    var cellStyle3 = new Formatting();
        //    cellStyle3.Size = 10;

        //    var protocolResults = ReportModel.ReportParameters["ProtocolResults"] as IOrderedEnumerable<ProtocolResult>;
        //    var table = Document.Tables.First();
        //    int rowIndex = 2;
        //    foreach (var result in protocolResults)
        //    {
        //        if (rowIndex > 2)
        //        {
        //            var row = table.InsertRow();
        //            row.Cells[0].Width = 0.83;
        //            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[1].Width = 2.99;
        //            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[2].Width = 1.74;
        //            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[3].Width = 4;
        //            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[4].Width = 1.97;
        //            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[5].Width = 4.75;
        //            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[6].Width = 2.25;
        //            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
        //            row.Cells[7].Width = 2;
        //            row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
        //        }

        //        table.Rows[rowIndex].Cells[0].Paragraphs[0].InsertText((rowIndex - 1) + ".", false, cellStyle0);
        //        table.Rows[rowIndex].Cells[0].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[1].Paragraphs[0].InsertText(result.ProductTest.Test.Name, false, cellStyle1);
        //        table.Rows[rowIndex].Cells[1].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[2].Paragraphs[0].InsertText(result.ProductTest.Test.UnitName, false, cellStyle0);
        //        table.Rows[rowIndex].Cells[2].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[3].Paragraphs[0].InsertText(result.ProductTest.Test.TestMethods, false, cellStyle0);
        //        table.Rows[rowIndex].Cells[3].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[4].Paragraphs[0].InsertText(result.ResultNumber, false, cellStyle0);
        //        table.Rows[rowIndex].Cells[4].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[5].Paragraphs[0].InsertText(result.Results, false, cellStyle0);
        //        table.Rows[rowIndex].Cells[5].Paragraphs[0].Alignment = Alignment.center;

        //        //table.Rows[rowIndex].Cells[6].Paragraphs[0].InsertText(result.MethodValue, false, cellStyle0);
        //        //table.Rows[rowIndex].Cells[6].Paragraphs[0].Alignment = Alignment.center;

        //        table.Rows[rowIndex].Cells[7].Paragraphs[0].InsertText(result.ProductTest.Test.Temperature, false, cellStyle3);
        //        table.Rows[rowIndex].Cells[7].Paragraphs[0].Alignment = Alignment.center;

        //        rowIndex++;
        //    }
        //}

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