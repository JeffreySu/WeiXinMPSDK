#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DeliveryPlanApis.cs
    文件功能描述：微信支付 V3 服务商摇一摇有优惠投放计划接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐摇一摇有优惠投放计划接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.DeliveryPlan;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 服务商“摇一摇有优惠”投放计划接口。
    /// <para>用于创建、查询、更新和终止品牌商品券投放计划，并设置投放计划状态回调地址。</para>
    /// </summary>
    public class DeliveryPlanApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建投放计划接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 服务商配置；为空时使用全局默认配置。</param>
        public DeliveryPlanApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 创建摇一摇有优惠投放计划。
        /// <para>单券模式填写批次 ID，多次优惠模式填写批次组 ID。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016184554</para>
        /// </summary>
        /// <param name="data">品牌、商品券、库存、投放时间和限领规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建成功的投放计划详情。</returns>
        public Task<DeliveryPlanResultJson> CreateDeliveryPlanAsync(
            DeliveryPlanCreateRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path =
                "v3/marketing/partner/delivery-plan/delivery-plans";
            return PostAsync<DeliveryPlanResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 分页查询指定品牌的投放计划列表。
        /// <para>可按计划状态、审核状态或计划 ID 筛选，offset 从 0 开始。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016184563</para>
        /// </summary>
        /// <param name="brandId">创建投放计划的品牌 ID。</param>
        /// <param name="data">分页和筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>符合条件的投放计划总数和列表。</returns>
        public Task<DeliveryPlanListResultJson> QueryDeliveryPlansAsync(
            string brandId, DeliveryPlanQueryRequestData data = null,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/marketing/partner/delivery-plan/delivery-plans/{EscapeDeliveryPlanValue(brandId)}/delivery-plans";
            path = BuildDeliveryPlanQuery(path,
                "page_size", data?.page_size?.ToString(CultureInfo.InvariantCulture),
                "offset", data?.offset?.ToString(CultureInfo.InvariantCulture),
                "plan_state", data?.plan_state,
                "audit_state", data?.audit_state,
                "plan_id", data?.plan_id);
            return GetAsync<DeliveryPlanListResultJson>(path, timeOut);
        }

        /// <summary>
        /// 更新投放计划。
        /// <para>库存、限领和结束时间仅支持按官方规则增加或延长，不支持减少。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016184594</para>
        /// </summary>
        /// <param name="planId">微信支付生成的投放计划 ID。</param>
        /// <param name="data">唯一修改请求单号和需要更新的字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新后的投放计划详情。</returns>
        public Task<DeliveryPlanResultJson> UpdateDeliveryPlanAsync(
            string planId, DeliveryPlanUpdateRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/marketing/partner/delivery-plan/delivery-plans/{EscapeDeliveryPlanValue(planId)}";
            return PatchAsync<DeliveryPlanResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 终止投放计划。
        /// <para>该请求不包含正文，成功时微信支付返回 HTTP 204。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016184572</para>
        /// </summary>
        /// <param name="planId">需要终止的投放计划 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付接口结果；成功时 <see cref="ReturnJsonBase.IsSuccess"/> 为 true。</returns>
        public Task<ReturnJsonBase> TerminateDeliveryPlanAsync(string planId,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/marketing/partner/delivery-plan/delivery-plans/{EscapeDeliveryPlanValue(planId)}/terminate";
            return PostWithoutBodyAsync<ReturnJsonBase>(path, timeOut);
        }

        /// <summary>
        /// 设置投放计划状态变更通知地址。
        /// <para>计划状态或审核状态发生变化时，微信支付会向该地址发送加密通知。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016184598</para>
        /// </summary>
        /// <param name="serviceProviderMchId">服务商商户号。</param>
        /// <param name="data">HTTPS 通知回调地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>设置后的通知回调地址。</returns>
        public Task<DeliveryPlanNotifyUrlResultJson> SetDeliveryPlanNotifyUrlAsync(
            string serviceProviderMchId, DeliveryPlanNotifyUrlRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/marketing/partner/delivery-plan/{EscapeDeliveryPlanValue(serviceProviderMchId)}/notify-url";
            return PostAsync<DeliveryPlanNotifyUrlResultJson>(path, data, timeOut);
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetUrl(path), data, timeOut);
        }

        private Task<T> PatchAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetUrl(path), data, timeOut,
                ApiRequestMethod.PATCH);
        }

        private Task<T> GetAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private Task<T> PostWithoutBodyAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestWithoutBodyAsync<T>(GetUrl(path), timeOut);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildDeliveryPlanQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeDeliveryPlanValue(query[index])}=" +
                          $"{EscapeDeliveryPlanValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeDeliveryPlanValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
