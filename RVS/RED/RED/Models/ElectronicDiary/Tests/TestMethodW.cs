using System;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestMethodW
    {
        public TestMethodW(TestMethod method)
        {
            this.Id = method.Id;
            this.TestId = method.TestId;
            this.Method = method.Method;
        }

        public Guid Id { get; set; }

        public Guid TestId { get; set; }

        public string Method { get; set; }

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