using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestW
    {
        public Guid Id { get; set; }

        [Display(Name = "Категория")]
        public Guid TestCategoryId { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public string FullName { get; set; }

        public string FullValue { get; set; }

        [Display(Name = "Стойност на показателя")]
        public string MethodValue { get; set; }

        [Display(Name = "Вид Акредитация")]
        public Guid AcredetationLevelId { get; set; }

        [Display(Name = "Температура")]
        public string Temperature { get; set; }

        [Display(Name = "Единица на Величината")]
        public string UnitName { get; set; }

        [Required]
        [Display(Name = "Вид")]
        public Guid TypeId { get; set; }

        public virtual AcredetationLevel AcredetationLevel { get; set; }
        public virtual TestCategory TestCategory { get; set; }
        public virtual TestType TestType { get; set; }

        [Required(ErrorMessage = "Полето Методи е задължително!")]
        [Display(Name = "Методи")]
        public virtual ICollection<TestMethod> TestMethods { get; set; }

        public TestW()
        {

        }

        public TestW(Test test)
        {
            this.Id = test.Id;
            this.TestCategoryId = test.TestCategoryId;
            this.Name = test.Name;
            this.FullName = test.Name + " - " + test.TestCategory.Name;
            this.FullValue = test.TestType.ShortName + "_" + test.Id;
            this.AcredetationLevelId = test.AcredetationLevelId;
            this.Temperature = test.Temperature;
            this.UnitName = test.UnitName;
            this.TypeId = test.TypeId;
            this.MethodValue = test.MethodValue;

            this.AcredetationLevel = test.AcredetationLevel;
            this.TestCategory = test.TestCategory;
            this.TestType = test.TestType;
            this.TestMethods = test.TestMethods;
        }

        public Test ToBase()
        {
            Test test = new Test();

            test.Id = this.Id;
            test.TestCategoryId = this.TestCategoryId;
            test.Name = this.Name;
            test.AcredetationLevelId = this.AcredetationLevelId;
            test.Temperature = this.Temperature;
            test.UnitName = this.UnitName;
            test.TypeId = this.TypeId;
            test.MethodValue = this.MethodValue;

            test.AcredetationLevel = this.AcredetationLevel;
            test.TestCategory = this.TestCategory;
            test.TestType = this.TestType;
            test.TestMethods = this.TestMethods;

            return test;
        }

    }
}