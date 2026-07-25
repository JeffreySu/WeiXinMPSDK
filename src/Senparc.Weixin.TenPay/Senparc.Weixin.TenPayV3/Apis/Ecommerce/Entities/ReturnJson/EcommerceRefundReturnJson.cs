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

    文件名：EcommerceRefundReturnJson.cs
    文件功能描述：微信支付 V3 电商收付通交易退款返回及通知模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商退款、垫付回补和退款通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通退款申请或查询结果。
    /// </summary>
    public class EcommerceRefundResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付退款单号。
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 商户退款单号。
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 原支付交易的微信支付订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 原支付交易的商户订单号。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 退款渠道：ORIGINAL、BALANCE、OTHER_BALANCE 或 OTHER_BANKCARD。
        /// </summary>
        public string channel { get; set; }

        /// <summary>
        /// 用户实际收到退款的账户描述。
        /// </summary>
        public string user_received_account { get; set; }

        /// <summary>
        /// 退款成功的 RFC 3339 时间。
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 退款创建的 RFC 3339 时间。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 退款状态：SUCCESS、CLOSED、PROCESSING 或 ABNORMAL。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 退款金额、用户退款金额、垫付金额和出资明细。
        /// </summary>
        public EcommerceRefundResultAmount amount { get; set; }

        /// <summary>
        /// 优惠退款明细。
        /// </summary>
        public EcommerceRefundPromotionDetail[] promotion_detail { get; set; }

        /// <summary>
        /// 实际退款出资商户。
        /// </summary>
        public string refund_account { get; set; }

        /// <summary>
        /// 退款使用的资金账户。
        /// </summary>
        public string funds_account { get; set; }
    }

    /// <summary>
    /// 电商退款结果金额信息。
    /// </summary>
    public class EcommerceRefundResultAmount
    {
        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public int refund { get; set; }

        /// <summary>
        /// 退款出资账户及金额明细。
        /// </summary>
        public EcommerceRefundFundsFrom[] from { get; set; }

        /// <summary>
        /// 实际退给用户的现金金额，单位为分。
        /// </summary>
        public int payer_refund { get; set; }

        /// <summary>
        /// 优惠退款金额，单位为分。
        /// </summary>
        public int? discount_refund { get; set; }

        /// <summary>
        /// 退款币种，当前仅支持 CNY。
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 电商平台垫付金额，单位为分。
        /// </summary>
        public int? advance { get; set; }
    }

    /// <summary>
    /// 电商退款优惠明细。
    /// </summary>
    public class EcommerceRefundPromotionDetail
    {
        /// <summary>
        /// 优惠券或立减优惠 ID。
        /// </summary>
        public string promotion_id { get; set; }

        /// <summary>
        /// 优惠范围：GLOBAL 或 SINGLE。
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// 优惠类型：COUPON 或 DISCOUNT。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 优惠券面额，单位为分。
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 优惠退款金额，单位为分。
        /// </summary>
        public int refund_amount { get; set; }
    }

    /// <summary>
    /// 电商平台垫付退款回补结果。
    /// </summary>
    public class EcommerceRefundAdvanceReturnResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付退款单号。
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 微信支付生成的垫付回补单号。
        /// </summary>
        public string advance_return_id { get; set; }

        /// <summary>
        /// 垫付回补金额，单位为分。
        /// </summary>
        public int return_amount { get; set; }

        /// <summary>
        /// 出款方商户号。
        /// </summary>
        public string payer_mchid { get; set; }

        /// <summary>
        /// 出款方账户：BASIC 或 OPERATION。
        /// </summary>
        public string payer_account { get; set; }

        /// <summary>
        /// 入账方商户号。
        /// </summary>
        public string payee_mchid { get; set; }

        /// <summary>
        /// 入账方账户：BASIC 或 OPERATION。
        /// </summary>
        public string payee_account { get; set; }

        /// <summary>
        /// 回补结果：SUCCESS、FAILED 或 PROCESSING。
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 回补成功的 RFC 3339 时间。
        /// </summary>
        public string success_time { get; set; }
    }

    /// <summary>
    /// 电商收付通退款结果通知的解密数据。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012124635</para>
    /// </summary>
    public class EcommerceRefundNotifyJson : ReturnJsonBase
    {
        /// <summary>
        /// 电商平台商户号。
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 原支付交易的商户订单号。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 原支付交易的微信支付订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户退款单号。
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信支付退款单号。
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 退款状态：SUCCESS、CLOSED 或 ABNORMAL。
        /// </summary>
        public string refund_status { get; set; }

        /// <summary>
        /// 退款成功的 RFC 3339 时间。
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 用户实际收到退款的账户描述。
        /// </summary>
        public string user_received_account { get; set; }

        /// <summary>
        /// 原订单金额和退款金额信息。
        /// </summary>
        public EcommerceRefundNotifyAmount amount { get; set; }

        /// <summary>
        /// 实际退款出资商户。
        /// </summary>
        public string refund_account { get; set; }
    }

    /// <summary>
    /// 电商退款结果通知金额信息。
    /// </summary>
    public class EcommerceRefundNotifyAmount
    {
        /// <summary>
        /// 原订单总金额，单位为分。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public int refund { get; set; }

        /// <summary>
        /// 用户原支付金额，单位为分。
        /// </summary>
        public int payer_total { get; set; }

        /// <summary>
        /// 实际退给用户的金额，单位为分。
        /// </summary>
        public int payer_refund { get; set; }
    }

    /// <summary>
    /// 电商退款结果通知契约常量。
    /// </summary>
    public static class EcommerceRefundNotificationTypes
    {
        /// <summary>
        /// 退款成功通知事件类型。
        /// </summary>
        public const string Success = "REFUND.SUCCESS";

        /// <summary>
        /// 退款异常通知事件类型。
        /// </summary>
        public const string Abnormal = "REFUND.ABNORMAL";

        /// <summary>
        /// 退款关闭通知事件类型。
        /// </summary>
        public const string Closed = "REFUND.CLOSED";

        /// <summary>
        /// 退款通知资源原始类型。
        /// </summary>
        public const string OriginalType = "refund";
    }
}
