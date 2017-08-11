using System;
using System.Linq.Expressions;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Tests;

namespace RED.Mappings
{
    public static class TestMappings
    {
        public static readonly Expression<Func<Test, TestW>> ToTestW =
            entity => new TestW()
            {
                Id = entity.Id,
                TestCategoryId = entity.TestCategoryId,
                Name = entity.Name,
                AcredetationLevelId = entity.AcredetationLevelId,
                Temperature = entity.Temperature,
                UnitName = entity.UnitName,
                TypeId = entity.TypeId,
                MethodValue = entity.MethodValue,
                FullName = entity.Name + " - " + entity.TestCategory.Name,
                FullValue = entity.TestType.ShortName + "_" + entity.Id,

                AcredetationLevel = entity.AcredetationLevel,
                TestCategory = entity.TestCategory,
                TestType = entity.TestType,
                TestMethods = entity.TestMethods,
            };

        public static readonly Expression<Func<Test, TestW>> ToTestSelectList =
            entity => new TestW()
            {
                FullName = entity.Name + " - " + entity.TestCategory.Name,
                FullValue = entity.TestType.ShortName + "_" + entity.Id
            };

        public static readonly Expression<Func<TestMethod, TestMethodW>> ToTestMethodW =
            entity => new TestMethodW()
            {
                Id = entity.Id,
                TestId = entity.TestId,
                Method = entity.Method
            };
    }
}