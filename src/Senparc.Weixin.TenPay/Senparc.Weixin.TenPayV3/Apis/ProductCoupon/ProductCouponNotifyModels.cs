#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCouponNotifyModels.cs
    文件功能描述：微信支付商品券（单券）通知模型及事件常量


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增商品券领券与图片生成结果通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.ProductCoupon
{
    /// <summary>商品券服务商通知事件常量。</summary>
    public static class ProductCouponNotifyEventTypes
    {
        public const string Send = "PRODUCT_COUPON_SP.SEND";
        public const string ImageGeneration =
            "PRODUCT_COUPON_SP.IMAGE_GENERATION";
        public const string CouponOriginalType = "coupon";
        public const string ProductCouponOriginalType = "product_coupon";
    }

    /// <summary>商品券领券通知的解密资源。</summary>
    public class ProductCouponSendNotifyJson : ReturnJsonBase
    {
        public string brand_id { get; set; }
        public string coupon_code { get; set; }
        public string product_coupon_id { get; set; }
        public string stock_id { get; set; }
        public string stock_bundle_id { get; set; }
        public string user_coupon_bundle_id { get; set; }
        public string appid { get; set; }
        public string openid { get; set; }
        public string unionid { get; set; }
        public string receive_time { get; set; }
        public string send_request_no { get; set; }
        public string send_channel { get; set; }
        public string valid_begin_time { get; set; }
        public string valid_end_time { get; set; }
        public string phone_number { get; set; }
        public string country_code { get; set; }
        public string attach { get; set; }
        public string channel_custom_info { get; set; }
    }

    /// <summary>商品券图片生成结果通知的解密资源。</summary>
    public class ProductCouponImageGenerationNotifyJson :
        ProductCouponImageGenerationTaskResultJson
    {
    }
}
