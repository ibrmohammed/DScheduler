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
    
    public partial class TRef
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRef()
        {
            this.TJobRules = new HashSet<TJobRule>();
            this.TLogins = new HashSet<TLogin>();
            this.TRecurrencePatterns = new HashSet<TRecurrencePattern>();
            this.TRecurrenceRanges = new HashSet<TRecurrenceRange>();
            this.TSessions = new HashSet<TSession>();
            this.TUprocs = new HashSet<TUproc>();
            this.TUprocs1 = new HashSet<TUproc>();
        }
    
        public int RefID { get; set; }
        public string RefName { get; set; }
        public string RefValue { get; set; }
        public string RefDescription { get; set; }
        public string IsActive { get; set; }
        public string IsVisible { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TJobRule> TJobRules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TLogin> TLogins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRecurrencePattern> TRecurrencePatterns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRecurrenceRange> TRecurrenceRanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSession> TSessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUproc> TUprocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUproc> TUprocs1 { get; set; }
    }
}
