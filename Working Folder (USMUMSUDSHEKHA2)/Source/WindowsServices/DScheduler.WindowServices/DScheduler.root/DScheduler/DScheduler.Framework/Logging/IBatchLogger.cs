using System;
using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    /// Interface to log all batch exceptions.
    /// </summary>
    public interface IBatchLogger
    {
        /// <summary>
        /// Logs the batch exception.
        /// </summary>
        void LogBatchException();

        /// <summary>
        /// Logs the batch message.
        /// </summary>
        /// <param name="exception">The message.</param>
        void LogBatchException(Exception exception);

        /// <summary>
        /// Logs the batch information.
        /// </summary>
        /// <param name="batchError"></param>
        void LogBatchException(BatchError batchError);

        /// <summary>
        /// Logs the batch information.
        /// </summary>
        /// <param name="batchInformation">The batch information.</param>
        void LogBatchInformation(BatchInformation batchInformation);

        /// <summary>
        /// Logs the batch exception.
        /// </summary>
        /// <param name="exceptions">The exceptions.</param>
        void LogBatchException(IEnumerable<BatchException> exceptions);
    }
}
