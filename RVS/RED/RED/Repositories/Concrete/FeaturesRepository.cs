using System;
using System.Linq;
using System.Data.Entity;
using RED.Repositories.Abstract;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;

namespace RED.Repositories.Concrete
{
    public class FeaturesRepository : IFeaturesRepository
    {
        private readonly RvsDbContext Db;

        public FeaturesRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public Feature Get(Guid id)
        {
            return Db.Features.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Feature> GetAll()
        {
            return Db.Features.AsQueryable();
        }

        public void Create(Feature feature)
        {
            feature.Id = Guid.NewGuid();
            Db.Features.Add(feature);
            Db.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var feature = Get(id);
            Db.Features.Remove(feature);

            try
            {
                Db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Edit(Feature feature)
        {
            Db.Entry(feature).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}