#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SecurityEchoNotifyHandlerExtensions.cs
    文件功能描述：微信支付安全探测通知强类型解密入口


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增安全探测成功通知解密扩展

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Security;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>微信支付安全探测通知的强类型验签、解密入口。</summary>
    public static class SecurityEchoNotifyHandlerExtensions
    {
        /// <summary>验签并解密安全探测成功通知。</summary>
        /// <param name="handler">微信支付通知处理器。</param>
        /// <param name="isPublicKey">是否使用微信支付公钥模式验签。</param>
        public static Task<SecurityEchoNotifyJson> DecryptSecurityEchoNotifyAsync(
            this TenPayNotifyHandler handler, bool isPublicKey = false) =>
            handler.DecryptGetObjectAsync<SecurityEchoNotifyJson>(isPublicKey);
    }
}
