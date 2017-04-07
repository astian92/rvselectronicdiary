using System.Configuration;
using System.IO;
using Novacode;

namespace RED.Models.ReportGeneration.DocXApi
{
    public abstract class DocXBase
    {
        private string mappedHomePath;
        private string fileName;
        private DocX document;

        public DocXBase()
        {
            Initialize();
        }

        public DocXBase(string fileName)
        {
            this.fileName = fileName;
            this.mappedHomePath = ConfigurationManager.AppSettings["TemplatesSourcePath"];
            Initialize();
        }

        public bool NoTemplate
        {
            get { return string.IsNullOrEmpty(fileName); }
        }

        public DocX Document
        {
            get
            {
                return this.document;
            }
        }

        public byte[] GenerateReport()
        {
            FillContent();

            var memoryStr = new MemoryStream();
            this.Document.SaveAs(memoryStr);

            return memoryStr.ToArray();
        }

        /// <summary>
        /// Override to write content in the excel file.
        /// </summary>
        protected abstract void FillContent();

        private void Initialize()
        {
            if (NoTemplate)
            {
                this.document = DocX.Create(mappedHomePath + "NoTemplate.docx");
            }
            else
            {
                this.document = DocX.Load(mappedHomePath + fileName);
            }
        }
    }
}