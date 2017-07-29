using System;
using System.Collections.Generic;
using System.Linq;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Tests;
using RED.Models.Responses;

namespace RED.Repositories.Abstract
{
    public interface ITestsRepository
    {
        void Add(TestW testW);

        void AddCategory(TestCategoryW cat);

        bool Delete(Guid id);

        bool DeleteCategory(Guid id);

        ActionResponse Edit(TestW testW);

        void EditCategory(TestCategoryW cat);

        IEnumerable<AcredetationLevel> GetAcredetationLevels();

        IEnumerable<TestCategoryW> GetCategories();

        TestCategoryW GetCategory(Guid id);

        TestW GetTest(Guid Id);

        IQueryable<Test> GetTests();

        IEnumerable<TestType> GetTestTypes();

        bool IsExisting(TestCategoryW cat);

        bool IsTestExisting(TestW test);
    }
}