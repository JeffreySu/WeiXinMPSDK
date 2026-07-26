/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ReportApi.Grid.cs
    文件功能描述：企业微信政民沟通网格与事件分类管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐政民沟通网格与事件分类管理 9 项接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Report
{
    public static partial class ReportApi
    {
        private const string AddReportGridPath = "/cgi-bin/report/grid/add";
        private const string UpdateReportGridPath = "/cgi-bin/report/grid/update";
        private const string DeleteReportGridPath = "/cgi-bin/report/grid/delete";
        private const string GetReportGridListPath = "/cgi-bin/report/grid/list";
        private const string GetUserReportGridInfoPath = "/cgi-bin/report/grid/get_user_grid_info";
        private const string AddReportGridCategoryPath = "/cgi-bin/report/grid/add_cata";
        private const string UpdateReportGridCategoryPath = "/cgi-bin/report/grid/update_cata";
        private const string DeleteReportGridCategoryPath = "/cgi-bin/report/grid/delete_cata";
        private const string GetReportGridCategoryListPath = "/cgi-bin/report/grid/list_cata";

        /// <summary>
        /// 新增政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94478"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">网格名称、上级网格、管理员和成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增的网格 ID 及无效成员列表。</returns>
        public static ReportGridAddResult AddReportGrid(string accessTokenOrAppKey,
            ReportGridAddRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridAddResult>(accessTokenOrAppKey, AddReportGridPath, request, timeOut);

        /// <summary>
        /// 异步新增政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94478"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">网格名称、上级网格、管理员和成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增的网格 ID 及无效成员列表。</returns>
        public static Task<ReportGridAddResult> AddReportGridAsync(string accessTokenOrAppKey,
            ReportGridAddRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridAddResult>(accessTokenOrAppKey, AddReportGridPath, request, timeOut);

        /// <summary>
        /// 更新政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94479"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">网格 ID 及完整的网格信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果及无效成员列表。</returns>
        public static ReportGridUpdateResult UpdateReportGrid(string accessTokenOrAppKey,
            ReportGridUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridUpdateResult>(accessTokenOrAppKey, UpdateReportGridPath, request, timeOut);

        /// <summary>
        /// 异步更新政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94479"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">网格 ID 及完整的网格信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果及无效成员列表。</returns>
        public static Task<ReportGridUpdateResult> UpdateReportGridAsync(string accessTokenOrAppKey,
            ReportGridUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridUpdateResult>(accessTokenOrAppKey, UpdateReportGridPath, request, timeOut);

        /// <summary>
        /// 删除政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94480"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要删除的网格 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>删除结果。</returns>
        public static ReportGridDeleteResult DeleteReportGrid(string accessTokenOrAppKey,
            ReportGridDeleteRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridDeleteResult>(accessTokenOrAppKey, DeleteReportGridPath, request, timeOut);

        /// <summary>
        /// 异步删除政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94480"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要删除的网格 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>删除结果。</returns>
        public static Task<ReportGridDeleteResult> DeleteReportGridAsync(string accessTokenOrAppKey,
            ReportGridDeleteRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridDeleteResult>(accessTokenOrAppKey, DeleteReportGridPath, request, timeOut);

        /// <summary>
        /// 获取政民沟通网格列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94481"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的上级网格 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格列表。</returns>
        public static ReportGridListResult GetReportGridList(string accessTokenOrAppKey,
            ReportGridListRequest request = null, int timeOut = Config.TIME_OUT)
            => Post<ReportGridListResult>(accessTokenOrAppKey, GetReportGridListPath,
                request ?? new ReportGridListRequest(), timeOut);

        /// <summary>
        /// 异步获取政民沟通网格列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94481"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">可选的上级网格 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>网格列表。</returns>
        public static Task<ReportGridListResult> GetReportGridListAsync(string accessTokenOrAppKey,
            ReportGridListRequest request = null, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridListResult>(accessTokenOrAppKey, GetReportGridListPath,
                request ?? new ReportGridListRequest(), timeOut);

        /// <summary>
        /// 获取成员管理和加入的政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94482"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员管理和加入的网格列表。</returns>
        public static ReportGridUserInfoResult GetUserReportGridInfo(string accessTokenOrAppKey,
            ReportGridUserInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridUserInfoResult>(accessTokenOrAppKey, GetUserReportGridInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取成员管理和加入的政民沟通网格。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94482"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要查询的成员 UserId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员管理和加入的网格列表。</returns>
        public static Task<ReportGridUserInfoResult> GetUserReportGridInfoAsync(string accessTokenOrAppKey,
            ReportGridUserInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridUserInfoResult>(accessTokenOrAppKey, GetUserReportGridInfoPath, request, timeOut);

        /// <summary>
        /// 新增政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94536"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分类名称、层级和上级分类。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增的分类 ID。</returns>
        public static ReportGridCategoryAddResult AddReportGridCategory(string accessTokenOrAppKey,
            ReportGridCategoryAddRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridCategoryAddResult>(accessTokenOrAppKey, AddReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 异步新增政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94536"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分类名称、层级和上级分类。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增的分类 ID。</returns>
        public static Task<ReportGridCategoryAddResult> AddReportGridCategoryAsync(string accessTokenOrAppKey,
            ReportGridCategoryAddRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridCategoryAddResult>(accessTokenOrAppKey, AddReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 更新政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94537"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分类 ID、名称、层级和上级分类。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static ReportGridCategoryUpdateResult UpdateReportGridCategory(string accessTokenOrAppKey,
            ReportGridCategoryUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridCategoryUpdateResult>(accessTokenOrAppKey, UpdateReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 异步更新政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94537"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">分类 ID、名称、层级和上级分类。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新结果。</returns>
        public static Task<ReportGridCategoryUpdateResult> UpdateReportGridCategoryAsync(string accessTokenOrAppKey,
            ReportGridCategoryUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridCategoryUpdateResult>(accessTokenOrAppKey, UpdateReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 删除政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94538"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要删除的分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>删除结果。</returns>
        public static ReportGridCategoryDeleteResult DeleteReportGridCategory(string accessTokenOrAppKey,
            ReportGridCategoryDeleteRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReportGridCategoryDeleteResult>(accessTokenOrAppKey, DeleteReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 异步删除政民沟通事件分类。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94538"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">需要删除的分类 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>删除结果。</returns>
        public static Task<ReportGridCategoryDeleteResult> DeleteReportGridCategoryAsync(string accessTokenOrAppKey,
            ReportGridCategoryDeleteRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridCategoryDeleteResult>(accessTokenOrAppKey, DeleteReportGridCategoryPath, request, timeOut);

        /// <summary>
        /// 获取政民沟通事件分类列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94540"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">空的分类列表请求；可传入 <see langword="null"/>。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>事件分类列表。</returns>
        public static ReportGridCategoryListResult GetReportGridCategoryList(string accessTokenOrAppKey,
            ReportGridCategoryListRequest request = null, int timeOut = Config.TIME_OUT)
            => Post<ReportGridCategoryListResult>(accessTokenOrAppKey, GetReportGridCategoryListPath,
                request ?? new ReportGridCategoryListRequest(), timeOut);

        /// <summary>
        /// 异步获取政民沟通事件分类列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/94540"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">空的分类列表请求；可传入 <see langword="null"/>。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>事件分类列表。</returns>
        public static Task<ReportGridCategoryListResult> GetReportGridCategoryListAsync(string accessTokenOrAppKey,
            ReportGridCategoryListRequest request = null, int timeOut = Config.TIME_OUT)
            => PostAsync<ReportGridCategoryListResult>(accessTokenOrAppKey, GetReportGridCategoryListPath,
                request ?? new ReportGridCategoryListRequest(), timeOut);
    }
}
