using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Senparc.Weixin.MP.Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "WeixinApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "WeixinService", action = "Get", id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
