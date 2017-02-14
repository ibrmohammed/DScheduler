using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Common
{
    public class GlobalFunctions
    {
        public static string GetLoggedInUser()
        {
            string user = string.Empty;

            // For Now
            user = Environment.UserDomainName + Environment.UserName;

            return user;
        }

        public static DateTime GetDate()
        {
            DateTime now = DateTime.Now;

            return now;
        }
    }
}
