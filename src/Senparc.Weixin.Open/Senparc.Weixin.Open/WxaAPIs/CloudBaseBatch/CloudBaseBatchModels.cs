#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudBaseBatchModels.cs
    文件功能描述：第三方平台批量云开发接口强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.CloudBaseBatch
{
    #region 环境管理

    /// <summary>云开发 access_token 调用权限请求。</summary>
    public class CloudBaseBatchAccessTokenRequest
    {
        /// <summary>操作类型，取值为 <c>get</c> 或 <c>set</c>。</summary>
        public string action { get; set; }

        /// <summary>是否允许使用 access_token 调用；仅设置操作需要。</summary>
        public bool? open { get; set; }

        /// <summary>允许调用的接口白名单；仅设置操作需要。</summary>
        public List<string> api_whitelist { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>配置版本号；仅设置操作需要。</summary>
        public int? version { get; set; }
    }

    /// <summary>云开发 access_token 调用权限结果。</summary>
    public class CloudBaseBatchAccessTokenJsonResult : WxJsonResult
    {
        /// <summary>是否允许使用 access_token 调用。</summary>
        public bool open { get; set; }

        /// <summary>允许调用的接口白名单。</summary>
        public List<string> api_whitelist { get; set; }

        /// <summary>配置版本号。</summary>
        public int version { get; set; }
    }

    /// <summary>云开发环境共享关系查询结果。</summary>
    public class CloudBaseBatchGetShareEnvJsonResult : WxJsonResult
    {
        /// <summary>AppID 与云开发环境的共享关系列表。</summary>
        public List<CloudBaseBatchEnvRelation> relation_data { get; set; }

        /// <summary>查询失败的 AppID 列表。</summary>
        public List<CloudBaseBatchEnvError> err_list { get; set; }
    }

    /// <summary>云开发环境共享或解除共享结果。</summary>
    public class CloudBaseBatchShareEnvJsonResult : WxJsonResult
    {
        /// <summary>处理失败的环境与 AppID 列表。</summary>
        public List<CloudBaseBatchEnvError> err_list { get; set; }

        /// <summary>需要小程序管理员确认的链接列表。</summary>
        public List<CloudBaseBatchShareConfirmInfo> msg_info_list { get; set; }
    }

    /// <summary>AppID 与云开发环境的共享关系。</summary>
    public class CloudBaseBatchEnvRelation
    {
        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>已共享的云开发环境 ID 列表。</summary>
        public List<string> env_list { get; set; }
    }

    /// <summary>云开发环境共享处理错误。</summary>
    public class CloudBaseBatchEnvError
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>错误信息。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>云开发环境共享确认信息。</summary>
    public class CloudBaseBatchShareConfirmInfo
    {
        /// <summary>需要确认的小程序 AppID。</summary>
        public string appid { get; set; }

        /// <summary>发送给小程序管理员的确认链接。</summary>
        public string url { get; set; }
    }

    /// <summary>单个云开发环境共享请求项。</summary>
    public class CloudBaseBatchEnvShareItem
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>需要建立或解除共享关系的小程序 AppID 列表。</summary>
        public List<string> appids { get; set; }
    }

    /// <summary>云开发环境列表查询结果。</summary>
    public class CloudBaseBatchEnvListJsonResult : WxJsonResult
    {
        /// <summary>云开发环境列表。</summary>
        public List<CloudBaseBatchEnvInfo> info_list { get; set; }
    }

    /// <summary>云开发环境信息。</summary>
    public class CloudBaseBatchEnvInfo
    {
        /// <summary>环境 ID。</summary>
        public string env { get; set; }

        /// <summary>环境别名。</summary>
        public string alias { get; set; }

        /// <summary>创建时间。</summary>
        public string create_time { get; set; }

        /// <summary>最后更新时间。</summary>
        public string update_time { get; set; }

        /// <summary>环境状态。</summary>
        public string status { get; set; }

        /// <summary>云开发套餐 ID。</summary>
        public string package_id { get; set; }

        /// <summary>云开发套餐名称。</summary>
        public string package_name { get; set; }

        /// <summary>数据库实例 ID。</summary>
        public string dbinstance_id { get; set; }

        /// <summary>云存储 Bucket ID。</summary>
        public string bucket_id { get; set; }
    }

    /// <summary>创建云开发环境结果。</summary>
    public class CloudBaseBatchCreateEnvJsonResult : WxJsonResult
    {
        /// <summary>创建任务 ID。</summary>
        public string tranid { get; set; }

        /// <summary>新创建的云开发环境 ID。</summary>
        public string env { get; set; }
    }

    #endregion

    #region 云函数管理

    /// <summary>批量创建云函数请求。</summary>
    public class CloudBaseBatchUploadFunctionRequest
    {
        /// <summary>云函数名称。</summary>
        public string functionname { get; set; }

        /// <summary>目标云开发环境 ID 列表。</summary>
        public List<string> envs { get; set; }

        /// <summary>Base64 编码的 ZIP 代码包，最大 20 MB。</summary>
        public string zipfile { get; set; }

        /// <summary>可选的私有网络 ID。</summary>
        public string vpcid { get; set; }

        /// <summary>可选的子网 ID。</summary>
        public string subnetid { get; set; }
    }

    /// <summary>批量更新云函数代码请求。</summary>
    public class CloudBaseBatchUploadFunctionCodeRequest
    {
        /// <summary>云函数名称。</summary>
        public string functionname { get; set; }

        /// <summary>目标云开发环境 ID 列表。</summary>
        public List<string> envs { get; set; }

        /// <summary>Base64 编码的 ZIP 代码包，最大 20 MB。</summary>
        public string zipfile { get; set; }
    }

    /// <summary>云函数配置更新请求。</summary>
    public class CloudBaseBatchFunctionConfigRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云函数名称。</summary>
        public string functionname { get; set; }

        /// <summary>可选内存大小，单位 MB。</summary>
        public int? memorysize { get; set; }

        /// <summary>可选超时时间，单位秒。</summary>
        public int? timeout { get; set; }

        /// <summary>可选环境变量列表。</summary>
        public List<CloudBaseBatchEnvironmentVariable> environment_variables { get; set; }

        /// <summary>可选公网访问配置。</summary>
        public CloudBaseBatchPublicNetConfig public_net_config { get; set; }

        /// <summary>可选私有网络配置。</summary>
        public CloudBaseBatchVpcConfig vpc_config { get; set; }
    }

    /// <summary>云函数环境变量。</summary>
    public class CloudBaseBatchEnvironmentVariable
    {
        /// <summary>环境变量名称。</summary>
        public string key { get; set; }

        /// <summary>环境变量值。</summary>
        public string value { get; set; }
    }

    /// <summary>云函数公网访问配置。</summary>
    public class CloudBaseBatchPublicNetConfig
    {
        /// <summary>公网访问状态。</summary>
        public string public_net_status { get; set; }

        /// <summary>固定出口 IP 状态。</summary>
        public string eip_status { get; set; }
    }

    /// <summary>云函数私有网络配置。</summary>
    public class CloudBaseBatchVpcConfig
    {
        /// <summary>私有网络 ID。</summary>
        public string vpcid { get; set; }

        /// <summary>子网 ID。</summary>
        public string subnetid { get; set; }
    }

    /// <summary>通过环境和名称定位云函数的请求。</summary>
    public class CloudBaseBatchFunctionIdentityRequest
    {
        /// <summary>云函数名称。</summary>
        public string functionname { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }
    }

    /// <summary>云函数列表查询请求。</summary>
    public class CloudBaseBatchFunctionListRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>分页偏移量。</summary>
        public int? offset { get; set; }

        /// <summary>每页数量。</summary>
        public int? limit { get; set; }

        /// <summary>函数名称搜索关键字。</summary>
        public string searchkey { get; set; }
    }

    /// <summary>云函数触发器查询请求。</summary>
    public class CloudBaseBatchTriggerQueryRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云函数名称。</summary>
        public string funcname { get; set; }
    }

    /// <summary>批量更新云函数触发器请求。</summary>
    public class CloudBaseBatchUpdateTriggersRequest
    {
        /// <summary>触发器配置列表。</summary>
        public List<CloudBaseBatchTriggerInfo> triggers { get; set; }

        /// <summary>云函数名称。</summary>
        public string funcname { get; set; }

        /// <summary>目标云开发环境 ID 列表。</summary>
        public List<string> envs { get; set; }
    }

    /// <summary>云函数批量操作结果。</summary>
    public class CloudBaseBatchFunctionFailJsonResult : WxJsonResult
    {
        /// <summary>创建失败的环境列表。</summary>
        public List<CloudBaseBatchFunctionError> fail_env_list { get; set; }

        /// <summary>更新代码失败的环境列表。</summary>
        public List<CloudBaseBatchFunctionError> fail_list { get; set; }
    }

    /// <summary>云函数批量操作错误结果。</summary>
    public class CloudBaseBatchFunctionErrorJsonResult : WxJsonResult
    {
        /// <summary>处理失败的环境列表。</summary>
        public List<CloudBaseBatchFunctionError> err_list { get; set; }
    }

    /// <summary>单个环境的云函数操作错误。</summary>
    public class CloudBaseBatchFunctionError
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>错误信息；创建和触发器接口使用此字段。</summary>
        public string errmsg { get; set; }

        /// <summary>错误信息；更新代码接口使用此字段。</summary>
        public string errormsg { get; set; }
    }

    /// <summary>云函数列表结果。</summary>
    public class CloudBaseBatchFunctionListJsonResult : WxJsonResult
    {
        /// <summary>云函数总数。</summary>
        public int total_count { get; set; }

        /// <summary>云函数列表。</summary>
        public List<CloudBaseBatchFunctionInfo> functions { get; set; }
    }

    /// <summary>云函数信息。</summary>
    public class CloudBaseBatchFunctionInfo
    {
        /// <summary>最后修改时间。</summary>
        public string mod_time { get; set; }

        /// <summary>创建时间。</summary>
        public string add_time { get; set; }

        /// <summary>运行时名称。</summary>
        public string runtime { get; set; }

        /// <summary>云函数名称。</summary>
        public string name { get; set; }

        /// <summary>云函数状态。</summary>
        public string status { get; set; }

        /// <summary>状态原因列表。</summary>
        public List<CloudBaseBatchFunctionStatusReason> status_reason { get; set; }
    }

    /// <summary>云函数状态原因。</summary>
    public class CloudBaseBatchFunctionStatusReason
    {
        /// <summary>错误码。</summary>
        public string errcode { get; set; }

        /// <summary>错误信息。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>云函数触发器列表结果。</summary>
    public class CloudBaseBatchTriggerListJsonResult : WxJsonResult
    {
        /// <summary>触发器列表。</summary>
        public List<CloudBaseBatchTriggerInfo> triggers { get; set; }
    }

    /// <summary>云函数触发器信息。</summary>
    public class CloudBaseBatchTriggerInfo
    {
        /// <summary>触发器名称。</summary>
        public string trigger_name { get; set; }

        /// <summary>触发器配置。</summary>
        public string config { get; set; }

        /// <summary>触发器类型。</summary>
        public string type { get; set; }
    }

    /// <summary>调用云函数结果。</summary>
    public class CloudBaseBatchInvokeFunctionJsonResult : WxJsonResult
    {
        /// <summary>云函数返回的字符串数据。</summary>
        public string resp_data { get; set; }
    }

    #endregion

    #region 数据库管理

    /// <summary>数据库导入请求。</summary>
    public class CloudBaseBatchDbImportRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>目标集合名称。</summary>
        public string collection_name { get; set; }

        /// <summary>云存储中的导入文件路径。</summary>
        public string file_path { get; set; }

        /// <summary>文件类型：1 表示 JSON，2 表示 CSV。</summary>
        public int file_type { get; set; }

        /// <summary>遇到错误时是否停止导入。</summary>
        public bool stop_on_error { get; set; }

        /// <summary>冲突模式：1 表示 INSERT，2 表示 UPSERT。</summary>
        public int conflict_mode { get; set; }
    }

    /// <summary>数据库导出请求。</summary>
    public class CloudBaseBatchDbExportRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>导出文件在云存储中的路径。</summary>
        public string file_path { get; set; }

        /// <summary>文件类型：1 表示 JSON，2 表示 CSV。</summary>
        public int file_type { get; set; }

        /// <summary>数据库导出查询语句。</summary>
        public string query { get; set; }
    }

    /// <summary>数据库迁移状态请求。</summary>
    public class CloudBaseBatchMigrationStateRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>迁移任务 ID。</summary>
        public long job_id { get; set; }
    }

    /// <summary>数据库查询请求。</summary>
    public class CloudBaseBatchDbQueryRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>数据库命令字符串。</summary>
        public string query { get; set; }
    }

    /// <summary>数据库集合定位请求。</summary>
    public class CloudBaseBatchCollectionRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>集合名称。</summary>
        public string collection_name { get; set; }
    }

    /// <summary>设置数据库集合权限请求。</summary>
    public class CloudBaseBatchSetPermissionRequest : CloudBaseBatchCollectionRequest
    {
        /// <summary>权限类型：READONLY、PRIVATE、ADMINWRITE、ADMINONLY 或 CUSTOM。</summary>
        public string acl_tag { get; set; }

        /// <summary>CUSTOM 权限对应的自定义安全规则。</summary>
        public string rule { get; set; }
    }

    /// <summary>数据库记录管理请求。</summary>
    public class CloudBaseBatchRecordRequest
    {
        /// <summary>操作类型：insert、delete、update 或 query。</summary>
        public string action { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>数据库命令字符串。</summary>
        public string query { get; set; }
    }

    /// <summary>数据库索引管理请求。</summary>
    public class CloudBaseBatchIndexRequest
    {
        /// <summary>操作类型：create 或 delete。</summary>
        public string action { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>集合名称。</summary>
        public string collection_name { get; set; }

        /// <summary>需要创建或删除的索引列表。</summary>
        public List<CloudBaseBatchIndexInfo> indexes { get; set; }
    }

    /// <summary>数据库索引定义。</summary>
    public class CloudBaseBatchIndexInfo
    {
        /// <summary>索引名称。</summary>
        public string name { get; set; }

        /// <summary>创建索引时使用的索引字段列表；删除时可省略。</summary>
        public List<CloudBaseBatchIndexKey> keys { get; set; }
    }

    /// <summary>数据库索引字段。</summary>
    public class CloudBaseBatchIndexKey
    {
        /// <summary>字段名称。</summary>
        public string name { get; set; }

        /// <summary>索引方向。</summary>
        public string direction { get; set; }
    }

    /// <summary>数据库迁移任务创建结果。</summary>
    public class CloudBaseBatchJobJsonResult : WxJsonResult
    {
        /// <summary>迁移任务 ID。</summary>
        public long job_id { get; set; }
    }

    /// <summary>数据库迁移状态结果。</summary>
    public class CloudBaseBatchMigrationStateJsonResult : WxJsonResult
    {
        /// <summary>迁移任务状态。</summary>
        public string status { get; set; }

        /// <summary>成功处理的记录数。</summary>
        public long record_success { get; set; }

        /// <summary>处理失败的记录数。</summary>
        public long record_fail { get; set; }

        /// <summary>任务错误信息。</summary>
        public string error_msg { get; set; }

        /// <summary>结果文件 URL。</summary>
        public string file_url { get; set; }
    }

    /// <summary>数据库查询结果。</summary>
    public class CloudBaseBatchQueryJsonResult : WxJsonResult
    {
        /// <summary>JSON 字符串形式的数据列表。</summary>
        public List<string> data { get; set; }
    }

    /// <summary>数据库集合权限结果。</summary>
    public class CloudBaseBatchPermissionJsonResult : WxJsonResult
    {
        /// <summary>权限类型。</summary>
        public string acl_tag { get; set; }

        /// <summary>自定义安全规则。</summary>
        public string rule { get; set; }
    }

    /// <summary>数据库记录操作结果。</summary>
    public class CloudBaseBatchRecordJsonResult : WxJsonResult
    {
        /// <summary>新增记录的 ID 列表。</summary>
        public List<string> id_list { get; set; }

        /// <summary>删除的记录数。</summary>
        public long deleted { get; set; }

        /// <summary>匹配的记录数。</summary>
        public long matched { get; set; }

        /// <summary>修改的记录数。</summary>
        public long modified { get; set; }

        /// <summary>单条新增记录的 ID。</summary>
        public string id { get; set; }

        /// <summary>查询分页信息。</summary>
        public CloudBaseBatchPager pager { get; set; }

        /// <summary>JSON 字符串形式的查询数据。</summary>
        public List<string> data { get; set; }
    }

    /// <summary>数据库查询分页信息。</summary>
    public class CloudBaseBatchPager
    {
        /// <summary>分页偏移量。</summary>
        public int offset { get; set; }

        /// <summary>每页数量。</summary>
        public int limit { get; set; }

        /// <summary>记录总数。</summary>
        public long total { get; set; }
    }

    #endregion

    #region 文件与静态网站管理

    /// <summary>获取云存储上传链接请求。</summary>
    public class CloudBaseBatchFilePathRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云存储中的上传路径。</summary>
        public string path { get; set; }
    }

    /// <summary>批量删除云存储文件请求。</summary>
    public class CloudBaseBatchDeleteFileRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>待删除的文件 ID 列表。</summary>
        public List<string> fileid_list { get; set; }
    }

    /// <summary>云存储或静态网站文件列表请求。</summary>
    public class CloudBaseBatchFileListRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>需要匹配的文件路径前缀。</summary>
        public string prefix { get; set; }

        /// <summary>用于目录归类的定界符。</summary>
        public string delimiter { get; set; }

        /// <summary>继续列举文件时使用的起始标记。</summary>
        public string marker { get; set; }
    }

    /// <summary>批量获取云存储下载链接请求。</summary>
    public class CloudBaseBatchDownloadFileRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>待获取下载链接的文件列表。</summary>
        public List<CloudBaseBatchDownloadFileRequestItem> file_list { get; set; }
    }

    /// <summary>单个文件下载链接请求项。</summary>
    public class CloudBaseBatchDownloadFileRequestItem
    {
        /// <summary>文件 ID。</summary>
        public string fileid { get; set; }

        /// <summary>下载链接有效期，单位秒。</summary>
        public int max_age { get; set; }
    }

    /// <summary>静态网站文件上传请求。</summary>
    public class CloudBaseBatchStaticFileRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>静态网站中的文件路径。</summary>
        public string filename { get; set; }
    }

    /// <summary>云存储上传链接结果。</summary>
    public class CloudBaseBatchUploadFileJsonResult : WxJsonResult
    {
        /// <summary>上传 URL。</summary>
        public string url { get; set; }

        /// <summary>临时安全令牌。</summary>
        public string token { get; set; }

        /// <summary>COS 上传签名。</summary>
        public string authorization { get; set; }

        /// <summary>云存储文件 ID。</summary>
        public string file_id { get; set; }

        /// <summary>COS 元数据文件 ID。</summary>
        public string cos_file_id { get; set; }
    }

    /// <summary>批量删除云存储文件结果。</summary>
    public class CloudBaseBatchDeleteFileJsonResult : WxJsonResult
    {
        /// <summary>每个文件的删除结果。</summary>
        public List<CloudBaseBatchDeleteFileResultItem> delete_list { get; set; }
    }

    /// <summary>单个云存储文件删除结果。</summary>
    public class CloudBaseBatchDeleteFileResultItem
    {
        /// <summary>文件 ID。</summary>
        public string fileid { get; set; }

        /// <summary>状态码。</summary>
        public int status { get; set; }

        /// <summary>该文件的错误信息。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>云存储或静态网站文件列表结果。</summary>
    public class CloudBaseBatchFileListJsonResult : WxJsonResult
    {
        /// <summary>文件信息列表。</summary>
        public List<CloudBaseBatchFileInfo> contents { get; set; }

        /// <summary>返回内容是否被截断。</summary>
        public bool is_truncated { get; set; }
    }

    /// <summary>云存储或静态网站文件信息。</summary>
    public class CloudBaseBatchFileInfo
    {
        /// <summary>文件名称。</summary>
        public string key { get; set; }

        /// <summary>最后修改时间。</summary>
        public string last_modified { get; set; }

        /// <summary>文件 MD5。</summary>
        public string md5 { get; set; }

        /// <summary>文件大小；官方接口以字符串返回。</summary>
        public string size { get; set; }
    }

    /// <summary>批量获取云存储下载链接结果。</summary>
    public class CloudBaseBatchDownloadFileJsonResult : WxJsonResult
    {
        /// <summary>每个文件的下载链接和处理状态。</summary>
        public List<CloudBaseBatchDownloadFileResultItem> file_list { get; set; }
    }

    /// <summary>单个云存储文件下载链接结果。</summary>
    public class CloudBaseBatchDownloadFileResultItem
    {
        /// <summary>文件 ID。</summary>
        public string fileid { get; set; }

        /// <summary>下载 URL。</summary>
        public string download_url { get; set; }

        /// <summary>状态码。</summary>
        public int status { get; set; }

        /// <summary>错误信息。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>静态网站状态结果。</summary>
    public class CloudBaseBatchStaticStoreJsonResult : WxJsonResult
    {
        /// <summary>静态网站状态数据。</summary>
        public List<CloudBaseBatchStaticStoreInfo> data { get; set; }
    }

    /// <summary>静态网站状态信息。</summary>
    public class CloudBaseBatchStaticStoreInfo
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>静态网站域名。</summary>
        public string domain { get; set; }

        /// <summary>COS 存储桶名称。</summary>
        public string bucket { get; set; }

        /// <summary>所在区域；字段名沿用官方返回的 <c>regoin</c>。</summary>
        public string regoin { get; set; }

        /// <summary>静态网站状态。</summary>
        public string status { get; set; }
    }

    /// <summary>静态网站文件上传链接结果。</summary>
    public class CloudBaseBatchStaticUploadJsonResult : WxJsonResult
    {
        /// <summary>带签名的上传 URL。</summary>
        public string signed_url { get; set; }

        /// <summary>x-cos-security-token 的值。</summary>
        public string token { get; set; }
    }

    #endregion
}
