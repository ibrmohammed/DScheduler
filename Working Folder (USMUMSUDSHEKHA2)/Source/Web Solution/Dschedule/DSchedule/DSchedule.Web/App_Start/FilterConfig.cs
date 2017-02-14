using System.Web;
using System.Web.Mvc;

namespace DSchedule.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //This will be reuired if we want to control authentication globally
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
