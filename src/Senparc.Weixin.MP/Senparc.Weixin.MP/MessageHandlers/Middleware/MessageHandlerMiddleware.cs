#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers.Middleware
{
    /// <summary>
    /// MessageHandler 中间件
    /// </summary>
    /// <typeparam name="TMC"></typeparam>
    public class MessageHandlerMiddleware<TMC>
        where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        private readonly RequestDelegate _next;
        private readonly Func<Stream, PostModel, int, MessageHandler<TMC>> _messageHandler;
        private readonly Func<HttpContext, ISenparcWeixinSettingForMP> _senparcWeixinSettingFunc;
        private readonly MessageHandlerMiddlewareOptions _options;

        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public MessageHandlerMiddleware(RequestDelegate next, Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler,
            Action<MessageHandlerMiddlewareOptions> options)
        {
            _next = next;
            _messageHandler = messageHandler;

            if (options == null)
            {
                throw new MessageHandlerException($"{nameof(options)} 参数必须提供！");
            }

            _options = new MessageHandlerMiddlewareOptions();
            options(_options);

            if (_options.SenparcWeixinSetting == null)
            {
                throw new MessageHandlerException($"{nameof(options)} 中必须对 SenparcWeixinSetting 进行配置！");
            }

            _senparcWeixinSettingFunc = _options.SenparcWeixinSetting;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var senparcWeixinSetting = _senparcWeixinSettingFunc(context);
            PostModel postModel = new PostModel()
            {
                Token = senparcWeixinSetting.Token,
                AppId = senparcWeixinSetting.WeixinAppId,
                EncodingAESKey = senparcWeixinSetting.EncodingAESKey,
                Signature = context.Request.Query["signature"],
                Timestamp = context.Request.Query["timestamp"],
                Nonce = context.Request.Query["nonce"]
            };

            string echostr = context.Request.Query["echostr"];

            // GET 验证
            if (context.Request.Method.ToUpper() == "GET")
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
                {
                    await context.Response.WriteAsync(echostr ?? "未提供 echostr 参数！");
                }
                else
                {
                    string signature = context.Request.IsLocal()
                        ? $"提供签名：{postModel.Signature}<br />正确签名：{CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token)}"
                        : "非本地访问，无法显示签名信息，请在服务器本机查看！";

                    await context.Response.WriteAsync($@"服务器 token 签名校验失败！<br>
<h2>签名信息</h2>
{signature}<br /><br />
<h2>提示</h2>
如果你在浏览器中打开并看到这句话，那么看到这条消息<span style=""color:#f00"">并不能说明</span>你的程序有问题，
而是意味着此地址可以被作为微信公众账号后台的 Url，并开始进行官方的对接校验，请注意保持 Token 设置的一致。");

                    //TODO:给文档链接
                }
            }
            // POST 消息请求
            else if (context.Request.Method.ToUpper() == "POST")
            {
                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
                {
                    context.Response.ContentType = "text/plain;charset=utf-8";
                    await context.Response.WriteAsync("签名校验失败！");
                    return;
                }

                var cancellationToken = new CancellationToken();//给异步方法使用

                var messageHandler = _messageHandler(context.Request.GetRequestMemoryStream(), postModel, _options.MaxRecordCount);


                #region 没有重写的异步方法将默认尝试调用同步方法中的代码（为了偷懒）

                /* 使用 SelfSynicMethod 的好处是可以让异步、同步方法共享同一套（同步）代码，无需写两次，
                 * 当然，这并不一定适用于所有场景，所以是否选用需要根据实际情况而定，这里只是演示，并不盲目推荐。*/
                messageHandler.DefaultMessageHandlerAsyncEvent = _options.DefaultMessageHandlerAsyncEvent;

                #endregion

                #region 设置消息去重 设置

                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

                #endregion

                if (_options.EnableRequestLog)
                {
                    messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）
                }

                await messageHandler.ExecuteAsync(cancellationToken); //执行微信处理过程（关键）

                if (_options.EnbleResponseLog)
                {
                    messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）
                }

                string returnResult = null;
                //使用IMessageHandler输出
                if (messageHandler is IMessageHandlerDocument messageHandlerDocument)
                {
                    //先从 messageHandlerDocument.TextResponseMessage 中取值
                    returnResult = messageHandlerDocument.TextResponseMessage?.Replace("\r\n", "\n");

                    if (returnResult == null)
                    {
                        var finalResponseDocument = messageHandlerDocument.FinalResponseDocument;

                        if (finalResponseDocument != null)
                        {
                            returnResult = finalResponseDocument.ToString()?.Replace("\r\n", "\n");
                        }
                        else
                        {
                            //throw new Senparc.Weixin.MP.WeixinException("FinalResponseDocument不能为Null！", null);
                        }
                    }
                }
                else
                {
                    throw new Senparc.Weixin.Exceptions.WeixinException("执行 WeixinResult 时提供的 MessageHandler 不能为 Null！", null);
                }

                returnResult = returnResult ?? "";

                SenparcTrace.SendCustomLog("MessageHandler 中间件返回消息", returnResult);

                context.Response.ContentType = "text/xml;charset=utf-8";
                await context.Response.WriteAsync(returnResult);
            }

            //不再继续向下执行
            //await _next(context).ConfigureAwait(false);
        }

    }

    public static class MessageHandlerMiddlewareExtension
    {
        /// <summary>
        /// 使用 MessageHandler 配置。注意：会默认使用异步方法 messageHandler.ExecuteAsync()。
        /// </summary>
        /// <typeparam name="TMC">上下文消息类型</typeparam>
        /// <param name="builder"></param>
        /// <param name="pathMatch">路径规则（路径开头，可带参数）</param>
        /// <param name="messageHandler"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMessageHandler<TMC>(this IApplicationBuilder builder, PathString pathMatch,
            Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler, Action<MessageHandlerMiddlewareOptions> options)
            where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            return builder.Map(pathMatch, app =>
        {
            builder.UseMiddleware<MessageHandlerMiddleware<TMC>>(messageHandler, options);
        });
        }
    }
}

#endif