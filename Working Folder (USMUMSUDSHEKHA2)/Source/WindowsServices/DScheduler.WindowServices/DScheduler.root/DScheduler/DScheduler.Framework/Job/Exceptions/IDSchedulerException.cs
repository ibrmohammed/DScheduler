using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Framework
{
    public interface IDSchedulerException
    {
        /// <summary>
        /// CREATED_BY
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// CREATED_DATE
        /// </summary>
        DateTime CreatedDate { get; }

        /// <summary>
        /// EXCEPTION_DETAILS
        /// </summary>
        string ExceptionDetails { get; }

        /// <summary>
        /// EXCEPTION_SUMMARY
        /// </summary>
        string ExceptionSummary { get; }

        /// <summary>
        /// EXCEPTION_TIME
        /// </summary>
        DateTime ExceptionTime { get; }

        /// <summary>
        /// EXCEPTION_TYPE
        /// </summary>
        string ExceptionType { get; }

        /// <summary>
        /// INTERFACE_CODE
        /// </summary>
        string InterfaceCode { get; }

        /// <summary>
        /// JOB_ID
        /// </summary>
        Guid JobId { get; }

        /// <summary>
        /// KEY_IDENTIFIER
        /// </summary>
        string KeyIdentifier { get; }

        /// <summary>
        /// KEY_VALUE
        /// </summary>
        string KeyValue { get; }

        /// <summary>
        /// PROCESS_DATE
        /// </summary>
        DateTime ProcessDate { get; }

        /// <summary>
        /// RAW_ERROR_MESSAGE
        /// </summary>
        string RawErrorMessage { get; }

        /// <summary>
        /// RECORD_NUM
        /// </summary>
        string RecordNum { get; }

        /// <summary>
        /// RECORD_VALUE
        /// </summary>
        string RecordValue { get; }

        /// <summary>
        /// RUN_NUM
        /// </summary>
        int RunNum { get; }

        /// <summary>
        /// SSN
        /// </summary>
        int Ssn { get; }

        /// <summary>
        /// UPDATED_BY
        /// </summary>
        string UpdatedBy { get; }

        /// <summary>
        /// UPDATED_DATE
        /// </summary>
        DateTime UpdatedDate { get; }
    }
}

