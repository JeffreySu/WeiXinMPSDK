#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：EcommerceApis.MerchantCancellation.cs
    文件功能描述：微信支付 V3 电商收付通商户注销接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐商户注销预校验、新旧注销申请及提现接口

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 微信支付 V3 电商收付通商户注销接口。
    /// </summary>
    public partial class EcommerceApis
    {
        /// <summary>
        /// 校验二级商户是否具备发起注销提现申请的资格。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4016420099</para>
        /// </summary>
        /// <param name="subMchId">申请注销的二级商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户状态、资格校验结果、账户余额及不可注销原因。</returns>
        public Task<EcommerceCancellationValidationResultJson>
            ValidateMerchantCancellationAsync(string subMchId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/account/apply-cancel-withdraw/validate-cancel/{EscapeMerchantCancellationPath(subMchId)}";
            return GetMerchantCancellationAsync<EcommerceCancellationValidationResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按新流程提交二级商户注销提现申请。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013892756</para>
        /// </summary>
        /// <param name="data">注销、提现、收款账户及证明材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付申请单号和商户申请单号。</returns>
        public Task<EcommerceCancellationApplyResultJson> ApplyCancelWithdrawAsync(
            EcommerceApplyCancelWithdrawRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/ecommerce/account/apply-cancel-withdraw";
            return PostMerchantCancellationAsync<EcommerceCancellationApplyResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按商户注销申请单号查询新流程注销提现申请状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013892759</para>
        /// </summary>
        /// <param name="outRequestNo">商户自定义的注销申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>注销、提现、账户资金及商户确认状态。</returns>
        public Task<EcommerceCancelWithdrawQueryResultJson>
            QueryCancelWithdrawByOutRequestNoAsync(string outRequestNo,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/account/apply-cancel-withdraw/out-request-no/{EscapeMerchantCancellationPath(outRequestNo)}";
            return GetMerchantCancellationAsync<EcommerceCancelWithdrawQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按微信支付申请单号查询新流程注销提现申请状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4013892765</para>
        /// </summary>
        /// <param name="applymentId">微信支付生成的注销提现申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>注销、提现、账户资金及商户确认状态。</returns>
        public Task<EcommerceCancelWithdrawQueryResultJson>
            QueryCancelWithdrawByApplymentIdAsync(string applymentId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/account/apply-cancel-withdraw/applyment-id/{EscapeMerchantCancellationPath(applymentId)}";
            return GetMerchantCancellationAsync<EcommerceCancelWithdrawQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按旧流程提交二级商户注销申请单。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476217</para>
        /// </summary>
        /// <param name="data">二级商户号、商户申请单号及注销申请材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>注销申请的受理或审核状态。</returns>
        public Task<EcommerceLegacyCancelApplicationResultJson>
            SubmitLegacyCancelApplicationAsync(
                EcommerceLegacyCancelApplicationRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/ecommerce/account/cancel-applications";
            return PostMerchantCancellationAsync<EcommerceLegacyCancelApplicationResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按旧流程商户申请单号查询注销状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476223</para>
        /// </summary>
        /// <param name="outApplyNo">提交注销申请时使用的商户申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>旧流程注销申请的审核状态及驳回原因。</returns>
        public Task<EcommerceLegacyCancelApplicationResultJson>
            QueryLegacyCancelApplicationAsync(string outApplyNo,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/ecommerce/account/cancel-applications/out-apply-no/{EscapeMerchantCancellationPath(outApplyNo)}";
            return GetMerchantCancellationAsync<EcommerceLegacyCancelApplicationResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 上传旧流程商户注销申请使用的 JPG、BMP 或 PNG 图片。
        /// <para>该接口按官方契约发送 <c>meta.file_name</c> 与 <c>meta.file_digest</c>。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012691710</para>
        /// </summary>
        /// <param name="fileName">带 JPG、BMP 或 PNG 扩展名的文件名。</param>
        /// <param name="fileStream">待上传图片流。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可用于注销或提现材料的媒体文件 ID。</returns>
        public Task<EcommerceCancellationMediaUploadResultJson>
            UploadCancelApplicationImageAsync(string fileName, Stream fileStream,
                int timeOut = Config.TIME_OUT)
        {
            return UploadCancelApplicationImageAsync(fileName, fileStream,
                CancellationToken.None, timeOut);
        }

        /// <summary>
        /// 上传旧流程商户注销申请图片，并支持取消操作。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012691710</para>
        /// </summary>
        /// <param name="fileName">带 JPG、BMP 或 PNG 扩展名的文件名。</param>
        /// <param name="fileStream">待上传图片流。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可用于注销或提现材料的媒体文件 ID。</returns>
        public Task<EcommerceCancellationMediaUploadResultJson>
            UploadCancelApplicationImageAsync(string fileName, Stream fileStream,
                CancellationToken cancellationToken, int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/ecommerce/account/cancel-applications/media";
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request
                .RequestMultipartWithFileDigestAsync<EcommerceCancellationMediaUploadResultJson>(
                    GetMerchantCancellationUrl(path), fileName, fileStream,
                    cancellationToken, timeOut);
        }

        /// <summary>
        /// 按旧流程提交已注销二级商户可用余额提现申请。
        /// <para>官方路径中的 <c>withdrawl</c> 为微信支付现行接口拼写。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012488950</para>
        /// </summary>
        /// <param name="data">出款账户、金额、收款对象、银行账户及证明材料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付提现申请单号和商户提现申请单号。</returns>
        public Task<EcommerceCancellationApplyResultJson>
            SubmitLegacyCancelWithdrawAsync(
                EcommerceLegacyCancelWithdrawRequestData data,
                int timeOut = Config.TIME_OUT)
        {
            const string path = "v3/mch_operate/risk/withdrawl-apply";
            return PostMerchantCancellationAsync<EcommerceCancellationApplyResultJson>(
                path, data, timeOut);
        }

        /// <summary>
        /// 按商户提现申请单号查询旧流程注销后提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012476164</para>
        /// </summary>
        /// <param name="outRequestNo">商户自定义的提现申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>旧流程提现申请单及付款状态。</returns>
        public Task<EcommerceLegacyCancelWithdrawQueryResultJson>
            QueryLegacyCancelWithdrawByOutRequestNoAsync(string outRequestNo,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/mch_operate/risk/withdrawl-apply/out-request-no/{EscapeMerchantCancellationPath(outRequestNo)}";
            return GetMerchantCancellationAsync<EcommerceLegacyCancelWithdrawQueryResultJson>(
                path, timeOut);
        }

        /// <summary>
        /// 按微信支付提现申请单号查询旧流程注销后提现状态。
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/partner/4012778400</para>
        /// </summary>
        /// <param name="applymentId">微信支付生成的提现申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>旧流程提现申请单及付款状态。</returns>
        public Task<EcommerceLegacyCancelWithdrawQueryResultJson>
            QueryLegacyCancelWithdrawByApplymentIdAsync(string applymentId,
                int timeOut = Config.TIME_OUT)
        {
            var path =
                $"v3/mch_operate/risk/withdrawl-apply/applyment-id/{EscapeMerchantCancellationPath(applymentId)}";
            return GetMerchantCancellationAsync<EcommerceLegacyCancelWithdrawQueryResultJson>(
                path, timeOut);
        }

        private Task<T> PostMerchantCancellationAsync<T>(string path, object data,
            int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetMerchantCancellationUrl(path), data,
                timeOut);
        }

        private Task<T> GetMerchantCancellationAsync<T>(string path, int timeOut)
            where T : Apis.Entities.ReturnJsonBase, new()
        {
            var request = new TenPayApiRequest(_tenpayV3Setting);
            return request.RequestAsync<T>(GetMerchantCancellationUrl(path), null,
                timeOut, ApiRequestMethod.GET);
        }

        private static string GetMerchantCancellationUrl(string path)
        {
            return BasePayApis.GetPayApiUrl(
                $"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}{path}");
        }

        private static string EscapeMerchantCancellationPath(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }
    }
}
