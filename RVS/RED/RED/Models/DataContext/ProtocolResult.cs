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
    
    public partial class ProtocolResult
    {
        public System.Guid Id { get; set; }
        public System.Guid ProtocolId { get; set; }
        public System.Guid ProductId { get; set; }
        public string Results { get; set; }
        public string MethodValue { get; set; }
        public string ResultNumber { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Protocol Protocol { get; set; }
    }
}