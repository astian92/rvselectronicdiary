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
    
    public partial class TestMethod
    {
        public TestMethod()
        {
            this.ProductTests = new HashSet<ProductTest>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid TestId { get; set; }
        public string Method { get; set; }
    
        public virtual Test Test { get; set; }
        public virtual ICollection<ProductTest> ProductTests { get; set; }
    }
}
