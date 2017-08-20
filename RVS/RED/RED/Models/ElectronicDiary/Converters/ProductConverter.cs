using System;
using System.Collections.Generic;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Converters
{
    public class ProductConverter
    {
        public Product ConvertFromArchived(ArchivedProduct aproduct)
        {
            var product = new Product();

            product.Number = aproduct.Number;
            product.Name = aproduct.Name;
            product.Quantity = aproduct.Quantity;
            product.Diary = new Diary() { Number = aproduct.ArchivedDiary.Number }; //HACK!!! (like the rest is not but still..)
            product.ProductTests = new List<ProductTest>();

            var converter = new ProductTestsConverter();
            foreach (var item in aproduct.ArchivedProductTests)
            {
                var ptest = converter.ConvertFromArchived(item);
                ptest.Product = product;
                product.ProductTests.Add(ptest);
            }

            return product;
        }

        public ArchivedProduct ConvertToArchive(Product product)
        {
            throw new NotImplementedException();
        }
    }
}