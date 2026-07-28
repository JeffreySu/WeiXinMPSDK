#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：FundAppNotifyHandlerExtensions.cs
    文件功能描述：商家转账免确认收款通知强类型解密入口


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增转账与授权结果通知解密扩展

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.FundApp;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>商家转账免确认收款通知的强类型验签、解密入口。</summary>
    public static class FundAppNotifyHandlerExtensions
    {
        /// <summary>验签并解密商家转账结果通知。</summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式验签。</param>
        public static Task<FundAppTransferResultNotifyJson>
            DecryptFundAppTransferResultNotifyAsync(
                this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<FundAppTransferResultNotifyJson>(isPublicKey);

        /// <summary>验签并解密免确认收款授权结果通知。</summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式验签。</param>
        public static Task<FundAppAuthorizationResultNotifyJson>
            DecryptFundAppAuthorizationResultNotifyAsync(
                this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<FundAppAuthorizationResultNotifyJson>(isPublicKey);
    }
}
