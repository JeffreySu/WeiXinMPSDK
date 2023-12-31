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
  
    文件名：QueryProfitsharingConfigsReturnJson.cs
    文件功能描述：查询最大分账比例返回Json类
    
    
    创建标识：Senparc - 20230602
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询最大分账比例返回Json类
    /// <para>服务商连锁品牌 详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_7_10.shtml </para>
    /// <para>服务商普通商户 详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_7.shtml </para>
    /// </summary>
    public class QueryProfitsharingConfigsReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingConfigsReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>参考请求参数</para><para>示例值：1900000109</para></param>
        /// <param name="sub_mchid">子商户号 <para>参考请求参数</para><para>示例值：1900000109</para></param>
        /// <param name="max_ratio">最大分账比例 <para>子商户允许父商户分账的最大比例，单位万分比，比如2000表示20%</para><para>示例值：1000</para></param>
        public QueryProfitsharingConfigsReturnJson(string brand_mchid, string sub_mchid, int max_ratio)
        {
            this.brand_mchid = brand_mchid;
            this.max_ratio = max_ratio;
        }

        /// <summary>
        /// 子商户号
        /// <para>参考请求参数</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string brand_mchid { get; set; }

        /// <summary>
        /// 子商户号
        /// <para>参考请求参数</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 最大分账比例
        /// <para>子商户允许父商户分账的最大比例，单位万分比，比如2000表示20%</para>
        /// <para>示例值：2000</para>
        /// </summary>
        public int max_ratio { get; set; }

    }


}
