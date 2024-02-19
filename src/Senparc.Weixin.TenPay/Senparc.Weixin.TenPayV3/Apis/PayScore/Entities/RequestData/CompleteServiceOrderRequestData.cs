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
  
    文件名：CompleteServiceOrderRequestData.cs
    文件功能描述：微信支付V3完结支付分订单接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/


using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3完结支付分订单接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_18.shtml </para>
    /// </summary>
    public class CompleteServiceOrderRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_order_no">商户服务订单号  <para>path商户系统内部服务订单号（不是交易单号），与创建订单时一致</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="appid">应用ID  <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="service_id">服务ID  <para>body服务订单的主键，唯一定义此资源的标识</para><para>示例值：500001</para></param>
        /// <param name="post_payments">后付费项目 <para>body后付费项目列表，最多包含100条付费项目</para></param>
        /// <param name="post_discounts">后付费商户优惠 <para>body后付费商户优惠列表，最多包含30条商户优惠如果传入，用户侧则显示此参数</para><para>可为null</para></param>
        /// <param name="total_amount">总金额  <para>body1、金额：数字，必须≥0（单位：分），只能为整数，详见支付金额。2、总金额=（完结付费项目1…+完结付费项目n）-（完结商户优惠项目1…+完结商户优惠项目n）3、总金额上限 1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额” 2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para><para>示例值：50000</para></param>
        /// <param name="time_range">服务时间段 <para>body服务时间范围，创建订单未填写服务结束时间，则完结的时候，服务结束时间必填如果传入，用户侧则显示此参数。</para></param>
        /// <param name="location">服务位置 <para>body服务位置如果传入，用户侧则显示此参数。</para><para>可为null</para></param>
        /// <param name="profit_sharing">微信支付服务分账标记  <para>body完结订单分账接口标记。分账开通流程，详见false：不分账，默认：falsetrue：分账。</para><para>示例值：false</para><para>可为null</para></param>
        /// <param name="goods_tag">订单优惠标记  <para>body订单优惠标记，代金券或立减金优惠的参数，说明详见代金券或立减金优惠</para><para>示例值：goods_tag</para><para>可为null</para></param>
        public CompleteServiceOrderRequestData(string out_order_no, string appid, string service_id, Post_Payment[] post_payments, Post_Discount
            [] post_discounts, long total_amount, Time_Range time_range, Location location, bool? profit_sharing, string goods_tag)
        {
            this.out_order_no = out_order_no;
            this.appid = appid;
            this.service_id = service_id;
            this.post_payments = post_payments;
            this.post_discounts = post_discounts;
            this.total_amount = total_amount;
            this.time_range = time_range;
            this.location = location;
            this.profit_sharing = profit_sharing;
            this.goods_tag = goods_tag;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CompleteServiceOrderRequestData()
        {
        }

        /// <summary>
        /// 商户服务订单号 
        /// <para>path 商户系统内部服务订单号（不是交易单号），与创建订单时一致</para>
        /// <para>示例值：1234323JKHDFE1243252 </para>
        /// </summary>
        [JsonIgnore]
        public string out_order_no { get; set; }

        /// <summary>
        /// 应用ID 
        /// <para>body 微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。 </para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>body 服务订单的主键，唯一定义此资源的标识 </para>
        /// <para>示例值：500001</para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 后付费项目
        /// <para>body 后付费项目列表，最多包含100条付费项目 </para>
        /// </summary>
        public Post_Payment[] post_payments { get; set; }

        /// <summary>
        /// 后付费商户优惠
        /// <para>body 后付费商户优惠列表，最多包含30条商户优惠 如果传入，用户侧则显示此参数 </para>
        /// <para>可为null</para>
        /// </summary>
        public Post_Discount[] post_discounts { get; set; }

        /// <summary>
        /// 总金额 
        /// <para>body 1、金额：数字，必须≥0（单位：分），只能为整数，详见支付金额。 2、总金额 =（完结付费项目1…+完结付费项目n）-（完结商户优惠项目1…+完结商户优惠项目n） 3、总金额上限   1）【评估不通过：交押金】模式：总金额≤创单时填写的“订单风险金额”   2）【评估不通过：拒绝】模式：总金额≤“每个服务ID的风险金额上限”</para>
        /// <para>示例值：50000 </para>
        /// </summary>
        public long total_amount { get; set; }

        /// <summary>
        /// 服务时间段
        /// <para>body 服务时间范围，创建订单未填写服务结束时间，则完结的时候，服务结束时间必填 如果传入，用户侧则显示此参数。</para>
        /// </summary>
        public Time_Range time_range { get; set; }

        /// <summary>
        /// 服务位置
        /// <para>body 服务位置 如果传入，用户侧则显示此参数。</para>
        /// <para>可为null</para>
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        /// 微信支付服务分账标记 
        /// <para>body 完结订单分账接口标记。分账开通流程，详见 false：不分账，默认：false true：分账。 </para>
        /// <para>示例值：false </para>
        /// <para>可为null</para>
        /// </summary>
        public bool? profit_sharing { get; set; }

        /// <summary>
        /// 订单优惠标记 
        /// <para>body 订单优惠标记，代金券或立减金优惠的参数，说明详见代金券或立减金优惠 </para>
        /// <para>示例值：goods_tag </para>
        /// <para>可为null</para>
        /// </summary>
        public string goods_tag { get; set; }

        #region 子数据类型
        public class Post_Payment
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="name">付费项目名称  <para>相同订单号下不能出现相同的付费项目名称，当参数长度超过20个字符时，报错处理。</para><para>示例值：就餐费用,服务费</para></param>
            /// <param name="amount">金额  <para>此付费项目总金额，大于等于0，单位为分，等于0时代表不需要扣费，只能为整数，详见支付金额。</para><para>示例值：40000</para></param>
            /// <param name="description">计费说明  <para>描述计费规则，不超过30个字符，超出报错处理</para><para>示例值：就餐人均100元，服务费：100/小时</para><para>可为null</para></param>
            /// <param name="count">付费数量  <para>付费项目的数量</para><para>特殊规则：数量限制100，不填时默认1</para><para>示例值：4</para><para>可为null</para></param>
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
            /// <para>相同订单号下不能出现相同的付费项目名称，当参数长度超过20个字符时，报错处理。 </para>
            /// <para>示例值：就餐费用, 服务费</para>
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
            /// <para>描述计费规则，不超过30个字符，超出报错处理 </para>
            /// <para>示例值：就餐人均100元，服务费：100/小时 </para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 付费数量 
            /// <para>付费项目的数量 </para>
            /// <para>特殊规则：数量限制100，不填时默认1 </para>
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
            /// <param name="name">优惠名称  <para>优惠名称说明</para><para>示例值：满20减1元</para></param>
            /// <param name="description">优惠说明  <para>优惠使用条件说明</para><para>示例值：不与其他优惠叠加</para><para></para></param>
            /// <param name="amount">优惠金额  <para>优惠金额，单位为分，只能为整数，详见支付金额。 </para><para>示例值：100</para></param>
            /// <param name="count">优惠数量  <para>优惠的数量</para><para>特殊规则：数量限制100，不填时默认1</para><para>示例值：2</para><para>可为null</para></param>
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
            /// <para>优惠名称说明 </para>
            /// <para>示例值：满20减1元</para>
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 优惠说明 
            /// <para>优惠使用条件说明 </para>
            /// <para>示例值：不与其他优惠叠加 </para>
            /// <para></para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 优惠金额 
            /// <para>优惠金额，单位为分，只能为整数，详见支付金额。 </para>
            /// <para>示例值：100</para>
            /// </summary>
            public long amount { get; set; }

            /// <summary>
            /// 优惠数量 
            /// <para>优惠的数量</para>
            /// <para>特殊规则：数量限制100，不填时默认1 </para>
            /// <para>示例值：2 </para>
            /// <para>可为null</para>
            /// </summary>
            public uint count { get; set; }

        }

        public class Time_Range
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="start_time">服务开始时间  <para>用户端展示用途用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）.1、不能比【商户调用创建订单接口时间早】2、不能比【商户调用完结订单接口时间晚】3、根据传入时间精度进行校验，若传入时间精确到秒，则校验精确到秒；若传入时间精确到日，则校验精确到日。4、要求时间格式必须与首次传入格式保持一致，在一致前提下可修改。支持三种格式：“yyyyMMddHHmmss”、“yyyyMMdd”和“OnAccept”。●传入20091225091010表示2009年12月25日9点10分10秒●传入20091225默认时间为2009年12月25日●传入OnAccept表示用户确认订单成功时间为【服务开始时间】【服务开始时间】不能早于调用接口时间。【建议】  实际服务开始时间与创建订单填写的“服务开始时间”一致时，不填写</para><para>示例值：20091225091010</para><para>可为null</para></param>
            /// <param name="start_time_remark">服务开始时间备注 <para>服务开始时间备注说明。1、服务开始时间有填时，可填写服务开始时间备注2、若与【服务开始时间备注】不一致，则以【实际服务开始时间备注】为准，不超过20个字符，超出报错处理。</para><para>示例值：出账日</para><para>可为null</para></param>
            /// <param name="end_time">服务结束时间  <para>创建订单未填写服务结束时间，则完结的时候，服务结束时间必填1、【调用完结接口时间】≥【实际服务结束时间】>【服务开始时间】2、要求时间格式必须与首次传入格式保持一致，在一致前提下可修改。3、若创建时，服务开始时间为格式3=OnAccept，则完结时间默认精确到秒级。用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para><para>●传入20091225091010表示2009年12月25日9点10分10秒</para><para>●传入20091225默认时间为2009年12月25日</para><para>【建议】  实际服务结束时间和预计服务结束时间一致时，不填写</para><para>示例值：20091225121010</para></param>
            /// <param name="end_time_remark">服务结束时间备注 <para>服务结束时间备注说明。1、服务结束时间有填时，可填写服务结束时间备注2、若与【服务结束时间备注】不一致，则以【实际服务结束时间备注】为准，不超过20个字符，超出报错处理。</para><para>示例值：结束租借时间</para><para>可为null</para></param>
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
            /// <para> 用户端展示用途 用户下单时确认的服务开始时间（比如用户今天下单，明天开始接受服务，这里指的是明天的服务开始时间）. 1、不能比【商户调用创建订单接口时间早】 2、不能比【商户调用完结订单接口时间晚】 3、根据传入时间精度进行校验，若传入时间精确到秒，则校验精确到秒；若传入时间精确到日，则校验精确到日。 4、要求时间格式必须与首次传入格式保持一致，在一致前提下可修改。 支持三种格式：“yyyyMMddHHmmss”、“yyyyMMdd” 和 “OnAccept”。 ● 传入20091225091010表示2009年12月25日9点10分10秒 ● 传入20091225默认时间为2009年12月25日 ● 传入OnAccept表示用户确认订单成功时间为【服务开始时间】 【服务开始时间】不能早于调用接口时间。  【建议】    实际服务开始时间与创建订单填写的“服务开始时间”一致时，不填写</para>
            /// <para>示例值：20091225091010</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_time { get; set; }

            /// <summary>
            /// 服务开始时间备注
            /// <para> 服务开始时间备注说明。 1、服务开始时间有填时，可填写服务开始时间备注 2、若与【服务开始时间备注】不一致，则以【实际服务开始时间备注】为准，不超过20个字符，超出报错处理。 </para>
            /// <para>示例值：出账日</para>
            /// <para>可为null</para>
            /// </summary>
            public string start_time_remark { get; set; }

            /// <summary>
            /// 服务结束时间 
            /// <para>创建订单未填写服务结束时间，则完结的时候，服务结束时间必填 1、【调用完结接口时间】≥【实际服务结束时间】>【服务开始时间】 2、要求时间格式必须与首次传入格式保持一致，在一致前提下可修改。 3、若创建时，服务开始时间为格式3=OnAccept，则完结时间默认精确到秒级。 用户端展示用途，支持两种格式：yyyyMMddHHmmss和yyyyMMdd </para>
            /// <para>● 传入20091225091010表示2009年12月25日9点10分10秒 </para>
            /// <para>● 传入20091225默认时间为2009年12月25日 </para>
            /// <para> 【建议】    实际服务结束时间和预计服务结束时间一致时，不填写</para>
            /// <para>示例值：20091225121010 </para>
            /// </summary>
            public string end_time { get; set; }

            /// <summary>
            /// 服务结束时间备注
            /// <para> 服务结束时间备注说明。 1、服务结束时间有填时，可填写服务结束时间备注 2、若与【服务结束时间备注】不一致，则以【实际服务结束时间备注】为准，不超过20个字符，超出报错处理。 </para>
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
            /// <param name="end_location">服务结束位置  <para>结束使用服务的地点，不超过50个字符，超出报错处理。创建订单传入了【服务开始地点】，此项才能填写【建议】  1、预计结束地点为空时，实际结束地点与开始地点相同，不填写  2、预计结束地点不为空时，实际结束地点与预计结束地点相同，不填写</para><para>示例值：嗨客时尚主题展餐厅</para></param>
            public Location(string end_location)
            {
                this.end_location = end_location;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Location()
            {
            }

            /// <summary>
            /// 服务结束位置 
            /// <para> 结束使用服务的地点，不超过50个字符，超出报错处理 。 创建订单传入了【服务开始地点】，此项才能填写 【建议】    1、预计结束地点为空时，实际结束地点与开始地点相同，不填写    2、预计结束地点不为空时，实际结束地点与预计结束地点相同，不填写</para>
            /// <para>示例值：嗨客时尚主题展餐厅 </para>
            /// </summary>
            public string end_location { get; set; }

        }


        #endregion
    }

}
