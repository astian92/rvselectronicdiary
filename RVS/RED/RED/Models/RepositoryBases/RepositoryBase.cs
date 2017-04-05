using RED.Models.DataContext;

namespace RED.Models.RepositoryBases
{
    public abstract class RepositoryBase
    {
        public RepositoryBase()
        {
            Db = DbContextFactory.GetDbContext();
        }

        protected RvsDbContext Db { get; set; }
    }
}