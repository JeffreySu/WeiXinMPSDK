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
    
    文件名：UsageResultJson.cs
    文件功能描述：小程序 查询购买资源包的用量情况 返回结果
    
    
    创建标识：mc7246 - 20240831
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Charge
{
    /// <summary>
    /// 小程序-查询购买资源包的用量情况 返回结果
    /// </summary>
    public class UsageResultJson : WxJsonResult
    {
        /// <summary>
        /// 资源可用总量（64位数字），用于资源包类商品
        /// </summary>
        public string all {  get; set; }

        /// <summary>
        /// 资源总量（64位数字），用于资源包类商品
        /// </summary>
        public string effectiveAll { get; set; }

        /// <summary>
        /// 累计用量（64位数字），用于资源包类商品
        /// </summary>
        public string effectiveUse { get; set; }

        /// <summary>
        /// 订阅开始时间戳（单位：秒），用于订阅类商品
        /// </summary>
        public long startServiceTime { get; set; }

        /// <summary>
        /// 订阅结束时间戳（单位：秒），用于订阅类商品
        /// </summary>
        public long endServiceTime { get; set; }

        /// <summary>
        /// 用量详情列表总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 用量详情列表
        /// </summary>
        public List<UsageResultJson_DetailList> detailList { get; set; }
    }

    public class UsageResultJson_DetailList
    {
        /// <summary>
        /// 资源包ID
        /// </summary>
        public string pkgId { get; set; }

        /// <summary>
        /// 资源包状态，1生效中，2未生效，3已失效
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 额度有效期开始时间戳（单位：秒）
        /// </summary>
        public long startTime { get; set; }

        /// <summary>
        /// 额度有效期至结束时间戳（单位：秒）
        /// </summary>
        public long endTime { get; set; }

        /// <summary>
        /// 使用额度（64位数字）
        /// </summary>
        public string used { get; set; }

        /// <summary>
        /// 额度容量（64位数字）
        /// </summary>
        public string all { get; set; }

        /// <summary>
        /// 额度来源的商品SPU ID（64位数字）
        /// </summary>
        public string spuId {  get; set; }

        /// <summary>
        /// 额度来源的商品SKU ID（64位数字）
        /// </summary>
        public string skuId { get; set; }

        /// <summary>
        /// 额度来源，1体验额度，2付费购买，3服务商分配，4其他，5其他
        /// </summary>
        public int source { get; set; }

    }
}
