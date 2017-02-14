using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;



namespace DScheduler.Framework
{
    /// <summary>
    /// This job can be used to run jobs which needs to gate chunk data.
    /// </summary>
    public abstract class GatedJob : Job
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name to reference the Batch by</param>
        /// <param name="jobCode">The Job code of the running batch.</param>
        protected GatedJob(string name, string jobCode)
            : base(name, jobCode)
        {
            JobStatistics.ContinueChunkProcessingOnError = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name to reference the Batch by</param>
        /// <param name="jobCode">The Job code of the running batch.</param>
        /// <param name="jobStepCode">The job step code.</param>
        protected GatedJob(string name, string jobCode, string jobStepCode)
            : base(name, jobCode, jobStepCode)
        {
            JobStatistics.ContinueChunkProcessingOnError = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        protected GatedJob(string name, string jobCode, string jobStepCode, Func<int> totalCount)
            : base(name, jobCode, jobStepCode, totalCount)
        {
            JobStatistics.ContinueChunkProcessingOnError = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        protected GatedJob(string name, string jobCode, string jobStepCode, Func<int> totalCount, string connectionStringName)
            : base(name, jobCode, jobStepCode, totalCount, connectionStringName)
        {
            JobStatistics.ContinueChunkProcessingOnError = true;
        }

        #region ChunkProcessing Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name to reference the Batch by</param>
        /// <param name="jobCode">The Job code of the running batch.</param>
        /// <param name="continueChunkProcessingOnError">if set to <c>true</c> [continue chunk processing on error].</param>
        protected GatedJob(string name, string jobCode, bool continueChunkProcessingOnError)
            : base(name, jobCode)
        {
            JobStatistics.ContinueChunkProcessingOnError = continueChunkProcessingOnError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name to reference the Batch by</param>
        /// <param name="jobCode">The Job code of the running batch.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="continueChunkProcessingOnError">if set to <c>true</c> [continue chunk processing on error].</param>
        protected GatedJob(string name, string jobCode, string jobStepCode, bool continueChunkProcessingOnError)
            : base(name, jobCode, jobStepCode)
        {
            JobStatistics.ContinueChunkProcessingOnError = continueChunkProcessingOnError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="continueChunkProcessingOnError">if set to <c>true</c> [continue chunk processing on error].</param>
        protected GatedJob(string name, string jobCode, string jobStepCode, Func<int> totalCount, bool continueChunkProcessingOnError)
            : base(name, jobCode, jobStepCode, totalCount)
        {
            JobStatistics.ContinueChunkProcessingOnError = continueChunkProcessingOnError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatedJob"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="continueChunkProcessingOnError">if set to <c>true</c> [continue chunk processing on error].</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        protected GatedJob(string name, string jobCode, string jobStepCode, Func<int> totalCount, bool continueChunkProcessingOnError, string connectionStringName)
            : base(name, jobCode, jobStepCode, totalCount, connectionStringName)
        {
            JobStatistics.ContinueChunkProcessingOnError = continueChunkProcessingOnError;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current chunk.
        /// </summary>
        /// <value>
        /// The current chunk.
        /// </value>
        public static ChunkInformation CurrentChunk { get; internal set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Executes a gated job. Make sure chunking has been done OnInitialize
        /// </summary>
        /// <typeparam name="TJob">The type of the job.</typeparam>
        /// <param name="createSegments">The create segments.</param>
        public static void ExecuteGatedJob<TJob>(Func<IEnumerable<ChunkInformation>> createSegments) where TJob : GatedJob, new()
        {
            var job = new TJob();
            job.ExecuteGatedJob(createSegments);
        }

        /// <summary>
        /// Executes the gated job.
        /// </summary>
        /// <typeparam name="TJob">The type of the job.</typeparam>
        /// <param name="chunkSize">The chunk size.</param>
        /// <param name="recordStartIndex">Start index of the record.</param>
        /// <param name="recordEndIndex">End index of the record.</param>
        public static void ExecuteGatedJob<TJob>(long chunkSize, long recordStartIndex, long recordEndIndex) where TJob : GatedJob, new()
        {
            var job = new TJob();
            job.ExecuteGatedJob(chunkSize, recordStartIndex, recordEndIndex);
        }

        /// <summary>
        /// The core method
        /// </summary>
        public void ExecuteGatedJob()
        {
            try
            {
                OnInitialize();

                //Create the chunks if not already present.
                var success = ChunkProcessor.GetChunkCreationStatus();

                if (success == null)
                {
                    throw new InvalidOperationException(BatchConstants.MessageChunkNotCreated);
                }

                if (success == ProcessingStatus.Processing.ToString())
                {
                    while (success == ProcessingStatus.Processing.ToString())
                    {
                        //Poll after a certain time.
                        Thread.Sleep(500);//Arbitrary
                        success = ChunkProcessor.GetChunkCreationStatus();
                    }
                }

                if (success == ProcessingStatus.Failed.ToString())
                {
                    throw new Exception(BatchConstants.MessageChunkCreationBatchFailed);
                }

                if (success == ProcessingStatus.Completed.ToString())
                {
                    ProcessChunks();
                }

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
        /// The core method
        /// </summary>
        /// <param name="createChunks">The create segments.</param>
        public void ExecuteGatedJob(Func<IEnumerable<ChunkInformation>> createChunks)
        {
            if (createChunks == null)
            {
                throw new InvalidDataException(BatchConstants.MessageInvalidChunkCreationMethod);
            }

            try
            {
                OnInitialize();

                //Create the chunks if not already present.
                var success = ChunkProcessor.CreateChunks(createChunks);
                //If locked, exit instance immediately.
                if (success != ProcessingStatus.Locked.ToString())
                {
                    while (success == ProcessingStatus.Processing.ToString())
                    {
                        //Poll after a certain time.
                        Thread.Sleep(500);//Arbitrary
                        success = ChunkProcessor.GetChunkCreationStatus();
                    }

                    if (success == ProcessingStatus.Failed.ToString())
                    {
                    }

                    if (success == ProcessingStatus.Completed.ToString())
                    {
                        ProcessChunks();
                    }

                    OnComplete();
                }
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
        /// The core method
        /// </summary>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="recordStartIndex">Start index of the record.</param>
        /// <param name="recordEndIndex">End index of the record.</param>
        public void ExecuteGatedJob(long chunkSize, long recordStartIndex, long recordEndIndex)
        {
            if (chunkSize == 0 || recordEndIndex == 0)
            {
                throw new InvalidDataException(BatchConstants.MessageInvalidChunkIndex);
            }

            try
            {
                OnInitialize();

                //Create the chunks if not already present.
                var success = ChunkProcessor.CreateChunks(chunkSize, recordStartIndex, recordEndIndex);

                //TODO: This might need a change.
                while (success == ProcessingStatus.Processing.ToString())
                {
                    //Poll after a certain time.
                    Thread.Sleep(500);//Arbitrary
                    success = ChunkProcessor.GetChunkCreationStatus();
                }

                if (success == ProcessingStatus.Failed.ToString())
                {
                }

                if (success == ProcessingStatus.Completed.ToString())
                {
                    ProcessChunks();
                }

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

        #endregion

        #region Private methods

        /// <summary>
        /// Processes the chunks.
        /// </summary>
        private void ProcessChunks()
        {
            //Get the next chunk to process
            while (ChunkProcessor.TryAssignNextChunk() && JobProcessor.GateBatch())
            {
                try
                {
                    Task.Run();
                    if (JobStatistics.FailureCount > 0 && JobStatistics.SuccessCount == 0)
                    {
                        //If none were successful.
                        ChunkProcessor.TryMarkChunkFailed();
                    }
                    else if (JobStatistics.FailureCount > 0 && JobStatistics.SuccessCount > 0)
                    {
                        //If some were successful.
                        ChunkProcessor.TryMarkChunkCompletedWithErrors();
                    }
                    else
                    {
                        //  If all were successful
                        ChunkProcessor.TryMarkChunkCompleted();
                    }
                }
                //In case an unhandled exception happens while running the tasks for a chunk
                //Based on the continuation flag, we continue or rethrow the exception
                catch (Exception ex)
                {
                    if (JobStatistics.ContinueChunkProcessingOnError)
                    {
                        JobStatistics.AddBatchError(new BatchException
                            {
                                Exception = ex,
                                JobChunkId = CurrentChunk.JobChunkId
                            });

                        ChunkProcessor.TryMarkChunkFailed();
                        continue;
                    }

                    throw;
                }
                finally
                {
                    JobLogger.LogBatchException();
                    JobProcessor.UpdateBatchStatistics(JobStatus.Processing);
                    JobStatistics.FailureCount = 0;
                    JobStatistics.SuccessCount = 0;
                }
            }
        }

        #endregion
    }
}