#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MerchantLimitationApis.cs
    文件功能描述：微信支付 V3 子商户管控情况查询接口


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
    /// 微信支付 V3 商户被管控能力及原因查询接口。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012803072</para>
    /// </summary>
    public class MerchantLimitationApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建子商户管控情况查询接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 普通服务商配置；为空时使用全局默认配置。</param>
        public MerchantLimitationApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 查询子商户当前被管控的能力、管控原因及对应解脱路径。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012803072</para>
        /// </summary>
        /// <param name="subMchId">与当前普通服务商存在受理关系的子商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>子商户管控能力和恢复指引。</returns>
        public Task<MerchantLimitationResultJson> QuerySubMerchantLimitationAsync(
            string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/mch-operation-manage/merchant-limitations/sub-mchid/{Escape(subMchId)}";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<MerchantLimitationResultJson>(GetUrl(path), null,
                timeOut, ApiRequestMethod.GET);
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
