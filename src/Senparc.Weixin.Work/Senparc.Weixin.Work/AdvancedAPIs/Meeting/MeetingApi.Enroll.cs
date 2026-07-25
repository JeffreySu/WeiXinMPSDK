/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Enroll.cs
    文件功能描述：企业微信会议报名管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议报名查询、审批、导入和删除接口

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议报名配置读取和设置接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string SetEnrollmentConfigPath = "/cgi-bin/meeting/enroll/set_config";
        private const string GetEnrollmentConfigPath = "/cgi-bin/meeting/enroll/get_config";
        private const string QueryEnrollmentsByTempOpenIdsPath =
            "/cgi-bin/meeting/enroll/query_by_tmp_openid";
        private const string GetEnrollmentsPath = "/cgi-bin/meeting/enroll/list";
        private const string ApproveEnrollmentsPath = "/cgi-bin/meeting/enroll/approve";
        private const string ImportEnrollmentsPath = "/cgi-bin/meeting/enroll/import";
        private const string DeleteEnrollmentsPath = "/cgi-bin/meeting/enroll/delete";

        /// <summary>
        /// 设置会议报名审批、问题和企业成员免报名配置。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98821，但企业微信公开站点当前将该编号展示为其他会议文档；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、报名审批方式和问题配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>保存后的报名问题数量。</returns>
        public static SetMeetingEnrollmentConfigResult SetMeetingEnrollmentConfig(
            string accessTokenOrAppKey, SetMeetingEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetMeetingEnrollmentConfigResult>(accessTokenOrAppKey,
                SetEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 异步设置会议报名审批、问题和企业成员免报名配置。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98821，但企业微信公开站点当前将该编号展示为其他会议文档；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、报名审批方式和问题配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>保存后的报名问题数量。</returns>
        public static Task<SetMeetingEnrollmentConfigResult> SetMeetingEnrollmentConfigAsync(
            string accessTokenOrAppKey, SetMeetingEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingEnrollmentConfigResult>(accessTokenOrAppKey,
                SetEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 获取会议报名审批、问题和企业成员免报名配置。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98821，但企业微信公开站点当前将该编号展示为其他会议文档；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询报名配置的会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名审批方式、问题和企业成员免报名配置。</returns>
        public static GetMeetingEnrollmentConfigResult GetMeetingEnrollmentConfig(
            string accessTokenOrAppKey, GetMeetingEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingEnrollmentConfigResult>(accessTokenOrAppKey,
                GetEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 异步获取会议报名审批、问题和企业成员免报名配置。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98821，但企业微信公开站点当前将该编号展示为其他会议文档；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询报名配置的会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名审批方式、问题和企业成员免报名配置。</returns>
        public static Task<GetMeetingEnrollmentConfigResult> GetMeetingEnrollmentConfigAsync(
            string accessTokenOrAppKey, GetMeetingEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingEnrollmentConfigResult>(accessTokenOrAppKey,
                GetEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 根据会议成员临时 OpenId 批量查询报名 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98794"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、排序方式和临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>临时 OpenId 与报名 ID 的对应关系。</returns>
        public static QueryMeetingEnrollmentsByTempOpenIdsResult QueryMeetingEnrollmentsByTempOpenIds(
            string accessTokenOrAppKey, QueryMeetingEnrollmentsByTempOpenIdsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<QueryMeetingEnrollmentsByTempOpenIdsResult>(accessTokenOrAppKey,
                QueryEnrollmentsByTempOpenIdsPath, request, timeOut);

        /// <summary>
        /// 异步根据会议成员临时 OpenId 批量查询报名 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98794"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、排序方式和临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>临时 OpenId 与报名 ID 的对应关系。</returns>
        public static Task<QueryMeetingEnrollmentsByTempOpenIdsResult> QueryMeetingEnrollmentsByTempOpenIdsAsync(
            string accessTokenOrAppKey, QueryMeetingEnrollmentsByTempOpenIdsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<QueryMeetingEnrollmentsByTempOpenIdsResult>(accessTokenOrAppKey,
                QueryEnrollmentsByTempOpenIdsPath, request, timeOut);

        /// <summary>
        /// 分页获取会议报名列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98810"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、可选审批状态和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名人、报名状态及问题答案列表。</returns>
        public static GetMeetingEnrollmentsResult GetMeetingEnrollments(string accessTokenOrAppKey,
            GetMeetingEnrollmentsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingEnrollmentsResult>(accessTokenOrAppKey, GetEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取会议报名列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98810"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、可选审批状态和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名人、报名状态及问题答案列表。</returns>
        public static Task<GetMeetingEnrollmentsResult> GetMeetingEnrollmentsAsync(string accessTokenOrAppKey,
            GetMeetingEnrollmentsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingEnrollmentsResult>(accessTokenOrAppKey,
                GetEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量审批会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98807"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批动作和报名 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功处理的报名数量。</returns>
        public static ApproveMeetingEnrollmentsResult ApproveMeetingEnrollments(string accessTokenOrAppKey,
            ApproveMeetingEnrollmentsRequest request, int timeOut = Config.TIME_OUT)
            => Post<ApproveMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ApproveEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量审批会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98807"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批动作和报名 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功处理的报名数量。</returns>
        public static Task<ApproveMeetingEnrollmentsResult> ApproveMeetingEnrollmentsAsync(
            string accessTokenOrAppKey, ApproveMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ApproveMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ApproveEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量导入会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98816"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待导入的企业成员或手机号报名人。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功导入数量、报名 ID 和报名码。</returns>
        public static ImportMeetingEnrollmentsResult ImportMeetingEnrollments(string accessTokenOrAppKey,
            ImportMeetingEnrollmentsRequest request, int timeOut = Config.TIME_OUT)
            => Post<ImportMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ImportEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量导入会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98816"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待导入的企业成员或手机号报名人。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功导入数量、报名 ID 和报名码。</returns>
        public static Task<ImportMeetingEnrollmentsResult> ImportMeetingEnrollmentsAsync(
            string accessTokenOrAppKey, ImportMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ImportMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ImportEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量删除会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98817"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待删除的报名 ID 对象列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功删除的报名数量。</returns>
        public static DeleteMeetingEnrollmentsResult DeleteMeetingEnrollments(string accessTokenOrAppKey,
            DeleteMeetingEnrollmentsRequest request, int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingEnrollmentsResult>(accessTokenOrAppKey,
                DeleteEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除会议报名记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98817"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待删除的报名 ID 对象列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功删除的报名数量。</returns>
        public static Task<DeleteMeetingEnrollmentsResult> DeleteMeetingEnrollmentsAsync(
            string accessTokenOrAppKey, DeleteMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingEnrollmentsResult>(accessTokenOrAppKey,
                DeleteEnrollmentsPath, request, timeOut);
    }
}
