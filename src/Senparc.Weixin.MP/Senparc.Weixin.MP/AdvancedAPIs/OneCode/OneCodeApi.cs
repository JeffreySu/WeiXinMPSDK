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

    文件名：OneCodeApi.cs
    文件功能描述：OneCodeApi 微信接口封装


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.OneCode
{
    /// <summary>
    /// 微信“一物一码”营销码接口。
    /// </summary>
    /// <remarks>
    /// 本能力仅支持已申请开通的服务号；AccessToken 接口同时支持第三方平台使用 authorizer_access_token 代商家调用。
    /// </remarks>
    [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, true)]
    public static class OneCodeApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(ignoreNulls: true);

        #region 同步方法

        /// <summary>
        /// 查询二维码申请单状态及详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请单查询条件，可按申请单号或外部单号查询。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单状态、码段及创建更新时间。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycodequery"/>。
        /// </remarks>
        public static ApplyCodeQueryJsonResult ApplyCodeQuery(string accessTokenOrAppId, ApplyCodeQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycodequery?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<ApplyCodeQueryJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载已生成的二维码数据包。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请单号及需要下载的码段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>包含 Base64 编码文件内容的下载结果。</returns>
        /// <remarks>
        /// 下载前申请单状态必须为 <c>FINISH</c>。返回的 <c>buffer</c> 需先进行 Base64 解码，再按官方规则解密。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycodedownload"/>。
        /// </remarks>
        public static ApplyCodeDownloadJsonResult ApplyCodeDownload(string accessTokenOrAppId, ApplyCodeDownloadRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycodedownload?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<ApplyCodeDownloadJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 激活指定范围的二维码并关联营销活动及小程序页面。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">二维码码段、商品、活动及小程序配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_codeactive"/>。
        /// </remarks>
        public static WxJsonResult CodeActive(string accessTokenOrAppId, CodeActiveRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/codeactive?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询二维码激活状态及关联信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">按申请单号和码偏移量、28 位普通码字符或 9 位原始码填写一种查询方式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码原始码及关联的营销活动信息。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_codeactivequery"/>。
        /// </remarks>
        public static CodeActiveQueryJsonResult CodeActiveQuery(string accessTokenOrAppId, CodeActiveQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/codeactivequery?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CodeActiveQueryJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 将用户扫码获得的 CODE_TICKET 换取正式营销码。
        /// </summary>
        /// <param name="request">用户 OpenId 和扫码跳转参数中的 code_ticket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码原始码及关联的营销活动信息。</returns>
        /// <remarks>
        /// 官方请求路径不包含 AccessToken，因此本方法不要求 AccessToken 或 AppId。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_tickettocode"/>。
        /// </remarks>
        public static TicketToCodeJsonResult TicketToCode(TicketToCodeRequest request, int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/intp/marketcode/tickettocode";
            return CommonJsonSend.Send<TicketToCodeJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>
        /// 批量申请一物一码营销二维码。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请数量和调用方外部单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信生成的申请单号。</returns>
        /// <remarks>
        /// 申请数量必须为 10000 的整数倍，范围为 10000 至 20000000；相同外部单号视为同一申请单。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycode"/>。
        /// </remarks>
        public static ApplyCodeJsonResult ApplyCode(string accessTokenOrAppId, ApplyCodeRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycode?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<ApplyCodeJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步查询二维码申请单状态及详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请单查询条件，可按申请单号或外部单号查询。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单状态、码段及创建更新时间。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycodequery"/>。
        /// </remarks>
        public static async Task<ApplyCodeQueryJsonResult> ApplyCodeQueryAsync(string accessTokenOrAppId, ApplyCodeQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycodequery?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<ApplyCodeQueryJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步下载已生成的二维码数据包。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请单号及需要下载的码段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>包含 Base64 编码文件内容的下载结果。</returns>
        /// <remarks>
        /// 下载前申请单状态必须为 <c>FINISH</c>。返回的 <c>buffer</c> 需先进行 Base64 解码，再按官方规则解密。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycodedownload"/>。
        /// </remarks>
        public static async Task<ApplyCodeDownloadJsonResult> ApplyCodeDownloadAsync(string accessTokenOrAppId, ApplyCodeDownloadRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycodedownload?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<ApplyCodeDownloadJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步激活指定范围的二维码并关联营销活动及小程序页面。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">二维码码段、商品、活动及小程序配置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口执行结果。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_codeactive"/>。
        /// </remarks>
        public static async Task<WxJsonResult> CodeActiveAsync(string accessTokenOrAppId, CodeActiveRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/codeactive?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步查询二维码激活状态及关联信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">按申请单号和码偏移量、28 位普通码字符或 9 位原始码填写一种查询方式。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码原始码及关联的营销活动信息。</returns>
        /// <remarks>
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_codeactivequery"/>。
        /// </remarks>
        public static async Task<CodeActiveQueryJsonResult> CodeActiveQueryAsync(string accessTokenOrAppId, CodeActiveQueryRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/codeactivequery?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<CodeActiveQueryJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步将用户扫码获得的 CODE_TICKET 换取正式营销码。
        /// </summary>
        /// <param name="request">用户 OpenId 和扫码跳转参数中的 code_ticket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二维码原始码及关联的营销活动信息。</returns>
        /// <remarks>
        /// 官方请求路径不包含 AccessToken，因此本方法不要求 AccessToken 或 AppId。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_tickettocode"/>。
        /// </remarks>
        public static Task<TicketToCodeJsonResult> TicketToCodeAsync(TicketToCodeRequest request, int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/intp/marketcode/tickettocode";
            return CommonJsonSend.SendAsync<TicketToCodeJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting);
        }

        /// <summary>
        /// 异步批量申请一物一码营销二维码。
        /// </summary>
        /// <param name="accessTokenOrAppId">公众号 AccessToken、authorizer_access_token 或已注册的 AppId。</param>
        /// <param name="request">申请数量和调用方外部单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信生成的申请单号。</returns>
        /// <remarks>
        /// 申请数量必须为 10000 的整数倍，范围为 10000 至 20000000；相同外部单号视为同一申请单。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/onecode/api_intp_marketcode_applycode"/>。
        /// </remarks>
        public static async Task<ApplyCodeJsonResult> ApplyCodeAsync(string accessTokenOrAppId, ApplyCodeRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/intp/marketcode/applycode?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<ApplyCodeJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut: timeOut, jsonSetting: IgnoreNullJsonSetting).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}
