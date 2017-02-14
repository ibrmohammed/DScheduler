using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public interface IStatusInfo
    {
        /// <summary>
        ///
        /// </summary>
        string Details { get; }

        /// <summary>
        ///
        /// </summary>
        StatusType Status { get; }
    }
}