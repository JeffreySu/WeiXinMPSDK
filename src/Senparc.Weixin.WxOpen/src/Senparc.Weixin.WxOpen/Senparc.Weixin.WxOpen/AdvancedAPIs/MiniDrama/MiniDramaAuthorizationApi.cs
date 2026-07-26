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

    文件名：MiniDramaAuthorizationApi.cs
    文件功能描述：MiniDramaAuthorizationApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>小程序短剧剧目、账号与版权授权接口。</summary>
    public static partial class MiniDramaApi
    {
        #region 剧目授权

        /// <summary>查询当前小程序被授权的短剧信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选授权方 AppId 和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>被授权剧目列表。</returns>
        /// <remarks>官方在“剧目授权”和“账号授权”目录重复列出同一路径，本 SDK 复用一个方法。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizeapp/api_getauthorizedobjects.html"/>。</remarks>
        public static MiniDramaGetAuthorizedObjectsJsonResult GetAuthorizedObjects(string accessTokenOrAppId, MiniDramaGetAuthorizedObjectsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetAuthorizedObjectsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizedobjects", request, timeOut);
        }

        /// <summary>异步查询当前小程序被授权的短剧信息。</summary>
        /// <inheritdoc cref="GetAuthorizedObjects"/>
        public static Task<MiniDramaGetAuthorizedObjectsJsonResult> GetAuthorizedObjectsAsync(string accessTokenOrAppId, MiniDramaGetAuthorizedObjectsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetAuthorizedObjectsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizedobjects", request, timeOut);
        }

        /// <summary>向指定小程序增加短剧剧目播放授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">剧目列表、被授权方 AppId 和到期时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>逐剧目授权结果。</returns>
        /// <remarks>
        /// 官方页面当前把 HTTPS 调用方式写为 GET，同时又定义 JSON 请求体并给出 JSON 示例；为确保请求体可可靠传输，本实现按同簇接口语义使用 POST。
        /// 官方参数表还把 <c>authorized_appid</c> 误写为 <c>authorized</c>，本模型采用官方示例和关联接口一致的字段名。
        /// 微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizedrama/api_authorizedrama.html"/>。
        /// </remarks>
        public static MiniDramaAuthorizationJsonResult AuthorizeDrama(string accessTokenOrAppId, MiniDramaDramaAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizedrama", request, timeOut);
        }

        /// <summary>异步向指定小程序增加短剧剧目播放授权。</summary>
        /// <inheritdoc cref="AuthorizeDrama"/>
        public static Task<MiniDramaAuthorizationJsonResult> AuthorizeDramaAsync(string accessTokenOrAppId, MiniDramaDramaAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizedrama", request, timeOut);
        }

        /// <summary>解除指定小程序的短剧剧目播放授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">待解除授权的剧目列表和被授权方 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>逐剧目解除授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizedrama/api_deauthorizedrama.html"/>。</remarks>
        public static MiniDramaAuthorizationJsonResult DeauthorizeDrama(string accessTokenOrAppId, MiniDramaDramaDeauthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizedrama", request, timeOut);
        }

        /// <summary>异步解除指定小程序的短剧剧目播放授权。</summary>
        /// <inheritdoc cref="DeauthorizeDrama"/>
        public static Task<MiniDramaAuthorizationJsonResult> DeauthorizeDramaAsync(string accessTokenOrAppId, MiniDramaDramaDeauthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizedrama", request, timeOut);
        }

        /// <summary>查询当前小程序对外授予的短剧剧目授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选剧目、被授权方和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>授权信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizedrama/api_getauthorizeobjects.html"/>。</remarks>
        public static MiniDramaGetAuthorizeObjectsJsonResult GetAuthorizeObjects(string accessTokenOrAppId, MiniDramaGetAuthorizeObjectsRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetAuthorizeObjectsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizeobjects", request, timeOut);
        }

        /// <summary>异步查询当前小程序对外授予的短剧剧目授权。</summary>
        /// <inheritdoc cref="GetAuthorizeObjects"/>
        public static Task<MiniDramaGetAuthorizeObjectsJsonResult> GetAuthorizeObjectsAsync(string accessTokenOrAppId, MiniDramaGetAuthorizeObjectsRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetAuthorizeObjectsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizeobjects", request, timeOut);
        }

        #endregion

        #region 账号授权

        /// <summary>向指定小程序授权当前账号全部短剧的播放权限。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">被授权方 AppId 和可选到期时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账号授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizeapp/api_authorizeapp.html"/>。</remarks>
        public static WxJsonResult AuthorizeApp(string accessTokenOrAppId, MiniDramaAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizeapp", request, timeOut);
        }

        /// <summary>异步向指定小程序授权当前账号全部短剧的播放权限。</summary>
        /// <inheritdoc cref="AuthorizeApp"/>
        public static Task<WxJsonResult> AuthorizeAppAsync(string accessTokenOrAppId, MiniDramaAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizeapp", request, timeOut);
        }

        /// <summary>解除指定小程序的短剧账号播放授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">被授权方小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>解除授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizeapp/api_deauthorizeapp.html"/>。</remarks>
        public static WxJsonResult DeauthorizeApp(string accessTokenOrAppId, MiniDramaAppIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizeapp", request, timeOut);
        }

        /// <summary>异步解除指定小程序的短剧账号播放授权。</summary>
        /// <inheritdoc cref="DeauthorizeApp"/>
        public static Task<WxJsonResult> DeauthorizeAppAsync(string accessTokenOrAppId, MiniDramaAppIdentityRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizeapp", request, timeOut);
        }

        /// <summary>查询当前小程序授予的短剧账号授权信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账号授权信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizeapp/api_getauthorizeapps.html"/>。</remarks>
        public static MiniDramaGetAuthorizeAppsJsonResult GetAuthorizeApps(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaGetAuthorizeAppsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizeapps", new { }, timeOut);
        }

        /// <summary>异步查询当前小程序授予的短剧账号授权信息。</summary>
        /// <inheritdoc cref="GetAuthorizeApps"/>
        public static Task<MiniDramaGetAuthorizeAppsJsonResult> GetAuthorizeAppsAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaGetAuthorizeAppsJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getauthorizeapps", new { }, timeOut);
        }

        #endregion

        #region 版权授权

        /// <summary>增加受版权保护短剧的版权授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">被授权主体或小程序、剧目列表及到期时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>逐剧目版权授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizecopyright/api_authorizecopyright.html"/>。</remarks>
        public static MiniDramaAuthorizationJsonResult AuthorizeCopyright(string accessTokenOrAppId, MiniDramaCopyrightAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizecopyright", request, timeOut);
        }

        /// <summary>异步增加受版权保护短剧的版权授权。</summary>
        /// <inheritdoc cref="AuthorizeCopyright"/>
        public static Task<MiniDramaAuthorizationJsonResult> AuthorizeCopyrightAsync(string accessTokenOrAppId, MiniDramaCopyrightAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/authorizecopyright", request, timeOut);
        }

        /// <summary>解除受版权保护短剧的版权授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">被授权主体或小程序及剧目列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>逐剧目解除版权授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizecopyright/api_deauthorizecopyright.html"/>。</remarks>
        public static MiniDramaAuthorizationJsonResult DeauthorizeCopyright(string accessTokenOrAppId, MiniDramaCopyrightDeauthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizecopyright", request, timeOut);
        }

        /// <summary>异步解除受版权保护短剧的版权授权。</summary>
        /// <inheritdoc cref="DeauthorizeCopyright"/>
        public static Task<MiniDramaAuthorizationJsonResult> DeauthorizeCopyrightAsync(string accessTokenOrAppId, MiniDramaCopyrightDeauthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/deauthorizecopyright", request, timeOut);
        }

        /// <summary>分页查询当前小程序授予的短剧版权授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">授权类型、被授权方、剧目和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>版权授权信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizecopyright/api_getcopyrightauthorizationlist.html"/>。</remarks>
        public static MiniDramaCopyrightAuthorizationListJsonResult GetCopyrightAuthorizationList(string accessTokenOrAppId, MiniDramaGetCopyrightAuthorizationListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaCopyrightAuthorizationListJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcopyrightauthorizationlist", request, timeOut);
        }

        /// <summary>异步分页查询当前小程序授予的短剧版权授权。</summary>
        /// <inheritdoc cref="GetCopyrightAuthorizationList"/>
        public static Task<MiniDramaCopyrightAuthorizationListJsonResult> GetCopyrightAuthorizationListAsync(string accessTokenOrAppId, MiniDramaGetCopyrightAuthorizationListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaCopyrightAuthorizationListJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcopyrightauthorizationlist", request, timeOut);
        }

        /// <summary>分页查询当前小程序收到的短剧版权授权。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选授权方 AppId 和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>被版权授权信息列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/minidrama/authorizecopyright/api_getcopyrightauthorizedlist.html"/>。</remarks>
        public static MiniDramaCopyrightAuthorizationListJsonResult GetCopyrightAuthorizedList(string accessTokenOrAppId, MiniDramaGetCopyrightAuthorizedListRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<MiniDramaCopyrightAuthorizationListJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcopyrightauthorizedlist", request, timeOut);
        }

        /// <summary>异步分页查询当前小程序收到的短剧版权授权。</summary>
        /// <inheritdoc cref="GetCopyrightAuthorizedList"/>
        public static Task<MiniDramaCopyrightAuthorizationListJsonResult> GetCopyrightAuthorizedListAsync(string accessTokenOrAppId, MiniDramaGetCopyrightAuthorizedListRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<MiniDramaCopyrightAuthorizationListJsonResult>(accessTokenOrAppId, "/wxa/sec/vod/getcopyrightauthorizedlist", request, timeOut);
        }

        #endregion
    }
}
