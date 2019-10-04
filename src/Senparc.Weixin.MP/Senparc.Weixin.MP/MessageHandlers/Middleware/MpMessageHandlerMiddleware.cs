

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar;
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
    public class MpMessageHandlerMiddleware<TMC, TS> : MessageHandlerMiddleware<TMC, PostModel, SenparcWeixinSetting>, IMessageHandlerMiddleware<TMC, PostModel, TS>
                where TMC : DefaultMpMessageContext, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
                //where TPM : PostModel, IEncryptPostModel
                where TS : class
    {
        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public MpMessageHandlerMiddleware(RequestDelegate next, Func<Stream, PostModel, int, MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<SenparcWeixinSetting>> options)
            : base(next, messageHandler, options)
        {

        }

        public override async Task<bool> GetCheckSignature(HttpContext context)
        {
            var postModel = GetPostModel(context);
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                var echostr = GetEchostr(context);
                if (string.IsNullOrEmpty(echostr))
                {
                    await context.Response.WriteAsync("未提供 echostr 参数！").ConfigureAwait(false);
                    return false;
                }
                else
                {
                    await context.Response.WriteAsync(echostr).ConfigureAwait(false);
                    return true;
                }
            }
            else
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                var currectSignature = CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token);
                var msgTip = base.GetGetCheckFaildMessage(context, currectSignature);
                await context.Response.WriteAsync(msgTip);
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

    /// <summary>
    /// 公众号 MessageHandlerMiddleware 扩展类，用于提供简洁的注册过程
    /// </summary>
    public static class MessageHandlerMiddlewareExtension
    {
        /// <summary>
        /// 使用 MessageHandler 配置。注意：会默认使用异步方法 messageHandler.ExecuteAsync()。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pathMatch">路径规则（路径开头，可带参数）</param>
        /// <param name="messageHandler"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMpMessageHandler<TMC>(this IApplicationBuilder builder, PathString pathMatch,
            Func<Stream, PostModel, int, MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<SenparcWeixinSetting>> options)
                where TMC : DefaultMpMessageContext, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            return builder.UseMessageHandler<MpMessageHandlerMiddleware<TMC, SenparcWeixinSetting>, TMC, PostModel, SenparcWeixinSetting>(pathMatch, messageHandler, options);
        }
    }

    ////证明泛型可以用在中间件中
    //public class TestWM<T>
    //    where T : class
    //{
    //    protected readonly RequestDelegate _next;
    //    protected readonly T _t;

    //    public TestWM(RequestDelegate next, T t)
    //    {
    //        _next = next;
    //        _t = t;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        await context.Response.WriteAsync(_t.GetType().Name);
    //    }


    //}

    //public static class TestWMExtension
    //{

    //    public static IApplicationBuilder UseTestWM<T>(this IApplicationBuilder builder, T t)
    //              where T : class

    //    {
    //        return builder.UseMiddleware<TestWM<T>>(t);
    //    }
    //}
}
#endif