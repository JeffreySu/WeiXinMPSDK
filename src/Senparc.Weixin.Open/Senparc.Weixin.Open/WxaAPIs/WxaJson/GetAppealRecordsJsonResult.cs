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
  
    文件名：GetAppealRecordsJsonResult.cs
    文件功能描述：“获取小程序申诉记录”接口返回结果
    
    
    创建标识：mc7246 - 20211209

    修改标识：Senparc - 20220224
    修改描述：v4.17.4 修复获取小程序申诉记录返回结果

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// “获取小程序申诉记录”接口返回结果
    /// </summary>
    public class GetAppealRecordsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 申诉记录列表
        /// </summary>
        public List<MaterialInfo> records { get; set; }
    }

    public class MaterialInfo
    {
        /// <summary>
        /// 申诉单id
        /// </summary>
        public string appeal_record_id { get; set; }

        /// <summary>
        /// 申诉时间
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
        /// 申诉状态，1正在处理，2申诉通过，3申诉不通过，4申诉已撤销
        /// </summary>
        public int appeal_status { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string audit_time { get; set; }

        /// <summary>
        /// 审核结果理由
        /// </summary>
        public string audit_reason { get; set; }

        /// <summary>
        /// 处罚原因描述
        /// </summary>
        public string punish_description { get; set; }

        /// <summary>
        /// 违规材料和申诉材料
        /// </summary>
        public List<Material> materials { get; set; }
    }

    public class Material
    {
        /// <summary>
        /// 违规材料
        /// </summary>
        public Illegal_Material illegal_material { get; set; }

        /// <summary>
        /// 申诉材料（针对违规材料提供的资料
        /// </summary>
        public Appeal_Material appeal_material { get; set; }
    }

    public class Illegal_Material
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

    public class Appeal_Material
    {
        /// <summary>
        /// 申诉理由
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 证明材料列表(可以通过“获取临时素材”接口下载对应的材料）
        /// </summary>
        public List<string> proof_material_ids { get; set; }
    }
}
