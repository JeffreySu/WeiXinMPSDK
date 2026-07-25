/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UrlSchemeApi.cs
    文件功能描述：URL Scheme 接口


    创建标识：Senparc - 20210118

    修改标识：Senparc - 20241114
    修改描述：v3.22.0 添加 NCF UrlScheme 接口 #3093 感谢 @mojinxun

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{
    /// <summary>
    /// URL Scheme 接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class UrlSchemeApi
    {
        #region 同步方法

        /// <summary>
        /// 获取小程序scheme码
        /// <para>适用于短信、邮件、外部网页等拉起小程序的业务场景。通过该接口，可以选择生成到期失效和永久有效的小程序码，目前仅针对国内非个人主体的小程序开放，</para>
        /// <para>详见<see langword="获取URL scheme码" cref="https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/url-scheme.html"/></para>
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/url-scheme/urlscheme.generate.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="jumpWxa">（必填）跳转到的目标小程序信息。</param>
        /// <param name="isExpire">（非必填）生成的scheme码类型，到期失效：true，永久有效：false。</param>
        /// <param name="expireTime">（非必填）到期失效的scheme码的失效时间，为Unix时间戳。生成的到期失效scheme码在该时间前有效。生成到期失效的scheme时必填。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GenerateSchemeJsonResult GenerateScheme(string accessTokenOrAppId, GenerateSchemeJumpWxa jumpWxa = null, bool? isExpire = null,
            DateTime? expireTime = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/generatescheme?access_token={0}";

                var data = new
                {
                    jump_wxa = jumpWxa,
                    is_expire = isExpire,
                    expire_time = expireTime.HasValue ? Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(expireTime.Value) : (long?)null
                };

                return CommonJsonSend.Send<GenerateSchemeJsonResult>(accessToken, urlFormat, data, timeOut: timeOut,
                     jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting(true));
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取 NFC 的小程序 scheme
        /// <para>该接口用于获取用于 NFC 的小程序 scheme 码，适用于 NFC 拉起小程序的业务场景。目前仅针对国内非个人主体的小程序开放，详见 NFC 标签打开小程序</para>
        /// <para>详见<see langword="获取 NFC 的小程序 scheme" cref="https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/qrcode-link/url-scheme/generateNFCScheme.html"/></para>
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/qrcode-link/url-scheme/generateNFCScheme.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="jumpWxa">（必填）跳转到的目标小程序信息。</param>
        /// <param name="isExpire">（非必填）生成的scheme码类型，到期失效：true，永久有效：false。</param>
        /// <param name="expireTime">（非必填）到期失效的scheme码的失效时间，为Unix时间戳。生成的到期失效scheme码在该时间前有效。生成到期失效的scheme时必填。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GenerateNFCSchemeJsonResult GenerateNFCScheme(string accessTokenOrAppId, GenerateNFCSchemeJumpWxa jumpWxa = null, string model_id = "",
            string sn = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/generatenfcscheme?access_token={0}";

                var data = new
                {
                    jump_wxa = jumpWxa,
                    model_id = model_id,
                    sn = sn
                };

                return CommonJsonSend.Send<GenerateNFCSchemeJsonResult>(accessToken, urlFormat, data, timeOut: timeOut,
                     jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting(true));
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询小程序 URL Scheme 信息或访问额度
        /// </summary>
        /// <param name="scheme">待查询的 URL Scheme；queryType 为 0 时必填</param>
        /// <param name="queryType">查询类型：0 查询 Scheme 信息，1 查询访问额度</param>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static QuerySchemeJsonResult QueryScheme(string accessTokenOrAppId, string scheme = null, int queryType = 0,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/queryscheme?access_token={0}";
                var data = new { scheme, query_type = queryType };
                return CommonJsonSend.Send<QuerySchemeJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】获取小程序scheme码
        /// <para>适用于短信、邮件、外部网页等拉起小程序的业务场景。通过该接口，可以选择生成到期失效和永久有效的小程序码，目前仅针对国内非个人主体的小程序开放，</para>
        /// <para>详见<see langword="获取URL scheme码" cref="https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/url-scheme.html"/></para>
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/url-scheme/urlscheme.generate.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="jumpWxa">（必填）跳转到的目标小程序信息。</param>
        /// <param name="isExpire">（非必填）生成的scheme码类型，到期失效：true，永久有效：false。</param>
        /// <param name="expireTime">（非必填）到期失效的scheme码的失效时间，为Unix时间戳。生成的到期失效scheme码在该时间前有效。生成到期失效的scheme时必填。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GenerateSchemeJsonResult> GenerateSchemeAsync(string accessTokenOrAppId, GenerateSchemeJumpWxa jumpWxa = null, bool? isExpire = null,
            DateTime? expireTime = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/generatescheme?access_token={0}";

                var data = new
                {
                    jump_wxa = jumpWxa,
                    is_expire = isExpire,
                    expire_time = expireTime.HasValue ? Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(expireTime.Value) : (long?)null
                };

                return CommonJsonSend.Send<GenerateSchemeJsonResult>(accessToken, urlFormat, data, timeOut: timeOut, 
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting(true));
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取 NFC 的小程序 scheme
        /// <para>该接口用于获取用于 NFC 的小程序 scheme 码，适用于 NFC 拉起小程序的业务场景。目前仅针对国内非个人主体的小程序开放，详见 NFC 标签打开小程序</para>
        /// <para>详见<see langword="获取 NFC 的小程序 scheme" cref="https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/qrcode-link/url-scheme/generateNFCScheme.html"/></para>
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/qrcode-link/url-scheme/generateNFCScheme.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="jumpWxa">（必填）跳转到的目标小程序信息。</param>
        /// <param name="isExpire">（非必填）生成的scheme码类型，到期失效：true，永久有效：false。</param>
        /// <param name="expireTime">（非必填）到期失效的scheme码的失效时间，为Unix时间戳。生成的到期失效scheme码在该时间前有效。生成到期失效的scheme时必填。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GenerateNFCSchemeJsonResult> GenerateNFCSchemeAsync(string accessTokenOrAppId, GenerateNFCSchemeJumpWxa jumpWxa = null, string model_id = "",
            string sn = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/generatenfcscheme?access_token={0}";

                var data = new
                {
                    jump_wxa = jumpWxa,
                    model_id = model_id,
                    sn = sn
                };

                return CommonJsonSend.Send<GenerateNFCSchemeJsonResult>(accessToken, urlFormat, data, timeOut: timeOut,
                     jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting(true));
            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】查询小程序 URL Scheme 信息或访问额度
        /// </summary>
        /// <param name="scheme">待查询的 URL Scheme；queryType 为 0 时必填</param>
        /// <param name="queryType">查询类型：0 查询 Scheme 信息，1 查询访问额度</param>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<QuerySchemeJsonResult> QuerySchemeAsync(string accessTokenOrAppId, string scheme = null, int queryType = 0,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/wxa/queryscheme?access_token={0}";
                var data = new { scheme, query_type = queryType };
                return await CommonJsonSend.SendAsync<QuerySchemeJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
