using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Entity
{
    public class TLogin
    {
        public int LoginID { get; set; }
        public int AccountTypeID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }
        public string Organization { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }

        public virtual TRef TRef { get; set; }
    }
}
