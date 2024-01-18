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

    修改标识：mc7246 - 20211215
    修改描述：v4.13.3 修复获取小程序违规记录返回信息

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// “获取小程序违规处罚记录”接口返回结果
    /// </summary>
    public class GetIllegalRecordsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 违规处罚记录列表
        /// </summary>
        public List<IllegalInfo> records = new List<IllegalInfo>();
    }

    public class IllegalInfo
    {
        /// <summary>
        /// 违规处罚记录id
        /// </summary>
        public string illegal_record_id { get; set; }

        /// <summary>
        /// 违规处罚时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 违规原因
        /// </summary>
        public string illegal_reason { get; set; }

        /// <summary>
        /// 违规内容
        /// </summary>
        public string illegal_content { get; set; }

        /// <summary>
        /// 规则链接
        /// </summary>
        public string rule_url { get; set; }

        /// <summary>
        /// 违反的规则名称
        /// </summary>
        public string rule_name { get; set; }
    }
}
