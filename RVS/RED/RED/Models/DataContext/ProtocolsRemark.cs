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
    
    public partial class ProtocolsRemark
    {
        public System.Guid Id { get; set; }
        public System.Guid ProtocolId { get; set; }
        public System.Guid RemarkId { get; set; }
        public System.Guid AcredetationLevelId { get; set; }
        public int Number { get; set; }
    
        public virtual AcredetationLevel AcredetationLevel { get; set; }
        public virtual Remark Remark { get; set; }
        public virtual Protocol Protocol { get; set; }
    }
}
