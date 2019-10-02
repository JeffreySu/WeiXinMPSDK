#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0

using Microsoft.AspNetCore.Http;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers.Middleware
{
    /// <summary>
    /// MessageHandlerMiddleware 配置信息
    /// </summary>
    public class MessageHandlerMiddlewareOptions
    {
        /// <summary>
        /// 启用 RequestMessage 的日志记录
        /// </summary>
        public bool EnableRequestLog { get; set; } = true;
        /// <summary>
        /// 启用 ResponseMessage 的日志记录
        /// </summary>
        public bool EnbleResponseLog { get; set; } = true;

        /// <summary>
        /// 公众号的 SenparcWeixinSetting 信息
        /// </summary>
        public Func<HttpContext, ISenparcWeixinSettingForMP> SenparcWeixinSetting { get; set; }
    }
}

#endif