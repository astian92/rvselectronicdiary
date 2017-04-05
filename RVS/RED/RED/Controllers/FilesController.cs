using System;
using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Models.FileModels;

namespace RED.Controllers
{
    public class FilesController : ControllerBase<FilesRepository>
    {
        public FileResult GetRequestFile(Guid diaryId, string category)
        {
            string fileName = string.Empty;
            var reportData = Rep.GetRequestListReport(diaryId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public FileResult GetProtocolFile(Guid protocolId, string category)
        {
            string fileName = string.Empty;
            var reportData = Rep.GetProtocolReport(protocolId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        public FileResult GetArchivedProtocolFile(Guid archivedDiaryId, string category)
        {
            string fileName = string.Empty;
            var reportData = Rep.GetArchivedProtocolReport(archivedDiaryId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}