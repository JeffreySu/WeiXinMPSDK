﻿#region Apache License Version 2.0
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
    
    文件名：RequestMessageEvent_NamingVerifyFail.cs
    文件功能描述：事件之名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
    
    
    创建标识：Senparc - 20170826

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
    /// </summary>
    public class RequestMessageEvent_NamingVerifyFail : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.naming_verify_fail; }
        }

        /// <summary>
        /// 失败发生时间 (整形)，时间戳
        /// </summary>
        public DateTime FailTime { get; set; }
        /// <summary>
        /// 认证失败的原因
        /// </summary>

        public string FailReason { get; set; }
    }
}
