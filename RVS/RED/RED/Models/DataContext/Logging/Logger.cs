using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext.Logging
{
    public class Logger
    {
        private List<DbEntityEntry> AddedEntites { get; set; }

        private List<DbEntityEntry> EditedEntities { get; set; }

        private List<DbEntityEntry> DeletedEntities { get; set; }

        private Guid UserId { get; set; }

        private List<LogW> Logs { get; set; }

        public Logger(Guid userId)
        {
            this.UserId = userId;
            this.Logs = new List<LogW>();
        }

        private void CreateLogs(List<DbEntityEntry> entries, int actionTypeId)
        {
            foreach (var entry in entries)
            {
                LogW log = new LogW();

                log.UserId = this.UserId;
                log.On = DateTime.Now.ToUniversalTime();
                log.ActionTypeId = actionTypeId;

                var entityType = entry.Entity.GetType();

                string name = entityType.BaseType.Name;
                string fullName = entityType.BaseType.FullName;
                if (actionTypeId == 1)
                {
                    name = entityType.Name;
                    fullName = entityType.FullName;
                }

                log.TableName = name;
                log.FullTableName = fullName;
                
                log.TableNameBg = TableNameToBg.Get(log.TableName);

                IEnumerable<string> propertyNames = null;
                if (actionTypeId == 1 || actionTypeId == 2)
                {
                    propertyNames = entry.CurrentValues.PropertyNames;
                }
                else if (actionTypeId == 3)
                {
                    propertyNames = entry.OriginalValues.PropertyNames;
                }

                foreach (string propName in propertyNames)
                {
                    var property = entry.Property(propName);

                    ActionLogProperty logProp = new ActionLogProperty();
                    logProp.PropertyName = propName;

                    if (actionTypeId == (int)ActionTypesEnum.Add)
                    {
                        logProp.NewValue = (property.CurrentValue ?? string.Empty).ToString(); ;
                        log.LogProperties.Add(logProp);
                    }
                    else if (actionTypeId == (int)ActionTypesEnum.Delete)
                    {
                        logProp.OldValue = (property.OriginalValue ?? string.Empty).ToString(); ;
                        log.LogProperties.Add(logProp);
                    }
                    else if (actionTypeId == (int)ActionTypesEnum.Edit)
                    {
                        var currentVal = (property.CurrentValue ?? string.Empty).ToString();
                        var originalVal = (property.OriginalValue ?? string.Empty).ToString();

                        if (currentVal != originalVal)
                        {
                            logProp.OldValue = originalVal;
                            logProp.NewValue = currentVal;

                            log.LogProperties.Add(logProp);
                        }
                    }
                }

                this.Logs.Add(log);
            }
        }

        public void SetChanges(List<DbEntityEntry> changes)
        {
            this.AddedEntites = changes.Where(c => c.State == System.Data.Entity.EntityState.Added).ToList();
            this.EditedEntities = changes.Where(c => c.State == System.Data.Entity.EntityState.Modified).ToList();
            this.DeletedEntities = changes.Where(c => c.State == System.Data.Entity.EntityState.Deleted).ToList();
        }

        public IEnumerable<ActionLog> GetLogs()
        {
            CreateLogs(this.AddedEntites, (int)ActionTypesEnum.Add);
            CreateLogs(this.EditedEntities, (int)ActionTypesEnum.Edit);
            CreateLogs(this.DeletedEntities, (int)ActionTypesEnum.Delete);

            return Logs.Select(l => l.ToBase());
        }
    }
}