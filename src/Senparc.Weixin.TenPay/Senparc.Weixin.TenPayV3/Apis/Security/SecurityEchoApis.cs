#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SecurityEchoApis.cs
    文件功能描述：微信支付 V3 安全探测接口


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增安全探测接口及敏感消息自动加密

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Security
{
    /// <summary>微信支付 V3 安全探测接口。</summary>
    public class SecurityEchoApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _setting;

        /// <summary>创建安全探测接口实例。</summary>
        /// <param name="setting">微信支付 V3 商户配置；为空时使用全局配置。</param>
        public SecurityEchoApis(ISenparcWeixinSettingForTenpayV3 setting = null)
        {
            _setting = setting ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 发起微信支付安全探测。
        /// <para>SDK 自动加密 encrypted_echo_message 并设置 Wechatpay-Serial。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014551946</para>
        /// </summary>
        /// <param name="data">通知地址、明文消息和可选的加密消息明文。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<SecurityEchoReturnJson> EchoAsync(
            SecurityEchoRequestData data, int timeOut = Config.TIME_OUT)
        {
            var request = await CreateSecurityRequestAsync(data).ConfigureAwait(false);
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/security/echo");
            return await request.RequestAsync<SecurityEchoReturnJson>(url, data, timeOut)
                .ConfigureAwait(false);
        }

        private async Task<TenPayApiRequest> CreateSecurityRequestAsync(object target)
        {
            var publicKey = GetConfiguredPaymentPublicKey();
            if (string.IsNullOrWhiteSpace(publicKey.Key))
            {
                var publicKeys = await new BasePayApis(_setting)
                    .GetPublicKeysAsync().ConfigureAwait(false);
                publicKey = SelectPaymentPublicKey(publicKeys);
            }

            if (string.IsNullOrWhiteSpace(publicKey.Key) ||
                string.IsNullOrWhiteSpace(publicKey.Value))
            {
                throw new TenpayApiRequestException(
                    "未获取到用于安全探测的微信支付公钥或平台证书。");
            }

            SecurityHelper.FieldEncrypt(target, publicKey.Value,
                _setting.EncryptionType.Value,
                _setting.TenPayV3_TenPayPubKeyEnable);
            return new TenPayApiRequest(_setting, httpClient =>
                httpClient.DefaultRequestHeaders.Add("Wechatpay-Serial", publicKey.Key));
        }

        private KeyValuePair<string, string> GetConfiguredPaymentPublicKey()
        {
            if (!_setting.TenPayV3_TenPayPubKeyEnable)
            {
                return default;
            }

            return new KeyValuePair<string, string>(
                _setting.TenPayV3_TenPayPubKeyID,
                SecurityHelper.GetUnwrapCertKey(_setting.TenPayV3_TenPayPubKey));
        }

        private KeyValuePair<string, string> SelectPaymentPublicKey(
            IReadOnlyDictionary<string, string> publicKeys)
        {
            if (publicKeys == null)
            {
                return default;
            }

            var configuredId = _setting.TenPayV3_TenPayPubKeyID;
            if (!string.IsNullOrWhiteSpace(configuredId) &&
                publicKeys.TryGetValue(configuredId, out var configuredKey))
            {
                return new KeyValuePair<string, string>(configuredId, configuredKey);
            }

            return publicKeys.FirstOrDefault();
        }
    }
}
