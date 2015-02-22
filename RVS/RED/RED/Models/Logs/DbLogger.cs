using RED.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RED.Models.Logs
{
    public class DbLogger
    {
        public void Log()
        {
            var context = DbContextFactory.GetDbContext();

            var user = context.Users.First();
            user.MiddleName = "MIDDELE NAME";

            var role = context.Roles.First();
            role.DisplayName = "Admin4e";

            var changedEntitiesEntries = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added
                                              || e.State == EntityState.Modified
                                              || e.State == EntityState.Deleted);

            foreach (var entry in changedEntitiesEntries)
            {
                foreach (string propName in entry.CurrentValues.PropertyNames)
                {
                    var property = entry.Property(propName);
                    var currentVal = (property.CurrentValue ?? string.Empty).ToString();
                    var originalVal = (property.OriginalValue ?? string.Empty).ToString();

                    if (currentVal != originalVal)
                    {
                        var entityType = entry.Entity.GetType();
                        string tableName = entityType.BaseType.Name;
                        string fullType = entityType.BaseType.FullName;

                        var table = context.Set(Type.GetType(fullType));
                    }
                }
            }

            int a = 5;
        }
    }
}