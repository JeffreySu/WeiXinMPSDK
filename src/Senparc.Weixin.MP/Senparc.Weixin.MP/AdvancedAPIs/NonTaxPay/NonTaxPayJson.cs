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

    文件名：NonTaxPayJson.cs
    文件功能描述：NonTaxPayJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.NonTaxPay
{
    /// <summary>
    /// 微信非税缴费子项目。
    /// </summary>
    public class NonTaxFeeItem
    {
        /// <summary>
        /// 项目号，例如 1、2、3。
        /// </summary>
        public long no { get; set; }

        /// <summary>
        /// 项目编码。
        /// </summary>
        public string item_id { get; set; }

        /// <summary>
        /// 项目名称。
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 滞纳金，单位为分。
        /// </summary>
        public long? overdue { get; set; }

        /// <summary>
        /// 加罚金额，单位为分。
        /// </summary>
        public long? penalty { get; set; }

        /// <summary>
        /// 项目金额，包含滞纳金和加罚金额，单位为分。
        /// </summary>
        public long fee { get; set; }
    }

    /// <summary>
    /// 查询非税应收信息请求。
    /// </summary>
    public class NonTaxQueryFeeRequest
    {
        /// <summary>
        /// 调用接口的公众号、小程序或应用 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信非税平台分配的服务 ID。
        /// </summary>
        public long service_id { get; set; }

        /// <summary>
        /// 微信非税平台分配的银行全局唯一 ID；不传时从已配置银行中选择。
        /// </summary>
        public string bank_id { get; set; }

        /// <summary>
        /// 缴费通知书编号。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 通知书类型：1 普通通知书，2 处罚通知书。
        /// </summary>
        public int? payment_notice_type { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }
    }

    /// <summary>
    /// 查询非税应收信息结果。
    /// </summary>
    public class NonTaxQueryFeeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 用户姓名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 应缴总金额，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 缴费子项目列表。
        /// </summary>
        public List<NonTaxFeeItem> items { get; set; }

        /// <summary>
        /// 缴费通知书编号。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 执收单位名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 通知书类型。
        /// </summary>
        public int payment_notice_type { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }

        /// <summary>
        /// 缴费通知书创建时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long payment_notice_create_time { get; set; }

        /// <summary>
        /// 限缴日期，格式为 yyyyMMdd。
        /// </summary>
        public string payment_expire_date { get; set; }
    }

    /// <summary>
    /// 非税缴费支付下单请求。
    /// </summary>
    public class NonTaxUnifiedOrderRequest
    {
        /// <summary>
        /// 调用接口的公众号、小程序或应用 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信非税平台分配的服务 ID。
        /// </summary>
        public long? service_id { get; set; }

        /// <summary>
        /// 微信非税平台分配的银行全局唯一 ID；测试环境调用时需要填写。
        /// </summary>
        public string bank_id { get; set; }

        /// <summary>
        /// 清分银行账号；不使用清分机制时无需填写。
        /// </summary>
        public string bank_account { get; set; }

        /// <summary>
        /// 指定资金结算商户号，必须是 <see cref="bank_id"/> 下绑定的商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 用户 OpenId；MWEB 交易无需填写，其他交易类型必填。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 缴费服务描述。
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 缴费总金额，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 支付中间页完成支付后的跳转地址；小程序场景可不填。
        /// </summary>
        public string return_url { get; set; }

        /// <summary>
        /// 用户端 IP 地址。
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 业务订单号；与 <see cref="payment_notice_no"/> 二选一。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 缴费通知书编号；与 <see cref="order_no"/> 二选一。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 执收单位名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 通知书类型。
        /// </summary>
        public int? payment_notice_type { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }

        /// <summary>
        /// 用户姓名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 缴费子项目列表。
        /// </summary>
        public List<NonTaxFeeItem> items { get; set; }

        /// <summary>
        /// 缴费通知书创建时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long payment_notice_create_time { get; set; }

        /// <summary>
        /// 限缴日期，格式为 yyyyMMdd。
        /// </summary>
        public string payment_expire_date { get; set; }

        /// <summary>
        /// 调用场景：biz 公众号、ctiyservice 城市服务、miniprogram 小程序。
        /// </summary>
        /// <remarks>官方文档中的城市服务枚举值拼写为 <c>ctiyservice</c>，调用时应按官方值传入。</remarks>
        public string scene { get; set; }

        /// <summary>
        /// App 场景下对应移动应用的 AppId。
        /// </summary>
        public string app_appid { get; set; }

        /// <summary>
        /// 交易类型；默认 JSAPI，非微信浏览器 H5 支付填写 MWEB。
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 支付中间页或小程序是否自动调起支付。
        /// </summary>
        public bool? auto_call_pay { get; set; }
    }

    /// <summary>
    /// 非税缴费支付下单结果。
    /// </summary>
    /// <remarks>
    /// 当前官方返回参数表与查询应收信息一致，因此继承查询结果模型；后续官方增加下单专属字段时可在本类型中兼容扩展。
    /// </remarks>
    public class NonTaxUnifiedOrderJsonResult : NonTaxQueryFeeJsonResult
    {
    }

    /// <summary>
    /// 下载非税缴费对账单请求。
    /// </summary>
    public class NonTaxDownloadBillRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 对账单日期，格式为 yyyyMMdd。
        /// </summary>
        public string bill_date { get; set; }

        /// <summary>
        /// 账单类型：ALL 全部、SUCCESS 支付成功、REFUND 退款；默认 ALL。
        /// </summary>
        public string bill_type { get; set; }
    }

    /// <summary>
    /// 下载非税缴费对账单结果。
    /// </summary>
    public class NonTaxDownloadBillJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信返回的原始内容。成功时为 CSV 文本；失败时保留错误 JSON 文本。
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 触发不一致订单重新通知请求。
    /// </summary>
    public class NonTaxNotifyInconsistentOrderRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 需要重新触发支付结果通知的非税缴费订单号。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 非税缴费联调模拟请求。
    /// </summary>
    public class NonTaxMockRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 接收模拟通知或模拟查询请求的业务回调地址。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 协议版本号，当前官方默认值为 1。
        /// </summary>
        public int version { get; set; } = 1;
    }

    /// <summary>
    /// 非税缴费刷卡支付请求。
    /// </summary>
    public class NonTaxMicroPayRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信非税平台分配的银行全局唯一 ID；测试环境调用时需要填写。
        /// </summary>
        public string bank_id { get; set; }

        /// <summary>
        /// 清分银行账号；不使用清分机制时无需填写。
        /// </summary>
        public string bank_account { get; set; }

        /// <summary>
        /// 指定资金结算商户号，必须是 <see cref="bank_id"/> 下绑定的商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 缴费服务描述。
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 缴费总金额，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 用户姓名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 缴费子项目列表。
        /// </summary>
        public List<NonTaxFeeItem> items { get; set; }

        /// <summary>
        /// 缴费通知书创建时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long payment_notice_create_time { get; set; }

        /// <summary>
        /// 限缴日期，格式为 yyyyMMdd。
        /// </summary>
        public string payment_expire_date { get; set; }

        /// <summary>
        /// 缴费通知书编号；与 <see cref="order_no"/> 二选一。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 业务订单号；与 <see cref="payment_notice_no"/> 二选一。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 执收单位名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 通知书类型。
        /// </summary>
        public int? payment_notice_type { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }

        /// <summary>
        /// 用户付款码授权码，为 18 位纯数字。
        /// </summary>
        public string auth_code { get; set; }

        /// <summary>
        /// 前次请求已返回的微信非税缴费订单号；轮询支付结果时填写。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 非税缴费刷卡支付结果。
    /// </summary>
    public class NonTaxMicroPayJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信非税缴费订单号。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 获取非税缴费订单列表请求。
    /// </summary>
    public class NonTaxGetOrderListRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 缴费通知书编号。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 业务订单号。官方参数说明要求与缴费通知书编号二选一，但当前参数表未单独列出此字段。
        /// </summary>
        public string order_no { get; set; }
    }

    /// <summary>
    /// 获取非税缴费订单列表结果。
    /// </summary>
    public class NonTaxGetOrderListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 已创建的微信非税缴费订单号列表。
        /// </summary>
        public List<string> order_id_list { get; set; }

        /// <summary>
        /// 已支付的微信非税缴费订单号。
        /// </summary>
        public string paid_order_id { get; set; }
    }

    /// <summary>
    /// 非税缴费订单退款请求。
    /// </summary>
    public class NonTaxRefundRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信非税缴费订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 退款原因。
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 退款金额，单位为分；部分退款时必填。
        /// </summary>
        public long? refund_fee { get; set; }

        /// <summary>
        /// 调用方退款单号，每笔部分退款必须唯一；部分退款时必填。
        /// </summary>
        public string refund_out_id { get; set; }
    }

    /// <summary>
    /// 非税缴费订单退款结果。
    /// </summary>
    public class NonTaxRefundJsonResult : WxJsonResult
    {
        /// <summary>
        /// 微信生成的退款订单号。
        /// </summary>
        public string refund_order_id { get; set; }
    }

    /// <summary>
    /// 获取非税缴费订单详情请求。
    /// </summary>
    public class NonTaxGetOrderRequest
    {
        /// <summary>
        /// 调用接口的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信非税平台分配的服务 ID。
        /// </summary>
        public long? service_id { get; set; }

        /// <summary>
        /// 微信非税缴费订单号。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 非税缴费部分退款详情。
    /// </summary>
    public class NonTaxPartialRefundInfo
    {
        /// <summary>
        /// 微信退款订单号。
        /// </summary>
        public string refund_order_id { get; set; }

        /// <summary>
        /// 退款原因。
        /// </summary>
        public string refund_reason { get; set; }

        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public long refund_fee { get; set; }

        /// <summary>
        /// 退款完成时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long refund_finish_time { get; set; }

        /// <summary>
        /// 调用方退款单号。
        /// </summary>
        public string refund_out_id { get; set; }

        /// <summary>
        /// 退款状态：5 已退款，6 退款中。
        /// </summary>
        public int refund_status { get; set; }
    }

    /// <summary>
    /// 非税缴费通知历史。
    /// </summary>
    public class NonTaxNotifyHistory
    {
        /// <summary>
        /// 被通知的第三方 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 被通知的第三方名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 首次及最后一次通知详情。
        /// </summary>
        public List<NonTaxNotifyDetail> notify_detail { get; set; }

        /// <summary>
        /// 累计通知次数。
        /// </summary>
        public int notify_cnt { get; set; }
    }

    /// <summary>
    /// 非税缴费单次通知详情。
    /// </summary>
    public class NonTaxNotifyDetail
    {
        /// <summary>
        /// 通知时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long notify_time { get; set; }

        /// <summary>
        /// 微信后台通知总返回码。
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 微信后台通知总返回信息。
        /// </summary>
        public string ret_errmsg { get; set; }

        /// <summary>
        /// 通知耗时，单位为毫秒。
        /// </summary>
        public long cost_time { get; set; }

        /// <summary>
        /// 通知 URL 参数中的单次请求随机字符串。
        /// </summary>
        public string wxnontaxstr { get; set; }

        /// <summary>
        /// 订单状态：3 或 4 支付成功，5 已退款。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 第三方接收通知的 URL。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 第三方返回码；0 表示成功，其他值表示业务或验签、解密错误。
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 第三方返回信息。
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 第三方原始响应内容。
        /// </summary>
        public string third_resp { get; set; }

        /// <summary>
        /// 第三方响应中解密得到的 data 内容。
        /// </summary>
        public string third_resp_data { get; set; }
    }

    /// <summary>
    /// 获取非税缴费订单详情结果。
    /// </summary>
    public class NonTaxGetOrderJsonResult : WxJsonResult
    {
        /// <summary>
        /// 下单使用的 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户 OpenId。
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 微信非税缴费订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单创建时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 支付成功时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long pay_finish_time { get; set; }

        /// <summary>
        /// 缴费服务描述。
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 订单总金额，单位为分。
        /// </summary>
        public long fee { get; set; }

        /// <summary>
        /// 币种：1 人民币，2 美元。
        /// </summary>
        public int fee_type { get; set; }

        /// <summary>
        /// 微信支付交易单号。
        /// </summary>
        public string trans_id { get; set; }

        /// <summary>
        /// 订单状态：1 未支付，3 或 4 支付成功，5 已退款，6 退款中，12 超时关闭。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 微信非税平台分配的银行全局唯一 ID。
        /// </summary>
        public string bank_id { get; set; }

        /// <summary>
        /// 银行名称。
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 银行账号。
        /// </summary>
        public string bank_account { get; set; }

        /// <summary>
        /// 退款完成时间，Unix 时间戳，单位为秒。
        /// </summary>
        public long refund_finish_time { get; set; }

        /// <summary>
        /// 退款原因。
        /// </summary>
        public string refund_reason { get; set; }

        /// <summary>
        /// 微信退款订单号。
        /// </summary>
        public string refund_order_id { get; set; }

        /// <summary>
        /// 调用方退款单号。
        /// </summary>
        public string refund_out_id { get; set; }

        /// <summary>
        /// 缴费通知书编号。
        /// </summary>
        public string payment_notice_no { get; set; }

        /// <summary>
        /// 业务订单号。
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 执收单位编码。
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// 执收单位名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 通知书类型。
        /// </summary>
        public int payment_notice_type { get; set; }

        /// <summary>
        /// 行政区划代码。
        /// </summary>
        public string region_code { get; set; }

        /// <summary>
        /// 缴费子项目列表。
        /// </summary>
        public List<NonTaxFeeItem> items { get; set; }

        /// <summary>
        /// 票据类型编码。
        /// </summary>
        public string bill_type_code { get; set; }

        /// <summary>
        /// 票据号码。
        /// </summary>
        public string bill_no { get; set; }

        /// <summary>
        /// 应收款信息来源：1 财政，2 委办局。
        /// </summary>
        public int payment_info_source { get; set; }

        /// <summary>
        /// 部分退款信息。
        /// </summary>
        public NonTaxPartialRefundInfo partial_refund_info { get; set; }

        /// <summary>
        /// 微信向各接入方发送结果通知的历史记录。
        /// </summary>
        public List<NonTaxNotifyHistory> notify_history { get; set; }

        /// <summary>
        /// 下单场景，例如 biz、ctiyservice、miniprogram、offline、pc、app 或 other。
        /// </summary>
        public string scene { get; set; }
    }
}
