using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;

#if NET45

#else
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.WebSocket.SignalR;
#endif


namespace Senparc.WebSocket
{

#if !NET45

    //.NET Core(SignalR) 不需要

    //public partial class WebSocketHandler
    //{
    //    SenparcWebSocketHubBase _socket;

    //    WebSocketHandler(SenparcWebSocketHubBase socket)
    //    {
    //        this._socket = socket;
    //    }
    //}
#endif
}
