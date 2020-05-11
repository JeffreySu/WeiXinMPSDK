using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;

#if NET45
using System.Web.Script.Serialization;
#else
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.WebSocket.SignalR;
#endif

namespace Senparc.WebSocket
{
    public partial class WebSocketHandler
    {
#if NET45
        private static async Task HandleMessage(System.Net.WebSockets.WebSocket webSocket)
        {
            //Gets the current WebSocket object.

            /*We define a certain constant which will represent
            size of received data. It is established by us and 
            we can set any value. We know that in this case the size of the sent
            data is very small.
            */
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();


            WebSocketHelper webSocketHandler = new WebSocketHelper(webSocket, /*webSocketContext, */cancellationToken);
            var messageHandler = WebSocketConfig.WebSocketMessageHandlerFunc.Invoke();

            while (webSocket.State == WebSocketState.Open)
            {
                //var incoming = await this._socket.ReceiveAsync(seg, CancellationToken.None);
                //var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                //await this._socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);


                //Reads data.
                WebSocketReceiveResult webSocketReceiveResult =
                  await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken).ConfigureAwait(false);

                //If input frame is cancelation frame, send close command.
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    if (WebSocketConfig.WebSocketMessageHandlerFunc != null)
                    {
                        await messageHandler.OnDisConnected(webSocketHandler).ConfigureAwait(false);//调用MessageHandler
                    }

                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                      String.Empty, cancellationToken).ConfigureAwait(false);
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


#if NET45
                                JavaScriptSerializer js = new JavaScriptSerializer();
                                receivedMessage = js.Deserialize<ReceivedMessage>(receiveString);
#else
                                receivedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceivedMessage>(receiveString);
#endif

                                
                            }
                            catch (Exception e)
                            {
                                receivedMessage = new ReceivedMessage()
                                {
                                    Message = receiveString// + " | 系统错误：" + e.Message
                                };
                            }
                            await messageHandler.OnMessageReceiced(webSocketHandler, receivedMessage, receiveString).ConfigureAwait(false);//调用MessageHandler
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }
            }
        }
#else
        //.NET Core（SignalR）不需要

        //protected static async Task HandleMessage(SenparcWebSocketHubBase webSocket, string message)
        //{
        //    var cancellationToken = new CancellationToken();
        //    WebSocketHelper webSocketHandler = new WebSocketHelper(webSocket, /*webSocketContext, */cancellationToken);
        //    var receivedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceivedMessage>(message);
        //    var messageHandler = WebSocketConfig.WebSocketMessageHandlerFunc.Invoke();
        //    //messageHandler.OnMessageReceiced(webSocketHandler,)
        //}
#endif




    }
}
