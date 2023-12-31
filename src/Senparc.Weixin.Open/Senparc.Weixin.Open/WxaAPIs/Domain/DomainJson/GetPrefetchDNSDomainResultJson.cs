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

    文件名：GetPrefetchDNSDomainResultJson.cs
    文件功能描述：获取DNS预解析域名 接口返回类型


    创建标识：Yaofeng - 20220809
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.Domain
{
    /// <summary>
    /// 获取DNS预解析域名 接口返回类型
    /// </summary>
    public class GetPrefetchDNSDomainResultJson : WxJsonResult
    {
        /// <summary>
        /// 预解析 dns 域名
        /// </summary>
        public List<GetPrefetchDNSDomainData> prefetch_dns_domain { get; set; }

        /// <summary>
        /// 总共可配置域名个数
        /// </summary>
        public int size_limit { get; set; }
    }
}
