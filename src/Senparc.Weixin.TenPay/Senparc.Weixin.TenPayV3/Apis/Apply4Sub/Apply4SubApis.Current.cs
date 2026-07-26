#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubApis.Current.cs
    文件功能描述：微信支付 V3 特约商户进件现行接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐特约商户进件现行 8 项接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 微信支付 V3 特约商户进件现行接口。
    /// </summary>
    public partial class Apply4SubApis
    {
        /// <summary>
        /// 按现行契约提交特约商户进件申请单。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012719997</para>
        /// </summary>
        /// <param name="data">完整的特约商户进件资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付生成的申请单号。</returns>
        public Task<Apply4SubCurrentApplymentResultJson> SubmitApplymentAsync(
            Apply4SubCurrentApplymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/applyment4sub/applyment/";
            return PostCurrentAsync<Apply4SubCurrentApplymentResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 按微信支付申请单号查询特约商户进件状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012697052</para>
        /// </summary>
        /// <param name="applymentId">微信支付生成的申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单状态、签约链接、特约商户号及驳回详情。</returns>
        public Task<Apply4SubCurrentApplymentQueryResultJson> QueryApplymentByIdAsync(
            long applymentId, int timeOut = Config.TIME_OUT)
        {
            var path = $"v3/applyment4sub/applyment/applyment_id/{applymentId}";
            return GetCurrentAsync<Apply4SubCurrentApplymentQueryResultJson>(path, timeOut);
        }

        /// <summary>
        /// 按服务商业务申请编号查询特约商户进件状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012697168</para>
        /// </summary>
        /// <param name="businessCode">服务商自定义且唯一的业务申请编号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单状态、签约链接、特约商户号及驳回详情。</returns>
        public Task<Apply4SubCurrentApplymentQueryResultJson> QueryApplymentByBusinessCodeAsync(
            string businessCode, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/applyment4sub/applyment/business_code/{EscapeCurrent(businessCode)}";
            return GetCurrentAsync<Apply4SubCurrentApplymentQueryResultJson>(path, timeOut);
        }

        /// <summary>
        /// 为已进件特约商户提交修改结算账户申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761102</para>
        /// </summary>
        /// <param name="subMchId">特约商户号或二级商户号。</param>
        /// <param name="data">新的结算账户资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>用于查询审核状态的结算账户修改申请单号。</returns>
        public Task<Apply4SubModifySettlementResultJson> ModifySettlementAsync(
            string subMchId, Apply4SubModifySettlementRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/apply4sub/sub_merchants/{EscapeCurrent(subMchId)}/modify-settlement";
            return PostCurrentAsync<Apply4SubModifySettlementResultJson>(path, data, timeOut);
        }

        /// <summary>
        /// 查询已进件特约商户当前结算账户及验证结果。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761113</para>
        /// </summary>
        /// <param name="subMchId">特约商户号或二级商户号。</param>
        /// <param name="accountNumberRule">银行账号掩码规则；为空时使用微信支付默认规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>结算银行、掩码账号及验证结果。</returns>
        public Task<Apply4SubSettlementResultJson> QuerySettlementAsync(string subMchId,
            string accountNumberRule = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/apply4sub/sub_merchants/{EscapeCurrent(subMchId)}/settlement" +
                BuildAccountNumberRuleQuery(accountNumberRule);
            return GetCurrentAsync<Apply4SubSettlementResultJson>(path, timeOut);
        }

        /// <summary>
        /// 查询特约商户结算账户修改申请的审核状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761120</para>
        /// </summary>
        /// <param name="subMchId">特约商户号或二级商户号。</param>
        /// <param name="applicationNo">修改结算账户接口返回的申请单号。</param>
        /// <param name="accountNumberRule">银行账号掩码规则；为空时使用微信支付默认规则。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>修改后的账户资料、审核结果及更新时间。</returns>
        public Task<Apply4SubSettlementModificationResultJson>
            QuerySettlementModificationAsync(string subMchId, string applicationNo,
                string accountNumberRule = null, int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/apply4sub/sub_merchants/{EscapeCurrent(subMchId)}/application/{EscapeCurrent(applicationNo)}" +
                BuildAccountNumberRuleQuery(accountNumberRule);
            return GetCurrentAsync<Apply4SubSettlementModificationResultJson>(path, timeOut);
        }

        /// <summary>
        /// 上传特约商户进件使用的图片或 PDF 文件。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012760490</para>
        /// </summary>
        /// <param name="fileName">带 JPG、BMP、PNG 或 PDF 扩展名的文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到进件资料中的媒体文件 ID。</returns>
        public Task<Apply4SubMediaUploadResultJson> UploadFileAsync(string fileName,
            Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            return UploadFileAsync(fileName, fileStream, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传特约商户进件使用的图片或 PDF 文件，并支持取消操作。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012760490</para>
        /// </summary>
        /// <param name="fileName">带 JPG、BMP、PNG 或 PDF 扩展名的文件名。</param>
        /// <param name="fileStream">待上传文件流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到进件资料中的媒体文件 ID。</returns>
        public Task<Apply4SubMediaUploadResultJson> UploadFileAsync(string fileName,
            Stream fileStream, CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            return UploadCurrentMediaAsync("v3/merchant/media/upload", fileName,
                fileStream, cancellationToken, timeOut);
        }

        /// <summary>
        /// 上传特约商户进件使用的开户意愿视频。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761084</para>
        /// </summary>
        /// <param name="fileName">带官方支持的视频扩展名的文件名。</param>
        /// <param name="fileStream">待上传视频流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到开户意愿视频字段中的媒体文件 ID。</returns>
        public Task<Apply4SubMediaUploadResultJson> UploadVideoAsync(string fileName,
            Stream fileStream, int timeOut = Config.TIME_OUT)
        {
            return UploadVideoAsync(fileName, fileStream, CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传特约商户进件使用的开户意愿视频，并支持取消操作。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012761084</para>
        /// </summary>
        /// <param name="fileName">带官方支持的视频扩展名的文件名。</param>
        /// <param name="fileStream">待上传视频流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可填写到开户意愿视频字段中的媒体文件 ID。</returns>
        public Task<Apply4SubMediaUploadResultJson> UploadVideoAsync(string fileName,
            Stream fileStream, CancellationToken cancellationToken,
            int timeOut = Config.TIME_OUT)
        {
            return UploadCurrentMediaAsync("v3/merchant/media/video_upload", fileName,
                fileStream, cancellationToken, timeOut);
        }

        private Task<T> PostCurrentAsync<T>(string path, object data, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetCurrentUrl(path), data, timeOut);
        }

        private Task<T> GetCurrentAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetCurrentUrl(path), null, timeOut,
                ApiRequestMethod.GET);
        }

        private Task<Apply4SubMediaUploadResultJson> UploadCurrentMediaAsync(string path,
            string fileName, Stream fileStream, CancellationToken cancellationToken,
            int timeOut)
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestMultipartAsync<Apply4SubMediaUploadResultJson>(
                GetCurrentUrl(path), fileName, fileStream, cancellationToken, timeOut);
        }

        private static string GetCurrentUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string EscapeCurrent(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }

        private static string BuildAccountNumberRuleQuery(string accountNumberRule)
        {
            return string.IsNullOrWhiteSpace(accountNumberRule)
                ? string.Empty
                : $"?account_number_rule={EscapeCurrent(accountNumberRule)}";
        }
    }
}
