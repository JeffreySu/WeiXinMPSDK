#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BrandApplymentApis.cs
    文件功能描述：微信支付 V3 服务商品牌入驻接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.BrandApplyment;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付 V3 服务商品牌入驻接口。
    /// <para>用于提交品牌入驻申请、查询或撤销申请，以及上传申请材料图片。</para>
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016433410</para>
    /// </summary>
    public class BrandApplymentApis
    {
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 创建品牌入驻接口实例。
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">微信支付 V3 服务商配置；为空时使用全局默认配置。</param>
        public BrandApplymentApis(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ??
                                  Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 提交品牌入驻申请。
        /// </summary>
        /// <param name="data">管理员、主体、品牌和商标申请资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付申请单号和业务申请编号。</returns>
        public Task<BrandApplymentResultJson> SubmitApplymentAsync(
            BrandApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/applyments";
            return PostAsync<BrandApplymentResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 根据服务商业务申请编号查询品牌入驻申请状态。
        /// </summary>
        /// <param name="businessCode">提交申请时使用的业务申请编号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌入驻申请状态和审核信息。</returns>
        public Task<BrandApplymentQueryResultJson> QueryByBusinessCodeAsync(
            string businessCode, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/brand/applyments/business-code/{Escape(businessCode)}";
            return GetAsync<BrandApplymentQueryResultJson>(path, timeOut);
        }

        /// <summary>
        /// 根据微信支付申请单 ID 查询品牌入驻申请状态。
        /// </summary>
        /// <param name="applymentId">微信支付申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>品牌入驻申请状态和审核信息。</returns>
        public Task<BrandApplymentQueryResultJson> QueryByApplymentIdAsync(
            string applymentId, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/brand/applyments/applyment-id/{Escape(applymentId)}";
            return GetAsync<BrandApplymentQueryResultJson>(path, timeOut);
        }

        /// <summary>
        /// 撤销品牌入驻申请。
        /// </summary>
        /// <param name="data">业务申请编号或微信支付申请单号，两者填写一个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>被撤销申请的微信支付申请单号和业务申请编号。</returns>
        public Task<BrandApplymentResultJson> CancelApplymentAsync(
            BrandApplymentCancelRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/brand/applyments/cancel-applyment";
            return PostAsync<BrandApplymentResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 上传品牌入驻申请材料图片。
        /// </summary>
        /// <param name="fileName">带 JPG、JPEG、PNG 或 BMP 扩展名的文件名。</param>
        /// <param name="fileStream">待上传的图片流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到品牌申请资料中的媒体文件 ID。</returns>
        public Task<BrandMediaUploadResultJson> UploadImageAsync(string fileName,
            Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            return UploadImageAsync(fileName, fileStream, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传品牌入驻申请材料图片，并支持取消操作。
        /// </summary>
        /// <param name="fileName">带 JPG、JPEG、PNG 或 BMP 扩展名的文件名。</param>
        /// <param name="fileStream">待上传的图片流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到品牌申请资料中的媒体文件 ID。</returns>
        public Task<BrandMediaUploadResultJson> UploadImageAsync(string fileName,
            Stream fileStream, CancellationToken cancellationToken, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/merchant/media/upload";
            var url = GetUrl(path);
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestMultipartAsync<BrandMediaUploadResultJson>(url, fileName,
                fileStream, cancellationToken, timeOut);
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
            return request.RequestAsync<T>(GetUrl(path), null, timeOut, ApiRequestMethod.GET);
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
