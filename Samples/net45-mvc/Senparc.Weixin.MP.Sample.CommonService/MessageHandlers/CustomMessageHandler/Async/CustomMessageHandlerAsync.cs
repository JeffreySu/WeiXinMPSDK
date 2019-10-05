/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CustomMessageHandlerAsync.cs
    文件功能描述：自定义MessageHandler（异步方法）
    
    
    创建标识：Senparc - 20191003
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.NeuChar.Entities;
using System.Threading;

#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

//TODO:提供异步上下文消息方法

namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            //测试MessageContext.StorageData

            var currentMessageContext = await base.GetCurrentMessageContext();
            if (currentMessageContext.StorageData == null || (currentMessageContext.StorageData is int))
            {
                currentMessageContext.StorageData = (int)0;
                await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存
            }
            await base.OnExecutingAsync(cancellationToken);
        }

        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            var currentMessageContext = await base.GetCurrentMessageContext();
            currentMessageContext.StorageData = ((int)currentMessageContext.StorageData) + 1;
            GlobalMessageContext.UpdateMessageContext(currentMessageContext);//储存到缓存
            await base.OnExecutedAsync(cancellationToken);
        }
    }
}