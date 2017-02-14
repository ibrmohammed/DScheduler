using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DScheduler.Common;
using DScheduler.Framework;

namespace DScheduler.BusinessServices
{
    public class ScheduledSessionsConverter : DataRowConverter<ScheduledSessionDetails>
    {
        /// <summary>
        /// Overrides the Assign function
        /// </summary>
        protected override void Assign()
        {
            SetField<string>(Constants.UProcIDs, (t, p) => t.UProcID = p);

            SetField<string>(Constants.SessionID, (t, p) => t.SessionID = p);

           // SetField<System.TimeSpan>(Constants.JobStartTime, (t, p) => t.JobStartTime = p);
            
        }
    }
}