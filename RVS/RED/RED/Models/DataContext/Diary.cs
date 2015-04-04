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
    
    public partial class Diary
    {
        public Diary()
        {
            this.Requests = new HashSet<Request>();
            this.Products = new HashSet<Product>();
        }
    
        public System.Guid Id { get; set; }
        public int Number { get; set; }
        public System.DateTime AcceptanceDateAndTime { get; set; }
        public int LetterNumber { get; set; }
        public System.DateTime LetterDate { get; set; }
        public string Contractor { get; set; }
        public System.Guid ClientId { get; set; }
        public string Comment { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
