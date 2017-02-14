using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class JobMonitor
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public int UprocID { get; set; }
        public string UprocName { get; set; }
        public int EnvironmentID { get; set; }
        public string EnvironmentName { get; set; }
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public string ScheduleDateTime { get; set; }
        public string StartedDateTime { get; set; }
        public string CompletedDateTime { get; set; }
        public string UProcStatus { get; set; }
        public string CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }

    }
}
