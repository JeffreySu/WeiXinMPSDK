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
    
    文件名：UpdateContactWayRequest.cs
    文件功能描述：“更新企业已配置的「联系我」方式”接口 请求参数
    
    
    创建标识：Senparc - 20220918
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
#nullable enable

    /// <summary>
    /// “更新企业已配置的「联系我」方式”接口 请求参数
    /// </summary>
    public class UpdateContactWayRequest
    {
        /// <summary>
        /// 企业联系方式的配置id
        /// </summary>
        public string config_id { get; set; }
        /// <summary>
        /// 联系方式的备注信息，不超过30个字符，将覆盖之前的备注
        /// </summary>
        public string? remark { get; set; }
        /// <summary>
        /// 样式，只针对“在小程序中联系”的配置生效
        /// </summary>
        public bool? skip_verify { get; set; }
        /// <summary>
        /// 样式，只针对“在小程序中联系”的配置生效
        /// </summary>
        public int? style { get; set; }
        /// <summary>
        /// 企业自定义的state参数，用于区分不同的添加渠道，在调用“<see href="https://developer.work.weixin.qq.com/document/path/92228#13878">获取外部联系人详情</see>”时会返回该参数值
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 使用该联系方式的用户列表，将覆盖原有用户列表
        /// </summary>
        public string[]? user { get; set; }
        /// <summary>
        /// 使用该联系方式的部门列表，将覆盖原有部门列表，只在配置的type为2时有效
        /// </summary>
        public int[] party { get; set; }
        /// <summary>
        /// 临时会话二维码有效期，以秒为单位，该参数仅在临时会话模式下有效
        /// </summary>
        public int? expires_in { get; set; }
        /// <summary>
        /// 临时会话有效期，以秒为单位，该参数仅在临时会话模式下有效
        /// </summary>
        public int? chat_expires_in { get; set; }
        /// <summary>
        /// 可进行临时会话的客户unionid，该参数仅在临时会话模式有效，如不指定则不进行限制
        /// </summary>
        public string? unionid { get; set; }
        public Conclusions? conclusions { get; set; }
    }
#nullable disable

}
