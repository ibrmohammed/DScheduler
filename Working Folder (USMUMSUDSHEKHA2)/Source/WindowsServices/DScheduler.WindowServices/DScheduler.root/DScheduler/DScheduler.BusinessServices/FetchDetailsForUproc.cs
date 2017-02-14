using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DScheduler.Framework;

namespace DScheduler.BusinessServices
{
    class FetchDetailsForUproc : Job
    {
        private List<ScheduledSessionDetails> singleUprocRunList;
        private int numberOfThreads;
        private string connectionString;

        public FetchDetailsForUproc(List<ScheduledSessionDetails> singleUprocRunList, int numberOfThreads, string connectionString)
        {
            // TODO: Complete member initialization
            this.singleUprocRunList = singleUprocRunList;
            this.numberOfThreads = numberOfThreads;
            this.connectionString = connectionString;
        }
        protected override ITask Task
        {
            get { return CreateTask(); }
        }
        private ITask CreateTask()
        {
            var getDependencyRecords = new GetSessionRecordsForList
            {
                scheduledRecordsList = this.singleUprocRunList
            };
            //Need to calculation the number of partitions which needs to be done
            int numberOfPartitions = this.singleUprocRunList != null ? this.singleUprocRunList.Count() / numberOfThreads : numberOfThreads;
            var dependentListTask = new ScheduledJobRuleTasks(numberOfThreads);
            var processProviderIdForRequestTriggers = new ProcessUProcsTasksForEachSession(connectionString);
            getDependencyRecords.
                Chain<ConvertDataRowToEntities>().
                Chain(dependentListTask).
                Chain(processProviderIdForRequestTriggers);
            return getDependencyRecords;
        }

    }
}
