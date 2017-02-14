using DSchedule.Contracts.Models;
using DSchedule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
