using System.Data.Entity;
using RED.Models.DataContext.Abstract;

namespace RED.Models.DataContext.Concrete
{
    public class RvsContextFactory : IRvsContextFactory
    {
        public DbContext Create()
        {
            return new RvsDbContext();
        }

        public RvsDbContext CreateConcrete()
        {
            return (RvsDbContext)this.Create();
        }
    }
}