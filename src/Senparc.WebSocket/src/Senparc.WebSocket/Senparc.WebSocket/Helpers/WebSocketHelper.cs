/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：WebSocketHelper.cs
    文件功能描述：WebSocket处理类


    创建标识：Senparc - 20170127

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if NET45
using System.Web.Routing;
using System.Web.WebSockets;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.WebSocket
{
    /// <summary>
    /// WebSocketHelper
    /// </summary>
    public class WebSocketHelper
    {
        //private readonly AspNetWebSocketContext _webSocketContext;
        private readonly System.Net.WebSockets.WebSocket _webSocket;
        private readonly CancellationToken _cancellationToken;


        /// <summary>
        /// WebSocketHelper
        /// </summary>
        ///// <param name="webSocketContext"></param>
        /// <param name="cancellationToken"></param>
#if NET45
        public WebSocketHelper(System.Net.WebSockets.WebSocket socket,/*AspNetWebSocketContext webSocketContext,*/ CancellationToken cancellationToken)
#else
         public WebSocketHelper(System.Net.WebSockets.WebSocket socket,/*AspNetWebSocketContext webSocketContext,*/ CancellationToken cancellationToken)
#endif
        {
            //_webSocketContext = webSocketContext;
            //_webSocket = webSocketContext.WebSocket;
            _webSocket = socket;
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">文字消息</param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            var data = new
            {
                content = message,
                time = DateTime.Now.ToString()
            };

            var newString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //String.Format("Hello, " + receiveString + " ! Time {0}", DateTime.Now.ToString());

            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);
            await _webSocket.SendAsync(new ArraySegment<byte>(bytes),
                              WebSocketMessageType.Text, true, _cancellationToken);
        }
    }
}
