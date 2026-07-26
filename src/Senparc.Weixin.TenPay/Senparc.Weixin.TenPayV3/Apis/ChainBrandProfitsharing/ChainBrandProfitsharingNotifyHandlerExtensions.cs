#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChainBrandProfitsharingNotifyHandlerExtensions.cs
    文件功能描述：连锁品牌分账动账通知强类型解密入口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 新增连锁品牌分账动账通知解密扩展

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.ChainBrandProfitsharing;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 连锁品牌分账动账通知的强类型验签、解密入口。
    /// </summary>
    public static class ChainBrandProfitsharingNotifyHandlerExtensions
    {
        /// <summary>
        /// 异步验签并解密连锁品牌分账或分账回退动账通知。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012075400</para>
        /// </summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式验签。</param>
        /// <returns>验签并解密后的服务商、出资商户、分账单和接收方信息。</returns>
        public static Task<ChainBrandProfitsharingNotifyJson>
            DecryptChainBrandProfitsharingNotifyAsync(
                this TenPayNotifyHandler handler,
                bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<
                ChainBrandProfitsharingNotifyJson>(isPublicKey);
    }
}
