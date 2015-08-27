using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.ArchivedWrappers
{
    public class ArchivedProtocolResultW
    {
        public Guid Id { get; set; }
        public Guid ArchivedDiaryId { get; set; }
        public Guid ArchivedProductTestId { get; set; }
        public string ResultNumber { get; set; }
        public string Results { get; set; }
        public string MethodValue { get; set; }

        public ArchivedProtocolResultW()
        {

        }

        public ArchivedProtocolResultW(ArchivedProtocolResult aresult)
        {
            this.Id = aresult.Id;
            this.ArchivedDiaryId = aresult.ArchivedDiaryId;
            this.ArchivedProductTestId = aresult.ArchivedProductTestId;
            this.ResultNumber = aresult.ResultNumber;
            this.MethodValue = aresult.MethodValue;
        }

        public ArchivedProtocolResult ToBase()
        {
            var aresult = new ArchivedProtocolResult();

            aresult.Id = this.Id;
            aresult.ArchivedDiaryId = this.ArchivedDiaryId;
            aresult.ArchivedProductTestId = this.ArchivedProductTestId;
            aresult.ResultNumber = this.ResultNumber;
            aresult.MethodValue = this.MethodValue;

            return aresult;
        }

        //public virtual ArchivedProductTest ArchivedProductTest { get; set; }
        //public virtual ArchivedDiary ArchivedDiary { get; set; }
    }
}