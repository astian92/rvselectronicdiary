using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext
{
    public static class DbContextFactory
    {
        private static REDEntities DataContext { get; set; }

        public static REDEntities GetDbContext() 
        {
            return new REDEntities();
        }

        public static REDEntities GetPersistentContext()
        {
            if (DataContext != null)
            {
                return DataContext;
            }
            else
            {
                DataContext = new REDEntities();
                return DataContext;
            }
        }

        public static void Log()
        {
            
        }
    }
}