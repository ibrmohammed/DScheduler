using DSchedule.BusinessComponent;
using DSchedule.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DSchedule.Web.Controllers
{
    public class JobMonitorController : Controller
    {
        // GET: JobMonitoring       
        JobMonitorBusinessComponent jobMonitorBusinessComponent = new JobMonitorBusinessComponent();
        DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();
        JobMonitorModel model = new JobMonitorModel();
        public ActionResult Index()
        {
            model = jobMonitorBusinessComponent.GetJobMonitorDetails();
            var jobActionTypes = businessComponent.GetReferenceDataNew("JobStatusTypeID");
            model.JobActionTypes = jobActionTypes;
            return View("JobMonitor", model);
        }

       // [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 15)] // every 3 sec
        public ActionResult GetJobMonitorDetails()
        {
            model = jobMonitorBusinessComponent.GetJobMonitorDetails();
            return PartialView("JobMonitorPartial", model);

        }

        public ActionResult GetUProcDetailsByID(string uprocID, string sessionID)
        {
            model = jobMonitorBusinessComponent.GetJobMonitorDetails();
            var results = (from m in model.JobMonitorList
                          where m.SessionID == Convert.ToInt32(sessionID) && m.UprocID == Convert.ToInt32(uprocID)
                          select new JobMonitor
                                {
                                    UprocID = m.UprocID,
                                    UprocName= m.UprocName,
                                    SessionID = m.SessionID,
                                    SessionName = m.SessionName,
                                    UProcStatus=  m.UProcStatus
                                }).FirstOrDefault();

            
            var jobActionTypes = businessComponent.GetReferenceData("JobActionTypeID");
            //var jobActiontypeList = businessComponent.GetRefTableValueDetails(jobActionTypes);

            var uprocStatus = results.UProcStatus;
            IEnumerable<SelectListItem> filteredList;
            switch (uprocStatus)
            {
                case "Pending":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Start",Value = "Start"},
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Hold",Value = "Hold" }, 
                            new SelectListItem { Text = "Log",Value = "Log" },
                            new SelectListItem { Text = "Cancel",Value = "Cancel" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }

                case "Started":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Abort",Value = "Abort"},
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Force Completion",Value = "Force Completion" },
                            new SelectListItem { Text = "Hold",Value = "Hold" },
                            new SelectListItem { Text = "Log",Value = "Log" },
                            new SelectListItem { Text = "Cancel",Value = "Cancel" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }

                case "Aborted":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Re-Run",Value = "Re-Run" },
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Log",Value = "Log" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }

                case "Completed":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Re-Run",Value = "Re-Run" },
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Log",Value = "Log" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }

                case "Cancelled":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Start",Value = "Start"},
                            new SelectListItem { Text = "Re-Run",Value = "Re-Run" },
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Log",Value = "Log" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }

                case "On Hold":
                    {
                        filteredList = new List<SelectListItem> {
                            new SelectListItem { Text = "Start",Value = "Start"},
                            new SelectListItem { Text = "Re-Run",Value = "Re-Run" },
                            new SelectListItem { Text = "Re-Schedule",Value = "Re-Schedule" },
                            new SelectListItem { Text = "Force Completion",Value = "Force Completion" },                            
                            new SelectListItem { Text = "Log",Value = "Log" },
                            new SelectListItem { Text = "Cancel",Value = "Cancel" }
                        };
                        ViewBag.JobActionTypes = filteredList;
                        break;
                    }
            }
            
            

           return PartialView("_UprocsDetailsPartialView",results);
        }

        [HttpPost]
        public ActionResult SearchUprocDetails(JobMonitorSearchCriteria SearchCriteria )
        {
            var jobMonitorList = jobMonitorBusinessComponent.JobMonitorSearch(SearchCriteria);
            var jobMonitorModel = new JobMonitorModel();
            jobMonitorModel.JobMonitorList = jobMonitorList;
            return PartialView("JobMonitorPartial", jobMonitorModel);
        }



        public ActionResult UpdateUProcStatusForSession(JobMonitor jobMonitorModel)
        {
            // jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);

            //Model contains the action information in the uprocstatus attribute.As per action information, uproc status needs to be updated.   
            //DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();
            switch (jobMonitorModel.UProcStatus)
            {
                case "Start":
                    jobMonitorBusinessComponent.UpdateUProcStatusInMSMQ(jobMonitorModel);
                    jobMonitorModel.UProcStatus = "Pending";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Re-Run":
                    jobMonitorBusinessComponent.UpdateUProcStatusInMSMQ(jobMonitorModel);
                    jobMonitorModel.UProcStatus = "Pending";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Re-Schedule":
                    jobMonitorBusinessComponent.UpdateUProcStatusInMSMQ(jobMonitorModel);
                    jobMonitorModel.UProcStatus = "Pending";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Hold":
                    jobMonitorModel.UProcStatus = "On Hold";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Abort":
                    jobMonitorModel.UProcStatus = "Aborted";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Force Completion":
                    jobMonitorModel.UProcStatus = "Completed";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
                case "Log":
                    // this status should remain the same... as we are just checking the logs.
                    break;
                case "Cancel":
                    jobMonitorModel.UProcStatus = "Cancelled";
                    jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
                    break;
            }


            return RedirectToAction("Index", "JobMonitor");
        }

    }
}