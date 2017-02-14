using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DScheduler.Common;
using DScheduler.Framework;
using DScheduler.Framework.Helpers;

namespace DScheduler.BusinessServices
{
    public class ProcessDependencyTasksForEachSession : EnumerateTask<IEnumerable<ScheduledSessionDetails>>
    {
        public string connectionString;
        public ProcessDependencyTasksForEachSession(string connectionString)
        {
            // TODO: Complete member initialization
            this.connectionString = connectionString;
        }

        #region Batch Framework overrides

        /// <summary>
        /// Override batch framework task cleanup method
        /// </summary>
        protected override void CleanUp()
        {
            base.CleanUp();
        }

        /// <summary>
        /// Override batch framework task initialize method
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        protected override void ProcessItem(IEnumerable<ScheduledSessionDetails> item)
        {
            List<TDependency> dependantRecords;
            if (item != null)
            {
                foreach (var processSessionRecords in item)
                {
                    /*
                    * We need to add our validations here for Session perspective, whether we need to process this session or not                    * 
                    */
                    using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
                    {
                        int sessionId = Convert.ToInt32(processSessionRecords.SessionID);
                        dependantRecords = (from records in dbConnection.TDependencies
                                            orderby records.SequenceNumber
                                            where records.SessionID == sessionId
                                            && records.IsActive == 1
                                            select records as TDependency).ToList();
                    }

                    InsertInformationInJobMontiorRecords(dependantRecords);
                    foreach (var dRow in dependantRecords)
                    {
                        /*
                         * We need to add our validations here for UProc perspective, whether we need to process this UProc or not
                         * Validations Cover Outage 
                         * Covers last Uproc/Session completed validations
                         * Covers Recurrence Validations
                        */
                        if (ValidateUProcBeforeProcessing(dRow))
                        {
                            string state = "Started";
                            UpdateJobMontior(dRow, false, state);
                            bool flag = ProcessJobs(dRow);
                            state = "Completed";
                            UpdateJobMontior(dRow, flag, state);
                        }
                    }

                }
            }
        }

        private bool ValidateUProcBeforeProcessing(TDependency dRow)
        {
            if (dRow != null && dRow.SequenceNumber == 1)
                return true;
            if (dRow != null)
            {
                //var helper = new SqlClientHelper();
                //helper.RunProcedure(Constants.SpValidateUProcBeforeExecution, Constants.DefaultConnectionString, true,
                //                               new KeyValuePair<string, object>(Constants.SessionID, Convert.ToInt32(dRow.SessionID)),
                //                               new KeyValuePair<string, object>(Constants.UprocID, Convert.ToInt32(dRow.UprocID)),
                //                               new KeyValuePair<string, object>(Constants.SequenceNumber, Convert.ToInt32(dRow.SequenceNumber)));

                using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
                {
                    var previousUProcDetails = (from pre in dbConnection.TDependencies
                                                where pre.SessionID == dRow.SessionID
                                                && pre.SequenceNumber == dRow.SequenceNumber - 1
                                                select pre).ToList().FirstOrDefault();

                    var previousRecordsStatus = (from records in dbConnection.TJobMonitors
                                                 where records.UprocID == previousUProcDetails.UprocID
                                                 && records.SessionID == previousUProcDetails.SessionID
                                                 select records).ToList().FirstOrDefault();

                    if (previousRecordsStatus.UProcStatus == Constants.CompletedStatus.ToString())
                    {
                        var currentUProcStatus = (from records in dbConnection.TJobMonitors
                                                  where records.SessionID == dRow.SessionID
                                                  && records.UprocID == dRow.UprocID
                                                  select records).ToList().FirstOrDefault();
                        if (currentUProcStatus.UProcStatus.Equals(Constants.PendingStatus.ToString()))
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }


        #endregion

        #region private methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependantRecords"></param>
        private void InsertInformationInJobMontiorRecords(List<TDependency> dependantRecords)
        {
            var helper = new SqlClientHelper();
            foreach (var records in dependantRecords)
            {
                int numberOfRowsAffected =
                       helper.RunProcedure(Constants.SpInsertDataIntoJobMonitor, Constants.DefaultConnectionString, true,
                                           new KeyValuePair<string, object>(Constants.NodeID, Convert.ToInt32(Constants.DefaultNodeID)),
                                           new KeyValuePair<string, object>(Constants.EnvironmentID, Convert.ToInt32(Constants.DefaultEnvironmentID)),
                                           new KeyValuePair<string, object>(Constants.SessionID, Convert.ToInt32(records.SessionID)),
                                           new KeyValuePair<string, object>(Constants.UprocID, Convert.ToInt32(records.UprocID)),
                                           new KeyValuePair<string, object>(Constants.UProcStatus, Constants.PendingStatus),
                                           new KeyValuePair<string, object>(Constants.StartedDateTime, DateTime.Now),
                                           new KeyValuePair<string, object>(Constants.CreatedBy, Constants.AdminUser),
                                           new KeyValuePair<string, object>(Constants.UpdatedBy, Constants.AdminUser),
                                           new KeyValuePair<string, object>(Constants.UpdatedDateTime, DateTime.Now),
                                           new KeyValuePair<string, object>(Constants.CreatedDateTime, DateTime.Now));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dRow"></param>
        /// <returns></returns>
        private bool ProcessJobs(TDependency dRow)
        {
            try
            {
                List<TUproc> fetchUProcRecords;
                TUproc records;
                if (dRow.UprocID != null || dRow.UprocID > 0)
                {
                    using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
                    {
                        fetchUProcRecords = (from abc in dbConnection.TUprocs
                                             where abc.UprocID == dRow.UprocID
                                             select abc as TUproc).ToList();

                        records = dbConnection.TUprocs.FirstOrDefault(x => x.UprocID == dRow.UprocID);
                    }
                    if (records != null)
                    {
                        using (Process pr = new Process())
                        {
                            pr.StartInfo = new ProcessStartInfo()
                            {
                                FileName = records.FolderPath + records.UprocName,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                UseShellExecute = false,
                                CreateNoWindow = true,
                                Arguments = "50"
                            };
                            pr.Start();
                            string strLog;
                            string jobName = dRow.SessionID.ToString() + dRow.UprocID.ToString() +"_" + Convert.ToDateTime(dRow.CreatedDateTime).ToString("dd-mm-yyyy");
                            while ((strLog = pr.StandardOutput.ReadLine()) != null)
                            {
                                Logger.WriteLog(strLog, jobName);
                            }
                            pr.WaitForExit();
                        }
                    }
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Logger.WriteLog(Ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dRow"></param>
        /// <param name="newStatus"></param>
        /// <param name="state"></param>
        private void UpdateJobMontior(TDependency dRow, bool newStatus, string state)
        {
            using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
            {
                var records = (from monitor in dbConnection.TJobMonitors
                               where monitor.SessionID == dRow.SessionID
                               && monitor.UprocID == dRow.UprocID
                               orderby monitor.StartedDateTime descending
                               select monitor).FirstOrDefault();

                if (records != null)
                {
                    if (state.Equals("Completed"))
                        records.UProcStatus = newStatus ? "Completed" : "Failed";
                    else
                        records.UProcStatus = "Started";
                    records.UpdatedBy = "admin2";
                    records.UpdatedDateTime = DateTime.Now;
                }
                dbConnection.SaveChanges();
            }
        }
        #endregion
    }
}

