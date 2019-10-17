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
    
    文件名：GetAuditStatusResultJson.cs
    文件功能描述：查询审核状态返回结果
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 查询审核状态返回结果
    /// </summary>
    public class GetAuditStatusResultJson : WxJsonResult
    {
        public GetAuditStatusResultJson_Data data { get; set; }
    }

    public class GetAuditStatusResultJson_Data
    {
        /// <summary>
        /// 提交申请的时间戳
        /// </summary>
        public long apply_time { get; set; }
        /// <summary>
        /// 审核备注，包括审核不通过的原因
        /// </summary>
        public string audit_comment { get; set; }
        /// <summary>
        /// 审核状态。0：审核未通过、1：审核中、2：审核已通过；审核会在三个工作日内完成
        /// </summary>
        public int audit_status { get; set; }
        /// <summary>
        /// 确定审核结果的时间戳；若状态为审核中，则该时间值为0
        /// </summary>
        public long audit_time { get; set; }
    }
}
