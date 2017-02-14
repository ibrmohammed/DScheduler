using DSchedule.BusinessComponent;
using DSchedule.Contracts.Models;
using DSchedule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Messaging;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace DSchedule.Web.Controllers
{
    public class UserAccountsController : Controller
    {
        UserBusinessComponent userComponent = new UserBusinessComponent();
        DScheduleBusinessComponent businessComponent = new DScheduleBusinessComponent();
        // GET: UserAccounts

        public ActionResult UserDetails()
        {
            //if (!Request.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            //else
            //{
            UserProfileCollection userProfileList = new UserProfileCollection();

            userProfileList.UserProfiles = userComponent.getUserDetails();
            userProfileList.Default = new UserProfile();
            var accountTypes = businessComponent.GetReferenceData("AccountTypeID");

            ViewBag.AccountTypes = businessComponent.GetRefTableValueDetails(accountTypes);
            if (userProfileList.UserProfiles != null)
            {
                foreach (UserProfile userInfo in userProfileList.UserProfiles)
                {
                    userInfo.AccountType = accountTypes.Single(x => x.RefValue == userInfo.AccountType).RefDescription;
                    userInfo.IsActive = userInfo.IsActive != null && userInfo.IsActive.Equals("1") ? "Yes" : "No";
                }
            }

            //MSMQ Demo
            var encoding = Encoding.UTF8;
            try
            {
                //MessageQueue myQueue;
                //Message myMessage = new System.Messaging.Message();

                //if (MessageQueue.Exists(@".\Private$\MyQueue"))
                //{
                //    myQueue = new MessageQueue(@".\Private$\MyQueue");
                //}
                //else
                //{
                //    myQueue = MessageQueue.Create(@".\Private$\MyQueue");
                //}
                //DSchedulerEntities usersDB = new DSchedulerEntities();
                //var json = new JavaScriptSerializer().Serialize(usersDB).Data); ;
                //myMessage.BodyStream = new MemoryStream(encoding.GetBytes(json));

                //myQueue.Send(myMessage);

                //myQueue = new MessageQueue(@".\Private$\MyQueue");
                //Message myMessge2 = myQueue.Receive();
                //var jsonResult = Read(myMessge2);

                //List<UserProfile> userP = JsonConvert.DeserializeObject<List<UserProfile>>(json);
            }
            catch (Exception ex)
            {

            }




            return View(userProfileList);
        }

        //private JsonResult getJson(DSchedulerEntities usersDB)
        //{
        //    return this.Json(
        //          (
        //            from obj in usersDB.TLogins select new { Id = obj.LoginID, Name = obj.FirstName, EmailId = obj.Email })
        //          , JsonRequestBehavior.AllowGet
        //          );
        //}

        //public object Read(Message message)
        //{
        //    if (message == null)
        //        throw new ArgumentNullException("message");

        //    using (var reader = new StreamReader(message.BodyStream))
        //    {
        //        var json = reader.ReadToEnd();
        //        return JsonConvert.DeserializeObject(json);
        //    }
        //}


        [HttpPost]
        public ActionResult CreateUser(UserProfileCollection userProfile)
        {
            //if (!Request.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            //else
            //{
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            if (ModelState.IsValid && userProfile.Default != null)
            {
                try
                {
                    userComponent.CreateNewUser(userProfile.Default, userName != null ? userName : string.Empty);
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("UserDetails", "UserAccounts");
            //  }
        }
        public ActionResult Edit(int id)
        {
            return PartialView(new UserProfile());
        }

        private dynamic GetOrganizations(UprocsModel uprocs)
        {
            uprocs.AccountTypeReferenceData.Add(new ReferenceData { RefName = "AccountTypeId", RefValue = "SuperAdmin", RefDescription = "SuperAdmin" });
            uprocs.AccountTypeReferenceData.Add(new ReferenceData { RefName = "AccountTypeId", RefValue = "Administrator", RefDescription = "Administrator" });
            uprocs.AccountTypeReferenceData.Add(new ReferenceData { RefName = "AccountTypeId", RefValue = "Developers", RefDescription = "Developers" });

            var accountTypes = uprocs.AccountTypeReferenceData
                        .OrderBy(x => x.RefDescription)
                        .ToList()
                        .Select(x => new SelectListItem
                        {
                            Text = x.RefDescription,
                            Value = x.RefValue.ToString()
                        });

            return new SelectList(accountTypes, "Value", "Text");
        }
      
    }
}