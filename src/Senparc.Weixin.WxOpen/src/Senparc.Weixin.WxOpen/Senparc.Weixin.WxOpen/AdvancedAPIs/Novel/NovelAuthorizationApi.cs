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

    文件名：NovelAuthorizationApi.cs
    文件功能描述：NovelAuthorizationApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>小程序小说授权管理接口。</summary>
    public static partial class NovelApi
    {
        /// <summary>批量新增账号与指定小说的授权关系。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">最多 20 条作品、被授权 AppId 和到期时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>整体及逐条授权结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_addbookauth.html"/>。</remarks>
        public static NovelAuthorizationJsonResult AddBookAuthorization(string accessTokenOrAppId, NovelAddBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/addbookauth", request, timeOut);
        }

        /// <summary>异步批量新增账号与指定小说的授权关系。</summary>
        /// <inheritdoc cref="AddBookAuthorization"/>
        public static Task<NovelAuthorizationJsonResult> AddBookAuthorizationAsync(string accessTokenOrAppId, NovelAddBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/addbookauth", request, timeOut);
        }

        /// <summary>查询账号与小说的授权或被授权关系列表。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">查询方向、分页及可选作品筛选。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>作品级授权关系列表。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_querybookauth.html"/>。</remarks>
        public static NovelQueryBookAuthorizationJsonResult QueryBookAuthorization(string accessTokenOrAppId, NovelQueryBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelQueryBookAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/querybookauth", request, timeOut);
        }

        /// <summary>异步查询账号与小说的授权或被授权关系列表。</summary>
        /// <inheritdoc cref="QueryBookAuthorization"/>
        public static Task<NovelQueryBookAuthorizationJsonResult> QueryBookAuthorizationAsync(string accessTokenOrAppId, NovelQueryBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelQueryBookAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/querybookauth", request, timeOut);
        }

        /// <summary>删除指定账号与小说的授权关系。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">作品 ID 和被授权账号 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_delbookauth.html"/>。</remarks>
        public static WxJsonResult DeleteBookAuthorization(string accessTokenOrAppId, NovelDeleteBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/delbookauth", request, timeOut);
        }

        /// <summary>异步删除指定账号与小说的授权关系。</summary>
        /// <inheritdoc cref="DeleteBookAuthorization"/>
        public static Task<WxJsonResult> DeleteBookAuthorizationAsync(string accessTokenOrAppId, NovelDeleteBookAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/delbookauth", request, timeOut);
        }

        /// <summary>批量新增账号级小说授权关系。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">最多 20 个被授权 AppId 及到期时间。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>整体及逐条授权结果。</returns>
        /// <remarks>账号级授权覆盖授权方的全部小说。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_addbookauthbyappid.html"/>。</remarks>
        public static NovelAuthorizationJsonResult AddAppAuthorization(string accessTokenOrAppId, NovelAddAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/addbookauthbyappid", request, timeOut);
        }

        /// <summary>异步批量新增账号级小说授权关系。</summary>
        /// <inheritdoc cref="AddAppAuthorization"/>
        public static Task<NovelAuthorizationJsonResult> AddAppAuthorizationAsync(string accessTokenOrAppId, NovelAddAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/addbookauthbyappid", request, timeOut);
        }

        /// <summary>查询账号级授权、被授权或指定小说授权信息。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">查询方向、游标、授权方或作品 ID 条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>账号级或作品级授权结果和下一页游标。</returns>
        /// <remarks>官方返回表仅列 appid_results，但按授权方或作品查询的示例返回 book_results，模型兼容二者。微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_querybookauthv2.html"/>。</remarks>
        public static NovelQueryAppAuthorizationJsonResult QueryAppAuthorization(string accessTokenOrAppId, NovelQueryAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<NovelQueryAppAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/querybookauthv2", request, timeOut);
        }

        /// <summary>异步查询账号级授权、被授权或指定小说授权信息。</summary>
        /// <inheritdoc cref="QueryAppAuthorization"/>
        public static Task<NovelQueryAppAuthorizationJsonResult> QueryAppAuthorizationAsync(string accessTokenOrAppId, NovelQueryAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<NovelQueryAppAuthorizationJsonResult>(accessTokenOrAppId, "/wxa/book/querybookauthv2", request, timeOut);
        }

        /// <summary>删除指定账号级小说授权关系。</summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">被授权账号 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>删除结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/novel/auth/api_delbookauthbyappid.html"/>。</remarks>
        public static WxJsonResult DeleteAppAuthorization(string accessTokenOrAppId, NovelDeleteAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/wxa/book/delbookauthbyappid", request, timeOut);
        }

        /// <summary>异步删除指定账号级小说授权关系。</summary>
        /// <inheritdoc cref="DeleteAppAuthorization"/>
        public static Task<WxJsonResult> DeleteAppAuthorizationAsync(string accessTokenOrAppId, NovelDeleteAppAuthorizationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/wxa/book/delbookauthbyappid", request, timeOut);
        }
    }
}
