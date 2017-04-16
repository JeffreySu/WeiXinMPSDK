/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：WebSocketMessageHandler.cs
    文件功能描述：WebSocketMessageHandler基类


    创建标识：Senparc - 20170127

----------------------------------------------------------------*/

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
        public abstract Task OnConnecting(WebSocketHelper webSocketHandler);
        public abstract Task OnDisConnected(WebSocketHelper webSocketHandler);
        public abstract Task OnMessageReceiced(WebSocketHelper webSocketHandler, string message);
    }
}
