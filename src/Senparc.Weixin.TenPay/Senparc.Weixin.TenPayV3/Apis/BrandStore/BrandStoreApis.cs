#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandStoreApis.cs
    文件功能描述：微信支付品牌门店接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 补齐品牌门店创建、查询、维护、营业状态及收款商户接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BrandStore;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付品牌门店接口。
    /// <para>用于维护品牌门店资料、营业状态和收款商户号，所有请求均使用品牌 API 专用 RSA 鉴权。</para>
    /// </summary>
    public class BrandStoreApis
    {
        private readonly TenPayApiRequest _request;

        /// <summary>
        /// 创建品牌门店接口实例。
        /// </summary>
        /// <param name="brandApiCredentials">品牌 ID、品牌 API 证书和微信支付公钥凭据。</param>
        public BrandStoreApis(TenPayBrandApiCredentials brandApiCredentials)
        {
            _request = TenPayApiRequest.CreateForBrand(brandApiCredentials);
        }

        /// <summary>
        /// 创建品牌门店。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015782988</para>
        /// </summary>
        /// <param name="data">门店基础信息、地址和经营信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>创建后的品牌门店详情和审核状态。</returns>
        public Task<BrandStoreResultJson> CreateBrandStoreAsync(
            BrandStoreCreateRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "brand/store/brandstores";
            return PostAsync<BrandStoreResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 根据品牌门店 ID 查询门店详情。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015783027</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌门店详情、审核状态及收款商户绑定信息。</returns>
        public Task<BrandStoreResultJson> QueryBrandStoreAsync(string storeId,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}";
            return GetAsync<BrandStoreResultJson>(path, timeOut);
        }

        /// <summary>
        /// 分页查询品牌门店列表。
        /// <para>可按 OPEN、CREATING 或 CLOSED 状态筛选，单页最多返回 200 条。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4016756646</para>
        /// </summary>
        /// <param name="data">可选的门店状态和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌门店列表、分页信息和门店总数。</returns>
        public Task<BrandStoreListResultJson> QueryBrandStoresAsync(
            BrandStoreListQueryRequestData data = null,
            int timeOut = Config.TIME_OUT)
        {
            var path = BuildBrandStoreQuery("brand/store/brandstores",
                "store_state", data?.store_state,
                "offset", data?.offset?.ToString(CultureInfo.InvariantCulture),
                "limit", data?.limit?.ToString(CultureInfo.InvariantCulture));
            return GetAsync<BrandStoreListResultJson>(path, timeOut);
        }

        /// <summary>
        /// 更新指定品牌门店的资料。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015783036</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="data">需要更新的门店基础、地址或经营信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>更新后的品牌门店详情和审核状态。</returns>
        public Task<BrandStoreResultJson> UpdateBrandStoreAsync(string storeId,
            BrandStoreUpdateRequestData data, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}";
            return PatchAsync<BrandStoreResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 永久删除指定品牌门店。
        /// <para>请求不包含正文，成功时微信支付返回 HTTP 204；删除后门店无法恢复。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015783019</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付接口结果；成功时 <see cref="ReturnJsonBase.IsSuccess"/> 为 true。</returns>
        public Task<ReturnJsonBase> DeleteBrandStoreAsync(string storeId,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}";
            return RequestWithoutBodyAsync<ReturnJsonBase>(path, timeOut,
                ApiRequestMethod.DELETE);
        }

        /// <summary>
        /// 暂停指定品牌门店营业。
        /// <para>请求不包含正文，门店状态由 OPEN 变为 CLOSED，并对用户隐藏门店信息。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4016756637</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌门店 ID 和变更后的 CLOSED 状态。</returns>
        public Task<BrandStoreStateResultJson> CloseBrandStoreAsync(
            string storeId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}/close";
            return RequestWithoutBodyAsync<BrandStoreStateResultJson>(path,
                timeOut, ApiRequestMethod.POST);
        }

        /// <summary>
        /// 恢复指定品牌门店营业。
        /// <para>请求不包含正文，门店状态由 CLOSED 变为 OPEN，并重新向用户展示。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4016756643</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌门店 ID 和变更后的 OPEN 状态。</returns>
        public Task<BrandStoreStateResultJson> ResumeBrandStoreAsync(
            string storeId, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}/resume";
            return RequestWithoutBodyAsync<BrandStoreStateResultJson>(path,
                timeOut, ApiRequestMethod.POST);
        }

        /// <summary>
        /// 为指定品牌门店绑定收款商户号。
        /// <para>一个门店目前最多绑定三个收款商户号。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015782993</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="data">品牌已关联的收款商户号和收款主体名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>收款商户号、主体名称和绑定状态。</returns>
        public Task<BrandStoreBindRecipientResultJson> BindRecipientAsync(
            string storeId, BrandStoreBindRecipientRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}/bindrecipient";
            return PostAsync<BrandStoreBindRecipientResultJson>(path, data,
                timeOut);
        }

        /// <summary>
        /// 解绑指定品牌门店的收款商户号。
        /// <para>仅支持解绑收款绑定状态为 CONFIRMED 的商户号。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/brand/4015783007</para>
        /// </summary>
        /// <param name="storeId">微信支付分配的品牌门店 ID。</param>
        /// <param name="data">需要解绑的门店收款商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解绑结果；失败时包含失败原因。</returns>
        public Task<BrandStoreUnbindRecipientResultJson> UnbindRecipientAsync(
            string storeId, BrandStoreUnbindRecipientRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"brand/store/brandstores/{EscapeBrandStoreValue(storeId)}/unbindrecipient";
            return PostAsync<BrandStoreUnbindRecipientResultJson>(path, data,
                timeOut);
        }

        private Task<T> PostAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), data, timeOut);
        }

        private Task<T> PatchAsync<T>(string path, object data, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), data, timeOut,
                ApiRequestMethod.PATCH);
        }

        private Task<T> GetAsync<T>(string path, int timeOut)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestAsync<T>(GetUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private Task<T> RequestWithoutBodyAsync<T>(string path, int timeOut,
            ApiRequestMethod requestMethod)
            where T : ReturnJsonBase, new()
        {
            return _request.RequestWithoutBodyAsync<T>(GetUrl(path), timeOut,
                requestMethod);
        }

        private static string GetUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string BuildBrandStoreQuery(string path,
            params string[] query)
        {
            var parts = new List<string>();
            for (var index = 0; index + 1 < query.Length; index += 2)
            {
                if (string.IsNullOrEmpty(query[index + 1]))
                {
                    continue;
                }

                parts.Add($"{EscapeBrandStoreValue(query[index])}=" +
                          $"{EscapeBrandStoreValue(query[index + 1])}");
            }

            return parts.Count == 0
                ? path
                : $"{path}?{string.Join("&", parts)}";
        }

        private static string EscapeBrandStoreValue(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
