using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestsRepository : RepositoryBase
    {
        public IEnumerable<AcredetationLevel> GetAcredetationLevels()
        {
            return db.AcredetationLevels.ToList();
        }

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
        
        public bool DeleteCategory(Guid id)
        {
            var category = db.TestCategories.Single(c => c.Id == id);
            db.TestCategories.Remove(category);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsExisting(TestCategoryW cat)
        {
            var founded = db.TestCategories.FirstOrDefault(x => x.Name.ToLower() == cat.Name.ToLower());
            if (founded != null)
                return true;

            return false;
        }

        public TestW GetTest(Guid Id)
        {
            var test = db.Tests.Single(t => t.Id == Id);
            return new TestW(test);
        }

        public IEnumerable<TestW> GetTests()
        {
            var tests = db.Tests.Include(x => x.TestCategory)
                            .Include(x => x.AcredetationLevel)
                            .OrderBy(t => t.TestCategory.Name)
                            .ToList()
                            .Select(t => new TestW(t));
            return tests;
        }

        public void Add(TestW testW)
        {
            var test = testW.ToBase();
            test.Id = Guid.NewGuid();
            db.Tests.Add(test);

            db.SaveChanges();
        }

        public void Edit(TestW testW)
        {
            var test = db.Tests.Single(c => c.Id == testW.Id);
            test.Name = testW.Name;
            test.TestCategoryId = testW.TestCategoryId;
            test.TestMethods = testW.TestMethods;
            test.AcredetationLevelId = testW.AcredetationLevelId;
            test.Temperature = testW.Temperature;
            test.UnitName = testW.UnitName;

            db.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var test = db.Tests.Single(c => c.Id == id);
            db.Tests.Remove(test);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsTestExisting(TestW test)
        {
            var founded = db.Tests.FirstOrDefault(x => x.Name.ToLower() == test.Name.ToLower());
            if (founded != null)
                return true;

            return false;
        }
    }
}