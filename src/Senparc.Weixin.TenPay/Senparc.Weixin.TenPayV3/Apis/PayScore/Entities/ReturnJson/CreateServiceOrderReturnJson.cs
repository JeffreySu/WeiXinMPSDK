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
  
    文件名：CreateServiceOrderReturnJson.cs
    文件功能描述：创建支付分订单Json类
    
    
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
    /// 创建支付分订单Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_14.shtml </para>
    /// </summary>
    public class CreateServiceOrderReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID  <para>调用接口提交的公众账号ID。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="mchid">商户号  <para>调用接口提交的商户号。</para><para>示例值：1230000109</para></param>
        /// <param name="out_order_no">商户服务订单号  <para>调用接口提交的商户服务订单号。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="service_id">服务ID  <para>调用该接口提交的服务ID。</para><para>示例值：500001</para></param>
        /// <param name="service_introduction">服务信息  <para>服务信息，用于介绍本订单所提供的服务。</para><para>示例值：某某酒店</para></param>
        /// <param name="state">服务订单状态  <para>表示当前单据状态。</para><para>枚举值：1、CREATED：商户已创建服务订单2、DOING：服务订单进行中3、DONE：服务订单完成4、REVOKED：商户取消服务订单5、EXPIRED：服务订单已失效</para><para>示例值：CREATED</para></param>
        /// <param name="state_description">订单状态说明  <para>对服务订单"进行中"状态的附加说明。1、USER_CONFIRM：用户确认2、MCH_COMPLETE：商户完结</para><para>示例值：MCH_COMPLETE</para><para>可为null</para></param>
        /// <param name="post_payments">后付费项目 <para>后付费项目列表，最多包含100条付费项目。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="post_discounts">后付费商户优惠 <para>后付费商户优惠，最多包含30条付费项目。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="risk_fund">订单风险金 <para>订单风险金信息</para></param>
        /// <param name="time_range">服务时间段 <para>服务时间范围</para></param>
        /// <param name="location">服务位置 <para>服务使用信息。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="attach">商户数据包  <para>商户数据包,可存放本订单所需信息，需要先urlencode后传入，总长度不大于256字符,超出报错处理。</para><para>示例值：Easdfowealsdkjfnlaksjdlfkwqoi&wl3l2sald</para><para>可为null</para></param>
        /// <param name="notify_url">商户回调地址  <para>商户接收用户确认订单或扣款成功回调通知的地址。</para><para>示例值：https://api.test.com</para><para>可为null</para></param>
        /// <param name="order_id">微信支付服务订单号  <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。</para><para>示例值：15646546545165651651</para></param>
        /// <param name="package">跳转微信侧小程序订单数据  <para>用户跳转到微信侧小程序订单数据，需确认模式特有API中调起支付分-确认订单传入。该数据一小时内有效。</para><para>示例值：DJIOSQPYWDxsjdldeuwhdodwxasd_dDiodnwjh9we</para></param>
        public CreateServiceOrderReturnJson(string appid, string mchid, string out_order_no, string service_id, string service_introduction, string state, string state_description, Post_Payments[] post_payments, Post_Discounts[] post_discounts, Risk_Fund risk_fund, Time_Range time_range, Location location, string attach, string notify_url, string order_id, string package)
        {
            this.appid = appid;
            this.mchid = mchid;
            this.out_order_no = out_order_no;
            this.service_id = service_id;
            this.service_introduction = service_introduction;
            this.state = state;
            this.state_description = state_description;
            this.post_payments = post_payments;
            this.post_discounts = post_discounts;
            this.risk_fund = risk_fund;
            this.time_range = time_range;
            this.location = location;
            this.attach = attach;
            this.notify_url = notify_url;
            this.order_id = order_id;
            this.package = package;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateServiceOrderReturnJson()
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
        /// 商户服务订单号 
        /// <para>调用接口提交的商户服务订单号。 </para>
        /// <para>示例值：1234323JKHDFE1243252</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>调用该接口提交的服务ID。 </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 服务信息 
        /// <para>服务信息，用于介绍本订单所提供的服务。 </para>
        /// <para>示例值：某某酒店</para>
        /// </summary>
        public string service_introduction { get; set; }

        /// <summary>
        /// 服务订单状态 
        /// <para>表示当前单据状态。</para>
        /// <para>枚举值：    1、CREATED：商户已创建服务订单    2、DOING：服务订单进行中    3、DONE：服务订单完成    4、REVOKED：商户取消服务订单    5、EXPIRED：服务订单已失效</para>
        /// <para>示例值：CREATED</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 订单状态说明 
        /// <para>对服务订单"进行中"状态的附加说明。 1、USER_CONFIRM：用户确认 2、MCH_COMPLETE：商户完结 </para>
        /// <para>示例值：MCH_COMPLETE </para>
        /// <para>可为null</para>
        /// </summary>
        public string state_description { get; set; }

        /// <summary>
        /// 后付费项目
        /// <para>后付费项目列表，最多包含100条付费项目。 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
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
        /// <para>订单风险金信息</para>
        /// </summary>
        public Risk_Fund risk_fund { get; set; }

        /// <summary>
        /// 服务时间段
        /// <para>服务时间范围</para>
        /// </summary>
        public Time_Range time_range { get; set; }

        /// <summary>
        /// 服务位置
        /// <para>服务使用信息。 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        /// 商户数据包 
        /// <para>商户数据包,可存放本订单所需信息，需要先urlencode后传入，总长度不大于256字符,超出报错处理。 </para>
        /// <para>示例值：Easdfowealsdkjfnlaksjdlfkwqoi&wl3l2sald </para>
        /// <para>可为null</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户回调地址 
        /// <para>商户接收用户确认订单或扣款成功回调通知的地址。 </para>
        /// <para>示例值：https://api.test.com </para>
        /// <para>可为null</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 微信支付服务订单号 
        /// <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。 </para>
        /// <para>示例值：15646546545165651651</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 跳转微信侧小程序订单数据 
        /// <para>用户跳转到微信侧小程序订单数据，需确认模式特有API中调起支付分-确认订单传入。该数据一小时内有效。</para>
        /// <para>示例值：DJIOSQPYWDxsjdldeuwhdodwxasd_dDiodnwjh9we </para>
        /// </summary>
        public string package { get; set; }

        #region 子数据类型
        public class Post_Payments
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">付费项目名称  <para>不超过20个字符，超出报错处理。</para><para>示例值：就餐费用,服务费</para><para>可为null</para></param>
            /// <param name="amount">金额  <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。</para><para>示例值：40000</para></param>
            /// <param name="description">计费说明  <para>描述计费规则，不超过30个字符，超出报错处理。</para><para>示例值：就餐人均100元，服务费：100/小时</para></param>
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
            /// <para>不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：就餐费用, 服务费</para>
            /// <para>可为null</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 金额 
            /// <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。 如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。</para>
            /// <para>示例值：40000</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 计费说明 
            /// <para>描述计费规则，不超过30个字符，超出报错处理。 </para>
            /// <para>示例值：就餐人均100元，服务费：100/小时 </para>
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
            /// <param name="amount">优惠金额  <para>优惠金额，只能为整数，详见支付金额。</para><para>示例值：100</para><para>可为null</para></param>
            /// <param name="count">优惠数量  <para>优惠的数量。</para><para>特殊规则：数量限制100，不填时默认1。</para><para>示例值：2</para><para>可为null</para></param>
            public Post_Discounts(string name, string description, int amount, uint count)
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
            /// <para>优惠使用条件说明。 如果填写了name（优惠名称）和description（优惠说明）其中一个字段内容，则另一个字段也必须填写。</para>
            /// <para>示例值：不与其他优惠叠加</para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 优惠金额 
            /// <para>优惠金额，只能为整数，详见支付金额。</para>
            /// <para>示例值：100</para>
            /// <para>可为null</para>
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 优惠数量 
            /// <para>优惠的数量。</para>
            /// <para>特殊规则：数量限制100，不填时默认1。 </para>
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
            /// <param name="name">风险金名称  <para>枚举值：【先免模式】（评估不通过可交押金）可填名称为DEPOSIT：押金ADVANCE：预付款CASH_DEPOSIT：保证金【先享模式】（评估不通过不可使用服务）可填名称为ESTIMATE_ORDER_COST：预估订单费用</para><para>示例值：DEPOSIT</para></param>
            /// <param name="amount">风险金额  <para>1、数字，必须>0（单位分）。2、风险金额≤服务ID的风险金额上限。3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限（未填写金额的付费项目，视为该付费项目金额为0）。4、完结订单的总金额和风险金额的关系。    1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额”    2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para><para>示例值：10000</para></param>
            /// <param name="description">风险说明  <para>文字，不超过30个字。</para><para>示例值：就餐的预估费用</para><para>可为null</para></param>
            public Risk_Fund(string name, uint amount, string description)
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
            /// <para>枚举值： 【先免模式】（评估不通过可交押金）可填名称为 DEPOSIT：押金 ADVANCE：预付款 CASH_DEPOSIT：保证金 【先享模式】（评估不通过不可使用服务）可填名称为 ESTIMATE_ORDER_COST：预估订单费用</para>
            /// <para>示例值：DEPOSIT</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 风险金额 
            /// <para>1、数字，必须>0（单位分）。 2、风险金额≤服务ID的风险金额上限。 3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限 （未填写金额的付费项目，视为该付费项目金额为0）。 4、完结订单的总金额和风险金额的关系。     1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额”     2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para>
            /// <para>示例值：10000</para>
            /// </summary>
            public uint amount { get; set; }

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
            /// <param name="start_time">服务开始时间  <para>用户端展示用途。用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。支持三种格式：yyyyMMddHHmmss、yyyyMMdd和OnAccept●传入20091225091010表示2009年12月25日9点10分10秒。●传入20091225默认时间为2009年12月25日●传入OnAccept表示用户确认订单成功时间为【服务开始时间】。根据传入时间精准度进行校验1）若传入时间精准到秒，则校验精准到秒：【服务开始时间】>【商户调用创建订单接口时间】2）若传入时间精准到日，则校验精准到日：【服务开始时间】>=【商户调用创建订单接口时间】</para><para>示例值：20091225091010</para></param>
            /// <param name="start_time_remark">服务开始时间备注 <para>服务开始时间备注说明，服务开始时间有填时，可填写服务开始时间备注，不超过20个字符，超出报错处理。</para><para>示例值：开始租借日期</para><para>可为null</para></param>
            /// <param name="end_time">预计服务结束时间  <para>用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para><para>●传入20091225091010表示2009年12月25日9点10分10秒。</para><para>●传入20091225默认时间为2009年12月25日</para><para>根据传入时间精准度进行校验1、若传入时间精准到秒，则校验精准到秒：1）【预计服务结束时间】>【服务开始时间】2）【预计服务结束时间】>【商户调用接口时间+1分钟】2、若传入时间精准到日，则校验精准到日：1）【预计服务结束时间】>=【服务开始时间】2）【预计服务结束时间】>=【商户调用接口时间】【建议】  1、用户下单时【未确定】服务结束时间，不填写。  2、用户下单时【已确定】服务结束时间，填写。</para><para>示例值：20091225121010</para><para>可为null</para></param>
            /// <param name="end_time_remark">预计服务结束时间备注 <para>预计服务结束时间备注说明，预计服务结束时间有填时，可填写预计服务结束时间备注，不超过20个字符，超出报错处理。</para><para>示例值：结束租借日期</para><para>可为null</para></param>
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
            /// <para> 用户端展示用途。 用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。 支持三种格式：yyyyMMddHHmmss、yyyyMMdd和 OnAccept ● 传入20091225091010表示2009年12月25日9点10分10秒。 ● 传入20091225默认时间为2009年12月25日 ● 传入OnAccept表示用户确认订单成功时间为【服务开始时间】。 根据传入时间精准度进行校验 1）若传入时间精准到秒，则校验精准到秒：【服务开始时间】>【商户调用创建订单接口时间】 2）若传入时间精准到日，则校验精准到日：【服务开始时间】>=【商户调用创建订单接口时间】</para>
            /// <para>示例值：20091225091010</para>
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
            /// 预计服务结束时间 
            /// <para> 用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd </para>
            /// <para>● 传入20091225091010表示2009年12月25日9点10分10秒。 </para>
            /// <para>● 传入20091225默认时间为2009年12月25日</para>
            /// <para> 根据传入时间精准度进行校验 1、若传入时间精准到秒，则校验精准到秒： 1）【预计服务结束时间】>【服务开始时间】 2）【预计服务结束时间】>【商户调用接口时间+1分钟】 2、若传入时间精准到日，则校验精准到日： 1）【预计服务结束时间】>=【服务开始时间】 2）【预计服务结束时间】>=【商户调用接口时间】 【建议】    1、用户下单时【未确定】服务结束时间，不填写。    2、用户下单时【已确定】服务结束时间，填写。</para>
            /// <para>示例值：20091225121010 </para>
            /// <para>可为null</para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 预计服务结束时间备注
            /// <para> 预计服务结束时间备注说明，预计服务结束时间有填时，可填写预计服务结束时间备注，不超过20个字符，超出报错处理。 </para>
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
            /// <param name="start_location">服务开始地点  <para>开始使用服务的地点，不超过50个字符，超出报错处理。【建议】 1、用户下单时【未确定】服务结束地点，不填写。 2、服务在同一地点开始和结束，不填写。 3、用户下单时【已确定】服务结束地点，填写。</para><para>示例值：嗨客时尚主题展餐厅</para><para>可为null</para></param>
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
            /// <para> 开始使用服务的地点，不超过50个字符，超出报错处理。 【建议】  1、用户下单时【未确定】服务结束地点，不填写。  2、服务在同一地点开始和结束，不填写。  3、用户下单时【已确定】服务结束地点，填写。</para>
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


        #endregion
    }






}
