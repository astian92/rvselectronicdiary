using System;
using System.Web.Mvc;
using RED.Models.ControllerBases;
using RED.Repositories.Abstract;

namespace RED.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IFilesRepository _rep;

        public FilesController(IFilesRepository filesRepo)
        {
            _rep = filesRepo;
        }

        public FileResult GetRequestFile(Guid diaryId, string category)
        {
            string fileName = string.Empty;
            var reportData = _rep.GetRequestListReport(diaryId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public FileResult GetProtocolFile(Guid protocolId, string category)
        {
            string fileName = string.Empty;
            var reportData = _rep.GetProtocolReport(protocolId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        public FileResult GetArchivedProtocolFile(Guid archivedDiaryId, string category)
        {
            string fileName = string.Empty;
            var reportData = _rep.GetArchivedProtocolReport(archivedDiaryId, category, out fileName);
            return File(reportData, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}