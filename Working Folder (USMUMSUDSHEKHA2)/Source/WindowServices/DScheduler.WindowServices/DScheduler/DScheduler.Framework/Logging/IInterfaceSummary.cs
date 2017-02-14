using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public interface IInterfaceSummary
    {
        /// <summary>
        /// INTERFACE_CODE
        /// </summary>
        string InterfaceCode { get; }

        /// <summary>
        /// INTERFACE_PROCESSING_SUMMARY_ID
        /// </summary>
        int InterfaceProcessingSummaryId { get; }

        /// <summary>
        /// JOB_ID
        /// </summary>
        string JobId { get; }

        /// <summary>
        /// JOB_SUMMARY
        /// </summary>
        string JobSummary { get; }

        /// <summary>
        /// RUN_DATE
        /// </summary>
        DateTime RunDate { get; }
    }
}