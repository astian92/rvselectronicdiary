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
    
        public virtual DbSet<AcredetationLevel> AcredetationLevels { get; set; }
        public virtual DbSet<ActionLogProperty> ActionLogProperties { get; set; }
        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Protocol> Protocols { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesFeature> RolesFeatures { get; set; }
        public virtual DbSet<TestCategory> TestCategories { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ArchivedDiary> ArchivedDiaries { get; set; }
        public virtual DbSet<ArchivedProduct> ArchivedProducts { get; set; }
        public virtual DbSet<ArchivedProtocolResult> ArchivedProtocolResults { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTest> ProductTests { get; set; }
        public virtual DbSet<ProtocolResult> ProtocolResults { get; set; }
    }
}
