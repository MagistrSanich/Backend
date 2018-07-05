using System.Web;
using System.Web.Mvc;

namespace Pr_DataBase_5_student
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
