using System;
using System.Linq;
using System.Linq.Expressions;
using RED.Models.DataContext;
using RED.Models.ElectronicDiary;

namespace RED.Mappings
{
    public static class DiaryMappings
    {
        public static readonly Expression<Func<Diary, DiaryW>> ToDiaryW =
            entity => new DiaryW()
            {
                Id = entity.Id,
                Number = entity.Number,
                LetterNumber = entity.LetterNumber,
                LetterDate = entity.LetterDate,
                AcceptanceDateAndTime = entity.AcceptanceDateAndTime,
                Contractor = entity.Contractor,
                ClientId = entity.ClientId,
                Comment = entity.Comment,
                Client = entity.Client,
                Request = entity.Requests.FirstOrDefault(),
                Products = entity.Products
            };

        public static DiaryW ToDiaryWrapper(this Diary entity)
        {
            var diaryW = new DiaryW()
            {
                Id = entity.Id,
                Number = entity.Number,
                LetterNumber = entity.LetterNumber,
                LetterDate = entity.LetterDate,
                AcceptanceDateAndTime = entity.AcceptanceDateAndTime,
                Contractor = entity.Contractor,
                ClientId = entity.ClientId,
                Comment = entity.Comment,
                Client = entity.Client,
                Request = entity.Requests.FirstOrDefault(),
                Products = entity.Products
            };

            return diaryW;
        }
    }
}