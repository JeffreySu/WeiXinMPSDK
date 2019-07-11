using Microsoft.AspNetCore.SignalR;
using Senparc.WebSocket.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.CoreSample.WebSocket.Hubs
{
    public class SenparcHub : SenparcWebSocketHubBase
    {
        /// <summary>
        /// 给普通网页用的自定义扩展方法 /WebScoket
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        ///// <summary>
        ///// 给小程序用
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public async Task SendText(string message)
        //{
        //    await Clients.Caller.SendAsync("ReceiveMessage", "您发送了文字：" + message);
        //    await Clients.Caller.SendAsync("ReceiveMessage", "正在处理中...");

        //    await Task.Delay(1000);//模拟停顿1秒

        //    //处理文字
        //    var result = string.Concat(message.Reverse());
        //    await Clients.Caller.SendAsync("ReceiveMessage", result);
        //}
    }
}
