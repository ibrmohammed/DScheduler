using System;

namespace DScheduler.Framework
{
    /// <summary>
    /// Interface to handle errors from task.
    /// </summary>
    public interface ITaskError
    {
        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        event EventHandler TaskException;
    }
}