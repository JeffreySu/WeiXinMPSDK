#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
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
    public class MessageHandlerMiddleware<TMC>
        where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        private readonly RequestDelegate _next;
        private readonly Func<Stream, PostModel, int, MessageHandler<TMC>> _messageHandler;
        private readonly ISenparcWeixinSettingForMP _senparcWeixinSetting;

        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public MessageHandlerMiddleware(RequestDelegate next, Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler, ISenparcWeixinSettingForMP senparcWeixinSetting)
        {
            _next = next;
            _messageHandler = messageHandler;
            _senparcWeixinSetting = senparcWeixinSetting;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            PostModel postModel = new PostModel()
            {
                Token = _senparcWeixinSetting.Token,
                AppId = _senparcWeixinSetting.WeixinAppId,
                EncodingAESKey = _senparcWeixinSetting.EncodingAESKey,
                Signature = context.Request.Query["signature"],
                Timestamp = context.Request.Query["timestamp"],
                Nonce = context.Request.Query["nonce"]
            };
            string echostr = context.Request.Query["echostr"];

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
                        ? $"提供签名：{postModel.Signature} vs 正确签名：{CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token)}"
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
            else if (context.Request.Method.ToUpper() == "POST")
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
                {
                    await context.Response.WriteAsync("签名校验失败！");
                    return;
                }

                var cancellationToken = new CancellationToken();//给异步方法使用

                var messageHandler = _messageHandler(context.Request.GetRequestMemoryStream(), postModel, 10);

                #region 没有重写的异步方法将默认尝试调用同步方法中的代码（为了偷懒）

                /* 使用 SelfSynicMethod 的好处是可以让异步、同步方法共享同一套（同步）代码，无需写两次，
                 * 当然，这并不一定适用于所有场景，所以是否选用需要根据实际情况而定，这里只是演示，并不盲目推荐。*/
                messageHandler.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod;

                #endregion

                #region 设置消息去重 设置

                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

                #endregion

                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                await messageHandler.ExecuteAsync(cancellationToken); //执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

                string returnResult = null;
                if (_messageHandler is IMessageHandlerDocument messageHandlerDocument && messageHandlerDocument.TextResponseMessage != null)
                {
                    returnResult = messageHandlerDocument.TextResponseMessage.Replace("\r\n", "\n");

                    if (returnResult == null)
                    {
                        //使用IMessageHandler输出
                        if (messageHandlerDocument == null)
                        {
                            throw new Senparc.Weixin.Exceptions.WeixinException("执行 WeixinResult 时提供的 MessageHandler 不能为 Null！", null);
                        }
                        var finalResponseDocument = messageHandlerDocument.FinalResponseDocument;


                        if (finalResponseDocument == null)
                        {
                            //throw new Senparc.Weixin.MP.WeixinException("FinalResponseDocument不能为Null！", null);
                        }
                        else
                        {
                            returnResult = finalResponseDocument.ToString().Replace("\r\n", "\n");
                        }
                    }


                }
                context.Response.ContentType = "text/xml;charset=utf-8";
                returnResult = returnResult ?? "";
                await context.Response.WriteAsync(returnResult);
            }

            //不再继续向下执行
            //await _next(context).ConfigureAwait(false);
        }

    }

    public static class MessageHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseMessageHandler<TMC>(this IApplicationBuilder builder, PathString pathMatch, Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler, ISenparcWeixinSettingForMP senparcWeixinSetting)
            where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            return builder.Map(pathMatch, app =>
        {
            builder.UseMiddleware<MessageHandlerMiddleware<TMC>>(messageHandler, senparcWeixinSetting);
        });
        }
    }
}

#endif