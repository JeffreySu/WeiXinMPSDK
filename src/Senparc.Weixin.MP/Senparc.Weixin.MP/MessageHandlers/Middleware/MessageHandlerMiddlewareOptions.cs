#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0

using Microsoft.AspNetCore.Http;
using Senparc.NeuChar.MessageHandlers;
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
        /// 在没有 override 的情况下，MessageHandler 事件异步方法的默认调用方法
        /// </summary>
        public DefaultMessageHandlerAsyncEvent DefaultMessageHandlerAsyncEvent { get; set; } = DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync;

        /// <summary>
        /// 上下文最大纪录数量（默认为 10)
        /// </summary>
        public int MaxRecordCount { get; set; } = 10;

        /// <summary>
        /// 是否去重（默认为 true）
        /// </summary>
        public bool OmitRepeatedMessage { get; set; } = true;

        /// <summary>
        /// 公众号的 SenparcWeixinSetting 信息，必须包含 Token、AppId，以及 EncodingAESKey（如果有）
        /// </summary>
        public Func<HttpContext, ISenparcWeixinSettingForMP> SenparcWeixinSetting { get; set; }
    }
}

#endif