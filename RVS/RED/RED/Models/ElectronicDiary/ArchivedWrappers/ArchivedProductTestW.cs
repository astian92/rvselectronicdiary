﻿using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProductTestW
    {
        public Guid Id { get; set; }
        public Guid ArchivedProductId { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string TestName { get; set; }

        //[Required(ErrorMessage = "Полето \"Единица на величината\" е задължително!")]
        [Display(Name = "Единица на величината")]
        public string TestUnitName { get; set; }

        [Required(ErrorMessage = "Полето \"Методи\" е задължително!")]
        [Display(Name = "Методи")]
        public string TestMethods { get; set; }

        [Required(ErrorMessage = "Полето \"Ниво Акредитация\" е задължително!")]
        [Display(Name = "Ниво Акредитация")]
        public string TestAcredetationLevel { get; set; }

        //[Required(ErrorMessage = "Полето \"Температура\" е задължително!")]
        [Display(Name = "Температура")]
        public string TestTemperature { get; set; }

        [Required(ErrorMessage = "Полето \"Категория\" е задължително!")]
        [Display(Name = "Категория")]
        public string TestCategory { get; set; }

        //[Required(ErrorMessage = "Полето \"Единици\" е задължително!")]
        //[Display(Name = "Единици")]
        //public int Units { get; set; }

        public IEnumerable<ArchivedProtocolResultW> Results { get; set; }

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
            //this.Units = atest.Units;

            this.Results = atest.ArchivedProtocolResults.Select(ar => new ArchivedProtocolResultW(ar));
        }

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
            //atest.Units = this.Units;

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