using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Converters
{
    public class ProductTestsConverter
    {
        /// <summary>
        /// Shallow copy !
        /// </summary>
        public ProductTest ConvertFromArchived(ArchivedProductTest aproductTest)
        {
            var productTest = new ProductTest();

            productTest.Test = new Test();
            productTest.Test.Name = aproductTest.TestName;
            productTest.Test.UnitName = aproductTest.TestUnitName;
            productTest.TestMethod = new TestMethod() { Id = Guid.NewGuid(), Method = aproductTest.TestMethods };
            productTest.Test.AcredetationLevel = new AcredetationLevel();
            productTest.Test.AcredetationLevel.Level = aproductTest.TestAcredetationLevel;
            productTest.Test.Temperature = aproductTest.TestTemperature;
            productTest.Test.TestCategory = new TestCategory();
            productTest.Test.TestCategory.Name = aproductTest.TestCategory;
            productTest.Test.TestType = new TestType() { Id = Guid.NewGuid(), Type = aproductTest.TestType, ShortName = aproductTest.TestTypeShortName };

            productTest.Product = new Product(); //not using converter because RECURSION
            productTest.Product.Number = aproductTest.ArchivedProduct.Number;
            productTest.Product.Name = aproductTest.ArchivedProduct.Name;
            productTest.Product.Quantity = aproductTest.ArchivedProduct.Quantity;
            //and no product tests in it !

            return productTest;
        }

        /// <summary>
        /// Shallow copy !
        /// </summary>
        public ArchivedProductTest ConvertToArchived(ProductTest productTest)
        {
            var archivedProductTest = new ArchivedProductTest();

            archivedProductTest.TestName = productTest.Test.Name;
            archivedProductTest.TestUnitName = productTest.Test.UnitName;
            archivedProductTest.TestMethods = productTest.TestMethod.Method;
            archivedProductTest.TestAcredetationLevel = productTest.Test.AcredetationLevel.Level;
            archivedProductTest.TestTemperature = productTest.Test.Temperature;
            archivedProductTest.TestCategory = productTest.Test.TestCategory.Name;

            return archivedProductTest;
        }
    }
}