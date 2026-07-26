#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandMemberCardNotifyModels.cs
    文件功能描述：微信支付商家名片会员回调模型及事件常量


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增会员卡创建/删除、积分兑券和积分同步回调模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BrandMemberCard
{
    /// <summary>
    /// 商家名片会员通知事件及资源类型常量。
    /// </summary>
    public static class BrandMemberCardNotifyEventTypes
    {
        /// <summary>用户会员卡创建成功。</summary>
        public const string UserCardCreate =
            "BRAND_MEMBER_CARD.USER_CARD.CREATE";

        /// <summary>用户会员卡删除。</summary>
        public const string UserCardDelete =
            "BRAND_MEMBER_CARD.USER_CARD.DELETE";

        /// <summary>用户发起积分兑券。</summary>
        public const string PointExchangeCoupon =
            "BRAND_MEMBER_CARD.POINT_EXCHANGE_COUPON";

        /// <summary>微信支付请求商家同步用户积分。</summary>
        public const string SyncUserPoint =
            "BRAND_MEMBER_CARD.SYNC_USER_POINT";

        /// <summary>用户会员卡通知的 resource.original_type。</summary>
        public const string UserCardOriginalType = "user_card";

        /// <summary>积分兑券通知的 resource.original_type。</summary>
        public const string PointCouponOriginalType = "point_coupon";

        /// <summary>积分同步通知的 resource.original_type。</summary>
        public const string SyncUserPointOriginalType = "sync_user_point";
    }

    /// <summary>
    /// 用户会员卡创建或删除通知的解密资源。
    /// </summary>
    public class BrandMemberCardUserCardNotifyJson :
        BrandMemberCardUserCardResultJson
    {
    }

    /// <summary>
    /// 用户积分兑券通知的解密资源。
    /// </summary>
    public class BrandMemberCardPointExchangeNotifyJson : ReturnJsonBase
    {
        /// <summary>积分兑券记录 ID。</summary>
        public string record_id { get; set; }

        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>会员卡关联的商家 AppID。</summary>
        public string appid { get; set; }

        /// <summary>用户在会员卡模板 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }

        /// <summary>积分兑券模板 ID。</summary>
        public string exchange_coupon_template_id { get; set; }

        /// <summary>本次兑换扣除的积分，官方字段为 uint64。</summary>
        public ulong deduct_points { get; set; }

        /// <summary>商品券 ID。</summary>
        public string product_coupon_id { get; set; }

        /// <summary>商品券批次类型。</summary>
        public string product_coupon_stock_type { get; set; }

        /// <summary>商品券批次 ID。</summary>
        public string stock_id { get; set; }
    }

    /// <summary>
    /// 用户积分同步通知的解密资源。
    /// </summary>
    public class BrandMemberCardPointSyncNotifyJson : ReturnJsonBase
    {
        /// <summary>品牌 ID。</summary>
        public string brand_id { get; set; }

        /// <summary>会员卡模板 ID。</summary>
        public string card_id { get; set; }

        /// <summary>用户在会员卡模板 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>会员卡 Code。</summary>
        public string user_card_code { get; set; }
    }
}
