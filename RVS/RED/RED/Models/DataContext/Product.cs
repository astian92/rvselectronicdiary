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
    
    public partial class Product
    {
        public Product()
        {
            this.ProductTests = new HashSet<ProductTest>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid DiaryId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    
        public virtual Diary Diary { get; set; }
        public virtual ICollection<ProductTest> ProductTests { get; set; }
    }
}
