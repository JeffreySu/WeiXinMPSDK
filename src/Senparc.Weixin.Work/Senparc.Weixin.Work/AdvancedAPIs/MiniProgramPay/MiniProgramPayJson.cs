/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MiniProgramPayJson.cs
    文件功能描述：企业微信小程序对外收款强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐企业微信小程序对外收款强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MiniProgramPay
{
    /// <summary>上传开户图片结果。</summary>
    public class UploadMiniProgramPayImageResult : WorkJsonResult
    {
        /// <summary>企业微信生成的图片 ID。</summary>
        public string open_wx_pay_media_id { get; set; }
    }

    /// <summary>营业执照或登记证书信息。</summary>
    public class MiniProgramPayBusinessLicenseInfo
    {
        /// <summary>证书类型。</summary>
        public int cert_type { get; set; }

        /// <summary>营业执照或登记证书图片 ID。</summary>
        public string business_license_copy_open_wx_pay_media_id { get; set; }

        /// <summary>营业执照或登记证书编号。</summary>
        public string business_license_number { get; set; }

        /// <summary>商户名称。</summary>
        public string merchant_name { get; set; }

        /// <summary>法定代表人姓名。</summary>
        public string legal_person { get; set; }

        /// <summary>注册地址。</summary>
        public string company_address { get; set; }

        /// <summary>营业期限开始日期，格式为 yyyy-MM-dd。</summary>
        public string business_time_begin_time { get; set; }

        /// <summary>营业期限截止日期，格式为 yyyy-MM-dd 或“长期”。</summary>
        public string business_time_end_time { get; set; }
    }

    /// <summary>金融机构许可证信息。</summary>
    public class MiniProgramPayFinanceInstitutionInfo
    {
        /// <summary>金融机构类型。</summary>
        public string finance_type { get; set; }

        /// <summary>金融机构许可证图片 ID 列表。</summary>
        public IList<string> finance_license_pics_open_wx_pay_media_id { get; set; }
    }

    /// <summary>开户申请中的身份证件信息。</summary>
    public class MiniProgramPayIdCardInfo
    {
        /// <summary>证件类型。</summary>
        public int id_doc_type { get; set; }

        /// <summary>证件人像面图片 ID。</summary>
        public string id_card_copy_open_wx_pay_media_id { get; set; }

        /// <summary>证件国徽面图片 ID。</summary>
        public string id_card_national_open_wx_pay_media_id { get; set; }

        /// <summary>证件姓名。</summary>
        public string id_card_name { get; set; }

        /// <summary>证件号码。</summary>
        public string id_card_number { get; set; }

        /// <summary>证件地址。</summary>
        public string id_card_address { get; set; }

        /// <summary>证件有效期开始日期。</summary>
        public string id_card_valid_time_begin { get; set; }

        /// <summary>证件有效期截止日期或“长期”。</summary>
        public string id_card_valid_time { get; set; }
    }

    /// <summary>开户申请中的超级管理员信息。</summary>
    public class MiniProgramPayContactInfo
    {
        /// <summary>超级管理员类型。</summary>
        public string contact_type { get; set; }

        /// <summary>业务办理授权函图片 ID。</summary>
        public string business_authorization_letter_open_wx_pay_media_id { get; set; }

        /// <summary>超级管理员证件信息。</summary>
        public MiniProgramPayIdCardInfo contact_info { get; set; }

        /// <summary>超级管理员手机号码。</summary>
        public string mobile_phone { get; set; }

        /// <summary>超级管理员邮箱。</summary>
        public string contact_email { get; set; }
    }

    /// <summary>银行卡补充资料。</summary>
    public class MiniProgramPayBankCardSupplement
    {
        /// <summary>结算证明图片 ID。</summary>
        public string settlement_certificate_open_wx_pay_media_id { get; set; }

        /// <summary>关系证明图片 ID。</summary>
        public string relationship_certificate_open_wx_pay_media_id { get; set; }

        /// <summary>其他证明图片 ID 列表。</summary>
        public IList<string> other_certificate_open_wx_pay_media_id { get; set; }
    }

    /// <summary>开户申请中的结算账户信息。</summary>
    public class MiniProgramPayAccountInfo
    {
        /// <summary>账户类型。</summary>
        public int? bank_account_type { get; set; }

        /// <summary>开户银行。</summary>
        public string account_bank { get; set; }

        /// <summary>开户名称。</summary>
        public string account_name { get; set; }

        /// <summary>银行账号。</summary>
        public string account_number { get; set; }

        /// <summary>开户银行省市编码。</summary>
        public string bank_address_code { get; set; }

        /// <summary>开户银行全称（含支行）。</summary>
        public string bank_name { get; set; }

        /// <summary>银行卡补充资料。</summary>
        public MiniProgramPayBankCardSupplement bank_card_supplement { get; set; }
    }

    /// <summary>开户申请中的图片 ID 集合。</summary>
    public class MiniProgramPayMediaIds
    {
        /// <summary>图片 ID 列表。</summary>
        public IList<string> id { get; set; }
    }

    /// <summary>开户申请中的经营场景信息。</summary>
    public class MiniProgramPaySalesSceneInfo
    {
        /// <summary>经营场景类型。</summary>
        public int type { get; set; }

        /// <summary>线上经营场景网址。</summary>
        public string store_url { get; set; }

        /// <summary>线上经营场景截图图片 ID。</summary>
        public string store_pic_open_wx_pay_media_id { get; set; }

        /// <summary>线下经营场景省市编码。</summary>
        public string address_code { get; set; }

        /// <summary>线下经营场景详细地址。</summary>
        public string offline_address { get; set; }

        /// <summary>线下门店门头图片 ID。</summary>
        public string entrance_pic_open_wx_pay_media_id { get; set; }

        /// <summary>线下门店内部图片 ID。</summary>
        public string indoor_pic_open_wx_pay_media_id { get; set; }
    }

    /// <summary>提交创建对外收款账户申请。</summary>
    public class ApplyMiniProgramPayMerchantRequest
    {
        /// <summary>业务申请编号。</summary>
        public string out_request_no { get; set; }

        /// <summary>主体类型。</summary>
        public int organization_type { get; set; }

        /// <summary>营业执照或登记证书信息。</summary>
        public MiniProgramPayBusinessLicenseInfo business_license_info { get; set; }

        /// <summary>金融机构许可证信息；主体为金融机构时填写。</summary>
        public MiniProgramPayFinanceInstitutionInfo finance_institution_info { get; set; }

        /// <summary>商户简称。</summary>
        public string merchant_short_name { get; set; }

        /// <summary>经营者或法人证件信息。</summary>
        public MiniProgramPayIdCardInfo id_card_info { get; set; }

        /// <summary>经营者或法人是否为受益人。</summary>
        public bool? owner { get; set; }

        /// <summary>最终受益人证件信息。</summary>
        public MiniProgramPayIdCardInfo ubo_info { get; set; }

        /// <summary>超级管理员信息。</summary>
        public MiniProgramPayContactInfo contact_info { get; set; }

        /// <summary>结算账户信息。</summary>
        public MiniProgramPayAccountInfo account_info { get; set; }

        /// <summary>经营场景信息。</summary>
        public MiniProgramPaySalesSceneInfo sales_scene_info { get; set; }

        /// <summary>经营范围 ID。</summary>
        public int business_id { get; set; }

        /// <summary>特殊资质图片。</summary>
        public MiniProgramPayMediaIds qualifications { get; set; }

        /// <summary>补充材料图片。</summary>
        public MiniProgramPayMediaIds business_addition_pics { get; set; }

        /// <summary>提现成员 UserID。</summary>
        public string userid { get; set; }
    }

    /// <summary>查询开户申请状态请求。</summary>
    public class GetMiniProgramPayApplymentStatusRequest
    {
        /// <summary>提交申请时使用的业务申请编号。</summary>
        public string out_request_no { get; set; }
    }

    /// <summary>开户申请审核驳回详情。</summary>
    public class MiniProgramPayApplymentAuditDetail
    {
        /// <summary>参数名称。</summary>
        public string param_name { get; set; }

        /// <summary>驳回原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>开户申请汇款账户验证信息。</summary>
    public class MiniProgramPayAccountValidation
    {
        /// <summary>付款户名。</summary>
        public string account_name { get; set; }

        /// <summary>付款卡号。</summary>
        public string account_no { get; set; }

        /// <summary>收款开户银行。</summary>
        public string destination_account_bank { get; set; }

        /// <summary>收款户名。</summary>
        public string destination_account_name { get; set; }

        /// <summary>收款卡号。</summary>
        public string destination_account_number { get; set; }

        /// <summary>付款金额，单位为分。</summary>
        public int pay_amount { get; set; }

        /// <summary>汇款备注。</summary>
        public string remark { get; set; }

        /// <summary>汇款截止时间。</summary>
        public string deadline { get; set; }
    }

    /// <summary>开户申请的详细状态。</summary>
    public class MiniProgramPayApplymentStatus
    {
        /// <summary>申请状态。</summary>
        public string applyment_state { get; set; }

        /// <summary>申请状态说明。</summary>
        public string applyment_state_desc { get; set; }

        /// <summary>签约状态。</summary>
        public string sign_state { get; set; }

        /// <summary>签约链接。</summary>
        public string sign_url { get; set; }

        /// <summary>审核驳回详情。</summary>
        public IList<MiniProgramPayApplymentAuditDetail> audit_detail { get; set; }

        /// <summary>汇款账户验证信息。</summary>
        public MiniProgramPayAccountValidation account_validation { get; set; }

        /// <summary>二级商户号。</summary>
        public string sub_mchid { get; set; }

        /// <summary>法人验证链接。</summary>
        public string legal_validation_url { get; set; }
    }

    /// <summary>查询开户申请状态结果。</summary>
    public class GetMiniProgramPayApplymentStatusResult : WorkJsonResult
    {
        /// <summary>申请单详细状态。</summary>
        public MiniProgramPayApplymentStatus status { get; set; }

        /// <summary>申请单当前阶段。</summary>
        public int apply_state { get; set; }

        /// <summary>当前签约阶段。</summary>
        public int real_sign_state { get; set; }

        /// <summary>驳回原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>订单金额信息。</summary>
    public class MiniProgramPayOrderAmount
    {
        /// <summary>订单总金额，单位为分。</summary>
        public int total { get; set; }

        /// <summary>货币类型，通常为 CNY。</summary>
        public string currency { get; set; }

        /// <summary>用户实际支付金额，单位为分。</summary>
        public int? payer_total { get; set; }

        /// <summary>用户支付币种。</summary>
        public string payer_currency { get; set; }
    }

    /// <summary>支付者信息。</summary>
    public class MiniProgramPayPayer
    {
        /// <summary>用户在小程序 AppID 下的 OpenID。</summary>
        public string openid { get; set; }
    }

    /// <summary>门店信息。</summary>
    public class MiniProgramPayStoreInfo
    {
        /// <summary>门店编号。</summary>
        public string id { get; set; }

        /// <summary>门店名称。</summary>
        public string name { get; set; }

        /// <summary>地区编码。</summary>
        public string area_code { get; set; }

        /// <summary>详细地址。</summary>
        public string address { get; set; }
    }

    /// <summary>下单场景信息。</summary>
    public class MiniProgramPaySceneInfo
    {
        /// <summary>用户终端 IP。</summary>
        public string payer_client_ip { get; set; }

        /// <summary>商户端设备号。</summary>
        public string device_id { get; set; }

        /// <summary>门店信息。</summary>
        public MiniProgramPayStoreInfo store_info { get; set; }
    }

    /// <summary>下单商品明细。</summary>
    public class MiniProgramPayGoodsDetail
    {
        /// <summary>商户侧商品编码。</summary>
        public string merchant_goods_id { get; set; }

        /// <summary>微信支付商品编码。</summary>
        public string wechatpay_goods_id { get; set; }

        /// <summary>商品名称。</summary>
        public string goods_name { get; set; }

        /// <summary>商品数量。</summary>
        public int quantity { get; set; }

        /// <summary>商品单价，单位为分。</summary>
        public int unit_price { get; set; }
    }

    /// <summary>下单商品详情。</summary>
    public class MiniProgramPayOrderDetail
    {
        /// <summary>订单原价，单位为分。</summary>
        public int? cost_price { get; set; }

        /// <summary>商品小票 ID。</summary>
        public string invoice_id { get; set; }

        /// <summary>单品列表。</summary>
        public IList<MiniProgramPayGoodsDetail> goods_detail { get; set; }
    }

    /// <summary>创建小程序支付订单请求。</summary>
    public class CreateMiniProgramPayOrderRequest
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>商品描述。</summary>
        public string description { get; set; }

        /// <summary>用于统计成员交易业绩的场景 Key。</summary>
        public string scenekey { get; set; }

        /// <summary>订单金额。</summary>
        public MiniProgramPayOrderAmount amount { get; set; }

        /// <summary>支付者信息。</summary>
        public MiniProgramPayPayer payer { get; set; }

        /// <summary>订单失效时间，RFC 3339 格式。</summary>
        public string time_expire { get; set; }

        /// <summary>附加数据。</summary>
        public string attach { get; set; }

        /// <summary>订单优惠标记。</summary>
        public string goods_tag { get; set; }

        /// <summary>下单场景信息。</summary>
        public MiniProgramPaySceneInfo scene_info { get; set; }

        /// <summary>商品详情。</summary>
        public MiniProgramPayOrderDetail detail { get; set; }
    }

    /// <summary>创建小程序支付订单结果。</summary>
    public class CreateMiniProgramPayOrderResult : WorkJsonResult
    {
        /// <summary>预支付交易会话标识。</summary>
        public string prepay_id { get; set; }
    }

    /// <summary>按商户号和商户订单号标识订单。</summary>
    public class MiniProgramPayOrderIdentity
    {
        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }
    }

    /// <summary>优惠商品详情。</summary>
    public class MiniProgramPayPromotionGoodsDetail
    {
        /// <summary>商品编码。</summary>
        public string goods_id { get; set; }

        /// <summary>商品数量。</summary>
        public int quantity { get; set; }

        /// <summary>商品单价，单位为分。</summary>
        public int unit_price { get; set; }

        /// <summary>商品优惠金额，单位为分。</summary>
        public int discount_amount { get; set; }

        /// <summary>商品备注。</summary>
        public string goods_remark { get; set; }
    }

    /// <summary>订单优惠详情。</summary>
    public class MiniProgramPayPromotionDetail
    {
        /// <summary>券 ID。</summary>
        public string coupon_id { get; set; }

        /// <summary>优惠名称。</summary>
        public string name { get; set; }

        /// <summary>优惠范围。</summary>
        public string scope { get; set; }

        /// <summary>优惠类型。</summary>
        public string type { get; set; }

        /// <summary>优惠金额，单位为分。</summary>
        public int amount { get; set; }

        /// <summary>活动 ID。</summary>
        public string stock_id { get; set; }

        /// <summary>微信支付出资金额。</summary>
        public int? wechatpay_contribute { get; set; }

        /// <summary>商户出资金额。</summary>
        public int? merchant_contribute { get; set; }

        /// <summary>其他出资金额。</summary>
        public int? other_contribute { get; set; }

        /// <summary>优惠币种。</summary>
        public string currency { get; set; }

        /// <summary>优惠单品列表。</summary>
        public IList<MiniProgramPayPromotionGoodsDetail> goods_detail { get; set; }
    }

    /// <summary>查询小程序支付订单结果，也是支付通知的明文基础结构。</summary>
    public class GetMiniProgramPayOrderResult : WorkJsonResult
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>交易类型。</summary>
        public string trade_type { get; set; }

        /// <summary>交易状态。</summary>
        public string trade_state { get; set; }

        /// <summary>交易状态说明。</summary>
        public string trade_state_desc { get; set; }

        /// <summary>付款银行类型。</summary>
        public string bank_type { get; set; }

        /// <summary>附加数据。</summary>
        public string attach { get; set; }

        /// <summary>支付完成时间，RFC 3339 格式。</summary>
        public string success_time { get; set; }

        /// <summary>支付者信息。</summary>
        public MiniProgramPayPayer payer { get; set; }

        /// <summary>订单金额。</summary>
        public MiniProgramPayOrderAmount amount { get; set; }

        /// <summary>支付场景信息。</summary>
        public MiniProgramPaySceneInfo scene_info { get; set; }

        /// <summary>优惠详情。</summary>
        public IList<MiniProgramPayPromotionDetail> promotion_detail { get; set; }
    }

    /// <summary>获取支付签名请求。</summary>
    public class GetMiniProgramPaySignRequest
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>下单接口返回的预支付交易会话标识。</summary>
        public string prepay_id { get; set; }

        /// <summary>签名方式，当前为 RSA。</summary>
        public string sign_type { get; set; }

        /// <summary>不超过 32 位的随机字符串。</summary>
        public string nonce { get; set; }

        /// <summary>秒级 Unix 时间戳，使用 64 位避免 2038 年问题。</summary>
        public long timestamp { get; set; }
    }

    /// <summary>获取支付签名结果。</summary>
    public class GetMiniProgramPaySignResult : WorkJsonResult
    {
        /// <summary>小程序支付签名。</summary>
        public string pay_sign { get; set; }
    }

    /// <summary>退款金额信息。</summary>
    public class MiniProgramPayRefundAmount
    {
        /// <summary>原订单总金额，单位为分。</summary>
        public int total { get; set; }

        /// <summary>退款金额，单位为分。</summary>
        public int refund { get; set; }

        /// <summary>退款币种，通常为 CNY。</summary>
        public string currency { get; set; }

        /// <summary>用户实际退款金额。</summary>
        public int? payer_refund { get; set; }

        /// <summary>优惠退款金额。</summary>
        public int? discount_refund { get; set; }

        /// <summary>用户原支付金额。</summary>
        public int? payer_total { get; set; }
    }

    /// <summary>申请小程序支付退款请求。</summary>
    public class MiniProgramPayRefundRequest
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>原支付交易的商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>退款原因。</summary>
        public string reason { get; set; }

        /// <summary>退款资金来源。</summary>
        public string funds_account { get; set; }

        /// <summary>退款金额信息。</summary>
        public MiniProgramPayRefundAmount amount { get; set; }
    }

    /// <summary>按商户号和退款单号标识退款。</summary>
    public class MiniProgramPayRefundIdentity
    {
        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }
    }

    /// <summary>优惠退款详情。</summary>
    public class MiniProgramPayRefundPromotionDetail
    {
        /// <summary>券或立减优惠 ID。</summary>
        public string promotion_id { get; set; }

        /// <summary>优惠范围。</summary>
        public string scope { get; set; }

        /// <summary>优惠类型。</summary>
        public string type { get; set; }

        /// <summary>优惠券面额。</summary>
        public int amount { get; set; }

        /// <summary>优惠退款金额。</summary>
        public int refund_amount { get; set; }
    }

    /// <summary>申请退款结果。</summary>
    public class MiniProgramPayRefundResult : WorkJsonResult
    {
        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>退款金额信息。</summary>
        public MiniProgramPayRefundAmount amount { get; set; }

        /// <summary>优惠退款详情。</summary>
        public IList<MiniProgramPayRefundPromotionDetail> promotion_detail { get; set; }
    }

    /// <summary>查询退款详情结果。</summary>
    public class GetMiniProgramPayRefundDetailResult : MiniProgramPayRefundResult
    {
        /// <summary>微信支付退款单号。</summary>
        public string refund_id { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>原商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>退款渠道。</summary>
        public string channel { get; set; }

        /// <summary>退款入账账户。</summary>
        public string user_received_account { get; set; }

        /// <summary>退款状态。</summary>
        public string status { get; set; }

        /// <summary>退款成功时间，RFC 3339 格式。</summary>
        public string success_time { get; set; }

        /// <summary>退款受理时间，RFC 3339 格式。</summary>
        public string create_time { get; set; }
    }

    /// <summary>申请交易账单请求。</summary>
    public class GetMiniProgramPayBillRequest
    {
        /// <summary>企业微信分配的商户号。</summary>
        public string mchid { get; set; }

        /// <summary>账单日期，格式为 yyyy-MM-dd。</summary>
        public string bill_date { get; set; }

        /// <summary>账单类型。</summary>
        public string bill_type { get; set; }

        /// <summary>压缩类型。</summary>
        public string tar_type { get; set; }
    }

    /// <summary>申请交易账单结果。</summary>
    public class GetMiniProgramPayBillResult : WorkJsonResult
    {
        /// <summary>哈希类型。</summary>
        public string hash_type { get; set; }

        /// <summary>账单文件哈希值。</summary>
        public string hash_value { get; set; }

        /// <summary>账单下载地址。</summary>
        public string download_url { get; set; }

        /// <summary>下载时使用的 Authorization 请求头。</summary>
        public string auth { get; set; }
    }

    /// <summary>支付成功通知解密后的业务数据。</summary>
    public class MiniProgramPayTransactionNotification
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>交易状态。</summary>
        public string trade_state { get; set; }

        /// <summary>附加数据。</summary>
        public string attach { get; set; }

        /// <summary>支付完成时间。</summary>
        public string success_time { get; set; }

        /// <summary>订单金额。</summary>
        public MiniProgramPayOrderAmount amount { get; set; }

        /// <summary>付款银行类型。</summary>
        public string bank_type { get; set; }

        /// <summary>支付者信息。</summary>
        public MiniProgramPayPayer payer { get; set; }

        /// <summary>优惠详情。</summary>
        public IList<MiniProgramPayPromotionDetail> promotion_detail { get; set; }

        /// <summary>交易状态说明。</summary>
        public string trade_state_desc { get; set; }

        /// <summary>交易类型。</summary>
        public string trade_type { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }
    }

    /// <summary>退款通知解密后的业务数据。</summary>
    public class MiniProgramPayRefundNotification
    {
        /// <summary>商户号。</summary>
        public string mchid { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }

        /// <summary>微信支付退款单号。</summary>
        public string refund_id { get; set; }

        /// <summary>退款状态。</summary>
        public string refund_status { get; set; }

        /// <summary>退款成功时间。</summary>
        public string success_time { get; set; }

        /// <summary>退款入账账户。</summary>
        public string user_received_account { get; set; }

        /// <summary>金额信息。</summary>
        public MiniProgramPayRefundAmount amount { get; set; }
    }
}
