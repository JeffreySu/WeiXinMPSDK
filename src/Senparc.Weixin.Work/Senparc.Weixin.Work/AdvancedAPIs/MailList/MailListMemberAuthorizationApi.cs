/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MailListMemberAuthorizationApi.cs
    文件功能描述：企业微信成员授权与二次验证接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐成员授权、选人结果和二次验证接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信成员授权与二次验证接口扩展。
    /// </summary>
    public static partial class MailListApi
    {
        private const string GetMemberAuthListPath = "/cgi-bin/user/list_member_auth";
        private const string CheckMemberAuthPath = "/cgi-bin/user/check_member_auth";
        private const string GetSelectedTicketUsersPath = "/cgi-bin/user/list_selected_ticket_user";
        private const string SetTfaSuccessPath = "/cgi-bin/user/tfa_succ";

        /// <summary>
        /// 获取成员授权列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94513">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">分页查询参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员授权列表和下一页游标。</returns>
        public static GetMemberAuthListResult GetMemberAuthList(string accessTokenOrAppKey,
            GetMemberAuthListRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<GetMemberAuthListResult>(
                accessToken, Config.ApiWorkHost + GetMemberAuthListPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取成员授权列表。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94513">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">分页查询参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员授权列表和下一页游标。</returns>
        public static Task<GetMemberAuthListResult> GetMemberAuthListAsync(string accessTokenOrAppKey,
            GetMemberAuthListRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<GetMemberAuthListResult>(
                accessToken, Config.ApiWorkHost + GetMemberAuthListPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 查询成员用户是否已授权当前第三方应用。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94514">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询成员的第三方成员唯一标识。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员是否已授权。</returns>
        public static CheckMemberAuthResult CheckMemberAuth(string accessTokenOrAppKey,
            CheckMemberAuthRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<CheckMemberAuthResult>(
                accessToken, Config.ApiWorkHost + CheckMemberAuthPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 异步查询成员用户是否已授权当前第三方应用。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94514">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待查询成员的第三方成员唯一标识。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成员是否已授权。</returns>
        public static Task<CheckMemberAuthResult> CheckMemberAuthAsync(string accessTokenOrAppKey,
            CheckMemberAuthRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<CheckMemberAuthResult>(
                accessToken, Config.ApiWorkHost + CheckMemberAuthPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 获取选人 JSAPI 的 SelectedTicket 对应的成员。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94894">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">选人 JSAPI 返回的 SelectedTicket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>操作成员、可见成员、未授权成员和选择总数。</returns>
        public static GetSelectedTicketUsersResult GetSelectedTicketUsers(string accessTokenOrAppKey,
            GetSelectedTicketUsersRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<GetSelectedTicketUsersResult>(
                accessToken, Config.ApiWorkHost + GetSelectedTicketUsersPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取选人 JSAPI 的 SelectedTicket 对应的成员。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94894">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">选人 JSAPI 返回的 SelectedTicket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>操作成员、可见成员、未授权成员和选择总数。</returns>
        public static Task<GetSelectedTicketUsersResult> GetSelectedTicketUsersAsync(string accessTokenOrAppKey,
            GetSelectedTicketUsersRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<GetSelectedTicketUsersResult>(
                accessToken, Config.ApiWorkHost + GetSelectedTicketUsersPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 通知企业微信成员已完成二次验证。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/99500">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员账号和二次验证授权码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二次验证完成结果。</returns>
        public static WorkJsonResult SetTfaSuccess(string accessTokenOrAppKey, TfaSuccessRequest request,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<WorkJsonResult>(
                accessToken, Config.ApiWorkHost + SetTfaSuccessPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 异步通知企业微信成员已完成二次验证。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/99500">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员账号和二次验证授权码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>二次验证完成结果。</returns>
        public static Task<WorkJsonResult> SetTfaSuccessAsync(string accessTokenOrAppKey,
            TfaSuccessRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<WorkJsonResult>(
                accessToken, Config.ApiWorkHost + SetTfaSuccessPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);
    }
}
