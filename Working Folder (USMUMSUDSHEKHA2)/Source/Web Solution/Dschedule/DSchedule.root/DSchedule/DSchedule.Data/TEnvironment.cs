//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSchedule.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEnvironment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEnvironment()
        {
            this.TSessions = new HashSet<TSession>();
            this.TUprocs = new HashSet<TUproc>();
            this.TJobMonitors = new HashSet<TJobMonitor>();
        }
    
        public int EnvironmentID { get; set; }
        public int NodeID { get; set; }
        public string EnvironmentName { get; set; }
        public string EnvironmentPath { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual TNode TNode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSession> TSessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUproc> TUprocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TJobMonitor> TJobMonitors { get; set; }
    }
}
