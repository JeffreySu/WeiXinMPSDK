﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：RequestMessageEvent_VerifyExpired.cs
    文件功能描述：事件之认证过期失效通知
    
    
    创建标识：Senparc - 20170826

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之认证过期失效通知
    /// </summary>
    public class RequestMessageEvent_VerifyExpired : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.verify_expired; }
        }

        /// <summary>
        /// 有效期 (整形)，指的是时间戳，表示已于该时间戳认证过期，需要重新发起微信认证
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}
