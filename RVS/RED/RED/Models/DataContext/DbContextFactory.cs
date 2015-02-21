using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext
{
    public static class DbContextFactory
    {
        private static RedDataEntities DataContext { get; set; }

        public static RedDataEntities GetDbContext() 
        {
            return new RedDataEntities();
        }

        public static RedDataEntities GetPersistentContext()
        {
            if (DataContext != null)
            {
                return DataContext;
            }
            else
            {
                DataContext = new RedDataEntities();
                return DataContext;
            }
        }

        public static void Log()
        {
            
        }
    }
}