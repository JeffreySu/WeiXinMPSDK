/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailApi.Group.cs
    文件功能描述：企业微信邮件群组接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件群组管理接口

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业微信邮件群组接口。
    /// </summary>
    public static partial class ExmailApi
    {
        private const string GroupCreatePath = "/cgi-bin/exmail/group/create";
        private const string GroupUpdatePath = "/cgi-bin/exmail/group/update";
        private const string GroupDeletePath = "/cgi-bin/exmail/group/delete";
        private const string GroupSearchPath = "/cgi-bin/exmail/group/search";
        private const string GroupGetPath = "/cgi-bin/exmail/group/get";

        /// <summary>
        /// 创建邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">群组地址、名称、成员范围和使用权限。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult CreateGroup(string accessTokenOrAppKey, ExmailGroupRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, GroupCreatePath, request, timeOut);

        /// <summary>
        /// 异步创建邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">群组地址、名称、成员范围和使用权限。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> CreateGroupAsync(string accessTokenOrAppKey,
            ExmailGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, GroupCreatePath, request, timeOut);

        /// <summary>
        /// 更新邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">群组地址及需要更新的名称、成员范围或使用权限。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateGroup(string accessTokenOrAppKey, ExmailGroupRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, GroupUpdatePath, request, timeOut);

        /// <summary>
        /// 异步更新邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">群组地址及需要更新的名称、成员范围或使用权限。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateGroupAsync(string accessTokenOrAppKey,
            ExmailGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, GroupUpdatePath, request, timeOut);

        /// <summary>
        /// 删除邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮件群组地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult DeleteGroup(string accessTokenOrAppKey, ExmailGroupIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, GroupDeletePath, request, timeOut);

        /// <summary>
        /// 异步删除邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮件群组地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> DeleteGroupAsync(string accessTokenOrAppKey,
            ExmailGroupIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, GroupDeletePath, request, timeOut);

        /// <summary>
        /// 搜索邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="groupId">群组地址关键字；不传时按接口默认范围搜索。</param>
        /// <param name="fuzzy">是否启用模糊搜索。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配的邮件群组列表。</returns>
        public static ExmailGroupSearchResult SearchGroups(string accessTokenOrAppKey, string groupId = null,
            bool fuzzy = false, int timeOut = Config.TIME_OUT)
            => Get<ExmailGroupSearchResult>(accessTokenOrAppKey, BuildGroupSearchUrl(groupId, fuzzy), timeOut);

        /// <summary>
        /// 异步搜索邮件群组。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="groupId">群组地址关键字；不传时按接口默认范围搜索。</param>
        /// <param name="fuzzy">是否启用模糊搜索。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>匹配的邮件群组列表。</returns>
        public static Task<ExmailGroupSearchResult> SearchGroupsAsync(string accessTokenOrAppKey,
            string groupId = null, bool fuzzy = false, int timeOut = Config.TIME_OUT)
            => GetAsync<ExmailGroupSearchResult>(accessTokenOrAppKey, BuildGroupSearchUrl(groupId, fuzzy), timeOut);

        /// <summary>
        /// 获取邮件群组详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="groupId">邮件群组地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>群组成员范围和使用权限。</returns>
        public static ExmailGroupResult GetGroup(string accessTokenOrAppKey, string groupId,
            int timeOut = Config.TIME_OUT)
            => Get<ExmailGroupResult>(accessTokenOrAppKey, BuildGroupGetUrl(groupId), timeOut);

        /// <summary>
        /// 异步获取邮件群组详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件群组权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="groupId">邮件群组地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>群组成员范围和使用权限。</returns>
        public static Task<ExmailGroupResult> GetGroupAsync(string accessTokenOrAppKey, string groupId,
            int timeOut = Config.TIME_OUT)
            => GetAsync<ExmailGroupResult>(accessTokenOrAppKey, BuildGroupGetUrl(groupId), timeOut);

        private static string BuildGroupSearchUrl(string groupId, bool fuzzy)
        {
            var url = Config.ApiWorkHost + GroupSearchPath + "?access_token={0}&fuzzy=" + (fuzzy ? "1" : "0");
            return string.IsNullOrEmpty(groupId)
                ? url
                : url + "&groupid=" + Uri.EscapeDataString(groupId);
        }

        private static string BuildGroupGetUrl(string groupId)
            => Config.ApiWorkHost + GroupGetPath + "?access_token={0}&groupid=" +
               Uri.EscapeDataString(groupId ?? string.Empty);
    }
}
