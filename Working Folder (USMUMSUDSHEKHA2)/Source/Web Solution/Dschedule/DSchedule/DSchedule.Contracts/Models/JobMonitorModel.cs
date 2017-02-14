using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class JobMonitorModel
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
        public Nullable<System.DateTime> ScheduleDateTime { get; set; }
        public Nullable<System.DateTime> StartedDateTime { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public string UProcStatus { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }

        public List<JobMonitor> JobMonitorList { get; set; }
        public List<Node> NodeList { get; set; }
        public List<Environment> EnvironmentList { get; set; }
        public List<Sessions> SessionsList { get; set; }
        public List<Uprocs> UprocsList { get; set; }

        public List<ReferenceData> JobActionTypes { get; set; }

    }
}
