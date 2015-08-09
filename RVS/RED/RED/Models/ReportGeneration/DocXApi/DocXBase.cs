using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Novacode;
using System.Configuration;
using System.IO;

namespace RED.Models.ReportGeneration.DocXApi
{
    public abstract class DocXBase
    {
        private string mappedHomePath;
        private string fileName;

        public bool NoTemplate
        {
            get { return string.IsNullOrEmpty(fileName); }
        }

        private DocX document;
        public DocX Document
        {
            get
            {
                return this.document;
            }
        }

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

        /// <summary>
        /// Override to write content in the excel file.
        /// </summary>
        protected abstract void FillContent();

        public byte[] GenerateReport()
        {
            FillContent();

            var memoryStr = new MemoryStream();
            this.Document.SaveAs(memoryStr);

            return memoryStr.ToArray();
        }
    }
}