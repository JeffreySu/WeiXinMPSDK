/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WebSocketRoute.cs
    文件功能描述：WebSocket的Route类（主要为了重写GetVirtualPath，
                  返回null，从而不影响正常的URL生成）


    创建标识：Senparc - 20170126
    创建描述：v0.1.2 
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET45
using System.Web.Routing;
#endif

namespace Senparc.WebSocket
{
#if NET45
    /// <summary>
    /// WebSocketRoute
    /// </summary>
    public class WebSocketRoute : Route
    {
        /// <summary>
        /// WebSocketRoute
        /// </summary>
        /// <param name="url"></param>
        /// <param name="routeHandler"></param>
        public WebSocketRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {

        }
        /// <summary>
        /// GetVirtualPath
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
#endif
}
