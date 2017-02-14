using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DScheduler.Common;
using System.IO;
using System.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DScheduler.BusinessServices
{
    public class ProcessServiceInformation
    {
        protected DScheduleEntities dbConnection;
        List<ScheduledSessionDetails> scheduledRecordsList = new List<ScheduledSessionDetails>();
        List<ScheduledSessionDetails> msmqScheduledRecordsList = new List<ScheduledSessionDetails>();
        ScheduledSessionDetails scheduledSession;
        public void FetchJobRecords(string connectionString, TimeSpan timeOfDay, int interval, int numberOfThreads)
        {

            this.dbConnection = new DScheduleEntities(connectionString);
            try
            {
                if (dbConnection != null)
                {
                    #region ScheduledRecords Processing Starts
                    RunScheduledRecordsThroughServices(connectionString, timeOfDay, interval, numberOfThreads);
                    var MSMQResult = ConsumeMSMQInformation(connectionString, timeOfDay, interval, numberOfThreads);
                    #endregion
                }
                Logger.WriteLog("Scheduled Records Processing Finished" + DateTime.Now);

            }

            catch (Exception ex)
            {
                Logger.WriteLog("ProcesseServiceInformation :  " + ex.Message.ToString() + DateTime.Now);
                Logger.WriteLog("ProcesseServiceInformation :" + ex.InnerException.ToString() + DateTime.Now);
            }
        }

        private bool ConsumeMSMQInformation(string connectionString, TimeSpan timeOfDay, int interval, int numberOfThreads)
        {
            //further we need to add transactions if required.
            var encoding = Encoding.UTF8;
            try
            {
                bool continueToSeekForMessages = true;
                while (continueToSeekForMessages)
                {
                    try
                    {
                        var messageQueue = new System.Messaging.MessageQueue(@".\Private$\MyQueue");
                        var message = messageQueue.Receive(new TimeSpan(0, 0, 3));
                        List<ScheduledSessionDetails> queuedSessions = Read(message);
                        List<ScheduledSessionDetails> singleUprocRunList = new List<ScheduledSessionDetails>();
                        List<ScheduledSessionDetails> upcomingSessionRunList = new List<ScheduledSessionDetails>();
                        foreach (ScheduledSessionDetails scheduledSessions in queuedSessions)
                        {
                            if (scheduledSessions.JobAction.Equals(Constants.JobActionReRunStatus))
                            {
                                singleUprocRunList.Add(scheduledSessions);
                            }
                            else
                            {
                                upcomingSessionRunList.Add(scheduledSessions);
                            }
                        }

                        if (upcomingSessionRunList != null && upcomingSessionRunList.Count > 0)
                        {
                            //this needs to be updated as per the scheduled datetime, since the records are already present in the JobMonitor Table
                            //Check
                            var fetchProviderDetails = new FetchDependencyRecordsForSession(upcomingSessionRunList, numberOfThreads, connectionString);
                            fetchProviderDetails.Execute();
                        }

                        if (singleUprocRunList != null && singleUprocRunList.Count > 0)
                        {
                            //seperate call to be made for ReRun Scenario or single Uproc Run Scenario
                            var fetchUProcDetails = new FetchDetailsForUproc(singleUprocRunList, numberOfThreads, connectionString);
                            fetchUProcDetails.Execute();
                        }
                    }
                    catch (Exception)
                    {
                        //this exception will be only thrown when there are no more records present in the Queue.
                        //Thus, we can go ahead and end the loop.
                        continueToSeekForMessages = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }



        public List<ScheduledSessionDetails> Read(Message message)
        {
            if (message == null)
                return null;
            using (var reader = new StreamReader(message.BodyStream))
            {
                ScheduledSessionDetails msmqSessionDetails = new ScheduledSessionDetails();
                var json = reader.ReadToEnd();
                JObject jObject = JObject.Parse(json);
                msmqSessionDetails.UProcID = Convert.ToString(jObject["UprocID"]);
                msmqSessionDetails.SessionID = Convert.ToString(jObject["SessionID"]);
                msmqSessionDetails.JobAction = Convert.ToString(jObject["JobAction"]);
                msmqScheduledRecordsList.Add(msmqSessionDetails);
            }
            return msmqScheduledRecordsList;
        }


        private void RunScheduledRecordsThroughServices(string connectionString, TimeSpan timeOfDay, int interval, int numberOfThreads)
        {
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, interval);
            TimeSpan endTime = timeOfDay.Add(duration);
            var scheduledRecords = (from abc in dbConnection.TJobRules
                                    where abc.JobStartTime >= timeOfDay
                                    && abc.JobStartTime <= endTime
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

        public object getJson()
        {            
            var scheduledRecords = (from obj in dbConnection.TJobMonitors
                                    where obj.SessionID == 2
                                        // && obj.NodeID == jobMonitorModel.NodeID
                                    && obj.UprocID == 1
                                    select new { UprocID = obj.UprocID, UProcStatus = obj.UProcStatus, SessionID = obj.SessionID }).ToList().FirstOrDefault();
            return scheduledRecords;
        }
    }
}
