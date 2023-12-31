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
  
    文件名：CreateServiceOrderRequestData.cs
    文件功能描述：微信支付V3创建支付分订单请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3创建支付分订单请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_14.shtml </para>
    /// </summary>
    public class CreateServiceOrderRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_order_no">商户服务订单号  <para>body商户系统内部服务订单号（不是交易单号），要求此参数只能由数字、大小写字母_-|*组成，且在同一个商户号下唯一。详见[商户订单号]。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="appid">应用ID  <para>body微信为调用商户分配的公众账号ID，接口传入的appid应该为公众号的appid和小程序的appid（在mp.weixin.qq.com申请的）或APP的appid（在open.weixin.qq.com申请的）。</para><para>校验规则：1、该appid需要与调用接口的商户号（即请求头中的商户号）有绑定关系，若未绑定，可参考该指引完成绑定（商家商户号与AppID账号关联管理）；2、该appid需要在支付分系统中先进行配置</para><para></para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="service_id">服务ID  <para>body该服务ID有本接口对应产品的权限。</para><para>示例值：500001</para></param>
        /// <param name="service_introduction">服务信息  <para>body服务信息，用于介绍本订单所提供的服务，当参数长度超过20个字符时，报错处理。</para><para>示例值：某某酒店</para></param>
        /// <param name="post_payments">后付费项目 <para>body后付费项目列表，最多包含100条付费项目。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="post_discounts">后付费商户优惠 <para>body后付费商户优惠列表，最多包含30条商户优惠。如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="time_range">服务时间段 <para>body服务时间范围</para></param>
        /// <param name="location">服务位置 <para>body服务位置信息如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="risk_fund">订单风险金 <para>body订单风险金信息</para></param>
        /// <param name="attach">商户数据包  <para>body商户数据包可存放本订单所需信息，需要先urlencode后传入。当商户数据包总长度超出256字符时，报错处理。</para><para>可为null</para></param>
        /// <param name="notify_url">商户回调地址  <para>body商户接收用户确认订单和付款成功回调通知的地址。</para><para>示例值：https://api.test.com</para></param>
        /// <param name="openid">用户标识  <para>body微信用户在商户对应appid下的唯一标识。免确认订单：必填需确认订单：不填</para><para>获取用户openid指引</para></param>
        /// <param name="need_user_confirm">是否需要用户确认 <para>body枚举值：false：免确认订单true：需确认订单默认值true</para><para>示例值：true</para><para>可为null</para></param>
        public CreateServiceOrderRequestData(string out_order_no, string appid, string service_id, string service_introduction, Post_Payment[] post_payments, Post_Discount[] post_discounts, Time_Range time_range, Location location, Risk_Fund risk_fund, string attach, string notify_url, string openid, bool? need_user_confirm)
        {
            this.out_order_no = out_order_no;
            this.appid = appid;
            this.service_id = service_id;
            this.service_introduction = service_introduction;
            this.post_payments = post_payments;
            this.post_discounts = post_discounts;
            this.time_range = time_range;
            this.location = location;
            this.risk_fund = risk_fund;
            this.attach = attach;
            this.notify_url = notify_url;
            this.openid = openid;
            this.need_user_confirm = need_user_confirm;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateServiceOrderRequestData()
        {
        }

        /// <summary>
        /// 商户服务订单号 
        /// <para>body 商户系统内部服务订单号（不是交易单号），要求此参数只能由数字、大小写字母_-|*组成，且在同一个商户号下唯一。详见[商户订单号]。</para>
        /// <para>示例值：1234323JKHDFE1243252 </para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 应用ID 
        /// <para>body 微信为调用商户分配的公众账号ID，接口传入的appid应该为公众号的appid和小程序的appid（在mp.weixin.qq.com申请的）或APP的appid（在open.weixin.qq.com申请的）。</para>
        /// <para>校验规则：1、该appid需要与调用接口的商户号（即请求头中的商户号）有绑定关系，若未绑定，可参考该指引完成绑定（商家商户号与AppID账号关联管理）；2、该appid需要在支付分系统中先进行配置</para>
        /// <para></para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>body该服务ID有本接口对应产品的权限。 </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 服务信息 
        /// <para>body 服务信息，用于介绍本订单所提供的服务 ，当参数长度超过20个字符时，报错处理。 </para>
        /// <para>示例值：某某酒店</para>
        /// </summary>
        public string service_introduction { get; set; }

        /// <summary>
        /// 后付费项目
        /// <para>body后付费项目列表，最多包含100条付费项目。 如果传入，用户侧则显示此参数。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Post_Payment[] post_payments { get; set; }

        /// <summary>
        /// 后付费商户优惠
        /// <para>body后付费商户优惠列表，最多包含30条商户优惠。 如果传入，用户侧则显示此参数。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Post_Discount[] post_discounts { get; set; }

        /// <summary>
        /// 服务时间段
        /// <para>body 服务时间范围</para>
        /// </summary>
        public Time_Range time_range { get; set; }

        /// <summary>
        /// 服务位置
        /// <para>body服务位置信息 如果传入，用户侧则显示此参数。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        /// 订单风险金
        /// <para>body订单风险金信息</para>
        /// </summary>
        public Risk_Fund risk_fund { get; set; }

        /// <summary>
        /// 商户数据包 
        /// <para>body商户数据包可存放本订单所需信息，需要先urlencode后传入。 当商户数据包总长度超出256字符时，报错处理。 </para>
        /// <para>示例值：Easdfowealsdkjfnlaksjdlfkwqoi&wl3l2sald </para>
        /// <para>可为null</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户回调地址 
        /// <para>body商户接收用户确认订单和付款成功回调通知的地址。 </para>
        /// <para>示例值：https://api.test.com </para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 用户标识 
        /// <para>body微信用户在商户对应appid下的唯一标识。 免确认订单：必填 需确认订单：不填</para>
        /// <para>获取用户openid指引</para>
        /// <para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 是否需要用户确认
        /// <para>body枚举值： false：免确认订单 true：需确认订单默认值true</para>
        /// <para>示例值：true</para>
        /// <para>可为null</para>
        /// </summary>
        public bool? need_user_confirm { get; set; }

        #region 子数据类型
        public class Post_Payment
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">付费项目名称  <para>相同订单号下不能出现相同的付费项目名称，当参数长度超过20个字符时，报错处理。</para><para>示例值：就餐费用,服务费</para><para></para><para>可为null</para></param>
            /// <param name="amount">金额  <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。</para><para>示例值：40000</para></param>
            /// <param name="description">计费说明  <para>描述计费规则，不超过30个字符，超出报错处理。如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。</para><para>示例值：就餐人均100元，服务费：100/小时</para></param>
            /// <param name="count">付费数量  <para>付费项目的数量。</para><para>特殊规则：数量限制100，不填时默认1。</para><para>示例值：4</para><para>可为null</para></param>
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
            /// <para>相同订单号下不能出现相同的付费项目名称，当参数长度超过20个字符时，报错处理。</para>
            /// <para>示例值：就餐费用, 服务费</para>
            /// <para></para>
            /// <para>可为null</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 金额 
            /// <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。</para>
            /// <para>示例值：40000</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 计费说明 
            /// <para>描述计费规则，不超过30个字符，超出报错处理。如果填写了“付费项目名称”，则amount或description必须填写其一，或都填。 </para>
            /// <para>示例值：就餐人均100元，服务费：100/小时 </para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 付费数量 
            /// <para>付费项目的数量。 </para>
            /// <para>特殊规则：数量限制100，不填时默认1。</para>
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
            /// <param name="name">优惠名称  <para>优惠名称说明；name和description若填写，则必须同时填写，优惠名称不可重复描述。</para><para>示例值：满20减1元</para></param>
            /// <param name="description">优惠说明  <para>条件选填</para>TODO:多选一</param>
            public Post_Discount(string name, string description)
            {
                this.name = name;
                this.description = description;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Post_Discount()
            {
            }

            /// <summary>
            /// 优惠名称 
            /// <para>优惠名称说明；name和description若填写，则必须同时填写，优惠名称不可重复描述。</para>
            /// <para>示例值：满20减1元</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠说明 
            /// <para>条件选填</para>TODO: 多选一
            /// </summary>
            public string description { get; set; }

        }

        public class Time_Range
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="start_time">服务开始时间  <para>用户端展示用途。用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。支持三种格式：yyyyMMddHHmmss、yyyyMMdd和OnAccept●传入20091225091010表示2009年12月25日9点10分10秒。●传入20091225默认时间为2009年12月25日●传入OnAccept表示用户确认订单成功时间为【服务开始时间】。根据传入时间精准度进行校验1）若传入时间精准到秒，则校验精准到秒：【服务开始时间】>【商户调用创建订单接口时间2）若传入时间精准到日，则校验精准到日：【服务开始时间】>=【商户调用创建订单接口时间】</para><para>示例值：20091225091010</para></param>
            /// <param name="start_time_remark">服务开始时间备注 <para>服务开始时间备注说明，服务开始时间有填时，可填写服务开始时间备注，不超过20个字符，超出报错处理。</para><para>示例值：开始租借日期</para><para>可为null</para></param>
            /// <param name="end_time">预计服务结束时间  <para>用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para><para>●传入20091225091010表示2009年12月25日9点10分10秒。</para><para>●传入20091225默认时间为2009年12月25日</para><para>根据传入时间精准度进行校验1、若传入时间精准到秒，则校验精准到秒：1）【预计服务结束时间】>【服务开始时间】2）【预计服务结束时间】>【商户调用接口时间+1分钟】2、若传入时间精准到日，则校验精准到日：1）【预计服务结束时间】>=【服务开始时间】2）【预计服务结束时间】>=【商户调用接口时间】【建议】  1、用户下单时【未确定】服务结束时间，不填写。  2、用户下单时【已确定】服务结束时间，填写。</para><para>示例值：20091225121010</para><para>可为null</para></param>
            /// <param name="end_time_remark">预计服务结束时间备注 <para>预计服务结束时间备注说明，预计服务结束时间有填时，可填写预计服务结束时间备注，不超过20个字符，超出报错处理。</para><para>示例值：结束租借时间</para><para>可为null</para></param>
            public Time_Range(TenpayDateTime start_time, string start_time_remark, TenpayDateTime end_time, string end_time_remark)
            {
                this.start_time = start_time.ToString();
                this.start_time_remark = start_time_remark;
                this.end_time = end_time.ToString();
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
            /// <para> 用户端展示用途。 用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）。 支持三种格式：yyyyMMddHHmmss、yyyyMMdd和 OnAccept ● 传入20091225091010表示2009年12月25日9点10分10秒。 ● 传入20091225默认时间为2009年12月25日 ● 传入OnAccept表示用户确认订单成功时间为【服务开始时间】。 根据传入时间精准度进行校验 1）若传入时间精准到秒，则校验精准到秒：【服务开始时间】>【商户调用创建订单接口时间 2）若传入时间精准到日，则校验精准到日：【服务开始时间】>=【商户调用创建订单接口时间】</para>
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
            /// <para>● 传入20091225默认时间为2009年12月25日 </para>
            /// <para> 根据传入时间精准度进行校验 1、若传入时间精准到秒，则校验精准到秒： 1）【预计服务结束时间】>【服务开始时间】 2）【预计服务结束时间】>【商户调用接口时间+1分钟】 2、若传入时间精准到日，则校验精准到日： 1）【预计服务结束时间】>=【服务开始时间】 2）【预计服务结束时间】>=【商户调用接口时间】 【建议】    1、用户下单时【未确定】服务结束时间，不填写。    2、用户下单时【已确定】服务结束时间，填写。</para>
            /// <para>示例值：20091225121010 </para>
            /// <para>可为null</para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 预计服务结束时间备注
            /// <para> 预计服务结束时间备注说明，预计服务结束时间有填时，可填写预计服务结束时间备注，不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：结束租借时间</para>
            /// <para>可为null</para>
            /// </summary>
            public string end_time_remark { get; set; }

        }

        public class Location
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="start_location">服务开始地点  <para>开始使用服务的地点，不超过50个字符，超出报错处理。【建议】  1、用户下单时【未确定】服务结束地点，不填写。  2、服务在同一地点开始和结束，不填写。  3、用户下单时【已确定】服务结束地点，填写。</para><para>示例值：嗨客时尚主题展餐厅</para><para>可为null</para></param>
            /// <param name="end_location">预计服务结束位置  <para>1、结束使用服务的地点，不超过50个字符，超出报错处理。2、填写了服务开始地点，才能填写服务结束地点。</para><para>示例值：嗨客时尚主题展餐厅</para></param>
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
            /// <para>开始使用服务的地点，不超过50个字符，超出报错处理。 【建议】   1、用户下单时【未确定】服务结束地点，不填写。   2、服务在同一地点开始和结束，不填写。   3、用户下单时【已确定】服务结束地点，填写。</para>
            /// <para>示例值：嗨客时尚主题展餐厅</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_location { get; set; }

            /// <summary>
            /// 预计服务结束位置 
            /// <para> 1、结束使用服务的地点，不超过50个字符，超出报错处理 。 2、填写了服务开始地点，才能填写服务结束地点。</para>
            /// <para>示例值：嗨客时尚主题展餐厅 </para>
            /// </summary>
            public string end_location { get; set; }

        }

        public class Risk_Fund
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">风险金名称  <para>枚举值：【先免模式】（评估不通过可交押金）可填名称为DEPOSIT：押金ADVANCE：预付款CASH_DEPOSIT：保证金【先享模式】（评估不通过不可使用服务）可填名称为ESTIMATE_ORDER_COST：预估订单费用</para><para>示例值：DEPOSIT</para></param>
            /// <param name="amount">风险金额  <para>1、数字，必须>0（单位分）。2、风险金额≤服务ID的风险金额上限。3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限（未填写金额的付费项目，视为该付费项目金额为0）。4、完结订单的总金额和风险金额的关系。    1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额”    2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para><para>示例值：10000</para></param>
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
            /// <para>枚举值： 【先免模式】（评估不通过可交押金）可填名称为 DEPOSIT：押金 ADVANCE：预付款 CASH_DEPOSIT：保证金 【先享模式】（评估不通过不可使用服务）可填名称为 ESTIMATE_ORDER_COST：预估订单费用</para>
            /// <para>示例值：DEPOSIT</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 风险金额 
            /// <para>1、数字，必须>0（单位分）。 2、风险金额≤服务ID的风险金额上限。 3、当商户优惠字段为空时，付费项目总金额≤服务ID的风险金额上限 （未填写金额的付费项目，视为该付费项目金额为0）。 4、完结订单的总金额和风险金额的关系。     1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额”     2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para>
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


        #endregion
    }




}
