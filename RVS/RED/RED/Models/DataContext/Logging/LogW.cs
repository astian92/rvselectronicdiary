using System;
using System.Collections.Generic;

namespace RED.Models.DataContext.Logging
{
    public class LogW
    {
        public LogW()
        {
            this.LogProperties = new List<ActionLogProperty>();
        }

        public Guid UserId { get; set; }

        public string TableName { get; set; }

        public string FullTableName { get; set; }

        public DateTime On { get; set; }

        public int ActionTypeId { get; set; }

        public string TableNameBg { get; set; }

        public List<ActionLogProperty> LogProperties { get; set; }

        public ActionLog ToBase()
        {
            ActionLog log = new ActionLog();

            log.UserId = this.UserId;
            log.TableName = this.TableName;
            log.FullTableName = this.FullTableName;
            log.On = this.On;
            log.ActionTypeId = this.ActionTypeId;
            log.TableNameBg = this.TableNameBg;

            foreach (var propLog in this.LogProperties)
            {
                log.ActionLogProperties.Add(propLog);
            }

            return log;
        }
    }
}