using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;

namespace Senparc.WebSocket.Routes.WebSocketHandler
{

    public class CoreSocketHandler
    {
        public const int BufferSize = 4096;
        System.Net.WebSockets.WebSocket _socket;

        CoreSocketHandler(System.Net.WebSockets.WebSocket socket)
        {
            this._socket = socket;
        }

        async Task EchoLoop()
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);
            while (this._socket.State == WebSocketState.Open)
            {
                //var incoming = await this._socket.ReceiveAsync(seg, CancellationToken.None);
                //var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                //await this._socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);


            }
        }
        static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;
            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new CoreSocketHandler(socket);
            await h.EchoLoop();
        }
        /// <summary>
        /// branches the request pipeline for this SocketHandler usage
        /// </summary>
        /// <param name="app"></param>
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(CoreSocketHandler.Acceptor);
        }
    }
}
