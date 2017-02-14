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
        JobMonitorModel model = new JobMonitorModel();
        public ActionResult Index()
        {
            model = jobMonitorBusinessComponent.GetJobMonitorDetails();
            return View("JobMonitor", model);
        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 15)] // every 3 sec
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

            DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();
            var jobActionTypes = businessComponent.GetReferenceData("JobActionTypeID");
            ViewBag.JobActionTypes = businessComponent.GetRefTableValueDetails(jobActionTypes);

           return PartialView("_UprocsDetailsPartialView",results);
        }

        public ActionResult UpdateUProcStatusForSession(JobMonitor jobMonitorModel)
        {
            jobMonitorBusinessComponent.UpdateUProcStatusInSession(jobMonitorModel);
            model = jobMonitorBusinessComponent.GetJobMonitorDetails();
            return View("JobMonitor", model);
        }
    }
}