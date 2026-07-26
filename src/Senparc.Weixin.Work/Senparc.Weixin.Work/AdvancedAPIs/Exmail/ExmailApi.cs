/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailApi.cs
    文件功能描述：企业微信邮件应用邮箱接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件发送、读取和应用邮箱别名接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 企业微信邮件接口。
    /// <para>调用前需要配置“邮件”或相应邮箱管理权限，并使用对应应用 Secret 获取的 access_token。</para>
    /// </summary>
    public static partial class ExmailApi
    {
        private const string ComposeSendPath = "/cgi-bin/exmail/app/compose_send";
        private const string AppMailListPath = "/cgi-bin/exmail/app/get_mail_list";
        private const string ReadMailPath = "/cgi-bin/exmail/app/read_mail";
        private const string UpdateAppEmailAliasPath = "/cgi-bin/exmail/app/update_email_alias";
        private const string GetAppEmailAliasPath = "/cgi-bin/exmail/app/get_email_alias";

        /// <summary>
        /// 使用应用邮箱发送邮件，可同时携带附件、日程或会议。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">收件人、主题、正文及可选附件、日程和会议信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult SendAppMail(string accessTokenOrAppKey, ExmailComposeSendRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, ComposeSendPath, request, timeOut);

        /// <summary>
        /// 异步使用应用邮箱发送邮件，可同时携带附件、日程或会议。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">收件人、主题、正文及可选附件、日程和会议信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> SendAppMailAsync(string accessTokenOrAppKey,
            ExmailComposeSendRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, ComposeSendPath, request, timeOut);

        /// <summary>
        /// 分页获取应用邮箱收到的邮件 ID 列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">时间范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>邮件 ID 列表及下一页游标。</returns>
        public static ExmailAppMailListResult GetAppMailList(string accessTokenOrAppKey,
            ExmailAppMailListRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExmailAppMailListResult>(accessTokenOrAppKey, AppMailListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取应用邮箱收到的邮件 ID 列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">时间范围和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>邮件 ID 列表及下一页游标。</returns>
        public static Task<ExmailAppMailListResult> GetAppMailListAsync(string accessTokenOrAppKey,
            ExmailAppMailListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailAppMailListResult>(accessTokenOrAppKey, AppMailListPath, request, timeOut);

        /// <summary>
        /// 读取指定应用邮箱邮件的 EML 原始内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>邮件 EML 原始内容。</returns>
        public static ExmailReadMailResult ReadAppMail(string accessTokenOrAppKey, ExmailReadMailRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ExmailReadMailResult>(accessTokenOrAppKey, ReadMailPath, request, timeOut);

        /// <summary>
        /// 异步读取指定应用邮箱邮件的 EML 原始内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">邮件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>邮件 EML 原始内容。</returns>
        public static Task<ExmailReadMailResult> ReadAppMailAsync(string accessTokenOrAppKey,
            ExmailReadMailRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailReadMailResult>(accessTokenOrAppKey, ReadMailPath, request, timeOut);

        /// <summary>
        /// 修改当前应用的邮箱地址。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">新的应用邮箱地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateAppEmailAlias(string accessTokenOrAppKey,
            ExmailUpdateAppEmailAliasRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateAppEmailAliasPath, request, timeOut);

        /// <summary>
        /// 异步修改当前应用的邮箱地址。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">新的应用邮箱地址。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateAppEmailAliasAsync(string accessTokenOrAppKey,
            ExmailUpdateAppEmailAliasRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateAppEmailAliasPath, request, timeOut);

        /// <summary>
        /// 获取当前应用的邮箱地址和别名列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应用邮箱地址和别名列表。</returns>
        public static ExmailAppEmailAliasResult GetAppEmailAlias(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Post<ExmailAppEmailAliasResult>(accessTokenOrAppKey, GetAppEmailAliasPath, new { }, timeOut);

        /// <summary>
        /// 异步获取当前应用的邮箱地址和别名列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置邮件权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>应用邮箱地址和别名列表。</returns>
        public static Task<ExmailAppEmailAliasResult> GetAppEmailAliasAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExmailAppEmailAliasResult>(accessTokenOrAppKey, GetAppEmailAliasPath, new { }, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static T Get<T>(string accessTokenOrAppKey, string urlFormat, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                urlFormat, null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        private static Task<T> GetAsync<T>(string accessTokenOrAppKey, string urlFormat, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                urlFormat, null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);
    }
}
