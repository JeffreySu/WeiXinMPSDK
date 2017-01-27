using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.WebSocket;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket
{
    public class CustomWebSocketMessageHandler : WebSocketMessageHandler
    {
        public override async Task OnMessageReceiced(WebSocketHelper webSocketHandler, string message)
        {
            if (message == null)
            {
                return;
            }

            await webSocketHandler.SendMessage("您发送了文字："+message);

            await webSocketHandler.SendMessage("正在处理中...");

            await Task.Delay(1000);

            //预处理文字
            var result = message.Reverse().ToString();

            await webSocketHandler.SendMessage(result);
        }
    }
}
