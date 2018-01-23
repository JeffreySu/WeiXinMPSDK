/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：CustomMessageHandler_Events.cs
    文件功能描述：自定义MessageHandler
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.Context;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers.Extensions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;


#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif


namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        public override Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            return Task.Factory.StartNew<IResponseMessageBase>(() =>
            {
                var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "这是一条来自【异步MessageHandler】方法的回复";

                //常识获取Click事件的同步方法
                var syncResponseMessage = OnEvent_ClickRequest(requestMessage);
                if (syncResponseMessage is ResponseMessageText)
                {
                    responseMessage.Content += "\r\n\r\n以下是同步方法的返回结果：" + (syncResponseMessage as ResponseMessageText).Content;
                }
                return responseMessage;
            });
        }
    }
}