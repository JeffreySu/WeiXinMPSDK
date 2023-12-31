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
  
    文件名：StockNotifyJson.cs
    文件功能描述：微信支付V3商家券领券事件回调通知回调通知Json
    
    
    创建标识：Senparc - 20210902
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities
{
    /// <summary>
    /// 微信支付V3核销事件回调通知Json
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_15.shtml </para>
    /// </summary>
    public class BusifavorNotifyJson : ReturnJsonBase
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="event_type">事件类型  <para>业务细分事件类型，枚举值：EVENT_TYPE_BUSICOUPON_SEND：商家券用户领券通知</para><para>示例值：EVENT_TYPE_BUSICOUPON_SEND</para></param>
        /// <param name="coupon_code">券code  <para>券的唯一标识。</para><para>示例值：1227944959000000911017</para></param>
        /// <param name="stock_id">批次号  <para>批次号</para><para>示例值：1286950000000039</para></param>
        /// <param name="send_time">发放时间  <para>发放时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="openid">用户标识  <para>微信用户在appid下的唯一标识。</para><para>示例值：odXnH1CJjeQ6666648db-pnxs-Wg</para><para>可为null</para></param>
        /// <param name="unionid">用户统一标识  <para>微信用户在同一个微信开放平台账号下的唯一用户标识，unionid获取方式请参见《UnionID机制说明》文档。</para><para>示例值：oOuyajgxj0oVw888SoQm6mp7PGKw</para><para>可为null</para></param>
        /// <param name="send_channel">发放渠道  <para>发放渠道，枚举值：BUSICOUPON_SEND_CHANNEL_MINIAPP：小程序BUSICOUPON_SEND_CHANNEL_API：APIBUSICOUPON_SEND_CHANNEL_PAYGIFT：支付有礼BUSICOUPON_SEND_CHANNEL_H5：H5BUSICOUPON_SEND_CHANNEL_FTOF：面对面BUSICOUPON_SEND_CHANNEL_MEMBERCARD_ACT：会员卡活动BUSICOUPON_SEND_CHANNEL_HALL：扫码领券（营销馆）BUSICOUPON_SEND_CHANNEL_JSAPI：JSAPIBUSICOUPON_SEND_CHANNEL_MINI_APP_LIVE：微信小程序直播BUSICOUPON_SEND_CHANNEL_WECHAT_SEARCH：搜一搜BUSICOUPON_SEND_CHANNEL_PAY_HAS_DISCOUNT：微信支付有优惠BUSICOUPON_SEND_CHANNEL_WECHAT_AD：微信广告BUSICOUPON_SEND_CHANNEL_RIGHTS_PLATFORM：权益平台BUSICOUPON_SEND_CHANNEL_RECEIVE_MONEY_GIFT：收款有礼BUSICOUPON_SEND_CHANNEL_MEMBER_PAY_RIGHT：会员付费权益BUSICOUPON_SEND_CHANNEL_BUSI_SMART_RETAIL：智慧零售活动BUSICOUPON_SEND_CHANNEL_FINDER_LIVEROOM：视频号直播</para><para>示例值：BUSICOUPON_SEND_CHANNEL_MINIAPP</para></param>
        /// <param name="send_merchant">发券商户号  <para>发券商户号</para><para>示例值：98568888</para></param>
        /// <param name="attach_info">发券附加信息 <para>仅在支付有礼、扫码领券（营销馆）、会员有礼发放渠道，才有该信息</para><para>可为null</para></param>
        public BusifavorNotifyJson(string event_type, string coupon_code, string stock_id, string send_time, string openid, string unionid, string send_channel, string send_merchant, Attach_Info attach_info)
        {
            this.event_type = event_type;
            this.coupon_code = coupon_code;
            this.stock_id = stock_id;
            this.send_time = send_time;
            this.openid = openid;
            this.unionid = unionid;
            this.send_channel = send_channel;
            this.send_merchant = send_merchant;
            this.attach_info = attach_info;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BusifavorNotifyJson()
        {
        }

        /// <summary>
        /// 事件类型 
        /// <para>业务细分事件类型，枚举值： EVENT_TYPE_BUSICOUPON_SEND：商家券用户领券通知 </para>
        /// <para>示例值：EVENT_TYPE_BUSICOUPON_SEND </para>
        /// </summary>
        public string event_type { get; set; }

        /// <summary>
        /// 券code 
        /// <para>券的唯一标识。 </para>
        /// <para>示例值：1227944959000000911017 </para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 批次号 
        /// <para>批次号 </para>
        /// <para>示例值：1286950000000039 </para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 发放时间 
        /// <para>发放时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// </summary>
        public string send_time { get; set; }

        /// <summary>
        /// 用户标识 
        /// <para>微信用户在appid下的唯一标识。 </para>
        /// <para>示例值：odXnH1CJjeQ6666648db-pnxs-Wg </para>
        /// <para>可为null</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户统一标识 
        /// <para>微信用户在同一个微信开放平台账号下的唯一用户标识，unionid获取方式请参见《UnionID机制说明》文档。 </para>
        /// <para>示例值：oOuyajgxj0oVw888SoQm6mp7PGKw </para>
        /// <para>可为null</para>
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 发放渠道 
        /// <para>发放渠道，枚举值： BUSICOUPON_SEND_CHANNEL_MINIAPP：小程序 BUSICOUPON_SEND_CHANNEL_API：API BUSICOUPON_SEND_CHANNEL_PAYGIFT：支付有礼 BUSICOUPON_SEND_CHANNEL_H5：H5 BUSICOUPON_SEND_CHANNEL_FTOF：面对面 BUSICOUPON_SEND_CHANNEL_MEMBERCARD_ACT：会员卡活动 BUSICOUPON_SEND_CHANNEL_HALL：扫码领券（营销馆） BUSICOUPON_SEND_CHANNEL_JSAPI：JSAPI BUSICOUPON_SEND_CHANNEL_MINI_APP_LIVE：微信小程序直播 BUSICOUPON_SEND_CHANNEL_WECHAT_SEARCH：搜一搜 BUSICOUPON_SEND_CHANNEL_PAY_HAS_DISCOUNT：微信支付有优惠 BUSICOUPON_SEND_CHANNEL_WECHAT_AD：微信广告 BUSICOUPON_SEND_CHANNEL_RIGHTS_PLATFORM：权益平台 BUSICOUPON_SEND_CHANNEL_RECEIVE_MONEY_GIFT：收款有礼 BUSICOUPON_SEND_CHANNEL_MEMBER_PAY_RIGHT：会员付费权益 BUSICOUPON_SEND_CHANNEL_BUSI_SMART_RETAIL：智慧零售活动 BUSICOUPON_SEND_CHANNEL_FINDER_LIVEROOM：视频号直播</para>
        /// <para>示例值：BUSICOUPON_SEND_CHANNEL_MINIAPP </para>
        /// </summary>
        public string send_channel { get; set; }

        /// <summary>
        /// 发券商户号 
        /// <para>发券商户号</para>
        /// <para>示例值：98568888 </para>
        /// </summary>
        public string send_merchant { get; set; }

        /// <summary>
        /// 发券附加信息
        /// <para>仅在支付有礼、扫码领券（营销馆）、会员有礼发放渠道，才有该信息</para>
        /// <para>可为null</para>
        /// </summary>
        public Attach_Info attach_info { get; set; }

        #region 子数据类型
        public class Attach_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="transaction_id">交易订单编号 <para>仅在支付有礼渠道，才有该信息，对应支付有礼曝光支付订单编号信息 </para><para>示例值：4200000462220200226114599</para><para>可为null</para></param>
            /// <param name="act_code">支付有礼活动编号 或 营销馆活动ID<para>======</para><para>支付有礼活动编号: </para><para>仅在支付有礼渠道，才有该信息，对应支付有礼活动编号信息 。该字段并不会和扫码领券(act_code)同时出现。</para><para>示例值：540358695</para><para>======</para><para>营销馆活动ID: </para><para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆领券活动ID信息信息。该字段并不会和支付有礼活动编码(act_code)同时出现。 </para><para>示例值：v3OlhIVg6HpBi2WP2bjNXw</para>
            /// <param name="hall_code">营销馆ID <para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆ID信息</para><para>示例值：7xcvJIFPzxucJMd3tUeNHg</para><para>可为null</para></param>
            /// <param name="hall_belong_mch_id">营销馆所属商户号 <para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆所属商户号信息</para><para>示例值：1900120923</para><para>可为null</para></param>
            /// <param name="card_id">会员卡ID <para>仅在会员卡活动渠道，才有该信息，对应会员卡Card_ID信息</para><para>示例值：pbLatjuzLrql_szmIQZhhTTPwBcg</para><para>可为null</para></param>
            /// <param name="code">会员卡code <para>仅在会员卡活动渠道，才有该信息，对应用户卡包会员卡Code信息</para><para>示例值：15994704420</para><para>可为null</para></param>
            /// <param name="activity_id">会员活动ID <para>仅在会员卡活动渠道，才有该信息，对应会员有礼活动ID信息</para><para>示例值：42001</para><para>可为null</para></param>
            public Attach_Info(string transaction_id, string act_code, string hall_code, int hall_belong_mch_id, string card_id, string code, string activity_id)
            {
                this.transaction_id = transaction_id;
                this.act_code = act_code;
                this.hall_code = hall_code;
                this.hall_belong_mch_id = hall_belong_mch_id;
                this.card_id = card_id;
                this.code = code;
                this.activity_id = activity_id;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Attach_Info()
            {
            }

            /// <summary>
            /// 交易订单编号
            /// <para>仅在支付有礼渠道，才有该信息，对应支付有礼曝光支付订单编号信息 </para>
            /// <para>示例值：4200000462220200226114599</para>
            /// <para>可为null</para>
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 支付有礼活动编号 或 营销馆活动ID
            /// <para>======</para>
            /// <para>支付有礼活动编号: </para>
            /// <para>仅在支付有礼渠道，才有该信息，对应支付有礼活动编号信息 。该字段并不会和扫码领券(act_code)同时出现。</para>
            /// <para>示例值：540358695</para>
            /// <para>======</para>
            /// <para>营销馆活动ID: </para>
            /// <para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆领券活动ID信息信息。该字段并不会和支付有礼活动编码(act_code)同时出现。 </para>
            /// <para>示例值：v3OlhIVg6HpBi2WP2bjNXw</para>
            /// </summary>
            public string act_code { get; set; }

            /// <summary>
            /// 营销馆ID
            /// <para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆ID信息 </para>
            /// <para>示例值：7xcvJIFPzxucJMd3tUeNHg</para>
            /// <para>可为null</para>
            /// </summary>
            public string hall_code { get; set; }

            /// <summary>
            /// 营销馆所属商户号
            /// <para>仅在扫码领券（营销馆）渠道，才有该信息，对应领券的营销馆所属商户号信息</para>
            /// <para>示例值：1900120923</para>
            /// <para>可为null</para>
            /// </summary>
            public int hall_belong_mch_id { get; set; }

            /// <summary>
            /// 会员卡ID
            /// <para>仅在会员卡活动渠道，才有该信息，对应会员卡Card_ID信息</para>
            /// <para>示例值：pbLatjuzLrql_szmIQZhhTTPwBcg</para>
            /// <para>可为null</para>
            /// </summary>
            public string card_id { get; set; }

            /// <summary>
            /// 会员卡code
            /// <para>仅在会员卡活动渠道，才有该信息，对应用户卡包会员卡Code信息</para>
            /// <para>示例值：15994704420</para>
            /// <para>可为null</para>
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// 会员活动ID
            /// <para>仅在会员卡活动渠道，才有该信息，对应会员有礼活动ID信息</para>
            /// <para>示例值：42001</para>
            /// <para>可为null</para>
            /// </summary>
            public string activity_id { get; set; }

        }
        #endregion
    }
}
