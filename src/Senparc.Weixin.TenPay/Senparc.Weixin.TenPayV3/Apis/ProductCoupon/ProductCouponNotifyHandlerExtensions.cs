#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ProductCouponNotifyHandlerExtensions.cs
    文件功能描述：商品券（单券）通知的强类型解密入口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增商品券领券与图片生成通知解密扩展

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.ProductCoupon;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>商品券（单券）通知的强类型验签、解密入口。</summary>
    public static class ProductCouponNotifyHandlerExtensions
    {
        /// <summary>验签并解密商品券领券通知。</summary>
        public static Task<ProductCouponSendNotifyJson>
            DecryptProductCouponSendNotifyAsync(
                this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<ProductCouponSendNotifyJson>(
                isPublicKey);

        /// <summary>验签并解密商品券图片生成结果通知。</summary>
        public static Task<ProductCouponImageGenerationNotifyJson>
            DecryptProductCouponImageGenerationNotifyAsync(
                this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<
                ProductCouponImageGenerationNotifyJson>(isPublicKey);
    }
}
