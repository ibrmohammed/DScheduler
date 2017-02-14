//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TUproc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TUproc()
        {
            this.TDependencies = new HashSet<TDependency>();
            this.TJobMonitors = new HashSet<TJobMonitor>();
        }
    
        public int UprocID { get; set; }
        public int JobTypeID { get; set; }
        public int EnvironmentID { get; set; }
        public int AccountTypeID { get; set; }
        public string UprocName { get; set; }
        public string EnvironmentName { get; set; }
        public string FolderPath { get; set; }
        public string Command { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TDependency> TDependencies { get; set; }
        public virtual TEnvironment TEnvironment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TJobMonitor> TJobMonitors { get; set; }
        public virtual TRef TRef { get; set; }
        public virtual TRef TRef1 { get; set; }
    }
}
