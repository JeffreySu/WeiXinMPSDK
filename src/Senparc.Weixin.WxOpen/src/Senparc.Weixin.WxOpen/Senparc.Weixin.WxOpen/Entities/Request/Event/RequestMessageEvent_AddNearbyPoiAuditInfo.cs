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
    
    文件名：RequestMessageEvent_UserEnterTempSession.cs
    文件功能描述：事件之地点审核
    
    
    创建标识：Senparc - 20170107
    
    修改标识：Senparc - 20190615
    修改描述：修复附近的小程序添加地点，修改注释
----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之地点审核
    /// </summary>
    public class RequestMessageEvent_AddNearbyPoiAuditInfo : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.add_nearby_poi_audit_info; }
        }

        /// <summary>
        /// 审核单id
        /// </summary>
        public string audit_id { get; set; }

        /// <summary>
        /// 审核状态（3：审核通过，2：审核失败）
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 如果status为2，会返回审核失败的原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// poi_id
        /// </summary>
        public string poi_id { get; set; }
    }
}
