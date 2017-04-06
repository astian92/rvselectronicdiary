using System.Data.Entity;

namespace RED.Models.DataContext.Abstract
{
    public interface IDbFactory
    {
        DbContext Create();
    }
}