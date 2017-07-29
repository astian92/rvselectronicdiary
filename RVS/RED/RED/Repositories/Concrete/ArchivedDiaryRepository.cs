using System;
using System.Collections.Generic;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.ArchivedWrappers;
using RED.Repositories.Abstract;

namespace RED.Repositories.Concrete
{
    public class ArchivedDiaryRepository : IArchivedDiaryRepository
    {
        private readonly RvsDbContext Db;

        public ArchivedDiaryRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public ArchivedDiary GetArchivedDiary(Guid archivedDiaryId)
        {
            var archivedDiary = Db.ArchivedDiaries.Single(ad => ad.Id == archivedDiaryId);
            return archivedDiary;
        }

        public ArchivedDiaryW GetArchivedDiaryW(Guid archivedDiaryId)
        {
            var archivedDiary = GetArchivedDiary(archivedDiaryId);
            return new ArchivedDiaryW(archivedDiary);
        }

        public void Edit(ArchivedDiaryW adiary)
        {
            var archivedDiary = Db.ArchivedDiaries.Single(ad => ad.Id == adiary.Id);

            archivedDiary.Number = adiary.Number;
            archivedDiary.LetterNumber = adiary.LetterNumber;
            archivedDiary.LetterDate = adiary.LetterDate.ToUniversalTime();
            archivedDiary.AcceptanceDateAndTime = adiary.AcceptanceDateAndTime;
            archivedDiary.Contractor = adiary.Contractor;
            archivedDiary.Client = adiary.Client;
            archivedDiary.ClientMobile = adiary.ClientMobile;
            archivedDiary.Comment = adiary.Comment;
            archivedDiary.RequestDate = adiary.RequestDate.ToUniversalTime();
            archivedDiary.RequestAcceptedBy = adiary.RequestAcceptedBy;
            archivedDiary.ProtocolIssuedDate = adiary.ProtocolIssuedDate;
            archivedDiary.ProtocolTester = adiary.ProtocolTester;
            archivedDiary.ProtocolLabLeader = adiary.ProtocolLabLeader;
            archivedDiary.Remark = adiary.Remark;
            archivedDiary.RequestTestingPeriod = adiary.RequestTestingPeriod;

            Db.SaveChanges();
        }

        public ArchivedProduct GetArchivedProduct(Guid archivedProductId)
        {
            var archivedProduct = Db.ArchivedProducts.Single(ap => ap.Id == archivedProductId);
            return archivedProduct;
        }

        public ArchivedProductW GetArchivedProductW(Guid archivedProductId)
        {
            var archivedProduct = GetArchivedProduct(archivedProductId);
            return new ArchivedProductW(archivedProduct);
        }

        public ArchivedProductTest GetArchivedProductTest(Guid archivedProductTestId)
        {
            var archivedProductTest = Db.ArchivedProductTests.Single(apt => apt.Id == archivedProductTestId);
            return archivedProductTest;
        }

        public ArchivedProductTestW GetArchivedProductTestW(Guid archivedProductTestId)
        {
            var archivedProductTest = GetArchivedProductTest(archivedProductTestId);
            return new ArchivedProductTestW(archivedProductTest);
        }

        public ArchivedProtocolResult GetArchivedProtocolResult(Guid archivedProtocolResultId)
        {
            var archivedProtocolResult = Db.ArchivedProtocolResults.Single(apr => apr.Id == archivedProtocolResultId);
            return archivedProtocolResult;
        }

        public ArchivedProtocolResultW GetArchivedProtocolResultW(Guid archivedProtocolResultId)
        {
            var archivedProtocolResult = GetArchivedProtocolResult(archivedProtocolResultId);
            return new ArchivedProtocolResultW(archivedProtocolResult);
        }

        public IEnumerable<ArchivedProductW> GetProducts(Guid archivedDiaryId)
        {
            var products = Db.ArchivedProducts.Where(ap => ap.ArchivedDiaryId == archivedDiaryId);
            var result = products.ToList().Select(p => new ArchivedProductW(p));

            return result;
        }

        public void AddProduct(ArchivedProductW aproduct)
        {
            aproduct.Id = Guid.NewGuid();
            Db.ArchivedProducts.Add(aproduct.ToBase());

            Db.SaveChanges();
        }

        public void EditProduct(ArchivedProductW aproduct)
        {
            //1 - edit the product properties
            var product = Db.ArchivedProducts.Single(p => p.Id == aproduct.Id);

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
                        (parts.Length > 2 ? "-" + parts[2].Trim() : string.Empty);
                }
            }

            Db.SaveChanges();
        }

        public bool DeleteProduct(Guid id)
        {
            var aproduct = Db.ArchivedProducts.Single(c => c.Id == id);
            Db.ArchivedProducts.Remove(aproduct);

            try
            {
                Db.SaveChanges();
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
            var productTests = Db.ArchivedProductTests.Where(ap => ap.ArchivedProductId == aproductId);
            var result = productTests.ToList().Select(pt => new ArchivedProductTestW(pt));

            return result;
        }

        public IEnumerable<AcredetationLevel> GetPossibleAcredetationLevels()
        {
            return Db.AcredetationLevels;
        }

        public void AddProductTest(ArchivedProductTestW aproductTest)
        {
            aproductTest.Id = Guid.NewGuid();
            Db.ArchivedProductTests.Add(aproductTest.ToBase());

            Db.SaveChanges();
        }

        public void EditProductTest(ArchivedProductTestW aproductTest)
        {
            var productTest = Db.ArchivedProductTests.Single(pt => pt.Id == aproductTest.Id);

            productTest.ArchivedProductId = aproductTest.ArchivedProductId;
            productTest.TestCategory = aproductTest.TestCategory;
            productTest.TestMethods = aproductTest.TestMethods;
            productTest.TestName = aproductTest.TestName;
            productTest.TestTemperature = aproductTest.TestTemperature;
            productTest.TestUnitName = aproductTest.TestUnitName;
            productTest.TestAcredetationLevel = aproductTest.TestAcredetationLevel;
            productTest.TestType = aproductTest.TestType;
            productTest.TestTypeShortName = aproductTest.TestTypeShortName;
            productTest.MethodValue = aproductTest.MethodValue;
            productTest.Remark = aproductTest.Remark;

            Db.SaveChanges();
        }

        public bool DeleteProductTest(Guid id)
        {
            var aproductTest = Db.ArchivedProductTests.Single(c => c.Id == id);
            Db.ArchivedProductTests.Remove(aproductTest);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                return false;
            }

            return true;
        }

        public IEnumerable<ArchivedProtocolResultW> GetProtocolResults(Guid aproductTestId)
        {
            var protocolResults = Db.ArchivedProtocolResults.Where(ap => ap.ArchivedProductTestId == aproductTestId);
            var result = protocolResults.ToList().Select(pr => new ArchivedProtocolResultW(pr));

            return result;
        }

        public void AddProtocolResult(ArchivedProtocolResultW aprotocolResult)
        {
            aprotocolResult.Id = Guid.NewGuid();
            Db.ArchivedProtocolResults.Add(aprotocolResult.ToBase());

            Db.SaveChanges();
        }

        public void EditProtocolResult(ArchivedProtocolResultW aprotocolResult)
        {
            var protocolResult = Db.ArchivedProtocolResults.Single(apr => apr.Id == aprotocolResult.Id);

            protocolResult.ResultNumber = aprotocolResult.ResultNumber;
            protocolResult.Results = aprotocolResult.Results;

            Db.SaveChanges();
        }

        public bool DeleteProtocolResult(Guid id)
        {
            var aprotocolResult = Db.ArchivedProtocolResults.Single(c => c.Id == id);
            Db.ArchivedProtocolResults.Remove(aprotocolResult);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                return false;
            }

            return true;
        }
    }
}