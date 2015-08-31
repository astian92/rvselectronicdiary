using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProtocolResultW
    {
        public Guid Id { get; set; }
        public Guid ArchivedDiaryId { get; set; }
        public Guid ArchivedProductTestId { get; set; }

        [Required(ErrorMessage = "Полето \"№ на образеца\" е задължително!")]
        [Display(Name = "№ на образеца")]
        public string ResultNumber { get; set; }

        [Required(ErrorMessage = "Полето \"Резултати от изследването, неопределеност\" е задължително!")]
        [Display(Name = "Резултати от изследването, неопределеност")]
        public string Results { get; set; }

        [Required(ErrorMessage = "Полето \"Стойност на показателя\" е задължително!")]
        [Display(Name = "Стойност на показателя")]
        public string MethodValue { get; set; }

        public ArchivedProtocolResultW()
        {

        }

        public ArchivedProtocolResultW(ArchivedProtocolResult aresult)
        {
            this.Id = aresult.Id;
            this.ArchivedDiaryId = aresult.ArchivedDiaryId;
            this.ArchivedProductTestId = aresult.ArchivedProductTestId;
            this.Results = aresult.Results;
            this.ResultNumber = aresult.ResultNumber;
            this.MethodValue = aresult.MethodValue;
        }

        public ArchivedProtocolResult ToBase()
        {
            var aresult = new ArchivedProtocolResult();

            aresult.Id = this.Id;
            aresult.ArchivedDiaryId = this.ArchivedDiaryId;
            aresult.ArchivedProductTestId = this.ArchivedProductTestId;
            aresult.Results = this.Results;
            aresult.ResultNumber = this.ResultNumber;
            aresult.MethodValue = this.MethodValue;

            return aresult;
        }
    }
}