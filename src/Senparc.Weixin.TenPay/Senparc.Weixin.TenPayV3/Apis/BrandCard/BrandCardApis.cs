#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandCardApis.cs
    文件功能描述：微信支付 V3 服务商商家名片接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐商家名片配置与交易连接名片接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.BrandCard;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 服务商商家名片接口。
    /// <para>用于配置、发布和查询商家名片，以及管理支付凭证与商家名片之间的交易连接规则。</para>
    /// </summary>
    public class BrandCardApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建商家名片接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 服务商配置；为空时使用全局默认配置。</param>
        public BrandCardApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 提交商家名片配置申请。
        /// <para>接口采用全量覆盖机制，最新提交会完全覆盖同一申请的历史配置。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016468440</para>
        /// </summary>
        /// <param name="data">业务申请编号、品牌、小程序、客服及服务列表配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单号、品牌 ID 和预览二维码链接。</returns>
        public Task<BrandCardConfigSubmitResultJson> SubmitCardConfigAsync(
            BrandCardConfigRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-configs";
            return PostAsync<BrandCardConfigSubmitResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 发布商家名片配置。
        /// <para>支持审核通过后立即发布，或在一小时后至九十天内的指定时间发布。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016475176</para>
        /// </summary>
        /// <param name="data">申请标识、品牌 ID、发布方式及可选的定时发布时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>发布申请标识和发布计划。</returns>
        public Task<BrandCardConfigPublishResultJson> PublishCardConfigAsync(
            BrandCardConfigPublishRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-configs/publish";
            return PostAsync<BrandCardConfigPublishResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 撤销处于非发布状态的商家名片配置申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016475172</para>
        /// </summary>
        /// <param name="data">业务申请编号或微信支付申请单号，以及品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>撤销后的申请单状态。</returns>
        public Task<BrandCardConfigApplymentResultJson>
            CancelCardConfigApplymentAsync(
                BrandCardConfigApplymentRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-configs/cancel-applyment";
            return PostAsync<BrandCardConfigApplymentResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 查询商家名片配置申请状态。
        /// <para>业务申请编号和微信支付申请单号二选一，品牌 ID 必填。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016475174</para>
        /// </summary>
        /// <param name="data">配置申请查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请状态、发布时间和可选的驳回原因。</returns>
        public Task<BrandCardConfigApplymentResultJson>
            QueryCardConfigApplymentAsync(
                BrandCardConfigApplymentRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandCardQuery("v3/brand/card/card-configs",
                "business_code", data?.business_code,
                "applyment_id", data?.applyment_id,
                "brand_id", data?.brand_id);
            return GetAsync<BrandCardConfigApplymentResultJson>(path, timeOut);
        }

        /// <summary>
        /// 重新获取商家名片预览二维码链接。
        /// <para>仅适用于配置申请提交后、正式发布提交前的预览阶段。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016641998</para>
        /// </summary>
        /// <param name="data">配置申请查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新的预览二维码链接和过期时间。</returns>
        public Task<BrandCardConfigPreviewResultJson> GetCardPreviewUrlAsync(
            BrandCardConfigApplymentRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandCardQuery(
                "v3/brand/card/card-configs/preview-url",
                "business_code", data?.business_code,
                "applyment_id", data?.applyment_id,
                "brand_id", data?.brand_id);
            return GetAsync<BrandCardConfigPreviewResultJson>(path, timeOut);
        }

        /// <summary>
        /// 添加交易连接名片规则申请。
        /// <para>规则决定哪些小程序、APP、支付分或付款码支付凭证可以跳转至商家名片。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016333302</para>
        /// </summary>
        /// <param name="data">业务申请编号、品牌、交易场景及对应账号标识。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>受理的交易连接规则。</returns>
        public Task<BrandCardLinkResultJson> AddCardLinkAsync(
            BrandCardLinkRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-links";
            return PostAsync<BrandCardLinkResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 解除已生效的交易连接名片场景。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016366804</para>
        /// </summary>
        /// <param name="data">品牌、交易场景及对应账号标识。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已解除的交易连接规则。</returns>
        public Task<BrandCardLinkResultJson> UnbindCardLinkAsync(
            BrandCardLinkUnbindRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-links/unbind-card-link";
            return PostAsync<BrandCardLinkResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 撤销已发起但尚未生效的交易连接名片配置申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016366797</para>
        /// </summary>
        /// <param name="data">品牌业务申请编号和品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>被撤销的品牌业务申请编号和品牌 ID。</returns>
        public Task<BrandCardLinkCancelResultJson>
            CancelCardLinkApplymentAsync(BrandCardLinkCancelRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/card/card-links/cancel-applyment";
            return PostAsync<BrandCardLinkCancelResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 查询品牌下已生效的交易连接名片规则。
        /// <para>可按交易场景筛选，分页从 1 开始，单页最多 50 条。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016366785</para>
        /// </summary>
        /// <param name="data">品牌、可选交易场景和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌下已生效的交易连接规则列表。</returns>
        public Task<BrandCardActiveLinksResultJson> QueryActiveCardLinksAsync(
            BrandCardActiveLinksQueryRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandCardQuery("v3/brand/card/card-links",
                "brand_id", data?.brand_id,
                "payment_scene", data?.payment_scene,
                "page_index", data?.page_index?.ToString(CultureInfo.InvariantCulture),
                "page_size", data?.page_size?.ToString(CultureInfo.InvariantCulture));
            return GetAsync<BrandCardActiveLinksResultJson>(path, timeOut);
        }

        /// <summary>
        /// 根据品牌业务申请编号查询交易连接名片添加申请状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016366816</para>
        /// </summary>
        /// <param name="businessCode">添加规则时使用的品牌业务申请编号。</param>
        /// <param name="brandId">品牌 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>交易场景、账号标识、配置状态和可选的驳回原因。</returns>
        public Task<BrandCardLinkApplymentResultJson>
            QueryCardLinkApplymentByBusinessCodeAsync(string businessCode,
                string brandId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/brand/card/card-links/business-code/{EscapeBrandCardValue(businessCode)}";
            path = BuildBrandCardQuery(path, "brand_id", brandId);
            return GetAsync<BrandCardLinkApplymentResultJson>(path, timeOut);
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetUrl(path), data, timeOut);
        }

        private Task<T> GetAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildBrandCardQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeBrandCardValue(query[index])}=" +
                          $"{EscapeBrandCardValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeBrandCardValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
