#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ComponentApi.Current.cs
    文件功能描述：第三方平台当前版授权方选项接口


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v4.24.4 补齐当前授权方选项获取和设置接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 第三方平台当前版授权方选项接口。
    /// </summary>
    public static partial class ComponentApi
    {
        /// <summary>
        /// 获取授权方的公众号或小程序选项设置信息。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="optionName">选项名称，例如 <c>location_report</c>、<c>voice_recognize</c> 或 <c>customer_service</c>。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>授权方选项名称和值。</returns>
        /// <remarks>
        /// 官方接口英文名：getAuthorizerOptionInfo。
        /// 当前接口使用 <c>/cgi-bin/component/get_authorizer_option</c>，请求体不再包含旧接口所需的
        /// <c>component_appid</c> 和 <c>authorizer_appid</c>。
        /// </remarks>
        public static AuthorizerOptionInfoJsonResult GetAuthorizerOptionInfo(string componentAccessToken,
            string optionName, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/component/get_authorizer_option?access_token={componentAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<AuthorizerOptionInfoJsonResult>(null, url,
                new { option_name = optionName }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步获取授权方的公众号或小程序选项设置信息。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="optionName">选项名称，例如 <c>location_report</c>、<c>voice_recognize</c> 或 <c>customer_service</c>。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>授权方选项名称和值。</returns>
        /// <remarks>官方接口英文名：getAuthorizerOptionInfo。</remarks>
        public static Task<AuthorizerOptionInfoJsonResult> GetAuthorizerOptionInfoAsync(
            string componentAccessToken, string optionName, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/component/get_authorizer_option?access_token={componentAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<AuthorizerOptionInfoJsonResult>(null, url,
                new { option_name = optionName }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 设置授权方的公众号或小程序选项信息。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="optionName">选项名称，例如 <c>location_report</c>、<c>voice_recognize</c> 或 <c>customer_service</c>。</param>
        /// <param name="optionValue">选项值。允许值由具体选项决定，例如 <c>0</c>、<c>1</c> 或 <c>2</c>。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        /// <remarks>
        /// 官方接口英文名：setAuthorizerOptionInfo。
        /// 当前接口使用 <c>/cgi-bin/component/set_authorizer_option</c>，请求体仅包含选项名称和值。
        /// </remarks>
        public static WxJsonResult SetAuthorizerOptionInfo(string componentAccessToken, string optionName,
            string optionValue, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/component/set_authorizer_option?access_token={componentAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<WxJsonResult>(null, url,
                new { option_name = optionName, option_value = optionValue }, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步设置授权方的公众号或小程序选项信息。
        /// </summary>
        /// <param name="componentAccessToken">第三方平台接口调用凭证。</param>
        /// <param name="optionName">选项名称，例如 <c>location_report</c>、<c>voice_recognize</c> 或 <c>customer_service</c>。</param>
        /// <param name="optionValue">选项值。允许值由具体选项决定，例如 <c>0</c>、<c>1</c> 或 <c>2</c>。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        /// <remarks>官方接口英文名：setAuthorizerOptionInfo。</remarks>
        public static Task<WxJsonResult> SetAuthorizerOptionInfoAsync(string componentAccessToken,
            string optionName, string optionValue, int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/component/set_authorizer_option?access_token={componentAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<WxJsonResult>(null, url,
                new { option_name = optionName, option_value = optionValue }, CommonJsonSendType.POST, timeOut);
        }
    }

    /// <summary>
    /// 获取授权方选项信息接口返回结果。
    /// </summary>
    public class AuthorizerOptionInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 选项名称。
        /// </summary>
        public string option_name { get; set; }

        /// <summary>
        /// 选项值。
        /// </summary>
        public string option_value { get; set; }
    }
}
