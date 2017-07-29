using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProductW
    {
        public ArchivedProductW()
        {
        }

        public ArchivedProductW(ArchivedProduct aproduct)
        {
            this.Id = aproduct.Id;
            this.ArchivedDiaryId = aproduct.ArchivedDiaryId;
            this.Name = aproduct.Name;
            this.Quantity = aproduct.Quantity;
            this.Number = aproduct.Number;

            this.ArchivedTests = aproduct.ArchivedProductTests.Select(a => new ArchivedProductTestW(a));
        }

        public Guid Id { get; set; }

        public Guid ArchivedDiaryId { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето \"Количество\" е задължително!")]
        [Display(Name = "Количество")]
        public string Quantity { get; set; }

        [Required(ErrorMessage = "Полето \"Номер\" е задължително!")]
        [Display(Name = "Номер")]
        public int Number { get; set; }

        public int ArchivedProductsCount
        {
            get
            {
                if (this.ArchivedTests != null)
                {
                    return this.ArchivedTests.Count();
                }

                return 0;
            }
        }

        public IEnumerable<ArchivedProductTestW> ArchivedTests { get; set; }

        public ArchivedProduct ToBase()
        {
            var archivedProduct = new ArchivedProduct();

            archivedProduct.Id = this.Id;
            archivedProduct.ArchivedDiaryId = this.ArchivedDiaryId;
            archivedProduct.Name = this.Name;
            archivedProduct.Quantity = this.Quantity;
            archivedProduct.Number = this.Number;

            if (this.ArchivedTests != null)
            {
                archivedProduct.ArchivedProductTests = this.ArchivedTests.Select(a => a.ToBase()).ToList();
            }
            else
            {
                archivedProduct.ArchivedProductTests = new List<ArchivedProductTest>();
            }

            return archivedProduct;
        }
    }
}