using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class JobMonitorSearchCriteria
    {
        public string UprocName { get; set; }
        public string SessionName { get; set; }
        public DateTime? JobRunDate { get; set; }
        public string EnvironmentName { get; set; }
        public string NodeName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string JobStatus { get; set; }

    }
}
