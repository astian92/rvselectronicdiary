using System;
using System.Linq.Expressions;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary.Requests;

namespace RED.Mappings
{
    public static class RequestMappings
    {
        public static readonly Expression<Func<Request, RequestW>> ToRequestW =
            entity => new RequestW()
            {
                Id = entity.Id,
                DiaryId = entity.DiaryId,
                Date = entity.Date,
                AcceptedBy = entity.AcceptedBy,
                IsAccepted = entity.IsAccepted,
                TestingPeriod = entity.TestingPeriod,
                Diary = entity.Diary,
                User = entity.User,
                Protocols = entity.Protocols
            };

        public static RequestW ToRequestWrapper(this Request entity)
        {
            var requestW = new RequestW()
            {
                Id = entity.Id,
                DiaryId = entity.DiaryId,
                Date = entity.Date,
                AcceptedBy = entity.AcceptedBy,
                IsAccepted = entity.IsAccepted,
                TestingPeriod = entity.TestingPeriod,
                Diary = entity.Diary,
                User = entity.User,
                Protocols = entity.Protocols
            };

            return requestW;
        }
    }
}