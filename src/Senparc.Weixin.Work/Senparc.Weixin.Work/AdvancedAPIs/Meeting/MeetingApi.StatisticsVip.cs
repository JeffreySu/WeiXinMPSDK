/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.StatisticsVip.cs
    文件功能描述：企业微信会议发起统计与高级账号管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议发起统计和高级账号批量管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string GetMeetingStartStatisticsPath =
            "/cgi-bin/meeting/statistics/get_start_list";
        private const string SubmitMeetingVipBatchAddJobPath =
            "/cgi-bin/meeting/vip/submit_batch_add_job";
        private const string GetMeetingVipBatchAddJobResultPath =
            "/cgi-bin/meeting/vip/batch_add_job_result";
        private const string SubmitMeetingVipBatchDeleteJobPath =
            "/cgi-bin/meeting/vip/submit_batch_del_job";
        private const string GetMeetingVipBatchDeleteJobResultPath =
            "/cgi-bin/meeting/vip/batch_del_job_result";
        private const string GetMeetingVipListPath = "/cgi-bin/meeting/vip/list";

        /// <summary>
        /// 分页获取企业成员发起会议的统计记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99651"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">统计类型、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议发起成员、发起时间和下一页游标。</returns>
        public static GetMeetingStartStatisticsResult GetMeetingStartStatistics(
            string accessTokenOrAppKey, GetMeetingStartStatisticsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingStartStatisticsResult>(accessTokenOrAppKey,
                GetMeetingStartStatisticsPath, request, timeOut);

        /// <summary>
        /// 异步分页获取企业成员发起会议的统计记录。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99651"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">统计类型、时间范围和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议发起成员、发起时间和下一页游标。</returns>
        public static Task<GetMeetingStartStatisticsResult> GetMeetingStartStatisticsAsync(
            string accessTokenOrAppKey, GetMeetingStartStatisticsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingStartStatisticsResult>(accessTokenOrAppKey,
                GetMeetingStartStatisticsPath, request, timeOut);

        /// <summary>
        /// 提交批量分配企业微信会议高级账号任务。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99508"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要分配会议高级账号的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步任务 ID 和无效成员列表。</returns>
        public static SubmitMeetingVipBatchAddJobResult SubmitMeetingVipBatchAddJob(
            string accessTokenOrAppKey, SubmitMeetingVipBatchAddJobRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SubmitMeetingVipBatchAddJobResult>(accessTokenOrAppKey,
                SubmitMeetingVipBatchAddJobPath, request, timeOut);

        /// <summary>
        /// 异步提交批量分配企业微信会议高级账号任务。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99508"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要分配会议高级账号的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步任务 ID 和无效成员列表。</returns>
        public static Task<SubmitMeetingVipBatchAddJobResult> SubmitMeetingVipBatchAddJobAsync(
            string accessTokenOrAppKey, SubmitMeetingVipBatchAddJobRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SubmitMeetingVipBatchAddJobResult>(accessTokenOrAppKey,
                SubmitMeetingVipBatchAddJobPath, request, timeOut);

        /// <summary>
        /// 查询批量分配企业微信会议高级账号任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99508"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的异步任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分配成功和失败的成员列表。</returns>
        public static GetMeetingVipBatchAddJobResultResult GetMeetingVipBatchAddJobResult(
            string accessTokenOrAppKey, GetMeetingVipBatchAddJobResultRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingVipBatchAddJobResultResult>(accessTokenOrAppKey,
                GetMeetingVipBatchAddJobResultPath, request, timeOut);

        /// <summary>
        /// 异步查询批量分配企业微信会议高级账号任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99508"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的异步任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分配成功和失败的成员列表。</returns>
        public static Task<GetMeetingVipBatchAddJobResultResult>
            GetMeetingVipBatchAddJobResultAsync(string accessTokenOrAppKey,
                GetMeetingVipBatchAddJobResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingVipBatchAddJobResultResult>(accessTokenOrAppKey,
                GetMeetingVipBatchAddJobResultPath, request, timeOut);

        /// <summary>
        /// 提交批量撤销企业微信会议高级账号任务。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99509"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要撤销会议高级账号的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步任务 ID 和无效成员列表。</returns>
        public static SubmitMeetingVipBatchDeleteJobResult SubmitMeetingVipBatchDeleteJob(
            string accessTokenOrAppKey, SubmitMeetingVipBatchDeleteJobRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SubmitMeetingVipBatchDeleteJobResult>(accessTokenOrAppKey,
                SubmitMeetingVipBatchDeleteJobPath, request, timeOut);

        /// <summary>
        /// 异步提交批量撤销企业微信会议高级账号任务。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99509"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要撤销会议高级账号的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步任务 ID 和无效成员列表。</returns>
        public static Task<SubmitMeetingVipBatchDeleteJobResult>
            SubmitMeetingVipBatchDeleteJobAsync(string accessTokenOrAppKey,
                SubmitMeetingVipBatchDeleteJobRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SubmitMeetingVipBatchDeleteJobResult>(accessTokenOrAppKey,
                SubmitMeetingVipBatchDeleteJobPath, request, timeOut);

        /// <summary>
        /// 查询批量撤销企业微信会议高级账号任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99509"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的异步任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>撤销成功和失败的成员列表。</returns>
        public static GetMeetingVipBatchDeleteJobResultResult GetMeetingVipBatchDeleteJobResult(
            string accessTokenOrAppKey, GetMeetingVipBatchDeleteJobResultRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingVipBatchDeleteJobResultResult>(accessTokenOrAppKey,
                GetMeetingVipBatchDeleteJobResultPath, request, timeOut);

        /// <summary>
        /// 异步查询批量撤销企业微信会议高级账号任务结果。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99509"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的异步任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>撤销成功和失败的成员列表。</returns>
        public static Task<GetMeetingVipBatchDeleteJobResultResult>
            GetMeetingVipBatchDeleteJobResultAsync(string accessTokenOrAppKey,
                GetMeetingVipBatchDeleteJobResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingVipBatchDeleteJobResultResult>(accessTokenOrAppKey,
                GetMeetingVipBatchDeleteJobResultPath, request, timeOut);

        /// <summary>
        /// 分页获取已分配企业微信会议高级账号的成员列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99510"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分页游标和每页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级账号成员列表、更多数据标记和下一页游标。</returns>
        public static GetMeetingVipListResult GetMeetingVipList(string accessTokenOrAppKey,
            GetMeetingVipListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingVipListResult>(accessTokenOrAppKey,
                GetMeetingVipListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取已分配企业微信会议高级账号的成员列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99510"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分页游标和每页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>高级账号成员列表、更多数据标记和下一页游标。</returns>
        public static Task<GetMeetingVipListResult> GetMeetingVipListAsync(
            string accessTokenOrAppKey, GetMeetingVipListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingVipListResult>(accessTokenOrAppKey,
                GetMeetingVipListPath, request, timeOut);
    }
}
