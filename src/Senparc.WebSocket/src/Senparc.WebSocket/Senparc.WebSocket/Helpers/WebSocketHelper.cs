/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WebSocketHelper.cs
    文件功能描述：WebSocket处理类


    创建标识：Senparc - 20170127
    
    修改标识：Senparc - 20170522
    修改描述：v0.7.0 修改 DateTime 为 DateTimeOffset
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
using Microsoft.AspNetCore.SignalR;
using Senparc.WebSocket.SignalR;
#endif

namespace Senparc.WebSocket
{
    /// <summary>
    /// WebSocketHelper
    /// </summary>
    public class WebSocketHelper
    {
#if NET45
        public System.Net.WebSockets.WebSocket WebSocket { get; set; }
#else
        public SenparcWebSocketHubBase WebSocket { get; set; }
#endif


        private readonly CancellationToken _cancellationToken;


        /// <summary>
        /// WebSocketHelper
        /// </summary>
        ///// <param name="webSocketContext"></param>
        /// <param name="cancellationToken"></param>
#if NET45
        public WebSocketHelper(System.Net.WebSockets.WebSocket socket,/*AspNetWebSocketContext webSocketContext,*/ CancellationToken cancellationToken)
        {
            WebSocket = socket;
#else
        public WebSocketHelper(SenparcWebSocketHubBase hub, CancellationToken cancellationToken)
        {

            WebSocket = hub;
#endif
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">文字消息</param>
        /// <returns></returns>
#if NET45
        public async Task SendMessage(string message)
#else
        public async Task SendMessage(string message, IClientProxy clientProxy)
#endif
        {
            var data = new
            {
                content = message,
                time = DateTimeOffset.Now.DateTime.ToString()
            };

            var newString = Senparc.CO2NET.Helpers.SerializerHelper.GetJsonString(data);
            //String.Format("Hello, " + receiveString + " ! Time {0}", DateTimeOffset.Now.ToString());

#if NET45
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);
            await WebSocket.SendAsync(new ArraySegment<byte>(bytes),
                              WebSocketMessageType.Text, true, _cancellationToken).ConfigureAwait(false);
#else
            await WebSocket.SendAsync(newString, clientProxy, _cancellationToken).ConfigureAwait(false);
#endif
        }
    }
}
