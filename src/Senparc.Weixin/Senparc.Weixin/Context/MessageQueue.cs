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
    
    文件名：MessageQueue.cs
    文件功能描述：微信消息队列（针对单个账号的往来消息）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/



using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    /// <summary>
    /// 微信消息队列（所有微信账号的往来消息）
    /// </summary>
    /// <typeparam name="TM">IMessageContext&lt;TRequest, TResponse&gt;</typeparam>
    /// <typeparam name="TRequest">IRequestMessageBase</typeparam>
    /// <typeparam name="TResponse">IResponseMessageBase</typeparam>
    public class MessageQueue<TM,TRequest, TResponse> : List<TM> 
        where TM : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
    }
}
