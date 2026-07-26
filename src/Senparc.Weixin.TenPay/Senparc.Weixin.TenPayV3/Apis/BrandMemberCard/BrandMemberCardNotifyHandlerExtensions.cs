#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandMemberCardNotifyHandlerExtensions.cs
    文件功能描述：商家名片会员品牌回调的强类型解密入口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v2.5.1 新增会员卡、积分兑券和积分同步通知解密扩展；完善品牌会员回调解密入口的 XML 注释

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BrandMemberCard;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 商家名片会员品牌通知的强类型验签、解密入口。
    /// </summary>
    public static class BrandMemberCardNotifyHandlerExtensions
    {
        /// <summary>
        /// 异步验签并解密用户会员卡创建或删除通知。
        /// <para>适用于 <see cref="BrandMemberCardNotifyEventTypes.UserCardCreate"/> 和 <see cref="BrandMemberCardNotifyEventTypes.UserCardDelete"/> 事件。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015582700</para>
        /// </summary>
        /// <param name="handler">已经读取微信支付通知正文的通知处理器。</param>
        /// <param name="brandApiKey">品牌经营平台设置的 32 字节品牌 API 密钥。</param>
        /// <param name="brandApiCredentials">包含品牌关联微信支付公钥 ID 和公钥的鉴权凭据。</param>
        /// <returns>验签并解密后的用户会员卡创建或删除信息。</returns>
        public static Task<BrandMemberCardUserCardNotifyJson>
            DecryptBrandMemberCardUserCardNotifyAsync(
                this TenPayNotifyHandler handler, string brandApiKey,
                TenPayBrandApiCredentials brandApiCredentials) =>
            handler.DecryptBrandGetObjectAsync<
                BrandMemberCardUserCardNotifyJson>(brandApiKey,
                brandApiCredentials);

        /// <summary>
        /// 异步验签并解密用户积分兑券通知。
        /// <para>官方事件：<see cref="BrandMemberCardNotifyEventTypes.PointExchangeCoupon"/>。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015879057</para>
        /// </summary>
        /// <param name="handler">已经读取微信支付通知正文的通知处理器。</param>
        /// <param name="brandApiKey">品牌经营平台设置的 32 字节品牌 API 密钥。</param>
        /// <param name="brandApiCredentials">包含品牌关联微信支付公钥 ID 和公钥的鉴权凭据。</param>
        /// <returns>验签并解密后的积分兑券记录、会员和商品券批次信息。</returns>
        public static Task<BrandMemberCardPointExchangeNotifyJson>
            DecryptBrandMemberCardPointExchangeNotifyAsync(
                this TenPayNotifyHandler handler, string brandApiKey,
                TenPayBrandApiCredentials brandApiCredentials) =>
            handler.DecryptBrandGetObjectAsync<
                BrandMemberCardPointExchangeNotifyJson>(brandApiKey,
                brandApiCredentials);

        /// <summary>
        /// 异步验签并解密用户积分同步通知。
        /// <para>官方事件：<see cref="BrandMemberCardNotifyEventTypes.SyncUserPoint"/>。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4016096803</para>
        /// </summary>
        /// <param name="handler">已经读取微信支付通知正文的通知处理器。</param>
        /// <param name="brandApiKey">品牌经营平台设置的 32 字节品牌 API 密钥。</param>
        /// <param name="brandApiCredentials">包含品牌关联微信支付公钥 ID 和公钥的鉴权凭据。</param>
        /// <returns>验签并解密后的品牌、会员卡和用户标识信息。</returns>
        public static Task<BrandMemberCardPointSyncNotifyJson>
            DecryptBrandMemberCardPointSyncNotifyAsync(
                this TenPayNotifyHandler handler, string brandApiKey,
                TenPayBrandApiCredentials brandApiCredentials) =>
            handler.DecryptBrandGetObjectAsync<
                BrandMemberCardPointSyncNotifyJson>(brandApiKey,
                brandApiCredentials);
    }
}
