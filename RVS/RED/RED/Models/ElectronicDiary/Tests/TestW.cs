using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestW
    {
        public Guid Id { get; set; }
        public Guid TestCategoryId { get; set; }
        public string Name { get; set; }
        public string TestMethods { get; set; }
        public Guid AcredetationLevelId { get; set; }

        public virtual AcredetationLevel AcredetationLevel { get; set; }
        public virtual TestCategory TestCategory { get; set; }
        public virtual ICollection<DiaryTest> DiaryTests { get; set; }

        public TestW()
        {
            this.DiaryTests = new List<DiaryTest>();
        }

        public TestW(Test test)
        {
            this.Id = test.Id;
            this.TestCategoryId = test.TestCategoryId;
            this.Name = test.Name;
            this.TestMethods = test.TestMethods;
            this.AcredetationLevelId = test.AcredetationLevelId;

            this.AcredetationLevel = test.AcredetationLevel;
            this.TestCategory = test.TestCategory;
            this.DiaryTests = test.DiaryTests;
        }

        public Test ToBase()
        {
            Test test = new Test();

            test.Id = this.Id;
            test.TestCategoryId = this.TestCategoryId;
            test.Name = this.Name;
            test.TestMethods = this.TestMethods;
            test.AcredetationLevelId = this.AcredetationLevelId;

            test.AcredetationLevel = this.AcredetationLevel;
            test.TestCategory = this.TestCategory;
            test.DiaryTests = this.DiaryTests;

            return test;
        }

    }
}