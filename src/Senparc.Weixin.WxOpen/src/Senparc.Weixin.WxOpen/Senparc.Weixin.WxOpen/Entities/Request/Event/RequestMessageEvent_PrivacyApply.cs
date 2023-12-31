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
    
    文件名：RequestMessageEvent_WeAppAuditSuccess.cs
    文件功能描述：事件之隐私权限审核结果推送
    
    
    创建标识：Senparc - 2010828

    修改标识：mc7246 - 20220504
    修改描述：vv3.15.2 添加小程序隐私接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之隐私权限审核结果推送
    /// </summary>
    public class RequestMessageEvent_PrivacyApply : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.wxa_privacy_apply; }
        }

        /// <summary>
        /// 审核结果
        /// </summary>
        public PrivacyApplyResultInfo result_info { get; set; }
    }

    public class PrivacyApplyResultInfo
    {
        public string api_name { get; set; }

        public uint apply_time { get; set; }

        public uint audit_id { get; set; }

        public uint audit_time { get; set; }

        public string reason { get; set; }

        /// <summary>
        /// 2-审核不通过,3-审核通过
        /// </summary>
        public string status { get; set; }
    }
}
