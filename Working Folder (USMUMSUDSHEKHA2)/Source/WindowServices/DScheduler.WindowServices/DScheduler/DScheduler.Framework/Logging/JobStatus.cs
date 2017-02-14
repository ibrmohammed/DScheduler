namespace DScheduler.Framework
{
    /// <summary>
    /// Job level statuses
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// Indicates a job has been scheduled for running but not yet running.
        /// </summary>
        New,

        /// <summary>
        /// Indicates that the job is running right now.
        /// </summary>
        Processing,

        /// <summary>
        /// Indicates that the batch completed successfully.
        /// </summary>
        Completed,

        /// <summary>
        /// Indicates that the batch completed with some errors. See JobException table for details.
        /// </summary>
        CompletedWithErrors,

        /// <summary>
        /// Indicates that the batch failed completely. See JobException table for details.
        /// </summary>
        Failed,

        /// <summary>
        /// Indicates the batch was aborted. All future step would not be allowed to run for this Run Id.
        /// </summary>
        Aborted,

        /// <summary>
        /// Indicates the job has been gated and would restart again at a later time.
        /// </summary>
        Gated
    }
}
