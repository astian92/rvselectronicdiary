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
    
    public partial class User
    {
        public User()
        {
            this.ActionLogs = new HashSet<ActionLog>();
            this.Requests = new HashSet<Request>();
        }
    
        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public System.Guid RoleId { get; set; }
    
        public virtual ICollection<ActionLog> ActionLogs { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual Role Role { get; set; }
    }
}
