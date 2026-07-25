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

    文件名：EcommerceRefundRequestData.cs
    文件功能描述：微信支付 V3 电商收付通交易退款请求模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐电商退款、垫付回补和异常退款请求模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通申请退款请求数据。
    /// </summary>
    public class EcommerceRefundRequestData
    {
        /// <summary>
        /// 退款对应的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 电商平台服务号对应的 AppId。
        /// </summary>
        public string sp_appid { get; set; }

        /// <summary>
        /// 二级商户已与平台配置绑定关系的 AppId。
        /// </summary>
        public string sub_appid { get; set; }

        /// <summary>
        /// 原支付交易的微信支付订单号，与 <see cref="out_trade_no"/> 二选一。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 原支付交易的商户订单号，与 <see cref="transaction_id"/> 二选一。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 平台商户维度唯一的商户退款单号。
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 展示给用户的退款原因。
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 退款金额、原订单金额和退款出资账户信息。
        /// </summary>
        public EcommerceRefundRequestAmount amount { get; set; }

        /// <summary>
        /// 退款结果通知地址；传入后优先于商户平台配置。
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 退款出资商户：REFUND_SOURCE_PARTNER_ADVANCE 或 REFUND_SOURCE_SUB_MERCHANT。
        /// </summary>
        public string refund_account { get; set; }

        /// <summary>
        /// 待分账订单指定使用的资金账户，当前支持 AVAILABLE。
        /// </summary>
        public string funds_account { get; set; }
    }

    /// <summary>
    /// 电商退款申请金额信息。
    /// </summary>
    public class EcommerceRefundRequestAmount
    {
        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public int refund { get; set; }

        /// <summary>
        /// 指定的退款出资账户和金额；与 <c>funds_account</c> 不可同时使用。
        /// </summary>
        public EcommerceRefundFundsFrom[] from { get; set; }

        /// <summary>
        /// 原支付订单总金额，单位为分。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 退款币种，当前仅支持 CNY。
        /// </summary>
        public string currency { get; set; }
    }

    /// <summary>
    /// 电商退款的单个出资账户及金额。
    /// </summary>
    public class EcommerceRefundFundsFrom
    {
        /// <summary>
        /// 出资账户类型：AVAILABLE 或 UNAVAILABLE。
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 对应账户的出资金额，单位为分。
        /// </summary>
        public int amount { get; set; }
    }

    /// <summary>
    /// 电商平台垫付退款回补请求数据。
    /// </summary>
    public class EcommerceRefundAdvanceReturnRequestData
    {
        /// <summary>
        /// 退款对应的二级商户号。
        /// </summary>
        public string sub_mchid { get; set; }
    }
}
