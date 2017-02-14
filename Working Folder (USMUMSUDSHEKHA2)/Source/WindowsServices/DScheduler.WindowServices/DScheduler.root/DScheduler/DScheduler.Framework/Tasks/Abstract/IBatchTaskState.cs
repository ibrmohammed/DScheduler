namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public interface IBatchTaskState : ITaskState
    {
        /// <summary>
        ///
        /// </summary>
        int CurrentPosition { get; set; }
    }
}