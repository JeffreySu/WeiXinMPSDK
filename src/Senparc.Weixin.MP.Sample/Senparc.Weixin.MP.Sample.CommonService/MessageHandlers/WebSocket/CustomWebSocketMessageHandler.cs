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
            await webSocketHandler.SendMessage(message);
        }
    }
}
