using System.Web;
using System.Web.Mvc;

namespace PlannerTasks.WEB_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
