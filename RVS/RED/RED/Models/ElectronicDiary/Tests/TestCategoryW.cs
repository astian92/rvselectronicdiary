using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestCategoryW
    {
        public TestCategoryW()
        {
        }

        public TestCategoryW(TestCategory category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public TestCategory ToBase()
        {
            var category = new TestCategory();

            category.Id = this.Id;
            category.Name = this.Name;

            return category;
        }
    }
}