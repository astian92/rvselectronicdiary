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
    
    public partial class ArchivedProduct
    {
        public ArchivedProduct()
        {
            this.ArchivedProtocolResults = new HashSet<ArchivedProtocolResult>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ArchivedDiaryId { get; set; }
        public string Name { get; set; }
        public string Test { get; set; }
        public string Quantity { get; set; }
        public string Number { get; set; }
    
        public virtual ArchivedDiary ArchivedDiary { get; set; }
        public virtual ICollection<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }
    }
}