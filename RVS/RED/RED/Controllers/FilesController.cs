using RED.Models.ControllerBases;
using RED.Models.FileModels;
using RED.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RED.Controllers
{
    public class FilesController : ControllerBase<FilesRepository>
    {
        public FileResult GetRequestFile(Guid diaryId)
        {
            string fileName = "";
            var reportData = Rep.GetRequestListReport(diaryId, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public FileResult GetProtocolFile(Guid protocolId, string category)
        {
            string fileName = "";
            var reportData = Rep.GetProtocolReport(protocolId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        public FileResult GetArchivedProtocolFile(Guid archivedDiaryId, string category)
        {
            string fileName = "";
            var reportData = Rep.GetArchivedProtocolReport(archivedDiaryId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

       

	}
}