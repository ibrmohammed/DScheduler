using DSchedule.BusinessComponent;
using DSchedule.Contracts;
using DSchedule.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Environment = DSchedule.Contracts.Models.Environment;

namespace DSchedule.Web.Controllers
{
    [Authorize]
    public class JobDesignController : Controller
    {
        DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();

        #region Nodes
        public ActionResult NodesView()
        {
            try
            {
                NodesModel nm = businessComponent.GetNodes();
                return View("NodesView", nm);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNode(NodesModel model)
        {
            try
            {
                Node node = new Node();
                if (model != null)
                {
                    node.NodeID = model.NodeID;
                    node.NodeName = model.NodeName;
                    node.NodePath = model.Path;

                    var success = businessComponent.SaveNode(node);
                }

                return RedirectToAction("NodesView");
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Environment
        public ActionResult EnvironmentView()
        {
            try
            {
                EnvironmentModel model = new EnvironmentModel();
                model = businessComponent.GetEnvironments();
                return View("EnvironmentView", model);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEnvironment(EnvironmentModel transaction)
        {
            try
            {
                Environment environment = new Environment();

                if (environment != null)
                {
                    environment.EnvironmentID = transaction.EnvironmentID;
                    environment.EnvironmentName = transaction.EnvironmentName;
                    environment.NodeID = transaction.NodeID;
                    environment.EnvironmentPath = transaction.EnvironmentPath;

                    var success = businessComponent.SaveEnvironment(environment);
                }

                return RedirectToAction("EnvironmentView");
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Uprocs
        public ActionResult UprocsView()
        {
            try
            {
                UprocsModel uprocs = businessComponent.GetUprocs();
                uprocs = SetUprocsReferenceData(uprocs);
                ViewBag.JobRef = uprocs.JobTypeReferenceData;
                ViewBag.EnvironmentRef = uprocs.EnvironmentReferenceData;
                ViewBag.AccountRef = uprocs.AccountTypeReferenceData;
                if (uprocs.UprocsList.Any())
                {
                    foreach (var item in uprocs.UprocsList)
                    {
                        item.JobTypeName = uprocs.JobTypeReferenceData.Where(x => x.RefId == item.JobTypeID).FirstOrDefault().RefValue;
                        item.AccountName = uprocs.AccountTypeReferenceData.Where(x => x.RefId == item.AccountTypeID).FirstOrDefault().RefValue;
                        item.EnvironmentName = uprocs.EnvironmentReferenceData.Where(x => x.RefId == item.EnvironmentID).FirstOrDefault().RefValue;
                    }
                }
                return View("UprocsView", uprocs);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UprocsView(UprocsModel model)
        {
            try
            {
                UprocsModel uprocs = new UprocsModel();
                uprocs = SetUprocsReferenceData(uprocs);
                model.EnvironmentName = uprocs.EnvironmentReferenceData.FirstOrDefault(x => x.RefId == model.EnvironmentID).RefValue;
                ModelState.Remove("UprocsList");
                ModelState.Remove("JobTypeReferenceData");
                ModelState.Remove("EnvironmentReferenceData");
                ModelState.Remove("AccountTypeReferenceData");
                if (ModelState.IsValid)
                {
                    uprocs.UprocID = model.UprocID;
                    uprocs.EnvironmentID = model.EnvironmentID;
                    uprocs.JobTypeID = model.JobTypeID;
                    uprocs.AccountTypeID = model.AccountTypeID;
                    uprocs.UprocName = model.UprocName;
                    uprocs.EnvironmentName = model.EnvironmentName;
                    uprocs.FolderPath = model.FolderPath;
                    uprocs.Command = model.Command;
                    businessComponent.SaveUprocDetails(uprocs);
                    return RedirectToAction("UprocsView");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Session
        public ActionResult SessionsView()
        {
            try
            {
                SessionsModel sessionsModel = businessComponent.GetSessions();
                sessionsModel = SetSessionsReferenceData(sessionsModel);
                ViewBag.UprocsRef = sessionsModel.UprocsReferenceData;
                ViewBag.EnvironmentRef = sessionsModel.EnvironmentReferenceData;
                ViewBag.UserRef = sessionsModel.UserReferenceData;
                ViewBag.SessionsRef = sessionsModel.SessionsReferenceData;
                ViewBag.DependentUprocs = sessionsModel.DependentUprocsReferenceData;
                if (sessionsModel.SessionsList.Any())
                {
                    foreach (var item in sessionsModel.SessionsList)
                    {
                        item.Uproc = sessionsModel.UprocsReferenceData.Where(x => x.RefId == item.UprocId).FirstOrDefault().RefValue;
                        item.User = sessionsModel.UserReferenceData.Where(x => x.RefId == item.AccountTypeID).FirstOrDefault().RefValue;
                        item.Environment = sessionsModel.EnvironmentReferenceData.Where(x => x.RefId == item.EnvironmentID).FirstOrDefault().RefValue;
                    }
                }
                return View("SessionsView", sessionsModel);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SessionsView(SessionsModel model)
        {
            try
            {
                SessionsModel request = new SessionsModel();
                request = SetSessionsReferenceData(request);
                model.EnvironmentName = request.EnvironmentReferenceData.FirstOrDefault(x => x.RefId == model.EnvironmentID).RefValue;
                ModelState.Remove("SessionsList");
                ModelState.Remove("UprocsReferenceData");
                ModelState.Remove("EnvironmentReferenceData");
                ModelState.Remove("UserReferenceData");
                ModelState.Remove("SessionsReferenceData");
                ModelState.Remove("DependentUprocsReferenceData");
                if (ModelState.IsValid)
                {
                    request.SessionId = model.SessionId;
                    request.SessionName = model.SessionName;
                    request.EnvironmentID = model.EnvironmentID;
                    request.EnvironmentName = model.EnvironmentName;
                    request.AccountTypeID = model.AccountTypeID;
                    request.UprocID = model.UprocID;
                    request.DependentSessionId = model.DependentSessionId;
                    request.DependentUprocId = model.DependentUprocId;
                    businessComponent.SaveSessions(request);
                    return RedirectToAction("SessionsView");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region JobRules
        public ActionResult JobRulesView()
        {
            try
            {
                JobRulesModel jobRules = new JobRulesModel();
                jobRules = businessComponent.GetJobRules();
                List<ReferenceData> listRefData = businessComponent.GetSessionsReferenceData();
                jobRules.UprocsReferenceData = listRefData.Where(x => x.RefName.Equals("UprocsTypeID") || x.RefName.Equals("SessionsID")).ToList();
                ViewBag.UprocSessionRef = jobRules.UprocsReferenceData;
                ViewBag.ListOutageDetails = new List<SelectListItem>();
                return View(jobRules);
            }
            catch (Exception ex)
            {
                int ErrorId = ErrorLoggingBusinessComponent.SendExcepToDB(ex);
                return RedirectToAction("Error", "Home");
            }
        }
        //[HttpPost]
        //public ActionResult JobRulesView(JobRulesModel model)
        //{
        //    JobRulesModel viewobj = new JobRulesModel();
        //    viewobj = businessComponent.GetJobRules();
        //    List<JobRules> listJobRules = new List<JobRules>();
        //    List<ReferenceData> listRefData = businessComponent.GetSessionsReferenceData();
        //    viewobj.UprocsReferenceData = listRefData.Where(x => x.RefName.Equals("UprocsTypeID") || x.RefName.Equals("SessionsID")).ToList();
        //    ViewBag.UprocSessionRef = viewobj.UprocsReferenceData;
        //    ViewBag.ListOutageDetails = new List<SelectListItem>();
        //    viewobj.JobStartDate = model.JobStartDate;
        //    viewobj.ActivityTypeID = model.ActivityTypeID;
        //    viewobj.UprocOrSessionId = model.UprocOrSessionId;
        //    viewobj.JobRuleId = model.JobRuleId;
        //    ModelState.Remove("JobRulesList");
        //    ModelState.Remove("Outage");
        //    ModelState.Remove("UprocsReferenceData");
        //    ModelState.Remove("SessionsReferenceData");
        //    if (ModelState.IsValid)
        //    {
        //        businessComponent.SaveJobRule(viewobj);
        //        return RedirectToAction("JobRulesView");
        //    }
        //    else
        //    {
        //        return View(viewobj);
        //    }
        //}
        [HttpPost]
        public ActionResult JobRulesView(FormCollection fc, JobRulesModel model)
        {
            JobRulesModel viewobj = new JobRulesModel();
            List<Outage> listOutage = new List<Outage>();
            string[] indexes = fc.GetValues("Outage.index");
            if (indexes != null && indexes.Length > 0)
            {
                foreach (var index in indexes)
                {
                    listOutage.Add(new Outage()
                    {
                        OutageId = Convert.ToInt32(String.Format("Outage[" + index + "].OutageId").FirstOrDefault()),
                        OutageName = fc.GetValues("Outage[" + index + "].OutageName").FirstOrDefault(),
                        OutageDate = Convert.ToDateTime(fc.GetValues("Outage[" + index + "].OutageDate").FirstOrDefault()),
                        //OutageFrom = TimeSpan.ParseExact(fc.GetValues("Outage[" + index + "].OutageFrom").FirstOrDefault(), @"hh\:mm",CultureInfo.InvariantCulture) ,
                        //OutageTo = TimeSpan.ParseExact(fc.GetValues("Outage[" + index + "].OutageTo").FirstOrDefault(), @"hh\:mm", CultureInfo.InvariantCulture),
                    });
                }
            }
            viewobj.Outage = listOutage;
            List<JobRules> listJobRules = new List<JobRules>();
            List<ReferenceData> listRefData = businessComponent.GetSessionsReferenceData();
            viewobj.UprocsReferenceData = listRefData.Where(x => x.RefName.Equals("UprocsTypeID") || x.RefName.Equals("SessionsID")).ToList();
            ViewBag.UprocSessionRef = viewobj.UprocsReferenceData;
            ViewBag.ListOutageDetails = new List<SelectListItem>();
            viewobj.JobStartDate = model.JobStartDate;
            viewobj.ActivityTypeID = model.ActivityTypeID;
            viewobj.UprocOrSessionId = model.UprocOrSessionId;
            viewobj.IsRecurrence = model.IsRecurrence;
            viewobj.JobRuleId = model.JobRuleId;
            ModelState.Remove("JobRulesList");
            ModelState.Remove("Outage");
            ModelState.Remove("UprocsReferenceData");
            ModelState.Remove("SessionsReferenceData");
            if (ModelState.IsValid)
            {
                businessComponent.SaveJobRule(viewobj);
                return RedirectToAction("JobRulesView");
            }
            else
            {
                return View(viewobj);
            }
        }

        [HttpPost]
        public ActionResult _OutageView(List<Outage> Outage, string OutageName, DateTime? OutageDate, TimeSpan? OutageFrom, TimeSpan? OutageTo)
        {
            List<Outage> listOutage = Outage;
            listOutage.Add(
                new Outage
                {
                    OutageName = "new",
                    OutageDate = DateTime.Now,
                    OutageFrom = TimeSpan.MinValue,
                    OutageTo = TimeSpan.MaxValue,
                });
            return View("_OutageView", listOutage);
        }
        [HttpPost]
        public PartialViewResult Add(string OutageName, DateTime? OutageDate)
        {
            Outage o = new Outage { OutageName = OutageName, OutageDate = OutageDate };
            return PartialView("_OutageView", o);
        }
        #endregion
        [AllowAnonymous]
        public ActionResult GetSessionOrUproc(int id)
        {
            List<ReferenceData> listRefData = businessComponent.GetSessionsReferenceData();
            if (id == 9)
            {
                listRefData = listRefData.Where(x => x.RefName.Equals("UprocsTypeID")).ToList();
            }
            else if (id == 10)
            {
                listRefData = listRefData.Where(x => x.RefName.Equals("SessionsID")).ToList();
            }
            return Json(listRefData, JsonRequestBehavior.AllowGet);
        }
        #region Common
        public UprocsModel SetUprocsReferenceData(UprocsModel model)
        {
            List<ReferenceData> listRefData = businessComponent.GetUprocsReferenceData();
            model.JobTypeReferenceData = listRefData.Where(x => x.RefName.Equals("JobTypeID")).ToList();
            model.EnvironmentReferenceData = listRefData.Where(x => x.RefName.Equals("EnvironmentTypeID")).ToList();
            model.AccountTypeReferenceData = listRefData.Where(x => x.RefName.Equals("AccountTypeID")).ToList();
            return model;
        }
        public SessionsModel SetSessionsReferenceData(SessionsModel model)
        {
            List<ReferenceData> listRefData = businessComponent.GetSessionsReferenceData();
            model.UprocsReferenceData = listRefData.Where(x => x.RefName.Equals("UprocsTypeID")).ToList();
            model.EnvironmentReferenceData = listRefData.Where(x => x.RefName.Equals("EnvironmentTypeID")).ToList();
            model.UserReferenceData = listRefData.Where(x => x.RefName.Equals("AccountTypeID")).ToList();
            model.SessionsReferenceData = listRefData.Where(x => x.RefName.Equals("SessionsID")).ToList();
            model.DependentUprocsReferenceData = model.UprocsReferenceData;//listRefData.Where(x => x.RefName.Equals("DependentUprocsID")).ToList();
            return model;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel()
        {
            return View();
        }
        #endregion
    }
}