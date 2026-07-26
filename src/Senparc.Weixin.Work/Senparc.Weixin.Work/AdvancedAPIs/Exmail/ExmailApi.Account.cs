/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailApi.Account.cs
    文件功能描述：企业微信邮件账号、功能设置和新邮件接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件账号激活、功能设置和新邮件数量接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业微信邮件账号、成员功能设置和新邮件数量接口。
    /// </summary>
    public static partial class ExmailApi
    {
        private const string ActivateAccountPath = "/cgi-bin/exmail/account/act_email";
        private const string GetUserOptionsPath = "/cgi-bin/exmail/useroption/get";
        private const string UpdateUserOptionsPath = "/cgi-bin/exmail/useroption/update";
        private const string GetNewMailCountPath = "/cgi-bin/exmail/mail/get_newcount";

        /// <summary>
        /// 激活或注销成员邮箱、业务邮箱账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 或业务邮箱 ID，以及操作类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult ActivateEmailAccount(string accessTokenOrAppKey,
            ExmailActivateAccountRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ActivateAccountPath, request, timeOut);

        /// <summary>
        /// 异步激活或注销成员邮箱、业务邮箱账号。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 或业务邮箱 ID，以及操作类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> ActivateEmailAccountAsync(string accessTokenOrAppKey,
            ExmailActivateAccountRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ActivateAccountPath, request, timeOut);

        /// <summary>
        /// 获取成员邮箱功能设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 和需要查询的设置类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员邮箱功能设置列表。</returns>
        public static ExmailUserOptionsResult GetUserOptions(string accessTokenOrAppKey,
            ExmailGetUserOptionsRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailUserOptionsResult>(accessTokenOrAppKey, GetUserOptionsPath, request, timeOut);

        /// <summary>
        /// 异步获取成员邮箱功能设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 和需要查询的设置类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员邮箱功能设置列表。</returns>
        public static Task<ExmailUserOptionsResult> GetUserOptionsAsync(string accessTokenOrAppKey,
            ExmailGetUserOptionsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailUserOptionsResult>(accessTokenOrAppKey, GetUserOptionsPath, request, timeOut);

        /// <summary>
        /// 更新成员邮箱功能设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 和需要更新的设置项。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateUserOptions(string accessTokenOrAppKey,
            ExmailUpdateUserOptionsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateUserOptionsPath, request, timeOut);

        /// <summary>
        /// 异步更新成员邮箱功能设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID 和需要更新的设置项。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateUserOptionsAsync(string accessTokenOrAppKey,
            ExmailUpdateUserOptionsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateUserOptionsPath, request, timeOut);

        /// <summary>
        /// 获取成员邮箱的新邮件数量。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新邮件数量。</returns>
        public static ExmailNewMailCountResult GetNewMailCount(string accessTokenOrAppKey,
            ExmailNewMailCountRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailNewMailCountResult>(accessTokenOrAppKey, GetNewMailCountPath, request, timeOut);

        /// <summary>
        /// 异步获取成员邮箱的新邮件数量。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件账号权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员 UserID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新邮件数量。</returns>
        public static Task<ExmailNewMailCountResult> GetNewMailCountAsync(string accessTokenOrAppKey,
            ExmailNewMailCountRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailNewMailCountResult>(accessTokenOrAppKey, GetNewMailCountPath, request, timeOut);
    }
}
