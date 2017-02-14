using DSchedule.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class UserProfile
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int LoginID { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string Organization { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }


        [DataType("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public virtual TRef TRef { get; set; }
    }
    public class UserProfileCollection
    {
        public List<UserProfile> UserProfiles { get; set; }

        public UserProfile Default
        {
            get;
            set;
        }
    }
}
