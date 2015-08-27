using RED.Models.DataContext;
using RED.Models.ElectronicDiary.ArchivedWrappers;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class ArchivedDiaryRepository : RepositoryBase
    {
        public ArchivedDiary GetArchivedDiary(Guid archivedDiaryId)
        {
            var archivedDiary = db.ArchivedDiaries.Single(ad => ad.Id == archivedDiaryId);
            return archivedDiary;
        }

        public ArchivedDiaryW GetArchivedDiaryW(Guid archivedDiaryId)
        {
            var archivedDiary = GetArchivedDiary(archivedDiaryId);
            return new ArchivedDiaryW(archivedDiary);
        }

        public void Edit(ArchivedDiaryW adiary)
        {
            var archivedDiary = db.ArchivedDiaries.Single(ad => ad.Id == adiary.Id);

            archivedDiary.Number = adiary.Number;
            archivedDiary.LetterNumber = adiary.LetterNumber;
            archivedDiary.LetterDate = adiary.LetterDate.ToUniversalTime();
            archivedDiary.AcceptanceDateAndTime = adiary.AcceptanceDateAndTime.ToUniversalTime();
            archivedDiary.Contractor = adiary.Contractor;
            archivedDiary.Client = adiary.Client;
            archivedDiary.Comment = adiary.Comment;
            archivedDiary.RequestDate = adiary.RequestDate.ToUniversalTime();
            archivedDiary.RequestAcceptedBy = adiary.RequestAcceptedBy;
            archivedDiary.ProtocolIssuedDate = adiary.ProtocolIssuedDate.ToUniversalTime();
            archivedDiary.ProtocolTester = adiary.ProtocolTester;
            archivedDiary.ProtocolLabLeader = adiary.ProtocolLabLeader;
            archivedDiary.Remark = adiary.Remark;
            archivedDiary.RequestTestingPeriod = adiary.RequestTestingPeriod;

            db.SaveChanges();
        }

        public ArchivedProduct GetArchivedProduct(Guid archivedProductId)
        {
            var archivedProduct = db.ArchivedProducts.Single(ap => ap.Id == archivedProductId);
            return archivedProduct;
        }

        public ArchivedProductW GetArchivedProductW(Guid archivedProductId)
        {
            var archivedProduct = GetArchivedProduct(archivedProductId);
            return new ArchivedProductW(archivedProduct);
        }

        public IEnumerable<ArchivedProductW> GetProducts(Guid archivedDiaryId)
        {
            var products = db.ArchivedProducts.Where(ap => ap.ArchivedDiaryId == archivedDiaryId);
            var result = products.ToList().Select(p => new ArchivedProductW(p));

            return result;
        }

        public void AddProduct(ArchivedProductW aproduct)
        {
            aproduct.Id = Guid.NewGuid();
            db.ArchivedProducts.Add(aproduct.ToBase());

            db.SaveChanges();
        }

        public void EditProduct(ArchivedProductW aproduct)
        {
            //1 - edit the product properties
            var product = db.ArchivedProducts.Single(p => p.Id == aproduct.Id);

            product.Number = aproduct.Number;
            product.Name = aproduct.Name;
            product.Quantity = aproduct.Quantity;

            //2 - update numbers of the productResults
            foreach (var test in product.ArchivedProductTests)
            {
                foreach (var result in test.ArchivedProtocolResults)
                {
                    var parts = result.ResultNumber.Split('-');

                    result.ResultNumber = test.TestAcredetationLevel.Trim() + product.ArchivedDiary.Number + "-" + product.Number.ToString() +
                        (parts.Length > 2 ? "-" + parts[2].Trim() : "");
                }
            }

            db.SaveChanges();
        }

        public bool DeleteProduct(Guid id)
        {
            var aproduct = db.ArchivedProducts.Single(c => c.Id == id);
            db.ArchivedProducts.Remove(aproduct);

            try
            {
                db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                return false;
            }

            return true;
        }

        public IEnumerable<ArchivedProductTestW> GetProductTests(Guid aproductId)
        {
            var productTests = db.ArchivedProductTests.Where(ap => ap.ArchivedProductId == aproductId);
            var result = productTests.ToList().Select(pt => new ArchivedProductTestW(pt));

            return result;
        }

        public void AddProductTest(ArchivedProductTestW aproductTest)
        {
            aproductTest.Id = Guid.NewGuid();
            db.ArchivedProductTests.Add(aproductTest.ToBase());

            db.SaveChanges();
        }


    }
}