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
  
    文件名：QueryMerchantsReturnJson.cs
    文件功能描述：查询代金券可用商户返回Json
    
    
    创建标识：Senparc - 20210901
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 查询代金券可用商户返回Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_7.shtml </para>
    /// </summary>
    public class QueryMerchantsReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 可用商户总数量
        /// </summary>
        public uint total_count { get; set; }

        /// <summary>
        /// 可用商户列表
        /// <para>特殊规则：单个商户号的字符长度为【1，20】,条目个数限制为【1，50】</para>
        /// </summary>
        public string[] data { get; set; }

        /// <summary>
        /// 分页页码
        /// </summary>
        public uint offset { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public uint limit { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string stock_id { get; set; }
    }
}
