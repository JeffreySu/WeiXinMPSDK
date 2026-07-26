/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseOrderJson.cs
    文件功能描述：企业微信服务商接口调用许可订单强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可订单、跨企业任务和余额支付强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>许可账号购买数量。</summary>
    public class LicenseAccountCount
    {
        /// <summary>基础账号购买数量。</summary>
        public int base_count { get; set; }

        /// <summary>互通账号购买数量。</summary>
        public int external_contact_count { get; set; }
    }

    /// <summary>许可账号购买或续期时长。</summary>
    public class LicenseAccountDuration
    {
        /// <summary>购买或续期月数。</summary>
        public int? months { get; set; }

        /// <summary>购买或续期天数。</summary>
        public int? days { get; set; }

        /// <summary>续期后的指定到期时间，Unix 时间戳。</summary>
        public long? new_expire_time { get; set; }
    }

    /// <summary>为单个企业下单购买许可账号请求。</summary>
    public class LicenseCreateOrderRequest
    {
        /// <summary>购买许可账号的企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>服务商企业内下单人 UserId。</summary>
        public string buyer_userid { get; set; }

        /// <summary>基础账号和互通账号购买数量。</summary>
        public LicenseAccountCount account_count { get; set; }

        /// <summary>账号购买时长。</summary>
        public LicenseAccountDuration account_duration { get; set; }
    }

    /// <summary>许可订单创建结果。</summary>
    public class LicenseCreateOrderResult : WorkJsonResult
    {
        /// <summary>许可订单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>许可订单中的成员账号。</summary>
    public class LicenseOrderAccount
    {
        /// <summary>企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>账号类型：1 基础账号，2 互通账号。</summary>
        public int type { get; set; }
    }

    /// <summary>创建许可账号续期任务请求。</summary>
    public class LicenseCreateRenewOrderJobRequest
    {
        /// <summary>续期账号所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待续期成员账号列表。</summary>
        public List<LicenseOrderAccount> account_list { get; set; }

        /// <summary>调用方生成的幂等任务号。</summary>
        public string jobid { get; set; }
    }

    /// <summary>创建许可账号续期任务结果。</summary>
    public class LicenseCreateRenewOrderJobResult : WorkJsonResult
    {
        /// <summary>企业微信返回的续期任务号。</summary>
        public string jobid { get; set; }

        /// <summary>无法续期的账号列表。</summary>
        public List<LicenseInvalidOrderAccount> invalid_account_list { get; set; }
    }

    /// <summary>无效许可订单账号。</summary>
    public class LicenseInvalidOrderAccount : LicenseOrderAccount
    {
        /// <summary>账号校验错误码。</summary>
        public int errcode { get; set; }

        /// <summary>账号校验错误说明。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>提交许可续期任务请求。</summary>
    public class LicenseSubmitRenewOrderJobRequest
    {
        /// <summary>待提交的续期任务号。</summary>
        public string jobid { get; set; }

        /// <summary>服务商企业内下单人 UserId。</summary>
        public string buyer_userid { get; set; }

        /// <summary>账号续期时长或新的到期时间。</summary>
        public LicenseAccountDuration account_duration { get; set; }
    }

    /// <summary>分页获取许可订单列表请求。</summary>
    public class LicenseOrderListRequest
    {
        /// <summary>订单所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>订单创建时间范围起点，Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>订单创建时间范围终点，Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>分页游标，首次请求不填。</summary>
        public string cursor { get; set; }

        /// <summary>每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>许可订单列表结果。</summary>
    public class LicenseOrderListResult : WorkJsonResult
    {
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否还有更多数据，零表示没有，一表示有。</summary>
        public int has_more { get; set; }

        /// <summary>许可订单摘要列表。</summary>
        public List<LicenseOrderSummary> order_list { get; set; }
    }

    /// <summary>许可订单摘要。</summary>
    public class LicenseOrderSummary
    {
        /// <summary>许可订单号。</summary>
        public string order_id { get; set; }

        /// <summary>订单类型。</summary>
        public int order_type { get; set; }
    }

    /// <summary>仅包含许可订单号的请求。</summary>
    public class LicenseOrderIdRequest
    {
        /// <summary>待查询许可订单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>获取许可订单详情结果。</summary>
    public class LicenseGetOrderResult : WorkJsonResult
    {
        /// <summary>许可订单详情。</summary>
        public LicenseOrderDetail order { get; set; }
    }

    /// <summary>单企业许可订单详情。</summary>
    public class LicenseOrderDetail : LicenseOrderSummary
    {
        /// <summary>订单状态。</summary>
        public int order_status { get; set; }

        /// <summary>订单所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>订单价格，单位为分。</summary>
        public long price { get; set; }

        /// <summary>订单购买的账号数量。</summary>
        public LicenseAccountCount account_count { get; set; }

        /// <summary>订单购买或续期时长。</summary>
        public LicenseAccountDuration account_duration { get; set; }

        /// <summary>订单创建时间，Unix 时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>订单支付时间，Unix 时间戳。</summary>
        public long? pay_time { get; set; }
    }

    /// <summary>分页获取订单账号列表请求。</summary>
    public class LicenseOrderAccountListRequest : LicenseOrderIdRequest
    {
        /// <summary>每页数量。</summary>
        public int? limit { get; set; }

        /// <summary>分页游标，首次请求不填。</summary>
        public string cursor { get; set; }
    }

    /// <summary>许可订单账号列表结果。</summary>
    public class LicenseOrderAccountListResult : WorkJsonResult
    {
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否还有更多数据，零表示没有，一表示有。</summary>
        public int has_more { get; set; }

        /// <summary>订单中的成员账号和激活码列表。</summary>
        public List<LicenseOrderAccountCode> account_list { get; set; }
    }

    /// <summary>许可订单中的成员账号和激活码。</summary>
    public class LicenseOrderAccountCode : LicenseOrderAccount
    {
        /// <summary>分配给账号的激活码。</summary>
        public string active_code { get; set; }
    }

    /// <summary>取消许可订单请求。</summary>
    public class LicenseCancelOrderRequest : LicenseOrderIdRequest
    {
        /// <summary>订单所属企业 CorpId。</summary>
        public string corpid { get; set; }
    }

    /// <summary>创建跨企业许可购买任务请求。</summary>
    public class LicenseCreateMultiCorpOrderJobRequest
    {
        /// <summary>多个企业的许可购买列表。</summary>
        public List<LicenseMultiCorpBuyItem> buy_list { get; set; }

        /// <summary>调用方生成的幂等任务号。</summary>
        public string jobid { get; set; }
    }

    /// <summary>跨企业许可购买项。</summary>
    public class LicenseMultiCorpBuyItem
    {
        /// <summary>购买许可账号的企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>基础账号和互通账号购买数量。</summary>
        public LicenseAccountCount account_count { get; set; }

        /// <summary>账号购买时长。</summary>
        public LicenseAccountDuration account_duration { get; set; }

        /// <summary>企业许可账号自动激活状态。</summary>
        public int auto_active_status { get; set; }
    }

    /// <summary>创建跨企业许可购买任务结果。</summary>
    public class LicenseCreateMultiCorpOrderJobResult : WorkJsonResult
    {
        /// <summary>企业微信返回的任务号。</summary>
        public string jobid { get; set; }

        /// <summary>请求中无效的企业列表。</summary>
        public List<LicenseInvalidCorp> invalid_list { get; set; }
    }

    /// <summary>许可订单任务中的无效或失败企业。</summary>
    public class LicenseInvalidCorp
    {
        /// <summary>企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>该企业的错误码。</summary>
        public int errcode { get; set; }

        /// <summary>该企业的错误说明。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>提交跨企业许可购买任务请求。</summary>
    public class LicenseSubmitMultiCorpOrderJobRequest
    {
        /// <summary>待提交的跨企业购买任务号。</summary>
        public string jobid { get; set; }

        /// <summary>服务商企业内下单人 UserId。</summary>
        public string buyer_userid { get; set; }
    }

    /// <summary>仅包含许可异步任务号的请求。</summary>
    public class LicenseJobIdRequest
    {
        /// <summary>待查询任务号。</summary>
        public string jobid { get; set; }
    }

    /// <summary>跨企业许可购买任务结果。</summary>
    public class LicenseMultiCorpOrderJobResult : WorkJsonResult
    {
        /// <summary>任务状态。</summary>
        public int status { get; set; }

        /// <summary>任务成功后生成的联合订单号。</summary>
        public string order_id { get; set; }

        /// <summary>购买失败的企业列表。</summary>
        public List<LicenseInvalidCorp> fail_list { get; set; }
    }

    /// <summary>分页获取联合订单请求。</summary>
    public class LicenseUnionOrderRequest : LicenseOrderIdRequest
    {
        /// <summary>每页企业子订单数量。</summary>
        public int? limit { get; set; }

        /// <summary>分页游标，首次请求不填。</summary>
        public string cursor { get; set; }
    }

    /// <summary>联合许可订单结果。</summary>
    public class LicenseUnionOrderResult : WorkJsonResult
    {
        /// <summary>联合订单基本信息。</summary>
        public LicenseUnionOrderInfo order { get; set; }

        /// <summary>是否还有更多企业子订单，零表示没有，一表示有。</summary>
        public int has_more { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>各企业许可购买子订单列表。</summary>
        public List<LicenseUnionBuyItem> buy_list { get; set; }
    }

    /// <summary>联合许可订单基本信息。</summary>
    public class LicenseUnionOrderInfo : LicenseOrderSummary
    {
        /// <summary>联合订单状态。</summary>
        public int order_status { get; set; }

        /// <summary>联合订单价格，单位为分。</summary>
        public long price { get; set; }

        /// <summary>订单创建时间，Unix 时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>订单支付时间，Unix 时间戳。</summary>
        public long? pay_time { get; set; }
    }

    /// <summary>联合订单中的单个企业购买信息。</summary>
    public class LicenseUnionBuyItem
    {
        /// <summary>企业子订单号。</summary>
        public string sub_order_id { get; set; }

        /// <summary>购买许可账号的企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>基础账号和互通账号购买数量。</summary>
        public LicenseAccountCount account_count { get; set; }

        /// <summary>账号购买时长。</summary>
        public LicenseAccountDuration account_duration { get; set; }
    }

    /// <summary>提交余额支付任务请求。</summary>
    public class LicenseSubmitPaymentJobRequest : LicenseOrderIdRequest
    {
        /// <summary>服务商企业内付款人 UserId。</summary>
        public string payer_userid { get; set; }
    }

    /// <summary>提交余额支付任务结果。</summary>
    public class LicenseSubmitPaymentJobResult : WorkJsonResult
    {
        /// <summary>余额支付任务号。</summary>
        public string jobid { get; set; }
    }

    /// <summary>余额支付任务查询结果。</summary>
    public class LicensePaymentJobResult : WorkJsonResult
    {
        /// <summary>支付任务状态。</summary>
        public int status { get; set; }

        /// <summary>支付完成后的结果和失败企业信息。</summary>
        public LicensePaymentJobDetail pay_job_result { get; set; }
    }

    /// <summary>余额支付任务处理详情。</summary>
    public class LicensePaymentJobDetail
    {
        /// <summary>支付任务业务错误码。</summary>
        public int errcode { get; set; }

        /// <summary>支付任务业务错误说明。</summary>
        public string errmsg { get; set; }

        /// <summary>联合订单中支付失败的企业列表。</summary>
        public List<LicenseInvalidCorp> fail_corp_list { get; set; }
    }
}
