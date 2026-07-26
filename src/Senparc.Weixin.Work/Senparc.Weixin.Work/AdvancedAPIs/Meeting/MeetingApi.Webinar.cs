/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Webinar.cs
    文件功能描述：企业微信会议网络研讨会接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐网络研讨会及报名管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string CreateWebinarPath = "/cgi-bin/meeting/webinar/create";
        private const string UpdateWebinarPath = "/cgi-bin/meeting/webinar/update";
        private const string CancelWebinarPath = "/cgi-bin/meeting/webinar/cancel";
        private const string GetWebinarPath = "/cgi-bin/meeting/webinar/get";
        private const string GetWebinarGuestsPath = "/cgi-bin/meeting/webinar/list_guest";
        private const string UpdateWebinarGuestsPath =
            "/cgi-bin/meeting/webinar/update_guest_list";
        private const string UpdateWebinarWarmUpPath = "/cgi-bin/meeting/webinar/update_warm_up";
        private const string SetWebinarEnrollmentConfigPath =
            "/cgi-bin/meeting/webinar/enroll/set_config";
        private const string GetWebinarEnrollmentConfigPath =
            "/cgi-bin/meeting/webinar/enroll/get_config";
        private const string QueryWebinarEnrollmentsPath =
            "/cgi-bin/meeting/webinar/enroll/query_by_tmp_openid";
        private const string GetWebinarEnrollmentsPath =
            "/cgi-bin/meeting/webinar/enroll/list";
        private const string ApproveWebinarEnrollmentsPath =
            "/cgi-bin/meeting/webinar/enroll/approve";
        private const string ImportWebinarEnrollmentsPath =
            "/cgi-bin/meeting/webinar/enroll/import";
        private const string DeleteWebinarEnrollmentsPath =
            "/cgi-bin/meeting/webinar/enroll/delete";

        /// <summary>
        /// 创建网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98842"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">管理员、时间、主持人、入会和录制设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID、会议号和观众、嘉宾、人工审核入口。</returns>
        public static CreateWebinarResult CreateWebinar(string accessTokenOrAppKey,
            CreateWebinarRequest request, int timeOut = Config.TIME_OUT)
            => Post<CreateWebinarResult>(accessTokenOrAppKey, CreateWebinarPath,
                request, timeOut);

        /// <summary>
        /// 异步创建网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98842"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">管理员、时间、主持人、入会和录制设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议 ID、会议号和观众、嘉宾、人工审核入口。</returns>
        public static Task<CreateWebinarResult> CreateWebinarAsync(string accessTokenOrAppKey,
            CreateWebinarRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CreateWebinarResult>(accessTokenOrAppKey, CreateWebinarPath,
                request, timeOut);

        /// <summary>
        /// 更新网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98843"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要更新的网络研讨会字段。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static WorkJsonResult UpdateWebinar(string accessTokenOrAppKey,
            UpdateWebinarRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarPath, request, timeOut);

        /// <summary>
        /// 异步更新网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98843"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要更新的网络研讨会字段。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static Task<WorkJsonResult> UpdateWebinarAsync(string accessTokenOrAppKey,
            UpdateWebinarRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarPath,
                request, timeOut);

        /// <summary>
        /// 取消网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98843"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要取消的会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消结果。</returns>
        public static WorkJsonResult CancelWebinar(string accessTokenOrAppKey,
            CancelWebinarRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CancelWebinarPath, request, timeOut);

        /// <summary>
        /// 异步取消网络研讨会。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98843"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要取消的会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消结果。</returns>
        public static Task<WorkJsonResult> CancelWebinarAsync(string accessTokenOrAppKey,
            CancelWebinarRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CancelWebinarPath,
                request, timeOut);

        /// <summary>
        /// 获取网络研讨会详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98860"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 或会议号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网络研讨会详情和参会、审核、回放入口。</returns>
        public static GetWebinarResult GetWebinar(string accessTokenOrAppKey,
            GetWebinarRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetWebinarResult>(accessTokenOrAppKey, GetWebinarPath, request, timeOut);

        /// <summary>
        /// 异步获取网络研讨会详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98860"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 或会议号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网络研讨会详情和参会、审核、回放入口。</returns>
        public static Task<GetWebinarResult> GetWebinarAsync(string accessTokenOrAppKey,
            GetWebinarRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetWebinarResult>(accessTokenOrAppKey, GetWebinarPath,
                request, timeOut);

        /// <summary>
        /// 获取网络研讨会嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98871"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 或会议号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业成员、手机号或邮箱形式的嘉宾列表。</returns>
        public static GetWebinarGuestsResult GetWebinarGuests(string accessTokenOrAppKey,
            GetWebinarGuestsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetWebinarGuestsResult>(accessTokenOrAppKey, GetWebinarGuestsPath,
                request, timeOut);

        /// <summary>
        /// 异步获取网络研讨会嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98871"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 或会议号。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业成员、手机号或邮箱形式的嘉宾列表。</returns>
        public static Task<GetWebinarGuestsResult> GetWebinarGuestsAsync(
            string accessTokenOrAppKey, GetWebinarGuestsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetWebinarGuestsResult>(accessTokenOrAppKey, GetWebinarGuestsPath,
                request, timeOut);

        /// <summary>
        /// 更新网络研讨会嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98872"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和替换后的嘉宾列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static WorkJsonResult UpdateWebinarGuests(string accessTokenOrAppKey,
            UpdateWebinarGuestsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarGuestsPath,
                request, timeOut);

        /// <summary>
        /// 异步更新网络研讨会嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98872"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和替换后的嘉宾列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static Task<WorkJsonResult> UpdateWebinarGuestsAsync(string accessTokenOrAppKey,
            UpdateWebinarGuestsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarGuestsPath,
                request, timeOut);

        /// <summary>
        /// 更新网络研讨会暖场配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98882"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、暖场图片、视频和邀请设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static WorkJsonResult UpdateWebinarWarmUp(string accessTokenOrAppKey,
            UpdateWebinarWarmUpRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarWarmUpPath,
                request, timeOut);

        /// <summary>
        /// 异步更新网络研讨会暖场配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98882"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、暖场图片、视频和邀请设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static Task<WorkJsonResult> UpdateWebinarWarmUpAsync(string accessTokenOrAppKey,
            UpdateWebinarWarmUpRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateWebinarWarmUpPath,
                request, timeOut);

        /// <summary>
        /// 设置网络研讨会报名配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98875"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">报名审批、问题和企业成员免报名设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>保存后的报名问题数量。</returns>
        public static SetWebinarEnrollmentConfigResult SetWebinarEnrollmentConfig(
            string accessTokenOrAppKey, SetWebinarEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetWebinarEnrollmentConfigResult>(accessTokenOrAppKey,
                SetWebinarEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 异步设置网络研讨会报名配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98875"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">报名审批、问题和企业成员免报名设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>保存后的报名问题数量。</returns>
        public static Task<SetWebinarEnrollmentConfigResult> SetWebinarEnrollmentConfigAsync(
            string accessTokenOrAppKey, SetWebinarEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetWebinarEnrollmentConfigResult>(accessTokenOrAppKey,
                SetWebinarEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 获取网络研讨会报名配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98874"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名审批、问题和企业成员免报名配置。</returns>
        public static GetWebinarEnrollmentConfigResult GetWebinarEnrollmentConfig(
            string accessTokenOrAppKey, GetWebinarEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetWebinarEnrollmentConfigResult>(accessTokenOrAppKey,
                GetWebinarEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 异步获取网络研讨会报名配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98874"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名审批、问题和企业成员免报名配置。</returns>
        public static Task<GetWebinarEnrollmentConfigResult> GetWebinarEnrollmentConfigAsync(
            string accessTokenOrAppKey, GetWebinarEnrollmentConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetWebinarEnrollmentConfigResult>(accessTokenOrAppKey,
                GetWebinarEnrollmentConfigPath, request, timeOut);

        /// <summary>
        /// 根据临时 OpenId 查询网络研讨会报名 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98873"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、排序规则和临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>临时 OpenId 与报名 ID 对应关系列表。</returns>
        public static QueryMeetingEnrollmentsByTempOpenIdsResult QueryWebinarEnrollmentsByTempOpenIds(
            string accessTokenOrAppKey, QueryMeetingEnrollmentsByTempOpenIdsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<QueryMeetingEnrollmentsByTempOpenIdsResult>(accessTokenOrAppKey,
                QueryWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步根据临时 OpenId 查询网络研讨会报名 ID。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98873"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、排序规则和临时 OpenId 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>临时 OpenId 与报名 ID 对应关系列表。</returns>
        public static Task<QueryMeetingEnrollmentsByTempOpenIdsResult>
            QueryWebinarEnrollmentsByTempOpenIdsAsync(string accessTokenOrAppKey,
                QueryMeetingEnrollmentsByTempOpenIdsRequest request,
                int timeOut = Config.TIME_OUT)
            => PostAsync<QueryMeetingEnrollmentsByTempOpenIdsResult>(accessTokenOrAppKey,
                QueryWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 分页获取网络研讨会报名列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98876"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批状态和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名记录和下一页游标。</returns>
        public static GetMeetingEnrollmentsResult GetWebinarEnrollments(
            string accessTokenOrAppKey, GetMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingEnrollmentsResult>(accessTokenOrAppKey,
                GetWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取网络研讨会报名列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98876"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批状态和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>报名记录和下一页游标。</returns>
        public static Task<GetMeetingEnrollmentsResult> GetWebinarEnrollmentsAsync(
            string accessTokenOrAppKey, GetMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingEnrollmentsResult>(accessTokenOrAppKey,
                GetWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量审批网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98877"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批动作和报名 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功处理的报名数量。</returns>
        public static ApproveMeetingEnrollmentsResult ApproveWebinarEnrollments(
            string accessTokenOrAppKey, ApproveMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ApproveMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ApproveWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量审批网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98877"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、审批动作和报名 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功处理的报名数量。</returns>
        public static Task<ApproveMeetingEnrollmentsResult> ApproveWebinarEnrollmentsAsync(
            string accessTokenOrAppKey, ApproveMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ApproveMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ApproveWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量导入网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98880"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待导入报名人列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功导入的报名记录。</returns>
        public static ImportMeetingEnrollmentsResult ImportWebinarEnrollments(
            string accessTokenOrAppKey, ImportMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ImportMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ImportWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量导入网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98880"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待导入报名人列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功导入的报名记录。</returns>
        public static Task<ImportMeetingEnrollmentsResult> ImportWebinarEnrollmentsAsync(
            string accessTokenOrAppKey, ImportMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ImportMeetingEnrollmentsResult>(accessTokenOrAppKey,
                ImportWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 批量删除网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98881"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待删除报名 ID 对象列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功删除的报名数量。</returns>
        public static DeleteMeetingEnrollmentsResult DeleteWebinarEnrollments(
            string accessTokenOrAppKey, DeleteMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingEnrollmentsResult>(accessTokenOrAppKey,
                DeleteWebinarEnrollmentsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除网络研讨会报名。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98881"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和待删除报名 ID 对象列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功删除的报名数量。</returns>
        public static Task<DeleteMeetingEnrollmentsResult> DeleteWebinarEnrollmentsAsync(
            string accessTokenOrAppKey, DeleteMeetingEnrollmentsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingEnrollmentsResult>(accessTokenOrAppKey,
                DeleteWebinarEnrollmentsPath, request, timeOut);
    }
}
