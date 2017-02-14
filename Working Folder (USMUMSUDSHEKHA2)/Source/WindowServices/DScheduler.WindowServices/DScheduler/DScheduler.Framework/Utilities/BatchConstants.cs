namespace DScheduler.Framework
{
    internal static class BatchConstants
    {
        #region Procedures

        internal const string ProcedureGateBatch = @"";
        internal const string ProcedureGetChunkCreationStatus = @"BatchJob.uspGetChunkCreationStatus";
        internal const string ProcedureGetLastBatchStatus = @"BatchJob.uspGetLastBatchRunStatus";
        internal const string ProcedureGetNextChunkToProcess = @"BatchJob.uspGetNextChunkToProcess";
        internal const string ProcedureInsertBatchJobException = "BatchJob.uspInsertBatchJobException";
        internal const string ProcedureInsertChunkData = @"BatchJob.uspInsertChunkData";
        internal const string ProcedureInsertJobException = "BatchJob.uspInsertJobException";
        internal const string ProcedureInsertJobSummary = "BatchJob.uspInsertJobSummary";
        internal const string ProcedureInsertNewBatchSubStepStatistics = @"";
        internal const string ProcedureMarkBatchGated = @"";
        internal const string ProcedureUpdateChunkInformation = @"BatchJob.uspUpdateChunkInformation";
        internal const string ProcedureReleaseCreationLock = @"BatchJob.uspReleaseCreationLock";
        internal const string ProcedureRestartBatchInstance = @"BatchJob.uspRestartBatchInstance";
        internal const string ProcedureSetCreationLock = @"BatchJob.uspSetCreationLock";
        internal const string ProcedureStartNewBatchInstance = @"BatchJob.uspStartNewBatchInstance";
        internal const string ProcedureUpdateCurrentBatchJobStatistics = @"BatchJob.uspUpdateBatchJobStatistics";
        internal const string ProcedureGetUnprocessedChunks = @"BatchJob.uspGetUnprocessedChunks";
        internal const string ProcedureGetBatchUserId = @"BatchJob.uspGetBatchUserId";
        internal const string ProcedureGetChunkModificationLock = @"BatchJob.uspGetChunkModificationLock";
        internal const string ProcedureAllocateUnprocessedChunks = @"BatchJob.uspAllocateUnprocessedChunks";

        #endregion Procedures

        #region Other constants

        internal const string ChunkStatusFailed = "Failed";
        internal const string ChunkStatusCompleted = "Completed";
        internal const string ChunkStatusCompletedWithErrors = "CompletedWithErrors";
        internal const string ConnectionBatchJob = "default";
        internal const string ConnectionBatchJobLogging = "BatchLoggingConnString";
        internal const string KeyBatchLogFilter = "BatchLogFilter";
        internal const string MessageChunkCreationBatchFailed = "Chunk creation has failed. Please run the chunk creation batch again.";
        internal const string MessageChunkNotCreated = "Chunk processing has not been done for this run. Please run the chunk processing step before running this step or use a different overload for this method.";
        internal const string MessageInvalidChunkCreationMethod = "A chunk creation method needs to be passed through if using this overload. If chunking is not intended, inherit from the job class or call ExecuteGatedJob with no parameters if chunking was done before in a separate batch.";
        internal const string MessageInvalidChunkIndex = "Chunk size or Record End Index cannot be 0.";
        internal const string MessageChunkCreationMethodCallFailed = "The supplied JobCode to call this method should match the JobCode for this step run. A JobStepCode needs to be supplied.";
        internal const string MessageContinuePreviousInstanceRun1 = "Note: This job has been run with JobParameters.ContinuePreviousInstanceRun as true.";
        internal const string MessageContinuePreviousInstanceRun2 = "It would have exited if no previous batch is present in processing state with pending chunks to be processed.";
        internal const string MessageContinuePreviousInstanceRun3 = "Turn this flag off if the previous batch has completed.";

        #endregion Other constants

        #region Properties and SQL Variables

        internal const string PropertyAdditionalInformation = "AdditionalInformation";
        internal const string PropertyBatchRunId = "BatchRunId";
        internal const string PropertyChunkEndPosition = "ChunkEndPosition";
        internal const string PropertyChunkStartPosition = "ChunkStartPosition";
        internal const string PropertyCreateDate = "CreateDate";
        internal const string PropertyCreateUserId = "CreateUserId";
        internal const string PropertyExceptionDetails = "ExceptionDetails";
        internal const string PropertyExceptionSummary = "ExceptionSummary";
        internal const string PropertyExceptionTime = "ExceptionTime";
        internal const string PropertyExceptionType = "ExceptionType";
        internal const string PropertyInstanceId = "InstanceId";
        internal const string PropertyJobChunkId = "JobChunkId";
        internal const string PropertyJobCode = "JobCode";
        internal const string PropertyJobExceptionId = "JobExceptionId";
        internal const string PropertyJobRunId = "JobRunId";
        internal const string PropertyJobStepCode = "JobStepCode";
        internal const string PropertyKeyIdentifier = "KeyIdentifier";
        internal const string PropertyKeyValue = "KeyValue";
        internal const string PropertyProcessingStatus = "ProcessingStatus";
        internal const string PropertyRunId = "RunId";
        internal const string PropertySourceName = "SourceName";
        internal const string PropertyBatchUserId = "BatchUserId";
        internal const string PropertyIsSingleInstanced = "IsSingleInstanced";
        internal const string VariableAdditionalInformation = "@AdditionalInformation";
        internal const string VariableBatchRunId = "@BatchRunId";
        internal const string VariableChunkDetails = "@ChunkDetails";
        internal const string VariableEndDate = "@EndDate";
        internal const string VariableFailureCount = "@FailureCount";
        internal const string VariableInstanceId = "@InstanceId";
        internal const string VariableJobChunkId = "@JobChunkId";
        internal const string VariableJobCode = "@JobCode";
        internal const string VariableJobException = "@JobException";
        internal const string VariableJobRunId = "@JobRunId";
        internal const string VariableJobStepCode = "@JobStepCode";
        internal const string VariableJobStepSummaryAdditionalInformation = "@JobStepSummaryAdditionalInformation";
        internal const string VariableProcessingStatus = "@ProcessingStatus";
        internal const string VariableRunDate = "@RunDate";
        internal const string VariableSuccessCount = "@SuccessCount";
        internal const string VariableTotalCount = "@TotalCount";
        internal const string VariableOtherCount = "@OtherCount";

        #endregion Properties and SQL Variables
    }
}