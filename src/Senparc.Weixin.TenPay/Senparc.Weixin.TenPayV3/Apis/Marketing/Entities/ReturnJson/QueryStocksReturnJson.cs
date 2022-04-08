﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2022 Senparc
  
    文件名：QueryStocksReturnJson.cs
    文件功能描述：条件查询批次列表返回Json
    
    
    创建标识：Senparc - 20210823
    
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
    /// 条件查询批次列表返回Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_4.shtml </para>
    /// </summary>
    public class QueryStocksReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 批次总数
        /// <para>经过条件筛选，查询到的批次总数量</para>
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 批次详情
        /// </summary>
        public StockReturnJson[] data { get; set; }

        /// <summary>
        /// 分页大小，最大10
        /// </summary>
        public uint limit { get; set; }

        /// <summary>
        /// 分页页码
        /// <para>页码从0开始，默认第0页</para>
        /// </summary>
        public uint offset { get; set; }
    }
}
