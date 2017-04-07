using System;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;

namespace RED.Repositories.Abstract
{
    public interface IFilesRepository
    {
        string FileTreePath { get; set; }

        void GenerateProtocolReport(Protocol protocol, Request request);

        string GenerateRequestListReport(Guid diaryId, DateTime date, int testingPeriod);

        byte[] GetArchivedProtocolReport(Guid archivedDiaryId, string category, out string fileName);

        byte[] GetProtocolReport(Guid protocolId, string category, out string fileName);

        byte[] GetRequestListReport(Guid diaryId, string category, out string fileName);

        void RegenerateProtocolReport(ArchivedDiaryW adiary);
    }
}