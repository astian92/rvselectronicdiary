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
    
    public partial class Test
    {
        public Test()
        {
            this.ProductTests = new HashSet<ProductTest>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid TestCategoryId { get; set; }
        public string Name { get; set; }
        public string TestMethods { get; set; }
        public System.Guid AcredetationLevelId { get; set; }
        public string Temperature { get; set; }
        public string UnitName { get; set; }
    
        public virtual AcredetationLevel AcredetationLevel { get; set; }
        public virtual ICollection<ProductTest> ProductTests { get; set; }
        public virtual TestCategory TestCategory { get; set; }
    }
}