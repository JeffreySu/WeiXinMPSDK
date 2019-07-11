#if NETSTANDARD2_0
using Microsoft.AspNetCore.SignalR;
using Senparc.WebSocket.Exceptions;
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
        protected WebSocketMessageHandler _webSocketMessageHandler;
        public SenparcWebSocketHubBase()
        {
            if (WebSocket.WebSocketConfig.WebSocketMessageHandlerFunc == null)
            {
                throw new WebSocketExceptionBase("WebSocket.WebSocketConfig.WebSocketMessageHandlerFunc 不能为 null！");
            }
            _webSocketMessageHandler = WebSocket.WebSocketConfig.WebSocketMessageHandlerFunc();
        }

        /// <summary>
        /// 默认客户端接收方法名称，默认为 ReceiveMessage
        /// </summary>
        public static string ClientFunctionName { get; set; } = "ReceiveMessage";

        /// <summary>
        /// 统一接收消息方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual async Task ReceiveMessage(string message)
        {
            var cancellationToken = new CancellationToken();
            var webSocketHelper = new WebSocketHelper(this, cancellationToken);
            var receivedMessage = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<ReceivedMessage>(message);
            await _webSocketMessageHandler.OnMessageReceiced(webSocketHelper, receivedMessage, message);//TODO：容错
        }

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