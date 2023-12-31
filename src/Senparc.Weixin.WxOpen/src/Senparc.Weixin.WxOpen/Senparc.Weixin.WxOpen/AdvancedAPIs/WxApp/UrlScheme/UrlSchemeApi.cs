/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：UrlSchemeApi.cs
    文件功能描述：URL Scheme 接口
    
    
    创建标识：Senparc - 20210118
    
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
        #endregion
    }
}
