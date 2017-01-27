using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace Senparc.WebSocket
{
    public abstract class WebSocketMessageHandler
    {
        public abstract Task OnMessageReceiced(WebSocketHelper webSocketHandler, string message);
    }
}
