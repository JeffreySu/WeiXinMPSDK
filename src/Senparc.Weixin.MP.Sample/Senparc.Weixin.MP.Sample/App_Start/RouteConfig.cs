﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Senparc.Weixin.MP.Sample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute(".well-known/acme-challenge/{filename}");
            //routes.IgnoreRoute("SenparcWebSocket");

            routes.MapRoute(
                name: "Open",
                url: "Open/Callback/{appId}",
                defaults: new { controller = "Open", action = "Callback", appId = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //  name: "WebSocket",
            //  url: "SenparcWebSocket",
            //  defaults: new { controller = "SenparcWebSocket", action = "SenparcWebSocket" }
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}