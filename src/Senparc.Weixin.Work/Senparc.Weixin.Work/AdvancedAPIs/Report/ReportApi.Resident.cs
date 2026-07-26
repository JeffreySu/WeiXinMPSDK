/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportApi.Resident.cs
    文件功能描述：企业微信政民沟通居民上报接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐政民沟通居民上报 6 项查询与统计接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    public static partial class ReportApi
    {
        private const string GetResidentGridInfoPath = "/cgi-bin/report/resident/get_grid_info";
        private const string GetResidentCorpStatusPath = "/cgi-bin/report/resident/get_corp_status";
        private const string GetResidentUserStatusPath = "/cgi-bin/report/resident/get_user_status";
        private const string GetResidentCategoryStatisticsPath = "/cgi-bin/report/resident/category_statistic";
        private const string GetResidentOrderListPath = "/cgi-bin/report/resident/get_order_list";
        private const string GetResidentOrderInfoPath = "/cgi-bin/report/resident/get_order_info";

        /// <summary>
        /// 获取政民沟通居民上报可见的网格信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93514"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格及管理员列表。</returns>
        public static ResidentReportGridInfoResult GetResidentGridInfo(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<ResidentReportGridInfoResult>(
                    accessToken, Config.ApiWorkHost + GetResidentGridInfoPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 异步获取政民沟通居民上报可见的网格信息。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93514"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格及管理员列表。</returns>
        public static Task<ResidentReportGridInfoResult> GetResidentGridInfoAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<ResidentReportGridInfoResult>(
                    accessToken, Config.ApiWorkHost + GetResidentGridInfoPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 获取企业的居民事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93515"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选网格范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>待受理、办理中、今日和累计统计。</returns>
        public static ResidentReportCorpStatusResult GetResidentCorpStatus(string accessTokenOrAppKey,
            ResidentReportCorpStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<ResidentReportCorpStatusResult>(accessTokenOrAppKey, GetResidentCorpStatusPath, request, timeOut);

        /// <summary>
        /// 异步获取企业的居民事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93515"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选网格范围。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>待受理、办理中、今日和累计统计。</returns>
        public static Task<ResidentReportCorpStatusResult> GetResidentCorpStatusAsync(string accessTokenOrAppKey,
            ResidentReportCorpStatusRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ResidentReportCorpStatusResult>(accessTokenOrAppKey, GetResidentCorpStatusPath, request, timeOut);

        /// <summary>
        /// 获取成员的居民事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93516"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员的待受理、办理中和今日统计。</returns>
        public static ResidentReportUserStatusResult GetResidentUserStatus(string accessTokenOrAppKey,
            ResidentReportUserStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<ResidentReportUserStatusResult>(accessTokenOrAppKey, GetResidentUserStatusPath, request, timeOut);

        /// <summary>
        /// 异步获取成员的居民事件统计数据。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93516"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员的待受理、办理中和今日统计。</returns>
        public static Task<ResidentReportUserStatusResult> GetResidentUserStatusAsync(string accessTokenOrAppKey,
            ResidentReportUserStatusRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ResidentReportUserStatusResult>(accessTokenOrAppKey, GetResidentUserStatusPath, request, timeOut);

        /// <summary>
        /// 获取居民事件分类统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93517"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的事件分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分类层级、类型及累计统计。</returns>
        public static ResidentReportCategoryStatisticsResult GetResidentCategoryStatistics(string accessTokenOrAppKey,
            ResidentReportCategoryStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => Post<ResidentReportCategoryStatisticsResult>(accessTokenOrAppKey, GetResidentCategoryStatisticsPath, request, timeOut);

        /// <summary>
        /// 异步获取居民事件分类统计。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93517"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的事件分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分类层级、类型及累计统计。</returns>
        public static Task<ResidentReportCategoryStatisticsResult> GetResidentCategoryStatisticsAsync(string accessTokenOrAppKey,
            ResidentReportCategoryStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ResidentReportCategoryStatisticsResult>(accessTokenOrAppKey, GetResidentCategoryStatisticsPath, request, timeOut);

        /// <summary>
        /// 分页获取居民事件工单列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93518"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">创建/修改时间、分页游标和单页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单列表及下一页游标。</returns>
        public static ResidentReportOrderListResult GetResidentOrderList(string accessTokenOrAppKey,
            ResidentReportOrderListRequest request, int timeOut = Config.TIME_OUT)
            => Post<ResidentReportOrderListResult>(accessTokenOrAppKey, GetResidentOrderListPath, request, timeOut);

        /// <summary>
        /// 异步分页获取居民事件工单列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93518"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">创建/修改时间、分页游标和单页数量。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单列表及下一页游标。</returns>
        public static Task<ResidentReportOrderListResult> GetResidentOrderListAsync(string accessTokenOrAppKey,
            ResidentReportOrderListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ResidentReportOrderListResult>(accessTokenOrAppKey, GetResidentOrderListPath, request, timeOut);

        /// <summary>
        /// 获取居民事件工单详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93519"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工单 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单上报人、位置、处理人及流程详情。</returns>
        public static ResidentReportOrderInfoResult GetResidentOrderInfo(string accessTokenOrAppKey,
            ResidentReportOrderInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<ResidentReportOrderInfoResult>(accessTokenOrAppKey, GetResidentOrderInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取居民事件工单详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93519"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工单 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>工单上报人、位置、处理人及流程详情。</returns>
        public static Task<ResidentReportOrderInfoResult> GetResidentOrderInfoAsync(string accessTokenOrAppKey,
            ResidentReportOrderInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ResidentReportOrderInfoResult>(accessTokenOrAppKey, GetResidentOrderInfoPath, request, timeOut);
    }
}
