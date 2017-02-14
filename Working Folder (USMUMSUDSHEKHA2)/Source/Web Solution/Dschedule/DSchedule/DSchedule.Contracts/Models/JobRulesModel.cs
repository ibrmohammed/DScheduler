using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DSchedule.Contracts.Models
{
    public class JobRulesModel
    {
        public JobRulesModel()
        {
            this.JobRulesList = new List<Models.JobRules>();
            this.Outage = new List<Outage>();
        }
        public int JobRuleId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field Uproc/Session is required")]
        [Display(Name = "Uproc/Session")]
        public int ActivityTypeID { get; set; }
        [RecurrenceValidator("IsRecurrence","Please select a recurrence pattern.",ErrorMessage ="Please select a recurrence pattern")]
        public string RecurrencePattern { get; set; }

        [Required]
        [Display(Name = "Uproc/Session")]
        public int UprocOrSessionId { get; set; }

        public bool IsRecurrence { get; set; }

        public bool IsSunday { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public string OutageName { get; set; }
        public string RYMonth { get; set; }
        public string RMDay { get; set; }
        public string RYDay { get; set; }
        public DateTime? OutageDate { get; set; }
        public TimeSpan? OutageFrom { get; set; }
        public TimeSpan? OutageTo { get; set; }

        [Required]
        [Display(Name = "Job Start Time")]
        [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Job Start Time is required and must be in proper format.")]
        public TimeSpan? JobStartDate { get; set; }

        public DateTime? RecurrenceStartDate { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public List<JobRules> JobRulesList { get; set; }
        public List<ReferenceData> UprocsReferenceData { get; set; }
        public List<ReferenceData> SessionsReferenceData { get; set; }
        public List<Outage> ListOutageDetails { get; set; }
        public List<Outage> Outage { get; set; }
        
    }
}
