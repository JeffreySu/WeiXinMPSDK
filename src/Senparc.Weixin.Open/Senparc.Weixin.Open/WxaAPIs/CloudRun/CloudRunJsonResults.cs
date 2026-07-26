#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudRunJsonResults.cs
    文件功能描述：CloudRunJsonResults 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.CloudRun
{
    /// <summary>
    /// 云托管环境共享关系查询结果。
    /// </summary>
    public class CloudRunGetShareEnvJsonResult : WxJsonResult
    {
        /// <summary>
        /// AppID 与云托管环境的共享关系列表。
        /// </summary>
        public List<CloudRunEnvRelation> relation_data { get; set; }

        /// <summary>
        /// 查询失败的 AppID 列表。
        /// </summary>
        public List<CloudRunEnvError> err_list { get; set; }
    }

    /// <summary>
    /// 云托管环境共享或解除共享的结果。
    /// </summary>
    public class CloudRunShareEnvJsonResult : WxJsonResult
    {
        /// <summary>
        /// 处理失败的环境与 AppID 列表。
        /// </summary>
        public List<CloudRunEnvError> err_list { get; set; }

        /// <summary>
        /// 需要小程序管理员确认的链接列表。
        /// </summary>
        public List<CloudRunShareConfirmInfo> msg_info_list { get; set; }
    }

    /// <summary>
    /// AppID 与云托管环境的共享关系。
    /// </summary>
    public class CloudRunEnvRelation
    {
        /// <summary>
        /// 小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 已共享的云托管环境 ID 列表。
        /// </summary>
        public List<string> env_list { get; set; }
    }

    /// <summary>
    /// 云托管环境共享处理错误。
    /// </summary>
    public class CloudRunEnvError
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env { get; set; }

        /// <summary>
        /// 小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string errmsg { get; set; }
    }

    /// <summary>
    /// 云托管环境共享确认信息。
    /// </summary>
    public class CloudRunShareConfirmInfo
    {
        /// <summary>
        /// 需要确认的小程序 AppID。
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 发送给小程序管理员的确认链接。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 单个云托管环境共享请求项。
    /// </summary>
    public class CloudRunEnvShareItem
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env { get; set; }

        /// <summary>
        /// 需要建立或解除共享关系的小程序 AppID 列表。
        /// </summary>
        public List<string> appids { get; set; }
    }

    /// <summary>
    /// 云托管环境列表查询结果。
    /// </summary>
    public class CloudRunEnvListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 云托管环境列表。
        /// </summary>
        public List<CloudRunEnvInfo> info_list { get; set; }
    }

    /// <summary>
    /// 云托管环境信息。
    /// </summary>
    public class CloudRunEnvInfo
    {
        /// <summary>
        /// 环境 ID。
        /// </summary>
        public string env { get; set; }

        /// <summary>
        /// 环境别名。
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 最后更新时间。
        /// </summary>
        public string update_time { get; set; }

        /// <summary>
        /// 环境状态。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 云开发产品套餐 ID。
        /// </summary>
        public string package_id { get; set; }

        /// <summary>
        /// 套餐名称。
        /// </summary>
        public string package_name { get; set; }

        /// <summary>
        /// 数据库实例 ID。
        /// </summary>
        public string dbinstance_id { get; set; }

        /// <summary>
        /// 静态存储 Bucket ID。
        /// </summary>
        public string bucket_id { get; set; }
    }

    /// <summary>
    /// 创建云托管环境的请求参数。
    /// </summary>
    public class CloudRunCreateEnvRequest
    {
        /// <summary>
        /// 环境别名，应以小写字母开头且只包含小写字母、数字和连字符。
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 私有网络 ID。
        /// </summary>
        public string vpc_id { get; set; }

        /// <summary>
        /// 子网 ID 列表。
        /// </summary>
        public List<string> sub_net_ids { get; set; }
    }

    /// <summary>
    /// 创建云托管环境的结果。
    /// </summary>
    public class CloudRunCreateEnvJsonResult : WxJsonResult
    {
        /// <summary>
        /// 新创建的环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 后付费订单号。
        /// </summary>
        public string tran_id { get; set; }
    }

    /// <summary>
    /// 创建云托管服务的请求参数。
    /// </summary>
    public class CloudRunCreateServiceRequest
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 服务名称。
        /// </summary>
        public string service_name { get; set; }

        /// <summary>
        /// 是否开通外网访问。
        /// </summary>
        public bool is_public { get; set; }

        /// <summary>
        /// 镜像仓库名称。
        /// </summary>
        public string image_repo { get; set; }

        /// <summary>
        /// 服务描述。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Elasticsearch 日志配置信息。
        /// </summary>
        public CloudRunEsInfo es_info { get; set; }

        /// <summary>
        /// 日志类型，例如 <c>es</c> 或 <c>cls</c>。
        /// </summary>
        public string log_type { get; set; }

        /// <summary>
        /// 私有网络配置。
        /// </summary>
        public CloudRunVpcInfo vpc_info { get; set; }

        /// <summary>
        /// 公网访问开关，0 表示关闭，1 表示开启。
        /// </summary>
        public int? public_access { get; set; }
    }

    /// <summary>
    /// 云托管 Elasticsearch 配置。
    /// </summary>
    public class CloudRunEsInfo
    {
        /// <summary>
        /// ES 配置 ID。
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// Secret 名称。
        /// </summary>
        public string secret_name { get; set; }

        /// <summary>
        /// ES 地址。
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// ES 端口。
        /// </summary>
        public int? port { get; set; }

        /// <summary>
        /// ES 索引名称。
        /// </summary>
        public string index { get; set; }

        /// <summary>
        /// ES 用户名。
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// ES 密码。
        /// </summary>
        public string password { get; set; }
    }

    /// <summary>
    /// 云托管私有网络配置。
    /// </summary>
    public class CloudRunVpcInfo
    {
        /// <summary>
        /// 私有网络 ID。
        /// </summary>
        public string vpc_id { get; set; }

        /// <summary>
        /// 子网 ID 列表。
        /// </summary>
        public List<string> subnet_ids { get; set; }

        /// <summary>
        /// 私有网络创建类型。
        /// </summary>
        public int? create_type { get; set; }
    }

    /// <summary>
    /// 创建云托管服务版本的请求参数。
    /// </summary>
    public class CloudRunCreateServiceVersionRequest
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 上传类型：package、repository、image、jar 或 war。
        /// </summary>
        public string upload_type { get; set; }

        /// <summary>
        /// 新版本流量占比。
        /// </summary>
        public decimal flow_ratio { get; set; }

        /// <summary>
        /// CPU 大小，单位为核。
        /// </summary>
        public decimal cpu { get; set; }

        /// <summary>
        /// 内存大小，单位为 GB。
        /// </summary>
        public decimal mem { get; set; }

        /// <summary>
        /// 最小副本数。
        /// </summary>
        public int min_num { get; set; }

        /// <summary>
        /// 最大副本数，最大值为 50。
        /// </summary>
        public int max_num { get; set; }

        /// <summary>
        /// 自动扩缩容策略类型，例如 <c>cpu</c>。
        /// </summary>
        public string policy_type { get; set; }

        /// <summary>
        /// 自动扩缩容策略阈值。
        /// </summary>
        public decimal policy_threshold { get; set; }

        /// <summary>
        /// 服务容器端口。
        /// </summary>
        public int container_port { get; set; }

        /// <summary>
        /// 服务名称。
        /// </summary>
        public string server_name { get; set; }

        /// <summary>
        /// 代码仓库类型，例如 github、gitlab 或 coding。
        /// </summary>
        public string repository_type { get; set; }

        /// <summary>
        /// Dockerfile 路径。
        /// </summary>
        public string dockerfile_path { get; set; }

        /// <summary>
        /// 构建目录。
        /// </summary>
        public string build_dir { get; set; }

        /// <summary>
        /// JSON 字符串形式的环境变量。
        /// </summary>
        public string env_params { get; set; }

        /// <summary>
        /// 代码仓库地址。
        /// </summary>
        public string repository { get; set; }

        /// <summary>
        /// 代码仓库分支。
        /// </summary>
        public string branch { get; set; }

        /// <summary>
        /// 版本备注。
        /// </summary>
        public string version_remark { get; set; }

        /// <summary>
        /// 代码包名称。
        /// </summary>
        public string package_name { get; set; }

        /// <summary>
        /// 代码包版本。
        /// </summary>
        public string package_version { get; set; }

        /// <summary>
        /// 镜像信息。
        /// </summary>
        public CloudRunImageInfo image_info { get; set; }

        /// <summary>
        /// 代码仓库详情。
        /// </summary>
        public CloudRunCodeDetail code_detail { get; set; }

        /// <summary>
        /// 私有镜像凭据信息。
        /// </summary>
        public CloudRunImageSecretInfo image_secret_info { get; set; }

        /// <summary>
        /// 私有镜像认证名称。
        /// </summary>
        public string image_pull_secret { get; set; }

        /// <summary>
        /// 用户自定义日志采集路径。
        /// </summary>
        public string custom_logs { get; set; }

        /// <summary>
        /// 延迟启动健康检查的秒数。
        /// </summary>
        public int? initial_delay_seconds { get; set; }

        /// <summary>
        /// CFS 挂载信息。
        /// </summary>
        public List<CloudRunVolumeMountInfo> mount_volume_info { get; set; }

        /// <summary>
        /// 访问类型，4 表示仅允许微信链路访问。
        /// </summary>
        public int? access_type { get; set; }

        /// <summary>
        /// Elasticsearch 日志配置信息。
        /// </summary>
        public CloudRunEsInfo es_info { get; set; }

        /// <summary>
        /// 是否使用统一域名。
        /// </summary>
        public bool? enable_union { get; set; }

        /// <summary>
        /// 服务路径。
        /// </summary>
        public string server_path { get; set; }

        /// <summary>
        /// Sidecar 容器描述列表。
        /// </summary>
        public List<CloudRunSidecarSpec> sidecar_specs { get; set; }

        /// <summary>
        /// 主容器安全特性。
        /// </summary>
        public CloudRunSecurity security { get; set; }

        /// <summary>
        /// 服务数据卷列表。
        /// </summary>
        public List<CloudRunServiceVolume> service_volumes { get; set; }

        /// <summary>
        /// JnsGw 创建策略：0 默认创建，1 创建，2 不创建。
        /// </summary>
        public int? is_create_jns_gw { get; set; }

        /// <summary>
        /// 服务数据卷挂载参数。
        /// </summary>
        public List<CloudRunServiceVolumeMount> service_volume_mounts { get; set; }

        /// <summary>
        /// Dockerfile 状态：0 默认存在，1 存在，2 不存在。
        /// </summary>
        public int? has_dockerfile { get; set; }

        /// <summary>
        /// 基础镜像。
        /// </summary>
        public string base_image { get; set; }

        /// <summary>
        /// 容器启动入口命令。
        /// </summary>
        public string entry_point { get; set; }

        /// <summary>
        /// 代码仓库语言。
        /// </summary>
        public string repo_language { get; set; }

        /// <summary>
        /// 上传文件名称。
        /// </summary>
        public string upload_filename { get; set; }

        /// <summary>
        /// 自动扩缩容策略组。
        /// </summary>
        public List<CloudRunPolicyDetail> policy_detail { get; set; }
    }

    /// <summary>
    /// 滚动更新云托管服务版本的请求参数。
    /// </summary>
    public class CloudRunUpdateServiceVersionRequest
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 要替换的版本名称，可使用 <c>latest</c>。
        /// </summary>
        public string version_name { get; set; }

        /// <summary>
        /// 上传类型：package、repository 或 image。
        /// </summary>
        public string upload_type { get; set; }

        /// <summary>
        /// 代码仓库类型。
        /// </summary>
        public string repository_type { get; set; }

        /// <summary>
        /// 新版本流量占比。
        /// </summary>
        public decimal? flow_ratio { get; set; }

        /// <summary>
        /// Dockerfile 路径。
        /// </summary>
        public string dockerfile_path { get; set; }

        /// <summary>
        /// 构建目录。
        /// </summary>
        public string build_dir { get; set; }

        /// <summary>
        /// CPU 大小。官方更新接口示例以字符串传递。
        /// </summary>
        public string cpu { get; set; }

        /// <summary>
        /// 内存大小。官方更新接口示例以字符串传递。
        /// </summary>
        public string mem { get; set; }

        /// <summary>
        /// 最小副本数。官方更新接口示例以字符串传递。
        /// </summary>
        public string min_num { get; set; }

        /// <summary>
        /// 最大副本数。官方更新接口示例以字符串传递。
        /// </summary>
        public string max_num { get; set; }

        /// <summary>
        /// 自动扩缩容策略类型。
        /// </summary>
        public string policy_type { get; set; }

        /// <summary>
        /// 自动扩缩容策略阈值。官方更新接口示例以字符串传递。
        /// </summary>
        public string policy_threshold { get; set; }

        /// <summary>
        /// JSON 字符串形式的环境变量。
        /// </summary>
        public string env_params { get; set; }

        /// <summary>
        /// 容器端口。
        /// </summary>
        public int? container_port { get; set; }

        /// <summary>
        /// 服务名称。
        /// </summary>
        public string server_name { get; set; }

        /// <summary>
        /// 代码仓库地址。
        /// </summary>
        public string repository { get; set; }

        /// <summary>
        /// 代码仓库分支。
        /// </summary>
        public string branch { get; set; }

        /// <summary>
        /// 版本备注。
        /// </summary>
        public string version_remark { get; set; }

        /// <summary>
        /// 代码包名称。
        /// </summary>
        public string package_name { get; set; }

        /// <summary>
        /// 代码包版本。
        /// </summary>
        public string package_version { get; set; }

        /// <summary>
        /// 镜像信息。
        /// </summary>
        public CloudRunImageInfo image_info { get; set; }

        /// <summary>
        /// 代码仓库详情。
        /// </summary>
        public CloudRunCodeDetail code_detail { get; set; }

        /// <summary>
        /// 是否回放流量。
        /// </summary>
        public bool? is_rebuild { get; set; }

        /// <summary>
        /// 延迟启动健康检查的秒数。
        /// </summary>
        public int? initial_delay_seconds { get; set; }

        /// <summary>
        /// CFS 挂载信息。
        /// </summary>
        public List<CloudRunVolumeMountInfo> mount_volume_info { get; set; }

        /// <summary>
        /// 是否执行版本回滚。
        /// </summary>
        public bool? rollback { get; set; }

        /// <summary>
        /// 版本历史快照名称。
        /// </summary>
        public string snapshot_name { get; set; }

        /// <summary>
        /// 自定义日志采集路径。
        /// </summary>
        public string custom_logs { get; set; }

        /// <summary>
        /// 是否启用统一域名。
        /// </summary>
        public bool? enable_union { get; set; }

        /// <summary>
        /// 服务路径，仅首次设置时生效。
        /// </summary>
        public string server_path { get; set; }

        /// <summary>
        /// 是否更新 CLS 配置。
        /// </summary>
        public bool? is_update_cls { get; set; }

        /// <summary>
        /// 自动扩缩容策略组。
        /// </summary>
        public List<CloudRunPolicyDetail> policy_detail { get; set; }
    }

    /// <summary>
    /// 云托管镜像信息。
    /// </summary>
    public class CloudRunImageInfo
    {
        /// <summary>
        /// 镜像仓库名称。
        /// </summary>
        public string repository_name { get; set; }

        /// <summary>
        /// 是否为公有镜像。
        /// </summary>
        public bool? is_public { get; set; }

        /// <summary>
        /// 镜像标签。
        /// </summary>
        public string tag_name { get; set; }

        /// <summary>
        /// 镜像服务地址。
        /// </summary>
        public string server_addr { get; set; }

        /// <summary>
        /// 镜像拉取地址。
        /// </summary>
        public string image_url { get; set; }
    }

    /// <summary>
    /// 云托管代码仓库详情。
    /// </summary>
    public class CloudRunCodeDetail
    {
        /// <summary>
        /// 代码仓库名称信息。
        /// </summary>
        public CloudRunRepositoryName name { get; set; }

        /// <summary>
        /// 代码仓库 URL。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 云托管代码仓库名称信息。
    /// </summary>
    public class CloudRunRepositoryName
    {
        /// <summary>
        /// 代码仓库短名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 代码仓库完整名称。
        /// </summary>
        public string full_name { get; set; }
    }

    /// <summary>
    /// 私有镜像凭据信息。
    /// </summary>
    public class CloudRunImageSecretInfo
    {
        /// <summary>
        /// 镜像注册服务地址。
        /// </summary>
        public string registry_server { get; set; }

        /// <summary>
        /// 镜像仓库用户名。
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 镜像仓库密码。
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 镜像仓库账号邮箱。
        /// </summary>
        public string email { get; set; }
    }

    /// <summary>
    /// 云托管 CFS 或 NFS 挂载信息。
    /// </summary>
    public class CloudRunVolumeMountInfo
    {
        /// <summary>
        /// 挂载资源名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 容器内挂载路径。
        /// </summary>
        public string mount_path { get; set; }

        /// <summary>
        /// 是否只读挂载。
        /// </summary>
        public bool? read_only { get; set; }

        /// <summary>
        /// NFS 挂载配置列表。
        /// </summary>
        public List<CloudRunNfsVolume> nfs_volumes { get; set; }
    }

    /// <summary>
    /// 云托管 NFS 数据卷配置。
    /// </summary>
    public class CloudRunNfsVolume
    {
        /// <summary>
        /// NFS 服务地址。
        /// </summary>
        public string server { get; set; }

        /// <summary>
        /// NFS 服务路径。
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 是否只读挂载。
        /// </summary>
        public bool? read_only { get; set; }

        /// <summary>
        /// Secret 名称。
        /// </summary>
        public string secret_name { get; set; }

        /// <summary>
        /// 是否启用临时目录数据卷。
        /// </summary>
        public bool? enable_empty_dir_volume { get; set; }
    }

    /// <summary>
    /// 云托管 Sidecar 容器描述。
    /// </summary>
    public class CloudRunSidecarSpec
    {
        /// <summary>
        /// Sidecar 容器镜像。
        /// </summary>
        public string container_image { get; set; }

        /// <summary>
        /// Sidecar 容器端口。
        /// </summary>
        public int? container_port { get; set; }

        /// <summary>
        /// Sidecar 容器名称。
        /// </summary>
        public string container_name { get; set; }

        /// <summary>
        /// JSON 字符串形式的环境变量。
        /// </summary>
        public string env_var { get; set; }

        /// <summary>
        /// 延迟启动健康检查的秒数。
        /// </summary>
        public int? initial_delay_seconds { get; set; }

        /// <summary>
        /// CPU 大小。
        /// </summary>
        public decimal? cpu { get; set; }

        /// <summary>
        /// 内存大小，单位为 MB。
        /// </summary>
        public decimal? mem { get; set; }

        /// <summary>
        /// Sidecar 安全特性。
        /// </summary>
        public CloudRunSecurity security { get; set; }

        /// <summary>
        /// Sidecar 数据卷挂载信息。
        /// </summary>
        public List<CloudRunVolumeMountInfo> volume_mount_infos { get; set; }
    }

    /// <summary>
    /// 云托管容器安全特性。
    /// </summary>
    public class CloudRunSecurity
    {
        /// <summary>
        /// Linux capabilities 配置。
        /// </summary>
        public CloudRunCapabilities capabilities { get; set; }
    }

    /// <summary>
    /// 云托管容器 Linux capabilities 配置。
    /// </summary>
    public class CloudRunCapabilities
    {
        /// <summary>
        /// 启用的安全能力项列表。
        /// </summary>
        public List<string> add { get; set; }

        /// <summary>
        /// 禁用的安全能力项列表。
        /// </summary>
        public List<string> drop { get; set; }
    }

    /// <summary>
    /// 云托管服务数据卷。
    /// </summary>
    public class CloudRunServiceVolume
    {
        /// <summary>
        /// 数据卷名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// NFS 数据卷配置。
        /// </summary>
        public CloudRunNfsVolume nfs { get; set; }

        /// <summary>
        /// Secret 名称。
        /// </summary>
        public string secret_name { get; set; }

        /// <summary>
        /// 是否启用旧版临时目录数据卷。
        /// </summary>
        public bool? enable_empty_dir_volume { get; set; }

        /// <summary>
        /// EmptyDir 数据卷配置。
        /// </summary>
        public CloudRunEmptyDir empty_dir { get; set; }
    }

    /// <summary>
    /// 云托管 EmptyDir 数据卷配置。
    /// </summary>
    public class CloudRunEmptyDir
    {
        /// <summary>
        /// 是否启用 EmptyDir 数据卷。
        /// </summary>
        public bool? enable_empty_dir_volume { get; set; }

        /// <summary>
        /// 存储介质，可为空、Memory 或 HugePages。
        /// </summary>
        public string medium { get; set; }

        /// <summary>
        /// EmptyDir 数据卷容量限制。
        /// </summary>
        public string size_limit { get; set; }
    }

    /// <summary>
    /// 云托管服务数据卷挂载参数。
    /// </summary>
    public class CloudRunServiceVolumeMount
    {
        /// <summary>
        /// 数据卷名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 容器内挂载路径。
        /// </summary>
        public string mount_path { get; set; }

        /// <summary>
        /// 是否只读挂载。
        /// </summary>
        public bool? read_only { get; set; }

        /// <summary>
        /// 数据卷子路径。
        /// </summary>
        public string sub_path { get; set; }

        /// <summary>
        /// 挂载传播方式。
        /// </summary>
        public string mount_propagation { get; set; }
    }

    /// <summary>
    /// 云托管自动扩缩容策略项。
    /// </summary>
    public class CloudRunPolicyDetail
    {
        /// <summary>
        /// 策略类型。
        /// </summary>
        public string policy_type { get; set; }

        /// <summary>
        /// 策略阈值。
        /// </summary>
        public decimal? policy_threshold { get; set; }
    }

    /// <summary>
    /// 创建或更新云托管服务版本的结果。
    /// </summary>
    public class CloudRunServiceVersionJsonResult : WxJsonResult
    {
        /// <summary>
        /// 操作结果，例如 creating 或 succ。
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 新版本名称；创建成功时返回。
        /// </summary>
        public string version_name { get; set; }

        /// <summary>
        /// 操作记录 ID。
        /// </summary>
        public string run_id { get; set; }
    }

    /// <summary>
    /// 删除云托管服务版本的请求参数。
    /// </summary>
    public class CloudRunDeleteServiceVersionRequest
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 服务名称。
        /// </summary>
        public string server_name { get; set; }

        /// <summary>
        /// 版本名称。
        /// </summary>
        public string version_name { get; set; }

        /// <summary>
        /// 删除最后一个版本时是否同时删除服务。
        /// </summary>
        public bool? is_delete_server { get; set; }

        /// <summary>
        /// 删除服务时是否同时删除镜像。
        /// </summary>
        public bool? is_delete_image { get; set; }
    }

    /// <summary>
    /// 发布云托管服务版本的请求参数。
    /// </summary>
    public class CloudRunReleaseServiceVersionRequest
    {
        /// <summary>
        /// 云托管环境 ID。
        /// </summary>
        public string env_id { get; set; }

        /// <summary>
        /// 服务名称。
        /// </summary>
        public string server_name { get; set; }

        /// <summary>
        /// 待发布的版本名称。
        /// </summary>
        public string release_version { get; set; }
    }

    /// <summary>
    /// 删除或发布云托管服务版本的结果。
    /// </summary>
    public class CloudRunReleaseJsonResult : WxJsonResult
    {
        /// <summary>
        /// 操作结果，例如 succ、success 或 failed。
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 发布单 ID，仅发布接口返回。
        /// </summary>
        public long release_order_id { get; set; }
    }
}
