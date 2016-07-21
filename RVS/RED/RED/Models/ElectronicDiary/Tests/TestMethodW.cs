using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestMethodW
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Method { get; set; }

        public TestMethodW(TestMethod method)
        {
            this.Id = method.Id;
            this.TestId = method.TestId;
            this.Method = method.Method;
        }

        public TestMethod ToBase()
        {
            var method = new TestMethod();
            method.Id = this.Id;
            method.TestId = this.TestId;
            method.Method = this.Method;

            return method;
        }
    }
}