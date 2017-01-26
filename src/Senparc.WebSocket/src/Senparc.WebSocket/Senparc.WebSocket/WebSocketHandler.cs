/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：WebSocketHandler.cs
    文件功能描述：WebSocket处理程序


    创建标识：Senparc - 20170126

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.WebSockets;

namespace Senparc.WebSocket
{
    /// <summary>
    /// WebSocket处理程序
    /// </summary>
    public class WebSocketHandler : IHttpHandler
    {

        public WebSocketHandler(RequestContext context)
        {
            ProcessRequest(context);
        }

        private static void ProcessRequest(RequestContext requestContext)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //Checks if the query is WebSocket request. 
            var context = requestContext.HttpContext;
            if (context.IsWebSocketRequest)
            {
                //If yes, we attach the asynchronous handler.
                context.AcceptWebSocketRequest(WebSocketRequestHandler);
            }
        }

        public static async Task WebSocketRequestHandler(AspNetWebSocketContext webSocketContext)
        {
            //Gets the current WebSocket object.
            System.Net.WebSockets.WebSocket webSocket = webSocketContext.WebSocket;

            /*We define a certain constant which will represent
            size of received data. It is established by us and 
            we can set any value. We know that in this case the size of the sent
            data is very small.
            */
            const int maxMessageSize = 1024;

            //Buffer for received bits.
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

            var cancellationToken = new CancellationToken();

            //Checks WebSocket state.
            while (webSocket.State == WebSocketState.Open)
            {
                //Reads data.
                WebSocketReceiveResult webSocketReceiveResult =
                  await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                //If input frame is cancelation frame, send close command.
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                      String.Empty, cancellationToken);
                }
                else
                {
                    byte[] payloadData = receivedDataBuffer.Array
                        .Where(b => b != 0)
                        .Take(webSocketReceiveResult.Count)
                        .ToArray();

                    //Because we know that is a string, we convert it.
                    string receiveString =
                      //System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);
                      System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);

                    //Converts string to byte array.
                    var data = new
                    {
                        content = receiveString,
                        time = DateTime.Now.ToString()
                    };

                    var newString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    //String.Format("Hello, " + receiveString + " ! Time {0}", DateTime.Now.ToString());

                    Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);

                    //Sends data back.
                    await webSocket.SendAsync(new ArraySegment<byte>(bytes),
                      WebSocketMessageType.Text, true, cancellationToken);
                }
            }
        }

        public void ProcessRequest(HttpContext context)
        {

        }

        public bool IsReusable { get; }
    }
}
