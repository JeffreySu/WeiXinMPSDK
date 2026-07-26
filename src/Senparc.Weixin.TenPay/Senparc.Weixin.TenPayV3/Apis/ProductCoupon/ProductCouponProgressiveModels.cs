#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCouponProgressiveModels.cs
    文件功能描述：微信支付商品券（多次优惠）请求与返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v2.5.1 新增多次优惠批次组和用户券组模型；复用单券模型中的多次优惠共享结构

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.ProductCoupon
{
    #region Requests

    /// <summary>为多次优惠商品券创建批次组。</summary>
    public class ProductCouponStockBundleCreateRequestData
    {
        /// <summary>创建请求单号。</summary>
        public string out_request_no { get; set; }
        /// <summary>待创建的批次组。</summary>
        public ProductCouponStockBundleCreateInfo stock_bundle { get; set; }
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>向用户发放多次优惠商品券组。</summary>
    public class ProductCouponSendBundleRequestData
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }
        /// <summary>批次组 ID。</summary>
        public string stock_bundle_id { get; set; }
        /// <summary>公众账号 AppID。</summary>
        public string appid { get; set; }
        /// <summary>发券请求单号。</summary>
        public string send_request_no { get; set; }
        /// <summary>品牌自定义附加信息。</summary>
        public string attach { get; set; }
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }
        /// <summary>用户商品券标签信息。</summary>
        public ProductCouponTagInfo coupon_tag_info { get; set; }
    }

    /// <summary>预发多次优惠商品券组。</summary>
    public class ProductCouponPreSendBundleRequestData
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }
        /// <summary>批次组 ID。</summary>
        public string stock_bundle_id { get; set; }
        /// <summary>公众账号 AppID。</summary>
        public string appid { get; set; }
        /// <summary>发券请求单号。</summary>
        public string send_request_no { get; set; }
        /// <summary>品牌自定义附加信息。</summary>
        public string attach { get; set; }
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    /// <summary>失效用户多次优惠商品券组。</summary>
    public class ProductCouponUserCouponBundleDeactivateRequestData
    {
        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }
        /// <summary>批次组 ID。</summary>
        public string stock_bundle_id { get; set; }
        /// <summary>公众账号 AppID。</summary>
        public string appid { get; set; }
        /// <summary>失效请求单号。</summary>
        public string out_request_no { get; set; }
        /// <summary>失效原因。</summary>
        public string deactivate_reason { get; set; }
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }
    }

    #endregion

    #region Shared structures

    /// <summary>创建多次优惠批次组时的批次组详情。</summary>
    public class ProductCouponStockBundleCreateInfo
    {
        /// <summary>品牌侧可见备注。</summary>
        public string remark { get; set; }
        /// <summary>券 Code 分配模式。</summary>
        public string coupon_code_mode { get; set; }
        /// <summary>批次组发放规则。</summary>
        public ProductCouponStockSendRule stock_send_rule { get; set; }
        /// <summary>批次组内各轮次优惠规则。</summary>
        public ProductCouponProgressiveBundleUsageRule
            progressive_bundle_usage_rule { get; set; }
        /// <summary>券使用规则展示信息。</summary>
        public ProductCouponUsageRuleDisplayInfo usage_rule_display_info
        { get; set; }
        /// <summary>用户商品券展示信息。</summary>
        public ProductCouponDisplayConfiguration coupon_display_info
        { get; set; }
        /// <summary>事件通知配置。</summary>
        public ProductCouponNotifyConfiguration notify_config { get; set; }
        /// <summary>可用门店范围。</summary>
        public string store_scope { get; set; }
    }

    #endregion

    #region Results

    /// <summary>多次优惠商品券批次组详情。</summary>
    public class ProductCouponStockBundleResultJson : ReturnJsonBase
    {
        /// <summary>批次组 ID。</summary>
        public string stock_bundle_id { get; set; }
        /// <summary>按轮次排序的批次列表。</summary>
        public ProductCouponStockResultJson[] stock_list { get; set; }
    }

    /// <summary>用户多次优惠商品券组详情。</summary>
    public class ProductCouponUserCouponBundleResultJson : ReturnJsonBase
    {
        /// <summary>用户券组 ID。</summary>
        public string user_coupon_bundle_id { get; set; }
        /// <summary>按批次组轮次排序的用户商品券列表。</summary>
        public ProductCouponUserCouponResultJson[] user_product_coupon_list
        { get; set; }
    }

    #endregion
}
