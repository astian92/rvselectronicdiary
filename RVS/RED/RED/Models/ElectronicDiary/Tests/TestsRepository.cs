using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using RED.Models.RepositoryBases;
using RED.Models.DataContext;
using RED.Models.Responses;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestsRepository : RepositoryBase
    {
        public IEnumerable<AcredetationLevel> GetAcredetationLevels()
        {
            return Db.AcredetationLevels.ToList();
        }

        public TestCategoryW GetCategory(Guid id)
        {
            var cat = Db.TestCategories.Single(c => c.Id == id);
            return new TestCategoryW(cat);
        }

        public IEnumerable<TestCategoryW> GetCategories()
        {
            var categories = Db.TestCategories.OrderBy(c => c.Name)
                                .ToList()
                                .Select(c => new TestCategoryW(c));
            return categories;
        }

        public void AddCategory(TestCategoryW cat)
        {
            var category = cat.ToBase();
            category.Id = Guid.NewGuid();
            Db.TestCategories.Add(category);

            Db.SaveChanges();
        }

        public void EditCategory(TestCategoryW cat)
        {
            var category = Db.TestCategories.Single(c => c.Id == cat.Id);
            category.Name = cat.Name;

            Db.SaveChanges();
        }

        public bool DeleteCategory(Guid id)
        {
            var category = Db.TestCategories.Single(c => c.Id == id);
            Db.TestCategories.Remove(category);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsExisting(TestCategoryW cat)
        {
            var founded = Db.TestCategories.FirstOrDefault(x => x.Name.ToLower() == cat.Name.ToLower());
            if (founded != null)
            {
                return true;
            }

            return false;
        }

        public TestW GetTest(Guid Id)
        {
            var test = Db.Tests.Single(t => t.Id == Id);
            return new TestW(test);
        }

        public IEnumerable<TestW> GetTests()
        {
            var tests = Db.Tests.Include(x => x.TestCategory)
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

            if (test.TestMethods != null)
            {
                foreach (var method in test.TestMethods)
                {
                    method.Id = Guid.NewGuid();
                }
            }

            Db.Tests.Add(test);

            Db.SaveChanges();
        }

        public ActionResponse Edit(TestW testW)
        {
            var response = new ActionResponse();

            var test = Db.Tests.Single(c => c.Id == testW.Id);
            test.Name = testW.Name;
            test.TestCategoryId = testW.TestCategoryId;
            test.AcredetationLevelId = testW.AcredetationLevelId;
            test.Temperature = testW.Temperature;
            test.UnitName = testW.UnitName;
            test.TypeId = testW.TypeId;
            test.MethodValue = testW.MethodValue;

            try
            {
                var toDelete = new List<TestMethod>();

                //1 add all to be deleted that not existing in the new list
                foreach (var item in test.TestMethods)
                {
                    if (!testW.TestMethods.Any(m => m.Method == item.Method))
                    {
                        toDelete.Add(item);
                    }
                }

                //1.5 Remove them
                foreach (var item in toDelete)
                {
                    Db.TestMethods.Remove(item);
                }

                //2 now insert all that are new for the list
                foreach (var item in testW.TestMethods)
                {
                    if (!test.TestMethods.Any(m => m.Method == item.Method))
                    {
                        var method = new TestMethod();
                        method.Id = Guid.NewGuid();
                        method.Method = item.Method;

                        test.TestMethods.Add(method);
                    }
                }

                response.IsSuccess = true;
                Db.SaveChanges();
            }
            catch (Exception exc)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
                response.IsSuccess = false;
                response.Error = ErrorFactory.MethodInUseError;
            }

            return response;
        }

        public bool Delete(Guid id)
        {
            var test = Db.Tests.Single(c => c.Id == id);
            Db.Tests.Remove(test);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsTestExisting(TestW test)
        {
            var founded = Db.Tests.FirstOrDefault(x => x.Name.ToLower() == test.Name.ToLower());
            if (founded != null)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<TestType> GetTestTypes()
        {
            return Db.TestTypes.ToList();
        }
    }
}