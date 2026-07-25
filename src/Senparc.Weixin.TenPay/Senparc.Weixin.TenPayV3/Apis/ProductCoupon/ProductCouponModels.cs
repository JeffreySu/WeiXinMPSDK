#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCouponModels.cs
    文件功能描述：微信支付 V3 服务商商品券请求与返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐商品券单券模式强类型模型

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 统一商品券兼容命名并复用多次优惠共享模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.ProductCoupon
{
    /// <summary>
    /// 创建单券模式商品券请求。
    /// </summary>
    public class ProductCouponCreateRequestData
    {
        /// <summary>品牌侧唯一的创建请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>优惠范围：ALL 全场券，SINGLE 部分商品券。</summary>
        public string scope { get; set; }

        /// <summary>优惠类型：NORMAL 满减、DISCOUNT 折扣、EXCHANGE 兑换。</summary>
        public string type { get; set; }

        /// <summary>使用模式；单券模式固定为 SINGLE。</summary>
        public string usage_mode { get; set; }

        /// <summary>全场单券的固定优惠规则。</summary>
        public ProductCouponSingleUsageInfo single_usage_info { get; set; }

        /// <summary>多次优惠模式的次数与间隔配置。</summary>
        public ProductCouponProgressiveBundleUsageInfo progressive_bundle_usage_info { get; set; }

        /// <summary>商品券卡面和商品展示信息。</summary>
        public ProductCouponDisplayInfo display_info { get; set; }

        /// <summary>品牌自定义商品编号。</summary>
        public string out_product_no { get; set; }

        /// <summary>随商品券一并创建的首个批次。</summary>
        public ProductCouponStockRequestData stock { get; set; }

        /// <summary>随多次优惠商品券一并创建的首个批次组。</summary>
        public ProductCouponStockBundleCreateInfo stock_bundle { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 修改商品券请求。
    /// </summary>
    public class ProductCouponUpdateRequestData
    {
        /// <summary>品牌侧唯一的修改请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>修改后的商品券展示信息。</summary>
        public ProductCouponDisplayInfo display_info { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 商品券或批次失效请求。
    /// </summary>
    public class ProductCouponDeactivateRequestData
    {
        /// <summary>品牌侧唯一的失效请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 全场单券固定优惠配置。
    /// </summary>
    public class ProductCouponSingleUsageInfo
    {
        /// <summary>满减券固定优惠规则。</summary>
        public ProductCouponNormalRule normal_coupon { get; set; }

        /// <summary>折扣券固定优惠规则。</summary>
        public ProductCouponDiscountRule discount_coupon { get; set; }
    }

    /// <summary>
    /// 满减券优惠规则。
    /// </summary>
    public class ProductCouponNormalRule
    {
        /// <summary>使用门槛金额，单位为分。</summary>
        public long threshold { get; set; }

        /// <summary>固定减免金额，单位为分。</summary>
        public long discount_amount { get; set; }
    }

    /// <summary>
    /// 折扣券优惠规则。
    /// </summary>
    public class ProductCouponDiscountRule
    {
        /// <summary>使用门槛金额，单位为分。</summary>
        public long threshold { get; set; }

        /// <summary>减免百分比，例如 30 表示减免 30%。</summary>
        public long percent_off { get; set; }
    }

    /// <summary>
    /// 兑换券优惠规则。
    /// </summary>
    public class ProductCouponExchangeRule
    {
        /// <summary>使用门槛金额，单位为分。</summary>
        public long threshold { get; set; }

        /// <summary>换购价，单位为分。</summary>
        public long exchange_price { get; set; }
    }

    /// <summary>
    /// 商品券卡面和商品展示信息。
    /// </summary>
    public class ProductCouponDisplayInfo
    {
        /// <summary>商品券名称。</summary>
        public string name { get; set; }

        /// <summary>通过商品券图片上传接口获得的主图 URL。</summary>
        public string image_url { get; set; }

        /// <summary>通过商品券图片上传接口获得的背景图 URL。</summary>
        public string background_url { get; set; }

        /// <summary>商品券详情图片 URL 列表。</summary>
        public string[] detail_image_url_list { get; set; }

        /// <summary>商品原价，单位为分。</summary>
        public long? original_price { get; set; }

        /// <summary>组合套餐列表。</summary>
        public ProductCouponComboPackage[] combo_package_list { get; set; }
    }

    /// <summary>
    /// 商品券组合套餐。
    /// </summary>
    public class ProductCouponComboPackage
    {
        /// <summary>组合套餐名称。</summary>
        public string name { get; set; }

        /// <summary>用户需要选择的商品数量。</summary>
        public long pick_count { get; set; }

        /// <summary>可选商品列表。</summary>
        public ProductCouponComboChoice[] choice_list { get; set; }
    }

    /// <summary>
    /// 组合套餐可选商品。
    /// </summary>
    public class ProductCouponComboChoice
    {
        /// <summary>商品名称。</summary>
        public string name { get; set; }

        /// <summary>商品价格，单位为分。</summary>
        public long price { get; set; }

        /// <summary>商品数量。</summary>
        public long count { get; set; }

        /// <summary>商品图片 URL。</summary>
        public string image_url { get; set; }

        /// <summary>商品详情小程序 AppID。</summary>
        public string mini_program_appid { get; set; }

        /// <summary>商品详情小程序路径。</summary>
        public string mini_program_path { get; set; }
    }

    /// <summary>
    /// 组合套餐可选商品的兼容命名。
    /// </summary>
    public class ProductCouponChoice : ProductCouponComboChoice
    {
    }

    /// <summary>
    /// 创建商品券批次请求。
    /// </summary>
    public class ProductCouponStockCreateRequestData
    {
        /// <summary>品牌侧唯一的创建请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>商品券批次配置。</summary>
        public ProductCouponStockRequestData stock { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 商品券批次创建配置。
    /// </summary>
    public class ProductCouponStockRequestData
    {
        /// <summary>批次备注。</summary>
        public string remark { get; set; }

        /// <summary>券 Code 模式，例如 UPLOAD 或 WECHATPAY。</summary>
        public string coupon_code_mode { get; set; }

        /// <summary>批次发放数量规则。</summary>
        public ProductCouponStockSendRule stock_send_rule { get; set; }

        /// <summary>单券核销有效期和优惠规则。</summary>
        public ProductCouponSingleUsageRule single_usage_rule { get; set; }

        /// <summary>卡包中展示的核销规则。</summary>
        public ProductCouponUsageRuleDisplayInfo usage_rule_display_info { get; set; }

        /// <summary>卡包中的券展示配置。</summary>
        public ProductCouponCouponDisplayInfo coupon_display_info { get; set; }

        /// <summary>商品券通知 AppID 配置。</summary>
        public ProductCouponStockNotifyConfig notify_config { get; set; }

        /// <summary>门店范围：NONE、ALL 或 SPECIFIC。</summary>
        public string store_scope { get; set; }
    }

    /// <summary>
    /// 商品券批次创建配置的兼容命名。
    /// </summary>
    public class ProductCouponStockCreateInfo : ProductCouponStockRequestData
    {
    }

    /// <summary>
    /// 商品券批次发放规则。
    /// </summary>
    public class ProductCouponStockSendRule
    {
        /// <summary>批次最大可发放数量。</summary>
        public long max_count { get; set; }

        /// <summary>批次每天最大可发放数量。</summary>
        public long? max_count_per_day { get; set; }

        /// <summary>每个用户最多可领取数量。</summary>
        public long max_count_per_user { get; set; }
    }

    /// <summary>
    /// 单券批次核销规则。
    /// </summary>
    public class ProductCouponSingleUsageRule
    {
        /// <summary>用户商品券可核销时间配置。</summary>
        public ProductCouponAvailablePeriod coupon_available_period { get; set; }

        /// <summary>满减券批次优惠规则。</summary>
        public ProductCouponNormalRule normal_coupon { get; set; }

        /// <summary>折扣券批次优惠规则。</summary>
        public ProductCouponDiscountRule discount_coupon { get; set; }

        /// <summary>兑换券批次优惠规则。</summary>
        public ProductCouponExchangeRule exchange_coupon { get; set; }
    }

    /// <summary>
    /// 用户商品券有效期和可用时段。
    /// </summary>
    public class ProductCouponAvailablePeriod
    {
        /// <summary>固定有效期开始时间，RFC 3339 格式。</summary>
        public string available_begin_time { get; set; }

        /// <summary>固定有效期结束时间，RFC 3339 格式。</summary>
        public string available_end_time { get; set; }

        /// <summary>领取后有效天数。</summary>
        public long? available_days { get; set; }

        /// <summary>领取后等待生效天数。</summary>
        public long? wait_days_after_receive { get; set; }

        /// <summary>每周可用日期和时段。</summary>
        public ProductCouponWeeklyAvailablePeriod weekly_available_period { get; set; }

        /// <summary>不规则可用日期列表。</summary>
        public ProductCouponIrregularAvailablePeriod[] irregular_available_period_list { get; set; }

        /// <summary>领取后按秒计算的有效时长。</summary>
        public long? available_seconds { get; set; }
    }

    /// <summary>
    /// 每周可用日期和时段。
    /// </summary>
    public class ProductCouponWeeklyAvailablePeriod
    {
        /// <summary>可用星期列表，例如 MONDAY。</summary>
        public string[] day_list { get; set; }

        /// <summary>每天可用时段列表。</summary>
        public ProductCouponDayPeriod[] day_period_list { get; set; }
    }

    /// <summary>
    /// 单日可用时段。
    /// </summary>
    public class ProductCouponDayPeriod
    {
        /// <summary>开始时间，按官方定义使用当天秒数。</summary>
        public long begin_time { get; set; }

        /// <summary>结束时间，按官方定义使用当天秒数。</summary>
        public long end_time { get; set; }
    }

    /// <summary>
    /// 不规则可用日期区间。
    /// </summary>
    public class ProductCouponIrregularAvailablePeriod
    {
        /// <summary>区间开始时间，RFC 3339 格式。</summary>
        public string begin_time { get; set; }

        /// <summary>区间结束时间，RFC 3339 格式。</summary>
        public string end_time { get; set; }
    }

    /// <summary>
    /// 商品券核销规则展示信息。
    /// </summary>
    public class ProductCouponUsageRuleDisplayInfo
    {
        /// <summary>支持的核销方式列表。</summary>
        public string[] coupon_usage_method_list { get; set; }

        /// <summary>核销小程序 AppID。</summary>
        public string mini_program_appid { get; set; }

        /// <summary>核销小程序路径。</summary>
        public string mini_program_path { get; set; }

        /// <summary>App 跳转路径。</summary>
        public string app_path { get; set; }

        /// <summary>核销规则说明。</summary>
        public string usage_description { get; set; }

        /// <summary>可用门店展示和跳转信息。</summary>
        public ProductCouponAvailableStoreInfo coupon_available_store_info { get; set; }
    }

    /// <summary>
    /// 商品券可用门店展示信息。
    /// </summary>
    public class ProductCouponAvailableStoreInfo
    {
        /// <summary>可用门店说明。</summary>
        public string description { get; set; }

        /// <summary>门店列表小程序 AppID。</summary>
        public string mini_program_appid { get; set; }

        /// <summary>门店列表小程序路径。</summary>
        public string mini_program_path { get; set; }

        /// <summary>App 跳转类型。</summary>
        public string app_jump_type { get; set; }

        /// <summary>核销码跳转链接。</summary>
        public string passcode_link { get; set; }
    }

    /// <summary>
    /// 用户卡包中的商品券展示配置。
    /// </summary>
    public class ProductCouponCouponDisplayInfo
    {
        /// <summary>券 Code 展示方式。</summary>
        public string code_display_mode { get; set; }

        /// <summary>卡包背景颜色。</summary>
        public string background_color { get; set; }

        /// <summary>小程序入口。</summary>
        public ProductCouponMiniProgramEntrance entrance_mini_program { get; set; }

        /// <summary>公众号入口。</summary>
        public ProductCouponOfficialAccountEntrance entrance_official_account { get; set; }

        /// <summary>视频号入口。</summary>
        public ProductCouponFinderEntrance entrance_finder { get; set; }
    }

    /// <summary>
    /// 商品券小程序入口。
    /// </summary>
    public class ProductCouponMiniProgramEntrance
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>小程序路径。</summary>
        public string path { get; set; }

        /// <summary>入口文案。</summary>
        public string entrance_wording { get; set; }

        /// <summary>引导文案。</summary>
        public string guidance_wording { get; set; }
    }

    /// <summary>
    /// 商品券公众号入口。
    /// </summary>
    public class ProductCouponOfficialAccountEntrance
    {
        /// <summary>公众号 AppID。</summary>
        public string appid { get; set; }
    }

    /// <summary>
    /// 商品券视频号入口。
    /// </summary>
    public class ProductCouponFinderEntrance
    {
        /// <summary>视频号 ID。</summary>
        public string finder_id { get; set; }

        /// <summary>视频号视频 ID。</summary>
        public string finder_video_id { get; set; }

        /// <summary>视频封面图片 URL。</summary>
        public string finder_video_cover_image_url { get; set; }
    }

    /// <summary>
    /// 商品券批次通知配置。
    /// </summary>
    public class ProductCouponStockNotifyConfig
    {
        /// <summary>计算通知用户 OpenId 使用的 AppID。</summary>
        public string notify_appid { get; set; }
    }

    /// <summary>
    /// 商品券批次列表查询条件。
    /// </summary>
    public class ProductCouponStockListQueryRequestData
    {
        /// <summary>批次状态筛选。</summary>
        public string state { get; set; }

        /// <summary>每页数量。</summary>
        public int? page_size { get; set; }

        /// <summary>下一页标记。</summary>
        public string page_token { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>多次优惠模式的批次组 ID。</summary>
        public string stock_bundle_id { get; set; }
    }

    /// <summary>
    /// 修改商品券批次请求。
    /// </summary>
    public class ProductCouponStockUpdateRequestData
    {
        /// <summary>品牌侧唯一的修改请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>批次备注。</summary>
        public string remark { get; set; }

        /// <summary>卡包中展示的核销规则。</summary>
        public ProductCouponUsageRuleDisplayInfo usage_rule_display_info { get; set; }

        /// <summary>卡包中的券展示配置。</summary>
        public ProductCouponCouponDisplayInfo coupon_display_info { get; set; }

        /// <summary>商品券通知 AppID 配置。</summary>
        public ProductCouponStockNotifyConfig notify_config { get; set; }

        /// <summary>门店范围：NONE、ALL 或 SPECIFIC。</summary>
        public string store_scope { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 修改商品券批次发放预算请求。
    /// </summary>
    public class ProductCouponStockBudgetRequestData
    {
        /// <summary>品牌侧唯一的预算修改请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>预算更新模式。</summary>
        public string update_mode { get; set; }

        /// <summary>更新前批次最大发放数量。</summary>
        public long? current_max_count { get; set; }

        /// <summary>更新后批次最大发放数量。</summary>
        public long? target_max_count { get; set; }

        /// <summary>更新前每天最大发放数量。</summary>
        public long? current_max_count_per_day { get; set; }

        /// <summary>更新后每天最大发放数量。</summary>
        public long? target_max_count_per_day { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 批次关联或取消关联门店请求。
    /// </summary>
    public class ProductCouponStoreOperationRequestData
    {
        /// <summary>门店列表。</summary>
        public ProductCouponStoreInfo[] store_list { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 商品券门店信息。
    /// </summary>
    public class ProductCouponStoreInfo
    {
        /// <summary>微信支付品牌门店 ID。</summary>
        public string store_id { get; set; }
    }

    /// <summary>
    /// 已关联门店分页查询条件。
    /// </summary>
    public class ProductCouponStoreListQueryRequestData
    {
        /// <summary>每页数量。</summary>
        public int? page_size { get; set; }

        /// <summary>下一页标记。</summary>
        public string page_token { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 预上传券 Code 请求。
    /// </summary>
    public class ProductCouponCodeUploadRequestData
    {
        /// <summary>品牌侧唯一的上传请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>待上传的券 Code 列表。</summary>
        public string[] code_list { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 商品券用户操作公共请求字段。
    /// </summary>
    public class ProductCouponUserOperationRequestData
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }

        /// <summary>商品券批次 ID。</summary>
        public string stock_id { get; set; }

        /// <summary>用于计算用户 OpenId 的 AppID。</summary>
        public string appid { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 向用户发放商品券请求。
    /// </summary>
    public class ProductCouponSendRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>上传模式批次指定的券 Code。</summary>
        public string coupon_code { get; set; }

        /// <summary>品牌侧唯一的发券请求单号。</summary>
        public string send_request_no { get; set; }

        /// <summary>随用户商品券透传的附加数据。</summary>
        public string attach { get; set; }

        /// <summary>商品券标签信息。</summary>
        public ProductCouponTagInfo coupon_tag_info { get; set; }

        /// <summary>会员标签信息。</summary>
        public ProductCouponMemberTagInfo member_tag_info { get; set; }
    }

    /// <summary>
    /// 商品券标签信息。
    /// </summary>
    public class ProductCouponTagInfo
    {
        /// <summary>商品券标签列表。</summary>
        public string[] coupon_tag_list { get; set; }
    }

    /// <summary>
    /// 商品券会员标签信息。
    /// </summary>
    public class ProductCouponMemberTagInfo
    {
        /// <summary>用户会员卡 ID。</summary>
        public string member_card_id { get; set; }
    }

    /// <summary>
    /// 确认发放用户商品券请求。
    /// </summary>
    public class ProductCouponConfirmRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>品牌侧唯一的确认请求单号。</summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 核销用户商品券请求。
    /// </summary>
    public class ProductCouponUseRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>实际核销时间，RFC 3339 格式。</summary>
        public string use_time { get; set; }

        /// <summary>品牌侧唯一的核销请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>实际核销门店 ID。</summary>
        public string store_id { get; set; }

        /// <summary>关联微信支付订单。</summary>
        public ProductCouponAssociatedOrderInfo associated_order_info { get; set; }

        /// <summary>关联微信支付分订单。</summary>
        public ProductCouponAssociatedPayScoreOrderInfo associated_pay_score_order_info { get; set; }

        /// <summary>本次核销优惠金额，单位为分。</summary>
        public long? saved_amount { get; set; }
    }

    /// <summary>
    /// 关联微信支付订单信息。
    /// </summary>
    public class ProductCouponAssociatedOrderInfo
    {
        /// <summary>微信支付订单号。</summary>
        public string transaction_id { get; set; }

        /// <summary>商户订单号。</summary>
        public string out_trade_no { get; set; }

        /// <summary>收款商户号。</summary>
        public string mchid { get; set; }

        /// <summary>收款子商户号。</summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 关联微信支付分订单信息。
    /// </summary>
    public class ProductCouponAssociatedPayScoreOrderInfo
    {
        /// <summary>微信支付分订单号。</summary>
        public string order_id { get; set; }

        /// <summary>商户支付分订单号。</summary>
        public string out_order_no { get; set; }
    }

    /// <summary>
    /// 查询用户商品券详情的查询参数。
    /// </summary>
    public class ProductCouponUserQueryRequestData : ProductCouponUserOperationRequestData
    {
    }

    /// <summary>
    /// 查询用户商品券列表的查询参数。
    /// </summary>
    public class ProductCouponUserListQueryRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>用户商品券状态筛选。</summary>
        public string coupon_state { get; set; }

        /// <summary>多次优惠模式的用户券包 ID。</summary>
        public string user_coupon_bundle_id { get; set; }

        /// <summary>每页数量。</summary>
        public int? page_size { get; set; }

        /// <summary>下一页标记。</summary>
        public string page_token { get; set; }
    }

    /// <summary>
    /// 失效用户商品券请求。
    /// </summary>
    public class ProductCouponUserDeactivateRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>品牌侧唯一的失效请求单号。</summary>
        public string out_request_no { get; set; }

        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }
    }

    /// <summary>
    /// 退回用户商品券请求。
    /// </summary>
    public class ProductCouponReturnRequestData : ProductCouponUserOperationRequestData
    {
        /// <summary>品牌侧唯一的退券请求单号。</summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 设置商品券事件通知地址请求。
    /// </summary>
    public class ProductCouponNotifyConfigRequestData
    {
        /// <summary>接收商品券事件的 HTTPS 通知地址。</summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 提交商品券图片生成任务请求。
    /// </summary>
    public class ProductCouponImageGenerationRequestData
    {
        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>服务商生成的唯一任务 ID。</summary>
        public string task_id { get; set; }

        /// <summary>图片生成类型，例如 COMBINE_IMAGE 或 CUT_OUT。</summary>
        public string image_generation_type { get; set; }

        /// <summary>商品券头图合成配置。</summary>
        public ProductCouponCombineImageRequestData combine_image { get; set; }

        /// <summary>商品图片抠图配置。</summary>
        public ProductCouponCutOutRequestData cut_out { get; set; }
    }

    /// <summary>
    /// 商品券头图合成配置。
    /// </summary>
    public class ProductCouponCombineImageRequestData
    {
        /// <summary>优惠范围。</summary>
        public string scope { get; set; }

        /// <summary>优惠类型。</summary>
        public string type { get; set; }

        /// <summary>使用模式。</summary>
        public string usage_mode { get; set; }

        /// <summary>满减券规则。</summary>
        public ProductCouponNormalRule normal_coupon { get; set; }

        /// <summary>折扣券规则。</summary>
        public ProductCouponDiscountRule discount_coupon { get; set; }

        /// <summary>兑换券规则。</summary>
        public ProductCouponExchangeRule exchange_coupon { get; set; }

        /// <summary>生成图片的背景颜色。</summary>
        public string background_color { get; set; }
    }

    /// <summary>
    /// 商品图片抠图配置。
    /// </summary>
    public class ProductCouponCutOutRequestData
    {
        /// <summary>待处理的商品图片 URL。</summary>
        public string image_url { get; set; }
    }

    /// <summary>
    /// 商品券详情返回结果。
    /// </summary>
    public class ProductCouponResultJson : ReturnJsonBase
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }

        /// <summary>优惠范围。</summary>
        public string scope { get; set; }

        /// <summary>优惠类型。</summary>
        public string type { get; set; }

        /// <summary>使用模式。</summary>
        public string usage_mode { get; set; }

        /// <summary>单券固定优惠规则。</summary>
        public ProductCouponSingleUsageInfo single_usage_info { get; set; }

        /// <summary>多次优惠模式配置。</summary>
        public ProductCouponProgressiveBundleUsageInfo progressive_bundle_usage_info { get; set; }

        /// <summary>商品券展示信息。</summary>
        public ProductCouponDisplayInfo display_info { get; set; }

        /// <summary>品牌自定义商品编号。</summary>
        public string out_product_no { get; set; }

        /// <summary>商品券状态。</summary>
        public string state { get; set; }

        /// <summary>失效请求单号。</summary>
        public string deactivate_request_no { get; set; }

        /// <summary>失效时间。</summary>
        public string deactivate_time { get; set; }

        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>创建商品券时一并创建的首批次。</summary>
        public ProductCouponStockResultJson stock { get; set; }

        /// <summary>创建多次优惠商品券时一并创建的首个批次组。</summary>
        public ProductCouponStockBundleResultJson stock_bundle { get; set; }
    }

    /// <summary>
    /// 多次优惠模式配置；用于兼容查询结果并供后续多次优惠接口复用。
    /// </summary>
    public class ProductCouponProgressiveBundleUsageInfo
    {
        /// <summary>券包包含的优惠次数，官方范围为 3 至 15 次。</summary>
        public int count { get; set; }

        /// <summary>相邻优惠可使用的间隔天数。</summary>
        public int? interval_days { get; set; }
    }

    /// <summary>
    /// 商品券批次返回结果。
    /// </summary>
    public class ProductCouponStockResultJson : ReturnJsonBase
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }

        /// <summary>商品券批次 ID。</summary>
        public string stock_id { get; set; }

        /// <summary>批次备注。</summary>
        public string remark { get; set; }

        /// <summary>券 Code 模式。</summary>
        public string coupon_code_mode { get; set; }

        /// <summary>券 Code 数量统计。</summary>
        public ProductCouponCodeCountInfo coupon_code_count_info { get; set; }

        /// <summary>批次发放数量规则。</summary>
        public ProductCouponStockSendRule stock_send_rule { get; set; }

        /// <summary>单券核销规则。</summary>
        public ProductCouponSingleUsageRule single_usage_rule { get; set; }

        /// <summary>多次优惠批次规则。</summary>
        public ProductCouponProgressiveBundleUsageRule progressive_bundle_usage_rule { get; set; }

        /// <summary>多次优惠批次组信息。</summary>
        public ProductCouponStockBundleInfo stock_bundle_info { get; set; }

        /// <summary>卡包中展示的核销规则。</summary>
        public ProductCouponUsageRuleDisplayInfo usage_rule_display_info { get; set; }

        /// <summary>卡包中的券展示配置。</summary>
        public ProductCouponCouponDisplayInfo coupon_display_info { get; set; }

        /// <summary>商品券通知 AppID 配置。</summary>
        public ProductCouponStockNotifyConfig notify_config { get; set; }

        /// <summary>门店范围。</summary>
        public string store_scope { get; set; }

        /// <summary>已发放次数。</summary>
        public ProductCouponSentCountInfo sent_count_info { get; set; }

        /// <summary>批次状态。</summary>
        public string state { get; set; }

        /// <summary>失效请求单号。</summary>
        public string deactivate_request_no { get; set; }

        /// <summary>失效时间。</summary>
        public string deactivate_time { get; set; }

        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }

        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>
    /// 券 Code 数量统计。
    /// </summary>
    public class ProductCouponCodeCountInfo
    {
        /// <summary>券 Code 总数。</summary>
        public long total_count { get; set; }

        /// <summary>当前可用券 Code 数。</summary>
        public long available_count { get; set; }
    }

    /// <summary>
    /// 商品券批次已发放次数。
    /// </summary>
    public class ProductCouponSentCountInfo
    {
        /// <summary>生命周期内已发放总次数。</summary>
        public long total_count { get; set; }

        /// <summary>当天已发放次数。</summary>
        public long today_count { get; set; }
    }

    /// <summary>
    /// 多次优惠批次规则。
    /// </summary>
    public class ProductCouponProgressiveBundleUsageRule
    {
        /// <summary>各轮次共同使用的可核销时间。</summary>
        public ProductCouponAvailablePeriod coupon_available_period { get; set; }

        /// <summary>该批次对应的满减优惠规则。</summary>
        public ProductCouponNormalRule normal_coupon { get; set; }

        /// <summary>该批次对应的优惠规则。</summary>
        public ProductCouponDiscountRule discount_coupon { get; set; }

        /// <summary>该批次对应的兑换优惠规则。</summary>
        public ProductCouponExchangeRule exchange_coupon { get; set; }

        /// <summary>批次组内各轮次的满减优惠规则。</summary>
        public ProductCouponNormalRule[] normal_coupon_list { get; set; }

        /// <summary>批次组内各轮次的折扣优惠规则。</summary>
        public ProductCouponDiscountRule[] discount_coupon_list { get; set; }

        /// <summary>批次组内各轮次的兑换优惠规则。</summary>
        public ProductCouponExchangeRule[] exchange_coupon_list { get; set; }
    }

    /// <summary>
    /// 多次优惠批次组信息。
    /// </summary>
    public class ProductCouponStockBundleInfo
    {
        /// <summary>批次组 ID。</summary>
        public string stock_bundle_id { get; set; }

        /// <summary>当前批次在批次组中的序号。</summary>
        public long stock_bundle_index { get; set; }
    }

    /// <summary>
    /// 商品券批次列表返回结果。
    /// </summary>
    public class ProductCouponStockListResultJson : ReturnJsonBase
    {
        /// <summary>符合条件的批次总数。</summary>
        public long total_count { get; set; }

        /// <summary>商品券批次列表。</summary>
        public ProductCouponStockResultJson[] stock_list { get; set; }

        /// <summary>下一页标记。</summary>
        public string next_page_token { get; set; }
    }

    /// <summary>
    /// 门店关联或取消关联结果。
    /// </summary>
    public class ProductCouponStoreOperationResultJson : ReturnJsonBase
    {
        /// <summary>本次处理的门店总数。</summary>
        public long total_count { get; set; }

        /// <summary>处理成功的门店列表。</summary>
        public ProductCouponStoreInfo[] success_store_list { get; set; }

        /// <summary>处理失败的门店列表。</summary>
        public ProductCouponFailedStoreInfo[] failed_store_list { get; set; }
    }

    /// <summary>
    /// 处理失败的商品券门店。
    /// </summary>
    public class ProductCouponFailedStoreInfo : ProductCouponStoreInfo
    {
        /// <summary>失败错误码。</summary>
        public string code { get; set; }

        /// <summary>失败原因。</summary>
        public string message { get; set; }
    }

    /// <summary>
    /// 已关联门店列表返回结果。
    /// </summary>
    public class ProductCouponStoreListResultJson : ReturnJsonBase
    {
        /// <summary>已关联门店总数。</summary>
        public long total_count { get; set; }

        /// <summary>已关联门店列表。</summary>
        public ProductCouponStoreInfo[] store_list { get; set; }

        /// <summary>下一页标记。</summary>
        public string next_page_token { get; set; }
    }

    /// <summary>
    /// 预上传券 Code 结果。
    /// </summary>
    public class ProductCouponCodeUploadResultJson : ReturnJsonBase
    {
        /// <summary>本次处理的券 Code 总数。</summary>
        public long total_count { get; set; }

        /// <summary>上传成功的券 Code 列表。</summary>
        public string[] success_code_list { get; set; }

        /// <summary>上传失败的券 Code 列表。</summary>
        public ProductCouponFailedCodeInfo[] failed_code_list { get; set; }

        /// <summary>其他请求已上传的券 Code 列表。</summary>
        public string[] already_exist_code_list { get; set; }

        /// <summary>本次请求中重复的券 Code 列表。</summary>
        public string[] duplicate_code_list { get; set; }
    }

    /// <summary>
    /// 上传失败的券 Code。
    /// </summary>
    public class ProductCouponFailedCodeInfo
    {
        /// <summary>券 Code。</summary>
        public string coupon_code { get; set; }

        /// <summary>失败错误码。</summary>
        public string code { get; set; }

        /// <summary>失败原因。</summary>
        public string message { get; set; }
    }

    /// <summary>
    /// 用户商品券返回结果。
    /// </summary>
    public class ProductCouponUserResultJson : ReturnJsonBase
    {
        /// <summary>用户商品券 Code。</summary>
        public string coupon_code { get; set; }

        /// <summary>用户商品券状态。</summary>
        public string coupon_state { get; set; }

        /// <summary>有效期开始时间。</summary>
        public string valid_begin_time { get; set; }

        /// <summary>有效期结束时间。</summary>
        public string valid_end_time { get; set; }

        /// <summary>领券时间。</summary>
        public string receive_time { get; set; }

        /// <summary>发券请求单号。</summary>
        public string send_request_no { get; set; }

        /// <summary>发券渠道。</summary>
        public string send_channel { get; set; }

        /// <summary>确认发券请求单号。</summary>
        public string confirm_request_no { get; set; }

        /// <summary>确认发券时间。</summary>
        public string confirm_time { get; set; }

        /// <summary>失效请求单号。</summary>
        public string deactivate_request_no { get; set; }

        /// <summary>失效时间。</summary>
        public string deactivate_time { get; set; }

        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }

        /// <summary>单券核销和退券明细。</summary>
        public ProductCouponSingleUsageDetail single_usage_detail { get; set; }

        /// <summary>多次优惠使用明细。</summary>
        public ProductCouponProgressiveBundleUsageDetail progressive_bundle_usage_detail { get; set; }

        /// <summary>商品券详情。</summary>
        public ProductCouponResultJson product_coupon { get; set; }

        /// <summary>商品券批次详情。</summary>
        public ProductCouponStockResultJson stock { get; set; }

        /// <summary>发券时透传的附加数据。</summary>
        public string attach { get; set; }

        /// <summary>商品券标签信息。</summary>
        public ProductCouponTagInfo coupon_tag_info { get; set; }

        /// <summary>会员标签信息。</summary>
        public ProductCouponMemberTagInfo member_tag_info { get; set; }
    }

    /// <summary>
    /// 单券核销和退券明细。
    /// </summary>
    public class ProductCouponSingleUsageDetail
    {
        /// <summary>核销请求单号。</summary>
        public string use_request_no { get; set; }

        /// <summary>核销时间。</summary>
        public string use_time { get; set; }

        /// <summary>退券请求单号。</summary>
        public string return_request_no { get; set; }

        /// <summary>退券时间。</summary>
        public string return_time { get; set; }

        /// <summary>关联微信支付订单。</summary>
        public ProductCouponAssociatedOrderInfo associated_order_info { get; set; }

        /// <summary>关联微信支付分订单。</summary>
        public ProductCouponAssociatedPayScoreOrderInfo associated_pay_score_order_info { get; set; }

        /// <summary>核销优惠金额，单位为分。</summary>
        public long? saved_amount { get; set; }
    }

    /// <summary>
    /// 多次优惠用户券包使用明细。
    /// </summary>
    public class ProductCouponProgressiveBundleUsageDetail
    {
        /// <summary>用户券包信息。</summary>
        public ProductCouponUserBundleInfo user_product_coupon_bundle_info { get; set; }

        /// <summary>券包总优惠次数。</summary>
        public long total_count { get; set; }

        /// <summary>已使用优惠次数。</summary>
        public long used_count { get; set; }
    }

    /// <summary>
    /// 多次优惠用户券包信息。
    /// </summary>
    public class ProductCouponUserBundleInfo
    {
        /// <summary>用户券包 ID。</summary>
        public string user_coupon_bundle_id { get; set; }

        /// <summary>当前用户券在券包中的序号。</summary>
        public long user_coupon_bundle_index { get; set; }
    }

    /// <summary>
    /// 用户商品券列表返回结果。
    /// </summary>
    public class ProductCouponUserListResultJson : ReturnJsonBase
    {
        /// <summary>符合条件的用户商品券总数。</summary>
        public long total_count { get; set; }

        /// <summary>用户商品券列表。</summary>
        public ProductCouponUserResultJson[] user_coupon_list { get; set; }

        /// <summary>下一页标记。</summary>
        public string next_page_token { get; set; }
    }

    /// <summary>
    /// 商品券事件通知地址返回结果。
    /// </summary>
    public class ProductCouponNotifyConfigResultJson : ReturnJsonBase
    {
        /// <summary>商品券事件通知地址。</summary>
        public string notify_url { get; set; }

        /// <summary>通知地址最后更新时间。</summary>
        public string update_time { get; set; }
    }

    /// <summary>
    /// 商品券图片生成任务结果。
    /// </summary>
    public class ProductCouponImageGenerationResultJson : ReturnJsonBase
    {
        /// <summary>微信支付分配的品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>图片生成任务 ID。</summary>
        public string task_id { get; set; }

        /// <summary>图片生成类型。</summary>
        public string image_generation_type { get; set; }

        /// <summary>任务状态。</summary>
        public string task_state { get; set; }

        /// <summary>头图合成结果。</summary>
        public ProductCouponImageResult combine_image_result { get; set; }

        /// <summary>抠图结果。</summary>
        public ProductCouponImageResult cut_out_result { get; set; }
    }

    /// <summary>
    /// 商品券图片处理结果。
    /// </summary>
    public class ProductCouponImageResult
    {
        /// <summary>生成后的图片 URL。</summary>
        public string image_url { get; set; }
    }

    /// <summary>
    /// 商品券图片上传结果。
    /// </summary>
    public class ProductCouponImageUploadResultJson : ReturnJsonBase
    {
        /// <summary>微信支付生成的图片 URL。</summary>
        public string image_url { get; set; }
    }

    #region Compatibility names

    /// <summary>修改商品券请求的兼容命名。</summary>
    public class ProductCouponModifyRequestData : ProductCouponUpdateRequestData
    {
    }

    /// <summary>修改商品券批次请求的兼容命名。</summary>
    public class ProductCouponStockModifyRequestData : ProductCouponStockUpdateRequestData
    {
    }

    /// <summary>批次关联或取消关联门店请求的兼容命名。</summary>
    public class ProductCouponStoresRequestData : ProductCouponStoreOperationRequestData
    {
    }

    /// <summary>预发单券模式商品券请求。</summary>
    public class ProductCouponPreSendRequestData : ProductCouponSendRequestData
    {
    }

    /// <summary>预发商品券返回的客户端领券令牌。</summary>
    public class ProductCouponPreSendResultJson : ReturnJsonBase
    {
        /// <summary>用于调起小程序领券组件的短期令牌。</summary>
        public string token { get; set; }

        /// <summary>令牌失效时间，RFC 3339 格式。</summary>
        public string expire_time { get; set; }
    }

    /// <summary>查询用户商品券详情参数的兼容命名。</summary>
    public class ProductCouponUserCouponQueryRequestData : ProductCouponUserQueryRequestData
    {
    }

    /// <summary>查询用户商品券列表参数的兼容命名。</summary>
    public class ProductCouponUserCouponListQueryRequestData : ProductCouponUserListQueryRequestData
    {
    }

    /// <summary>失效用户商品券请求的兼容命名。</summary>
    public class ProductCouponUserCouponDeactivateRequestData : ProductCouponUserDeactivateRequestData
    {
    }

    /// <summary>退回用户商品券请求的兼容命名。</summary>
    public class ProductCouponUserCouponReturnRequestData : ProductCouponReturnRequestData
    {
    }

    /// <summary>用户商品券返回结果的兼容命名。</summary>
    public class ProductCouponUserCouponResultJson : ProductCouponUserResultJson
    {
    }

    /// <summary>用户商品券列表返回结果的兼容命名。</summary>
    public class ProductCouponUserCouponListResultJson : ProductCouponUserListResultJson
    {
    }

    /// <summary>图片生成任务请求的兼容命名。</summary>
    public class ProductCouponImageGenerationTaskRequestData : ProductCouponImageGenerationRequestData
    {
    }

    /// <summary>图片生成任务结果的兼容命名。</summary>
    public class ProductCouponImageGenerationTaskResultJson : ProductCouponImageGenerationResultJson
    {
    }

    /// <summary>商品券卡包展示配置的兼容命名。</summary>
    public class ProductCouponDisplayConfiguration : ProductCouponCouponDisplayInfo
    {
    }

    /// <summary>商品券事件通知配置的兼容命名。</summary>
    public class ProductCouponNotifyConfiguration : ProductCouponStockNotifyConfig
    {
    }

    #endregion
}
