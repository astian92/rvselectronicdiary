namespace RED.Models.DataContext.Abstract
{
    public interface IRvsContextFactory : IDbFactory
    {
        RvsDbContext CreateConcrete();
    }
}