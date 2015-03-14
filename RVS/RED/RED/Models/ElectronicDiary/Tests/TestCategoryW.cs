using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Tests
{
    public class TestCategoryW
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [StringLength(100, ErrorMessage = "Името трябва да бъде поне 2 символа.", MinimumLength = 2)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public TestCategoryW()
        {

        }
    
        public TestCategoryW(TestCategory category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

        public TestCategory ToBase()
        {
            var category = new TestCategory();

            category.Id = this.Id;
            category.Name = this.Name;

            return category;
        }

    }
}