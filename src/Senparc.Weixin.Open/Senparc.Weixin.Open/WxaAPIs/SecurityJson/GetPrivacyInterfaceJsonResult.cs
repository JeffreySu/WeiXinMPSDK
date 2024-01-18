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
    Copyright (C) 2020 Senparc

    文件名：GetPrivacyInterfaceJsonResult.cs
    文件功能描述：获取隐私接口列表结果


    创建标识：mc7246 - 20220504

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
    /// 获取隐私接口列表结果
    /// </summary>
    [Serializable]
    public class GetPrivacyInterfaceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 隐私接口
        /// </summary>
        public List<PrivacyInterfaceInfo> interface_list { get; set; }
    }

    [Serializable]
    public class PrivacyInterfaceInfo
    {
        /// <summary>
        /// api 英文名
        /// </summary>
        public string api_name { get; set; }

        /// <summary>
        /// api 中文名
        /// </summary>
        public string api_ch_name { get; set; }

        /// <summary>
        /// api描述
        /// </summary>
        public string api_desc { get; set; }

        /// <summary>
        /// 申请时间 ，该字段发起申请后才会有
        /// </summary>
        public uint apply_time { get; set; }

        /// <summary>
        /// 接口状态，该字段发起申请后才会有
        /// 1-待申请开通,2-无权限,3-申请中,4-申请失败,5-已开通
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 申请单号，该字段发起申请后才会有
        /// </summary>
        public uint audit_id { get; set; }

        /// <summary>
        /// 申请被驳回原因或者无权限，该字段申请驳回时才会有
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// api文档链接
        /// </summary>
        public string api_link { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string group_name { get; set; }
    }

}
