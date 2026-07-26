#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：XPayIncrementApi.cs
    文件功能描述：XPayIncrementApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 小程序虚拟支付增量接口。
    /// </summary>
    public partial class XPayApi
    {
        private static readonly JsonSetting XPayIncrementIgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        /// <summary>
        /// 使用正确拼写的入口绑定广告金充值账户。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="data">充值账户及小程序环境信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_bind_transfer_accout"/>。保留原有 <see cref="BindTransferAccout"/> 方法用于兼容；本接口支持第三方平台代商家调用。</remarks>
        public static WxJsonResult BindTransferAccount(string accessTokenOrAppId, BindTransferAccoutRequestData data, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrement<WxJsonResult>(accessTokenOrAppId, "/xpay/bind_transfer_accout", null, data, timeOut);
        }

        /// <summary>
        /// 异步使用正确拼写的入口绑定广告金充值账户。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="data">充值账户及小程序环境信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_bind_transfer_accout"/>。保留原有 <see cref="BindTransferAccoutAsync"/> 方法用于兼容；本接口支持第三方平台代商家调用。</remarks>
        public static Task<WxJsonResult> BindTransferAccountAsync(string accessTokenOrAppId, BindTransferAccoutRequestData data, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrementAsync<WxJsonResult>(accessTokenOrAppId, "/xpay/bind_transfer_accout", null, data, timeOut);
        }

        /// <summary>
        /// 下载指定月份范围内的虚拟支付 iOS 月结账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="data">开始月份和结束月份，格式均为 YYYYMM。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>月结账单列表及临时下载链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_download_ios_settlement_bill"/>。本接口支持第三方平台代商家调用。</remarks>
        public static DownloadIosSettlementBillJsonResult DownloadIosSettlementBill(string accessTokenOrAppId, string pay_sig, DownloadIosSettlementBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrement<DownloadIosSettlementBillJsonResult>(accessTokenOrAppId, "/xpay/download_ios_settlement_bill", pay_sig, data, timeOut);
        }

        /// <summary>
        /// 异步下载指定月份范围内的虚拟支付 iOS 月结账单。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="data">开始月份和结束月份，格式均为 YYYYMM。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>月结账单列表及临时下载链接。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_download_ios_settlement_bill"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<DownloadIosSettlementBillJsonResult> DownloadIosSettlementBillAsync(string accessTokenOrAppId, string pay_sig, DownloadIosSettlementBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrementAsync<DownloadIosSettlementBillJsonResult>(accessTokenOrAppId, "/xpay/download_ios_settlement_bill", pay_sig, data, timeOut);
        }

        /// <summary>
        /// 查询虚拟支付商户被管控的能力、原因和解除路径。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户管控能力列表及对应的解除说明。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_query_punishment_reasons"/>。本接口支持第三方平台代商家调用。</remarks>
        public static QueryPunishmentReasonsJsonResult QueryPunishmentReasons(string accessTokenOrAppId, string pay_sig, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrement<QueryPunishmentReasonsJsonResult>(accessTokenOrAppId, "/xpay/query_punishment_reasons", pay_sig, new { }, timeOut);
        }

        /// <summary>
        /// 异步查询虚拟支付商户被管控的能力、原因和解除路径。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户管控能力列表及对应的解除说明。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_query_punishment_reasons"/>。本接口支持第三方平台代商家调用。</remarks>
        public static Task<QueryPunishmentReasonsJsonResult> QueryPunishmentReasonsAsync(string accessTokenOrAppId, string pay_sig, int timeOut = Config.TIME_OUT)
        {
            return SendXPayIncrementAsync<QueryPunishmentReasonsJsonResult>(accessTokenOrAppId, "/xpay/query_punishment_reasons", pay_sig, new { }, timeOut);
        }

        private static T SendXPayIncrement<T>(string accessTokenOrAppId, string path, string paySig, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
                if (!string.IsNullOrEmpty(paySig))
                {
                    url += "&pay_sig=" + paySig.AsUrlData();
                }

                return CommonJsonSend.Send<T>(null, url, data, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: XPayIncrementIgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        private static Task<T> SendXPayIncrementAsync<T>(string accessTokenOrAppId, string path, string paySig, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiMpHost + path + "?access_token=" + accessToken.AsUrlData();
                if (!string.IsNullOrEmpty(paySig))
                {
                    url += "&pay_sig=" + paySig.AsUrlData();
                }

                return await CommonJsonSend.SendAsync<T>(null, url, data, CommonJsonSendType.POST,
                    timeOut: timeOut, jsonSetting: XPayIncrementIgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId);
        }
    }
}
