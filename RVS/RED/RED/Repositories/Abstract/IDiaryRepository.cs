using System;
using System.Collections.Generic;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;
using RED.Models.ElectronicDiary.Clients;
using RED.Models.ElectronicDiary.Tests;
using RED.Models.Responses;

namespace RED.Repositories.Abstract
{
    public interface IDiaryRepository
    {
        bool AddComment(Guid diaryId, string comment);

        void AddLetter(DiaryW diary);

        ActionResponse ArchiveDiary(Guid diaryId);

        bool Delete(Guid diaryId);

        bool DeleteRequest(Guid diaryId);

        void Edit(DiaryW diaryW);

        string GenerateRequest(Guid diaryId, int testingPeriod);

        ArchivedDiaryW GetArchivedDiary(Guid adiaryId);

        IEnumerable<ArchivedDiaryW> GetArchivedDiaryEntries(int page = 1, int pageSize = 10, int number = -1, int diaryNumber = -1, string client = "Всички", DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<ClientW> GetClients();

        DiaryW GetDiary(Guid diaryId);

        IEnumerable<DiaryW> GetDiaryEntries(int page = 1, int pageSize = 10, string number = "-1", int diaryNumber = -1, Guid client = default(Guid), DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));

        IEnumerable<Client> GetSelectListClients(bool allValue = true);

        IEnumerable<TestW> GetSelectListTests();

        IEnumerable<TestMethodW> GetTestMethods(Guid testId);

        IEnumerable<TestW> GetTests();

        ActionResponse RegenerateArchivedProtocol(ArchivedDiaryW adiary);
    }
}