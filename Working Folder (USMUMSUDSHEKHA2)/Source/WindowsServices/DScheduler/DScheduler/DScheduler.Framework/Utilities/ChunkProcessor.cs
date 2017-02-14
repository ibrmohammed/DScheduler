using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DScheduler.Framework.Helpers;




namespace DScheduler.Framework
{
    /// <summary>
    /// Helper class for gated Jobs
    /// </summary>
    public static class ChunkProcessor
    {
        #region Public methods

        /// <summary>
        /// Persists the chunk information to the database and marks chunk creation as completed or failed for the
        /// supplied JobCode and JobStepCode for the current JobRunId.
        /// </summary>
        /// <param name="chunkInformations">The chunk informations.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">The supplied JobCode to call this method should match the JobCode for this step run. A JobStepCode needs to be supplied.</exception>
        public static bool PersistChunkInformation(IEnumerable<ChunkInformation> chunkInformations, string jobCode, string jobStepCode)
        {
            //Check if the chunk creation has been completed or going on.
            if (JobStatistics.JobCode == jobCode && !string.IsNullOrEmpty(jobStepCode))
            {
                var creationStatus = GetChunkCreationStatus();
                if (string.IsNullOrEmpty(creationStatus))
                {
                    //Start data creation
                    creationStatus = StartChunkCreation(jobCode, jobStepCode);
                    if (creationStatus == ProcessingStatus.Processing.ToString())
                    {
                        //Put the chunk information into the table
                        var success = PopulateChunkInformation(chunkInformations, jobCode, jobStepCode);
                        creationStatus = EndChunkCreation(success ? ProcessingStatus.Completed.ToString() : ProcessingStatus.Failed.ToString(), jobCode, jobStepCode);
                    }
                    return creationStatus == ProcessingStatus.Completed.ToString();
                }
                return creationStatus == ProcessingStatus.Completed.ToString();
            }
            throw new InvalidOperationException(BatchConstants.MessageChunkCreationMethodCallFailed);
        }

        /// <summary>
        /// Gets the unprocessed/processing chunk count.
        /// </summary>
        /// <returns></returns>
        public static int GetUnprocessedChunkCount()
        {
            return GetUnprocessedChunks().Count();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Creates the initial chunks for batch consumption.
        /// Use this overload if chunks need to be created based on custom business logic.
        /// </summary>
        /// <param name="getChunkInformation">A method which creates the chunk information.</param>
        /// <returns></returns>
        internal static string CreateChunks(Func<IEnumerable<ChunkInformation>> getChunkInformation)
        {
            //If JobParameters.RestartPreviousSingleInstanceRun is true,
            //Get the RunId and replace the InstanceId set for the previous run to the current one.
            var creationStatus = string.Empty;
            if (JobParameters.ContinuePreviousInstanceRun)
            {
                creationStatus = PrepareForPreviousRunContinuation();
            }
            else
            {
                //Check if the chunk creation has been completed or going on.
                creationStatus = GetChunkCreationStatus();
                if (string.IsNullOrEmpty(creationStatus))
                {
                    //Start data creation
                    creationStatus = StartChunkCreation();
                    if (creationStatus == ProcessingStatus.Processing.ToString())
                    {
                        var chunkInformations = getChunkInformation.Invoke().ToList();
                        //Put the chunk information into the table
                        var success = PopulateChunkInformation(chunkInformations);
                        creationStatus = EndChunkCreation(success ? ProcessingStatus.Completed.ToString() : ProcessingStatus.Failed.ToString());
                    }
                    else if (creationStatus == ProcessingStatus.Locked.ToString())
                    {
                        //To avoid conflicts with the single instancing change.
                        creationStatus = ProcessingStatus.Processing.ToString();
                    }
                    return creationStatus;
                }
            }
            return creationStatus;
        }

        /// <summary>
        /// Creates the initial chunks for batch consumption.
        /// Use this overload if the batch requires fixed width chunks.
        /// </summary>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="recordStartIndex">Start index of the total records to be processed.</param>
        /// <param name="recordEndIndex">End index of the total records to be processed.</param>
        /// <returns></returns>
        internal static string CreateChunks(long chunkSize, long recordStartIndex, long recordEndIndex)
        {
            //If chunk size passed is 0, get it from the master data table.
            if (chunkSize == 0)
            {
                chunkSize = JobParameters.DefaultChunkSize;
                if (chunkSize == 0)
                {
                    throw new InvalidDataException("The chunk size supplied is invalid. Please supply a finite chunk size greater than 0.");
                }
            }
            //Check if the chunk creation has been completed or going on.
            var creationStatus = GetChunkCreationStatus();
            if (string.IsNullOrEmpty(creationStatus))
            {
                //Start data creation
                creationStatus = StartChunkCreation();
                if (creationStatus != ProcessingStatus.Completed.ToString())
                {
                    var chunkInformations = CreateFixedSizeChunks(chunkSize, recordStartIndex, recordEndIndex);
                    //Put the chunk information into the table
                    var success = PopulateChunkInformation(chunkInformations);
                    creationStatus = EndChunkCreation(success ? ProcessingStatus.Completed.ToString() : ProcessingStatus.Processing.ToString());
                }
                return creationStatus;
            }
            return creationStatus;
        }

        /// <summary>
        /// Gets the chunk creation status. Will return null or empty if nothing has started yet.
        /// </summary>
        /// <returns></returns>
        internal static string GetChunkCreationStatus()
        {
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>> 
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId)
                };
            if (!string.IsNullOrEmpty(JobStatistics.JobStepCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
            }
            var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetChunkCreationStatus, Job.ConnectionStringName, true, parameters.ToArray());
            string status = null;
            if (data.Rows.Count > 0)
            {
                JobStatistics.RunId = data.Rows[0].Field<Guid>(BatchConstants.PropertyJobRunId);
                status = data.Rows[0].Field<string>(BatchConstants.PropertyProcessingStatus);
            }
            return status;
        }

        /// <summary>
        /// Tries the assignment of the next available chunk for processing. Returns false if no chunk is available.
        /// </summary>
        /// <returns></returns>
        internal static bool TryAssignNextChunk()
        {
            //Get the next chunk to process//The proc updates the status to Active before returning the data
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableInstanceId, JobStatistics.InstanceId)
                };
            if (!string.IsNullOrEmpty(JobStatistics.JobStepCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
            }
            var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetNextChunkToProcess, Job.ConnectionStringName, true, parameters.ToArray())
                      .ToEntities(ConvertToChunkInformation).FirstOrDefault();//This anyway returns one record always
            GatedJob.CurrentChunk = data;
            return data != null;
        }

        /// <summary>
        /// Tries the mark chunk completed.
        /// </summary>
        /// <returns></returns>
        internal static bool TryMarkChunkCompleted()
        {
            //Get the next chunk to process//The proc updates the status to Active before returning the data
            var helper = new SqlClientHelper();
            var parameters = new[]
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobChunkId, GatedJob.CurrentChunk.JobChunkId),
                    new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, BatchConstants.ChunkStatusCompleted),
                    new KeyValuePair<string, object>(BatchConstants.VariableAdditionalInformation, GatedJob.CurrentChunk.AdditionalInformation)
                };
            var data = helper.GetScalarByProcedure(BatchConstants.ProcedureUpdateChunkInformation, Job.ConnectionStringName, true, parameters);
            GatedJob.CurrentChunk = null;
            return (string)data == ProcessingStatus.Completed.ToString();
        }

        /// <summary>
        /// Tries the mark the chunk failed.
        /// </summary>
        /// <returns></returns>
        internal static bool TryMarkChunkFailed()
        {
            //Get the next chunk to process//The proc updates the status to Active before returning the data
            var helper = new SqlClientHelper();
            var parameters = new[]
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobChunkId, GatedJob.CurrentChunk.JobChunkId),
                    new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, BatchConstants.ChunkStatusFailed),
                    new KeyValuePair<string, object>(BatchConstants.VariableAdditionalInformation, GatedJob.CurrentChunk.AdditionalInformation)
                };
            var data = helper.GetScalarByProcedure(BatchConstants.ProcedureUpdateChunkInformation, Job.ConnectionStringName, true, parameters);
            //JobStatistics.FailureCount += GatedJob.CurrentChunk.ChunkEndPosition - GatedJob.CurrentChunk.ChunkStartPosition;
            GatedJob.CurrentChunk = null;
            return (string)data == ProcessingStatus.Failed.ToString();
        }

        /// <summary>
        /// Tries the mark chunk completed with errors.
        /// </summary>
        /// <returns></returns>
        internal static bool TryMarkChunkCompletedWithErrors()
        {
            //Get the next chunk to process//The proc updates the status to Active before returning the data
            var helper = new SqlClientHelper();
            var parameters = new[]
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobChunkId, GatedJob.CurrentChunk.JobChunkId),
                    new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, BatchConstants.ChunkStatusCompletedWithErrors),
                    new KeyValuePair<string, object>(BatchConstants.VariableAdditionalInformation, GatedJob.CurrentChunk.AdditionalInformation)
                };
            var data = helper.GetScalarByProcedure(BatchConstants.ProcedureUpdateChunkInformation, Job.ConnectionStringName, true, parameters);
            GatedJob.CurrentChunk = null;
            return (string)data == ProcessingStatus.Failed.ToString();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Prepares the previous run for continuation.
        /// Resets the chunk information to be able to pickup new/incomplete chunks.
        /// </summary>
        /// <returns></returns>
        private static string PrepareForPreviousRunContinuation()
        {
            //Lock chunk manipulation.
            var lockStatus = GetChunkModificationLock(JobStatistics.JobCode, JobStatistics.JobStepCode);

            if (lockStatus == ProcessingStatus.Processing.ToString())
            {
                var helper = new SqlClientHelper();
                var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableInstanceId, JobStatistics.InstanceId)
                };
                if (!string.IsNullOrEmpty(JobStatistics.JobStepCode))
                {
                    parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
                }
                var data = (string)helper.GetScalarByProcedure(BatchConstants.ProcedureAllocateUnprocessedChunks,
                                                          Job.ConnectionStringName, true, parameters.ToArray());

                lockStatus = EndChunkCreation(ProcessingStatus.Completed.ToString());
                return ProcessingStatus.Completed.ToString();
            }
            else
            {
                return ProcessingStatus.Locked.ToString();
            }
        }

        /// <summary>
        /// Takes a data row and returns a DataSegmentInformation object
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private static ChunkInformation ConvertToChunkInformation(DataRow row)
        {
            return new ChunkInformation
            {
                ProcessingStatus = row.Field<string>(BatchConstants.PropertyProcessingStatus),
                ChunkEndPosition = row.Field<long>(BatchConstants.PropertyChunkEndPosition),
                ChunkStartPosition = row.Field<long>(BatchConstants.PropertyChunkStartPosition),
                JobChunkId = row.Field<int>(BatchConstants.PropertyJobChunkId),
                AdditionalInformation = row.Field<string>(BatchConstants.PropertyAdditionalInformation),
                SourceName = row.Field<string>(BatchConstants.PropertySourceName)
            };
        }

        /// <summary>
        /// Creates the fixed size chunks.
        /// </summary>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="recordStartIndex">Start index of the record.</param>
        /// <param name="recordEndIndex">End index of the record.</param>
        /// <returns></returns>
        private static IEnumerable<ChunkInformation> CreateFixedSizeChunks(long chunkSize, long recordStartIndex, long recordEndIndex)
        {
            var totalRecords = recordEndIndex - recordStartIndex + 1;//Index always starts at 1 and not 0.
            var totalFixedWidthChunks = totalRecords / chunkSize;
            var fixedChunkEndPosition = (chunkSize * totalFixedWidthChunks) + recordStartIndex - 1;
            var chunks = new List<ChunkInformation>();

            //Create the fixed width chunks.
            for (var i = recordStartIndex; i < fixedChunkEndPosition; i = i + chunkSize)
            {
                var chunk = new ChunkInformation
                    {
                        ChunkStartPosition = i,
                        ChunkEndPosition = i + chunkSize - 1
                    };
                chunks.Add(chunk);
            }

            //Add the last chunk if there is some records left.
            var lastChunkSize = totalRecords % chunkSize;
            if (lastChunkSize > 0)
            {
                var lastChunk = new ChunkInformation
                    {
                        ChunkStartPosition = fixedChunkEndPosition + 1,
                        ChunkEndPosition = fixedChunkEndPosition + lastChunkSize
                    };
                chunks.Add(lastChunk);
            }
            if (chunkSize == 1)
            {
                var lastChunk = new ChunkInformation
                {
                    ChunkStartPosition = fixedChunkEndPosition,
                    ChunkEndPosition = fixedChunkEndPosition
                };
                chunks.Add(lastChunk);
            }

            return chunks;
        }

        /// <summary>
        /// Gets the data segment information table.
        /// </summary>
        /// <returns></returns>
        private static DataTable GetChunkInformationDataTable()
        {
            var usrChunkInformation = new DataTable();
            usrChunkInformation.Columns.Add(BatchConstants.PropertyJobCode, typeof(string));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyJobStepCode, typeof(string));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyJobRunId, typeof(Guid));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyChunkStartPosition, typeof(int));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyChunkEndPosition, typeof(int));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyProcessingStatus, typeof(string));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyInstanceId, typeof(Guid));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyAdditionalInformation, typeof(string));
            usrChunkInformation.Columns.Add(BatchConstants.PropertySourceName, typeof(string));
            usrChunkInformation.Columns.Add(BatchConstants.PropertyIsSingleInstanced, typeof(bool));
            return usrChunkInformation;
        }

        /// <summary>
        /// Inserts the chunk information.
        /// </summary>
        /// <param name="chunkInformations">A list of ChunkInformation metadata objects.</param>
        /// <returns></returns>
        private static bool PopulateChunkInformation(IEnumerable<ChunkInformation> chunkInformations)
        {
            try
            {
                bool isSingleInstanced = false;
                if (chunkInformations.Any(e => e.IsSingleInstanced))
                {
                    isSingleInstanced = true;
                }
                //Implement code to insert information into a database table. Table to be finalized.
                var helper = new SqlClientHelper();
                var udtChunkInformation = GetChunkInformationDataTable();
                chunkInformations.ToList().ForEach(information =>
                {
                    var row = udtChunkInformation.NewRow();
                    row[BatchConstants.PropertyJobCode] = JobStatistics.JobCode;
                    row[BatchConstants.PropertyJobStepCode] = JobStatistics.JobStepCode;
                    row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
                    row[BatchConstants.PropertyChunkStartPosition] = information.ChunkStartPosition;
                    row[BatchConstants.PropertyChunkEndPosition] = information.ChunkEndPosition;
                    row[BatchConstants.PropertyProcessingStatus] = ProcessingStatus.New.ToString();
                    row[BatchConstants.PropertyInstanceId] = JobStatistics.InstanceId;
                    row[BatchConstants.PropertyAdditionalInformation] = information.AdditionalInformation;
                    row[BatchConstants.PropertySourceName] = information.SourceName;
                    row[BatchConstants.PropertyIsSingleInstanced] = isSingleInstanced;
                    udtChunkInformation.Rows.Add(row);
                });
                var parameters = new[]
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableChunkDetails, udtChunkInformation),
                        new KeyValuePair<string, object>(BatchConstants.VariableInstanceId, JobStatistics.InstanceId)
                    };
                helper.RunProcedure(BatchConstants.ProcedureInsertChunkData, Job.ConnectionStringName, true, parameters);
                return true;
            }
            catch (SqlException ex)
            {
                //At a chunk information level, if duplicate chunks were passed.
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    throw;
                }

                //If some other error, it might be transient, consume exception and return false.
                var logger = new BatchLogger();
                logger.LogBatchException(ex);
                return false;
            }
        }

        /// <summary>
        /// Populates the chunk information.
        /// </summary>
        /// <param name="chunkInformations">The chunk informations.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <returns></returns>
        private static bool PopulateChunkInformation(IEnumerable<ChunkInformation> chunkInformations, string jobCode, string jobStepCode)
        {
            try
            {
                bool isSingleInstanced = false;
                if (chunkInformations.Any(e => e.IsSingleInstanced))
                {
                    isSingleInstanced = true;
                }
                //Implement code to insert information into a database table. Table to be finalized.
                var helper = new SqlClientHelper();
                var udtChunkInformation = GetChunkInformationDataTable();
                chunkInformations.ToList().ForEach(information =>
                {
                    var row = udtChunkInformation.NewRow();
                    row[BatchConstants.PropertyJobCode] = jobCode;
                    row[BatchConstants.PropertyJobStepCode] = jobStepCode;
                    row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
                    row[BatchConstants.PropertyChunkStartPosition] = information.ChunkStartPosition;
                    row[BatchConstants.PropertyChunkEndPosition] = information.ChunkEndPosition;
                    row[BatchConstants.PropertyProcessingStatus] = ProcessingStatus.New.ToString();
                    row[BatchConstants.PropertyInstanceId] = JobStatistics.InstanceId;
                    row[BatchConstants.PropertyAdditionalInformation] = information.AdditionalInformation;
                    row[BatchConstants.PropertySourceName] = information.SourceName;
                    row[BatchConstants.PropertyIsSingleInstanced] = isSingleInstanced;
                    udtChunkInformation.Rows.Add(row);
                });
                var parameters = new[]
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableChunkDetails, udtChunkInformation),
                        new KeyValuePair<string, object>(BatchConstants.VariableInstanceId, JobStatistics.InstanceId)
                    };
                helper.RunProcedure(BatchConstants.ProcedureInsertChunkData, Job.ConnectionStringName, true, parameters);
                return true;
            }
            catch (SqlException ex)
            {
                //At a chunk information level, if duplicate chunks were passed.
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    throw;
                }

                //If some other error, it might be transient, consume exception and return false.
                var logger = new BatchLogger();
                logger.LogBatchException(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the unprocessed chunks.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<ChunkInformation> GetUnprocessedChunks()
        {
            var helper = new SqlClientHelper();
            var parameters = new[]
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId)
                    };
            var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetUnprocessedChunks,
                                                      Job.ConnectionStringName, true, parameters)
                             .ToEntities(ConvertToChunkInformation);
            return data;
        }

        /// <summary>
        /// Starts the chunk creation process. Returns the RunId and the status for the batch instance to use.
        /// </summary>
        /// <returns></returns>
        private static string StartChunkCreation()
        {
            return StartChunkCreation(JobStatistics.JobCode, JobStatistics.JobStepCode);
        }

        /// <summary>
        /// Starts the chunk creation process. Returns the RunId and the status for the batch instance to use.
        /// </summary>
        /// <returns></returns>
        private static string StartChunkCreation(string jobCode, string jobStepCode)
        {
            var helper = new SqlClientHelper();
            try
            {
                var parameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableJobCode, jobCode),
                        new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                        new KeyValuePair<string, object>(BatchConstants.VariableInstanceId, JobStatistics.InstanceId)
                    };
                if (!string.IsNullOrEmpty(jobStepCode))
                {
                    parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, jobStepCode));
                }
                //Try and set the master lock.
                var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureSetCreationLock, Job.ConnectionStringName, true, parameters.ToArray());
                string status = null;
                var processingInstanceId = Guid.Empty;
                if (data.Rows.Count > 0)
                {
                    status = data.Rows[0].Field<string>(BatchConstants.PropertyProcessingStatus);
                    processingInstanceId = data.Rows[0].Field<Guid>(BatchConstants.PropertyInstanceId);
                }
                //If status is processing, the instance id should match, else set it as locked
                return processingInstanceId == JobStatistics.InstanceId
                    ? status
                    : status == ProcessingStatus.Processing.ToString() ? ProcessingStatus.Locked.ToString() : status;
            }
            catch (SqlException ex)//In a situation where two instances try and create the same row.
            {
                //In a case where two instances might be trying to start chunk creation for the same RunId/JobCode/JobStepCode combination
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    return GetChunkCreationStatus();
                }
                throw;
            }
        }

        /// <summary>
        /// Ends the chunk creation.
        /// </summary>
        /// <param name="processingStatus">The processing status.</param>
        /// <returns></returns>
        private static string EndChunkCreation(string processingStatus)
        {
            return EndChunkCreation(processingStatus, JobStatistics.JobCode, JobStatistics.JobStepCode);
        }

        /// <summary>
        /// Marks the chunk creation process as completed/failed.
        /// </summary>
        /// <param name="processingStatus">The processing status.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <returns></returns>
        private static string EndChunkCreation(string processingStatus, string jobCode, string jobStepCode)
        {
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, processingStatus),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, jobCode)
                };
            if (!string.IsNullOrEmpty(jobStepCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, jobStepCode));
            }
            var creationStatus = (string)helper.GetScalarByProcedure(BatchConstants.ProcedureReleaseCreationLock, Job.ConnectionStringName, true, parameters.ToArray());
            return creationStatus;
        }

        /// <summary>
        /// Gets the chunk modification lock.
        /// </summary>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <returns></returns>
        private static string GetChunkModificationLock(string jobCode, string jobStepCode)
        {
            var helper = new SqlClientHelper();
            try
            {
                var parameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableJobCode, jobCode),
                        new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId)
                    };
                if (!string.IsNullOrEmpty(jobStepCode))
                {
                    parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, jobStepCode));
                }
                var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetChunkModificationLock,
                                                          Job.ConnectionStringName, true, parameters.ToArray());
                string status = null;
                if (data.Rows.Count > 0)
                {
                    //JobStatistics.RunId = data.Rows[0].Field<Guid>(BatchConstants.PropertyJobRunId);
                    status = data.Rows[0].Field<string>(BatchConstants.PropertyProcessingStatus);
                }
                return status;
            }
            catch (SqlException ex)//In a situation where two instances try and create the same row.
            {
                //In a case where two instances might be trying to start chunk creation for the same RunId/JobCode/JobStepCode combination
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    return GetChunkCreationStatus();
                }
                throw;
            }
        }

        /// <summary>
        /// Releases the chunk modification lock.
        /// </summary>
        /// <param name="processingStatus">The processing status.</param>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <returns></returns>
        private static string ReleaseChunkModificationLock(string processingStatus, string jobCode, string jobStepCode)
        {
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, processingStatus),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, jobCode)
                };
            if (!string.IsNullOrEmpty(jobStepCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, jobStepCode));
            }
            var creationStatus = (string)helper.GetScalarByProcedure(BatchConstants.ProcedureReleaseCreationLock, Job.ConnectionStringName, true, parameters.ToArray());
            return creationStatus;
        }

        #endregion
    }
}
