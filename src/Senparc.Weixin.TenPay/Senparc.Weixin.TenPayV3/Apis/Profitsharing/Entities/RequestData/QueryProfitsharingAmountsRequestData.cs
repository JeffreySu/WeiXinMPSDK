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
  
    文件名：QueryProfitsharingAmountsRequestData.cs
    文件功能描述：查询分账剩余待分金额接口请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询分账剩余待分金额接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_6.shtml </para>
    /// </summary>
    public class QueryProfitsharingAmountsRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingAmountsRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>path微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        public QueryProfitsharingAmountsRequestData(string transaction_id)
        {
            this.transaction_id = transaction_id;
        }

        /// <summary>
        /// 含参构造函数(服务商模式-品牌连锁)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="transaction_id">微信订单号 <para>path微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        public QueryProfitsharingAmountsRequestData(string brand_mchid, string transaction_id)
        {
            this.brand_mchid = brand_mchid;
            this.transaction_id = transaction_id;
        }

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 
        /// 连锁平台需要 仅用于标志是否是连锁品牌分账，实际参数中不需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        [JsonIgnore]
        public string brand_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信订单号
        /// <para>path微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

    }




}
