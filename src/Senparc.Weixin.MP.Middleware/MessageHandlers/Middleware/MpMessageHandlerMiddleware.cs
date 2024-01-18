#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：MpMessageHandlerMiddleware.cs
    文件功能描述：公众号 MessageHandler 中间件
    
    
    创建标识：Senparc - 20191003
    
----------------------------------------------------------------*/

#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER || NET6_0_OR_GREATER
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar;
using Senparc.NeuChar.App.AppStore;
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
    /// 公众号 MessageHandler 中间件
    /// </summary>
    /// <typeparam name="TMC">上下文类型</typeparam>
    public class MpMessageHandlerMiddleware<TMC> : MessageHandlerMiddleware<TMC, PostModel, ISenparcWeixinSettingForMP>, IMessageHandlerMiddleware<TMC, PostModel, ISenparcWeixinSettingForMP>
                where TMC : DefaultMpMessageContext, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public MpMessageHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider, 
            Func<Stream, PostModel, int,IServiceProvider, MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForMP>> options)
            : base(next, serviceProvider, messageHandler, options)
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
                var correctSignature = CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token);
                var msgTip = base.GetGetCheckFaildMessage(context, postModel.Signature, correctSignature);
                await context.Response.WriteAsync(msgTip);
                return false;
            }
        }


        public override async Task<bool> PostCheckSignature(HttpContext context)
        {
            var postModel = GetPostModel(context);

            //CO2NET.Trace.SenparcTrace.SendCustomLog("PostCheckSignature", postModel.ToJson(true));

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
        /* 用法：
           startup.cs 中 Configure() 方法中加入，即可启用自定义的 CustomMessageHandler，无需任何 Controller 和多余代码：

           app.UseMpMessageHandler("/WeixinAsync", CustomMessageHandler.GenerateMessageHandler, o => o.AccountSettingFunc = c => senparcWeixinSetting.Value);
            );
         */

        /// <summary>
        /// 使用 MessageHandler 配置。注意：会默认使用异步方法 messageHandler.ExecuteAsync()。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="pathMatch">路径规则（路径开头，可带参数），此路径用于提供微信后台 Url 校验及消息推送</param>
        /// <param name="messageHandler"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMessageHandlerForMp<TMC>(this IApplicationBuilder builder, PathString pathMatch,
            Func<Stream, PostModel, int, IServiceProvider, MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForMP>> options)
                where TMC : DefaultMpMessageContext, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            return builder.UseMessageHandler<MpMessageHandlerMiddleware<TMC>, TMC, PostModel, ISenparcWeixinSettingForMP>(pathMatch, messageHandler, options);
        }
    }

#region 证明泛型可以用在中间件中
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
#endregion
}
#endif

