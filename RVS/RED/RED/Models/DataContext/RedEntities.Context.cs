﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RedDataEntities : DbContext
    {
        public RedDataEntities()
            : base("name=RedDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesFeature> RolesFeatures { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
