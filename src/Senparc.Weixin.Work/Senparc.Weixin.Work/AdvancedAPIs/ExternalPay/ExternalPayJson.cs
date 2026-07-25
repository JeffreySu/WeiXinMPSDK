/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalPayJson.cs
    文件功能描述：企业微信对外收款强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐对外收款商户、账单和付款信息模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.ExternalPay
{
    /// <summary>
    /// 添加对外收款商户号请求。
    /// </summary>
    public class ExternalPayAddMerchantRequest
    {
        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 微信支付商户号全称。
        /// </summary>
        public string merchant_name { get; set; }
    }

    /// <summary>
    /// 对外收款商户号标识请求。
    /// </summary>
    public class ExternalPayMerchantRequest
    {
        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string mch_id { get; set; }
    }

    /// <summary>
    /// 对外收款商户号的允许使用范围。
    /// </summary>
    public class ExternalPayUseScope
    {
        /// <summary>
        /// 允许使用的成员账号列表。
        /// </summary>
        public IList<string> user { get; set; }

        /// <summary>
        /// 允许使用的部门 ID 列表；使用 64 位整数兼容企业微信部门 ID。
        /// </summary>
        public IList<long> partyid { get; set; }

        /// <summary>
        /// 允许使用的标签 ID 列表。
        /// </summary>
        public IList<int> tagid { get; set; }
    }

    /// <summary>
    /// 设置对外收款商户号使用范围请求。
    /// </summary>
    public class ExternalPaySetMerchantUseScopeRequest : ExternalPayMerchantRequest
    {
        /// <summary>
        /// 允许使用的成员、部门和标签。
        /// </summary>
        public ExternalPayUseScope allow_use_scope { get; set; }
    }

    /// <summary>
    /// 查询对外收款商户号结果。
    /// </summary>
    public class ExternalPayGetMerchantResult : WorkJsonResult
    {
        /// <summary>
        /// 商户号绑定状态。
        /// </summary>
        public int bind_status { get; set; }

        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 微信支付商户号全称。
        /// </summary>
        public string merchant_name { get; set; }

        /// <summary>
        /// 允许使用的成员、部门和标签。
        /// </summary>
        public ExternalPayUseScope allow_use_scope { get; set; }
    }

    /// <summary>
    /// 查询对外收款交易记录请求。
    /// </summary>
    public class ExternalPayGetBillListRequest
    {
        /// <summary>
        /// 查询开始时间戳，单位为秒。
        /// </summary>
        public long begin_time { get; set; }

        /// <summary>
        /// 查询结束时间戳，单位为秒。
        /// </summary>
        public long end_time { get; set; }

        /// <summary>
        /// 指定收款成员账号；为空时查询应用可见范围内的记录。
        /// </summary>
        public string payee_userid { get; set; }

        /// <summary>
        /// 分页游标；首次请求可不填。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 对外收款商品信息。
    /// </summary>
    public class ExternalPayCommodity
    {
        /// <summary>
        /// 商品描述。
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        public int? amount { get; set; }
    }

    /// <summary>
    /// 对外收款退款记录。
    /// </summary>
    public class ExternalPayRefund
    {
        /// <summary>
        /// 商户退款单号。
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 发起退款的成员账号。
        /// </summary>
        public string refund_userid { get; set; }

        /// <summary>
        /// 退款备注。
        /// </summary>
        public string refund_comment { get; set; }

        /// <summary>
        /// 退款发起时间戳，单位为秒。
        /// </summary>
        public long refund_reqtime { get; set; }

        /// <summary>
        /// 退款状态。
        /// </summary>
        public int refund_status { get; set; }

        /// <summary>
        /// 退款金额，单位为分。
        /// </summary>
        public int refund_fee { get; set; }
    }

    /// <summary>
    /// 对外收款付款人联系信息。
    /// </summary>
    public class ExternalPayPayer
    {
        /// <summary>
        /// 付款人姓名。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 付款人电话号码。
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 付款人地址。
        /// </summary>
        public string address { get; set; }
    }

    /// <summary>
    /// 对外收款关联小程序信息。
    /// </summary>
    public class ExternalPayMiniProgram
    {
        /// <summary>
        /// 小程序 AppId。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 小程序名称。
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 对外收款交易记录。
    /// </summary>
    public class ExternalPayBill
    {
        /// <summary>
        /// 微信支付商户号。
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 交易类型。
        /// </summary>
        public int bill_type { get; set; }

        /// <summary>
        /// 商户订单号。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户退款单号。
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信支付交易单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 交易状态。
        /// </summary>
        public int trade_state { get; set; }

        /// <summary>
        /// 付款人外部联系人账号。
        /// </summary>
        public string external_userid { get; set; }

        /// <summary>
        /// 收款成员账号。
        /// </summary>
        public string payee_userid { get; set; }

        /// <summary>
        /// 交易总金额，单位为分。
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 已退款总金额，单位为分。
        /// </summary>
        public int total_refund_fee { get; set; }

        /// <summary>
        /// 收款方式。
        /// </summary>
        public int payment_type { get; set; }

        /// <summary>
        /// 支付时间戳，单位为秒。
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 收款备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 付款人联系信息。
        /// </summary>
        public ExternalPayPayer payer_info { get; set; }

        /// <summary>
        /// 商品列表。
        /// </summary>
        public IList<ExternalPayCommodity> commodity_list { get; set; }

        /// <summary>
        /// 退款记录列表。
        /// </summary>
        public IList<ExternalPayRefund> refund_list { get; set; }

        /// <summary>
        /// 关联小程序信息。
        /// </summary>
        public ExternalPayMiniProgram miniprogram_info { get; set; }
    }

    /// <summary>
    /// 查询对外收款交易记录结果。
    /// </summary>
    public class ExternalPayGetBillListResult : WorkJsonResult
    {
        /// <summary>
        /// 交易记录列表。
        /// </summary>
        public IList<ExternalPayBill> bill_list { get; set; }

        /// <summary>
        /// 下一页游标；为空表示没有更多数据。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 查询对外收款项目付款信息请求。
    /// </summary>
    public class ExternalPayGetPaymentInfoRequest
    {
        /// <summary>
        /// 收款项目 ID。
        /// </summary>
        public string payment_id { get; set; }
    }

    /// <summary>
    /// 收款项目关联的商户订单。
    /// </summary>
    public class ExternalPayPaymentBill
    {
        /// <summary>
        /// 商户订单号。
        /// </summary>
        public string out_trade_no { get; set; }
    }

    /// <summary>
    /// 查询对外收款项目付款信息结果。
    /// </summary>
    public class ExternalPayGetPaymentInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 收款项目关联的商户订单列表。
        /// </summary>
        public IList<ExternalPayPaymentBill> bill_list { get; set; }
    }
}
