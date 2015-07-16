using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED.Models.Dashboard
{
    public class TestsReferenceW
    {
        public string TestName { get; set; }
        public string ProductName { get; set; }
        public string ProductQuantity { get; set; }
        public string ClientName { get; set; }

        public TestsReferenceW()
        {

        }

        public TestsReferenceW(ProductTest test)
        {
            this.TestName = test.Test.Name;
            this.ProductName = test.Product.Name;
            this.ProductQuantity = test.Product.Quantity;
            this.ClientName = test.Product.Diary.Client.Name;
        }

        public TestsReferenceW(ArchivedProductTest test)
        {
            this.TestName = test.TestName;
            this.ProductName = test.ArchivedProduct.Name;
            this.ProductQuantity = test.ArchivedProduct.Quantity;
            this.ClientName = test.ArchivedProduct.ArchivedDiary.Client;
        }
    }
}
