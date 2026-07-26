/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ContactP1Api.cs
    文件功能描述：ContactP1Api 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Contact
{
    /// <summary>
    /// 企业微信账号 ID、通讯录查看权限及异步导出接口。
    /// </summary>
    public static class ContactP1Api
    {
        /// <summary>
        /// 转换临时外部联系人 ID。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ConvertTemporaryExternalUserIdResult ConvertTemporaryExternalUserId(
            string accessTokenOrAppKey, ConvertTemporaryExternalUserIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<ConvertTemporaryExternalUserIdResult>(accessTokenOrAppKey,
                "/cgi-bin/idconvert/convert_tmp_external_userid", request, timeOut);

        /// <summary>
        /// 异步转换临时外部联系人 ID。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ConvertTemporaryExternalUserIdResult> ConvertTemporaryExternalUserIdAsync(
            string accessTokenOrAppKey, ConvertTemporaryExternalUserIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ConvertTemporaryExternalUserIdResult>(accessTokenOrAppKey,
                "/cgi-bin/idconvert/convert_tmp_external_userid", request, timeOut);

        /// <summary>
        /// 创建通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CreateContactRuleResult CreateContactRules(string accessTokenOrAppKey,
            ContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => Post<CreateContactRuleResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/create", request, timeOut);

        /// <summary>
        /// 异步创建通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<CreateContactRuleResult> CreateContactRulesAsync(string accessTokenOrAppKey,
            ContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CreateContactRuleResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/create", request, timeOut);

        /// <summary>
        /// 获取通讯录查看权限规则列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetContactRuleListResult GetContactRuleList(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Post<GetContactRuleListResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/list", new { }, timeOut);

        /// <summary>
        /// 异步获取通讯录查看权限规则列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetContactRuleListResult> GetContactRuleListAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetContactRuleListResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/list", new { }, timeOut);

        /// <summary>
        /// 更新通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpdateContactRules(string accessTokenOrAppKey,
            ContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/update", request, timeOut);

        /// <summary>
        /// 异步更新通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpdateContactRulesAsync(string accessTokenOrAppKey,
            ContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/update", request, timeOut);

        /// <summary>
        /// 删除通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult DeleteContactRules(string accessTokenOrAppKey,
            DeleteContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/delete", request, timeOut);

        /// <summary>
        /// 异步删除通讯录查看权限规则。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> DeleteContactRulesAsync(string accessTokenOrAppKey,
            DeleteContactRuleRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, "/cgi-bin/contactrule/delete", request, timeOut);

        /// <summary>
        /// 导出成员基础信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ExportContactJobResult ExportMembers(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/simple_user", request, timeOut);

        /// <summary>
        /// 异步导出成员基础信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ExportContactJobResult> ExportMembersAsync(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/simple_user", request, timeOut);

        /// <summary>
        /// 导出成员详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ExportContactJobResult ExportMemberDetails(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/user", request, timeOut);

        /// <summary>
        /// 异步导出成员详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ExportContactJobResult> ExportMemberDetailsAsync(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/user", request, timeOut);

        /// <summary>
        /// 导出部门数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ExportContactJobResult ExportDepartments(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/department", request, timeOut);

        /// <summary>
        /// 异步导出部门数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ExportContactJobResult> ExportDepartmentsAsync(string accessTokenOrAppKey,
            ExportContactRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/department", request, timeOut);

        /// <summary>
        /// 导出标签成员数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ExportContactJobResult ExportTagMembers(string accessTokenOrAppKey,
            ExportTagContactRequest request, int timeOut = Config.TIME_OUT)
            => Post<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/taguser", request, timeOut);

        /// <summary>
        /// 异步导出标签成员数据。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ExportContactJobResult> ExportTagMembersAsync(string accessTokenOrAppKey,
            ExportTagContactRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ExportContactJobResult>(accessTokenOrAppKey, "/cgi-bin/export/taguser", request, timeOut);

        /// <summary>
        /// 查询通讯录导出结果。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="jobId">异步任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetExportContactResult GetExportResult(string accessTokenOrAppKey, string jobId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/export/get_result?access_token={accessToken.AsUrlData()}&jobid={jobId.AsUrlData()}";
                return CommonJsonSend.Send<GetExportContactResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 异步查询通讯录导出结果。
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证或已注册的 AppKey。</param>
        /// <param name="jobId">异步任务 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetExportContactResult> GetExportResultAsync(string accessTokenOrAppKey, string jobId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/export/get_result?access_token={accessToken.AsUrlData()}&jobid={jobId.AsUrlData()}";
                return await CommonJsonSend.SendAsync<GetExportContactResult>(null, url, null,
                    CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

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
    }
}
