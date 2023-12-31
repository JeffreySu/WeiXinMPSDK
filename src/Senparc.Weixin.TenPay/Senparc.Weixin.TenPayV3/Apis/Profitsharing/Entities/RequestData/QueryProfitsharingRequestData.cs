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
  
    文件名：QueryProfitsharingRequestData.cs
    文件功能描述：查询分账结果请求数据
    
    
    创建标识：Senparc - 20230602

----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 查询分账结果请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_2.shtml </para>
    /// </summary>
    public class QueryProfitsharingRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryProfitsharingRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        public QueryProfitsharingRequestData(string transaction_id, string out_order_no)
        {
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        public QueryProfitsharingRequestData(string sub_mchid, string transaction_id, string out_order_no)
        {
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
        }

        /// <summary>
        /// 含参构造函数(服务商模式-连锁品牌)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        public QueryProfitsharingRequestData(string brand_mchid, string sub_mchid, string transaction_id, string out_order_no)
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
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

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信订单号
        /// <para>body微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ </para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        [JsonIgnore]
        public string out_order_no { get; set; }
    }
}
