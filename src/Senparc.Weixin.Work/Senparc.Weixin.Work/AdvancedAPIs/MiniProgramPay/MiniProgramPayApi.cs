/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MiniProgramPayApi.cs
    文件功能描述：企业微信小程序对外收款接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐企业微信小程序对外收款接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MiniProgramPay
{
    /// <summary>
    /// 企业微信小程序对外收款接口。
    /// </summary>
    public static class MiniProgramPayApi
    {
        private const string UploadImagePath = "/cgi-bin/miniapppay/upload_image";
        private const string ApplyMerchantPath = "/cgi-bin/miniapppay/apply_mch";
        private const string GetApplymentStatusPath = "/cgi-bin/miniapppay/get_applyment_status";
        private const string CreateOrderPath = "/cgi-bin/miniapppay/create_order";
        private const string GetOrderPath = "/cgi-bin/miniapppay/get_order";
        private const string CloseOrderPath = "/cgi-bin/miniapppay/close_order";
        private const string GetPaySignPath = "/cgi-bin/miniapppay/get_sign";
        private const string RefundPath = "/cgi-bin/miniapppay/refund";
        private const string GetRefundDetailPath = "/cgi-bin/miniapppay/get_refund_detail";
        private const string GetBillPath = "/cgi-bin/miniapppay/get_bill";

        /// <summary>
        /// 上传开户申请所需图片。图片 ID 有效期以企业微信官方规则为准。
        /// </summary>
        /// <param name="accessTokenOrAppKey">“对外收款”应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="imageFilePath">图片文件的本地完整路径。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        public static UploadMiniProgramPayImageResult UploadImage(string accessTokenOrAppKey,
            string imageFilePath, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}{UploadImagePath}?access_token={accessToken.AsUrlData()}";
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = imageFilePath
                };
                return CO2NET.HttpUtility.Post.PostFileGetJson<UploadMiniProgramPayImageResult>(
                    CommonDI.CommonSP, url, null,
                    files, null, timeOut: timeOut);
            }, accessTokenOrAppKey);

        /// <summary>
        /// 异步上传开户申请所需图片。
        /// </summary>
        public static Task<UploadMiniProgramPayImageResult> UploadImageAsync(string accessTokenOrAppKey,
            string imageFilePath, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}{UploadImagePath}?access_token={accessToken.AsUrlData()}";
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = imageFilePath
                };
                return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadMiniProgramPayImageResult>(
                    CommonDI.CommonSP, url, null, files, null, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);

        /// <summary>
        /// 提交创建对外收款账户的申请单。
        /// </summary>
        public static WorkJsonResult ApplyMerchant(string accessTokenOrAppKey,
            ApplyMiniProgramPayMerchantRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ApplyMerchantPath, request, timeOut);

        /// <summary>
        /// 异步提交创建对外收款账户的申请单。
        /// </summary>
        public static Task<WorkJsonResult> ApplyMerchantAsync(string accessTokenOrAppKey,
            ApplyMiniProgramPayMerchantRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ApplyMerchantPath, request, timeOut);

        /// <summary>
        /// 查询对外收款账户申请单状态。
        /// </summary>
        public static GetMiniProgramPayApplymentStatusResult GetApplymentStatus(string accessTokenOrAppKey,
            GetMiniProgramPayApplymentStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMiniProgramPayApplymentStatusResult>(accessTokenOrAppKey,
                GetApplymentStatusPath, request, timeOut);

        /// <summary>
        /// 异步查询对外收款账户申请单状态。
        /// </summary>
        public static Task<GetMiniProgramPayApplymentStatusResult> GetApplymentStatusAsync(
            string accessTokenOrAppKey, GetMiniProgramPayApplymentStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMiniProgramPayApplymentStatusResult>(accessTokenOrAppKey,
                GetApplymentStatusPath, request, timeOut);

        /// <summary>
        /// 创建小程序预支付订单。
        /// </summary>
        public static CreateMiniProgramPayOrderResult CreateOrder(string accessTokenOrAppKey,
            CreateMiniProgramPayOrderRequest request, int timeOut = Config.TIME_OUT)
            => Post<CreateMiniProgramPayOrderResult>(accessTokenOrAppKey, CreateOrderPath, request, timeOut);

        /// <summary>
        /// 异步创建小程序预支付订单。
        /// </summary>
        public static Task<CreateMiniProgramPayOrderResult> CreateOrderAsync(string accessTokenOrAppKey,
            CreateMiniProgramPayOrderRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CreateMiniProgramPayOrderResult>(accessTokenOrAppKey, CreateOrderPath, request, timeOut);

        /// <summary>
        /// 按商户订单号查询订单。
        /// </summary>
        public static GetMiniProgramPayOrderResult GetOrder(string accessTokenOrAppKey,
            MiniProgramPayOrderIdentity request, int timeOut = Config.TIME_OUT)
            => Post<GetMiniProgramPayOrderResult>(accessTokenOrAppKey, GetOrderPath, request, timeOut);

        /// <summary>
        /// 异步按商户订单号查询订单。
        /// </summary>
        public static Task<GetMiniProgramPayOrderResult> GetOrderAsync(string accessTokenOrAppKey,
            MiniProgramPayOrderIdentity request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMiniProgramPayOrderResult>(accessTokenOrAppKey, GetOrderPath, request, timeOut);

        /// <summary>
        /// 关闭未支付订单。
        /// </summary>
        public static WorkJsonResult CloseOrder(string accessTokenOrAppKey,
            MiniProgramPayOrderIdentity request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CloseOrderPath, request, timeOut);

        /// <summary>
        /// 异步关闭未支付订单。
        /// </summary>
        public static Task<WorkJsonResult> CloseOrderAsync(string accessTokenOrAppKey,
            MiniProgramPayOrderIdentity request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CloseOrderPath, request, timeOut);

        /// <summary>
        /// 获取小程序调起支付所需签名。
        /// </summary>
        public static GetMiniProgramPaySignResult GetPaySign(string accessTokenOrAppKey,
            GetMiniProgramPaySignRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMiniProgramPaySignResult>(accessTokenOrAppKey, GetPaySignPath, request, timeOut);

        /// <summary>
        /// 异步获取小程序调起支付所需签名。
        /// </summary>
        public static Task<GetMiniProgramPaySignResult> GetPaySignAsync(string accessTokenOrAppKey,
            GetMiniProgramPaySignRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMiniProgramPaySignResult>(accessTokenOrAppKey, GetPaySignPath, request, timeOut);

        /// <summary>
        /// 申请退款。
        /// </summary>
        public static MiniProgramPayRefundResult Refund(string accessTokenOrAppKey,
            MiniProgramPayRefundRequest request, int timeOut = Config.TIME_OUT)
            => Post<MiniProgramPayRefundResult>(accessTokenOrAppKey, RefundPath, request, timeOut);

        /// <summary>
        /// 异步申请退款。
        /// </summary>
        public static Task<MiniProgramPayRefundResult> RefundAsync(string accessTokenOrAppKey,
            MiniProgramPayRefundRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<MiniProgramPayRefundResult>(accessTokenOrAppKey, RefundPath, request, timeOut);

        /// <summary>
        /// 查询退款详情。
        /// </summary>
        public static GetMiniProgramPayRefundDetailResult GetRefundDetail(string accessTokenOrAppKey,
            MiniProgramPayRefundIdentity request, int timeOut = Config.TIME_OUT)
            => Post<GetMiniProgramPayRefundDetailResult>(accessTokenOrAppKey,
                GetRefundDetailPath, request, timeOut);

        /// <summary>
        /// 异步查询退款详情。
        /// </summary>
        public static Task<GetMiniProgramPayRefundDetailResult> GetRefundDetailAsync(
            string accessTokenOrAppKey, MiniProgramPayRefundIdentity request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMiniProgramPayRefundDetailResult>(accessTokenOrAppKey,
                GetRefundDetailPath, request, timeOut);

        /// <summary>
        /// 申请指定日期的交易账单下载信息。
        /// </summary>
        public static GetMiniProgramPayBillResult GetBill(string accessTokenOrAppKey,
            GetMiniProgramPayBillRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<GetMiniProgramPayBillResult>(
                null, BuildBillUrl(accessToken, request), null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 异步申请指定日期的交易账单下载信息。
        /// </summary>
        public static Task<GetMiniProgramPayBillResult> GetBillAsync(string accessTokenOrAppKey,
            GetMiniProgramPayBillRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetMiniProgramPayBillResult>(null,
                    BuildBillUrl(accessToken, request), null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        private static string BuildBillUrl(string accessToken, GetMiniProgramPayBillRequest request)
        {
            var url = new StringBuilder(Config.ApiWorkHost)
                .Append(GetBillPath)
                .Append("?access_token=").Append(accessToken.AsUrlData())
                .Append("&mchid=").Append(request.mchid.AsUrlData())
                .Append("&bill_date=").Append(request.bill_date.AsUrlData());

            if (!string.IsNullOrEmpty(request.bill_type))
            {
                url.Append("&bill_type=").Append(request.bill_type.AsUrlData());
            }

            if (!string.IsNullOrEmpty(request.tar_type))
            {
                url.Append("&tar_type=").Append(request.tar_type.AsUrlData());
            }

            return url.ToString();
        }

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
