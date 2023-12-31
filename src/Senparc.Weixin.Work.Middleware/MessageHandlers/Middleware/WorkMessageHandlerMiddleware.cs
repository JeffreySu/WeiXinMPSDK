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
    
    文件名：WxOpenMessageHandlerMiddleware.cs
    文件功能描述：公众号 MessageHandler 中间件
    
    
    创建标识：Senparc - 20191004
    
----------------------------------------------------------------*/

#if !NET462
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.MessageHandlers;
using Senparc.NeuChar.Middlewares;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageContexts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.MessageHandlers.Middleware
{
    /// <summary>
    /// 企业号 MessageHandler 中间件
    /// </summary>
    /// <typeparam name="TMC">上下文类型</typeparam>
    public class WorkMessageHandlerMiddleware<TMC>
        : MessageHandlerMiddleware<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase, PostModel, ISenparcWeixinSettingForWork>,
          IMessageHandlerMiddleware<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase, PostModel, ISenparcWeixinSettingForWork>
                where TMC : DefaultWorkMessageContext, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
    {
        /// <summary>
        /// EnableRequestRewindMiddleware
        /// </summary>
        /// <param name="next"></param>
        public WorkMessageHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider,
            Func<Stream, PostModel, int, IServiceProvider, MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForWork>> options)
            : base(next, serviceProvider, messageHandler, options)
        {

        }

        public override async Task<bool> GetCheckSignature(HttpContext context)
        {
            //Url 参数：msg_signature=64d481da37981dd88ab9926a82534f0222905286&timestamp=1570283669&nonce=1569937039&echostr=XwdIMYbHVlZl1V2ckFg6P4ScQytQrDaOG00fgu0SStDQWQstJ2ApLFdYV%2F2BtzZB1%2FISW0KhLqPBiQSaAVabVQ%3D%3D

            var postModel = GetPostModel(context);
            var echostr = this.GetEchostr(context);
            var canCheck = !string.IsNullOrEmpty(postModel.Msg_Signature) && !string.IsNullOrEmpty(postModel.Timestamp) && !string.IsNullOrEmpty(postModel.Nonce) && !string.IsNullOrEmpty(echostr);

            string verifyUrl = null;
            if (canCheck)
            {
                verifyUrl = Work.Signature.VerifyURL(postModel.Token, postModel.EncodingAESKey, postModel.CorpId, postModel.Msg_Signature /*这里调用方法的参数名称不明确*/,
                     postModel.Timestamp, postModel.Nonce, echostr);
            }

            if (verifyUrl != null)
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync(verifyUrl).ConfigureAwait(false);//返回解密后的随机字符串则表示验证通过
                return true;
            }
            else
            {
                context.Response.ContentType = "text/html;charset=utf-8";

                var correctSignature = !canCheck
                            ? "企业号中，Url 中的 timestamp, nonce, echostr 参数必须提供，否则无法进行签名验证！"
                            : Work.Signature.GenarateSinature(postModel.Token, postModel.Timestamp, postModel.Nonce, postModel.Msg_Signature/*此参数不能为空*/);
                var msgTip = base.GetGetCheckFaildMessage(context, postModel.Msg_Signature, correctSignature);
                await context.Response.WriteAsync(msgTip);
                return false;
            }
        }


        public override async Task<bool> PostCheckSignature(HttpContext context)
        {
            //Url 参数：msg_signature=3eea248d554c5ce1586f897ca3ca6b390d698254&timestamp=1570287086&nonce=1570089610

            var postModel = GetPostModel(context);

            Senparc.Weixin.Work.Tencent.WXBizMsgCrypt crypt = new Senparc.Weixin.Work.Tencent.WXBizMsgCrypt(postModel.Token, postModel.EncodingAESKey, postModel.CorpId);
            string replyEchoStr = null;
            var result = Senparc.Weixin.Work.Tencent.WXBizMsgCrypt.GenarateSinature(postModel.Token, postModel.Timestamp, postModel.Nonce, postModel.EncodingAESKey, ref replyEchoStr);

            if (result != 0)
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
                Token = senparcWeixinSetting.WeixinCorpToken,
                CorpId = senparcWeixinSetting.WeixinCorpId,
                CorpAgentId = senparcWeixinSetting.WeixinCorpAgentId,
                EncodingAESKey = senparcWeixinSetting.WeixinCorpEncodingAESKey,
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
        public static IApplicationBuilder UseMessageHandlerForWork<TMC>(this IApplicationBuilder builder, PathString pathMatch,
            Func<Stream, PostModel, int, IServiceProvider, MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>> messageHandler,
            Action<MessageHandlerMiddlewareOptions<ISenparcWeixinSettingForWork>> options)
                where TMC : DefaultWorkMessageContext, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
        {
            return builder.UseMessageHandler<WorkMessageHandlerMiddleware<TMC>, IWorkRequestMessageBase, IWorkResponseMessageBase, TMC, PostModel, ISenparcWeixinSettingForWork>(pathMatch, messageHandler, options);
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

