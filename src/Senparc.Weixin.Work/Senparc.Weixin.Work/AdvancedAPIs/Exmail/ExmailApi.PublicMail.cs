/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailApi.PublicMail.cs
    文件功能描述：企业微信业务邮箱接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐业务邮箱及客户端专用密码接口

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业微信业务邮箱接口。
    /// </summary>
    public static partial class ExmailApi
    {
        private const string PublicMailCreatePath = "/cgi-bin/exmail/publicmail/create";
        private const string PublicMailUpdatePath = "/cgi-bin/exmail/publicmail/update";
        private const string PublicMailDeletePath = "/cgi-bin/exmail/publicmail/delete";
        private const string PublicMailSearchPath = "/cgi-bin/exmail/publicmail/search";
        private const string PublicMailGetPath = "/cgi-bin/exmail/publicmail/get";
        private const string PublicMailAuthCodeListPath = "/cgi-bin/exmail/publicmail/get_auth_code_list";
        private const string PublicMailDeleteAuthCodePath = "/cgi-bin/exmail/publicmail/delete_auth_code";

        /// <summary>
        /// 创建业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮箱地址、名称、使用范围及可选客户端专用密码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建业务邮箱 ID。</returns>
        public static ExmailPublicMailCreateResult CreatePublicMail(string accessTokenOrAppKey,
            ExmailPublicMailCreateRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailPublicMailCreateResult>(accessTokenOrAppKey, PublicMailCreatePath, request, timeOut);

        /// <summary>
        /// 异步创建业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮箱地址、名称、使用范围及可选客户端专用密码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建业务邮箱 ID。</returns>
        public static Task<ExmailPublicMailCreateResult> CreatePublicMailAsync(string accessTokenOrAppKey,
            ExmailPublicMailCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailPublicMailCreateResult>(accessTokenOrAppKey, PublicMailCreatePath, request, timeOut);

        /// <summary>
        /// 更新业务邮箱名称、使用范围、别名或客户端专用密码。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 及需要更新的字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdatePublicMail(string accessTokenOrAppKey,
            ExmailPublicMailUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, PublicMailUpdatePath, request, timeOut);

        /// <summary>
        /// 异步更新业务邮箱名称、使用范围、别名或客户端专用密码。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 及需要更新的字段。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdatePublicMailAsync(string accessTokenOrAppKey,
            ExmailPublicMailUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, PublicMailUpdatePath, request, timeOut);

        /// <summary>
        /// 删除业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult DeletePublicMail(string accessTokenOrAppKey,
            ExmailPublicMailIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, PublicMailDeletePath, request, timeOut);

        /// <summary>
        /// 异步删除业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> DeletePublicMailAsync(string accessTokenOrAppKey,
            ExmailPublicMailIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, PublicMailDeletePath, request, timeOut);

        /// <summary>
        /// 搜索业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="emailOrName">业务邮箱地址或名称关键字；不传时按接口默认范围搜索。</param>
        /// <param name="fuzzy">是否启用模糊搜索。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配的业务邮箱列表。</returns>
        public static ExmailPublicMailSearchResult SearchPublicMail(string accessTokenOrAppKey,
            string emailOrName = null, bool fuzzy = false, int timeOut = Config.TIME_OUT)
            => Get<ExmailPublicMailSearchResult>(accessTokenOrAppKey,
                BuildPublicMailSearchUrl(emailOrName, fuzzy), timeOut);

        /// <summary>
        /// 异步搜索业务邮箱。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="emailOrName">业务邮箱地址或名称关键字；不传时按接口默认范围搜索。</param>
        /// <param name="fuzzy">是否启用模糊搜索。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配的业务邮箱列表。</returns>
        public static Task<ExmailPublicMailSearchResult> SearchPublicMailAsync(string accessTokenOrAppKey,
            string emailOrName = null, bool fuzzy = false, int timeOut = Config.TIME_OUT)
            => GetAsync<ExmailPublicMailSearchResult>(accessTokenOrAppKey,
                BuildPublicMailSearchUrl(emailOrName, fuzzy), timeOut);

        /// <summary>
        /// 批量获取业务邮箱详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>业务邮箱详情列表。</returns>
        public static ExmailPublicMailListResult GetPublicMail(string accessTokenOrAppKey,
            ExmailPublicMailIdListRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailPublicMailListResult>(accessTokenOrAppKey, PublicMailGetPath, request, timeOut);

        /// <summary>
        /// 异步批量获取业务邮箱详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>业务邮箱详情列表。</returns>
        public static Task<ExmailPublicMailListResult> GetPublicMailAsync(string accessTokenOrAppKey,
            ExmailPublicMailIdListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailPublicMailListResult>(accessTokenOrAppKey, PublicMailGetPath, request, timeOut);

        /// <summary>
        /// 获取业务邮箱客户端专用密码列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>客户端专用密码 ID、备注及时间信息。</returns>
        public static ExmailAuthCodeListResult GetPublicMailAuthCodeList(string accessTokenOrAppKey,
            ExmailPublicMailIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailAuthCodeListResult>(accessTokenOrAppKey, PublicMailAuthCodeListPath, request, timeOut);

        /// <summary>
        /// 异步获取业务邮箱客户端专用密码列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>客户端专用密码 ID、备注及时间信息。</returns>
        public static Task<ExmailAuthCodeListResult> GetPublicMailAuthCodeListAsync(
            string accessTokenOrAppKey, ExmailPublicMailIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailAuthCodeListResult>(accessTokenOrAppKey,
                PublicMailAuthCodeListPath, request, timeOut);

        /// <summary>
        /// 删除业务邮箱客户端专用密码。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 和客户端专用密码 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult DeletePublicMailAuthCode(string accessTokenOrAppKey,
            ExmailDeleteAuthCodeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, PublicMailDeleteAuthCodePath, request, timeOut);

        /// <summary>
        /// 异步删除业务邮箱客户端专用密码。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置业务邮箱权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">业务邮箱 ID 和客户端专用密码 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> DeletePublicMailAuthCodeAsync(string accessTokenOrAppKey,
            ExmailDeleteAuthCodeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, PublicMailDeleteAuthCodePath, request, timeOut);

        private static string BuildPublicMailSearchUrl(string emailOrName, bool fuzzy)
        {
            var url = Config.ApiWorkHost + PublicMailSearchPath + "?access_token={0}&fuzzy=" +
                      (fuzzy ? "1" : "0");
            return string.IsNullOrEmpty(emailOrName)
                ? url
                : url + "&email=" + Uri.EscapeDataString(emailOrName);
        }
    }
}
