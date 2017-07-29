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
    
    public partial class Protocol
    {
        public Protocol()
        {
            this.ProtocolsRemarks = new HashSet<ProtocolsRemark>();
            this.ProtocolResults = new HashSet<ProtocolResult>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid RequestId { get; set; }
        public System.DateTime IssuedDate { get; set; }
        public string TesterMKB { get; set; }
        public string TesterFZH { get; set; }
        public string LabLeader { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual ICollection<ProtocolsRemark> ProtocolsRemarks { get; set; }
        public virtual ICollection<ProtocolResult> ProtocolResults { get; set; }
    }
}
