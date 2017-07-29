using System;
using System.Collections.Generic;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.ArchivedWrappers;

namespace RED.Repositories.Abstract
{
    public interface IArchivedDiaryRepository
    {
        void AddProduct(ArchivedProductW aproduct);

        void AddProductTest(ArchivedProductTestW aproductTest);

        void AddProtocolResult(ArchivedProtocolResultW aprotocolResult);

        bool DeleteProduct(Guid id);

        bool DeleteProductTest(Guid id);

        bool DeleteProtocolResult(Guid id);

        void Edit(ArchivedDiaryW adiary);

        void EditProduct(ArchivedProductW aproduct);

        void EditProductTest(ArchivedProductTestW aproductTest);

        void EditProtocolResult(ArchivedProtocolResultW aprotocolResult);

        ArchivedDiary GetArchivedDiary(Guid archivedDiaryId);

        ArchivedDiaryW GetArchivedDiaryW(Guid archivedDiaryId);

        ArchivedProduct GetArchivedProduct(Guid archivedProductId);

        ArchivedProductTest GetArchivedProductTest(Guid archivedProductTestId);

        ArchivedProductTestW GetArchivedProductTestW(Guid archivedProductTestId);

        ArchivedProductW GetArchivedProductW(Guid archivedProductId);

        ArchivedProtocolResult GetArchivedProtocolResult(Guid archivedProtocolResultId);

        ArchivedProtocolResultW GetArchivedProtocolResultW(Guid archivedProtocolResultId);

        IEnumerable<AcredetationLevel> GetPossibleAcredetationLevels();

        IEnumerable<ArchivedProductW> GetProducts(Guid archivedDiaryId);

        IEnumerable<ArchivedProductTestW> GetProductTests(Guid aproductId);

        IEnumerable<ArchivedProtocolResultW> GetProtocolResults(Guid aproductTestId);
    }
}