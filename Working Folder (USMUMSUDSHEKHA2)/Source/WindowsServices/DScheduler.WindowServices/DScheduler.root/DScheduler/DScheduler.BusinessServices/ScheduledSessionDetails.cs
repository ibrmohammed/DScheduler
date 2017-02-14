using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.BusinessServices
{
    public class ScheduledSessionDetails
    {
        public string SessionID { get; set; }

        public string UProcID { get; set; }

        public TimeSpan? JobStartTime { get; set; }

        public string JobAction { get; set; }
    }
}


