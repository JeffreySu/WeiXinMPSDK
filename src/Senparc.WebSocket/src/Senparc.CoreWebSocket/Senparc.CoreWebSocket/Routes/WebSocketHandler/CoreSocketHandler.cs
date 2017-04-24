using System;
using System.Linq;
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
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();


            WebSocketHelper webSocketHandler = new WebSocketHelper(_socket, /*webSocketContext, */cancellationToken);
            var messageHandler = WebSocketConfig.WebSocketMessageHandlerFunc.Invoke();

            while (this._socket.State == WebSocketState.Open)
            {
                //var incoming = await this._socket.ReceiveAsync(seg, CancellationToken.None);
                //var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                //await this._socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);


                //Reads data.
                WebSocketReceiveResult webSocketReceiveResult =
                  await _socket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                //If input frame is cancelation frame, send close command.
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    if (WebSocketConfig.WebSocketMessageHandlerFunc != null)
                    {
                        await messageHandler.OnDisConnected(webSocketHandler);//调用MessageHandler
                    }

                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                      String.Empty, cancellationToken);
                }
                else
                {
                    byte[] payloadData = receivedDataBuffer.Array
                        .Where(b => b != 0)
                        .Take(webSocketReceiveResult.Count)
                        .ToArray();

                    if (WebSocketConfig.WebSocketMessageHandlerFunc != null)
                    {
                        //Because we know that is a string, we convert it.
                        string receiveString =
                          //System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);
                          System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);
                        try
                        {
                            ReceivedMessage receivedMessage;
                            try
                            {
                                receivedMessage = new ReceivedMessage()
                                {
                                    Message = receiveString// + " | 系统错误：" + e.Message
                                };

                                receivedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceivedMessage>(receiveString);

                            }
                            catch (Exception e)
                            {
                                receivedMessage = new ReceivedMessage()
                                {
                                    Message = receiveString// + " | 系统错误：" + e.Message
                                };
                            }
                            await messageHandler.OnMessageReceiced(webSocketHandler, receivedMessage, receiveString);//调用MessageHandler
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }
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
