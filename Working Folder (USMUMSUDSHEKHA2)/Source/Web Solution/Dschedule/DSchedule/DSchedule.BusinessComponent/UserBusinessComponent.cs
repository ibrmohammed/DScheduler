using DSchedule.Contracts.Models;
using DSchedule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.BusinessComponent
{
    public class UserBusinessComponent
    {
        DSchedulerEntities context = new DSchedulerEntities();
       

        public bool ValidateUser(string userName, string password)
        {
            bool isValid = false;
            using (var db = new DSchedulerEntities())
            {
                var user = db.TLogins.FirstOrDefault(u => u.UserName == userName
                                        && u.Password == password);
                if (user != null)
                {
                    isValid = true;
                }
            }
            return isValid;
        }



        public List<UserProfile> getUserDetails()
        {           
            using (var db = new DSchedulerEntities())
            {
                var userDBList = db.TLogins == null ? null : db.TLogins.Select(x => new UserProfile
                {
                   FirstName = x.FirstName,
                   LastName =x.LastName,
                   Email =x.Email,
                   IsActive =x.IsActive,
                   UserName = x.UserName,
                   AccountType = x.TRef.RefDescription,
                   Organization =x.Organization
                }).ToList();

                return userDBList;
            }
        }


       
        private IEnumerable<ReferenceData> GetAccountTypes()
        {
            //var accountTypes = context.AccountTypes
            //            .OrderBy(x => x.AccountTypeName)
            //            .ToList()
            //            .Select(x => new SelectListItem
            //            {
            //                Text = x.AccountTypeName,
            //                Value = x.AccountTypeId.ToString()
            //            });

            //return new SelectList(accountTypes, "Value", "Text");
            return null;
        }

        public void CreateNewUser(UserProfile userProfile, string userName)
        {
            TLogin newUser = ConvertUserToDBFactory(userProfile, userName);
            context.TLogins.Add(newUser);
            context.SaveChanges();

        }

        private TLogin ConvertUserToDBFactory(UserProfile userProfile, string userName)
        {
            TLogin dbUserProfile = new TLogin();
            if (userProfile != null)
            {
                dbUserProfile.AccountTypeID = 2;
                dbUserProfile.CreatedDateTime = DateTime.Now;
                dbUserProfile.CreatedBy = userName;
                dbUserProfile.Email = userProfile.Email;
                dbUserProfile.FirstName = userProfile.FirstName;
                dbUserProfile.IsActive = "Y";
                dbUserProfile.LastName = userProfile.LastName;
                dbUserProfile.Organization = "1";
                dbUserProfile.Password = userProfile.Password;
                dbUserProfile.UpdatedDateTime = DateTime.Now;
                dbUserProfile.UpdatedBy = userName;
                dbUserProfile.UserName = userProfile.UserName;
                dbUserProfile.TRef = null;
            }
            return dbUserProfile;
        }

    }
}
