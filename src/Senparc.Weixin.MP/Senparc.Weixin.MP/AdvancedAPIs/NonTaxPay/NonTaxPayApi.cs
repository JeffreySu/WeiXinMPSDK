#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：NonTaxPayApi.cs
    文件功能描述：NonTaxPayApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.NonTaxPay
{
    /// <summary>
    /// 微信非税缴费接口。
    /// </summary>
    /// <remarks>
    /// 本能力仅面向已开通微信非税缴费能力的认证账号，且官方接口不支持第三方平台代调用。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class NonTaxPayApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 同步方法

        /// <summary>
        /// 查询缴费通知书对应的应收信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">缴费通知书、执收单位和行政区划等查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应缴总额、执收单位及缴费子项目。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxqueryfee"/>。
        /// </remarks>
        public static NonTaxQueryFeeJsonResult QueryFee(string accessTokenOrAppId, NonTaxQueryFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxQueryFeeJsonResult>(accessTokenOrAppId, "/nontax/queryfee", request, timeOut);
        }

        /// <summary>
        /// 创建非税缴费支付订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">缴费金额、项目、执收单位和支付场景等下单信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信非税缴费下单结果。</returns>
        /// <remarks>
        /// <paramref name="request"/> 中的缴费通知书编号与业务订单号必须二选一。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxunifiedorder"/>。
        /// </remarks>
        public static NonTaxUnifiedOrderJsonResult UnifiedOrder(string accessTokenOrAppId, NonTaxUnifiedOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxUnifiedOrderJsonResult>(accessTokenOrAppId, "/nontax/unifiedorder", request, timeOut);
        }

        /// <summary>
        /// 下载指定日期的非税缴费对账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">商户号、账单日期及账单类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功时 <see cref="NonTaxDownloadBillJsonResult.content"/> 为 CSV 文本；失败时返回微信错误码和原始错误 JSON。</returns>
        /// <remarks>
        /// 官方建议每天早上 6 点后拉取前一日对账单。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxdownloadbill"/>。
        /// </remarks>
        public static NonTaxDownloadBillJsonResult DownloadBill(string accessTokenOrAppId, NonTaxDownloadBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken => DownloadBillCore(accessToken, request, timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 触发微信重新发送指定不一致订单的支付结果通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">需要重新通知的微信非税缴费订单。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 本方法适用于对账单存在订单、业务系统却未收到支付通知的场景；调用后由微信重新请求业务通知地址。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxnotifyinconsistentorder"/>。
        /// </remarks>
        public static WxJsonResult NotifyInconsistentOrder(string accessTokenOrAppId, NonTaxNotifyInconsistentOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/nontax/notifyinconsistentorder", request, timeOut);
        }

        /// <summary>
        /// 触发微信向指定地址发送模拟支付结果通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">接收测试通知的地址及协议版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 协议版本为 1 时，微信会分别发送用于验证加解密和验签逻辑的测试通知。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmocknotification"/>。
        /// </remarks>
        public static WxJsonResult MockNotification(string accessTokenOrAppId, NonTaxMockRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/nontax/mocknotification", request, timeOut);
        }

        /// <summary>
        /// 触发微信向指定地址发送模拟应收查询请求。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">接收测试查询请求的地址及协议版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 协议版本为 1 时，微信会分别发送用于验证加解密和验签逻辑的测试请求。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmockqueryfee"/>。
        /// </remarks>
        public static WxJsonResult MockQueryFee(string accessTokenOrAppId, NonTaxMockRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/nontax/mockqueryfee", request, timeOut);
        }

        /// <summary>
        /// 提交非税缴费刷卡支付并同步获取支付状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">付款码、金额、项目及执收单位等支付信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信非税缴费订单号及执行结果。</returns>
        /// <remarks>
        /// 返回系统失败时可在 5 秒后重试；返回用户支付中时，官方建议每隔约 10 秒查询，最长等待约 30 秒。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmicropay"/>。
        /// </remarks>
        public static NonTaxMicroPayJsonResult MicroPay(string accessTokenOrAppId, NonTaxMicroPayRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxMicroPayJsonResult>(accessTokenOrAppId, "/nontax/micropay", request, timeOut);
        }

        /// <summary>
        /// 获取指定缴费通知书或业务订单对应的非税缴费订单列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">行政区划、执收单位及缴费通知书或业务订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已创建订单列表和已支付订单号。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxgetorderlist"/>。
        /// </remarks>
        public static NonTaxGetOrderListJsonResult GetOrderList(string accessTokenOrAppId, NonTaxGetOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxGetOrderListJsonResult>(accessTokenOrAppId, "/nontax/getorderlist", request, timeOut);
        }

        /// <summary>
        /// 申请非税缴费订单全额或部分退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">微信订单号、退款原因及可选的部分退款信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信退款订单号及执行结果。</returns>
        /// <remarks>
        /// 部分退款时必须同时填写退款金额和调用方唯一退款单号。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxrefund"/>。
        /// </remarks>
        public static NonTaxRefundJsonResult Refund(string accessTokenOrAppId, NonTaxRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxRefundJsonResult>(accessTokenOrAppId, "/nontax/refund", request, timeOut);
        }

        /// <summary>
        /// 获取非税缴费订单详情、退款信息和通知历史。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">AppId、微信非税缴费订单号及可选服务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单、缴费项目、退款及通知详情。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxgetorder"/>。
        /// </remarks>
        public static NonTaxGetOrderJsonResult GetOrder(string accessTokenOrAppId, NonTaxGetOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NonTaxGetOrderJsonResult>(accessTokenOrAppId, "/nontax/getorder", request, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步查询缴费通知书对应的应收信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">缴费通知书、执收单位和行政区划等查询条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应缴总额、执收单位及缴费子项目。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxqueryfee"/>。
        /// </remarks>
        public static Task<NonTaxQueryFeeJsonResult> QueryFeeAsync(string accessTokenOrAppId, NonTaxQueryFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxQueryFeeJsonResult>(accessTokenOrAppId, "/nontax/queryfee", request, timeOut);
        }

        /// <summary>
        /// 异步创建非税缴费支付订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">缴费金额、项目、执收单位和支付场景等下单信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信非税缴费下单结果。</returns>
        /// <remarks>
        /// <paramref name="request"/> 中的缴费通知书编号与业务订单号必须二选一。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxunifiedorder"/>。
        /// </remarks>
        public static Task<NonTaxUnifiedOrderJsonResult> UnifiedOrderAsync(string accessTokenOrAppId, NonTaxUnifiedOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxUnifiedOrderJsonResult>(accessTokenOrAppId, "/nontax/unifiedorder", request, timeOut);
        }

        /// <summary>
        /// 异步下载指定日期的非税缴费对账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">商户号、账单日期及账单类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功时 <see cref="NonTaxDownloadBillJsonResult.content"/> 为 CSV 文本；失败时返回微信错误码和原始错误 JSON。</returns>
        /// <remarks>
        /// 官方建议每天早上 6 点后拉取前一日对账单。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxdownloadbill"/>。
        /// </remarks>
        public static Task<NonTaxDownloadBillJsonResult> DownloadBillAsync(string accessTokenOrAppId, NonTaxDownloadBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(accessToken => DownloadBillCoreAsync(accessToken, request, timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 异步触发微信重新发送指定不一致订单的支付结果通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">需要重新通知的微信非税缴费订单。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 本方法适用于对账单存在订单、业务系统却未收到支付通知的场景；调用后由微信重新请求业务通知地址。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxnotifyinconsistentorder"/>。
        /// </remarks>
        public static Task<WxJsonResult> NotifyInconsistentOrderAsync(string accessTokenOrAppId, NonTaxNotifyInconsistentOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/nontax/notifyinconsistentorder", request, timeOut);
        }

        /// <summary>
        /// 异步触发微信向指定地址发送模拟支付结果通知。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">接收测试通知的地址及协议版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 协议版本为 1 时，微信会分别发送用于验证加解密和验签逻辑的测试通知。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmocknotification"/>。
        /// </remarks>
        public static Task<WxJsonResult> MockNotificationAsync(string accessTokenOrAppId, NonTaxMockRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/nontax/mocknotification", request, timeOut);
        }

        /// <summary>
        /// 异步触发微信向指定地址发送模拟应收查询请求。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">接收测试查询请求的地址及协议版本。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 协议版本为 1 时，微信会分别发送用于验证加解密和验签逻辑的测试请求。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmockqueryfee"/>。
        /// </remarks>
        public static Task<WxJsonResult> MockQueryFeeAsync(string accessTokenOrAppId, NonTaxMockRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/nontax/mockqueryfee", request, timeOut);
        }

        /// <summary>
        /// 异步提交非税缴费刷卡支付并获取支付状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">付款码、金额、项目及执收单位等支付信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信非税缴费订单号及执行结果。</returns>
        /// <remarks>
        /// 返回系统失败时可在 5 秒后重试；返回用户支付中时，官方建议每隔约 10 秒查询，最长等待约 30 秒。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxmicropay"/>。
        /// </remarks>
        public static Task<NonTaxMicroPayJsonResult> MicroPayAsync(string accessTokenOrAppId, NonTaxMicroPayRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxMicroPayJsonResult>(accessTokenOrAppId, "/nontax/micropay", request, timeOut);
        }

        /// <summary>
        /// 异步获取指定缴费通知书或业务订单对应的非税缴费订单列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">行政区划、执收单位及缴费通知书或业务订单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>已创建订单列表和已支付订单号。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxgetorderlist"/>。
        /// </remarks>
        public static Task<NonTaxGetOrderListJsonResult> GetOrderListAsync(string accessTokenOrAppId, NonTaxGetOrderListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxGetOrderListJsonResult>(accessTokenOrAppId, "/nontax/getorderlist", request, timeOut);
        }

        /// <summary>
        /// 异步申请非税缴费订单全额或部分退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">微信订单号、退款原因及可选的部分退款信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信退款订单号及执行结果。</returns>
        /// <remarks>
        /// 部分退款时必须同时填写退款金额和调用方唯一退款单号。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxrefund"/>。
        /// </remarks>
        public static Task<NonTaxRefundJsonResult> RefundAsync(string accessTokenOrAppId, NonTaxRefundRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxRefundJsonResult>(accessTokenOrAppId, "/nontax/refund", request, timeOut);
        }

        /// <summary>
        /// 异步获取非税缴费订单详情、退款信息和通知历史。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">AppId、微信非税缴费订单号及可选服务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>订单、缴费项目、退款及通知详情。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/nontaxpay/api_nontaxgetorder"/>。
        /// </remarks>
        public static Task<NonTaxGetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, NonTaxGetOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NonTaxGetOrderJsonResult>(accessTokenOrAppId, "/nontax/getorder", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + path + "?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<T>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + path + "?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<T>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }

        private static NonTaxDownloadBillJsonResult DownloadBillCore(string accessToken, NonTaxDownloadBillRequest request, int timeOut)
        {
            var url = string.Format(Config.ApiMpHost + "/nontax/downloadbill?access_token={0}", accessToken.AsUrlData());
            var json = SerializerHelper.GetJsonString(request, IgnoreNullJsonSetting);
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream(bytes))
            {
                var responseText = RequestUtility.HttpPost(CommonDI.CommonSP, url, null, stream,
                    encoding: Encoding.UTF8,
                    timeOut: timeOut,
                    contentType: "application/json; charset=utf-8");
                return ParseDownloadBillResponse(responseText);
            }
        }

        private static async Task<NonTaxDownloadBillJsonResult> DownloadBillCoreAsync(string accessToken, NonTaxDownloadBillRequest request, int timeOut)
        {
            var url = string.Format(Config.ApiMpHost + "/nontax/downloadbill?access_token={0}", accessToken.AsUrlData());
            var json = SerializerHelper.GetJsonString(request, IgnoreNullJsonSetting);
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream(bytes))
            {
                var responseText = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, url, null, stream,
                    encoding: Encoding.UTF8,
                    timeOut: timeOut,
                    contentType: "application/json; charset=utf-8").ConfigureAwait(false);
                return ParseDownloadBillResponse(responseText);
            }
        }

        private static NonTaxDownloadBillJsonResult ParseDownloadBillResponse(string responseText)
        {
            if (!string.IsNullOrWhiteSpace(responseText) && responseText.TrimStart().StartsWith("{"))
            {
                var result = Senparc.Weixin.HttpUtility.Post.GetResult<NonTaxDownloadBillJsonResult>(responseText);
                result.content = responseText;
                return result;
            }

            return new NonTaxDownloadBillJsonResult
            {
                content = responseText
            };
        }
    }
}
