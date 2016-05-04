using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RED.Models.ElectronicDiary.Tests;

namespace RED.Models.ElectronicDiary
{
    public class SimpleProduct
    {
        public string Name { get; set; }

        public Guid Key { get; set; }

        public List<TestSignature> Tests { get; set; }

        public SimpleProduct()
        {
            this.Tests = new List<TestSignature>();
        }
    }

    public class TestSignature
    {
        public Guid Id { get; set; }

        public Guid Key { get; set; }

        public string Type { get; set; }

        public int Units { get; set; }

        public string Name { get; set; }

        public string MethodValue { get; set; }

        public string Remark { get; set; }
    }
}