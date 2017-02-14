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
    class ProcessUProcsTasksForEachSession : EnumerateTask<IEnumerable<ScheduledSessionDetails>>
    {
        public string connectionString;
        public ProcessUProcsTasksForEachSession(string connectionString)
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
            if (item != null)
            {
                foreach (var processSessionRecords in item)
                {
                    /*
                     * We need to add our validations here for UProc perspective, whether we need to process this UProc or not
                     * Validations Cover Outage 
                     * Covers last Uproc/Session completed validations
                     * Covers Recurrence Validations
                    */
                    if (processSessionRecords != null)
                    {
                        string state = "Started";
                        UpdateJobMontior(processSessionRecords, false, state);
                        bool flag = ProcessJobs(processSessionRecords);
                        state = "Completed";
                        UpdateJobMontior(processSessionRecords, flag, state);
                    }
                }
            }
        }



        #endregion

        #region private methods        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dRow"></param>
        /// <returns></returns>
        private bool ProcessJobs(ScheduledSessionDetails dRow)
        {
            try
            {
                List<TUproc> fetchUProcRecords;
                TUproc records;
                if (dRow != null && dRow.UProcID != null && Convert.ToInt32(dRow.UProcID) > 0)
                {
                    using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
                    {
                        fetchUProcRecords = (from abc in dbConnection.TUprocs
                                             where abc.UprocID == Convert.ToInt32(dRow.UProcID)
                                             select abc as TUproc).ToList();

                        records = dbConnection.TUprocs.FirstOrDefault(x => x.UprocID == Convert.ToInt32(dRow.UProcID));
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
                            while ((strLog = pr.StandardOutput.ReadLine()) != null)
                            {
                                Logger.WriteLog(strLog);
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
        private void UpdateJobMontior(ScheduledSessionDetails dRow, bool newStatus, string state)
        {
            using (DScheduleEntities dbConnection = new DScheduleEntities(connectionString))
            {
                var records = (from monitor in dbConnection.TJobMonitors
                               where monitor.SessionID == Convert.ToInt32(dRow.SessionID)
                               && monitor.UprocID == Convert.ToInt32(dRow.UProcID)
                               orderby monitor.StartedDateTime descending
                               select monitor).FirstOrDefault();

                if (records != null)
                {
                    if (state.Equals("Completed"))
                        records.UProcStatus = newStatus ? "Completed" : "Failed";
                    else
                        records.UProcStatus = "Started";
                    records.UpdatedBy = Constants.UpdatedByDefaultUserName;
                    records.UpdatedDateTime = DateTime.Now;
                }
                dbConnection.SaveChanges();
            }
        }
        #endregion
    }
}


