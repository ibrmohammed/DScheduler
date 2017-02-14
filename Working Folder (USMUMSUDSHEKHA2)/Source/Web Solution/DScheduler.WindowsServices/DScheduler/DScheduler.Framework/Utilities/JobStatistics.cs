using System;
using System.Collections.Generic;
using System.Linq;

namespace DScheduler.Framework
{
    /// <summary>
    /// Static class with all static items required for batches.
    /// </summary>
    public static class JobStatistics
    {
        #region Fields

        /// <summary>
        /// This list can be used to hold exceptions thrown during execution.
        /// </summary>
        public static readonly Guid InstanceId = Guid.NewGuid();

        /// <summary>
        ///     The start date
        /// </summary>
        public static DateTime StartDate = ApplicationTime.GetCurrentTime();

        /// <summary>
        /// The batch errors property backing store
        /// </summary>
        private static List<BatchException> _batchErrors;

        #endregion

        #region Constructors and Destructors

        static JobStatistics()
        {
            _batchErrors = new List<BatchException>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The status of the job
        /// </summary>
        /// <value>
        /// The batch job status.
        /// </value>
        public static JobStatus BatchJobStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [continue chunk processing on error].
        /// </summary>
        /// <value>
        /// <c>true</c> if we need to [continue chunk processing on error]; otherwise, <c>false</c>.
        /// </value>
        public static bool ContinueChunkProcessingOnError { get; set; }

        /// <summary>
        /// Gets or sets the failure count.
        /// </summary>
        /// <value>
        /// The failure count.
        /// </value>
        public static int FailureCount { get; set; }

        /// <summary>
        /// Gets or sets the other count.
        /// </summary>
        /// <value>
        /// The other count.
        /// </value>
        public static int OtherCount { get; set; }

        /// <summary>
        /// The run identifier
        /// </summary>
        public static Guid RunId { get; internal set; }

        /// <summary>
        /// The number of items successfully processed.
        /// </summary>
        /// <value>
        /// The success count.
        /// </value>
        public static int SuccessCount { get; set; }

        /// <summary>
        /// The total number of items processed.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public static int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public static string AdditionalInformation { get; set; }

        /// <summary>
        /// Gets or sets the batch user identifier.
        /// </summary>
        /// <value>
        /// The batch user identifier.
        /// </value>
        public static string BatchUserId { get; internal set; }

        /// <summary>
        /// Gets or sets the job step summary additional information.
        /// </summary>
        /// <value>
        /// The job step summary additional information.
        /// </value>
        public static string JobStepSummaryAdditionalInformation { get; set; }

        #endregion Properties

        #region Internal properties

        /// <summary>
        ///     This list can be used to hold exceptions thrown during execution.
        /// </summary>
        internal static List<BatchException> BatchExceptions
        {
            get
            {
                return _batchErrors;
            }
        }

        /// <summary>
        /// This property holds the JobStepSummaryId or JobSummaryId depending on whether this is a Batch or a BatchStep
        /// </summary>
        internal static long BatchRunId { get; set; }

        /// <summary>
        /// This property holds the JobStepSummaryId or JobSummaryId depending on whether this is a Batch or a BatchStep
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is last process; otherwise, <c>false</c>.
        /// </value>
        internal static bool IsLastProcess { get; set; }

        /// <summary>
        /// The JobCode of the running job.
        /// </summary>
        internal static string JobCode { get; set; }

        /// <summary>
        /// The job step code
        /// </summary>
        internal static string JobStepCode { get; set; }

        /// <summary>
        /// The job sub step code
        /// </summary>
        internal static string JobSubStepCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [restart failed batch run].
        /// </summary>
        /// <value>
        ///   <c>true</c> if we need to [restart the last failed batch run]; otherwise, <c>false</c>.
        /// </value>
        internal static bool RestartFailedRun { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [process chunk information only].
        /// </summary>
        /// <value>
        /// <c>true</c> if [process chunk information only]; otherwise, <c>false</c>.
        /// </value>
        internal static bool ProcessChunkInformationOnly { get; set; }

        #endregion Internal properties

        #region Methods

        /// <summary>
        /// Adds the batch error.
        /// </summary>
        /// <param name="batchError">The batch error.</param>
        public static void AddBatchError(BatchException batchError)
        {
            bool isIdentifierKeyValueEmpty = string.IsNullOrWhiteSpace(batchError.IdentifierInformation.Key) && string.IsNullOrWhiteSpace(batchError.IdentifierInformation.Value);
            bool keyValueExists = BatchExceptions
                .Select(x => x.IdentifierInformation)
                .Any(x => x.Key == batchError.IdentifierInformation.Key && x.Value == batchError.IdentifierInformation.Value);

            var aggregateExceptions = batchError.Exception as AggregateException;
            if (isIdentifierKeyValueEmpty || !keyValueExists)
            {
                FailureCount += 1;
            }

            if (aggregateExceptions != null)
            {
                //Create a list
                var innerErrors = aggregateExceptions.InnerExceptions.Select(e => new BatchException {Exception = e, IdentifierInformation = batchError.IdentifierInformation, JobChunkId = batchError.JobChunkId});
                BatchExceptions.AddRange(innerErrors);
            }
            else
            {
                BatchExceptions.Add(batchError);
            }
        }

        /// <summary>
        /// Adds the batch error.
        /// </summary>
        /// <param name="batchErrors">The batch error.</param>
        public static void AddBatchError(IEnumerable<BatchException> batchErrors)
        {
            foreach (var batchError in batchErrors)
            {
                AddBatchError(batchError);
            }
        }

        /// <summary>
        /// Clears the batch error list.
        /// </summary>
        internal static void ClearBatchErrors()
        {
            _batchErrors = new List<BatchException>();
        }

        #endregion Methods
    }
}