/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.SmartDoc.cs
    文件功能描述：企业微信智能文档页面、内容块、数据表与发布接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智能文档内容管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string AddSmartDocumentPagePath = "/cgi-bin/wedoc/smartdoc/add_page";
        private const string UpdateSmartDocumentPagePath = "/cgi-bin/wedoc/smartdoc/update_page";
        private const string DeleteSmartDocumentPagePath = "/cgi-bin/wedoc/smartdoc/delete_page";
        private const string GetSmartDocumentPageHierarchyPath = "/cgi-bin/wedoc/smartdoc/get_page_hierarchy";
        private const string AddSmartDocumentBlocksPath = "/cgi-bin/wedoc/smartdoc/add_blocks";
        private const string UpdateSmartDocumentBlocksPath = "/cgi-bin/wedoc/smartdoc/update_blocks";
        private const string DeleteSmartDocumentBlocksPath = "/cgi-bin/wedoc/smartdoc/delete_blocks";
        private const string GetSmartDocumentBlockListPath = "/cgi-bin/wedoc/smartdoc/get_block_list";
        private const string CreateSmartDocumentExportTaskPath = "/cgi-bin/wedoc/smartdoc/export_task";
        private const string GetSmartDocumentExportResultPath = "/cgi-bin/wedoc/smartdoc/get_export_result";
        private const string GetSmartDocumentDataSourcePath = "/cgi-bin/wedoc/smartdoc/get_smartsheet_info";
        private const string AddSmartDocumentDataTablePath = "/cgi-bin/wedoc/smartdoc/add_smartsheet";
        private const string DeleteSmartDocumentDataTablePath = "/cgi-bin/wedoc/smartdoc/delete_smartsheet";
        private const string UpdateSmartDocumentDataTablePath = "/cgi-bin/wedoc/smartdoc/update_smartsheet";
        private const string PublishSmartDocumentPath = "/cgi-bin/wedoc/smartdoc/publish";
        private const string CancelSmartDocumentPublishPath = "/cgi-bin/wedoc/smartdoc/cancel_publish";
        private const string UpdateSmartDocumentPublishSettingPath = "/cgi-bin/wedoc/smartdoc/publish_setting";

        /// <summary>向智能文档添加页面。<see href="https://developer.work.weixin.qq.com/document/path/101620"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和页面信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>系统生成的页面信息。</returns>
        public static WeDocSmartDocumentPageResult AddSmartDocumentPage(string accessTokenOrAppKey,
            WeDocSmartDocumentAddPageRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentPageResult>(accessTokenOrAppKey, AddSmartDocumentPagePath, request, timeOut);

        /// <summary>异步向智能文档添加页面。<see href="https://developer.work.weixin.qq.com/document/path/101620"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和页面信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>系统生成的页面信息。</returns>
        public static Task<WeDocSmartDocumentPageResult> AddSmartDocumentPageAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentAddPageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentPageResult>(accessTokenOrAppKey, AddSmartDocumentPagePath, request, timeOut);

        /// <summary>更新智能文档页面标题、布局、层级或顺序。<see href="https://developer.work.weixin.qq.com/document/path/101621"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和需要更新的页面信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的页面信息。</returns>
        public static WeDocSmartDocumentPageResult UpdateSmartDocumentPage(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdatePageRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentPageResult>(accessTokenOrAppKey, UpdateSmartDocumentPagePath, request, timeOut);

        /// <summary>异步更新智能文档页面标题、布局、层级或顺序。<see href="https://developer.work.weixin.qq.com/document/path/101621"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和需要更新的页面信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的页面信息。</returns>
        public static Task<WeDocSmartDocumentPageResult> UpdateSmartDocumentPageAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdatePageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentPageResult>(accessTokenOrAppKey, UpdateSmartDocumentPagePath, request, timeOut);

        /// <summary>删除智能文档页面及其子页面。<see href="https://developer.work.weixin.qq.com/document/path/101622"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和页面 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartDocumentPage(string accessTokenOrAppKey,
            WeDocSmartDocumentDeletePageRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentPagePath, request, timeOut);

        /// <summary>异步删除智能文档页面及其子页面。<see href="https://developer.work.weixin.qq.com/document/path/101622"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和页面 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartDocumentPageAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentDeletePageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentPagePath, request, timeOut);

        /// <summary>获取智能文档页面层级结构。<see href="https://developer.work.weixin.qq.com/document/path/101619"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>页面及其父页面关系。</returns>
        public static WeDocSmartDocumentPageHierarchyResult GetSmartDocumentPageHierarchy(
            string accessTokenOrAppKey, WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentPageHierarchyResult>(accessTokenOrAppKey,
                GetSmartDocumentPageHierarchyPath, request, timeOut);

        /// <summary>异步获取智能文档页面层级结构。<see href="https://developer.work.weixin.qq.com/document/path/101619"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>页面及其父页面关系。</returns>
        public static Task<WeDocSmartDocumentPageHierarchyResult> GetSmartDocumentPageHierarchyAsync(
            string accessTokenOrAppKey, WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentPageHierarchyResult>(accessTokenOrAppKey,
                GetSmartDocumentPageHierarchyPath, request, timeOut);

        /// <summary>批量向智能文档页面添加内容块。<see href="https://developer.work.weixin.qq.com/document/path/101623"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和内容块列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>包含系统生成 ID 的内容块列表。</returns>
        public static WeDocSmartDocumentBlocksResult AddSmartDocumentBlocks(string accessTokenOrAppKey,
            WeDocSmartDocumentAddBlocksRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentBlocksResult>(accessTokenOrAppKey, AddSmartDocumentBlocksPath, request, timeOut);

        /// <summary>异步批量向智能文档页面添加内容块。<see href="https://developer.work.weixin.qq.com/document/path/101623"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和内容块列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>包含系统生成 ID 的内容块列表。</returns>
        public static Task<WeDocSmartDocumentBlocksResult> AddSmartDocumentBlocksAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentAddBlocksRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentBlocksResult>(accessTokenOrAppKey, AddSmartDocumentBlocksPath, request, timeOut);

        /// <summary>批量更新智能文档页面内容块。<see href="https://developer.work.weixin.qq.com/document/path/101624"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和待更新内容块列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的内容块列表。</returns>
        public static WeDocSmartDocumentBlocksResult UpdateSmartDocumentBlocks(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdateBlocksRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentBlocksResult>(accessTokenOrAppKey, UpdateSmartDocumentBlocksPath, request, timeOut);

        /// <summary>异步批量更新智能文档页面内容块。<see href="https://developer.work.weixin.qq.com/document/path/101624"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和待更新内容块列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的内容块列表。</returns>
        public static Task<WeDocSmartDocumentBlocksResult> UpdateSmartDocumentBlocksAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdateBlocksRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentBlocksResult>(accessTokenOrAppKey, UpdateSmartDocumentBlocksPath, request, timeOut);

        /// <summary>批量删除智能文档页面内容块。<see href="https://developer.work.weixin.qq.com/document/path/101625"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和内容块 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartDocumentBlocks(string accessTokenOrAppKey,
            WeDocSmartDocumentDeleteBlocksRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentBlocksPath, request, timeOut);

        /// <summary>异步批量删除智能文档页面内容块。<see href="https://developer.work.weixin.qq.com/document/path/101625"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识和内容块 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartDocumentBlocksAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentDeleteBlocksRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentBlocksPath, request, timeOut);

        /// <summary>分页或按 ID 获取智能文档内容块。<see href="https://developer.work.weixin.qq.com/document/path/101626"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识、内容块 ID 或分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>内容块列表和下一批起始位置。</returns>
        public static WeDocSmartDocumentBlockListResult GetSmartDocumentBlocks(string accessTokenOrAppKey,
            WeDocSmartDocumentGetBlocksRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentBlockListResult>(accessTokenOrAppKey, GetSmartDocumentBlockListPath, request, timeOut);

        /// <summary>异步分页或按 ID 获取智能文档内容块。<see href="https://developer.work.weixin.qq.com/document/path/101626"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">页面标识、内容块 ID 或分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>内容块列表和下一批起始位置。</returns>
        public static Task<WeDocSmartDocumentBlockListResult> GetSmartDocumentBlocksAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentGetBlocksRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentBlockListResult>(accessTokenOrAppKey, GetSmartDocumentBlockListPath, request, timeOut);

        /// <summary>提交智能文档 Markdown 导出任务。<see href="https://developer.work.weixin.qq.com/document/path/101627"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档、内容格式及可选页面标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步导出任务 ID。</returns>
        public static WeDocSmartDocumentExportTaskResult CreateSmartDocumentExportTask(string accessTokenOrAppKey,
            WeDocSmartDocumentExportTaskRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentExportTaskResult>(accessTokenOrAppKey, CreateSmartDocumentExportTaskPath, request, timeOut);

        /// <summary>异步提交智能文档 Markdown 导出任务。<see href="https://developer.work.weixin.qq.com/document/path/101627"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档、内容格式及可选页面标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>异步导出任务 ID。</returns>
        public static Task<WeDocSmartDocumentExportTaskResult> CreateSmartDocumentExportTaskAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentExportTaskRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentExportTaskResult>(accessTokenOrAppKey, CreateSmartDocumentExportTaskPath, request, timeOut);

        /// <summary>查询智能文档导出任务及 Markdown 内容。<see href="https://developer.work.weixin.qq.com/document/path/101627"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">异步导出任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>任务完成状态和导出内容。</returns>
        public static WeDocSmartDocumentExportResult GetSmartDocumentExportResult(string accessTokenOrAppKey,
            WeDocSmartDocumentExportResultRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentExportResult>(accessTokenOrAppKey, GetSmartDocumentExportResultPath, request, timeOut);

        /// <summary>异步查询智能文档导出任务及 Markdown 内容。<see href="https://developer.work.weixin.qq.com/document/path/101627"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">异步导出任务 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>任务完成状态和导出内容。</returns>
        public static Task<WeDocSmartDocumentExportResult> GetSmartDocumentExportResultAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentExportResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentExportResult>(accessTokenOrAppKey, GetSmartDocumentExportResultPath, request, timeOut);

        /// <summary>获取或创建智能文档绑定的数据源。<see href="https://developer.work.weixin.qq.com/document/path/101628"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>绑定的智能表格文档 ID。</returns>
        public static WeDocSmartDocumentDataSourceResult GetSmartDocumentDataSource(string accessTokenOrAppKey,
            WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentDataSourceResult>(accessTokenOrAppKey, GetSmartDocumentDataSourcePath, request, timeOut);

        /// <summary>异步获取或创建智能文档绑定的数据源。<see href="https://developer.work.weixin.qq.com/document/path/101628"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>绑定的智能表格文档 ID。</returns>
        public static Task<WeDocSmartDocumentDataSourceResult> GetSmartDocumentDataSourceAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentDataSourceResult>(accessTokenOrAppKey, GetSmartDocumentDataSourcePath, request, timeOut);

        /// <summary>向智能文档添加数据表。<see href="https://developer.work.weixin.qq.com/document/path/101629"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和数据表标题、排序位置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>数据表块、子表和数据源信息。</returns>
        public static WeDocSmartDocumentDataTableResult AddSmartDocumentDataTable(string accessTokenOrAppKey,
            WeDocSmartDocumentAddDataTableRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentDataTableResult>(accessTokenOrAppKey, AddSmartDocumentDataTablePath, request, timeOut);

        /// <summary>异步向智能文档添加数据表。<see href="https://developer.work.weixin.qq.com/document/path/101629"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和数据表标题、排序位置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>数据表块、子表和数据源信息。</returns>
        public static Task<WeDocSmartDocumentDataTableResult> AddSmartDocumentDataTableAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentAddDataTableRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentDataTableResult>(accessTokenOrAppKey, AddSmartDocumentDataTablePath, request, timeOut);

        /// <summary>删除智能文档数据表。<see href="https://developer.work.weixin.qq.com/document/path/101630"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和数据表块 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteSmartDocumentDataTable(string accessTokenOrAppKey,
            WeDocSmartDocumentDeleteDataTableRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentDataTablePath, request, timeOut);

        /// <summary>异步删除智能文档数据表。<see href="https://developer.work.weixin.qq.com/document/path/101630"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和数据表块 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteSmartDocumentDataTableAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentDeleteDataTableRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteSmartDocumentDataTablePath, request, timeOut);

        /// <summary>更新智能文档数据表标题或顺序。<see href="https://developer.work.weixin.qq.com/document/path/101631"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和待更新的数据表信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的数据表信息。</returns>
        public static WeDocSmartDocumentDataTableResult UpdateSmartDocumentDataTable(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdateDataTableRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentDataTableResult>(accessTokenOrAppKey, UpdateSmartDocumentDataTablePath, request, timeOut);

        /// <summary>异步更新智能文档数据表标题或顺序。<see href="https://developer.work.weixin.qq.com/document/path/101631"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识和待更新的数据表信息。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>更新后的数据表信息。</returns>
        public static Task<WeDocSmartDocumentDataTableResult> UpdateSmartDocumentDataTableAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentUpdateDataTableRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentDataTableResult>(accessTokenOrAppKey, UpdateSmartDocumentDataTablePath, request, timeOut);

        /// <summary>发布智能文档并设置可见范围。<see href="https://developer.work.weixin.qq.com/document/path/101616"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识、发布范围和指定成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分享码、发布地址、版本和发布时间。</returns>
        public static WeDocSmartDocumentPublishResult PublishSmartDocument(string accessTokenOrAppKey,
            WeDocSmartDocumentPublishRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSmartDocumentPublishResult>(accessTokenOrAppKey, PublishSmartDocumentPath, request, timeOut);

        /// <summary>异步发布智能文档并设置可见范围。<see href="https://developer.work.weixin.qq.com/document/path/101616"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识、发布范围和指定成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>分享码、发布地址、版本和发布时间。</returns>
        public static Task<WeDocSmartDocumentPublishResult> PublishSmartDocumentAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentPublishRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSmartDocumentPublishResult>(accessTokenOrAppKey, PublishSmartDocumentPath, request, timeOut);

        /// <summary>取消发布智能文档。<see href="https://developer.work.weixin.qq.com/document/path/101617"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult CancelSmartDocumentPublish(string accessTokenOrAppKey,
            WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CancelSmartDocumentPublishPath, request, timeOut);

        /// <summary>异步取消发布智能文档。<see href="https://developer.work.weixin.qq.com/document/path/101617"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">智能文档标识。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> CancelSmartDocumentPublishAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CancelSmartDocumentPublishPath, request, timeOut);

        /// <summary>修改智能文档发布页可见范围。<see href="https://developer.work.weixin.qq.com/document/path/101618"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识、发布范围和指定成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult UpdateSmartDocumentPublishSetting(string accessTokenOrAppKey,
            WeDocSmartDocumentPublishSettingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartDocumentPublishSettingPath, request, timeOut);

        /// <summary>异步修改智能文档发布页可见范围。<see href="https://developer.work.weixin.qq.com/document/path/101618"/></summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">文档标识、发布范围和指定成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> UpdateSmartDocumentPublishSettingAsync(string accessTokenOrAppKey,
            WeDocSmartDocumentPublishSettingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateSmartDocumentPublishSettingPath, request, timeOut);
    }
}
