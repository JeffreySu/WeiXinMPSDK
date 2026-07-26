/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartSheet.Views.cs
    文件功能描述：企业微信智能表格视图接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能表格视图增删改查接口及必要注释

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string GetSmartSheetViewsPath = "/cgi-bin/wedoc/smartsheet/get_views";
        private const string AddSmartSheetViewPath = "/cgi-bin/wedoc/smartsheet/add_view";
        private const string DeleteSmartSheetViewsPath = "/cgi-bin/wedoc/smartsheet/delete_views";
        private const string UpdateSmartSheetViewPath = "/cgi-bin/wedoc/smartsheet/update_view";

        /// <summary>
        /// 获取智能表格视图及排序、筛选、分组等配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99913"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、可选视图 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>视图列表及分页信息。</returns>
        public static WeDocSmartSheetGetViewsResult GetSmartSheetViews(string accessTokenOrAppKey,
            WeDocSmartSheetGetViewsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetGetViewsResult>(accessTokenOrAppKey, GetSmartSheetViewsPath, request, timeOut);

        /// <summary>
        /// 异步获取智能表格视图及排序、筛选、分组等配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99913"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、可选视图 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>视图列表及分页信息。</returns>
        public static Task<WeDocSmartSheetGetViewsResult> GetSmartSheetViewsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetGetViewsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetGetViewsResult>(accessTokenOrAppKey, GetSmartSheetViewsPath, request, timeOut);

        /// <summary>
        /// 在指定工作表中新增视图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99896"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、视图标题、类型及可选的日期字段配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的视图。</returns>
        public static WeDocSmartSheetAddViewResult AddSmartSheetView(string accessTokenOrAppKey,
            WeDocSmartSheetAddViewRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartSheetAddViewResult>(accessTokenOrAppKey, AddSmartSheetViewPath, request, timeOut);

        /// <summary>
        /// 异步在指定工作表中新增视图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99896"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识、视图标题、类型及可选的日期字段配置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新增后的视图。</returns>
        public static Task<WeDocSmartSheetAddViewResult> AddSmartSheetViewAsync(string accessTokenOrAppKey,
            WeDocSmartSheetAddViewRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartSheetAddViewResult>(accessTokenOrAppKey, AddSmartSheetViewPath, request, timeOut);

        /// <summary>
        /// 批量删除指定工作表中的视图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99901"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除视图 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartSheetViews(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteViewsRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetViewsPath, request, timeOut);

        /// <summary>
        /// 异步批量删除指定工作表中的视图。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99901"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表标识及待删除视图 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartSheetViewsAsync(string accessTokenOrAppKey,
            WeDocSmartSheetDeleteViewsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartSheetViewsPath, request, timeOut);

        /// <summary>
        /// 更新智能表格视图标题、排序、筛选、分组、字段可见性或条件填色配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99902"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表、视图标识及需要更新的视图属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateSmartSheetView(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateViewRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetViewPath, request, timeOut);

        /// <summary>
        /// 异步更新智能表格视图标题、排序、筛选、分组、字段可见性或条件填色配置。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99902"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">工作表、视图标识及需要更新的视图属性。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateSmartSheetViewAsync(string accessTokenOrAppKey,
            WeDocSmartSheetUpdateViewRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartSheetViewPath, request, timeOut);
    }
}
