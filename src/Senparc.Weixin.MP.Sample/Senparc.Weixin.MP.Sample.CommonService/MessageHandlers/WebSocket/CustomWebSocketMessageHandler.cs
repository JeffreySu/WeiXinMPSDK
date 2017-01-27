using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.WebSocket;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket
{
    /// <summary>
    /// 自定义 WebSocket 处理类
    /// </summary>
    public class CustomWebSocketMessageHandler : WebSocketMessageHandler
    {
        public override Task OnConnecting(WebSocketHelper webSocketHandler)
        {
        }

        public override Task OnDisConnected(WebSocketHelper webSocketHandler)
        {
        }

        public override async Task OnMessageReceiced(WebSocketHelper webSocketHandler, string message)
        {
            if (message == null)
            {
                return;
            }

            await webSocketHandler.SendMessage("您发送了文字："+message);
            await webSocketHandler.SendMessage("正在处理中...");

            await Task.Delay(1000);

            //处理文字
            var result = string.Concat(message.Reverse());
            await webSocketHandler.SendMessage(result);
        }
    }
}
