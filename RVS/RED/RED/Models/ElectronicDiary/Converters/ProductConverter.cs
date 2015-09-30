using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Converters
{
    public class ProductConverter
    {
        public Product ConvertFromArchived(ArchivedProduct aproduct)
        {
            var product = new Product();

            //product.Diary - not needed yet
            product.Number = aproduct.Number;
            product.Name = aproduct.Name;
            product.Quantity = aproduct.Quantity;
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