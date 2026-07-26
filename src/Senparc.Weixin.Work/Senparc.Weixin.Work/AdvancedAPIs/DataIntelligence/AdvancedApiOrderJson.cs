/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AdvancedApiOrderJson.cs
    文件功能描述：企业微信数据与智能专区订单管理协议模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐高级接口订单管理强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>高级接口版本生效区间。</summary>
    public class AdvancedApiEditionInfo
    {
        /// <summary>获取或设置版本生效时间；Unix 时间戳，必须与 <see cref="end_time"/> 同时指定。</summary>
        public long? begin_time { get; set; }

        /// <summary>获取或设置版本结束时间；Unix 时间戳，必须与 <see cref="begin_time"/> 同时指定。</summary>
        public long? end_time { get; set; }
    }

    /// <summary>创建订单时的会话内容数据接口购买信息。</summary>
    public class AdvancedApiCreateOrderChatArchive
    {
        /// <summary>获取或设置目标版本：2 表示内外部会话，3 表示内外部会话及语音通话。</summary>
        public int edition { get; set; }

        /// <summary>获取或设置升级前版本；升级订单目前仅支持值 2，缺省值为 2。</summary>
        public int? old_edition { get; set; }

        /// <summary>获取或设置购买人数；新购、增购和升级订单必须指定，范围为 1 至 1000000。</summary>
        public int? purchase_count { get; set; }

        /// <summary>获取或设置指定生效日期的 Unix 时间戳。</summary>
        public long? take_effect_time { get; set; }

        /// <summary>获取或设置待增购或升级的旧版本生效区间。</summary>
        public AdvancedApiEditionInfo old_edition_info { get; set; }

        /// <summary>获取或设置升级目标版本的生效区间。</summary>
        public AdvancedApiEditionInfo target_edition_info { get; set; }
    }

    /// <summary>创建数据与智能专区高级接口订单请求。</summary>
    public class AdvancedApiCreateOrderRequest
    {
        /// <summary>获取或设置高级接口类型；目前仅支持 1（会话内容数据接口）。</summary>
        public int advanced_api_type { get; set; } = 1;

        /// <summary>获取或设置客户企业 ID。</summary>
        public string custom_corpid { get; set; }

        /// <summary>获取或设置下单人；须为服务商企业内具有购买或管理高级接口权限的明文 UserId。</summary>
        public string buyer_userid { get; set; }

        /// <summary>获取或设置订单类型：0 新购、1 增购、2 续期、3 升级。</summary>
        public int? order_type { get; set; }

        /// <summary>获取或设置会话内容数据接口订单信息；当 <see cref="advanced_api_type"/> 为 1 时必填。</summary>
        public AdvancedApiCreateOrderChatArchive chat_archive_api { get; set; }
    }

    /// <summary>创建数据与智能专区高级接口订单结果。</summary>
    public class AdvancedApiCreateOrderResult : WorkJsonResult
    {
        /// <summary>获取或设置新创建的订单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>以订单号定位数据与智能专区高级接口订单的请求。</summary>
    public class AdvancedApiOrderIdRequest
    {
        /// <summary>获取或设置订单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>使用服务商充值账户余额支付高级接口订单请求。</summary>
    public class AdvancedApiSubmitPayRequest
    {
        /// <summary>获取或设置支付人；须为服务商企业内具有购买或管理高级接口权限的明文 UserId。</summary>
        public string payer_userid { get; set; }

        /// <summary>获取或设置待支付的订单号。</summary>
        public string order_id { get; set; }
    }

    /// <summary>分页获取数据与智能专区高级接口订单列表请求。</summary>
    public class AdvancedApiOrderListRequest
    {
        /// <summary>获取或设置客户企业 ID；不指定时查询服务商的全部客户企业。</summary>
        public string custom_corpid { get; set; }

        /// <summary>获取或设置下单时间范围起点；必须与 <see cref="end_time"/> 同时指定。</summary>
        public long? start_time { get; set; }

        /// <summary>获取或设置下单时间范围终点；查询区间为 [start_time, end_time)。</summary>
        public long? end_time { get; set; }

        /// <summary>获取或设置高级接口类型；目前仅支持 1（会话内容数据接口）。</summary>
        public int? advanced_api_type { get; set; }

        /// <summary>获取或设置分页游标；首次请求可不指定。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置分页大小；缺省值为 500，最大值为 1000。</summary>
        public int? limit { get; set; }
    }

    /// <summary>数据与智能专区高级接口订单摘要。</summary>
    public class AdvancedApiOrderSummary
    {
        /// <summary>获取或设置订单号。</summary>
        public string order_id { get; set; }

        /// <summary>获取或设置订单类型：0 新购、1 增购、2 续期、3 升级。</summary>
        public int order_type { get; set; }

        /// <summary>获取或设置订单状态：0 待支付、1 已支付、2 已取消、3 已过期、4 申请退款中、5 退款成功、6 退款被拒绝。</summary>
        public int order_status { get; set; }

        /// <summary>获取或设置订单创建时间；Unix 时间戳。</summary>
        public long create_time { get; set; }
    }

    /// <summary>分页获取数据与智能专区高级接口订单列表结果。</summary>
    public class AdvancedApiOrderListResult : WorkJsonResult
    {
        /// <summary>获取或设置订单摘要列表。</summary>
        public AdvancedApiOrderSummary[] order_list { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>获取或设置是否还有更多记录：0 表示没有，1 表示有。</summary>
        public int has_more { get; set; }
    }

    /// <summary>订单详情中的会话内容数据接口购买信息。</summary>
    public class AdvancedApiOrderChatArchive
    {
        /// <summary>获取或设置购买版本：2 表示内外部会话，3 表示内外部会话及语音通话。</summary>
        public int edition { get; set; }

        /// <summary>获取或设置购买人数。</summary>
        public int purchase_count { get; set; }

        /// <summary>获取或设置使用时长，单位为天。</summary>
        public int purchase_duration_days { get; set; }

        /// <summary>获取或设置生效时间；Unix 时间戳。</summary>
        public long take_effect_time { get; set; }

        /// <summary>获取或设置到期时间；Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置原价金额，单位为分；目前仅升级订单返回。</summary>
        public int? original_price { get; set; }
    }

    /// <summary>数据与智能专区高级接口订单详情。</summary>
    public class AdvancedApiOrder
    {
        /// <summary>获取或设置高级接口类型；目前仅支持 1（会话内容数据接口）。</summary>
        public int advanced_api_type { get; set; }

        /// <summary>获取或设置订单号。</summary>
        public string order_id { get; set; }

        /// <summary>获取或设置订单类型：0 新购、1 增购、2 续期、3 升级。</summary>
        public int order_type { get; set; }

        /// <summary>获取或设置订单状态：0 待支付、1 已支付、2 已取消、3 已过期、4 申请退款中、5 退款成功、6 退款被拒绝。</summary>
        public int order_status { get; set; }

        /// <summary>获取或设置客户企业 ID；企业微信返回加密后的 CorpId。</summary>
        public string custom_corpid { get; set; }

        /// <summary>获取或设置订单创建时间；Unix 时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>获取或设置下单人的明文 UserId。</summary>
        public string buyer_userid { get; set; }

        /// <summary>获取或设置应付金额，单位为分。</summary>
        public int paid_price { get; set; }

        /// <summary>获取或设置会话内容数据接口购买信息。</summary>
        public AdvancedApiOrderChatArchive chat_archive_api { get; set; }
    }

    /// <summary>获取数据与智能专区高级接口订单详情结果。</summary>
    public class AdvancedApiOrderDetailResult : WorkJsonResult
    {
        /// <summary>获取或设置订单详情。</summary>
        public AdvancedApiOrder order { get; set; }
    }

    /// <summary>获取客户企业已购的数据与智能专区高级接口版本请求。</summary>
    public class AdvancedApiCorpPurchaseInfoRequest
    {
        /// <summary>获取或设置高级接口类型；目前固定为 1（会话内容数据接口）。</summary>
        public int advanced_api_type { get; set; } = 1;

        /// <summary>获取或设置客户企业 ID。</summary>
        public string custom_corpid { get; set; }
    }

    /// <summary>客户企业已购的高级接口版本信息。</summary>
    public class AdvancedApiPurchasedEdition
    {
        /// <summary>获取或设置已购版本：1 历史内部会话、2 内外部会话、3 内外部会话及语音通话。</summary>
        public int edition { get; set; }

        /// <summary>获取或设置购买人数。</summary>
        public int purchase_count { get; set; }

        /// <summary>获取或设置生效时间；Unix 时间戳。</summary>
        public long begin_time { get; set; }

        /// <summary>获取或设置到期时间；Unix 时间戳。</summary>
        public long end_time { get; set; }
    }

    /// <summary>客户企业已购的会话内容数据接口信息。</summary>
    public class AdvancedApiChatArchivePurchaseInfo
    {
        /// <summary>获取或设置已购版本列表。</summary>
        public AdvancedApiPurchasedEdition[] edition_list { get; set; }
    }

    /// <summary>获取客户企业已购的数据与智能专区高级接口版本结果。</summary>
    public class AdvancedApiCorpPurchaseInfoResult : WorkJsonResult
    {
        /// <summary>获取或设置会话内容数据接口购买信息。</summary>
        public AdvancedApiChatArchivePurchaseInfo chat_archive_api_buy_info { get; set; }
    }
}
