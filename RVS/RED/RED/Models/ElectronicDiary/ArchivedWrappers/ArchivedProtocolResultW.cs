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
            Id = aresult.Id;
            ArchivedDiaryId = aresult.ArchivedDiaryId;
            ArchivedProductTestId = aresult.ArchivedProductTestId;
            Results = aresult.Results;
            ResultNumber = aresult.ResultNumber;
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

            aresult.Id = Id;
            aresult.ArchivedDiaryId = ArchivedDiaryId;
            aresult.ArchivedProductTestId = ArchivedProductTestId;
            aresult.Results = Results;
            aresult.ResultNumber = ResultNumber;

            return aresult;
        }
    }
}