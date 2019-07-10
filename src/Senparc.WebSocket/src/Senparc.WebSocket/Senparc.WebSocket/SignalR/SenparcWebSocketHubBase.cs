#if NETSTANDARD2_0
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.WebSocket.SignalR
{
    public abstract class SenparcWebSocketHubBase : Hub
    {
        /// <summary>
        /// 默认客户端接收方法名称，默认为 ReceiveMessage
        /// </summary>
        public static string ClientFunctionName { get; set; } = "ReceiveMessage";

        //public virtual async Task ReceiveMessage(string message)
        //{
        //    WebSocketMessageHandler webSocketMessageHandler = new 
        //}

        /// <summary>
        /// 通用消息发送
        /// </summary>
        /// <param name="message">SenparcWebSocket 标准化信息</param>
        /// <returns></returns>
        public virtual async Task SendAsync(string message, IClientProxy clientProxy, CancellationToken cancellationToken)
        {
            await clientProxy.SendAsync(ClientFunctionName, message);
        }
    }
}
#endif