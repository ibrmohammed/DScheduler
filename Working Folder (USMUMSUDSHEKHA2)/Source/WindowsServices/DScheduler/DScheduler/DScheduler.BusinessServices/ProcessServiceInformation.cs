using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DScheduler.Common;

namespace DScheduler.BusinessServices
{
    public class ProcessServiceInformation
    {
        protected DScheduleEntities dbConnection;
        List<ScheduledSessionDetails> scheduledRecordsList = new List<ScheduledSessionDetails>();
        ScheduledSessionDetails scheduledSession;
        int numberOfThreads;

        public void FetchJobRecords(string connectionString, TimeSpan timeOfDay, int interval, int numberOfThreads)
        {

            this.dbConnection = new DScheduleEntities(connectionString);
            try
            {
                if (dbConnection != null)
                {
                    #region ScheduledRecords Processing Starts
                    System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, interval);
                    TimeSpan endTime = timeOfDay.Add(duration);
                    var scheduledRecords = (from abc in dbConnection.TJobRules
                                            //where abc.JobStartTime >= timeOfDay
                                            //&& abc.JobStartTime <= endTime
                                            orderby abc.JobStartTime
                                            select new { abc.SessionID, abc.UprocID, abc.JobStartTime }).ToList();

                    Logger.WriteLog("Scheduled Records Fetched" + DateTime.Now);
                    //need to remove this in future
                    foreach (var records in scheduledRecords)
                    {
                        scheduledSession = new ScheduledSessionDetails();
                        scheduledSession.UProcID = records.UprocID.ToString();
                        scheduledSession.SessionID = records.SessionID.ToString();
                        scheduledSession.JobStartTime = records.JobStartTime;
                        scheduledRecordsList.Add(scheduledSession);
                    }
                    //multi threading task starts here                        
                    if (scheduledRecords != null && scheduledRecords.Count() > 0)
                    {
                        var fetchProviderDetails = new FetchDependencyRecordsForSession(scheduledRecordsList, numberOfThreads, connectionString);
                        fetchProviderDetails.Execute();
                    }
                }
                Logger.WriteLog("Scheduled Records Processing Finished" + DateTime.Now);
                    #endregion
            }

            catch (Exception ex)
            {
                Logger.WriteLog("ProcesseServiceInformation :  " + ex.Message.ToString() + DateTime.Now);
                Logger.WriteLog("ProcesseServiceInformation :" + ex.InnerException.ToString() + DateTime.Now);
            }
        }
    }
}
