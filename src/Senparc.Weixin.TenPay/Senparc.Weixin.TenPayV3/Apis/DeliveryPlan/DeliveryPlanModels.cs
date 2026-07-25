#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DeliveryPlanModels.cs
    文件功能描述：微信支付 V3 服务商摇一摇有优惠投放计划强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐投放计划请求、返回及状态变更通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.DeliveryPlan
{
    /// <summary>
    /// 创建摇一摇有优惠投放计划的请求数据。
    /// </summary>
    public class DeliveryPlanCreateRequestData
    {
        /// <summary>
        /// 服务商自定义请求单号，长度为 6 至 40 个字符，仅支持数字、字母、下划线和短横线。
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 创建投放计划的品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 品牌商品券 ID。
        /// </summary>
        public string product_coupon_id { get; set; }

        /// <summary>
        /// 营销批次 ID；单券模式（SINGLE）时必填。
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 是否复用商品券已有的领券规则。
        /// </summary>
        public bool reuse_coupon_config { get; set; }

        /// <summary>
        /// 投放计划名称；不复用商品券配置时必填，最长 36 个字符。
        /// </summary>
        public string plan_name { get; set; }

        /// <summary>
        /// 投放计划总数量；不复用商品券配置时必填。
        /// </summary>
        public long? total_count { get; set; }

        /// <summary>
        /// 单个用户可领取的总数量上限。
        /// </summary>
        public long? user_limit { get; set; }

        /// <summary>
        /// 单个用户每日可领取的数量上限。
        /// </summary>
        public long? daily_limit { get; set; }

        /// <summary>
        /// 投放开始时间，遵循 RFC 3339 格式。
        /// </summary>
        public string delivery_start_time { get; set; }

        /// <summary>
        /// 投放结束时间，遵循 RFC 3339 格式。
        /// </summary>
        public string delivery_end_time { get; set; }

        /// <summary>
        /// 推荐文案，最长 27 个字符。
        /// </summary>
        public string recommend_word { get; set; }

        /// <summary>
        /// 使用模式：SINGLE（单券）或 PROGRESSIVE_BUNDLE（多次优惠）。
        /// </summary>
        public string usage_mode { get; set; }

        /// <summary>
        /// 批次组 ID；多次优惠模式（PROGRESSIVE_BUNDLE）时必填。
        /// </summary>
        public string stock_bundle_id { get; set; }
    }

    /// <summary>
    /// 投放计划列表的分页和筛选条件。
    /// </summary>
    public class DeliveryPlanQueryRequestData
    {
        /// <summary>
        /// 单页条数，默认 10，最大 50。
        /// </summary>
        public int? page_size { get; set; }

        /// <summary>
        /// 分页偏移量，从 0 开始。
        /// </summary>
        public int? offset { get; set; }

        /// <summary>
        /// 计划状态筛选条件：CREATED、TERMINATED、EXPIRED、DELIVERING 或 PAUSED。
        /// </summary>
        public string plan_state { get; set; }

        /// <summary>
        /// 审核状态筛选条件：AUDIT_INITIAL、AUDIT_PROCESSING、AUDIT_PASSED 或 AUDIT_REJECTED。
        /// </summary>
        public string audit_state { get; set; }

        /// <summary>
        /// 微信支付生成的投放计划 ID。
        /// </summary>
        public string plan_id { get; set; }
    }

    /// <summary>
    /// 更新投放计划的请求数据。
    /// </summary>
    public class DeliveryPlanUpdateRequestData
    {
        /// <summary>
        /// 需要更新的投放计划内容。
        /// </summary>
        public DeliveryPlanModifyContent modify_content { get; set; }

        /// <summary>
        /// 服务商自定义修改请求单号，长度为 6 至 40 个字符。
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 投放计划可修改的内容。
    /// </summary>
    public class DeliveryPlanModifyContent
    {
        /// <summary>
        /// 新的投放计划名称，最长 36 个字符。
        /// </summary>
        public string plan_name { get; set; }

        /// <summary>
        /// 新的投放结束时间，遵循 RFC 3339 格式且只能延长。
        /// </summary>
        public string delivery_end_time { get; set; }

        /// <summary>
        /// 新的投放总数量，只能增加。
        /// </summary>
        public long? total_count { get; set; }

        /// <summary>
        /// 新的单用户总领取上限，只能增加。
        /// </summary>
        public long? user_limit { get; set; }

        /// <summary>
        /// 新的单用户每日领取上限，只能增加。
        /// </summary>
        public long? daily_limit { get; set; }

        /// <summary>
        /// 新的推荐文案，最长 27 个字符。
        /// </summary>
        public string recommend_word { get; set; }
    }

    /// <summary>
    /// 设置投放计划状态变更通知地址的请求数据。
    /// </summary>
    public class DeliveryPlanNotifyUrlRequestData
    {
        /// <summary>
        /// 接收投放计划状态变更通知的 HTTPS 地址，最长 256 个字符。
        /// </summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 单个投放计划接口的返回结果。
    /// </summary>
    public class DeliveryPlanResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 投放计划详情。
        /// </summary>
        public DeliveryPlanInfo plan { get; set; }
    }

    /// <summary>
    /// 投放计划列表接口的返回结果。
    /// </summary>
    public class DeliveryPlanListResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 符合筛选条件的投放计划总数。
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 当前分页返回的投放计划列表；没有数据时可能为空。
        /// </summary>
        public DeliveryPlanInfo[] plan_list { get; set; }
    }

    /// <summary>
    /// 摇一摇有优惠投放计划详情。
    /// </summary>
    public class DeliveryPlanInfo
    {
        /// <summary>
        /// 微信支付生成的投放计划 ID。
        /// </summary>
        public string plan_id { get; set; }

        /// <summary>
        /// 投放计划名称。
        /// </summary>
        public string plan_name { get; set; }

        /// <summary>
        /// 计划状态：CREATED、TERMINATED、EXPIRED、DELIVERING 或 PAUSED。
        /// </summary>
        public string plan_state { get; set; }

        /// <summary>
        /// 投放开始时间，采用 RFC 3339 格式。
        /// </summary>
        public string delivery_start_time { get; set; }

        /// <summary>
        /// 投放结束时间，采用 RFC 3339 格式。
        /// </summary>
        public string delivery_end_time { get; set; }

        /// <summary>
        /// 品牌商品券 ID。
        /// </summary>
        public string product_coupon_id { get; set; }

        /// <summary>
        /// 使用模式：SINGLE 或 PROGRESSIVE_BUNDLE。
        /// </summary>
        public string usage_mode { get; set; }

        /// <summary>
        /// 单券模式使用的营销批次 ID。
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 多次优惠模式使用的批次组 ID。
        /// </summary>
        public string stock_bundle_id { get; set; }

        /// <summary>
        /// 推荐文案。
        /// </summary>
        public string recommend_word { get; set; }

        /// <summary>
        /// 创建投放计划的品牌 ID。
        /// </summary>
        public string brand_id { get; set; }

        /// <summary>
        /// 投放计划总数量。
        /// </summary>
        public long total_count { get; set; }

        /// <summary>
        /// 单个用户可领取的总数量上限。
        /// </summary>
        public long user_limit { get; set; }

        /// <summary>
        /// 单个用户每日可领取的数量上限。
        /// </summary>
        public long daily_limit { get; set; }

        /// <summary>
        /// 是否复用商品券已有的领券规则。
        /// </summary>
        public bool reuse_coupon_config { get; set; }
    }

    /// <summary>
    /// 设置投放计划状态变更通知地址的返回结果。
    /// </summary>
    public class DeliveryPlanNotifyUrlResultJson : ReturnJsonBase
    {
        /// <summary>
        /// 已生效的 HTTPS 通知地址。
        /// </summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 投放计划状态变更通知解密后的资源数据。
    /// </summary>
    public class DeliveryPlanNotifyJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信支付生成的投放计划 ID。
        /// </summary>
        public string plan_id { get; set; }

        /// <summary>
        /// 计划状态：CREATED、DELIVERING、PAUSED、TERMINATED 或 EXPIRED。
        /// </summary>
        public string plan_state { get; set; }

        /// <summary>
        /// 审核状态：PROCESSING、PASSED 或 REJECTED。
        /// </summary>
        public string audit_state { get; set; }

        /// <summary>
        /// 本次状态变化的原因。
        /// </summary>
        public string change_reason { get; set; }

        /// <summary>
        /// 状态修改时间，采用 RFC 3339 格式。
        /// </summary>
        public string modify_time { get; set; }
    }

    /// <summary>
    /// 投放计划状态变更通知的官方事件常量。
    /// </summary>
    public static class DeliveryPlanNotifyConstants
    {
        /// <summary>
        /// 通知事件类型。
        /// </summary>
        public const string EventType = "DELIVERY_PLAN.CHANGE";

        /// <summary>
        /// 加密资源的原始类型。
        /// </summary>
        public const string OriginalType = "delivery_plan";
    }
}
