using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}