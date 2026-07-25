/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportApi.Patrol.cs
    文件功能描述：企业微信政民沟通巡查上报接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐政民沟通巡查上报 6 项查询与统计接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    public static partial class ReportApi
    {
        private const string GetPatrolGridInfoPath = "/cgi-bin/report/patrol/get_grid_info";
        private const string GetPatrolCorpStatusPath = "/cgi-bin/report/patrol/get_corp_status";
        private const string GetPatrolUserStatusPath = "/cgi-bin/report/patrol/get_user_status";
        private const string GetPatrolCategoryStatisticsPath = "/cgi-bin/report/patrol/category_statistic";
        private const string GetPatrolOrderListPath = "/cgi-bin/report/patrol/get_order_list";
        private const string GetPatrolOrderInfoPath = "/cgi-bin/report/patrol/get_order_info";

        /// <summary>
        /// 获取政民沟通巡查上报可见的网格信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93531"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格及管理员列表。</returns>
        public static PatrolReportGridInfoResult GetPatrolGridInfo(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<PatrolReportGridInfoResult>(
                    accessToken, Config.ApiWorkHost + GetPatrolGridInfoPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 异步获取政民沟通巡查上报可见的网格信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93531"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格及管理员列表。</returns>
        public static Task<PatrolReportGridInfoResult> GetPatrolGridInfoAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<PatrolReportGridInfoResult>(
                    accessToken, Config.ApiWorkHost + GetPatrolGridInfoPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 获取企业的巡查事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93532"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选网格范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>待分配、办理中、今日和累计统计。</returns>
        public static PatrolReportCorpStatusResult GetPatrolCorpStatus(string accessTokenOrAppKey,
            PatrolReportCorpStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<PatrolReportCorpStatusResult>(accessTokenOrAppKey, GetPatrolCorpStatusPath, request, timeOut);

        /// <summary>
        /// 异步获取企业的巡查事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93532"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选网格范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>待分配、办理中、今日和累计统计。</returns>
        public static Task<PatrolReportCorpStatusResult> GetPatrolCorpStatusAsync(string accessTokenOrAppKey,
            PatrolReportCorpStatusRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<PatrolReportCorpStatusResult>(accessTokenOrAppKey, GetPatrolCorpStatusPath, request, timeOut);

        /// <summary>
        /// 获取成员的巡查事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93533"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员的办理中和今日统计。</returns>
        public static PatrolReportUserStatusResult GetPatrolUserStatus(string accessTokenOrAppKey,
            PatrolReportUserStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<PatrolReportUserStatusResult>(accessTokenOrAppKey, GetPatrolUserStatusPath, request, timeOut);

        /// <summary>
        /// 异步获取成员的巡查事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93533"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员的办理中和今日统计。</returns>
        public static Task<PatrolReportUserStatusResult> GetPatrolUserStatusAsync(string accessTokenOrAppKey,
            PatrolReportUserStatusRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<PatrolReportUserStatusResult>(accessTokenOrAppKey, GetPatrolUserStatusPath, request, timeOut);

        /// <summary>
        /// 获取巡查事件分类统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93534"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的事件分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分类层级、类型及累计统计。</returns>
        public static PatrolReportCategoryStatisticsResult GetPatrolCategoryStatistics(string accessTokenOrAppKey,
            PatrolReportCategoryStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => Post<PatrolReportCategoryStatisticsResult>(accessTokenOrAppKey, GetPatrolCategoryStatisticsPath, request, timeOut);

        /// <summary>
        /// 异步获取巡查事件分类统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93534"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的事件分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分类层级、类型及累计统计。</returns>
        public static Task<PatrolReportCategoryStatisticsResult> GetPatrolCategoryStatisticsAsync(string accessTokenOrAppKey,
            PatrolReportCategoryStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<PatrolReportCategoryStatisticsResult>(accessTokenOrAppKey, GetPatrolCategoryStatisticsPath, request, timeOut);

        /// <summary>
        /// 分页获取巡查事件工单列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93536"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">创建/修改时间、分页游标和单页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单列表及下一页游标。</returns>
        public static PatrolReportOrderListResult GetPatrolOrderList(string accessTokenOrAppKey,
            PatrolReportOrderListRequest request, int timeOut = Config.TIME_OUT)
            => Post<PatrolReportOrderListResult>(accessTokenOrAppKey, GetPatrolOrderListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取巡查事件工单列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93536"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">创建/修改时间、分页游标和单页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单列表及下一页游标。</returns>
        public static Task<PatrolReportOrderListResult> GetPatrolOrderListAsync(string accessTokenOrAppKey,
            PatrolReportOrderListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<PatrolReportOrderListResult>(accessTokenOrAppKey, GetPatrolOrderListPath, request, timeOut);

        /// <summary>
        /// 获取巡查事件工单详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93535"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工单 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单位置、处理人及流程详情。</returns>
        public static PatrolReportOrderInfoResult GetPatrolOrderInfo(string accessTokenOrAppKey,
            PatrolReportOrderInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<PatrolReportOrderInfoResult>(accessTokenOrAppKey, GetPatrolOrderInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取巡查事件工单详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93535"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工单 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单位置、处理人及流程详情。</returns>
        public static Task<PatrolReportOrderInfoResult> GetPatrolOrderInfoAsync(string accessTokenOrAppKey,
            PatrolReportOrderInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<PatrolReportOrderInfoResult>(accessTokenOrAppKey, GetPatrolOrderInfoPath, request, timeOut);
    }
}
