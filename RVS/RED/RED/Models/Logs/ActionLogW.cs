using System;
using RED.Models.DataContext;

namespace RED.Models.Logs
{
    public class ActionLogW
    {
        public ActionLogW(ActionLog log, User user)
        {
            this.Id = log.Id;
            this.UserId = log.User.Id;
            this.TableName = log.TableName;
            this.FullTableName = log.FullTableName;
            this.On = log.On;
            this.ActionTypeId = log.ActionTypeId;
            this.TableNameBg = log.TableNameBg;
            this.UserName = user.Username;
        }

        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string TableName { get; set; }

        public string FullTableName { get; set; }

        public DateTime On { get; set; }

        public int ActionTypeId { get; set; }

        public string TableNameBg { get; set; }

        public ActionLogW()
        {

        }

        public ActionLog ToBase()
        {
            ActionLog log = new ActionLog();

            log.Id = this.Id;
            log.User.Id = this.UserId;
            log.TableName = this.TableName;
            log.FullTableName = this.FullTableName;
            log.On = this.On;
            log.ActionTypeId = this.ActionTypeId;
            log.TableNameBg = this.TableNameBg;

            return log;
        }
    }
}