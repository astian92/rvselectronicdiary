using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext
{
    public static class DbContextFactory
    {
        private static RvsDbContext DataContext { get; set; }

        public static RvsDbContext GetDbContext() 
        {
            return new RvsDbContext();
        }

        public static RvsDbContext GetPersistentContext()
        {
            if (DataContext != null)
            {
                return DataContext;
            }
            else
            {
                DataContext = new RvsDbContext();
                return DataContext;
            }
        }

        public static void Log()
        {
            
        }
    }
}