#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：B2BPaymentJson.cs
    文件功能描述：B2BPaymentJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    #region 订单与退款

    /// <summary>
    /// B2B 支付订单标识请求。
    /// </summary>
    public class B2BOrderIdentityRequest
    {
        /// <summary>微信商户号。</summary>
        public string mchid { get; set; }

        /// <summary>可选的商户订单号；与 B2B 支付订单号二选一。</summary>
        public string out_trade_no { get; set; }

        /// <summary>可选的 B2B 支付订单号；与商户订单号二选一。</summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// B2B 支付订单金额。
    /// </summary>
    public class B2BOrderAmount
    {
        /// <summary>订单总金额，单位为分。</summary>
        public long order_amount { get; set; }

        /// <summary>用户实际支付金额，单位为分。</summary>
        public long payer_amount { get; set; }

        /// <summary>货币类型，目前仅支持 CNY。</summary>
        public string currency { get; set; }
    }

    /// <summary>
    /// B2B 支付订单查询结果。
    /// </summary>
    public class B2BGetOrderJsonResult : WxJsonResult
    {
        /// <summary>小程序 AppId。</summary>
        public string appid { get; set; }

        /// <summary>微信商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>B2B 支付订单号。</summary>
        public string order_id { get; set; }

        /// <summary>订单状态，例如 ORDER_PRE_PAY、ORDER_PAY_SUCC、ORDER_CLOSE 或 ORDER_REFUND。</summary>
        public string pay_status { get; set; }

        /// <summary>支付完成时间，格式为 yyyy-MM-dd HH:mm:ss。</summary>
        public string pay_time { get; set; }

        /// <summary>下单时传入的附加数据。</summary>
        public string attach { get; set; }

        /// <summary>支付用户在当前小程序下的 OpenId。</summary>
        public string payer_openid { get; set; }

        /// <summary>订单金额信息。</summary>
        public B2BOrderAmount amount { get; set; }

        /// <summary>微信支付订单号；合单支付场景可能不返回。</summary>
        public string wxpay_transaction_id { get; set; }

        /// <summary>订单环境：0 正式环境，1 沙箱环境。</summary>
        public int env { get; set; }

        /// <summary>结算状态：0 未结算，1 结算中，2 结算完成。</summary>
        public int? settle_status { get; set; }

        /// <summary>结算完成时间；结算完成时返回。</summary>
        public string settle_finish_time { get; set; }

        /// <summary>技术服务费率，单位为万分比；结算完成时返回。</summary>
        public int? platform_profit_percent { get; set; }

        /// <summary>技术服务费，单位为分；结算完成时返回。</summary>
        public long? platform_profit_fee { get; set; }

        /// <summary>微信支付渠道的银行类型，例如 ICBC_DEBIT。</summary>
        public string bank_type { get; set; }
    }

    /// <summary>
    /// 发起 B2B 订单退款请求。
    /// </summary>
    public class B2BRefundRequest : B2BOrderIdentityRequest
    {
        /// <summary>商户退款单号，在同一商户号下唯一。</summary>
        public string out_refund_no { get; set; }

        /// <summary>退款金额，单位为分，不能超过原订单支付金额。</summary>
        public long refund_amount { get; set; }

        /// <summary>退款来源：1 人工客服退款，2 用户自行退款，3 其他。</summary>
        public int refund_from { get; set; }

        /// <summary>可选的退款原因：0 暂无描述，1 产品问题，2 售后问题，3 意愿问题，4 价格问题，5 其他原因。</summary>
        public int? refund_reason { get; set; }

        /// <summary>可选的退款商品描述，最长 127 个字符。</summary>
        public string description { get; set; }
    }

    /// <summary>
    /// 发起 B2B 订单退款结果。
    /// </summary>
    public class B2BRefundJsonResult : WxJsonResult
    {
        /// <summary>B2B 支付退款单号。</summary>
        public string refund_id { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>B2B 支付订单号。</summary>
        public string order_id { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }
    }

    /// <summary>
    /// 查询 B2B 退款请求。
    /// </summary>
    public class B2BGetRefundRequest
    {
        /// <summary>微信商户号。</summary>
        public string mchid { get; set; }

        /// <summary>可选的商户退款单号；与 B2B 支付退款单号二选一。官方参数表误标为必填。</summary>
        public string out_refund_no { get; set; }

        /// <summary>可选的 B2B 支付退款单号；与商户退款单号二选一。</summary>
        public string refund_id { get; set; }
    }

    /// <summary>
    /// B2B 退款金额信息。
    /// </summary>
    public class B2BRefundAmount
    {
        /// <summary>订单总金额，单位为分。</summary>
        public long order_amount { get; set; }

        /// <summary>退款金额，单位为分。</summary>
        public long refund_amount { get; set; }

        /// <summary>货币类型，目前仅支持 CNY。</summary>
        public string currency { get; set; }
    }

    /// <summary>
    /// 微信支付退款渠道信息。
    /// </summary>
    public class B2BRefundChannelInfo
    {
        /// <summary>退款渠道。</summary>
        public string channel { get; set; }

        /// <summary>用户实际收款账户。</summary>
        public string user_received_account { get; set; }

        /// <summary>退款出资账户。</summary>
        public string funds_account { get; set; }
    }

    /// <summary>
    /// B2B 退款查询结果。
    /// </summary>
    public class B2BGetRefundJsonResult : WxJsonResult
    {
        /// <summary>B2B 支付退款单号。</summary>
        public string refund_id { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>B2B 支付订单号。</summary>
        public string order_id { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>退款受理时间，格式为 yyyy-MM-dd HH:mm:ss。</summary>
        public string create_time { get; set; }

        /// <summary>退款成功时间，格式为 yyyy-MM-dd HH:mm:ss。</summary>
        public string refund_time { get; set; }

        /// <summary>退款状态：REFUND_INIT、REFUND_PROCESSING、REFUND_SUCC 或 REFUND_FAIL。</summary>
        public string refund_status { get; set; }

        /// <summary>退款状态说明。</summary>
        public string refund_desc { get; set; }

        /// <summary>退款金额信息。</summary>
        public B2BRefundAmount amount { get; set; }

        /// <summary>微信支付退款单号。</summary>
        public string wxpay_refund_id { get; set; }

        /// <summary>技术服务费回退状态：0 未回退，1 回退中，2 回退成功，3 无需回退。</summary>
        public int? reverse_sett_state { get; set; }

        /// <summary>技术服务费回退完成时间。</summary>
        public string reverse_sett_finish_time { get; set; }

        /// <summary>技术服务费率，单位为万分比。</summary>
        public int? platform_profit_percent { get; set; }

        /// <summary>回退技术服务费，单位为分。</summary>
        public long? reverse_sett_amt { get; set; }

        /// <summary>微信支付退款渠道信息。</summary>
        public B2BRefundChannelInfo refund_channel_info { get; set; }

        /// <summary>退款商品描述。</summary>
        public string description { get; set; }
    }

    #endregion

    #region 密钥、账单与资金

    /// <summary>
    /// 仅包含微信商户号的 B2B 支付请求。
    /// </summary>
    public class B2BMerchantIdRequest
    {
        /// <summary>微信商户号。</summary>
        public string mchid { get; set; }
    }

    /// <summary>
    /// B2B AppKey 查询结果。
    /// </summary>
    public class B2BGetAppKeyJsonResult : WxJsonResult
    {
        /// <summary>正式环境 AppKey。</summary>
        public string appkey { get; set; }

        /// <summary>沙箱环境 AppKey。</summary>
        public string sandbox_appkey { get; set; }
    }

    /// <summary>
    /// 下载 B2B 交易及资金账单请求。
    /// </summary>
    public class B2BDownloadBillRequest : B2BMerchantIdRequest
    {
        /// <summary>账单日期，格式为 yyyyMMdd。</summary>
        public string bill_date { get; set; }
    }

    /// <summary>
    /// B2B 交易及资金账单下载结果。
    /// </summary>
    public class B2BDownloadBillJsonResult : WxJsonResult
    {
        /// <summary>微信支付成功订单账单下载链接。</summary>
        public string success_bill_url { get; set; }

        /// <summary>微信支付退款账单下载链接。</summary>
        public string refund_bill_url { get; set; }

        /// <summary>微信支付成功订单和退款合并账单下载链接。</summary>
        public string all_bill_url { get; set; }

        /// <summary>微信支付资金账单下载链接。</summary>
        public string fund_bill_url { get; set; }

        /// <summary>日终账户可提现金额。官方未明确单位，按原始数值返回。</summary>
        public decimal ended_day_avail_amt { get; set; }

        /// <summary>日终账户待结算金额。官方未明确单位，按原始数值返回。</summary>
        public decimal ended_day_frozen_amt { get; set; }

        /// <summary>日终账户总金额。官方未明确单位，按原始数值返回。</summary>
        public decimal ended_day_total_amt { get; set; }

        /// <summary>分账成功订单账单下载链接。</summary>
        public string profit_sharing_bill_url { get; set; }

        /// <summary>分账回退账单下载链接。</summary>
        public string profit_refund_bill_url { get; set; }

        /// <summary>银行转账渠道资金账单下载链接。</summary>
        public string bankpay_fund_bill_url { get; set; }
    }

    /// <summary>
    /// B2B 商户账户余额。
    /// </summary>
    public class B2BMerchantBalance
    {
        /// <summary>资金类型：BALANCE_TYPE_AVAILABLE 可提现，BALANCE_TYPE_FROZEN 待结算。</summary>
        public string balance_type { get; set; }

        /// <summary>金额，单位为元；官方以字符串返回以保留两位小数。</summary>
        public string amount { get; set; }

        /// <summary>货币类型。</summary>
        public string currency { get; set; }
    }

    /// <summary>
    /// B2B 商户账户余额查询结果。
    /// </summary>
    public class B2BGetMerchantBalanceJsonResult : WxJsonResult
    {
        /// <summary>账户资金列表。</summary>
        public IList<B2BMerchantBalance> balance_list { get; set; }
    }

    /// <summary>
    /// 发起 B2B 手动提现请求。
    /// </summary>
    public class B2BWithdrawRequest : B2BMerchantIdRequest
    {
        /// <summary>提现金额，单位为分，必须大于 0。</summary>
        public long withdraw_amount { get; set; }

        /// <summary>商户外部提现单号，在同一商户号下唯一。</summary>
        public string out_withdraw_no { get; set; }
    }

    /// <summary>
    /// 查询 B2B 提现状态请求。
    /// </summary>
    public class B2BQueryWithdrawRequest : B2BMerchantIdRequest
    {
        /// <summary>发起提现时传入的商户外部提现单号。</summary>
        public string out_withdraw_no { get; set; }
    }

    /// <summary>
    /// B2B 提现状态查询结果。
    /// </summary>
    public class B2BQueryWithdrawJsonResult : WxJsonResult
    {
        /// <summary>商户外部提现单号。</summary>
        public string out_withdraw_no { get; set; }

        /// <summary>提现金额，单位为分。</summary>
        public long withdraw_amount { get; set; }

        /// <summary>提现状态：WITHDRAW_INIT、WITHDRAW_PROCESSING、WITHDRAW_SUCC、WITHDRAW_FAIL 或 WITHDRAW_REFUND。</summary>
        public string status { get; set; }

        /// <summary>提现失败原因，仅失败时返回。</summary>
        public string fail_reason { get; set; }
    }

    /// <summary>
    /// 设置 B2B 微信支付自动提现请求。
    /// </summary>
    public class B2BSetAutoWithdrawRequest : B2BMerchantIdRequest
    {
        /// <summary>可选的自动提现状态：1 开启，2 关闭。</summary>
        public int? status { get; set; }

        /// <summary>可选的账户留存金额，单位为分。</summary>
        public long? retain_amt { get; set; }
    }

    #endregion

    #region 分账

    /// <summary>
    /// 添加 B2B 分账接收方请求。
    /// </summary>
    public class B2BAddProfitSharingAccountRequest
    {
        /// <summary>接收方关系类型，例如 RELATION_TYPE_SUPPLIER、RELATION_TYPE_DISTRIBUTOR、RELATION_TYPE_SERVICE_PROVIDER、RELATION_TYPE_PLATFORM 或 RELATION_TYPE_OTHERS。</summary>
        public string profit_sharing_relation_type { get; set; }

        /// <summary>接收方类型：PAYEE_TYPE_EXTERNAL_USER 或 PAYEE_TYPE_EXTERNAL_MERCHANT。</summary>
        public string payee_type { get; set; }

        /// <summary>接收方标识；外部用户填写 OpenId，外部商户填写商户号。</summary>
        public string payee_id { get; set; }

        /// <summary>可选的接收方名称；接收方为外部商户时必填商户名称。</summary>
        public string payee_name { get; set; }
    }

    /// <summary>
    /// 删除 B2B 分账接收方请求。
    /// </summary>
    public class B2BDeleteProfitSharingAccountRequest
    {
        /// <summary>接收方类型：PAYEE_TYPE_EXTERNAL_USER 或 PAYEE_TYPE_EXTERNAL_MERCHANT。</summary>
        public string payee_type { get; set; }

        /// <summary>接收方 OpenId 或商户号。</summary>
        public string payee_id { get; set; }
    }

    /// <summary>
    /// 分页查询 B2B 分账接收方请求。
    /// </summary>
    public class B2BQueryProfitSharingAccountRequest
    {
        /// <summary>可选的起始偏移量，默认值为 0。</summary>
        public int? offset { get; set; }

        /// <summary>可选的最大返回数量，默认值为 10。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// B2B 分账接收方信息。
    /// </summary>
    public class B2BProfitSharingAccount
    {
        /// <summary>接收方类型。</summary>
        public string sharing_account_type { get; set; }

        /// <summary>接收方 OpenId 或商户号。</summary>
        public string sharing_account { get; set; }

        /// <summary>添加时间，Unix 秒级时间戳。</summary>
        public long add_time { get; set; }

        /// <summary>更新时间，Unix 秒级时间戳。</summary>
        public long update_time { get; set; }

        /// <summary>外部商户名称；接收方为外部商户时返回。</summary>
        public string name { get; set; }
    }

    /// <summary>
    /// B2B 分账接收方查询结果。
    /// </summary>
    public class B2BQueryProfitSharingAccountJsonResult : WxJsonResult
    {
        /// <summary>分账接收方列表。</summary>
        public IList<B2BProfitSharingAccount> account_list { get; set; }
    }

    /// <summary>
    /// 请求 B2B 分账。
    /// </summary>
    public class B2BCreateProfitSharingOrderRequest
    {
        /// <summary>发起交易的子商户号。</summary>
        public string mchid { get; set; }

        /// <summary>原支付单商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>分账金额，单位为分，不得超过支付单可分账金额。</summary>
        public long profit_fee { get; set; }

        /// <summary>分账接收方类型，与添加分账方时一致。</summary>
        public string receiver_type { get; set; }

        /// <summary>分账接收方账号，与添加分账方时一致。</summary>
        public string receiver_account { get; set; }
    }

    /// <summary>
    /// 查询 B2B 分账结果请求。
    /// </summary>
    public class B2BQueryProfitSharingOrderRequest
    {
        /// <summary>原支付单商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>分账接收方类型。</summary>
        public string receiver_type { get; set; }

        /// <summary>分账接收方账号。</summary>
        public string receiver_account { get; set; }

        /// <summary>发起交易的商户号。官方参数表标记为必填，但请求示例漏填。</summary>
        public string mchid { get; set; }
    }

    /// <summary>
    /// B2B 分账结果。
    /// </summary>
    public class B2BQueryProfitSharingOrderJsonResult : WxJsonResult
    {
        /// <summary>分账状态：1 初始化，2 成功，3 失败。</summary>
        public int order_status { get; set; }
    }

    /// <summary>
    /// 仅包含商户号和原支付单号的 B2B 分账请求。
    /// </summary>
    public class B2BProfitSharingOrderRequest
    {
        /// <summary>发起交易的子商户号。</summary>
        public string mchid { get; set; }

        /// <summary>原支付单商户订单号。</summary>
        public string out_trade_no { get; set; }
    }

    /// <summary>
    /// B2B 订单剩余可分账金额查询结果。
    /// </summary>
    public class B2BQueryProfitSharingRemainingAmountJsonResult : WxJsonResult
    {
        /// <summary>订单剩余冻结金额，单位为分。</summary>
        public long remain_amt { get; set; }
    }

    /// <summary>
    /// 请求 B2B 分账回退。
    /// </summary>
    public class B2BRefundProfitSharingRequest
    {
        /// <summary>原支付单商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>需要回退的分账接收方类型。</summary>
        public string payee_type { get; set; }

        /// <summary>需要回退的分账接收方 OpenId 或商户号。</summary>
        public string payee_id { get; set; }

        /// <summary>发起原交易的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>分账回退金额，单位为分。官方请求示例漏填，但参数表标记为必填。</summary>
        public long refund_amt { get; set; }
    }

    /// <summary>
    /// 查询 B2B 分账回退结果请求。
    /// </summary>
    public class B2BQueryRefundProfitSharingRequest
    {
        /// <summary>原支付单商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>发起原交易的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>分账回退接收方类型。</summary>
        public string payee_type { get; set; }

        /// <summary>分账回退接收方 OpenId 或商户号。</summary>
        public string payee_id { get; set; }
    }

    /// <summary>
    /// B2B 分账回退结果。
    /// </summary>
    public class B2BQueryRefundProfitSharingJsonResult : WxJsonResult
    {
        /// <summary>分账回退状态：1 回退中，2 回退完成，3 回退失败。</summary>
        public int order_status { get; set; }
    }

    #endregion
}
