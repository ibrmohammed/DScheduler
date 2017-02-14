using DSchedule.Common;
using DSchedule.Contracts.Models;
using DSchedule.Data;
using DScheduler.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Environment = DSchedule.Contracts.Models.Environment;

namespace DSchedule.BusinessComponent
{
    public class JobMonitorBusinessComponent
    {
        DSchedulerEntities context = new DSchedulerEntities();
        public Contracts.Models.JobMonitorModel GetJobMonitorDetails()
        {
            JobMonitorModel model = new JobMonitorModel();
            List<JobMonitor> jobMonitorList = new List<JobMonitor>();
            List<Node> nodesList = new List<Node>();
            List<Environment> environmentsList = new List<Environment>();
            List<Sessions> sessionsList = new List<Sessions>();
            List<Uprocs> uProcsList = new List<Uprocs>();

            var tempJobMonitors = (from jobMonitor in context.TJobMonitors
                                   select new
                                   {
                                       jobMonitor.JobID,
                                       jobMonitor.NodeID,
                                       jobMonitor.EnvironmentID,
                                       jobMonitor.SessionID,
                                       jobMonitor.UprocID,
                                       jobMonitor.StartedDateTime,
                                       jobMonitor.ScheduleDateTime,
                                       jobMonitor.CompletedDateTime,
                                       jobMonitor.UProcStatus,
                                       jobMonitor.CreatedDateTime,
                                       jobMonitor.CreatedBy,
                                       jobMonitor.UpdatedDateTime,
                                       jobMonitor.UpdatedBy
                                   }).ToList();
            if (tempJobMonitors.Any())
            {
                tempJobMonitors.ForEach(x => jobMonitorList.Add(new JobMonitor
                {
                    SessionID = x.SessionID,
                    UprocID = x.UprocID,
                    EnvironmentName = getEnvironmentNameByID(x.EnvironmentID),
                    NodeName = getNodeNameByID(x.NodeID),
                    SessionName = getSessionNameByID(x.SessionID),
                    UprocName = getUProcsNameByID(x.UprocID),
                    ScheduleDateTime = x.ScheduleDateTime != null ? x.ScheduleDateTime.Value.ToString("dd/MM/yyyy hh:mm:ss") : DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    CreatedDateTime = x.CreatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                    UpdatedDateTime = x.UpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                    CompletedDateTime = x.CompletedDateTime != null ? x.CompletedDateTime.Value.ToString("dd/MM/yyyy hh:mm:ss") : string.Empty,
                    StartedDateTime = x.StartedDateTime != null ? x.StartedDateTime.Value.ToString("dd/MM/yyyy hh:mm:ss") : string.Empty,
                    UProcStatus = x.UProcStatus
                }));
            }
            var tempNodes = (from node in context.TNodes
                             select new { node.NodeID, node.NodeName }).ToList();

            var tempEnvironments = (from node in context.TEnvironments
                                    select new { node.EnvironmentID, node.EnvironmentName }).ToList();

            if (tempNodes.Any())
            {
                tempNodes.ForEach(x => nodesList.Add(new Node { NodeID = x.NodeID, NodeName = x.NodeName }));
            }
            if (tempEnvironments.Any())
            {
                tempEnvironments.ForEach(x => environmentsList.Add(new Environment { EnvironmentID = x.EnvironmentID, EnvironmentName = x.EnvironmentName }));
            }

            model.NodeList = nodesList;
            model.EnvironmentList = environmentsList;
            model.JobMonitorList = jobMonitorList;
            return model;
        }

        public List<JobMonitor> JobMonitorSearch(JobMonitorSearchCriteria SearchCriteria)
        {
            var helper = new SqlClientHelper();
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("UprocName", SearchCriteria.UprocName));
            parameters.Add(new KeyValuePair<string, object>("SessionName", SearchCriteria.SessionName));
            parameters.Add(new KeyValuePair<string, object>("RunDate", SearchCriteria.JobRunDate));
            parameters.Add(new KeyValuePair<string, object>("Environment", SearchCriteria.EnvironmentName));
            parameters.Add(new KeyValuePair<string, object>("Node", SearchCriteria.NodeName));
            parameters.Add(new KeyValuePair<string, object>("CreatedBy", SearchCriteria.CreatedBy));
            parameters.Add(new KeyValuePair<string, object>("CreatedDate", SearchCriteria.CreatedDateTime));
            parameters.Add(new KeyValuePair<string, object>("JobStatus", SearchCriteria.JobStatus));

            DataSet JobSearchResults = helper.GetDataSetByProcedure(Constants.UspJobMonitorSearch, "default", true, parameters.ToArray());
            if(JobSearchResults != null && JobSearchResults.Tables != null)
            {
                DataTable datatable = JobSearchResults.Tables[0];
                var jobSearchJobMonitorModel = (from data in datatable.AsEnumerable()
                                                select
                                                new JobMonitor
                                                {
                                                    SessionID = !data.IsNull("SessionId") ? data.Field<int>("SessionId") : 0,
                                                    UprocID = !data.IsNull("UprocID") ? data.Field<int>("UprocID") : 0,
                                                    EnvironmentName = !data.IsNull("EnvironmentName") ? data.Field<string>("EnvironmentName") : "",
                                                    NodeName = !data.IsNull("NodeName") ? data.Field<string>("NodeName") : "",
                                                    SessionName = !data.IsNull("SessionName") ? data.Field<string>("SessionName") : "",                                                    
                                                    UprocName = !data.IsNull("UprocName") ? data.Field<string>("UprocName") : "",
                                                    ScheduleDateTime = !data.IsNull("ScheduleDateTime") ? data.Field<DateTime>("ScheduleDateTime").ToString("dd/MM/yyyy hh:mm:ss") : string.Empty,
                                                    CreatedBy = !data.IsNull("CreatedBy") ? data.Field<string>("CreatedBy") : "",
                                                    UpdatedBy = !data.IsNull("UpdatedBy") ? data.Field<string>("UpdatedBy") : "",
                                                    CreatedDateTime = data.Field<DateTime>("CreatedDateTime").ToString("dd/MM/yyyy hh:mm:ss"),
                                                    UpdatedDateTime = data.Field<DateTime>("UpdatedDateTime").ToString("dd/MM/yyyy hh:mm:ss"),
                                                    CompletedDateTime = !data.IsNull("CompletedDateTime") ? data.Field<DateTime>("CompletedDateTime").ToString("dd/MM/yyyy hh:mm:ss") : string.Empty,
                                                    StartedDateTime = !data.IsNull("StartedDateTime") ? data.Field<DateTime>("StartedDateTime").ToString("dd/MM/yyyy hh:mm:ss") : string.Empty,
                                                    UProcStatus = !data.IsNull("UprocName") ? data.Field<string>("UprocName") : ""

                                                }).ToList();

                return jobSearchJobMonitorModel;
            }
            else
            return new List<JobMonitor>();

        }

        private string getUProcsNameByID(int uprocID)
        {
            var uprocDetails = (from uProcs in context.TUprocs
                                where uProcs.UprocID == uprocID
                                select new
                                {
                                    uProcs.UprocID,
                                    uProcs.UprocName,

                                }).FirstOrDefault();

            return uprocDetails.UprocName.ToString();
        }

        private string getSessionNameByID(int sessionID)
        {
            var sessionDetails = (from uProcs in context.TSessions
                                  where uProcs.SessionID == sessionID
                                  select new
                                  {
                                      uProcs.SessionID,
                                      uProcs.SessionName,

                                  }).FirstOrDefault();

            return sessionDetails.SessionName.ToString();
        }

        private string getNodeNameByID(int nodeID)
        {
            var nodeDetails = (from uProcs in context.TNodes
                               where uProcs.NodeID == nodeID
                               select new
                               {
                                   uProcs.NodeID,
                                   uProcs.NodeName,

                               }).FirstOrDefault();

            return nodeDetails.NodeName.ToString();
        }

        private string getEnvironmentNameByID(int environmentID)
        {
            var environmentDetails = (from uProcs in context.TEnvironments
                                      where uProcs.EnvironmentID == environmentID
                                      select new
                                      {
                                          uProcs.EnvironmentID,
                                          uProcs.EnvironmentName,

                                      }).FirstOrDefault();

            return environmentDetails.EnvironmentName.ToString();
        }

        public bool UpdateUProcStatusInSession(JobMonitor jobMonitorModel)
        {
            try
            {
                var results = (from m in context.TJobMonitors
                               where m.SessionID == jobMonitorModel.SessionID
                                    && m.UprocID == jobMonitorModel.UprocID
                               select m).ToList().FirstOrDefault();
                results.UProcStatus = jobMonitorModel.UProcStatus;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUProcStatusInMSMQ(JobMonitor jobMonitorModel)
        {
            DScheduleBusinessComponent dbc = new DScheduleBusinessComponent();
            var encoding = Encoding.UTF8;
            try
            {
                MessageQueue myQueue;
                Message myMessage = new System.Messaging.Message();

                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    myQueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    myQueue = MessageQueue.Create(@".\Private$\MyQueue");
                }
                var json = dbc.getJson(jobMonitorModel).ToString();
                if (json == null)
                {
                    return false;
                }

                myMessage.BodyStream = new MemoryStream(encoding.GetBytes(json));
                myQueue.Formatter = new System.Messaging.ActiveXMessageFormatter();
                myQueue.DefaultPropertiesToSend.Priority = System.Messaging.MessagePriority.High;
                myQueue.Send(myMessage);

                //myQueue = new MessageQueue(@".\Private$\MyQueue");
                //Message myMessge2 = myQueue.Receive();
                //var jsonResult = dbc.Read(myMessge2);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
