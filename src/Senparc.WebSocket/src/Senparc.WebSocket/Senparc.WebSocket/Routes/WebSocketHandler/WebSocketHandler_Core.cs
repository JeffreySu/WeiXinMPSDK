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
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebSockets;
#endif


namespace Senparc.WebSocket
{

#if !NET45
    public partial class WebSocketHandler
    {
        public const int BufferSize = 4096;
        System.Net.WebSockets.WebSocket _socket;

        WebSocketHandler(System.Net.WebSockets.WebSocket socket)
        {
            this._socket = socket;
        }

        async Task EchoLoop()
        {
          await HandleMessage(_socket);
        }

        static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;
            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new WebSocketHandler(socket);
            await h.EchoLoop();
        }
        /// <summary>
        /// branches the request pipeline for this SocketHandler usage
        /// </summary>
        /// <param name="app"></param>
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(WebSocketHandler.Acceptor);
        }
    }
#endif
}
