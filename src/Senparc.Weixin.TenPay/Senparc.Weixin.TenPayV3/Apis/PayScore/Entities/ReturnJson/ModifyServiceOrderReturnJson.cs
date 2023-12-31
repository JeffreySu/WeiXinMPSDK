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
  
    文件名：ModifyServiceOrderReturnJson.cs
    文件功能描述：修改订单金额返回Json类
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 修改订单金额返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_17.shtml </para>
    /// </summary>
    public class ModifyServiceOrderReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID  <para>调用接口提交的公众账号ID。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="mchid">商户号  <para>调用接口提交的商户号。</para><para>示例值：1230000109</para></param>
        /// <param name="service_id">服务ID  <para>调用该接口提交的服务ID。</para><para>示例值：500001</para></param>
        /// <param name="out_order_no">商户服务订单号  <para>调用接口提交的商户服务订单号。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="service_introduction">服务信息  <para>服务信息用于介绍本订单所提供的服务，当参数长度超过20个字符时，报错处理。</para><para>示例值：某某酒店</para></param>
        /// <param name="state">服务订单状态  <para>表示当前单据状态。枚举值：CREATED：商户已创建服务订单DOING：服务订单进行中DONE：服务订单完成REVOKED：商户取消服务订单 EXPIRED：服务订单已失效，"商户已创建服务订单"状态超过30天未变动，则订单失效</para><para>示例值：CREATED</para></param>
        /// <param name="state_description">订单状态说明  <para>对服务订单"进行中"状态的附加说明。USER_CONFIRM：用户确认MCH_COMPLETE：商户完结</para><para>示例值：MCH_COMPLETE</para><para>可为null</para></param>
        /// <param name="total_amount">商户收款总金额  <para>总金额，大于等于0的数字，单位为分，只能为整数，详见支付金额。此参数需满足：总金额=修改后付费项目金额之和-修改后付费商户优惠项目金额之和，且小于等于订单风险金额。</para><para>示例值：40000</para><para>可为null</para></param>
        /// <param name="post_payments">后付费项目 <para>后付费项目列表，最多包含100条付费项目。</para></param>
        /// <param name="post_discounts">后付费商户优惠 <para>后付费商户优惠，最多包含30条付费项目。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="risk_fund">订单风险金 <para>订单风险金信息。如果传入，用户侧则显示此参数。</para></param>
        /// <param name="time_range">服务时间段 <para>服务时间范围。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="location">服务位置 <para>服务位置信息。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="attach">商户数据包  <para>商户数据包可存放本订单所需信息，需要先urlencode后传入。当商户数据包总长度超出256字符时，报错处理。商户接收回包是根据场景，决定是否需要做安全过滤(XSS/CSRF)。</para><para>示例值：Easdfowealsdkjfnlaksjdlfkwqoi&wl3l2sald</para><para>可为null</para></param>
        /// <param name="notify_url">商户回调地址  <para>商户接收用户确认订单和扣款成功回调通知的地址。</para><para>示例值：https://api.test.com</para><para>可为null</para></param>
        /// <param name="order_id">微信支付服务订单号  <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。</para><para>示例值：15646546545165651651</para><para>可为null</para></param>
        /// <param name="need_collection">是否需要收款  <para>true：微信支付分代收款false：无需微信支付分代收款</para><para>示例值：true</para><para>可为null</para></param>
        /// <param name="collection">收款信息 <para>服务使用信息</para><para>可为null</para></param>
        public ModifyServiceOrderReturnJson(string appid, string mchid, string service_id, string out_order_no, string service_introduction, string state, string state_description, long total_amount, Post_Payments[] post_payments, Post_Discounts[] post_discounts, Risk_Fund risk_fund, Time_Range time_range, Location location, string attach, string notify_url, string order_id, bool need_collection, Collection collection)
        {
            this.appid = appid;
            this.mchid = mchid;
            this.service_id = service_id;
            this.out_order_no = out_order_no;
            this.service_introduction = service_introduction;
            this.state = state;
            this.state_description = state_description;
            this.total_amount = total_amount;
            this.post_payments = post_payments;
            this.post_discounts = post_discounts;
            this.risk_fund = risk_fund;
            this.time_range = time_range;
            this.location = location;
            this.attach = attach;
            this.notify_url = notify_url;
            this.order_id = order_id;
            this.need_collection = need_collection;
            this.collection = collection;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyServiceOrderReturnJson()
        {
        }

        /// <summary>
        /// 应用ID 
        /// <para>调用接口提交的公众账号ID。 </para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号 
        /// <para>调用接口提交的商户号。 </para>
        /// <para>示例值：1230000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>调用该接口提交的服务ID。 </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 商户服务订单号 
        /// <para>调用接口提交的商户服务订单号。 </para>
        /// <para>示例值：1234323JKHDFE1243252</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 服务信息 
        /// <para>服务信息用于介绍本订单所提供的服务，当参数长度超过20个字符时，报错处理。 </para>
        /// <para>示例值：某某酒店 </para>
        /// </summary>
        public string service_introduction { get; set; }

        /// <summary>
        /// 服务订单状态 
        /// <para>表示当前单据状态。 枚举值： CREATED：商户已创建服务订单 DOING：服务订单进行中 DONE：服务订单完成 REVOKED：商户取消服务订单  EXPIRED：服务订单已失效，"商户已创建服务订单"状态超过30天未变动，则订单失效</para>
        /// <para>示例值：CREATED </para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 订单状态说明 
        /// <para>对服务订单"进行中"状态的附加说明。 USER_CONFIRM：用户确认 MCH_COMPLETE：商户完结 </para>
        /// <para>示例值：MCH_COMPLETE </para>
        /// <para>可为null</para>
        /// </summary>
        public string state_description { get; set; }

        /// <summary>
        /// 商户收款总金额 
        /// <para>总金额，大于等于0的数字，单位为分，只能为整数，详见支付金额。 此参数需满足：总金额=修改后付费项目金额之和-修改后付费商户优惠项目金额之和，且小于等于订单风险金额。</para>
        /// <para>示例值：40000 </para>
        /// <para>可为null</para>
        /// </summary>
        public long total_amount { get; set; }

        /// <summary>
        /// 后付费项目
        /// <para>后付费项目列表，最多包含100条付费项目。 </para>
        /// </summary>
        public Post_Payments[] post_payments { get; set; }

        /// <summary>
        /// 后付费商户优惠
        /// <para>后付费商户优惠，最多包含30条付费项目。 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
        /// </summary>
        public Post_Discounts[] post_discounts { get; set; }

        /// <summary>
        /// 订单风险金
        /// <para>订单风险金信息。 如果传入，用户侧则显示此参数。</para>
        /// </summary>
        public Risk_Fund risk_fund { get; set; }

        /// <summary>
        /// 服务时间段
        /// <para>服务时间范围。 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
        /// </summary>
        public Time_Range time_range { get; set; }

        /// <summary>
        /// 服务位置
        /// <para>服务位置信息。 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        /// 商户数据包 
        /// <para>商户数据包可存放本订单所需信息，需要先urlencode后传入。 当商户数据包总长度超出256字符时，报错处理。商户接收回包是根据场景，决定是否需要做安全过滤(XSS/CSRF)。 </para>
        /// <para>示例值：Easdfowealsdkjfnlaksjdlfkwqoi&wl3l2sald </para>
        /// <para>可为null</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户回调地址 
        /// <para>商户接收用户确认订单和扣款成功回调通知的地址。 </para>
        /// <para>示例值：https://api.test.com</para>
        /// <para>可为null</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 微信支付服务订单号 
        /// <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。 </para>
        /// <para>示例值：15646546545165651651</para>
        /// <para>可为null</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 是否需要收款 
        /// <para>true：微信支付分代收款 false：无需微信支付分代收款</para>
        /// <para>示例值：true </para>
        /// <para>可为null</para>
        /// </summary>
        public bool need_collection { get; set; }

        /// <summary>
        /// 收款信息
        /// <para>服务使用信息</para>
        /// <para>可为null</para>
        /// </summary>
        public Collection collection { get; set; }

        #region 子数据类型
        public class Post_Payments
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">付费项目名称  <para>当参数长度超过20个字符时，报错处理。</para><para>示例值：就餐费用,服务费</para></param>
            /// <param name="amount">金额  <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费。</para><para>示例值：40000</para></param>
            /// <param name="description">计费说明  <para>描述计费规则，当参数长度超过30个字符时，报错处理。</para><para>示例值：就餐人均100元，服务费：100/小时</para><para>可为null</para></param>
            /// <param name="count">付费数量  <para>付费项目的数量。</para><para>示例值：4</para><para>可为null</para></param>
            public Post_Payments(string name, long amount, string description, uint count)
            {
                this.name = name;
                this.amount = amount;
                this.description = description;
                this.count = count;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Post_Payments()
            {
            }

            /// <summary>
            /// 付费项目名称 
            /// <para>当参数长度超过20个字符时，报错处理。 </para>
            /// <para>示例值：就餐费用, 服务费 </para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 金额 
            /// <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费。 </para>
            /// <para>示例值：40000</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 计费说明 
            /// <para>描述计费规则，当参数长度超过30个字符时，报错处理。 </para>
            /// <para>示例值：就餐人均100元，服务费：100/小时 </para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 付费数量 
            /// <para>付费项目的数量。 </para>
            /// <para>示例值：4</para>
            /// <para>可为null</para>
            /// </summary>
            public uint count { get; set; }

        }

        public class Post_Discounts
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">优惠名称  <para>优惠名称说明。</para><para>示例值：满20减1元</para><para>可为null</para></param>
            /// <param name="description">优惠说明  <para>优惠使用条件说明。如果填写了name（优惠名称）和description（优惠说明）其中一个字段内容，则另一个字段也必须填写。</para><para>示例值：不与其他优惠叠加</para><para>可为null</para></param>
            /// <param name="amount">优惠金额  <para>优惠金额</para><para>示例值：100</para></param>
            /// <param name="count">优惠数量  <para>优惠的数量。</para><para>特殊规则：数量限制100，不填时默认1。</para><para>示例值：2</para><para>可为null</para></param>
            public Post_Discounts(string name, string description, long amount, uint count)
            {
                this.name = name;
                this.description = description;
                this.amount = amount;
                this.count = count;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Post_Discounts()
            {
            }

            /// <summary>
            /// 优惠名称 
            /// <para>优惠名称说明。 </para>
            /// <para>示例值：满20减1元</para>
            /// <para>可为null</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠说明 
            /// <para>优惠使用条件说明。 如果填写了name（优惠名称）和description（优惠说明）其中一个字段内容，则另一个字段也必须填写。 </para>
            /// <para>示例值：不与其他优惠叠加 </para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 优惠金额 
            /// <para>优惠金额 </para>
            /// <para>示例值：100</para>
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

        public class Risk_Fund
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">风险金名称  <para>枚举值：【先免模式】（评估不通过可交押金）可填名称为DEPOSIT：押金ADVANCE：预付款CASH_DEPOSIT：保证金【先享模式】（评估不通过不可使用服务）可填名称为ESTIMATE_ORDER_COST：预估订单费用</para><para>示例值：ESTIMATE_ORDER_COST</para></param>
            /// <param name="amount">风险金额  <para>1、数字，必须>0（单位分）。2、风险金额≤每个服务ID的风险金额上限。3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限（未填写金额的付费项目，视为该付费项目金额为0）。</para><para>示例值：10000</para></param>
            /// <param name="description">风险说明  <para>文字，不超过30个字。</para><para>示例值：就餐的预估费用</para><para>可为null</para></param>
            public Risk_Fund(string name, long amount, string description)
            {
                this.name = name;
                this.amount = amount;
                this.description = description;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Risk_Fund()
            {
            }

            /// <summary>
            /// 风险金名称 
            /// <para>枚举值： 【先免模式】（评估不通过可交押金）可填名称为 DEPOSIT：押金 ADVANCE：预付款 CASH_DEPOSIT：保证金  【先享模式】（评估不通过不可使用服务）可填名称为 ESTIMATE_ORDER_COST：预估订单费用</para>
            /// <para>示例值：ESTIMATE_ORDER_COST</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 风险金额 
            /// <para>1、数字，必须>0（单位分）。 2、风险金额≤每个服务ID的风险金额上限。 3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限 （未填写金额的付费项目，视为该付费项目金额为0）。 </para>
            /// <para>示例值：10000</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 风险说明 
            /// <para>文字，不超过30个字。 </para>
            /// <para>示例值：就餐的预估费用</para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

        }

        public class Time_Range
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="start_time">服务开始时间  <para>用户端展示用途。用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。支持三种格式：yyyyMMddHHmmss、yyyyMMdd和OnAccept●传入20091225091010表示2009年12月25日9点10分10秒。●传入20091225默认时间为2009年12月25日。●传入OnAccept表示用户确认订单成功时间为【服务开始时间】。根据传入时间精准度进行校验1）若传入时间精准到秒，则校验精准到秒。2）若传入时间精准到日，则校验精准到日。</para><para>示例值：20091225091010</para><para>可为null</para></param>
            /// <param name="start_time_remark">服务开始时间备注 <para>服务开始时间备注说明，服务开始时间有填时，可填写服务开始时间备注，不超过20个字符，超出报错处理。</para><para>示例值：开始租借日期</para><para>可为null</para></param>
            /// <param name="end_time">服务结束时间  <para>用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para><para>●传入20091225091010表示2009年12月25日9点10分10秒。</para><para>●传入20091225默认时间为2009年12月25日。</para><para>根据传入时间精准度进行校验1、若传入时间精准到秒，则校验精准到秒。2、若传入时间精准到日，则校验精准到日。</para><para>示例值：20091225121010</para><para>可为null</para></param>
            /// <param name="end_time_remark">服务结束时间备注 <para>服务结束时间备注说明，服务结束时间有填时，可填写服务结束时间备注，不超过20个字符，超出报错处理。</para><para>示例值：结束租借日期</para><para>可为null</para></param>
            public Time_Range(string start_time, string start_time_remark, string end_time, string end_time_remark)
            {
                this.start_time = start_time;
                this.start_time_remark = start_time_remark;
                this.end_time = end_time;
                this.end_time_remark = end_time_remark;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Time_Range()
            {
            }

            /// <summary>
            /// 服务开始时间 
            /// <para> 用户端展示用途。 用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。 支持三种格式：yyyyMMddHHmmss、yyyyMMdd和 OnAccept ● 传入20091225091010表示2009年12月25日9点10分10秒。 ● 传入20091225默认时间为2009年12月25日。 ● 传入OnAccept表示用户确认订单成功时间为【服务开始时间】。 根据传入时间精准度进行校验 1）若传入时间精准到秒，则校验精准到秒。 2）若传入时间精准到日，则校验精准到日。</para>
            /// <para>示例值：20091225091010</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_time { get; set; }

            /// <summary>
            /// 服务开始时间备注
            /// <para> 服务开始时间备注说明，服务开始时间有填时，可填写服务开始时间备注，不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：开始租借日期</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_time_remark { get; set; }

            /// <summary>
            /// 服务结束时间 
            /// <para> 用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd </para>
            /// <para>● 传入20091225091010表示2009年12月25日9点10分10秒。 </para>
            /// <para>● 传入20091225默认时间为2009年12月25日。 </para>
            /// <para> 根据传入时间精准度进行校验 1、若传入时间精准到秒，则校验精准到秒。  2、若传入时间精准到日，则校验精准到日。 </para>
            /// <para>示例值：20091225121010 </para>
            /// <para>可为null</para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 服务结束时间备注
            /// <para> 服务结束时间备注说明，服务结束时间有填时，可填写服务结束时间备注，不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：结束租借日期</para>
            /// <para>可为null</para>
            /// </summary>
            public string end_time_remark { get; set; }

        }

        public class Location
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="start_location">服务开始地点  <para>开始使用服务的地点，不超过50个字符，超出报错处理。</para><para>示例值：嗨客时尚主题展餐厅</para><para>可为null</para></param>
            /// <param name="end_location">服务结束位置  <para>结束使用服务的地点，不超过50个字符，超出报错处理。</para><para>示例值：嗨客时尚主题展餐厅</para><para>可为null</para></param>
            public Location(string start_location, string end_location)
            {
                this.start_location = start_location;
                this.end_location = end_location;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Location()
            {
            }

            /// <summary>
            /// 服务开始地点 
            /// <para> 开始使用服务的地点，不超过50个字符，超出报错处理。</para>
            /// <para>示例值：嗨客时尚主题展餐厅</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_location { get; set; }

            /// <summary>
            /// 服务结束位置 
            /// <para> 结束使用服务的地点，不超过50个字符，超出报错处理。 </para>
            /// <para>示例值：嗨客时尚主题展餐厅 </para>
            /// <para>可为null</para>
            /// </summary>
            public string end_location { get; set; }

        }

        public class Collection
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="state">收款状态  <para>USER_PAYING：待支付USER_PAID：已支付</para><para>示例值：USER_PAID</para></param>
            /// <param name="total_amount">总收款金额  <para>总金额，大于等于0的数字，单位为分，只能为整数，详见支付金额。此参数需满足：总金额=付费项目金额之和-商户优惠项目金额之和，且小于等于订单风险金额。未使用服务、取消订单时，该字段必须为0。</para><para>示例值：50000</para><para>可为null</para></param>
            /// <param name="paying_amount">待收金额  <para>等待用户付款金额，只能为整数，详见支付金额。</para><para>示例值：40000</para><para>可为null</para></param>
            /// <param name="paid_amount">已收金额  <para>用户已付款的金额，只能为整数，详见支付金额。</para><para>示例值：10000</para><para>可为null</para></param>
            /// <param name="details">收款明细列表 <para>收款明细列表</para><para>可为null</para></param>
            public Collection(string state, long total_amount, long paying_amount, long paid_amount, Details[] details)
            {
                this.state = state;
                this.total_amount = total_amount;
                this.paying_amount = paying_amount;
                this.paid_amount = paid_amount;
                this.details = details;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Collection()
            {
            }

            /// <summary>
            /// 收款状态 
            /// <para>USER_PAYING：待支付 USER_PAID：已支付 </para>
            /// <para>示例值：USER_PAID</para>
            /// </summary>
            public string state { get; set; }

            /// <summary>
            /// 总收款金额 
            /// <para>总金额，大于等于0的数字，单位为分，只能为整数，详见支付金额。 此参数需满足：总金额=付费项目金额之和-商户优惠项目金额之和，且小于等于订单风险金额 。未使用服务、取消订单时，该字段必须为0。 </para>
            /// <para>示例值：50000</para>
            /// <para>可为null</para>
            /// </summary>
            public long total_amount { get; set; }

            /// <summary>
            /// 待收金额 
            /// <para>等待用户付款金额，只能为整数，详见支付金额。 </para>
            /// <para>示例值：40000</para>
            /// <para>可为null</para>
            /// </summary>
            public long paying_amount { get; set; }

            /// <summary>
            /// 已收金额 
            /// <para>用户已付款的金额，只能为整数，详见支付金额。 </para>
            /// <para>示例值：10000 </para>
            /// <para>可为null</para>
            /// </summary>
            public long paid_amount { get; set; }

            /// <summary>
            /// 收款明细列表
            /// <para>收款明细列表</para>
            /// <para>可为null</para>
            /// </summary>
            public Details[] details { get; set; }

            #region 子数据类型
            public class Details
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="seq">收款序号  <para>从1开始递增</para><para>示例值：1</para><para>可为null</para></param>
                /// <param name="amount">单笔收款金额  <para>单笔收款动作的金额，只能为整数，详见支付金额。</para><para>示例值：10000</para><para>可为null</para></param>
                /// <param name="paid_type">收款成功渠道  <para>NEWTON：微信支付分MCH：商户渠道</para><para>示例值：NEWTON</para><para>可为null</para></param>
                /// <param name="paid_time">收款成功时间  <para>支付成功时间，支持两种格式:yyyyMMddHHmmss和yyyyMMdd</para><para></para><para>●传入20091225091010表示2009年12月25日9点10分10秒</para><para>●传入20091225默认时间为2009年12月25日0点0分0秒</para><para></para><para>示例值：20091225091210</para><para>可为null</para></param>
                /// <param name="transaction_id">微信支付交易单号  <para>结单交易单号，等于普通支付接口中的transaction_id，可以使用该订单号在APP支付->API列表->查询订单、申请退款。只有单据状态为USER_PAID，且收款成功渠道为支付分渠道，收款金额大于0，才会返回结单交易单号。</para><para>示例值：15646546545165651651</para><para>可为null</para></param>
                public Details(ulong seq, long amount, string paid_type, string paid_time, string transaction_id)
                {
                    this.seq = seq;
                    this.amount = amount;
                    this.paid_type = paid_type;
                    this.paid_time = paid_time;
                    this.transaction_id = transaction_id;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Details()
                {
                }

                /// <summary>
                /// 收款序号 
                /// <para>从1开始递增 </para>
                /// <para>示例值：1</para>
                /// <para>可为null</para>
                /// </summary>
                public ulong seq { get; set; }

                /// <summary>
                /// 单笔收款金额 
                /// <para>单笔收款动作的金额，只能为整数，详见支付金额。 </para>
                /// <para>示例值：10000 </para>
                /// <para>可为null</para>
                /// </summary>
                public long amount { get; set; }

                /// <summary>
                /// 收款成功渠道 
                /// <para>NEWTON：微信支付分 MCH：商户渠道 </para>
                /// <para>示例值：NEWTON</para>
                /// <para>可为null</para>
                /// </summary>
                public string paid_type { get; set; }

                /// <summary>
                /// 收款成功时间 
                /// <para>支付成功时间，支持两种格式:yyyyMMddHHmmss和yyyyMMdd </para>
                /// <para></para>
                /// <para>● 传入20091225091010表示2009年12月25日9点10分10秒 </para>
                /// <para>● 传入20091225默认时间为2009年12月25日0点0分0秒 </para>
                /// <para></para>
                /// <para>示例值：20091225091210</para>
                /// <para>可为null</para>
                /// </summary>
                public string paid_time { get; set; }

                /// <summary>
                /// 微信支付交易单号 
                /// <para>结单交易单号，等于普通支付接口中的transaction_id，可以使用该订单号在APP支付->API列表->查询订单、申请退款。只有单据状态为USER_PAID，且收款成功渠道为支付分渠道，收款金额大于0，才会返回结单交易单号。</para>
                /// <para>示例值：15646546545165651651</para>
                /// <para>可为null</para>
                /// </summary>
                public string transaction_id { get; set; }

            }


            #endregion
        }


        #endregion
    }






}
