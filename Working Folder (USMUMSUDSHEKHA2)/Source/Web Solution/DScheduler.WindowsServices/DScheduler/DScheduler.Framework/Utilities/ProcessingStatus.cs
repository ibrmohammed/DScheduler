namespace DScheduler.Framework
{
    /// <summary>
    /// The different status a process might have.
    /// </summary>
    internal enum ProcessingStatus
    {
        New,
        Processing,
        Completed,
        Aborted,
        Gated,
        Failed,
        Locked
    }
}
