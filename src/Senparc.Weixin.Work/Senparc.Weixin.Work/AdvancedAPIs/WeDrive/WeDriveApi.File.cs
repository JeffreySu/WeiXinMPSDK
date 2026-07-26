/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveApi.File.cs
    文件功能描述：企业微信微盘文件接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘文件、分块上传、权限与安全设置接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 企业微信微盘文件、分块上传、权限和安全设置接口。
    /// </summary>
    public static partial class WeDriveApi
    {
        private const string FileListPath = "/cgi-bin/wedrive/file_list";
        private const string FileUploadPath = "/cgi-bin/wedrive/file_upload";
        private const string FileDownloadPath = "/cgi-bin/wedrive/file_download";
        private const string FileUploadInitializePath = "/cgi-bin/wedrive/file_upload_init";
        private const string FileUploadPartPath = "/cgi-bin/wedrive/file_upload_part";
        private const string FileUploadFinishPath = "/cgi-bin/wedrive/file_upload_finish";
        private const string FileCreatePath = "/cgi-bin/wedrive/file_create";
        private const string FileRenamePath = "/cgi-bin/wedrive/file_rename";
        private const string FileMovePath = "/cgi-bin/wedrive/file_move";
        private const string FileDeletePath = "/cgi-bin/wedrive/file_delete";
        private const string FileInfoPath = "/cgi-bin/wedrive/file_info";
        private const string FileSettingPath = "/cgi-bin/wedrive/file_setting";
        private const string FileSecureSettingPath = "/cgi-bin/wedrive/file_secure_setting";
        private const string FileSharePath = "/cgi-bin/wedrive/file_share";
        private const string GetFilePermissionPath = "/cgi-bin/wedrive/get_file_permission";
        private const string FileAclAddPath = "/cgi-bin/wedrive/file_acl_add";
        private const string FileAclDeletePath = "/cgi-bin/wedrive/file_acl_del";

        /// <summary>
        /// 获取微盘指定目录下的文件列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间、目录、排序和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分页文件列表。</returns>
        public static WeDriveFileListResult GetFileList(string accessTokenOrAppKey,
            WeDriveFileListRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileListResult>(accessTokenOrAppKey, FileListPath, request, timeOut);

        /// <summary>
        /// 异步获取微盘指定目录下的文件列表。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">空间、目录、排序和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>分页文件列表。</returns>
        public static Task<WeDriveFileListResult> GetFileListAsync(string accessTokenOrAppKey,
            WeDriveFileListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileListResult>(accessTokenOrAppKey, FileListPath, request, timeOut);

        /// <summary>
        /// 上传不超过 10 MB 的文件到微盘。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件名及 Base64 文件内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传后的文件 ID。</returns>
        public static WeDriveFileUploadResult UploadFile(string accessTokenOrAppKey,
            WeDriveFileUploadRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileUploadResult>(accessTokenOrAppKey, FileUploadPath, request, timeOut);

        /// <summary>
        /// 异步上传不超过 10 MB 的文件到微盘。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件名及 Base64 文件内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>上传后的文件 ID。</returns>
        public static Task<WeDriveFileUploadResult> UploadFileAsync(string accessTokenOrAppKey,
            WeDriveFileUploadRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileUploadResult>(accessTokenOrAppKey, FileUploadPath, request, timeOut);

        /// <summary>
        /// 获取普通微盘文件的临时下载地址和 Cookie。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 或文件选择器 selected_ticket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载地址及必须携带的 Cookie。</returns>
        public static WeDriveFileDownloadResult DownloadFile(string accessTokenOrAppKey,
            WeDriveFileDownloadRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileDownloadResult>(accessTokenOrAppKey, FileDownloadPath, request, timeOut);

        /// <summary>
        /// 异步获取普通微盘文件的临时下载地址和 Cookie。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 或文件选择器 selected_ticket。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>下载地址及必须携带的 Cookie。</returns>
        public static Task<WeDriveFileDownloadResult> DownloadFileAsync(string accessTokenOrAppKey,
            WeDriveFileDownloadRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileDownloadResult>(accessTokenOrAppKey, FileDownloadPath, request, timeOut);

        /// <summary>
        /// 初始化最大 20 GB 文件的微盘分块上传；命中秒传时无需继续上传分块。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件信息及分块 SHA 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>秒传状态、上传凭证或文件 ID。</returns>
        public static WeDriveFileUploadInitializeResult InitializeFileUpload(string accessTokenOrAppKey,
            WeDriveFileUploadInitializeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileUploadInitializeResult>(accessTokenOrAppKey, FileUploadInitializePath, request,
                timeOut);

        /// <summary>
        /// 异步初始化最大 20 GB 文件的微盘分块上传；命中秒传时无需继续上传分块。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件信息及分块 SHA 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>秒传状态、上传凭证或文件 ID。</returns>
        public static Task<WeDriveFileUploadInitializeResult> InitializeFileUploadAsync(
            string accessTokenOrAppKey, WeDriveFileUploadInitializeRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileUploadInitializeResult>(accessTokenOrAppKey, FileUploadInitializePath,
                request, timeOut);

        /// <summary>
        /// 上传一个微盘文件分块。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">上传凭证、分块序号及 Base64 分块内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UploadFilePart(string accessTokenOrAppKey,
            WeDriveFileUploadPartRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileUploadPartPath, request, timeOut);

        /// <summary>
        /// 异步上传一个微盘文件分块。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">上传凭证、分块序号及 Base64 分块内容。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UploadFilePartAsync(string accessTokenOrAppKey,
            WeDriveFileUploadPartRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileUploadPartPath, request, timeOut);

        /// <summary>
        /// 完成微盘文件分块上传并生成文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">初始化分块上传返回的上传凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>生成的文件 ID。</returns>
        public static WeDriveFileUploadFinishResult FinishFileUpload(string accessTokenOrAppKey,
            WeDriveFileUploadFinishRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileUploadFinishResult>(accessTokenOrAppKey, FileUploadFinishPath, request,
                timeOut);

        /// <summary>
        /// 异步完成微盘文件分块上传并生成文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">初始化分块上传返回的上传凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>生成的文件 ID。</returns>
        public static Task<WeDriveFileUploadFinishResult> FinishFileUploadAsync(string accessTokenOrAppKey,
            WeDriveFileUploadFinishRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileUploadFinishResult>(accessTokenOrAppKey, FileUploadFinishPath, request,
                timeOut);

        /// <summary>
        /// 在微盘指定位置新建文件夹或在线文档。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件类型及文件名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建文件 ID 及在线文档链接。</returns>
        public static WeDriveFileCreateResult CreateFile(string accessTokenOrAppKey,
            WeDriveFileCreateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileCreateResult>(accessTokenOrAppKey, FileCreatePath, request, timeOut);

        /// <summary>
        /// 异步在微盘指定位置新建文件夹或在线文档。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">目标位置、文件类型及文件名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>新建文件 ID 及在线文档链接。</returns>
        public static Task<WeDriveFileCreateResult> CreateFileAsync(string accessTokenOrAppKey,
            WeDriveFileCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileCreateResult>(accessTokenOrAppKey, FileCreatePath, request, timeOut);

        /// <summary>
        /// 重命名指定微盘文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 和新文件名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>重命名后的文件信息。</returns>
        public static WeDriveFileRenameResult RenameFile(string accessTokenOrAppKey,
            WeDriveFileRenameRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileRenameResult>(accessTokenOrAppKey, FileRenamePath, request, timeOut);

        /// <summary>
        /// 异步重命名指定微盘文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 和新文件名。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>重命名后的文件信息。</returns>
        public static Task<WeDriveFileRenameResult> RenameFileAsync(string accessTokenOrAppKey,
            WeDriveFileRenameRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileRenameResult>(accessTokenOrAppKey, FileRenamePath, request, timeOut);

        /// <summary>
        /// 将一个或多个微盘文件移动到指定目录。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 列表、目标目录和同名覆盖策略。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>移动后的文件信息。</returns>
        public static WeDriveFileMoveResult MoveFile(string accessTokenOrAppKey,
            WeDriveFileMoveRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileMoveResult>(accessTokenOrAppKey, FileMovePath, request, timeOut);

        /// <summary>
        /// 异步将一个或多个微盘文件移动到指定目录。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 列表、目标目录和同名覆盖策略。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>移动后的文件信息。</returns>
        public static Task<WeDriveFileMoveResult> MoveFileAsync(string accessTokenOrAppKey,
            WeDriveFileMoveRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileMoveResult>(accessTokenOrAppKey, FileMovePath, request, timeOut);

        /// <summary>
        /// 批量删除微盘文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待删除的文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult DeleteFile(string accessTokenOrAppKey, WeDriveFileDeleteRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileDeletePath, request, timeOut);

        /// <summary>
        /// 异步批量删除微盘文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待删除的文件 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> DeleteFileAsync(string accessTokenOrAppKey,
            WeDriveFileDeleteRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileDeletePath, request, timeOut);

        /// <summary>
        /// 获取指定微盘文件的详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件详细信息。</returns>
        public static WeDriveFileInfoResult GetFileInfo(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileInfoResult>(accessTokenOrAppKey, FileInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取指定微盘文件的详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件详细信息。</returns>
        public static Task<WeDriveFileInfoResult> GetFileInfoAsync(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileInfoResult>(accessTokenOrAppKey, FileInfoPath, request, timeOut);

        /// <summary>
        /// 修改微盘文件的分享范围和权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID、分享范围和权限类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult SetFileShareSetting(string accessTokenOrAppKey,
            WeDriveFileSettingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileSettingPath, request, timeOut);

        /// <summary>
        /// 异步修改微盘文件的分享范围和权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID、分享范围和权限类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> SetFileShareSettingAsync(string accessTokenOrAppKey,
            WeDriveFileSettingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileSettingPath, request, timeOut);

        /// <summary>
        /// 修改在线文档类型微盘文件的水印安全设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及水印设置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateFileSecureSetting(string accessTokenOrAppKey,
            WeDriveFileSecureSettingRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileSecureSettingPath, request, timeOut);

        /// <summary>
        /// 异步修改在线文档类型微盘文件的水印安全设置。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及水印设置。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateFileSecureSettingAsync(string accessTokenOrAppKey,
            WeDriveFileSecureSettingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileSecureSettingPath, request, timeOut);

        /// <summary>
        /// 获取微盘文件分享链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件分享链接。</returns>
        public static WeDriveFileShareResult GetFileShareLink(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFileShareResult>(accessTokenOrAppKey, FileSharePath, request, timeOut);

        /// <summary>
        /// 异步获取微盘文件分享链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件分享链接。</returns>
        public static Task<WeDriveFileShareResult> GetFileShareLinkAsync(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFileShareResult>(accessTokenOrAppKey, FileSharePath, request, timeOut);

        /// <summary>
        /// 获取微盘文件的分享、安全、继承、成员和水印权限信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件完整权限信息。</returns>
        public static WeDriveFilePermissionResult GetFilePermission(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDriveFilePermissionResult>(accessTokenOrAppKey, GetFilePermissionPath, request, timeOut);

        /// <summary>
        /// 异步获取微盘文件的分享、安全、继承、成员和水印权限信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>文件完整权限信息。</returns>
        public static Task<WeDriveFilePermissionResult> GetFilePermissionAsync(string accessTokenOrAppKey,
            WeDriveFileIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDriveFilePermissionResult>(accessTokenOrAppKey, GetFilePermissionPath, request,
                timeOut);

        /// <summary>
        /// 为微盘文件批量添加成员或部门权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及待添加的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult AddFileMembers(string accessTokenOrAppKey, WeDriveFileAclRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileAclAddPath, request, timeOut);

        /// <summary>
        /// 异步为微盘文件批量添加成员或部门权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及待添加的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> AddFileMembersAsync(string accessTokenOrAppKey,
            WeDriveFileAclRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileAclAddPath, request, timeOut);

        /// <summary>
        /// 从微盘文件批量移除成员或部门权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及待移除的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult RemoveFileMembers(string accessTokenOrAppKey,
            WeDriveFileAclRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, FileAclDeletePath, request, timeOut);

        /// <summary>
        /// 异步从微盘文件批量移除成员或部门权限。
        /// </summary>
        /// <param name="accessTokenOrAppKey">已配置微盘权限的应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">文件 ID 及待移除的授权对象。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> RemoveFileMembersAsync(string accessTokenOrAppKey,
            WeDriveFileAclRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, FileAclDeletePath, request, timeOut);
    }
}
