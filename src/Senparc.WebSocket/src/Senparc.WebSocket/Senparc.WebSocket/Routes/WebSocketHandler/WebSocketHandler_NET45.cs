/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WebSocketHandler.cs
    文件功能描述：WebSocket处理程序


    创建标识：Senparc - 20170126

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.AspNetCore.WebSockets;

#if NET45
using System.Web;
using System.Web.Routing;
using System.Web.WebSockets;
#endif

namespace Senparc.WebSocket
{
#if NET45
    /// <summary>
    /// WebSocket处理程序
    /// </summary>
    public partial class WebSocketHandler : IHttpHandler
    {
        private RequestContext _requestContext;
        public WebSocketHandler(RequestContext context)
        {
            _requestContext = context;
            ProcessRequest(context);
        }

        private static void ProcessRequest(RequestContext requestContext)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //Checks if the query is WebSocket request. 
            var context = requestContext.HttpContext;
            if (context.IsWebSocketRequest)
            {
                //If yes, we attach the asynchronous handler.
                context.AcceptWebSocketRequest(WebSocketRequestHandler);
            }
        }

        /// <summary>
        /// 部分Handler过程参考：http://www.cnblogs.com/lookbs/p/MVC-IMG.html
        /// </summary>
        /// <param name="webSocketContext"></param>
        /// <returns></returns>
        public static async Task WebSocketRequestHandler(AspNetWebSocketContext webSocketContext)
        {
            //Gets the current WebSocket object.
            System.Net.WebSockets.WebSocket webSocket = webSocketContext.WebSocket;
            /*We define a certain constant which will represent
            size of received data. It is established by us and 
            we can set any value. We know that in this case the size of the sent
            data is very small.
            */
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();


            WebSocketHelper webSocketHandler = new WebSocketHelper(webSocket, cancellationToken);
            var messageHandler = WebSocketConfig.WebSocketMessageHandlerFunc.Invoke();

            if (webSocket.State == WebSocketState.Connecting)
            {
                if (WebSocketConfig.WebSocketMessageHandlerFunc != null)
                {
                    await messageHandler.OnConnecting(webSocketHandler).ConfigureAwait(false);//调用MessageHandler
                }
            }

            //Checks WebSocket state.
            await HandleMessage(webSocket).ConfigureAwait(false);
        }

        public void ProcessRequest(HttpContext context)
        {

        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
#endif
}
