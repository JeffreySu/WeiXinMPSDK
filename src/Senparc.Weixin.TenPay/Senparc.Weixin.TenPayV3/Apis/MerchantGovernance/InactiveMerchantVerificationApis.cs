#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：InactiveMerchantVerificationApis.cs
    文件功能描述：微信支付 V3 不活跃商户身份核实接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐不活跃商户身份核实与子商户管控查询接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.MerchantGovernance;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 不活跃商户身份核实接口。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012471357</para>
    /// </summary>
    public class InactiveMerchantVerificationApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建不活跃商户身份核实接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 平台商户配置；为空时使用全局默认配置。</param>
        public InactiveMerchantVerificationApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 代特约商户发起不活跃商户身份核实。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012471357</para>
        /// </summary>
        /// <param name="data">需要核实身份的特约商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付生成的核实单号。</returns>
        public Task<InactiveMerchantVerificationSubmitResultJson> StartVerificationAsync(
            InactiveMerchantVerificationRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/compliance/inactive-merchant-identity-verification/merchants";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<InactiveMerchantVerificationSubmitResultJson>(
                GetUrl(path), data, timeOut);
        }

        /// <summary>
        /// 查询指定特约商户的不活跃商户身份核实结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012471359</para>
        /// </summary>
        /// <param name="subMchId">微信支付分配的特约商户号。</param>
        /// <param name="verificationId">发起核实时返回的核实单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>核实状态、失败原因及处理时间。</returns>
        public Task<InactiveMerchantVerificationResultJson> QueryVerificationAsync(
            string subMchId, string verificationId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/compliance/inactive-merchant-identity-verification/merchants/{Escape(subMchId)}/verifications/{Escape(verificationId)}";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<InactiveMerchantVerificationResultJson>(GetUrl(path),
                null, timeOut, ApiRequestMethod.GET);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
