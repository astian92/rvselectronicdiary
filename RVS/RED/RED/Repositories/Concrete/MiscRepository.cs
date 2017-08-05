using System.Linq;
using RED.Repositories.Abstract;
using RED.Models.ElectronicDiary;
using RED.Models.DataContext;
using RED.Models.DataContext.Abstract;

namespace RED.Repositories.Concrete
{
    public class MiscRepository : IMiscRepository
    {
        private readonly RvsDbContext Db;

        public MiscRepository(IRvsContextFactory factory)
        {
            Db = factory.CreateConcrete();
        }

        public AcreditationMetaW GetAcreditationRegistry()
        {
            var acR = Db.AcreditationMetas.First(); //there should be only one :)

            var wrapper = new AcreditationMetaW()
            {
                Id = acR.Id,
                Registered = acR.Registered,
                ValidTo = acR.ValidTo
            };

            return wrapper;
        }

        public void SaveAcreditationRegistry(AcreditationMetaW model)
        {
            var acR = Db.AcreditationMetas.First();

            acR.Registered = model.Registered;
            acR.ValidTo = model.ValidTo;

            Db.SaveChanges();
        }
    }
}