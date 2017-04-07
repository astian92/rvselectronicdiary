using System;
using System.ComponentModel.DataAnnotations;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProtocolResultW
    {
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
        }

        public Guid Id { get; set; }

        public Guid ArchivedDiaryId { get; set; }

        public Guid ArchivedProductTestId { get; set; }

        [Required(ErrorMessage = "Полето \"№ на образеца\" е задължително!")]
        [Display(Name = "№ на образеца")]
        public string ResultNumber { get; set; }

        [Required(ErrorMessage = "Полето \"Резултати от изследването, неопределеност\" е задължително!")]
        [Display(Name = "Резултати от изследването, неопределеност")]
        public string Results { get; set; }

        public ArchivedProtocolResult ToBase()
        {
            var aresult = new ArchivedProtocolResult();

            aresult.Id = this.Id;
            aresult.ArchivedDiaryId = this.ArchivedDiaryId;
            aresult.ArchivedProductTestId = this.ArchivedProductTestId;
            aresult.Results = this.Results;
            aresult.ResultNumber = this.ResultNumber;

            return aresult;
        }
    }
}