/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：PayToolJson.cs
    文件功能描述：企业微信服务商收银台收款工具强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐收款订单和发票管理强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.PayTool
{
    /// <summary>
    /// 收款工具签名请求公共字段。
    /// </summary>
    public abstract class PayToolSignedRequestBase
    {
        /// <summary>
        /// 32 字节以内的防重放随机字符串，15 分钟内不可重复。
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// Unix 时间戳，精确到秒；与企业微信服务器时间差不可超过 15 分钟。
        /// </summary>
        public long ts { get; set; }

        /// <summary>
        /// 使用收银台 API 调用密钥生成的 HMAC-SHA256 Base64 签名。
        /// </summary>
        public string sig { get; set; }
    }

    /// <summary>
    /// 获取发票列表请求。
    /// </summary>
    public class PayToolGetInvoiceListRequest
    {
        /// <summary>
        /// 申请开票开始时间；与 end_time 同时提供。
        /// </summary>
        public long? start_time { get; set; }

        /// <summary>
        /// 申请开票结束时间；与 start_time 同时提供。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 上一页返回的分页游标；首次请求可不填。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 最大返回记录数，最大 100，默认 50。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取发票列表结果。
    /// </summary>
    public class PayToolGetInvoiceListResult : WorkJsonResult
    {
        /// <summary>
        /// 是否还有更多数据：0 表示没有，1 表示有。
        /// </summary>
        public int has_more { get; set; }

        /// <summary>
        /// 下一页分页游标。
        /// </summary>
        public string next_cursor { get; set; }

        /// <summary>
        /// 发票申请列表。
        /// </summary>
        public List<PayToolInvoiceInfo> invoice_list { get; set; }
    }

    /// <summary>
    /// 应用订单发票申请信息。
    /// </summary>
    public class PayToolInvoiceInfo
    {
        /// <summary>
        /// 申请开票的订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单对应的客户企业 CorpId。
        /// </summary>
        public string custom_corpid { get; set; }

        /// <summary>
        /// 客户申请开票的 Unix 时间戳。
        /// </summary>
        public long apply_time { get; set; }

        /// <summary>
        /// 发票类型：0 普通发票，1 增值税专用发票。
        /// </summary>
        public int invoice_type { get; set; }

        /// <summary>
        /// 实付金额，单位分。
        /// </summary>
        public long paid_price { get; set; }

        /// <summary>
        /// 开票状态：0 开票中，1 已寄出，2 已发送，3 已取消。
        /// </summary>
        public int invoice_status { get; set; }

        /// <summary>
        /// 发票抬头。
        /// </summary>
        public string invoice_title { get; set; }

        /// <summary>
        /// 纳税人识别号。
        /// </summary>
        public string tax_number { get; set; }

        /// <summary>
        /// 发票收取方式：0 待定，1 快递，2 电子邮箱。
        /// </summary>
        public int send_way { get; set; }

        /// <summary>
        /// 联系人姓名。
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// 联系电话。
        /// </summary>
        public string contact_tel { get; set; }

        /// <summary>
        /// 收件地址。
        /// </summary>
        public string contact_addr { get; set; }

        /// <summary>
        /// 邮政编码。
        /// </summary>
        public string contact_postcode { get; set; }

        /// <summary>
        /// 接收电子发票的邮箱。
        /// </summary>
        public string receive_email { get; set; }

        /// <summary>
        /// 公司地址。
        /// </summary>
        public string company_addr { get; set; }

        /// <summary>
        /// 公司电话。
        /// </summary>
        public string company_tel { get; set; }

        /// <summary>
        /// 开户行。
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 银行账号。
        /// </summary>
        public string bank_account_number { get; set; }

        /// <summary>
        /// 客户可见的开票备注。
        /// </summary>
        public string invoice_note { get; set; }
    }

    /// <summary>
    /// 标记开票状态请求。
    /// </summary>
    public class PayToolMarkInvoiceStatusRequest
    {
        /// <summary>
        /// 要标记开票状态的订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 操作人 UserId；操作人需具有收银台发票管理权限。
        /// </summary>
        public string oper_userid { get; set; }

        /// <summary>
        /// 目标状态：1 已寄出纸质发票，2 已发送电子发票，3 取消开具。
        /// </summary>
        public int invoice_status { get; set; }

        /// <summary>
        /// 客户可见备注，不超过 200 字节。
        /// </summary>
        public string invoice_note { get; set; }
    }

    /// <summary>
    /// 创建收款订单请求。
    /// </summary>
    public class PayToolOpenOrderRequest : PayToolSignedRequestBase
    {
        /// <summary>
        /// 业务类型：1 普通第三方应用，2 代开发应用，3 行业解决方案。
        /// </summary>
        public int business_type { get; set; }

        /// <summary>
        /// 指定客户企业 CorpId；代开发应用业务必填。
        /// </summary>
        public string custom_corpid { get; set; }

        /// <summary>
        /// 支付方式：0 客户支付，1 服务商代支付，2 免支付。
        /// </summary>
        public int pay_type { get; set; }

        /// <summary>
        /// 服务商代支付凭证的临时素材 MediaId。
        /// </summary>
        public string bank_receipt_media_id { get; set; }

        /// <summary>
        /// 订单创建人 UserId；设置后可接收取消或确认失败提醒。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 与 business_type 对应的购买商品。
        /// </summary>
        public PayToolProductList product_list { get; set; }
    }

    /// <summary>
    /// 创建订单商品联合模型；按业务类型仅填写对应的一项。
    /// </summary>
    public class PayToolProductList
    {
        /// <summary>
        /// 普通第三方应用购买详情。
        /// </summary>
        public PayToolThirdAppProduct third_app { get; set; }

        /// <summary>
        /// 代开发应用购买详情。
        /// </summary>
        public PayToolCustomizedAppProduct customized_app { get; set; }

        /// <summary>
        /// 行业解决方案购买详情。
        /// </summary>
        public PayToolPromotionCaseProduct promotion_case { get; set; }
    }

    /// <summary>
    /// 创建订单商品公共字段。
    /// </summary>
    public abstract class PayToolProductBase
    {
        /// <summary>
        /// 购买类型：0 新购，1 扩容，2 续期。
        /// </summary>
        public int order_type { get; set; }

        /// <summary>
        /// 是否向指定客户企业推送确认或支付提醒：0 否，1 是；不填默认是。
        /// </summary>
        public int? notify_custom_corp { get; set; }
    }

    /// <summary>
    /// 普通第三方应用购买详情。
    /// </summary>
    public class PayToolThirdAppProduct : PayToolProductBase
    {
        /// <summary>
        /// 购买应用列表，支持 1 至 20 项。
        /// </summary>
        public List<PayToolThirdAppBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 普通第三方应用单项购买信息。
    /// </summary>
    public class PayToolThirdAppBuyInfo
    {
        /// <summary>
        /// 套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 应用 ID，仅旧套件应用需要填写。
        /// </summary>
        public int? appid { get; set; }

        /// <summary>
        /// 应用版本 ID。
        /// </summary>
        public string edition_id { get; set; }

        /// <summary>
        /// 购买人数；扩容时表示新增人数。
        /// </summary>
        public int? user_count { get; set; }

        /// <summary>
        /// 购买时长，单位天，范围 1 至 1825。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd；不填表示按官方规则立即或接续生效。
        /// </summary>
        public string take_effect_date { get; set; }

        /// <summary>
        /// 可选优惠信息。
        /// </summary>
        public PayToolDiscountInfo discount_info { get; set; }
    }

    /// <summary>
    /// 普通第三方应用优惠信息。
    /// </summary>
    public class PayToolDiscountInfo
    {
        /// <summary>
        /// 优惠类型：1 固定优惠，2 价格折扣。
        /// </summary>
        public int discount_type { get; set; }

        /// <summary>
        /// 固定优惠金额，单位分。
        /// </summary>
        public long? discount_amount { get; set; }

        /// <summary>
        /// 优惠后价格比例，单位百分比，例如 75 表示 7.5 折。
        /// </summary>
        public int? discount_ratio { get; set; }

        /// <summary>
        /// 优惠原因，不超过 256 字节。
        /// </summary>
        public string discount_remarks { get; set; }
    }

    /// <summary>
    /// 代开发应用购买详情。
    /// </summary>
    public class PayToolCustomizedAppProduct : PayToolProductBase
    {
        /// <summary>
        /// 购买应用列表，支持 1 至 20 项。
        /// </summary>
        public List<PayToolCustomizedAppBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 代开发应用单项购买信息。
    /// </summary>
    public class PayToolCustomizedAppBuyInfo
    {
        /// <summary>
        /// 代开发模板套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 购买人数；扩容时表示新增人数。
        /// </summary>
        public int? user_count { get; set; }

        /// <summary>
        /// 购买时长，单位天，范围 1 至 1825。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd。
        /// </summary>
        public string take_effect_date { get; set; }

        /// <summary>
        /// 应用总价，单位分，需大于 0 且不超过 500 万元。
        /// </summary>
        public long total_price { get; set; }
    }

    /// <summary>
    /// 行业解决方案购买详情。
    /// </summary>
    public class PayToolPromotionCaseProduct : PayToolProductBase
    {
        /// <summary>
        /// 行业方案 ID。
        /// </summary>
        public string case_id { get; set; }

        /// <summary>
        /// 行业方案版本名。
        /// </summary>
        public string promotion_edition_name { get; set; }

        /// <summary>
        /// 购买时长，单位天，范围 1 至 1825。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd；不填表示立即生效。
        /// </summary>
        public string take_effect_date { get; set; }

        /// <summary>
        /// 方案内购买应用列表，支持 1 至 20 项。
        /// </summary>
        public List<PayToolPromotionCaseBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 行业解决方案内单项应用购买信息。
    /// </summary>
    public class PayToolPromotionCaseBuyInfo
    {
        /// <summary>
        /// 套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 应用 ID，仅旧套件应用需要填写。
        /// </summary>
        public int? appid { get; set; }

        /// <summary>
        /// 购买人数；扩容时表示新增人数。
        /// </summary>
        public int? user_count { get; set; }
    }

    /// <summary>
    /// 创建收款订单结果。
    /// </summary>
    public class PayToolOpenOrderResult : WorkJsonResult
    {
        /// <summary>
        /// 收款订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 客户支付链接；服务商代支付或免支付时为订单确认链接。
        /// </summary>
        public string order_url { get; set; }

        /// <summary>
        /// 可确定时返回的原价，单位分。
        /// </summary>
        public long? origin_price { get; set; }

        /// <summary>
        /// 可确定时返回的折后价，单位分。
        /// </summary>
        public long? paid_price { get; set; }
    }

    /// <summary>
    /// 取消收款订单请求。
    /// </summary>
    public class PayToolCloseOrderRequest : PayToolSignedRequestBase
    {
        /// <summary>
        /// 待取消的收款订单号，不超过 64 字节。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 获取收款订单列表请求。
    /// </summary>
    public class PayToolGetOrderListRequest : PayToolSignedRequestBase
    {
        /// <summary>
        /// 可选业务类型：1 普通第三方应用，2 代开发应用，3 行业解决方案。
        /// </summary>
        public int? business_type { get; set; }

        /// <summary>
        /// 订单创建起始 Unix 时间戳。
        /// </summary>
        public long? start_time { get; set; }

        /// <summary>
        /// 订单创建结束 Unix 时间戳。
        /// </summary>
        public long? end_time { get; set; }

        /// <summary>
        /// 上一页返回的分页游标；首次请求可不填。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 预期返回记录数，官方范围 1 至 2000。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 获取收款订单列表结果。
    /// </summary>
    public class PayToolGetOrderListResult : WorkJsonResult
    {
        /// <summary>
        /// 下一页分页游标。
        /// </summary>
        public string next_cursor { get; set; }

        /// <summary>
        /// 是否还有更多数据：0 表示没有，1 表示有。
        /// </summary>
        public int has_more { get; set; }

        /// <summary>
        /// 收款订单摘要列表。
        /// </summary>
        public List<PayToolOrderSummary> pay_order_list { get; set; }
    }

    /// <summary>
    /// 收款订单摘要。
    /// </summary>
    public class PayToolOrderSummary
    {
        /// <summary>
        /// 收款订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单创建 Unix 时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 客户企业 CorpId。
        /// </summary>
        public string custom_corpid { get; set; }

        /// <summary>
        /// 购买内容摘要。
        /// </summary>
        public string buy_content { get; set; }

        /// <summary>
        /// 原价金额，单位分。
        /// </summary>
        public long origin_price { get; set; }

        /// <summary>
        /// 实付金额，单位分；免支付订单为 0。
        /// </summary>
        public long paid_price { get; set; }

        /// <summary>
        /// 订单状态：1 待支付、2 已支付、3 已取消、4 已过期、5 退款申请中、6 已退款、7 交易完成、8 待企业确认、9 已部分退款。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 订单来源：1 客户下单，2 服务商创建。
        /// </summary>
        public int order_from { get; set; }

        /// <summary>
        /// 订单创建人 UserId。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 支付方式：0 客户支付，1 服务商代支付，2 免支付。
        /// </summary>
        public int pay_type { get; set; }
    }

    /// <summary>
    /// 获取收款订单详情请求。
    /// </summary>
    public class PayToolGetOrderDetailRequest : PayToolSignedRequestBase
    {
        /// <summary>
        /// 待查询的收款订单号。
        /// </summary>
        public string order_id { get; set; }
    }

    /// <summary>
    /// 获取收款订单详情结果。
    /// </summary>
    public class PayToolGetOrderDetailResult : WorkJsonResult
    {
        /// <summary>
        /// 收款订单完整详情。
        /// </summary>
        public PayToolOrderInfo pay_order { get; set; }
    }

    /// <summary>
    /// 收款订单、支付、到账和商品明细。
    /// </summary>
    public class PayToolOrderInfo
    {
        /// <summary>
        /// 收款订单号。
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单创建 Unix 时间戳。
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 客户企业 CorpId。
        /// </summary>
        public string custom_corpid { get; set; }

        /// <summary>
        /// 购买内容摘要。
        /// </summary>
        public string buy_content { get; set; }

        /// <summary>
        /// 原价金额，单位分。
        /// </summary>
        public long origin_price { get; set; }

        /// <summary>
        /// 实付金额，单位分；免支付订单为 0。
        /// </summary>
        public long paid_price { get; set; }

        /// <summary>
        /// 订单状态：1 待支付、2 已支付、3 已取消、4 已过期、5 退款申请中、6 已退款、7 交易完成、8 待企业确认、9 已部分退款。
        /// </summary>
        public int order_status { get; set; }

        /// <summary>
        /// 订单来源：1 客户下单，2 服务商创建。
        /// </summary>
        public int order_from { get; set; }

        /// <summary>
        /// 订单创建人 UserId；官方返回示例为字符串。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 支付方式：0 客户支付，1 服务商代支付，2 免支付。
        /// </summary>
        public int pay_type { get; set; }

        /// <summary>
        /// 客户企业简称。
        /// </summary>
        public string custom_corp_name { get; set; }

        /// <summary>
        /// 付款渠道：1 微信支付，2 网银支付；未支付时为空。
        /// </summary>
        public int? pay_channel { get; set; }

        /// <summary>
        /// 付款流水号；未支付时为空。
        /// </summary>
        public string channel_order_id { get; set; }

        /// <summary>
        /// 付款 Unix 时间戳；未支付时为空。
        /// </summary>
        public long? paid_time { get; set; }

        /// <summary>
        /// 业务类型：1 普通第三方应用，2 代开发应用，3 行业解决方案。
        /// </summary>
        public int business_type { get; set; }

        /// <summary>
        /// 收入到账商户号类型：1 微信支付商户号，2 财付通商户号；未到账时为空。
        /// </summary>
        public int? income_type { get; set; }

        /// <summary>
        /// 到账 Unix 时间戳；未到账时为空。
        /// </summary>
        public long? income_time { get; set; }

        /// <summary>
        /// 到账金额，单位分；未到账时为空。
        /// </summary>
        public long? income_amount { get; set; }

        /// <summary>
        /// 与业务类型对应的购买商品明细。
        /// </summary>
        public PayToolOrderProductList product_list { get; set; }
    }

    /// <summary>
    /// 订单详情商品联合模型；按业务类型返回对应的一项。
    /// </summary>
    public class PayToolOrderProductList
    {
        /// <summary>
        /// 普通第三方应用订单商品。
        /// </summary>
        public PayToolOrderThirdAppProduct third_app { get; set; }

        /// <summary>
        /// 代开发应用订单商品。
        /// </summary>
        public PayToolOrderCustomizedAppProduct customized_app { get; set; }

        /// <summary>
        /// 行业解决方案订单商品。
        /// </summary>
        public PayToolOrderPromotionCaseProduct promotion_case { get; set; }
    }

    /// <summary>
    /// 订单详情商品公共字段。
    /// </summary>
    public abstract class PayToolOrderProductBase
    {
        /// <summary>
        /// 购买类型：0 新购，1 扩容，2 续期。
        /// </summary>
        public int order_type { get; set; }
    }

    /// <summary>
    /// 普通第三方应用订单商品。
    /// </summary>
    public class PayToolOrderThirdAppProduct : PayToolOrderProductBase
    {
        /// <summary>
        /// 已购买应用列表。
        /// </summary>
        public List<PayToolOrderThirdAppBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 普通第三方应用订单单项明细。
    /// </summary>
    public class PayToolOrderThirdAppBuyInfo
    {
        /// <summary>
        /// 套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 应用 ID，仅旧套件应用返回。
        /// </summary>
        public int? appid { get; set; }

        /// <summary>
        /// 应用版本 ID。
        /// </summary>
        public string edition_id { get; set; }

        /// <summary>
        /// 购买人数。
        /// </summary>
        public int? user_count { get; set; }

        /// <summary>
        /// 购买时长，单位天。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 原价金额，单位分。
        /// </summary>
        public long origin_price { get; set; }

        /// <summary>
        /// 实付金额，单位分。
        /// </summary>
        public long paid_price { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd。
        /// </summary>
        public string take_effect_date { get; set; }
    }

    /// <summary>
    /// 代开发应用订单商品。
    /// </summary>
    public class PayToolOrderCustomizedAppProduct : PayToolOrderProductBase
    {
        /// <summary>
        /// 已购买应用列表。
        /// </summary>
        public List<PayToolOrderCustomizedAppBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 代开发应用订单单项明细。
    /// </summary>
    public class PayToolOrderCustomizedAppBuyInfo
    {
        /// <summary>
        /// 代开发模板套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 购买人数。
        /// </summary>
        public int? user_count { get; set; }

        /// <summary>
        /// 购买时长，单位天。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 原价金额，单位分。
        /// </summary>
        public long origin_price { get; set; }

        /// <summary>
        /// 实付金额，单位分。
        /// </summary>
        public long paid_price { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd。
        /// </summary>
        public string take_effect_date { get; set; }
    }

    /// <summary>
    /// 行业解决方案订单商品。
    /// </summary>
    public class PayToolOrderPromotionCaseProduct : PayToolOrderProductBase
    {
        /// <summary>
        /// 行业方案 ID。
        /// </summary>
        public string case_id { get; set; }

        /// <summary>
        /// 行业方案版本名。
        /// </summary>
        public string promotion_edition_name { get; set; }

        /// <summary>
        /// 购买时长，单位天。
        /// </summary>
        public int? duration_days { get; set; }

        /// <summary>
        /// 方案内购买应用列表。
        /// </summary>
        public List<PayToolOrderPromotionCaseBuyInfo> buy_info_list { get; set; }
    }

    /// <summary>
    /// 行业解决方案订单单项应用明细。
    /// </summary>
    public class PayToolOrderPromotionCaseBuyInfo
    {
        /// <summary>
        /// 套件 ID。
        /// </summary>
        public string suiteid { get; set; }

        /// <summary>
        /// 应用 ID，仅旧套件应用返回。
        /// </summary>
        public int? appid { get; set; }

        /// <summary>
        /// 购买人数。
        /// </summary>
        public int? user_count { get; set; }

        /// <summary>
        /// 生效日期，格式 yyyyMMdd。
        /// </summary>
        public string take_effect_date { get; set; }
    }
}
