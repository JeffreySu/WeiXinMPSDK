/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：WebSocketRouteConfig.cs
    文件功能描述：自动配置WebSocket路由


    创建标识：Senparc - 20170126

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Senparc.WebSocket
{
    public class WebSocketConfig
    {
        internal static Func<WebSocketMessageHandler> WebSocketMessageHandlerFunc { get; set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("SenparcWebSocketRoute", new Route("SenparcWebSocket", new WebSocketRouteHandler()));//SenparcWebSocket/{app}
        }

        public static void RegisterMessageHandler<T>() where T : WebSocketMessageHandler, new()
        {
            WebSocketMessageHandlerFunc = () => new T();
        }
    }
}
