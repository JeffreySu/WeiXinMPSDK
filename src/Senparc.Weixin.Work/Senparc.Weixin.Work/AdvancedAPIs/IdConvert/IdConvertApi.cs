/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：IdConvertApi.cs
    文件功能描述：企业微信账号与群聊 ID 转换接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐账号、标签、客服账号及群聊 ID 转换接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.IdConvert
{
    /// <summary>
    /// 企业微信账号、标签、客服账号和群聊 ID 转换接口。
    /// </summary>
    public static partial class IdConvertApi
    {
        private const string UnionIdToExternalUserIdPath =
            "/cgi-bin/idconvert/unionid_to_external_userid";
        private const string BatchExternalUserIdToPendingIdPath =
            "/cgi-bin/idconvert/batch/external_userid_to_pending_id";
        private const string ExternalTagIdPath = "/cgi-bin/idconvert/external_tagid";
        private const string OpenKfIdPath = "/cgi-bin/idconvert/open_kfid";
        private const string ApplyToUpgradeChatIdPath =
            "/cgi-bin/idconvert/apply_to_upgrade_chatid";
        private const string ChatIdPath = "/cgi-bin/idconvert/chatid";
        private const string UpgradeChatIdForNewCorpPath =
            "/cgi-bin/idconvert/upgrade_chatid_for_new_corp";

        /// <summary>
        /// 将微信 UnionId 或 OpenId 转换为企业微信外部联系人账号。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">UnionId、OpenId 和主体类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>外部联系人账号或待处理的临时账号。</returns>
        public static UnionIdToExternalUserIdResult UnionIdToExternalUserId(
            string accessTokenOrAppKey, UnionIdToExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<UnionIdToExternalUserIdResult>(accessTokenOrAppKey,
                UnionIdToExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 异步将微信 UnionId 或 OpenId 转换为企业微信外部联系人账号。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">UnionId、OpenId 和主体类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>外部联系人账号或待处理的临时账号。</returns>
        public static Task<UnionIdToExternalUserIdResult> UnionIdToExternalUserIdAsync(
            string accessTokenOrAppKey, UnionIdToExternalUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<UnionIdToExternalUserIdResult>(accessTokenOrAppKey,
                UnionIdToExternalUserIdPath, request, timeOut);

        /// <summary>
        /// 批量将外部联系人账号转换为迁移中的 PendingId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95900"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">群聊 ID 和待转换的外部联系人账号列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>外部联系人账号与 PendingId 的对应关系。</returns>
        public static BatchExternalUserIdToPendingIdResult BatchExternalUserIdToPendingId(
            string accessTokenOrAppKey, BatchExternalUserIdToPendingIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<BatchExternalUserIdToPendingIdResult>(accessTokenOrAppKey,
                BatchExternalUserIdToPendingIdPath, request, timeOut);

        /// <summary>
        /// 异步批量将外部联系人账号转换为迁移中的 PendingId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/95900"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">群聊 ID 和待转换的外部联系人账号列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>外部联系人账号与 PendingId 的对应关系。</returns>
        public static Task<BatchExternalUserIdToPendingIdResult> BatchExternalUserIdToPendingIdAsync(
            string accessTokenOrAppKey, BatchExternalUserIdToPendingIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<BatchExternalUserIdToPendingIdResult>(accessTokenOrAppKey,
                BatchExternalUserIdToPendingIdPath, request, timeOut);

        /// <summary>
        /// 将企业客户标签 ID 转换为服务商范围内的标签 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/96169"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的企业客户标签 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>标签 ID 转换结果和无效标签 ID 列表。</returns>
        public static ExternalTagIdConvertResult ConvertExternalTagId(
            string accessTokenOrAppKey, ExternalTagIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ExternalTagIdConvertResult>(accessTokenOrAppKey,
                ExternalTagIdPath, request, timeOut);

        /// <summary>
        /// 异步将企业客户标签 ID 转换为服务商范围内的标签 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/95926"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/96169"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的企业客户标签 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>标签 ID 转换结果和无效标签 ID 列表。</returns>
        public static Task<ExternalTagIdConvertResult> ConvertExternalTagIdAsync(
            string accessTokenOrAppKey, ExternalTagIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExternalTagIdConvertResult>(accessTokenOrAppKey,
                ExternalTagIdPath, request, timeOut);

        /// <summary>
        /// 将企业客服账号 ID 转换为服务商范围内的新客服账号 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/97064"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/96169"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的客服账号 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客服账号 ID 转换结果和无效账号 ID 列表。</returns>
        public static OpenKfIdConvertResult ConvertOpenKfId(
            string accessTokenOrAppKey, OpenKfIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<OpenKfIdConvertResult>(accessTokenOrAppKey,
                OpenKfIdPath, request, timeOut);

        /// <summary>
        /// 异步将企业客服账号 ID 转换为服务商范围内的新客服账号 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/97064"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/96169"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">待转换的客服账号 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>客服账号 ID 转换结果和无效账号 ID 列表。</returns>
        public static Task<OpenKfIdConvertResult> ConvertOpenKfIdAsync(
            string accessTokenOrAppKey, OpenKfIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<OpenKfIdConvertResult>(accessTokenOrAppKey,
                OpenKfIdPath, request, timeOut);

        /// <summary>
        /// 申请在指定时间前完成群聊 ID 升级。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">计划完成升级的 Unix 时间戳。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>申请结果。</returns>
        public static ApplyToUpgradeChatIdResult ApplyToUpgradeChatId(
            string accessTokenOrAppKey, ApplyToUpgradeChatIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ApplyToUpgradeChatIdResult>(accessTokenOrAppKey,
                ApplyToUpgradeChatIdPath, request, timeOut);

        /// <summary>
        /// 异步申请在指定时间前完成群聊 ID 升级。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">计划完成升级的 Unix 时间戳。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>申请结果。</returns>
        public static Task<ApplyToUpgradeChatIdResult> ApplyToUpgradeChatIdAsync(
            string accessTokenOrAppKey, ApplyToUpgradeChatIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ApplyToUpgradeChatIdResult>(accessTokenOrAppKey,
                ApplyToUpgradeChatIdPath, request, timeOut);

        /// <summary>
        /// 批量获取升级后的群聊 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">升级前的群聊 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新旧群聊 ID 对应关系和无效群聊 ID 列表。</returns>
        public static ChatIdConvertResult ConvertChatId(
            string accessTokenOrAppKey, ChatIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ChatIdConvertResult>(accessTokenOrAppKey,
                ChatIdPath, request, timeOut);

        /// <summary>
        /// 异步批量获取升级后的群聊 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">升级前的群聊 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新旧群聊 ID 对应关系和无效群聊 ID 列表。</returns>
        public static Task<ChatIdConvertResult> ConvertChatIdAsync(
            string accessTokenOrAppKey, ChatIdConvertRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatIdConvertResult>(accessTokenOrAppKey,
                ChatIdPath, request, timeOut);

        /// <summary>
        /// 使用第三方应用套件凭证为新企业升级群聊 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="suiteAccessToken">第三方应用套件的 SuiteAccessToken。</param>
        /// <param name="request">接口请求参数；当前协议请求体为空对象。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新企业群聊 ID 升级结果。</returns>
        public static UpgradeChatIdForNewCorpResult UpgradeChatIdForNewCorp(
            string suiteAccessToken, UpgradeChatIdForNewCorpRequest request,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiWorkHost}{UpgradeChatIdForNewCorpPath}" +
                      $"?suite_access_token={suiteAccessToken.AsUrlData()}";
            return CommonJsonSend.Send<UpgradeChatIdForNewCorpResult>(null, url,
                request, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 异步使用第三方应用套件凭证为新企业升级群聊 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99601"/>
        /// </summary>
        /// <param name="suiteAccessToken">第三方应用套件的 SuiteAccessToken。</param>
        /// <param name="request">接口请求参数；当前协议请求体为空对象。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新企业群聊 ID 升级结果。</returns>
        public static Task<UpgradeChatIdForNewCorpResult> UpgradeChatIdForNewCorpAsync(
            string suiteAccessToken, UpgradeChatIdForNewCorpRequest request,
            int timeOut = Config.TIME_OUT)
        {
            var url = $"{Config.ApiWorkHost}{UpgradeChatIdForNewCorpPath}" +
                      $"?suite_access_token={suiteAccessToken.AsUrlData()}";
            return CommonJsonSend.SendAsync<UpgradeChatIdForNewCorpResult>(null, url,
                request, CommonJsonSendType.POST, timeOut);
        }

        private static T Post<T>(string accessTokenOrAppKey, string path,
            object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path,
            object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
    }
}
