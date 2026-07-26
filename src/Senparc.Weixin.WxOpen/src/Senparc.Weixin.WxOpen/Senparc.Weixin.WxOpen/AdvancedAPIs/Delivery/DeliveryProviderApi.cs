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

    文件名：DeliveryProviderApi.cs
    文件功能描述：DeliveryProviderApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

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
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery
{
    /// <summary>
    /// 小程序物流助手运力方接口。
    /// </summary>
    /// <remarks>
    /// 本类对应官方“物流助手 / 运力方使用”目录，与商户侧 <see cref="DeliveryApi"/> 分开；官方接口不支持第三方平台代调用。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class DeliveryProviderApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 同步方法

        /// <summary>
        /// 更新商户物流账号审核结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">商户小程序、物流账号及审核结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_updatebusiness"/>。
        /// </remarks>
        public static WxJsonResult UpdateBusiness(string accessTokenOrAppId, DeliveryProviderUpdateBusinessRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/service/business/update", request, timeOut);
        }

        /// <summary>
        /// 更新普通电子面单的运单轨迹。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID 及轨迹节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_updatepath"/>。
        /// </remarks>
        public static WxJsonResult UpdatePath(string accessTokenOrAppId, DeliveryProviderUpdatePathRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/path/update", request, timeOut);
        }

        /// <summary>
        /// 使用运单和商户下单数据预览面单模板。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">运单 ID、Base64 模板、面单数据和商户下单数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>Base64 编码后的已渲染面单 HTML。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_previewtemplate"/>。
        /// </remarks>
        public static DeliveryProviderPreviewTemplateJsonResult PreviewTemplate(string accessTokenOrAppId, DeliveryProviderPreviewTemplateRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<DeliveryProviderPreviewTemplateJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/template/preview", request, timeOut);
        }

        /// <summary>
        /// 获取面单对应的发件人和收件人信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 和运单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单 ID、发件人和收件人信息。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_getcontact"/>。
        /// </remarks>
        public static DeliveryProviderGetContactJsonResult GetContact(string accessTokenOrAppId, DeliveryProviderGetContactRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<DeliveryProviderGetContactJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/contact/get", request, timeOut);
        }

        /// <summary>
        /// 由运力方取消散单订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 和取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterdeliverycancel"/>。
        /// </remarks>
        public static WxJsonResult CancelOrder(string accessTokenOrAppId, DeliveryProviderCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/cancel_order", request, timeOut);
        }

        /// <summary>
        /// 更新散单待支付运费。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID、费用明细及支付方式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 订单必须已离开待揽件状态；标准流程为先更新订单状态，再更新运费。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdateorderfee"/>。
        /// </remarks>
        public static WxJsonResult UpdateOrderFee(string accessTokenOrAppId, DeliveryProviderUpdateOrderFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/fee", request, timeOut);
        }

        /// <summary>
        /// 对散单订单发起全额或部分退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 及退款金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterdeliveryrefund"/>。
        /// </remarks>
        public static WxJsonResult RefundOrder(string accessTokenOrAppId, DeliveryProviderRefundOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/refund_order", request, timeOut);
        }

        /// <summary>
        /// 下载运力方散单对账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">对账日期及账单类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功时 <see cref="DeliveryProviderGetBillJsonResult.content"/> 为对账文件文本；失败时返回微信错误码和原始错误 JSON。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatter_get_bill"/>。
        /// </remarks>
        public static DeliveryProviderGetBillJsonResult GetBill(string accessTokenOrAppId, DeliveryProviderGetBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken => GetBillCore(accessToken, request, timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 返回用户物流投诉处理结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID 及投诉处理结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// AccessToken 通过查询参数发送；官方请求体参数表重复列出的 AccessToken 无需由调用方再次填写。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdatecomplainresult"/>。
        /// </remarks>
        public static WxJsonResult UpdateComplaintResult(string accessTokenOrAppId, DeliveryProviderUpdateComplaintResultRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/scatter/update_complaint_result", request, timeOut);
        }

        /// <summary>
        /// 更新散单订单的揽件、运输、派送或异常状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单、状态节点及取派件员信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdateorderstatus"/>。
        /// </remarks>
        public static WxJsonResult UpdateOrderStatus(string accessTokenOrAppId, DeliveryProviderUpdateOrderStatusRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/update", request, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步更新商户物流账号审核结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">商户小程序、物流账号及审核结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_updatebusiness"/>。
        /// </remarks>
        public static Task<WxJsonResult> UpdateBusinessAsync(string accessTokenOrAppId, DeliveryProviderUpdateBusinessRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/service/business/update", request, timeOut);
        }

        /// <summary>
        /// 异步更新普通电子面单的运单轨迹。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID 及轨迹节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_updatepath"/>。
        /// </remarks>
        public static Task<WxJsonResult> UpdatePathAsync(string accessTokenOrAppId, DeliveryProviderUpdatePathRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/path/update", request, timeOut);
        }

        /// <summary>
        /// 异步使用运单和商户下单数据预览面单模板。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">运单 ID、Base64 模板、面单数据和商户下单数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>Base64 编码后的已渲染面单 HTML。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_previewtemplate"/>。
        /// </remarks>
        public static Task<DeliveryProviderPreviewTemplateJsonResult> PreviewTemplateAsync(string accessTokenOrAppId, DeliveryProviderPreviewTemplateRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<DeliveryProviderPreviewTemplateJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/template/preview", request, timeOut);
        }

        /// <summary>
        /// 异步获取面单对应的发件人和收件人信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 和运单 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>运单 ID、发件人和收件人信息。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_getcontact"/>。
        /// </remarks>
        public static Task<DeliveryProviderGetContactJsonResult> GetContactAsync(string accessTokenOrAppId, DeliveryProviderGetContactRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<DeliveryProviderGetContactJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/contact/get", request, timeOut);
        }

        /// <summary>
        /// 异步由运力方取消散单订单。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 和取消原因。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterdeliverycancel"/>。
        /// </remarks>
        public static Task<WxJsonResult> CancelOrderAsync(string accessTokenOrAppId, DeliveryProviderCancelOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/cancel_order", request, timeOut);
        }

        /// <summary>
        /// 异步更新散单待支付运费。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID、费用明细及支付方式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 订单必须已离开待揽件状态；标准流程为先更新订单状态，再更新运费。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdateorderfee"/>。
        /// </remarks>
        public static Task<WxJsonResult> UpdateOrderFeeAsync(string accessTokenOrAppId, DeliveryProviderUpdateOrderFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/fee", request, timeOut);
        }

        /// <summary>
        /// 异步对散单订单发起全额或部分退款。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token 及退款金额。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterdeliveryrefund"/>。
        /// </remarks>
        public static Task<WxJsonResult> RefundOrderAsync(string accessTokenOrAppId, DeliveryProviderRefundOrderRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/refund_order", request, timeOut);
        }

        /// <summary>
        /// 异步下载运力方散单对账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">对账日期及账单类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功时 <see cref="DeliveryProviderGetBillJsonResult.content"/> 为对账文件文本；失败时返回微信错误码和原始错误 JSON。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatter_get_bill"/>。
        /// </remarks>
        public static Task<DeliveryProviderGetBillJsonResult> GetBillAsync(string accessTokenOrAppId, DeliveryProviderGetBillRequest request, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken => GetBillCoreAsync(accessToken, request, timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 异步返回用户物流投诉处理结果。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单 ID 及投诉处理结果。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// AccessToken 通过查询参数发送；官方请求体参数表重复列出的 AccessToken 无需由调用方再次填写。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdatecomplainresult"/>。
        /// </remarks>
        public static Task<WxJsonResult> UpdateComplaintResultAsync(string accessTokenOrAppId, DeliveryProviderUpdateComplaintResultRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/scatter/update_complaint_result", request, timeOut);
        }

        /// <summary>
        /// 异步更新散单订单的揽件、运输、派送或异常状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">运力方小程序 AccessToken 或已注册的 AppId。</param>
        /// <param name="request">下单 Token、运单、状态节点及取派件员信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/express/express-by-provider/api_scatterupdateorderstatus"/>。
        /// </remarks>
        public static Task<WxJsonResult> UpdateOrderStatusAsync(string accessTokenOrAppId, DeliveryProviderUpdateOrderStatusRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/cgi-bin/express/delivery/single_waybill/update", request, timeOut);
        }

        #endregion

        private static T Send<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return CommonJsonSend.Send<T>(accessToken, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendAsync<T>(string accessTokenOrAppId, string path, object request, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token={0}";
                return await CommonJsonSend.SendAsync<T>(accessToken, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }

        private static DeliveryProviderGetBillJsonResult GetBillCore(string accessToken, DeliveryProviderGetBillRequest request, int timeOut)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/express/delivery/single_waybill/get_bill?access_token={0}", accessToken.AsUrlData());
            var json = SerializerHelper.GetJsonString(request, IgnoreNullJsonSetting);
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream(bytes))
            {
                var responseText = RequestUtility.HttpPost(CommonDI.CommonSP, url, null, stream,
                    encoding: Encoding.UTF8,
                    timeOut: timeOut,
                    contentType: "application/json; charset=utf-8");
                return ParseGetBillResponse(responseText);
            }
        }

        private static async Task<DeliveryProviderGetBillJsonResult> GetBillCoreAsync(string accessToken, DeliveryProviderGetBillRequest request, int timeOut)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/express/delivery/single_waybill/get_bill?access_token={0}", accessToken.AsUrlData());
            var json = SerializerHelper.GetJsonString(request, IgnoreNullJsonSetting);
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream(bytes))
            {
                var responseText = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, url, null, stream,
                    encoding: Encoding.UTF8,
                    timeOut: timeOut,
                    contentType: "application/json; charset=utf-8").ConfigureAwait(false);
                return ParseGetBillResponse(responseText);
            }
        }

        private static DeliveryProviderGetBillJsonResult ParseGetBillResponse(string responseText)
        {
            if (!string.IsNullOrWhiteSpace(responseText) && responseText.TrimStart().StartsWith("{"))
            {
                var result = Senparc.Weixin.HttpUtility.Post.GetResult<DeliveryProviderGetBillJsonResult>(responseText);
                result.content = responseText;
                return result;
            }

            return new DeliveryProviderGetBillJsonResult
            {
                content = responseText
            };
        }
    }
}
