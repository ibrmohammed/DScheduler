namespace DScheduler.Framework
{
    /// <summary>
    /// The smallest unit in a batch job.
    /// </summary>
    public interface ITask : ITaskError
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        void Run();
    }
}