using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static class JobProcessor
    {
        #region Public Methods

        /// <summary>
        /// Gets the batch user identifier.
        /// </summary>
        /// <param name="jobCode">The job code.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string GetBatchUserId(string jobCode)
        {
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, jobCode)
                };

            var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetBatchUserId, Job.ConnectionStringName, true, parameters.ToArray());
            var returnValue = data.Rows.Count > 0
                ? data.Rows[0].Field<string>(BatchConstants.PropertyBatchUserId)
                : null;
            return returnValue;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Gates the batch and check if it can continue with the next chunk.
        /// </summary>
        /// <returns></returns>
        internal static bool GateBatch()
        {
            //var helper = new SqlClientHelper();
            //var parameters = new[]
            //    {
            //        new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
            //        new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
            //        new KeyValuePair<string, object>(BatchConstants.VariableBatchRunId, JobStatistics.BatchRunId)
            //    };
            //var status = (bool)helper.GetScalarByProcedure(BatchConstants.ProcedureGateBatch,
            //                                          Job.ConnectionStringName, true, parameters);
            //if (status)
            //{
            //    return status;
            //}
            //JobStatistics.BatchJobStatus = JobStatus.Gated;
            //return status;
            return true;
        }

        /// <summary>
        /// Initializes the batch.
        /// </summary>
        /// <param name="jobCode">The job code.</param>
        /// <param name="jobStepCode">The job step code.</param>
        /// <param name="totalCount">The total count.</param>
        internal static void InitializeBatch(string jobCode, string jobStepCode, Func<int> totalCount)
        {
            if (!String.IsNullOrEmpty(jobCode))
            {
                JobStatistics.JobCode = jobCode;
            }

            if (!String.IsNullOrEmpty(jobStepCode))
            {
                JobStatistics.JobStepCode = jobStepCode;
            }

            if (totalCount != null)
            {
                JobStatistics.TotalCount = totalCount.Invoke();
            }

            InitializeBatchStatistics();
        }

        /// <summary>
        /// Marks the batch as gated.
        /// </summary>
        internal static void MarkBatchAsGated()
        {
            var helper = new SqlClientHelper();
            var parameters = new[]
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableBatchRunId, JobStatistics.BatchRunId)
                };
            helper.RunProcedure(BatchConstants.ProcedureMarkBatchGated, Job.ConnectionStringName, true, parameters);
        }

        /// <summary>
        /// Uodates the batch statistics.
        /// </summary>
        /// <param name="status">The status.</param>
        internal static void UpdateBatchStatistics(JobStatus status)
        {
            do
            {
                try
                {
                    var helper = new SqlClientHelper();
                    var currentDate = ApplicationTime.GetCurrentTime();

                    var parameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                        new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                        new KeyValuePair<string, object>(BatchConstants.VariableEndDate,
                            status == JobStatus.Processing || status == JobStatus.New
                                ? new DateTime(1753, 1, 1) //Lowest available SQL date
                                : currentDate),
                        new KeyValuePair<string, object>(BatchConstants.VariableProcessingStatus, status.ToString()),
                        new KeyValuePair<string, object>(BatchConstants.VariableSuccessCount, JobStatistics.SuccessCount),
                        new KeyValuePair<string, object>(BatchConstants.VariableFailureCount, JobStatistics.FailureCount),
                        new KeyValuePair<string, object>(BatchConstants.VariableTotalCount, JobStatistics.TotalCount),
                        new KeyValuePair<string, object>(BatchConstants.VariableOtherCount, JobStatistics.OtherCount),
                        new KeyValuePair<string, object>(BatchConstants.VariableBatchRunId, JobStatistics.BatchRunId)
                    };

                    if (!string.IsNullOrEmpty(JobStatistics.JobStepCode))
                    {
                        parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
                    }

                    if (!string.IsNullOrWhiteSpace(JobStatistics.AdditionalInformation))
                    {
                        parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableAdditionalInformation, JobStatistics.AdditionalInformation));
                    }

                    if (!string.IsNullOrWhiteSpace(JobStatistics.JobStepSummaryAdditionalInformation))
                    {
                        parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepSummaryAdditionalInformation, JobStatistics.JobStepSummaryAdditionalInformation));
                    }

                    var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureUpdateCurrentBatchJobStatistics, Job.ConnectionStringName, true, parameters.ToArray());
                    if (data.Rows.Count > 0)
                    {
                        var jobStatus = data.Rows[0].Field<string>(BatchConstants.PropertyProcessingStatus);
                        JobStatistics.BatchJobStatus = (JobStatus)Enum.Parse(typeof(JobStatus), jobStatus);
                    }
                    break;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 1205 || ex.Message.Contains("was deadlocked"))//Deadlock
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        throw;
                    }
                }
            } while (true);
        }

        /// <summary>
        /// Gets the batch user identifier based on the JobCode in the JobStatistics class.
        /// </summary>
        internal static void GetBatchUserId()
        {
            JobStatistics.BatchUserId = GetBatchUserId(JobStatistics.JobCode);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// This method does the following.
        /// 1. It checks the status of the last run.
        /// 2. If the status is Processing, it just sets the properties.
        /// 3. If the status is Gated, it resets the status to processing, and sets the properties.
        /// 4. If the status is failed, it sets it for processing if the JobStatistics.RestartFailedRun flag is set.
        /// </summary>
        private static bool InitializeBatchStatistics()
        {
            //Check the last run status
            var lastRunStatus = GetLastBatchRunStatus();//This sets the JobStatistics.RunId property if status was processing.

            //If last run completed, start a new run.
            if (lastRunStatus == JobStatus.Completed.ToString() || lastRunStatus == JobStatus.CompletedWithErrors.ToString() || lastRunStatus == JobStatus.Failed.ToString())
            {
                lastRunStatus = StartNewBatchInstance();//This sets the JobStatistics.RunId property
            }
            //If last run gated, reset status to processing
            else if (lastRunStatus == JobStatus.Gated.ToString())
            {
                lastRunStatus = RestartLastBatchInstance();//This sets the JobStatistics.RunId property
            }
            //Get the BatchUserId
            GetBatchUserId();
            return lastRunStatus == JobStatus.Processing.ToString();
        }

        /// <summary>
        /// Gets the last batch run status from JmSummary or JobStepSummary.
        /// Also updates the JobStatistics.RunId property if the status is Gated/Processing.
        /// </summary>
        /// <returns></returns>
        private static string GetLastBatchRunStatus()
        {
            var helper = new SqlClientHelper();
            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode)
                };

            if (!string.IsNullOrWhiteSpace(JobStatistics.JobStepCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
            }

            var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureGetLastBatchStatus, Job.ConnectionStringName, true, parameters.ToArray());
            string runStatus;
            if (data.Rows.Count > 0)
            {
                runStatus = data.Rows[0].Field<string>(BatchConstants.PropertyProcessingStatus);
                if (runStatus == JobStatus.Gated.ToString() || runStatus == JobStatus.Processing.ToString())
                {
                    JobStatistics.RunId = data.Rows[0].Field<Guid>(BatchConstants.PropertyRunId);
                    JobStatistics.BatchRunId = data.Rows[0].Field<long>(BatchConstants.PropertyBatchRunId);
                }
            }
            else
            {
                runStatus = JobStatus.Completed.ToString();
            }

            return runStatus;
        }

        /// <summary>
        /// Restarts the gated batch instance and sets its status to processing.
        /// </summary>
        /// <returns></returns>
        private static string RestartLastBatchInstance()
        {
            //This is instance safe since two people updating the status to processed does not matter.
            var helper = new SqlClientHelper();
            var parameters = new[]
                {
                    new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode),
                    new KeyValuePair<string, object>(BatchConstants.VariableJobRunId, JobStatistics.RunId),
                    new KeyValuePair<string, object>(BatchConstants.VariableBatchRunId, JobStatistics.BatchRunId)
                };
            var status = (string)helper.GetScalarByProcedure(BatchConstants.ProcedureRestartBatchInstance, Job.ConnectionStringName, true, parameters);
            JobStatistics.BatchJobStatus = JobStatus.Processing;
            return status;
        }

        /// <summary>
        /// Starts the new batch instance with a status of processing.
        /// </summary>
        /// <returns></returns>
        private static string StartNewBatchInstance()
        {
            do
            {
                try
                {
                    //This is instance safe since two people updating the status to processed does not matter.
                    var helper = new SqlClientHelper();
                    var parameters = new List<KeyValuePair<string, object>>
                            {
                                new KeyValuePair<string, object>(BatchConstants.VariableJobCode, JobStatistics.JobCode)
                            };
                    if (!string.IsNullOrEmpty(JobStatistics.JobStepCode))
                    {
                        parameters.Add(new KeyValuePair<string, object>(BatchConstants.VariableJobStepCode, JobStatistics.JobStepCode));
                    }

                    var data = helper.GetDataTableByProcedure(BatchConstants.ProcedureStartNewBatchInstance, Job.ConnectionStringName, true, parameters.ToArray());
                    if (data.Rows.Count > 0)
                    {
                        JobStatistics.RunId = data.Rows[0].Field<Guid>(BatchConstants.PropertyRunId);
                        JobStatistics.BatchRunId = data.Rows[0].Field<long>(BatchConstants.PropertyBatchRunId);
                        JobStatistics.BatchJobStatus = JobStatus.Processing;

                    }
                    break;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 1205 || ex.Message.Contains("was deadlocked"))//Deadlock
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        throw;
                    }
                }
            } while (true);
            return JobStatistics.BatchJobStatus.ToString();
        }
        #endregion
    }
}