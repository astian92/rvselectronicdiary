using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.FileModels
{
    public class FileProperties
    {
        public string Path { get; set; }

        public string FileName { get; set; }

        public string FullPath 
        {
            get
            {
                return this.Path + @"\" + this.FileName;
            }
        }
    }
}