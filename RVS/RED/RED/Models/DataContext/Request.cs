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
    
    public partial class Request
    {
        public System.Guid Id { get; set; }
        public System.Guid DiaryId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.Guid> AcceptedBy { get; set; }
        public bool IsAccepted { get; set; }
    
        public virtual Diary Diary { get; set; }
        public virtual User User { get; set; }
    }
}
