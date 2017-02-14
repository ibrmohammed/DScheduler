using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class NewUpdateJobModel
    {
        public string UprocName { get; set; }

        public int UprocId { get; set; }

        public string SessionName { get; set; }

        public int SessionId { get; set; }

        public int EnvironmentId { get; set; }

        public string EnvironmentName { get; set; }

        public string NodeName { get; set; }

        public int NodeId { get; set; }

        public int JobRuleId { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime StartedDateTime { get; set; }

        public DateTime CompletedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class NewUpdateJob
    {
        public List<NewUpdateJobModel> NewUpdateJobs { get; set; }

        public int UprocId { get; set; }

        public string UprocName { get; set; }

        public int SessionId { get; set; }

        public int EnvironmentId { get; set; }

        public int JobRuleId { get; set; }

        public List<Uprocs> UprocsList { get; set; }

        public List<Sessions> SessionsList { get; set; }

        public List<Environment> EnvironmentList { get; set; }

        public TimeSpan? JobStartTime { get; set; }
    }
}
