using System;
using System.Linq;
using RED.Models.DataContext;

namespace RED.Repositories.Abstract
{
    public interface IFeaturesRepository
    {
        IQueryable<Feature> GetAll();

        Feature Get(Guid id);

        void Create(Feature feature);

        void Edit(Feature feature);

        bool Delete(Guid id);
    }
}