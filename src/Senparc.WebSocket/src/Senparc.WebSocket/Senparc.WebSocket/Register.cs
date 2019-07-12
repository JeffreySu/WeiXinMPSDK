using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.RegisterServices;
#if NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.WebSocket
{
    public static class Register
    {
        /// <summary>
        /// 注册基于 SignalR 的 Senparc.WebSocket。注册过程中会执行 services.AddSignalR();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSenparcWebSocket<T>(IServiceCollection services)
            where T : WebSocketMessageHandler, new()
        {
            services.AddSignalR();//使用 SignalR

            WebSocketConfig.RegisterMessageHandler<T>();//注册 WebSocket 消息处理程序
            return services;
        }
    }
}
#endif