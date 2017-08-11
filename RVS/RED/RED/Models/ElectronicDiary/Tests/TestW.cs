using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestW
    {
        public TestW()
        {
            TestMethods = new HashSet<TestMethod>();
        }

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

        public Test ToBase()
        {
            var test = new Test();

            test.Id = Id;
            test.TestCategoryId = TestCategoryId;
            test.Name = Name;
            test.AcredetationLevelId = AcredetationLevelId;
            test.Temperature = Temperature;
            test.UnitName = UnitName;
            test.TypeId = TypeId;
            test.MethodValue = MethodValue;

            test.AcredetationLevel = AcredetationLevel;
            test.TestCategory = TestCategory;
            test.TestType = TestType;
            test.TestMethods = TestMethods;

            return test;
        }
    }
}