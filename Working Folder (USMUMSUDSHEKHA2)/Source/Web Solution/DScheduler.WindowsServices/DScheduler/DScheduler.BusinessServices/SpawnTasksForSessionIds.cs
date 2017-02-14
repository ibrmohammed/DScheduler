using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DScheduler.Framework;
using DScheduler.Common;

namespace DScheduler.BusinessServices
{
    class SpawnTasksForSessionIds : EnumerateTask<ScheduledSessionDetails>
    {

        private readonly IEnumerable<ScheduledSessionDetails> providerIdList;
        DScheduleEntities connectionString;

        public SpawnTasksForSessionIds(IEnumerable<ScheduledSessionDetails> providerIdForTriggers, string connectionString)
        {           
            this.providerIdList = providerIdForTriggers;
            this.connectionString = new DScheduleEntities(connectionString);
        }
        /// <summary>
        /// Overrides the argument
        /// </summary>
        public override IEnumerable<ScheduledSessionDetails> Argument
        {
            get
            {
                return providerIdList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
        }


    }
}
