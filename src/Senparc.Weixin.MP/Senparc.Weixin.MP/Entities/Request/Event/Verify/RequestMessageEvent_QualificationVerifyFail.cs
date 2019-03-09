#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_QualificationVerifyFail.cs
    文件功能描述：事件之资质认证失败
    
    
    创建标识：Senparc - 20170826

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之资质认证成功（此时立即获得接口权限）
    /// </summary>
    public class RequestMessageEvent_QualificationVerifyFail : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.qualification_verify_fail; }
        }

        /// <summary>
        /// 有效期 (整形)，指的是时间戳，将于该时间戳认证过期
        /// </summary>
        public DateTimeOffset FailTime { get; set; }
        /// <summary>
        /// 失败发生时间 (整形)，时间戳
        /// </summary>

        public string FailReason { get; set; }
    }
}
