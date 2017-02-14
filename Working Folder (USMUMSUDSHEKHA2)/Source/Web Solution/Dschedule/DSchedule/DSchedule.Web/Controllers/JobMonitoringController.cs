using DSchedule.BusinessComponent;
using DSchedule.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSchedule.Web.Controllers
{
    public class JobMonitoringController : Controller
    {
        DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();
     
        // GET: JobMonitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUpdateJobView()
        {
            NewUpdateJob nm = businessComponent.GetJob();
            return View("NewUpdateJobView", nm);
        }

        [HttpPost]
        public ActionResult SaveUpdateJob(NewUpdateJob model)
        {
            if (model != null)
            {
                var success = businessComponent.SaveJob(model);
            }

            return RedirectToAction("NewUpdateJobView");
        }
    }
}