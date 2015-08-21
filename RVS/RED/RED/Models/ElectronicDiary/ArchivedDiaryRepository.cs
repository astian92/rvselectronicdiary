using RED.Models.DataContext;
using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary
{
    public class ArchivedDiaryRepository : RepositoryBase
    {
        public ArchivedDiary GetArchivedDiary(Guid archivedDiaryId)
        {
            var archivedDiary = db.ArchivedDiaries.Single(ad => ad.Id == archivedDiaryId);
            return archivedDiary;
        }

        public ArchivedDiaryW GetArchivedDiaryW(Guid archivedDiaryId)
        {
            var archivedDiary = GetArchivedDiary(archivedDiaryId);
            return new ArchivedDiaryW(archivedDiary);
        }

        public void Edit(ArchivedDiaryW adiary)
        {
            var archivedDiary = db.ArchivedDiaries.Single(ad => ad.Id == adiary.Id);

            archivedDiary.Number = adiary.Number;
            archivedDiary.LetterNumber = adiary.LetterNumber;
            archivedDiary.LetterDate = adiary.LetterDate.ToUniversalTime();
            archivedDiary.AcceptanceDateAndTime = adiary.AcceptanceDateAndTime.ToUniversalTime();
            archivedDiary.Contractor = adiary.Contractor;
            archivedDiary.Client = adiary.Client;
            archivedDiary.Comment = adiary.Comment;
            archivedDiary.RequestDate = adiary.RequestDate.ToUniversalTime();
            archivedDiary.RequestAcceptedBy = adiary.RequestAcceptedBy;
            archivedDiary.ProtocolIssuedDate = adiary.ProtocolIssuedDate.ToUniversalTime();
            archivedDiary.ProtocolTester = adiary.ProtocolTester;
            archivedDiary.ProtocolLabLeader = adiary.ProtocolLabLeader;
            archivedDiary.Remark = adiary.Remark;
            archivedDiary.RequestTestingPeriod = adiary.RequestTestingPeriod;

            db.SaveChanges();
        }
    }
}