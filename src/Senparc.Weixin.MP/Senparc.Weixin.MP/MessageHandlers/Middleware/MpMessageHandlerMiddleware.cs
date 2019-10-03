

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.NeuChar.Middlewares;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
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
    public class MpMessageHandlerMiddleware<TMC> : MessageHandlerMiddleware<TMC, PostModel, ISenparcWeixinSettingForMP>
            where TMC : DefaultMpMessageContext, new()
    {
        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public MpMessageHandlerMiddleware(RequestDelegate next, Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForMP>> options)
            : base(next, messageHandler, options)
        {

        }

        public override async Task<bool> GetCheckSignature(HttpContext context)
        {
            var postModel = GetPostModel(context);
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
            {
                var echostr = GetEchostr(context);
                await context.Response.WriteAsync(echostr ?? "未提供 echostr 参数！").ConfigureAwait(false);
                return true;
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

                return false;
            }
        }


        public override async Task<bool> PostCheckSignature(HttpContext context)
        {
            var postModel = GetPostModel(context);
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("签名校验失败！").ConfigureAwait(false);
                return false;
            }
            return true;
        }

        public override string GetEchostr(HttpContext context)
        {
            return context.Request.Query["echostr"];
        }

        public override PostModel GetPostModel(HttpContext context)
        {
            var senparcWeixinSetting = base._accountSettingFunc(context);

            PostModel postModel = new PostModel()
            {
                Token = senparcWeixinSetting.Token,
                AppId = senparcWeixinSetting.WeixinAppId,
                EncodingAESKey = senparcWeixinSetting.EncodingAESKey,
                Signature = context.Request.Query["signature"],
                Timestamp = context.Request.Query["timestamp"],
                Nonce = context.Request.Query["nonce"],
                Msg_Signature = context.Request.Query["msg_signature"],
            };
            return postModel;
        }
    }

    //public static class MessageHandlerMiddlewareExtension
    //{
    //    /// <summary>
    //    /// 使用 MessageHandler 配置。注意：会默认使用异步方法 messageHandler.ExecuteAsync()。
    //    /// </summary>
    //    /// <param name="builder"></param>
    //    /// <param name="pathMatch">路径规则（路径开头，可带参数）</param>
    //    /// <param name="messageHandler"></param>
    //    /// <param name="options"></param>
    //    /// <returns></returns>
    //    public static IApplicationBuilder UseMpMessageHandler<TMC>(this IApplicationBuilder builder, PathString pathMatch,
    //        Func<Stream, PostModel, int, MessageHandler<TMC>> messageHandler, Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForMP>> options)
    //        where TMC : DefaultMpMessageContext, new()
    //    {
    //        return builder.Map(pathMatch, app =>
    //                {
    //                    app.UseMiddleware<MpMessageHandlerMiddleware<TMC>>(messageHandler, options);
    //                });
    //    }
    //}
}

#endif