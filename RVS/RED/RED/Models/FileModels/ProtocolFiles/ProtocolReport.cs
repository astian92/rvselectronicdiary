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
        private double[] cellsWidth = { 2, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8 };

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
                var tester = ReportModel.ReportParameters["TesterMKB"] as string;
                tableTitle = "7.1 РЕЗУЛТАТИ ОТ МИКРОБИОЛОГИЧНО ИЗПИТВАНЕ:";
                var data = protocolResults.Where(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.MKB);
                CreateResultsTable(secondDocument, tableTitle, data);
                InsertTesterSignature(secondDocument, tester);
            }
            if (this.hasFZH)
            {
                string number = "1";
                if (this.HasBoth)
                {
                    number = "2";
                }

                var tester = ReportModel.ReportParameters["TesterFZH"] as string;
                tableTitle = "7." + number + " РЕЗУЛТАТИ ОТ ФИЗИКОХИМИЧНО И ОРГАНОЛЕПТИЧНО ИЗПИТВАНЕ:";
                var data = protocolResults.Where(pr => pr.ProductTest.Test.TestType.ShortName == TestTypes.FZH);
                CreateResultsTable(secondDocument, tableTitle, data);
                InsertTesterSignature(secondDocument, tester);
            }

            InsertRemarks(secondDocument);
            InsertLabLeaderSignature(secondDocument);

            secondDocument.Save();

            this.Document.InsertSection();
            this.Document.InsertDocument(secondDocument);

            //InsertSignatures();

            //InsertTable();
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
            table.AutoFit = AutoFit.Window;

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
            string lastProductName = "";

            foreach (var item in data)
            {
                var row = table.InsertRow();

                if (lastProductName != item.ProductTest.Product.Name)
                {
                    row.Cells[0].Paragraphs[0].InsertText(productIndex + ".", false, textStyle);
                    productIndex++;
                    lastProductName = item.ProductTest.Product.Name;
                }

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
                    cell.Width = cellsWidth[i]; //DOESNT FUCKING WORK !!!!!!!!! 
                    cell.Paragraphs[0].Alignment = Alignment.center;
                    cell.VerticalAlignment = VerticalAlignment.Center;
                }

                SetBorder(row);
            }

            //INCOMPETENT ... just incompetent API !
            //for (int i = 0; i < table.ColumnCount; i++)
            //{
            //    table.SetColumnWidth(i, cellsWidth[i]);
            //}
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

        private void InsertTesterSignature(DocX document2, string tester)
        {
            var textStyle = new Formatting();
            textStyle.Size = 12;
            textStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var signatureHeader = document2.InsertParagraph(
                    Environment.NewLine +
                    "Извършил изпитването:",
                    false,
                    textStyle
                );
            signatureHeader.IndentationBefore = 2;

            var nameBox = document2.InsertParagraph(
                    "/" + tester + "/" + Environment.NewLine,
                    false,
                    textStyle
                );
            nameBox.IndentationBefore = 2;
        }

        private void InsertRemarks(DocX document2)
        {
            var remarks = ReportModel.ReportParameters["Remarks"] as IEnumerable<ProtocolsRemark>;

            StringBuilder remarksText = new StringBuilder();

            foreach (var remark in remarks.OrderBy(r => r.Number))
            {
                if (remark.Remark != null)
                    remarksText.Append("\rЗабележка " + remark.Number + ": " + remark.Remark.Text + Environment.NewLine + Environment.NewLine);
            }

            var remarksParagraph = document2.InsertParagraph(remarksText.ToString());
            remarksParagraph.IndentationBefore = 2;
        }

        private void InsertLabLeaderSignature(DocX document2)
        {
            var labLeader = ReportModel.ReportParameters["LabLeader"] as string;

            var textStyle = new Formatting();
            textStyle.Size = 14;
            textStyle.Bold = true;
            textStyle.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var labLeaderHeader = document2.InsertParagraph(
                    "Ръководител на лабораторията:",
                    false,
                    textStyle
                );
            labLeaderHeader.Alignment = Alignment.right;
            labLeaderHeader.IndentationAfter = 2;

            var ts2 = new Formatting();
            ts2.Size = 14;
            ts2.FontFamily = new System.Drawing.FontFamily("Times New Roman");

            var nameBox = document2.InsertParagraph(
                    //Environment.NewLine +
                    "/" + labLeader + "/",
                    false,
                    ts2
                );
            nameBox.Alignment = Alignment.right;
            nameBox.IndentationAfter = 1;
        }

    }
}