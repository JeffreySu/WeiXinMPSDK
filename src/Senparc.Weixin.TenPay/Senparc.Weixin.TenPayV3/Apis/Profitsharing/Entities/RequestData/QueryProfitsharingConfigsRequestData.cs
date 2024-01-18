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
  
    文件名：QueryProfitsharingConfigsRequestData.cs
    文件功能描述：查询最大分账比例请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询最大分账比例请求数据
    /// <para>服务商连锁品牌 详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_7_10.shtml </para>
    /// <para>服务商普通商户 详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_7.shtml </para>
    /// </summary>
    public class QueryProfitsharingConfigsRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingConfigsRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数(服务商模式-品牌连锁)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 二选一 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 二选一 <para>参考请求参数</para><para>示例值：1900000109</para></param>
        public QueryProfitsharingConfigsRequestData(string brand_mchid = "", string sub_mchid = "")
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
        }

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 二选一
        /// 连锁平台需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        [JsonIgnore]
        public string brand_mchid { get; set; }
        #endregion

        #region 服务商
        /// <summary>
        /// 子商户号 二选一
        /// <para>参考请求参数</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        [JsonIgnore]
        public string sub_mchid { get; set; }
        #endregion

    }




}
