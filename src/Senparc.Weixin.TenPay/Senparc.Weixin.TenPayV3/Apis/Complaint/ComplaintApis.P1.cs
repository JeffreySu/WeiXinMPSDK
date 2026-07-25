#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ComplaintApis.P1.cs
    文件功能描述：ComplaintApis.P1 相关功能


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Complaint;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付消费者投诉补充接口。
    /// </summary>
    public partial class ComplaintApis
    {
        /// <summary>
        /// 更新投诉单的退款审批结果。
        /// </summary>
        /// <param name="complaintId">消费者投诉单号。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>退款审批更新结果。</returns>
        public async Task<ReturnJsonBase> UpdateRefundProgressAsync(string complaintId,
            UpdateRefundProgressRequestData data, int timeOut = Config.TIME_OUT)
        {
            var escapedComplaintId = Uri.EscapeDataString(complaintId ?? "");
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{escapedComplaintId}/update-refund-progress");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<ReturnJsonBase>(url, data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 回复需要即时服务的投诉单。
        /// </summary>
        /// <param name="complaintId">消费者投诉单号。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>即时服务回复结果。</returns>
        public async Task<ReturnJsonBase> ResponseImmediateServiceAsync(string complaintId,
            ImmediateServiceRequestData data, int timeOut = Config.TIME_OUT)
        {
            var escapedComplaintId = Uri.EscapeDataString(complaintId ?? "");
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/complaints-v2/{escapedComplaintId}/response-immediate-service");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestAsync<ReturnJsonBase>(url, data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 上传商户反馈图片，支持 JPG、BMP、PNG，文件不超过 2 MiB。
        /// </summary>
        /// <param name="fileName">上传文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传后的媒体文件 ID 等信息。</returns>
        public Task<UploadImageReturnJson> UploadImageAsync(string fileName, Stream fileStream,
            int timeOut = Config.TIME_OUT)
        {
            return UploadImageAsync(fileName, fileStream, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 异步上传消费者投诉图片。
        /// </summary>
        /// <param name="fileName">上传文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传后的媒体文件 ID 等信息。</returns>
        public async Task<UploadImageReturnJson> UploadImageAsync(string fileName, Stream fileStream,
            CancellationToken cancellationToken, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/images/upload");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return await request.RequestMultipartAsync<UploadImageReturnJson>(url, fileName, fileStream,
                cancellationToken, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取投诉图片并写入目标流。
        /// </summary>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="destination">接收投诉图片的可写流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载并校验成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadImageAsync(string mediaId, Stream destination,
            int timeOut = Config.TIME_OUT)
        {
            return DownloadImageAsync(mediaId, destination, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 异步下载消费者投诉图片。
        /// </summary>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="destination">接收投诉图片的可写流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载并校验成功时为 <see langword="true"/>。</returns>
        public Task<bool> DownloadImageAsync(string mediaId, Stream destination,
            CancellationToken cancellationToken, int timeOut = Config.TIME_OUT)
        {
            var escapedMediaId = Uri.EscapeDataString(mediaId ?? "");
            var url = BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/merchant-service/images/{escapedMediaId}");
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return TenPayDownloadHelper.DownloadAndVerifyAsync(request, url, destination,
                null, null, timeOut, cancellationToken);
        }
    }
}
