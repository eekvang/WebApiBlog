using System.Web.Mvc;
using WebApiBlog.Filters;

namespace WebApiBlog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}