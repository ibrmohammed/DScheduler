using DSchedule.BusinessComponent;
using DSchedule.Common;
using DSchedule.Contracts;
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
    public class DScheduleBusinessComponent
    {
        DSchedulerEntities context = new DSchedulerEntities();

        #region Nodes

        public NodesModel GetNodes()
        {
            NodesModel node = new NodesModel() { NodesList = new List<Node>() };

            var tempNodes = (from TNode in context.TNodes
                             select TNode).ToList();

            if (tempNodes.Any())
            {
                tempNodes.ForEach(x => node.NodesList.Add(new Node
                {
                    NodeID = x.NodeID,
                    CreatedBy = x.CreatedBy,
                    CreatedDateTime = x.CreatedDateTime,
                    NodeName = x.NodeName,
                    NodePath = x.NodePath,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDateTime = x.UpdatedDateTime
                }));
            }

            return node;
        }

        public bool SaveNode(Node node)
        {
            if (node != null)
            {
                if (node.NodeID == 0)
                {
                    context.TNodes.Add(new TNode
                    {
                        NodeName = node.NodeName,
                        NodePath = node.NodePath,
                        CreatedBy = GlobalFunctions.GetLoggedInUser(),
                        CreatedDateTime = GlobalFunctions.GetDate(),
                        UpdatedBy = GlobalFunctions.GetLoggedInUser(),
                        UpdatedDateTime = GlobalFunctions.GetDate()
                    });
                }
                else
                {
                    TNode entity = context.TNodes.FirstOrDefault(x => x.NodeID == node.NodeID);
                    if (entity != null)
                    {
                        entity.NodeName = node.NodeName;
                        entity.NodePath = node.NodePath;
                        entity.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                        entity.UpdatedDateTime = GlobalFunctions.GetDate();
                    }
                }

                context.SaveChanges();
            }

            return true;
        }

        #endregion

        #region Environment

        public bool SaveEnvironment(Environment environment)
        {
            if (environment != null)
            {
                if (environment.EnvironmentID == 0)
                {
                    context.TEnvironments.Add(new TEnvironment
                    {
                        CreatedBy = GlobalFunctions.GetLoggedInUser(),
                        CreatedDateTime = GlobalFunctions.GetDate(),
                        EnvironmentID = environment.EnvironmentID,
                        EnvironmentName = environment.EnvironmentName,
                        EnvironmentPath = environment.EnvironmentPath,
                        NodeID = environment.NodeID,
                        UpdatedBy = GlobalFunctions.GetLoggedInUser(),
                        UpdatedDateTime = GlobalFunctions.GetDate()
                    });
                }
                else
                {
                    TEnvironment entity = context.TEnvironments.FirstOrDefault(x => x.EnvironmentID == environment.EnvironmentID);

                    entity.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                    entity.UpdatedDateTime = GlobalFunctions.GetDate();
                    entity.NodeID = environment.NodeID;
                    entity.EnvironmentPath = environment.EnvironmentPath;
                    entity.EnvironmentName = environment.EnvironmentName;
                }

                context.SaveChanges();
            }

            return true;
        }

        public EnvironmentModel GetEnvironments()
        {
            EnvironmentModel model = new EnvironmentModel();
            List<Environment> environments = new List<Environment>();
            List<Node> nodes = new List<Node>();

            var tempEnvironments = (from env in context.TEnvironments
                                    let nod = env.TNode
                                    select new
                                    {
                                        env.EnvironmentPath,
                                        env.EnvironmentName,
                                        env.EnvironmentID,
                                        env.CreatedDateTime,
                                        env.NodeID,
                                        nod.NodeName
                                    }).ToList();

            var tempNodes = (from node in context.TNodes
                             select new { node.NodeID, node.NodeName }).ToList();

            if (tempEnvironments.Any())
            {
                tempEnvironments.ForEach(x => environments.Add(new Environment
                {
                    EnvironmentID = x.EnvironmentID,
                    EnvironmentName = x.EnvironmentName,
                    CreatedDateTime = x.CreatedDateTime,
                    EnvironmentPath = x.EnvironmentPath,
                    NodeID = x.NodeID,
                    NodeName = x.NodeName
                }));
            }

            if (tempNodes.Any())
            {
                tempNodes.ForEach(x => nodes.Add(new Node { NodeID = x.NodeID, NodeName = x.NodeName }));
            }

            model.NodesList = nodes;
            model.EnvironmentList = environments;
            return model;
        }

        #endregion

        #region Uprocs

        public UprocsModel GetUprocs()
        {
            UprocsModel uproc = new UprocsModel();

            var tempUprocs = (from TUproc in context.TUprocs
                              select TUproc).ToList();

            if (tempUprocs.Any())
            {
                tempUprocs.ForEach(x => uproc.UprocsList.Add(new Uprocs
                {
                    UprocID = x.UprocID,
                    UprocName = x.UprocName,
                    JobTypeID = x.JobTypeID,
                    EnvironmentID = x.EnvironmentID,
                    AccountTypeID = x.AccountTypeID,
                    EnvironmentName = x.EnvironmentName,
                    FolderPath = x.FolderPath,
                    Command = x.Command
                }));
            }
            return uproc;
        }

        public bool SaveUprocDetails(UprocsModel uprocs)
        {
            bool success = false;
            if (uprocs.UprocID == 0)
            {
                TUproc obj = new TUproc();
                obj.UprocID = uprocs.UprocID;
                obj.UprocName = uprocs.UprocName;
                obj.JobTypeID = uprocs.JobTypeID;
                obj.AccountTypeID = uprocs.AccountTypeID;
                obj.EnvironmentID = uprocs.EnvironmentID;
                obj.EnvironmentName = uprocs.EnvironmentName;
                obj.FolderPath = uprocs.FolderPath;
                obj.Command = uprocs.Command;
                obj.CreatedBy = GlobalFunctions.GetLoggedInUser();
                obj.CreatedDateTime = GlobalFunctions.GetDate();
                obj.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                obj.UpdatedDateTime = GlobalFunctions.GetDate();
                context.TUprocs.Add(obj);
            }
            else
            {
                TUproc obj = context.TUprocs.FirstOrDefault(x => x.UprocID == uprocs.UprocID);
                obj.UprocName = uprocs.UprocName;
                obj.JobTypeID = uprocs.JobTypeID;
                obj.AccountTypeID = uprocs.AccountTypeID;
                obj.EnvironmentID = uprocs.EnvironmentID;
                obj.EnvironmentName = uprocs.EnvironmentName;
                obj.FolderPath = uprocs.FolderPath;
                obj.Command = uprocs.Command;
                obj.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                obj.UpdatedDateTime = GlobalFunctions.GetDate();
            }
            context.SaveChanges();
            success = true;
            return success;
        }

        public List<ReferenceData> GetUprocsReferenceData()
        {
            List<ReferenceData> listRefData = new List<ReferenceData>();
            List<string> listUprocsRefData = new List<string>();
            listUprocsRefData.Add("AccountTypeID");
            listUprocsRefData.Add("JobTypeID");
            var tempRefdata = (from TRef in context.TRefs
                               where listUprocsRefData.Contains(TRef.RefName)
                               select TRef).ToList();
            //Adding Account and Job Type Refdata to list of ReferenceData
            if (tempRefdata.Any())
            {
                tempRefdata.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.RefID,
                    RefName = x.RefName,
                    RefValue = x.RefValue,
                    RefDescription = x.RefDescription,
                    IsActive = x.IsActive,
                    IsVisible = x.IsVisible
                }));
            }
            //Fetching Environment Ref Data
            var envRef = (from env in context.TEnvironments
                          select env).ToList();
            if (envRef.Any())
            {
                envRef.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.EnvironmentID,
                    RefName = "EnvironmentTypeID",
                    RefValue = x.EnvironmentName,
                    RefDescription = x.EnvironmentName,
                    IsActive = "Y",
                    IsVisible = "Y"
                }));
            }
            return listRefData;
        }

        #endregion



        #region Sessions

        public SessionsModel GetSessions()
        {
            SessionsModel session = new SessionsModel();

            var tempSessions = (from sessionItem in context.TSessions
                                join dependencyItem in context.TDependencies
                                on sessionItem.SessionID equals dependencyItem.SessionID
                                select new
                                {
                                    SessionId = sessionItem.SessionID,
                                    EnvironmentId = sessionItem.EnvironmentID,
                                    AccountTypeId = sessionItem.AccountTypeID,
                                    Session = sessionItem.SessionName,
                                    EnvironmentName = sessionItem.EnvironmentName,
                                    UprocId = dependencyItem.UprocID,
                                }).ToList();
            //var tempTDependencies = (from dependentItem in context.TDependencies
            //                         select dependentItem).ToList();
            if (tempSessions.Any())
            {
                //tempSessions.ForEach(x => session.SessionsList.Add(new Sessions
                //{
                //    SessionId = x.SessionID,
                //    EnvironmentID = x.EnvironmentID,
                //    AccountTypeID = x.AccountTypeID,
                //    Session = x.SessionName,
                //    Environment = x.EnvironmentName,

                //}
                //));
                foreach (var item in tempSessions)
                {
                    session.SessionsList.Add(new Sessions
                    {
                        SessionId = item.SessionId,
                        EnvironmentID = item.EnvironmentId,
                        AccountTypeID = item.AccountTypeId,
                        Session = item.Session,
                        Environment = item.EnvironmentName,
                        UprocId = item.UprocId.HasValue?item.UprocId.Value:0,
                    }
                    );
                }
            }
            return session;
        }

        public bool SaveSessions(SessionsModel request)
        {
            bool success = false;
            if (request.SessionId == 0)
            {
                //Adding record in the TSession Table
                TSession obj = new TSession();
                obj.SessionID = request.SessionId;
                obj.SessionName = request.SessionName;
                obj.EnvironmentID = request.EnvironmentID;
                obj.EnvironmentName = request.EnvironmentName;
                obj.AccountTypeID = request.AccountTypeID;
                obj.CreatedBy = GlobalFunctions.GetLoggedInUser();
                obj.CreatedDateTime = GlobalFunctions.GetDate();
                obj.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                obj.UpdatedDateTime = GlobalFunctions.GetDate();
                context.TSessions.Add(obj);

                //Adding record in the TDependencies table
                TDependency depObj = new TDependency();
                depObj.DependentID = 0;
                depObj.SessionID = request.SessionId;
                depObj.UprocID = request.UprocID;
                depObj.CreatedBy = GlobalFunctions.GetLoggedInUser();
                depObj.CreatedDateTime = GlobalFunctions.GetDate();
                depObj.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                depObj.UpdatedDateTime = GlobalFunctions.GetDate();
                context.TDependencies.Add(depObj);
                //code to create dependent session
                if(request.DependentSessionId !=0 && request.DependentUprocId != 0)
                {
                    TDependency dep = new TDependency();
                    dep.DependentID = -1;
                    dep.SessionID = request.DependentSessionId;
                    dep.UprocID = request.DependentUprocId;
                    dep.CreatedBy = GlobalFunctions.GetLoggedInUser();
                    dep.CreatedDateTime = GlobalFunctions.GetDate();
                    dep.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                    dep.UpdatedDateTime = GlobalFunctions.GetDate();
                    context.TDependencies.Add(dep);
                }
            }
            else
            {
                TSession obj = context.TSessions.FirstOrDefault(x => x.SessionID == request.SessionId);
                obj.SessionID = request.SessionId;
                obj.SessionName = request.SessionName;
                obj.EnvironmentID = request.EnvironmentID;
                obj.EnvironmentName = request.EnvironmentName;
                obj.AccountTypeID = request.AccountTypeID;
                obj.UpdatedBy = GlobalFunctions.GetLoggedInUser();
                obj.UpdatedDateTime = GlobalFunctions.GetDate();

                TDependency depObj = new TDependency();
            }
            context.SaveChanges();
            success = true;
            return success;
        }
        public List<ReferenceData> GetSessionsReferenceData()
        {
            List<ReferenceData> listRefData = new List<ReferenceData>();
            List<string> listSessionsRefData = new List<string>();
            listSessionsRefData.Add("AccountTypeID");
            var tempRefdata = (from TRef in context.TRefs
                               where listSessionsRefData.Contains(TRef.RefName)
                               select TRef).ToList();
            //Adding Account and Job Type Refdata to list of ReferenceData
            if (tempRefdata.Any())
            {
                tempRefdata.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.RefID,
                    RefName = x.RefName,
                    RefValue = x.RefValue,
                    RefDescription = x.RefDescription,
                    IsActive = x.IsActive,
                    IsVisible = x.IsVisible
                }));
            }
            //Fetching Environment Ref Data
            var envRef = (from env in context.TEnvironments
                          select env).ToList();
            if (envRef.Any())
            {
                envRef.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.EnvironmentID,
                    RefName = "EnvironmentTypeID",
                    RefValue = x.EnvironmentName,
                    RefDescription = x.EnvironmentName,
                    IsActive = "Y",
                    IsVisible = "Y"
                }));
            }
            //Fetching Uproc Reference Data
            var uprocRef = (from uproc in context.TUprocs
                            select uproc).ToList();
            if (uprocRef.Any())
            {
                uprocRef.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.UprocID,
                    RefName = "UprocsTypeID",
                    RefValue = x.UprocName,
                    RefDescription = x.UprocName,
                    IsActive = "Y",
                    IsVisible = "Y"
                }));
            }
            //Fetching Sessions reference data
            var sessionRef = (from session in context.TDependencies
                              select session.TSession).Distinct().ToList();
            if (sessionRef.Any())
            {
                sessionRef.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.SessionID,
                    RefName = "SessionsID",
                    RefValue = x.SessionName,
                    RefDescription = x.SessionName,
                    IsActive = "Y",
                    IsVisible = "Y"
                }));
            }

            //Fetching Dependent Uprocs reference data
            var dependentUprocsRef = (from dUprocs in context.TDependencies
                                      select dUprocs.TUproc).Distinct().ToList();
            if (dependentUprocsRef.Any())
            {
                dependentUprocsRef.ForEach(x => listRefData.Add(new ReferenceData
                {
                    RefId = x.UprocID,
                    RefName = "DependentUprocsID",
                    RefValue = x.UprocName,
                    RefDescription = x.UprocName,
                    IsActive = "Y",
                    IsVisible = "Y"
                }));
            }
            return listRefData;
        }
        #endregion

        public List<TRef> GetAccountTypes()
        {
            List<TRef> tRefList = new List<TRef>();
            TRef tRef;
            var tempUprocs = from accountTypes in context.TRefs
                             where accountTypes.RefName == "AccountTypeID"
                             select accountTypes;

            foreach (TRef myRef in tempUprocs)
            {
                tRef = new TRef();
                tRef.RefName = myRef.RefName;
                tRef.RefValue = myRef.RefValue;
                tRef.RefDescription = myRef.RefDescription;
                tRef.RefID = myRef.RefID;
                tRefList.Add(tRef);
            }
            return tRefList;
        }
    }
}
