 using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DScheduler.Framework;
namespace DScheduler.BusinessServices
{
    class ExecuteUProcOnReRunAction : Job
    {
        int numberOfThreads;
        IEnumerable<ScheduledSessionDetails> scheduledJobs;
        protected string dbConnection;
        public static ConcurrentDictionary<string, object> ContextValues = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Constructor containing Job Name
        /// </summary>
        public ExecuteUProcOnReRunAction(IEnumerable<ScheduledSessionDetails> scheduledJobs, int numberOfThreads, string connectionString)
            : base()
        {
            this.scheduledJobs = scheduledJobs;
            this.numberOfThreads = numberOfThreads;
            this.dbConnection = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override ITask Task
        {
            get { return CreateTask(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ITask CreateTask()
        {
            //Check
            var getDependencyRecords = new GetSessionRecordsForList
            {
                scheduledRecordsList = this.scheduledJobs
            };
            //Need to calculation the number of partitions which needs to be done
            int numberOfPartitions = this.scheduledJobs != null ? this.scheduledJobs.Count() / numberOfThreads : numberOfThreads;
            var dependentListTask = new ScheduledJobRuleTasks(numberOfThreads);
            var processProviderIdForRequestTriggers = new ProcessUProcsTasksForEachSession(dbConnection);
            getDependencyRecords.
                Chain<ConvertDataRowToEntities>().
                Chain(dependentListTask).
                Chain(processProviderIdForRequestTriggers);
            return getDependencyRecords;
        }

        #region Batch Framework overrides

        /// <summary>
        /// Overrides the OnInitialize methodzx
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        /// <summary>
        /// Overrides the On Error method
        /// </summary>
        /// <param name="exception"></param>
        protected override void OnError(Exception exception)
        {
            base.OnError(exception);
        }

        /// <summary>
        /// Overrides the Oncomplete Method
        /// </summary>
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        #endregion
    }
}