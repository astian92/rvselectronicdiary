//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RED.Models.DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActionLog
    {
        public ActionLog()
        {
            this.ActionLogProperties = new HashSet<ActionLogProperty>();
        }
    
        public int Id { get; set; }
        public System.Guid UserId { get; set; }
        public string TableName { get; set; }
        public string FullTableName { get; set; }
        public System.DateTime On { get; set; }
        public int ActionTypeId { get; set; }
        public string TableNameBg { get; set; }
    
        public virtual ICollection<ActionLogProperty> ActionLogProperties { get; set; }
        public virtual ActionType ActionType { get; set; }
        public virtual User User { get; set; }
    }
}
