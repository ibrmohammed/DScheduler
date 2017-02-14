using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Linq;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    /// The standard Framework implementation of IJobStatusLogger.
    /// This does statistical logging to the BatchJob tables.
    /// </summary>
    public class BatchLogger : IBatchLogger
    {
        #region Properties

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        private static string CurrentUserId
        {
            get
            {
                var identityName = string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name)
                    ? Environment.UserName
                    : Thread.CurrentPrincipal.Identity.Name;
                return identityName;
            }
        }

        #endregion Properties

        #region IBatchLogger Members

        /// <summary>
        /// Logs the batch exceptions.
        /// </summary>
        public void LogBatchException()
        {
            if (JobStatistics.BatchExceptions.Any())
            {
                LogBatchException(JobStatistics.BatchExceptions);
                JobStatistics.ClearBatchErrors();
            }
        }

        /// <summary>
        /// Logs the batch exception.
        /// </summary>
        /// <param name="exceptions">The exceptions.</param>
        public void LogBatchException(IEnumerable<BatchException> exceptions)
        {
            var helper = new SqlClientHelper();
            var exList = exceptions.ToList();
            var normalExceptions = new List<BatchException>();

            foreach (var item in exList)
            {
                var aggregateException = item.Exception as AggregateException;
                if (aggregateException != null)
                {
                    foreach (var ex in aggregateException.Flatten().InnerExceptions)
                    {
                        normalExceptions.Add(new BatchException
                        {
                            Exception = ex,
                            IdentifierInformation = item.IdentifierInformation,
                            JobChunkId = item.JobChunkId
                        });

                        var innerEx = ex.InnerException;
                        while (innerEx != null)
                        {
                            normalExceptions.Add(new BatchException()
                            {
                                Exception = innerEx,
                                IdentifierInformation = item.IdentifierInformation,
                                JobChunkId = item.JobChunkId
                            });
                            innerEx = innerEx.InnerException;
                        }
                    }
                }
                else
                {
                    normalExceptions.Add(item); //Add the exception

                    //Add the inner exceptions as well.
                    var ex = item.Exception.InnerException;
                    while (ex != null)
                    {
                        normalExceptions.Add(new BatchException
                        {
                            Exception = ex,
                            IdentifierInformation = item.IdentifierInformation,
                            JobChunkId = item.JobChunkId
                        });
                        ex = ex.InnerException;
                    }
                }
            }
            var datatable = GetJobExceptionRow(normalExceptions);
            var parameters = new[] {new KeyValuePair<string, object>(BatchConstants.VariableJobException, datatable)};
            helper.RunProcedure(BatchConstants.ProcedureInsertBatchJobException,
                Job.LoggingConnectionStringName, true, parameters);
        }

        /// <summary>
        /// Logs the job message.
        /// </summary>
        /// <param name="exception">The message.</param>
        public void LogBatchException(Exception exception)
        {
            var helper = new SqlClientHelper();
            var exceptionList = new List<Exception>();
            var aggregateException = exception as AggregateException;
            //Log the aggregate message separately
            if (aggregateException != null)
            {
                foreach (var item in aggregateException.Flatten().InnerExceptions)
                {
                    exceptionList.Add(item);

                    var ex = item.InnerException;
                    while (ex != null)
                    {
                        exceptionList.Add(ex);
                        ex = ex.InnerException;
                    }
                }
            }
            else
            {
                exceptionList.Add(exception);

                var ex = exception.InnerException;
                while (ex != null)
                {
                    exceptionList.Add(ex);
                    ex = ex.InnerException;
                }
            }

            var datatable = GetJobExceptionRow(exceptionList);
            var parameters = new[] { new KeyValuePair<string, object>(BatchConstants.VariableJobException, datatable) };
            helper.RunProcedure(BatchConstants.ProcedureInsertBatchJobException, Job.LoggingConnectionStringName, true, parameters);
        }

        /// <summary>
        /// Logs the batch information.
        /// </summary>
        /// <param name="batchError"></param>
        public void LogBatchException(BatchError batchError)
        {
            IDatabaseHelper helper = new SqlClientHelper();
            var datatable = GetJobExceptionRow(batchError);

            var parameters = new[] { new KeyValuePair<string, object>(BatchConstants.VariableJobException, datatable) };
            helper.RunProcedure(BatchConstants.ProcedureInsertBatchJobException, BatchConstants.ConnectionBatchJobLogging, true, parameters);
        }

        /// <summary>
        /// Logs the batch information.
        /// </summary>
        /// <param name="batchInformation"></param>
        public virtual void LogBatchInformation(BatchInformation batchInformation)
        {
            var value = GetBatchLogFilter();

            if (value)
            {
                var helper = new SqlClientHelper();
                var dataTable = GetJobExceptionRow(batchInformation);
                var parameters = new[] { new KeyValuePair<string, object>(BatchConstants.VariableJobException, dataTable) };
                helper.RunProcedure(BatchConstants.ProcedureInsertBatchJobException, Job.LoggingConnectionStringName, true, parameters);
            }
        }

        #endregion IBatchLogger Members

        #region Private Methods

        /// <summary>
        /// Gets the batch log filter.
        /// </summary>
        /// <returns></returns>
        private static bool GetBatchLogFilter()
        {
            var batchLogFilter = new BatchLogFilter();
            var value = batchLogFilter.Filter(TraceEventType.Information);

            return value;
        }

        /// <summary>
        /// Gets the job exception data table.
        /// </summary>
        /// <returns></returns>
        private static DataTable GetJobExceptionDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(BatchConstants.PropertyJobExceptionId, typeof(long));
            dataTable.Columns.Add(BatchConstants.PropertyJobCode, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyJobStepCode, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyJobRunId, typeof(Guid));
            dataTable.Columns.Add(BatchConstants.PropertyExceptionType, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyExceptionSummary, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyExceptionDetails, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyExceptionTime, typeof(DateTime));
            dataTable.Columns.Add(BatchConstants.PropertyKeyIdentifier, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyKeyValue, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyCreateUserId, typeof(string));
            dataTable.Columns.Add(BatchConstants.PropertyCreateDate, typeof(DateTime));
            dataTable.Columns.Add(BatchConstants.PropertyJobChunkId, typeof(int));

            return dataTable;
        }

        /// <summary>
        /// Gets the job exception row.
        /// </summary>
        /// <param name="batchError">The batch error.</param>
        /// <returns></returns>
        private static DataTable GetJobExceptionRow(IEnumerable<BatchException> batchError)
        {
            var time = DateTime.Now.ToShortTimeString(); //ApplicationTime.GetCurrentTime();
            var datatable = GetJobExceptionDataTable();

            batchError.ToList().ForEach(error =>
            {
                var row = datatable.NewRow();
                row[BatchConstants.PropertyJobCode] = JobStatistics.JobCode;
                row[BatchConstants.PropertyJobStepCode] = JobStatistics.JobStepCode;
                row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
                row[BatchConstants.PropertyExceptionType] = error.Exception.GetType().Name;
                row[BatchConstants.PropertyExceptionSummary] = error.Exception.Message;
                row[BatchConstants.PropertyExceptionDetails] = error.Exception.StackTrace;
                row[BatchConstants.PropertyExceptionTime] = DateTime.Now.ToShortTimeString(); //ApplicationTime.GetCurrentTime();
                row[BatchConstants.PropertyKeyIdentifier] = string.IsNullOrWhiteSpace(error.IdentifierInformation.Key)
                    ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                    : error.IdentifierInformation.Key;
                row[BatchConstants.PropertyKeyValue] = string.IsNullOrWhiteSpace(error.IdentifierInformation.Value)
                    ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                    : error.IdentifierInformation.Value;
                row[BatchConstants.PropertyCreateUserId] = CurrentUserId;
                row[BatchConstants.PropertyCreateDate] = time;
                row[BatchConstants.PropertyJobChunkId] = error.JobChunkId;
                datatable.Rows.Add(row);
            });

            return datatable;
        }

        /// <summary>
        /// Gets the job exception row.
        /// </summary>
        /// <param name="batchError">The batch error.</param>
        /// <returns></returns>
        private static DataTable GetJobExceptionRow(BatchError batchError)
        {
            var time = DateTime.Now.ToShortTimeString(); //ApplicationTime.GetCurrentTime();
            var datatable = GetJobExceptionDataTable();
            var row = datatable.NewRow();

            row[BatchConstants.PropertyJobCode] = JobStatistics.JobCode;
            row[BatchConstants.PropertyJobStepCode] = JobStatistics.JobStepCode;
            row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
            row[BatchConstants.PropertyExceptionType] = batchError.ErrorType;
            row[BatchConstants.PropertyExceptionSummary] = batchError.ErrorSummary;
            row[BatchConstants.PropertyExceptionDetails] = batchError.ErrorDetail;
            row[BatchConstants.PropertyExceptionTime] = DateTime.Now.ToShortTimeString(); //ApplicationTime.GetCurrentTime();
            row[BatchConstants.PropertyKeyIdentifier] = string.IsNullOrWhiteSpace(batchError.IdentifierInformation.Key)
                ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                : batchError.IdentifierInformation.Key;
            row[BatchConstants.PropertyKeyValue] = string.IsNullOrWhiteSpace(batchError.IdentifierInformation.Value)
                ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                : batchError.IdentifierInformation.Value;
            row[BatchConstants.PropertyCreateUserId] = CurrentUserId;
            row[BatchConstants.PropertyCreateDate] = time;
            row[BatchConstants.PropertyJobChunkId] = batchError.JobChunkId;
            datatable.Rows.Add(row);

            return datatable;
        }

        /// <summary>
        /// Gets the job exception row.
        /// </summary>
        /// <param name="batchInformation">The batch information.</param>
        /// <returns></returns>
        private static DataTable GetJobExceptionRow(BatchInformation batchInformation)
        {
            var time = DateTime.Now.ToShortTimeString();//ApplicationTime.GetCurrentTime();
            var datatable = GetJobExceptionDataTable();
            var row = datatable.NewRow();

            row[BatchConstants.PropertyJobCode] = JobStatistics.JobCode;
            row[BatchConstants.PropertyJobStepCode] = JobStatistics.JobStepCode;
            row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
            row[BatchConstants.PropertyExceptionType] = batchInformation.InformationType;
            row[BatchConstants.PropertyExceptionSummary] = batchInformation.InformationSummary;
            row[BatchConstants.PropertyExceptionDetails] = batchInformation.InformationDetail;
            row[BatchConstants.PropertyExceptionTime] = DateTime.Now.ToShortTimeString(); //ApplicationTime.GetCurrentTime();
            row[BatchConstants.PropertyKeyIdentifier] =
                string.IsNullOrWhiteSpace(batchInformation.IdentifierInformation.Key)
                    ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                    : batchInformation.IdentifierInformation.Key;
            row[BatchConstants.PropertyKeyValue] =
                string.IsNullOrWhiteSpace(batchInformation.IdentifierInformation.Value)
                    ? DBNull.Value.ToString(CultureInfo.InvariantCulture)
                    : batchInformation.IdentifierInformation.Value;
            row[BatchConstants.PropertyCreateUserId] = CurrentUserId;
            row[BatchConstants.PropertyCreateDate] = time;
            row[BatchConstants.PropertyJobChunkId] = batchInformation.JobChunkId;
            datatable.Rows.Add(row);

            return datatable;
        }

        /// <summary>
        /// Gets the job exception row.
        /// </summary>
        /// <param name="exceptions">The exception.</param>
        /// <returns></returns>
        private static DataTable GetJobExceptionRow(IEnumerable<Exception> exceptions)
        {
            var dataTable = GetJobExceptionDataTable();
            var time = ApplicationTime.GetCurrentTime();

            exceptions.ToList().ForEach(error =>
                {
                    var row = dataTable.NewRow();
                    row[BatchConstants.PropertyJobCode] = JobStatistics.JobCode;
                    row[BatchConstants.PropertyJobStepCode] = JobStatistics.JobStepCode;
                    row[BatchConstants.PropertyJobRunId] = JobStatistics.RunId;
                    row[BatchConstants.PropertyExceptionType] = exceptions.GetType().Name;
                    row[BatchConstants.PropertyExceptionSummary] = error.Message;
                    row[BatchConstants.PropertyExceptionDetails] = error.StackTrace;
                    row[BatchConstants.PropertyExceptionTime] = ApplicationTime.GetCurrentTime();
                    row[BatchConstants.PropertyKeyIdentifier] = DBNull.Value.ToString(CultureInfo.InvariantCulture);
                    row[BatchConstants.PropertyKeyValue] = DBNull.Value.ToString(CultureInfo.InvariantCulture);
                    row[BatchConstants.PropertyCreateUserId] = CurrentUserId;
                    row[BatchConstants.PropertyCreateDate] = time;
                    row[BatchConstants.PropertyJobChunkId] = 0;
                    dataTable.Rows.Add(row);
                });

            return dataTable;
        }

        #endregion Private Methods
    }
}