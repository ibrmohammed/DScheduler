using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class Job
    {
        internal readonly BatchLogger JobLogger = new BatchLogger();
        private readonly List<IDSchedulerException> _exceptions;
        private readonly List<IStatusInfo> _logs;
        internal static string ConnectionStringName = BatchConstants.ConnectionBatchJob;
        internal static string LoggingConnectionStringName = BatchConstants.ConnectionBatchJobLogging;

        #region Constructors

        /// <summary>
        /// The constructor to create a batch with a name.
        /// Note that all internal BatchJob logging and master data usage would be bypasses if this constructor is used.
        /// </summary>
        /// <param name="name">The name to reference the Batch by</param>
        protected Job()
        {
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        protected Job(string name, string jobCode)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();

            JobProcessor.InitializeBatch(jobCode, null, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        protected Job(string name, string jobCode, string jobStepCode)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();

            JobProcessor.InitializeBatch(jobCode, jobStepCode, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        protected Job(string name, string jobCode, string jobStepCode, Func<int> totalCount)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();

            JobProcessor.InitializeBatch(jobCode, jobStepCode, totalCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        protected Job(string name, string jobCode, string jobStepCode, Func<int> totalCount, string connectionStringName)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();
            ConnectionStringName = connectionStringName;
            LoggingConnectionStringName = connectionStringName;

            JobProcessor.InitializeBatch(jobCode, jobStepCode, totalCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job" /> class. Use this overload only if this batch processes and persists chunk information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="processChunkInformationOnly">if set to <c>true</c> [process chunk information only].</param>
        protected Job(string name, string jobCode, string jobStepCode, Func<int> totalCount, bool processChunkInformationOnly)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();

            JobProcessor.InitializeBatch(jobCode, jobStepCode, totalCount);

            JobStatistics.ProcessChunkInformationOnly = processChunkInformationOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Job" /> class. Use this overload only if this batch processes and persists chunk information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="processChunkInformationOnly">if set to <c>true</c> [process chunk information only].</param>
        /// <param name="batchConnectionString">The batch connection string.</param>
        protected Job(string name, string jobCode, string jobStepCode, Func<int> totalCount, bool processChunkInformationOnly, string batchConnectionString)
        {
            Name = name;
            _logs = new List<IStatusInfo>();
            _exceptions = new List<IDSchedulerException>();
            ConnectionStringName = batchConnectionString;
            LoggingConnectionStringName = batchConnectionString;
            JobProcessor.InitializeBatch(jobCode, jobStepCode, totalCount);

            JobStatistics.ProcessChunkInformationOnly = processChunkInformationOnly;
        }

        #endregion

        #region Public members and methods

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        public static void Execute<TJob>() where TJob : Job, new()
        {
            var job = new TJob();
            job.Execute();
        }

        /// <summary>
        /// The core method
        /// </summary>
        public virtual void Execute()
        {
            try
            {
                OnInitialize();
                Task.Run();
                OnComplete();
            }
            catch (Exception e)
            {
                OnError(e);
            }
            finally
            {
                OnCleanUp();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public virtual void PersistStatusInfo(IEnumerable<IDSchedulerException> exceptions, IEnumerable<IStatusInfo> logs)
        {
            if (DatabaseHelper == null)
            {
                return;
            }

            DatabaseHelper.BulkInsert(exceptions, "KhbeInterfaceException", FillRow);

            const string TableName = "KhbeInterfaceException";

            DataTable table = DatabaseHelper.GetDataTableBySql("SELECT * FROM KhbeInterfaceException WHERE 1=0");

            try
            {
                DatabaseHelper.BulkInsert(exceptions.ToTable(table, FillRow), TableName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

        #region Protected memebers and methods

        /// <summary>
        ///
        /// </summary>
        protected virtual IDatabaseHelper DatabaseHelper { get { return null; } }

        /// <summary>
        /// The root task for this job
        /// </summary>
        protected abstract ITask Task { get; }

        /// <summary>
        /// Clean up event can be called here from the derived classes
        /// </summary>
        protected virtual void OnCleanUp()
        {
            PersistStatusInfo(_exceptions, _logs);
        }

        /// <summary>
        /// This is method is called after a job task is complete.
        /// To be able to use the integrated logging, call base.OnComplete() in the derived class.
        /// </summary>
        protected virtual void OnComplete()
        {
            if (string.IsNullOrEmpty(JobStatistics.JobCode))
            {
                return;
            }
            if(JobParameters.ContinuePreviousInstanceRun)
            {
                Console.WriteLine(BatchConstants.MessageContinuePreviousInstanceRun1);
                Console.WriteLine(BatchConstants.MessageContinuePreviousInstanceRun2);
                Console.WriteLine(BatchConstants.MessageContinuePreviousInstanceRun3);
            }
            //Logic for Job Stat goes here

            //  If the exception list has data/Log the errors.
            if (JobStatistics.BatchExceptions.Any())
            {
                JobLogger.LogBatchException();
            }

            if (JobStatistics.BatchJobStatus != JobStatus.Gated)
            {
                JobStatistics.BatchJobStatus = JobStatus.Completed;
            }
            if (JobStatistics.ProcessChunkInformationOnly)
            {
                JobProcessor.UpdateBatchStatistics(JobStatistics.BatchExceptions.Any() ? JobStatus.Failed : JobStatus.Completed);
                return;
            }

            JobProcessor.UpdateBatchStatistics(JobStatistics.BatchJobStatus);
        }

        /// <summary>
        /// The error event can be raised here from the derived classes.
        /// To be able to use the integrated logging, call base.OnError() in the derived class.
        /// </summary>
        /// <param name="exception">The exception to encapsulate</param>
        protected virtual void OnError(Exception exception)
        {
            if (string.IsNullOrEmpty(JobStatistics.JobCode))
            {
                return;
            }
            //Logic for Job Stat exception logging goes here
            if (JobStatistics.BatchExceptions.Any())
            {
                JobLogger.LogBatchException();
            }

            JobLogger.LogBatchException(exception);

            JobProcessor.UpdateBatchStatistics(JobStatus.Failed); //This needs to be verified.
            Environment.ExitCode = -998;
        }

        /// <summary>
        /// The initialize can be raised here from the derived classes
        /// </summary>
        protected virtual void OnInitialize()
        {
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected virtual void OnLogInterfaceException(object sender, IDSchedulerException e)
        //{
        //    //Add job related properties such as JobId etc.
        //    _exceptions.Add(e.KhbeInterfaceException);
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="statusInfo"></param>
        protected virtual void OnLogStatus(IStatusInfo statusInfo)
        {
            //Add job related properties such as JobId etc.
            _logs.Add(statusInfo);
        }

        #endregion

        #region Private members and methods

        private static void FillRow(IDSchedulerException exception, DataRow dataRow)
        {           
            dataRow.SetField<string>("INTERFACE_CODE", exception.InterfaceCode);
            dataRow.SetField<DateTime>("PROCESS_DATE", exception.ProcessDate);
            dataRow.SetField<Guid>("JOB_ID", exception.JobId);
            dataRow.SetField<int>("RUN_NUM", exception.RunNum);
            dataRow.SetField<string>("RECORD_VALUE", exception.RecordValue);
            dataRow.SetField<string>("RECORD_NUM", exception.RecordNum);
            dataRow.SetField<string>("EXCEPTION_TYPE", exception.ExceptionType);
            dataRow.SetField<string>("EXCEPTION_SUMMARY", exception.ExceptionSummary);
            dataRow.SetField<string>("EXCEPTION_DETAILS", exception.ExceptionDetails);
            dataRow.SetField<string>("RAW_ERROR_MESSAGE", exception.RawErrorMessage);
            dataRow.SetField<DateTime>("EXCEPTION_TIME", exception.ExceptionTime);
            dataRow.SetField<string>("KEY_IDENTIFIER", exception.KeyIdentifier);
            dataRow.SetField<string>("KEY_VALUE", exception.KeyValue);
            dataRow.SetField<int>("SSN", exception.Ssn);
            dataRow.SetField<DateTime>("CREATED_DATE", exception.CreatedDate);
            dataRow.SetField<string>("CREATED_BY", exception.CreatedBy);
            dataRow.SetField<DateTime>("UPDATED_DATE", exception.UpdatedDate);
            dataRow.SetField<string>("UPDATED_BY", exception.UpdatedBy);
        }

        #endregion
    }
}