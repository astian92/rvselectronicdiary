using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestsRepository : RepositoryBase
    {
        public TestCategoryW GetCategory(Guid id)
        {
            var cat = db.TestCategories.Single(c => c.Id == id);
            return new TestCategoryW(cat);
        }

        public IEnumerable<TestCategoryW> GetCategories()
        {
            var categories = db.TestCategories.OrderBy(c => c.Name)
                                .ToList()
                                .Select(c => new TestCategoryW(c));
            return categories;
        }

        public void AddCategory(TestCategoryW cat)
        {
            var category = cat.ToBase();
            category.Id = Guid.NewGuid();
            db.TestCategories.Add(category);

            db.SaveChanges();
        }

        public void EditCategory(TestCategoryW cat)
        {
            var category = db.TestCategories.Single(c => c.Id == cat.Id);
            category.Name = cat.Name;

            db.SaveChanges();
        }
        
        public void DeleteCategory(Guid id)
        {
            var category = db.TestCategories.Single(c => c.Id == id);
            db.TestCategories.Remove(category);

            db.SaveChanges();
        }

        public TestW GetTest(Guid Id)
        {
            var test = db.Tests.Single(t => t.Id == Id);
            return new TestW(test);
        }

        public IEnumerable<TestW> GetTests()
        {
            var tests = db.Tests.OrderBy(t => t.TestCategory.Name)
                            .ToList()
                            .Select(t => new TestW(t));
            return tests;
        }
    }
}