using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class JobRules
    {
        public int UprocId { get; set; }
        public string UprocName { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public int EnvironmentId { get; set; }
        public string Environment { get; set; }
        public int NodeId { get; set; }
        public string Node { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Outage> Outage { get; set; }
    }
}
