using DSchedule.BusinessComponent;
using DSchedule.Contracts;
using DSchedule.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Environment = DSchedule.Contracts.Models.Environment;

namespace DSchedule.Web.Controllers
{
    public class JobDesignController : Controller
    {
        DScheduleBusinessComponent businessComponent= new DScheduleBusinessComponent();

        public ActionResult NodesView()
        {
            NodesModel nm = businessComponent.GetNodes();
            return View("NodesView", nm);
        }

        [HttpPost]
        public ActionResult SaveNode(NodesModel model)
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

        public ActionResult EnvironmentView()
        {
            EnvironmentModel model = new EnvironmentModel();
            model = businessComponent.GetEnvironments();
            return View("EnvironmentView", model);
        }

        [HttpPost]
        public ActionResult SaveEnvironment(EnvironmentModel transaction)
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

        [HttpPost]
        public ActionResult Cancel()
        {
            return View();
        }

        public ActionResult UprocsView()
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
            return View("UprocsView",uprocs);
        }

        [HttpPost]
        public ActionResult UprocsView(UprocsModel model)
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
        
        public ActionResult SessionsView()
        {
            SessionsModel sessionsModel = businessComponent.GetSessions();
            sessionsModel = SetSessionsReferenceData(sessionsModel);
            ViewBag.UprocsRef = sessionsModel.UprocsReferenceData;
            ViewBag.EnvironmentRef = sessionsModel.EnvironmentReferenceData;
            ViewBag.UserRef = sessionsModel.UserReferenceData;
            ViewBag.SessionsRef = sessionsModel.SessionsReferenceData;
            ViewBag.DependentUprocs = sessionsModel.DependentUprocsReferenceData;
            if(sessionsModel.SessionsList.Any())
            {
                foreach (var item in sessionsModel.SessionsList)
                {
                    //item.Uproc = sessionsModel.UprocsReferenceData.Where(x => x.RefId == item.UprocId).FirstOrDefault().RefValue;
                    item.User = sessionsModel.UserReferenceData.Where(x => x.RefId == item.AccountTypeID).FirstOrDefault().RefValue;
                    item.Environment = sessionsModel.EnvironmentReferenceData.Where(x => x.RefId == item.EnvironmentID).FirstOrDefault().RefValue;
                }
            }
            return View("SessionsView",sessionsModel);
        }
        [HttpPost]
        public ActionResult SessionsView(SessionsModel model)
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
                businessComponent.SaveSessions(request);
                return RedirectToAction("SessionsView");
            }
            return View(model);
        }
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
            model.DependentUprocsReferenceData = listRefData.Where(x => x.RefName.Equals("DependentUprocsID")).ToList();
            return model;
        }
    }
}