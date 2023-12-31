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
    
    文件名：RequestMessageEvent_UserEnterTempSession.cs
    文件功能描述：事件之腾讯地图中创建门店的审核结果
    
    
    创建标识：Senparc - 20170107
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 腾讯地图中创建门店的审核结果
    /// </summary>
    public class RequestMessageEvent_CreateMapPoiAuditInfo : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.create_map_poi_audit_info; }
        }

        /// <summary>
        /// 审核单id，即前面返回的base_id字段
        /// </summary>
        public string audit_id { get; set; }

        /// <summary>
        /// 审核状态（0：审核通过，1：审核失败）
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 从腾讯地图换取的位置点id
        /// </summary>
        public int map_poi_id { get; set; }

        /// <summary>
        /// 门店名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string sh_remark { get; set; }
    }
}
