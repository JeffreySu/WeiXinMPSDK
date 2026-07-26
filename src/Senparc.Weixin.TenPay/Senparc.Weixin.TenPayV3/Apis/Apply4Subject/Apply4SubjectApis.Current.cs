#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubjectApis.Current.cs
    文件功能描述：微信支付 V3 现行商户开户意愿确认接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Subject
{
    /// <summary>
    /// 微信支付 V3 商户开户意愿确认现行接口。
    /// </summary>
    public partial class Apply4SubjectApis
    {
        /// <summary>
        /// 按现行接口提交商户开户意愿确认申请单。
        /// </summary>
        /// <param name="data">联系人、主体、法定代表人和补充材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付生成的申请单编号。</returns>
        public Task<Apply4SubjectApplicationResultJson> SubmitApplymentAsync(
            Apply4SubjectApplicationRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/apply4subject/applyment/";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<Apply4SubjectApplicationResultJson>(GetUrl(path), data,
                timeOut);
        }

        /// <summary>
        /// 撤销商户开户意愿确认申请单。
        /// </summary>
        /// <param name="businessCode">业务申请编号，与 applymentId 至少填写一个。</param>
        /// <param name="applymentId">微信支付申请单编号，与 businessCode 至少填写一个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付接口结果；成功时官方返回 HTTP 204。</returns>
        public Task<ReturnJsonBase> CancelApplymentAsync(string businessCode = null,
            long? applymentId = null, int timeOut = Config.TIME_OUT)
        {
            var identifier = !string.IsNullOrWhiteSpace(businessCode)
                ? businessCode
                : applymentId?.ToString();
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentException("业务申请编号和微信支付申请单编号至少填写一个。");
            }

            var path = $"v3/apply4subject/applyment/{Escape(identifier)}/cancel";
            if (!string.IsNullOrWhiteSpace(businessCode) && applymentId.HasValue)
            {
                path += $"?applyment_id={applymentId.Value}";
            }

            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestWithoutBodyAsync<ReturnJsonBase>(GetUrl(path), timeOut);
        }

        /// <summary>
        /// 查询商户开户意愿确认申请单审核结果。
        /// </summary>
        /// <param name="applymentId">微信支付申请单编号，与 businessCode 至少填写一个。</param>
        /// <param name="businessCode">业务申请编号，与 applymentId 至少填写一个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请状态、小程序码和驳回信息。</returns>
        public Task<Apply4SubjectAuditResultJson> QueryApplymentAuditResultAsync(
            long? applymentId = null, string businessCode = null,
            int timeOut = Config.TIME_OUT)
        {
            var query = BuildQuery(new Dictionary<string, object>
            {
                ["applyment_id"] = applymentId,
                ["business_code"] = businessCode
            });
            if (query.Length == 0)
            {
                throw new ArgumentException("业务申请编号和微信支付申请单编号至少填写一个。");
            }

            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<Apply4SubjectAuditResultJson>(
                GetUrl("v3/apply4subject/applyment" + query), null, timeOut,
                ApiRequestMethod.GET);
        }

        /// <summary>
        /// 获取特约商户的开户意愿确认授权状态。
        /// </summary>
        /// <param name="subMchId">微信支付分配的特约商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>特约商户是否已完成实名认证授权。</returns>
        public Task<Apply4SubjectAuthorizationStateResultJson> QueryMerchantAuthorizationStateAsync(
            string subMchId, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/apply4subject/applyment/merchants/{Escape(subMchId)}/state";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<Apply4SubjectAuthorizationStateResultJson>(GetUrl(path),
                null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 上传商户开户意愿确认申请材料图片。
        /// </summary>
        /// <param name="fileName">带 JPG、JPEG、PNG 或 BMP 扩展名的文件名。</param>
        /// <param name="fileStream">待上传的图片流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到申请材料中的媒体文件 ID。</returns>
        public Task<Apply4SubjectMediaUploadResultJson> UploadImageAsync(string fileName,
            Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            return UploadImageAsync(fileName, fileStream, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传商户开户意愿确认申请材料图片，并支持取消操作。
        /// </summary>
        /// <param name="fileName">带 JPG、JPEG、PNG 或 BMP 扩展名的文件名。</param>
        /// <param name="fileStream">待上传的图片流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到申请材料中的媒体文件 ID。</returns>
        public Task<Apply4SubjectMediaUploadResultJson> UploadImageAsync(string fileName,
            Stream fileStream, CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestMultipartAsync<Apply4SubjectMediaUploadResultJson>(
                GetUrl("v3/merchant/media/upload"), fileName, fileStream, cancellationToken,
                timeOut);
        }

        private static string BuildQuery(IReadOnlyDictionary<string, object> values)
        {
            var parameters = values
                .Where(item => item.Value != null &&
                               !string.IsNullOrWhiteSpace(Convert.ToString(item.Value)))
                .Select(item => $"{item.Key}={Escape(Convert.ToString(item.Value))}")
                .ToArray();
            return parameters.Length == 0 ? string.Empty : "?" + string.Join("&", parameters);
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
