/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：InvoiceBatchJson.cs
    文件功能描述：企业微信批量查询电子发票请求及返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐当前批量查询电子发票强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>批量查询电子发票请求。</summary>
    public class GetInvoiceInfoBatchRequest
    {
        /// <summary>待查询的电子发票标识列表。</summary>
        public List<InvoiceItem> item_list { get; set; }
    }

    /// <summary>批量查询电子发票结果。</summary>
    public class GetInvoiceInfoBatchResultJson : WorkJsonResult
    {
        /// <summary>电子发票结构化信息列表。</summary>
        public List<GetInvoiceInfoBatchItem> item_list { get; set; }
    }

    /// <summary>批量查询返回的单张电子发票信息。</summary>
    public class GetInvoiceInfoBatchItem
    {
        /// <summary>发票 CardId。</summary>
        public string card_id { get; set; }

        /// <summary>发票有效期起始时间，Unix 时间戳。</summary>
        public long begin_time { get; set; }

        /// <summary>发票有效期截止时间，Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>选择该发票的用户 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>发票类型，如广东增值税普通发票。</summary>
        public string type { get; set; }

        /// <summary>发票收款方。</summary>
        public string payee { get; set; }

        /// <summary>发票详情或使用说明。</summary>
        public string detail { get; set; }

        /// <summary>发票票面及报销状态信息。</summary>
        public InvoiceBatchUserInfo user_info { get; set; }
    }

    /// <summary>批量查询电子发票返回的票面及报销信息。</summary>
    public class InvoiceBatchUserInfo
    {
        /// <summary>价税合计金额，单位为分。</summary>
        public long fee { get; set; }

        /// <summary>发票抬头。</summary>
        public string title { get; set; }

        /// <summary>开票时间，Unix 时间戳。</summary>
        public long billing_time { get; set; }

        /// <summary>发票代码。</summary>
        public string billing_no { get; set; }

        /// <summary>发票号码。</summary>
        public string billing_code { get; set; }

        /// <summary>税额，单位为分。</summary>
        public long tax { get; set; }

        /// <summary>不含税金额，单位为分。</summary>
        public long fee_without_tax { get; set; }

        /// <summary>发票详情，一般用于说明发票用途。</summary>
        public string detail { get; set; }

        /// <summary>发票 PDF 文件地址。</summary>
        public string pdf_url { get; set; }

        /// <summary>行程单、水单等其他消费凭证附件地址。</summary>
        public string trip_pdf_url { get; set; }

        /// <summary>发票校验码。</summary>
        public string check_code { get; set; }

        /// <summary>购买方纳税人识别号。</summary>
        public string buyer_number { get; set; }

        /// <summary>购买方地址和电话。</summary>
        public string buyer_address_and_phone { get; set; }

        /// <summary>购买方开户行及账号。</summary>
        public string buyer_bank_account { get; set; }

        /// <summary>销售方纳税人识别号。</summary>
        public string seller_number { get; set; }

        /// <summary>销售方地址和电话。</summary>
        public string seller_address_and_phone { get; set; }

        /// <summary>销售方开户行及账号。</summary>
        public string seller_bank_account { get; set; }

        /// <summary>发票备注。</summary>
        public string remarks { get; set; }

        /// <summary>收款人。</summary>
        public string cashier { get; set; }

        /// <summary>开票人。</summary>
        public string maker { get; set; }

        /// <summary>报销状态，如 INVOICE_REIMBURSE_INIT、LOCK 或 CLOSURE。</summary>
        public string reimburse_status { get; set; }

        /// <summary>官方返回示例中的关联订单号。</summary>
        public string order_id { get; set; }

        /// <summary>商品或服务明细列表。</summary>
        public List<InvoiceBatchProjectInfo> info { get; set; }
    }

    /// <summary>电子发票中的单项商品或服务明细。</summary>
    public class InvoiceBatchProjectInfo
    {
        /// <summary>项目或商品名称。</summary>
        public string name { get; set; }

        /// <summary>项目数量。</summary>
        public int num { get; set; }

        /// <summary>项目单位。</summary>
        public string unit { get; set; }

        /// <summary>官方返回示例中的项目金额，单位为分。</summary>
        public long fee { get; set; }

        /// <summary>项目单价，单位为分。</summary>
        public long price { get; set; }
    }
}
