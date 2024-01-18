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
  
    文件名：ModifyServiceOrderRequestData.cs
    文件功能描述：微信支付V3修改订单金额接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3修改订单金额接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_17.shtml </para>
    /// </summary>
    public class ModifyServiceOrderRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_order_no">商户服务订单号  <para>path商户系统内部订单号（不是交易单号），与创建订单时一致。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="appid">应用ID  <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系。此参数需在本系统先进行配置，并与创建订单时的appid保持一致。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="service_id">服务ID  <para>body该服务ID有本接口对应产品的权限，需要与创建订单时保持一致。</para><para>示例值：500001</para></param>
        /// <param name="post_payments">后付费项目 <para>body后付费项目列表，最多包含100条付费项目。</para></param>
        /// <param name="post_discounts">后付费商户优惠 <para>body后付费商户优惠列表，最多包含30条商户优惠。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="total_amount">总金额  <para>body总金额，单位为分，不能超过完结订单时候的总金额，只能为整数，详见支付金额。此参数需满足：总金额=（修改后付费项目1…+修改后完结付费项目n）-（修改后付费商户优惠项目1…+修改后付费商户优惠项目n）</para><para>示例值：50000</para></param>
        /// <param name="reason">修改原因  <para>body按照字符计算，超过长度报错处理。</para><para>示例值：用户投诉</para></param>
        public ModifyServiceOrderRequestData(string out_order_no, string appid, string service_id, Post_Payment[] post_payments, Post_Discount[] post_discounts, long total_amount, string reason)
        {
            this.out_order_no = out_order_no;
            this.appid = appid;
            this.service_id = service_id;
            this.post_payments = post_payments;
            this.post_discounts = post_discounts;
            this.total_amount = total_amount;
            this.reason = reason;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyServiceOrderRequestData()
        {
        }

        /// <summary>
        /// 商户服务订单号 
        /// <para>path 商户系统内部订单号（不是交易单号），与创建订单时一致。 </para>
        /// <para>示例值：1234323JKHDFE1243252</para>
        /// </summary>
        [JsonIgnore]
        public string out_order_no { get; set; }

        /// <summary>
        /// 应用ID 
        /// <para>body 微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系。 此参数需在本系统先进行配置，并与创建订单时的appid保持一致。 </para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>body 该服务ID有本接口对应产品的权限，需要与创建订单时保持一致。 </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 后付费项目
        /// <para>body后付费项目列表，最多包含100条付费项目。 </para>
        /// </summary>
        public Post_Payment[] post_payments { get; set; }

        /// <summary>
        /// 后付费商户优惠
        /// <para>body后付费商户优惠列表，最多包含30条商户优惠。 如果传入，用户侧则显示此参数。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Post_Discount[] post_discounts { get; set; }

        /// <summary>
        /// 总金额 
        /// <para>body总金额，单位为分，不能超过完结订单时候的总金额，只能为整数，详见支付金额。此参数需满足：总金额 =（修改后付费项目1…+修改后完结付费项目n）-（修改 后付费商户优惠项目1…+修改后付费商户优惠项目n） </para>
        /// <para>示例值：50000 </para>
        /// </summary>
        public long total_amount { get; set; }

        /// <summary>
        /// 修改原因 
        /// <para>body按照字符计算，超过长度报错处理。 </para>
        /// <para>示例值：用户投诉</para>
        /// </summary>
        public string reason { get; set; }

        #region 子数据类型
        public class Post_Payment
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">付费项目名称  <para>付费项目名称不能重复，不超过20个字符，超出报错处理。</para><para>示例值：就餐费用,服务费</para></param>
            /// <param name="amount">金额  <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。</para><para>示例值：40000</para></param>
            /// <param name="description">计费说明  <para>描述计费规则，不超过30个字符，超出报错处理。</para><para>示例值：就餐人均100元，服务费：100/小时</para><para>可为null</para></param>
            /// <param name="count">付费数量  <para>付费项目的数量。</para><para>特殊规则：数量限制100，不填时默认1</para><para>示例值：4</para><para>可为null</para></param>
            public Post_Payment(string name, long amount, string description, uint? count)
            {
                this.name = name;
                this.amount = amount;
                this.description = description;
                this.count = count;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Post_Payment()
            {
            }

            /// <summary>
            /// 付费项目名称 
            /// <para>付费项目名称不能重复，不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：就餐费用, 服务费 </para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 金额 
            /// <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。</para>
            /// <para>示例值：40000</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 计费说明 
            /// <para>描述计费规则，不超过30个字符，超出报错处理。 </para>
            /// <para>示例值：就餐人均100元，服务费：100/小时 </para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 付费数量 
            /// <para>付费项目的数量。 </para>
            /// <para>特殊规则：数量限制100，不填时默认1</para>
            /// <para>示例值：4</para>
            /// <para>可为null</para>
            /// </summary>
            public uint? count { get; set; }

        }

        public class Post_Discount
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">优惠名称  <para>优惠名称说明；name和description若填写，则必须同时填写。</para><para>示例值：满20减1元</para><para>可为null</para></param>
            /// <param name="description">优惠说明  <para>优惠使用条件说明。name和description若填写，则必须同时填写。</para><para>示例值：不与其他优惠叠加</para></param>
            /// <param name="amount">优惠金额  <para>优惠金额；单位为分，只能为整数，详见支付金额。</para><para>示例值：100</para></param>
            /// <param name="count">优惠数量  <para>优惠的数量。</para><para>特殊规则：数量限制100，不填时默认1。</para><para>示例值：2</para><para>可为null</para></param>
            public Post_Discount(string name, string description, long amount, uint count)
            {
                this.name = name;
                this.description = description;
                this.amount = amount;
                this.count = count;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Post_Discount()
            {
            }

            /// <summary>
            /// 优惠名称 
            /// <para>优惠名称说明；name和description若填写，则必须同时填写。 </para>
            /// <para>示例值：满20减1元</para>
            /// <para>可为null</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠说明 
            /// <para>优惠使用条件说明。 name和description若填写，则必须同时填写。</para>
            /// <para>示例值：不与其他优惠叠加 </para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 优惠金额 
            /// <para>优惠金额；单位为分，只能为整数，详见支付金额。</para>
            /// <para>示例值：100 </para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 优惠数量 
            /// <para>优惠的数量。</para>
            /// <para>特殊规则：数量限制100，不填时默认1。</para>
            /// <para>示例值：2 </para>
            /// <para>可为null</para>
            /// </summary>
            public uint count { get; set; }

        }


        #endregion
    }




}
