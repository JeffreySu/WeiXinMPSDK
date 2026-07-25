#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：WeixinExpressInsuranceJson.cs
    文件功能描述：WeixinExpressInsuranceJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress
{
    /// <summary>
    /// 发货时投保无忧退货请求。
    /// </summary>
    public class WeixinExpressInsuranceCreateOrderRequest
    {
        /// <summary>
        /// 买家 OpenId，理赔时必须保持一致。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信支付交易单号；同一支付单只能投保一次。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 微信支付时间，Unix 秒级时间戳，允许误差为 3 天以内。
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 微信支付金额，单位为分。
        /// </summary>
        public long pay_amount { get; set; }

        /// <summary>
        /// 发货运单号。
        /// </summary>
        public string delivery_no { get; set; }

        /// <summary>
        /// 发货地址。
        /// </summary>
        public WeixinExpressInsurancePlace delivery_place { get; set; }

        /// <summary>
        /// 收货地址。
        /// </summary>
        public WeixinExpressInsurancePlace receipt_place { get; set; }

        /// <summary>
        /// 投保订单展示信息。官方参数表标为必填，但当前请求示例未填写。
        /// </summary>
        public WeixinExpressInsuranceProductInfo product_info { get; set; }
    }

    /// <summary>
    /// 无忧退货投保地址。
    /// </summary>
    public class WeixinExpressInsurancePlace
    {
        /// <summary>
        /// 省份。
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市。
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区或县。
        /// </summary>
        public string county { get; set; }

        /// <summary>
        /// 详细地址。
        /// </summary>
        public string address { get; set; }
    }

    /// <summary>
    /// 无忧退货投保订单展示信息。
    /// </summary>
    public class WeixinExpressInsuranceProductInfo
    {
        /// <summary>
        /// 投保订单在商家小程序中的页面路径。
        /// </summary>
        public string order_path { get; set; }

        /// <summary>
        /// 投保商品列表。
        /// </summary>
        public IList<WeixinExpressInsuranceGoodsItem> goods_list { get; set; }
    }

    /// <summary>
    /// 无忧退货投保商品。
    /// </summary>
    public class WeixinExpressInsuranceGoodsItem
    {
        /// <summary>
        /// 投保商品名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 投保商品图片 URL。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 无忧退货投保结果。
    /// </summary>
    public class WeixinExpressInsuranceCreateOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 保单号。
        /// </summary>
        public string policy_no { get; set; }

        /// <summary>
        /// 保险止期，格式为 yyyy-MM-dd HH:mm:ss。
        /// </summary>
        public string insurance_end_date { get; set; }

        /// <summary>
        /// 保险公司预估理赔金额，单位为分。
        /// </summary>
        public long estimate_amount { get; set; }

        /// <summary>
        /// 保费，单位为分。
        /// </summary>
        public long premium { get; set; }
    }

    /// <summary>
    /// 无忧退货理赔请求。
    /// </summary>
    public class WeixinExpressInsuranceClaimRequest
    {
        /// <summary>
        /// 买家 OpenId，必须与投保时一致。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信支付交易单号，必须与投保时一致。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 退货运单号；理赔使用的退货运单号必须唯一。
        /// </summary>
        public string refund_delivery_no { get; set; }

        /// <summary>
        /// 退货快递公司编码或名称。
        /// </summary>
        public string refund_company { get; set; }
    }

    /// <summary>
    /// 无忧退货理赔结果。
    /// </summary>
    public class WeixinExpressInsuranceClaimJsonResult : WxJsonResult
    {
        /// <summary>
        /// 理赔报案号，成功申请理赔时返回。
        /// </summary>
        public string report_no { get; set; }

        /// <summary>
        /// 是否上门取件：0 否，1 是。
        /// </summary>
        public int is_home_pick_up { get; set; }
    }

    /// <summary>
    /// 申请无忧退货充值订单号请求。
    /// </summary>
    public class WeixinExpressInsuranceCreateChargeRequest
    {
        /// <summary>
        /// 充值金额，单位为分。
        /// </summary>
        public long quota { get; set; }
    }

    /// <summary>
    /// 申请无忧退货充值订单号结果。
    /// </summary>
    public class WeixinExpressInsuranceCreateChargeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 充值订单 ID。使用字符串避免 JavaScript 等调用方发生 64 位整数精度损失。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 申请无忧退货充值支付请求。
    /// </summary>
    public class WeixinExpressInsuranceApplyPayRequest
    {
        /// <summary>
        /// 申请充值订单号接口返回的订单 ID；按官方建议使用字符串传递以避免精度损失。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 申请无忧退货充值支付结果。
    /// </summary>
    public class WeixinExpressInsuranceApplyPayJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信服务市场充值页面地址。
        /// </summary>
        public string pay_url { get; set; }
    }

    /// <summary>
    /// 拉取无忧退货充值订单请求。
    /// </summary>
    public class WeixinExpressInsurancePayOrderListRequest
    {
        /// <summary>
        /// 订单状态列表：1 待支付、2 支付成功、3 使用中、4 已用完、5 退款中、6 已退款、10 支付超时。
        /// </summary>
        public IList<int> status_list { get; set; }

        /// <summary>
        /// 分页偏移量。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 分页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 无忧退货充值订单。
    /// </summary>
    public class WeixinExpressInsurancePayOrder
    {
        /// <summary>
        /// 充值订单 ID；使用字符串保存 64 位数值。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单状态：1 待支付、2 支付成功、3 使用中、4 已用完、5 退款中、6 已退款、10 支付超时。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 充值金额，单位为分。
        /// </summary>
        public long total_price { get; set; }

        /// <summary>
        /// 订单创建时间，Unix 秒级时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 支付时间，Unix 秒级时间戳。
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 是否可以退款。
        /// </summary>
        public bool can_refund { get; set; }

        /// <summary>
        /// 退款时间，Unix 秒级时间戳。
        /// </summary>
        public long refund_time { get; set; }

        /// <summary>
        /// 退款状态：1 未退款、2 退款中、4 退款成功、5 退款失败。
        /// </summary>
        public int refund_status { get; set; }

        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public long refund_amt { get; set; }
    }

    /// <summary>
    /// 拉取无忧退货充值订单结果。
    /// </summary>
    public class WeixinExpressInsurancePayOrderListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 充值订单总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 充值订单列表。
        /// </summary>
        public IList<WeixinExpressInsurancePayOrder> list { get; set; }
    }

    /// <summary>
    /// 拉取无忧退货理赔摘要请求。
    /// </summary>
    public class WeixinExpressInsuranceSummaryRequest
    {
        /// <summary>
        /// 查询开始时间，Unix 秒级时间戳。
        /// </summary>
        public long begin_time { get; set; }

        /// <summary>
        /// 查询结束时间，Unix 秒级时间戳。
        /// </summary>
        public long end_time { get; set; }
    }

    /// <summary>
    /// 无忧退货理赔摘要结果。
    /// </summary>
    public class WeixinExpressInsuranceSummaryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 投保总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 理赔总数。
        /// </summary>
        public int claim_num { get; set; }

        /// <summary>
        /// 理赔成功数。
        /// </summary>
        public int claim_succ_num { get; set; }

        /// <summary>
        /// 当前保费，单位为分。
        /// </summary>
        public long premium { get; set; }

        /// <summary>
        /// 当前账号余额，单位为分。
        /// </summary>
        public long funds { get; set; }

        /// <summary>
        /// 是否因系统安全原因暂时不能投保。
        /// </summary>
        public bool need_close { get; set; }
    }

    /// <summary>
    /// 拉取无忧退货保单请求。
    /// </summary>
    public class WeixinExpressInsuranceOrderListRequest
    {
        /// <summary>
        /// 买家 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信支付交易单号。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 保单号。
        /// </summary>
        public string policy_no { get; set; }

        /// <summary>
        /// 理赔报案号。
        /// </summary>
        public string report_no { get; set; }

        /// <summary>
        /// 发货运单号。
        /// </summary>
        public string delivery_no { get; set; }

        /// <summary>
        /// 退货运单号。
        /// </summary>
        public string refund_delivery_no { get; set; }

        /// <summary>
        /// 查询开始时间，Unix 秒级时间戳。
        /// </summary>
        public long? begin_time { get; set; }

        /// <summary>
        /// 查询结束时间，Unix 秒级时间戳。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 保单状态列表：2 保障中、4 理赔中、5 理赔成功、6 理赔失败、7 投保过期。
        /// </summary>
        public IList<int> status_list { get; set; }

        /// <summary>
        /// 分页偏移量。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 分页数量，默认及最大值均为 100。
        /// </summary>
        public int? limit { get; set; }

        /// <summary>
        /// 排序方式：0 按创建时间正序，1 倒序。
        /// </summary>
        public int? sort_direct { get; set; }
    }

    /// <summary>
    /// 无忧退货保单。
    /// </summary>
    public class WeixinExpressInsuranceOrder
    {
        /// <summary>
        /// 微信支付交易单号。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 保单号。
        /// </summary>
        public string policy_no { get; set; }

        /// <summary>
        /// 理赔报案号。
        /// </summary>
        public string report_no { get; set; }

        /// <summary>
        /// 发货运单号。
        /// </summary>
        public string delivery_no { get; set; }

        /// <summary>
        /// 退货运单号。
        /// </summary>
        public string refund_delivery_no { get; set; }

        /// <summary>
        /// 保费，单位为分。
        /// </summary>
        public long premium { get; set; }

        /// <summary>
        /// 预估理赔金额，单位为分。
        /// </summary>
        public long estimate_amount { get; set; }

        /// <summary>
        /// 保单状态：2 保障中、4 理赔中、5 理赔成功、6 理赔失败、7 投保过期。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 理赔打款失败原因。
        /// </summary>
        public string pay_fail_reason { get; set; }

        /// <summary>
        /// 理赔款打给用户的时间，Unix 秒级时间戳。
        /// </summary>
        public long pay_finish_time { get; set; }

        /// <summary>
        /// 是否上门取件：0 否，1 是。
        /// </summary>
        public int is_home_pick_up { get; set; }

        /// <summary>
        /// 保险止期，格式为 yyyy-MM-dd HH:mm:ss。官方返回示例包含该字段，当前参数表未单独列出。
        /// </summary>
        public string insurance_end_date { get; set; }
    }

    /// <summary>
    /// 拉取无忧退货保单结果。
    /// </summary>
    public class WeixinExpressInsuranceOrderListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 保单总数。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 保单列表。
        /// </summary>
        public IList<WeixinExpressInsuranceOrder> list { get; set; }
    }

    /// <summary>
    /// 设置无忧退货保费余额告警请求。
    /// </summary>
    public class WeixinExpressInsuranceNotifyFundsRequest
    {
        /// <summary>
        /// 触发通知的余额，单位为分；设置为 0 表示关闭通知。
        /// </summary>
        public long notify_funds { get; set; }
    }

    /// <summary>
    /// 查询无忧退货开通状态结果。
    /// </summary>
    public class WeixinExpressInsuranceOpenStatusJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否已开通：0 否，1 是。
        /// </summary>
        public int is_open { get; set; }
    }
}
