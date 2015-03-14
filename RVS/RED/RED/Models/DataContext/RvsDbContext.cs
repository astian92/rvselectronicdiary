using RED.Models.DataContext.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext
{
    public class RvsDbContext : RedDataEntities
    {
        public override int SaveChanges()
        {
            IEnumerable<ActionLog> logs = new List<ActionLog>();
            bool preLoggingWentOk = true;

            //Prepare loggs (before commiting because states and oldValues change after commit!
            try
            {
                string username = HttpContext.Current.User.Identity.Name;
                var user = this.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new NotSupportedException("The action could not be performed for a not authenticated user!");
                }

                this.ChangeTracker.DetectChanges();
                var changes = this.ChangeTracker.Entries().ToList();

                var logger = new Logger(user.Id);
                logger.SetChanges(changes);
                logs = logger.GetLogs();
            }
            catch (Exception exc)
            {
                var exception = new Exception("There was a problem with logging preparation. See inner exception for more details", exc);
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                preLoggingWentOk = false;
            }

            //save actual changes
            int changesCount = base.SaveChanges();

            //use the same context to commit the logs
            try
            {
                if (preLoggingWentOk)
                {
                    this.ActionLogs.AddRange(logs);
                    base.SaveChanges(); //Again this time for the logs
                }
            }
            catch (Exception exc)
            {
                var exception = new Exception("There was a problem with the logging. See inner exception for more details", exc);
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }

            return changesCount;
        }

        public System.Data.Entity.DbSet<RED.Models.ElectronicDiary.Tests.TestW> TestWs { get; set; }

        public System.Data.Entity.DbSet<RED.Models.ElectronicDiary.Tests.TestCategoryW> TestCategoryWs { get; set; }

    }
}