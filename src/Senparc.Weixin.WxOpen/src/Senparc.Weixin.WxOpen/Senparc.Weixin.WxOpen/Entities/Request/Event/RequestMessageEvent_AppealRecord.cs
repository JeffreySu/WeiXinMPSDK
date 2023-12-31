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
    
    文件名：RequestMessageEvent_AppealRecord.cs
    文件功能描述：事件之小程序申诉记录推送
    
    
    创建标识：mc7246 - 20211209

----------------------------------------------------------------*/


using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之小程序审核失败
    /// </summary>
    public class RequestMessageEvent_AppealRecord : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.wxa_appeal_record; }
        }

        /// <summary>
        /// 申诉单id
        /// </summary>
        public string appeal_record_id { get; set; }

        /// <summary>
        /// 违规小程序APPID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 违规时间
        /// </summary>
        public string appeal_time { get; set; }

        /// <summary>
        /// 申诉次数
        /// </summary>
        public int appeal_count { get; set; }

        /// <summary>
        /// 申诉来源（0--用户，1--服务商）
        /// </summary>
        public int appeal_from { get; set; }

        /// <summary>
        /// 申诉状态，1正在处理，2申诉通过，3申诉不通过，申诉已撤销
        /// </summary>
        public int appeal_status { get; set; }

        /// <summary>
        /// 审核时间（“正在处理”或者“撤销”状态，不返回该字段）
        /// </summary>
        public string audit_time { get; set; }

        /// <summary>
        /// 审核结果理由（“正在处理”或者“撤销”状态，不返回该字段）
        /// </summary>
        public string audit_reason { get; set; }

        /// <summary>
        /// 处罚原因描述
        /// </summary>
        public string punish_description { get; set; }

        /// <summary>
        /// 违规材料和申诉材料
        /// </summary>
        public Material[] material { get; set; }

    }

    public class Material
    {
        public Illegal_MaterialInfo illegal_material { get; set; }

        public Appeal_Material appeal_material { get; set; }
    }

    /// <summary>
    /// 违规材料
    /// </summary>
    public class Illegal_MaterialInfo
    {
        /// <summary>
        /// 违规内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 违规链接
        /// </summary>
        public string content_url { get; set; }
    }
    /// <summary>
    /// 申诉材料
    /// </summary>
    public class Appeal_Material
    {
        /// <summary>
        /// 申诉理由
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 申诉材料id(可以通过“获取临时素材”接口下载对应的材料）
        /// </summary>
        public string proof_material_id { get; set; }
    }
}
