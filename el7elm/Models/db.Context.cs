﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace el7elm.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<appointment> appointments { get; set; }
        public virtual DbSet<avail_appointments> avail_appointments { get; set; }
        public virtual DbSet<doctor> doctors { get; set; }
        public virtual DbSet<patient> patients { get; set; }
        public virtual DbSet<payment> payments { get; set; }
        public virtual DbSet<prescription> prescriptions { get; set; }
    }
}