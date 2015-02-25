using RED.Models.DataContext.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext
{
    public class RvsDbContext : RedDataEntities
    {
        private IEnumerable<ActionLog> GetAdded(User user)
        {
            var Added = new List<ActionLog>();
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ActionLog actionLog = new ActionLog();

                actionLog.User = user;
                actionLog.On = DateTime.Now.ToUniversalTime();
                var entityType = entry.Entity.GetType();
                actionLog.TableName = entityType.Name;
                actionLog.FullTableName = entityType.FullName;
                actionLog.ActionTypeId = (int)ActionTypesEnum.Add;
                actionLog.TableNameBg = TableNameToBg.Get(actionLog.TableName);

                foreach (string propName in entry.CurrentValues.PropertyNames)
                {
                    ActionLogProperty logProp = new ActionLogProperty();
                    var property = entry.Property(propName);

                    logProp.PropertyName = propName;
                    logProp.NewValue = (property.CurrentValue ?? string.Empty).ToString();

                    actionLog.ActionLogProperties.Add(logProp);
                }

                Added.Add(actionLog);
            }

            return Added;
        }

        private IEnumerable<ActionLog> GetEdited(User user)
        {
            var Edited = new List<ActionLog>();
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            
            foreach (var entry in entries)
            {
                ActionLog actionLog = new ActionLog();

                actionLog.User = user;
                actionLog.On = DateTime.Now.ToUniversalTime();
                var entityType = entry.Entity.GetType();
                //actionLog.TableName = entityType.BaseType.Name;
                //actionLog.FullTableName = entityType.BaseType.FullName;
                actionLog.TableName = entityType.Name;
                actionLog.FullTableName = entityType.FullName;
                actionLog.ActionTypeId = (int)ActionTypesEnum.Edit;
                actionLog.TableNameBg = TableNameToBg.Get(actionLog.TableName);

                foreach (string propName in entry.CurrentValues.PropertyNames)
                {
                    var property = entry.Property(propName);
                    var currentVal = (property.CurrentValue ?? string.Empty).ToString();
                    var originalVal = (property.OriginalValue ?? string.Empty).ToString();

                    if (currentVal != originalVal)
                    {
                        ActionLogProperty logProp = new ActionLogProperty();

                        logProp.PropertyName = propName;
                        logProp.NewValue = currentVal;
                        logProp.OldValue = originalVal;

                        actionLog.ActionLogProperties.Add(logProp);
                    }
                }
                
                Edited.Add(actionLog);
            }

            return Edited;
        }

        private IEnumerable<ActionLog> GetDeleted(User user)
        {
            var Deleted = new List<ActionLog>();
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                ActionLog actionLog = new ActionLog();

                actionLog.User = user;
                actionLog.On = DateTime.Now.ToUniversalTime();
                var entityType = entry.Entity.GetType();
                actionLog.TableName = entityType.BaseType.Name;
                actionLog.FullTableName = entityType.BaseType.FullName;
                actionLog.ActionTypeId = (int)ActionTypesEnum.Delete;
                actionLog.TableNameBg = TableNameToBg.Get(actionLog.TableName);

                foreach (string propName in entry.OriginalValues.PropertyNames)
                {
                    ActionLogProperty logProp = new ActionLogProperty();
                    var property = entry.Property(propName);

                    logProp.PropertyName = propName;
                    logProp.OldValue = (property.OriginalValue ?? string.Empty).ToString();

                    actionLog.ActionLogProperties.Add(logProp);
                }

                Deleted.Add(actionLog);
            }

            return Deleted;
        }

        public override int SaveChanges()
        {
            //try
            //{
            //    string username = HttpContext.Current.User.Identity.Name;
            //    var user = this.Users.FirstOrDefault(u => u.Username == username);

            //    if (user == null)
            //    {
            //        throw new NotSupportedException("The action could not be performed for a not authenticated user!");
            //    }

            //    this.ChangeTracker.DetectChanges();

            //    var added = GetAdded(user);
            //    var edited = GetEdited(user);
            //    var deleted = GetDeleted(user);

            //    if (added.Count() > 0)
            //    {
            //        this.ActionLogs.AddRange(added);
            //    }

            //    if (edited.Count() > 0)
            //    {
            //        this.ActionLogs.AddRange(edited);
            //    }

            //    if (deleted.Count() > 0)
            //    {
            //        this.ActionLogs.AddRange(deleted);
            //    }
            //}
            //catch (Exception exc)
            //{
            //    var exception = new Exception("There was a problem with the logging. See inner exception for more details", exc);
            //    Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            //}

            return base.SaveChanges();
        }
    }
}