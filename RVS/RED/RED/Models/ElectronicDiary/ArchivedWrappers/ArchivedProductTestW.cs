using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProductTestW
    {
        public ArchivedProductTestW()
        {
        }

        public ArchivedProductTestW(ArchivedProductTest atest)
        {
            this.Id = atest.Id;
            this.ArchivedProductId = atest.ArchivedProductId;
            this.TestName = atest.TestName;
            this.TestUnitName = atest.TestUnitName;
            this.TestMethods = atest.TestMethods;
            this.TestAcredetationLevel = atest.TestAcredetationLevel;
            this.TestTemperature = atest.TestTemperature;
            this.TestCategory = atest.TestCategory;
            this.TestType = atest.TestType;
            this.TestTypeShortName = atest.TestTypeShortName;
            this.MethodValue = atest.MethodValue;
            this.Remark = atest.Remark;

            this.Results = atest.ArchivedProtocolResults.Select(ar => new ArchivedProtocolResultW(ar));
        }

        public Guid Id { get; set; }

        public Guid ArchivedProductId { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string TestName { get; set; }

        [Display(Name = "Единица на величината")]
        public string TestUnitName { get; set; }

        [Required(ErrorMessage = "Полето \"Методи\" е задължително!")]
        [Display(Name = "Методи")]
        public string TestMethods { get; set; }

        [Required(ErrorMessage = "Полето \"Ниво Акредитация\" е задължително!")]
        [Display(Name = "Ниво Акредитация")]
        public string TestAcredetationLevel { get; set; }

        [Display(Name = "Температура")]
        public string TestTemperature { get; set; }

        [Required(ErrorMessage = "Полето \"Категория\" е задължително!")]
        [Display(Name = "Категория")]
        public string TestCategory { get; set; }

        [Required(ErrorMessage = "Полето \"Вид\" е задължително!")]
        [Display(Name = "Вид")]
        public string TestType { get; set; }

        [Required(ErrorMessage = "Полето \"Вид (съкратено)\" е задължително!")]
        [Display(Name = "Вид (съкратено)")]
        public string TestTypeShortName { get; set; }

        [Required(ErrorMessage = "Полето \"Стойност на показателя\" е задължително!")]
        [Display(Name = "Стойност на показателя")]
        public string MethodValue { get; set; }

        [Display(Name = "Забележка")]
        public string Remark { get; set; }

        public IEnumerable<ArchivedProtocolResultW> Results { get; set; }

        public ArchivedProductTest ToBase()
        {
            var atest = new ArchivedProductTest();

            atest.Id = this.Id;
            atest.ArchivedProductId = this.ArchivedProductId;
            atest.TestName = this.TestName;
            atest.TestUnitName = this.TestUnitName;
            atest.TestMethods = this.TestMethods;
            atest.TestAcredetationLevel = this.TestAcredetationLevel;
            atest.TestTemperature = this.TestTemperature;
            atest.TestCategory = this.TestCategory;
            atest.TestType = this.TestType;
            atest.TestTypeShortName = this.TestTypeShortName;
            atest.MethodValue = this.MethodValue;
            atest.Remark = this.Remark;

            if (this.Results != null)
            {
                atest.ArchivedProtocolResults = this.Results.Select(ar => ar.ToBase()).ToList();
            }
            else
            {
                atest.ArchivedProtocolResults = new List<ArchivedProtocolResult>();
            }

            return atest;
        }
    }
}