#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：FundAppApis.Authorization.cs
    文件功能描述：微信支付 V3 商家转账免确认收款授权接口


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增免确认收款授权及授权后转账接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    public partial class FundAppApis
    {
        /// <summary>
        /// 预受理免确认收款转账。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014399293</para>
        /// </summary>
        /// <param name="data">预受理转账和授权信息；user_name 传明文时由 SDK 自动加密。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<PreTransferWithAuthorizationReturnJson>
            PreTransferWithAuthorizationAsync(
                PreTransferWithAuthorizationRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var request = await CreateAuthorizationRequestAsync(data,
                !string.IsNullOrWhiteSpace(data?.user_name)).ConfigureAwait(false);
            return await request.RequestAsync<PreTransferWithAuthorizationReturnJson>(
                GetAuthorizationUrl("v3/fund-app/mch-transfer/transfer-bills/" +
                    "pre-transfer-with-authorization"), data, timeOut)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 创建用户确认免确认收款授权。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4015901167</para>
        /// </summary>
        /// <param name="data">授权单、用户及客户端场景信息。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<CreateUserConfirmAuthorizationReturnJson>
            CreateUserConfirmAuthorizationAsync(
                CreateUserConfirmAuthorizationRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<CreateUserConfirmAuthorizationReturnJson>(
                GetAuthorizationUrl("v3/fund-app/mch-transfer/" +
                    "user-confirm-authorization"), data, timeOut)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 根据商户授权单号查询免确认收款授权。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014399423</para>
        /// </summary>
        /// <param name="outAuthorizationNo">商户系统内部唯一的授权单号。</param>
        /// <param name="isDisplayAuthorization">是否返回用于调起授权确认页的 package_info。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<UserConfirmAuthorizationReturnJson>
            QueryUserConfirmAuthorizationAsync(string outAuthorizationNo,
                bool? isDisplayAuthorization = null,
                int timeOut = Config.TIME_OUT)
        {
            var path = "v3/fund-app/mch-transfer/user-confirm-authorization/" +
                "out-authorization-no/" + EscapeAuthorizationPath(outAuthorizationNo);
            if (isDisplayAuthorization.HasValue)
            {
                path += "?is_display_authorization=" +
                    isDisplayAuthorization.Value.ToString().ToLowerInvariant();
            }

            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<UserConfirmAuthorizationReturnJson>(
                GetAuthorizationUrl(path), null, timeOut, ApiRequestMethod.GET)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 使用已生效的免确认收款授权发起转账。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014399371</para>
        /// </summary>
        /// <param name="data">授权 ID、转账金额及场景信息；user_name 传明文时由 SDK 自动加密。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<TransferWithAuthorizationReturnJson>
            TransferWithAuthorizationAsync(TransferWithAuthorizationRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var request = await CreateAuthorizationRequestAsync(data,
                !string.IsNullOrWhiteSpace(data?.user_name)).ConfigureAwait(false);
            return await request.RequestAsync<TransferWithAuthorizationReturnJson>(
                GetAuthorizationUrl("v3/fund-app/mch-transfer/transfer-bills/transfer"),
                data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 关闭指定商户授权单号的免确认收款授权。
        /// <para>该接口发送不含请求正文的 POST 请求。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4015653811</para>
        /// </summary>
        /// <param name="outAuthorizationNo">商户系统内部唯一的授权单号。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public async Task<UserConfirmAuthorizationReturnJson>
            CloseUserConfirmAuthorizationAsync(string outAuthorizationNo,
                int timeOut = Config.TIME_OUT)
        {
            var path = "v3/fund-app/mch-transfer/user-confirm-authorization/" +
                "out-authorization-no/" + EscapeAuthorizationPath(outAuthorizationNo) +
                "/close";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestWithoutBodyAsync<UserConfirmAuthorizationReturnJson>(
                GetAuthorizationUrl(path), timeOut).ConfigureAwait(false);
        }

        private async Task<TenPayApiRequest> CreateAuthorizationRequestAsync(
            object target, bool containsSensitiveData)
        {
            if (!containsSensitiveData)
            {
                return new TenPayApiRequest(_tenpayV3Setting);
            }

            var publicKey = GetConfiguredPaymentPublicKey();
            if (string.IsNullOrWhiteSpace(publicKey.Key))
            {
                var publicKeys = await new BasePayApis(_tenpayV3Setting)
                    .GetPublicKeysAsync().ConfigureAwait(false);
                publicKey = SelectPaymentPublicKey(publicKeys);
            }

            if (string.IsNullOrWhiteSpace(publicKey.Key) ||
                string.IsNullOrWhiteSpace(publicKey.Value))
            {
                throw new TenpayApiRequestException(
                    "未获取到用于加密收款用户姓名的微信支付公钥或平台证书。");
            }

            SecurityHelper.FieldEncrypt(target, publicKey.Value,
                _tenpayV3Setting.EncryptionType.Value,
                _tenpayV3Setting.TenPayV3_TenPayPubKeyEnable);
            return new TenPayApiRequest(_tenpayV3Setting, httpClient =>
                httpClient.DefaultRequestHeaders.Add("Wechatpay-Serial", publicKey.Key));
        }

        private KeyValuePair<string, string> GetConfiguredPaymentPublicKey()
        {
            if (!_tenpayV3Setting.TenPayV3_TenPayPubKeyEnable)
            {
                return default;
            }

            return new KeyValuePair<string, string>(
                _tenpayV3Setting.TenPayV3_TenPayPubKeyID,
                SecurityHelper.GetUnwrapCertKey(_tenpayV3Setting.TenPayV3_TenPayPubKey));
        }

        private KeyValuePair<string, string> SelectPaymentPublicKey(
            IReadOnlyDictionary<string, string> publicKeys)
        {
            if (publicKeys == null)
            {
                return default;
            }

            var configuredId = _tenpayV3Setting.TenPayV3_TenPayPubKeyID;
            if (!string.IsNullOrWhiteSpace(configuredId) &&
                publicKeys.TryGetValue(configuredId, out var configuredKey))
            {
                return new KeyValuePair<string, string>(configuredId, configuredKey);
            }

            return publicKeys.FirstOrDefault();
        }

        private static string GetAuthorizationUrl(string path) =>
            BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");

        private static string EscapeAuthorizationPath(string value) =>
            Uri.EscapeDataString(value ?? string.Empty);
    }
}
