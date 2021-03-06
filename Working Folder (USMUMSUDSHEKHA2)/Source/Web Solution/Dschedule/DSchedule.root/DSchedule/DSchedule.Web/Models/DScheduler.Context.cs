﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSchedule.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DSchedulerEntities : DbContext
    {
        public DSchedulerEntities()
            : base("name=DSchedulerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<TDependency> TDependencies { get; set; }
        public virtual DbSet<TEnvironment> TEnvironments { get; set; }
        public virtual DbSet<TJobAction> TJobActions { get; set; }
        public virtual DbSet<TJobMonitor> TJobMonitors { get; set; }
        public virtual DbSet<TJobRule> TJobRules { get; set; }
        public virtual DbSet<TLogin> TLogins { get; set; }
        public virtual DbSet<TNode> TNodes { get; set; }
        public virtual DbSet<TOutageDetail> TOutageDetails { get; set; }
        public virtual DbSet<TRecurrencePattern> TRecurrencePatterns { get; set; }
        public virtual DbSet<TRecurrenceRange> TRecurrenceRanges { get; set; }
        public virtual DbSet<TRef> TRefs { get; set; }
        public virtual DbSet<TReport> TReports { get; set; }
        public virtual DbSet<TSession> TSessions { get; set; }
        public virtual DbSet<TTemplate> TTemplates { get; set; }
        public virtual DbSet<TUproc> TUprocs { get; set; }
    }
}
