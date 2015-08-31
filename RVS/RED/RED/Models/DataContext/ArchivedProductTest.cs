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
    
    public partial class ArchivedProductTest
    {
        public ArchivedProductTest()
        {
            this.ArchivedProtocolResults = new HashSet<ArchivedProtocolResult>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ArchivedProductId { get; set; }
        public string TestName { get; set; }
        public string TestUnitName { get; set; }
        public string TestMethods { get; set; }
        public string TestAcredetationLevel { get; set; }
        public string TestTemperature { get; set; }
        public string TestCategory { get; set; }
    
        public virtual ArchivedProduct ArchivedProduct { get; set; }
        public virtual ICollection<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }
    }
}
