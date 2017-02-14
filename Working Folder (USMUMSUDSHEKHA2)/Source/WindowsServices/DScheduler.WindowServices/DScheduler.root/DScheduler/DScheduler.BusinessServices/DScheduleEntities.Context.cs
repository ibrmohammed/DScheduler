﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DScheduler.BusinessServices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DScheduleEntities : DbContext
    {
        public DScheduleEntities()
            : base("name=DScheduleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TDependency> TDependencies { get; set; }
        public virtual DbSet<TEnvironment> TEnvironments { get; set; }
        public virtual DbSet<TJobMonitor> TJobMonitors { get; set; }
        public virtual DbSet<TLogin> TLogins { get; set; }
        public virtual DbSet<TRecurrenceRange> TRecurrenceRanges { get; set; }
        public virtual DbSet<TRef> TRefs { get; set; }
        public virtual DbSet<TReport> TReports { get; set; }
        public virtual DbSet<TSession> TSessions { get; set; }
        public virtual DbSet<TTemplate> TTemplates { get; set; }
        public virtual DbSet<TUproc> TUprocs { get; set; }
        public virtual DbSet<TJobAction> TJobActions { get; set; }
        public virtual DbSet<TNode> TNodes { get; set; }
        public virtual DbSet<TErrorLog> TErrorLogs { get; set; }
        public virtual DbSet<TRecurrencePatternDetail> TRecurrencePatternDetails { get; set; }
        public virtual DbSet<TJobRule> TJobRules { get; set; }
        public virtual DbSet<TOutage> TOutages { get; set; }
        public virtual DbSet<TOutageDetail> TOutageDetails { get; set; }
        public virtual DbSet<TRecurrencePattern> TRecurrencePatterns { get; set; }
    
        public virtual int DScheduler_uspInsertDataIntoJobMonitor(Nullable<int> nodeID, Nullable<int> environmentID, Nullable<int> sessionID, Nullable<int> uProcId, string uProcStatus, Nullable<System.DateTime> startedDateTime, string createdBy, string updatedBy, Nullable<System.DateTime> updatedDateTime, Nullable<System.DateTime> createdDateTime)
        {
            var nodeIDParameter = nodeID.HasValue ?
                new ObjectParameter("NodeID", nodeID) :
                new ObjectParameter("NodeID", typeof(int));
    
            var environmentIDParameter = environmentID.HasValue ?
                new ObjectParameter("EnvironmentID", environmentID) :
                new ObjectParameter("EnvironmentID", typeof(int));
    
            var sessionIDParameter = sessionID.HasValue ?
                new ObjectParameter("SessionID", sessionID) :
                new ObjectParameter("SessionID", typeof(int));
    
            var uProcIdParameter = uProcId.HasValue ?
                new ObjectParameter("UProcId", uProcId) :
                new ObjectParameter("UProcId", typeof(int));
    
            var uProcStatusParameter = uProcStatus != null ?
                new ObjectParameter("UProcStatus", uProcStatus) :
                new ObjectParameter("UProcStatus", typeof(string));
    
            var startedDateTimeParameter = startedDateTime.HasValue ?
                new ObjectParameter("StartedDateTime", startedDateTime) :
                new ObjectParameter("StartedDateTime", typeof(System.DateTime));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            var updatedByParameter = updatedBy != null ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(string));
    
            var updatedDateTimeParameter = updatedDateTime.HasValue ?
                new ObjectParameter("UpdatedDateTime", updatedDateTime) :
                new ObjectParameter("UpdatedDateTime", typeof(System.DateTime));
    
            var createdDateTimeParameter = createdDateTime.HasValue ?
                new ObjectParameter("CreatedDateTime", createdDateTime) :
                new ObjectParameter("CreatedDateTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DScheduler_uspInsertDataIntoJobMonitor", nodeIDParameter, environmentIDParameter, sessionIDParameter, uProcIdParameter, uProcStatusParameter, startedDateTimeParameter, createdByParameter, updatedByParameter, updatedDateTimeParameter, createdDateTimeParameter);
        }
    
        public virtual int DScheduler_uspFetchRecurrentSessionsDetails(Nullable<System.DateTime> processDate)
        {
            var processDateParameter = processDate.HasValue ?
                new ObjectParameter("ProcessDate", processDate) :
                new ObjectParameter("ProcessDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DScheduler_uspFetchRecurrentSessionsDetails", processDateParameter);
        }
    
        public virtual int uspSaveErrorLog(string exceptionMsg, string exceptionType, string exceptionSource, string exceptionURL, ObjectParameter errorId)
        {
            var exceptionMsgParameter = exceptionMsg != null ?
                new ObjectParameter("ExceptionMsg", exceptionMsg) :
                new ObjectParameter("ExceptionMsg", typeof(string));
    
            var exceptionTypeParameter = exceptionType != null ?
                new ObjectParameter("ExceptionType", exceptionType) :
                new ObjectParameter("ExceptionType", typeof(string));
    
            var exceptionSourceParameter = exceptionSource != null ?
                new ObjectParameter("ExceptionSource", exceptionSource) :
                new ObjectParameter("ExceptionSource", typeof(string));
    
            var exceptionURLParameter = exceptionURL != null ?
                new ObjectParameter("ExceptionURL", exceptionURL) :
                new ObjectParameter("ExceptionURL", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspSaveErrorLog", exceptionMsgParameter, exceptionTypeParameter, exceptionSourceParameter, exceptionURLParameter, errorId);
        }
    }
}
