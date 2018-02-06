#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：MessageHandler.cs
    文件功能描述：微信请求【异步方法】的集中处理方法
    
    
    创建标识：Senparc - 20160122

----------------------------------------------------------------*/

/*
 * V4.0 添加异步方法
 */

using System;
using System.IO;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC, TRequest, TResponse> : IMessageHandler<TRequest, TResponse>
        where TC : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 默认参数设置为 DefaultResponseMessageAsync
        /// </summary>
        private DefaultMessageHandlerAsyncEvent _defaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync;

        /// <summary>
        /// <para>MessageHandler事件异步方法的默认调用方法（在没有override的情况下）。默认值：DefaultDefaultResponseMessageAsync。</para>
        /// <para>默认参数设置为 DefaultResponseMessageAsync，目的是为了确保默认状态下不会执行意料以外的代码，
        /// 因此，如果需要在异步方法中调用同名的同步方法，请手动将此参数设置为SelfSynicMethod。</para>
        /// </summary>
        public DefaultMessageHandlerAsyncEvent DefaultMessageHandlerAsyncEvent
        {
            get { return _defaultMessageHandlerAsyncEvent; }
            set { _defaultMessageHandlerAsyncEvent = value; }
        }

        public virtual async Task OnExecutingAsync()
        {
            await Task.Run(() => this.OnExecuting());
        }

        public virtual async Task ExecuteAsync()
        {
            await Task.Run(() => this.Execute());
        }

        public virtual async Task OnExecutedAsync()
        {
            await Task.Run(() => this.OnExecuted());
        }

        #endregion
#endif

    }
}
