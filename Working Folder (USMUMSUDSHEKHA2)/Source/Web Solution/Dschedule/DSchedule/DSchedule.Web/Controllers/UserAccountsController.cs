using DSchedule.BusinessComponent;
using DSchedule.Contracts.Models;
using DSchedule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace DSchedule.Web.Controllers
{
    [Authorize]
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
                var accountTypes = businessComponent.GetAccountTypes();

                ViewBag.AccountTypes = GetAccountTypes(accountTypes);
                //ViewBag.Organization = GetOrganizations(uprocs);

                if (userProfileList.UserProfiles != null)
                {
                    foreach (UserProfile userInfo in userProfileList.UserProfiles)
                    {
                        userInfo.AccountType = accountTypes.Single(x => x.RefValue == userInfo.AccountType).RefDescription;
                        userInfo.IsActive = userInfo.IsActive!=null && userInfo.IsActive.Equals("1") ? "Yes" : "No";
                    }
                }

                return View(userProfileList);
           // }
        }



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
         [HttpGet]
        public PartialViewResult EditUser(int userId)
        {
            return PartialView();
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

        private dynamic GetAccountTypes(List<TRef> accountTypes)
        {

            var accountDetails = accountTypes.OrderBy(x => x.RefDescription)
                        .ToList()
                        .Select(x => new SelectListItem
                        {
                            Text = x.RefDescription,
                            Value = x.RefValue.ToString()
                        });

            return new SelectList(accountDetails, "Value", "Text");
        }
    }
}