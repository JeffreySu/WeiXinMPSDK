#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CloudBaseModels.cs
    文件功能描述：第三方平台普通代云开发接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.CloudBase
{
    #region 管理与环境

    /// <summary>腾讯云 API 临时调用凭证结果。</summary>
    public class CloudBaseQCloudTokenJsonResult : WxJsonResult
    {
        /// <summary>腾讯云临时 SecretId。</summary>
        public string secretid { get; set; }

        /// <summary>腾讯云临时 SecretKey。</summary>
        public string secretkey { get; set; }

        /// <summary>临时安全令牌。</summary>
        public string token { get; set; }

        /// <summary>凭证过期时间戳。</summary>
        public long expired_time { get; set; }
    }

    /// <summary>授权小程序手机号绑定状态结果。</summary>
    public class CloudBaseMobileConfigJsonResult : WxJsonResult
    {
        /// <summary>授权小程序是否已绑定手机号。</summary>
        public bool has_mobile { get; set; }
    }

    /// <summary>创建云开发环境结果。</summary>
    public class CloudBaseCreateEnvJsonResult : WxJsonResult
    {
        /// <summary>新创建的云开发环境 ID。</summary>
        public string envid { get; set; }
    }

    /// <summary>授权小程序云开发环境查询结果。</summary>
    public class CloudBaseEnvInfoJsonResult : WxJsonResult
    {
        /// <summary>云开发环境列表。</summary>
        public List<CloudBaseEnvInfo> info_list { get; set; }
    }

    /// <summary>授权小程序云开发环境信息。</summary>
    public class CloudBaseEnvInfo
    {
        /// <summary>环境 ID。</summary>
        public string env { get; set; }

        /// <summary>环境别名。</summary>
        public string alias { get; set; }

        /// <summary>开通时间。</summary>
        public string create_time { get; set; }

        /// <summary>最后修改时间。</summary>
        public string update_time { get; set; }

        /// <summary>环境状态。</summary>
        public string status { get; set; }

        /// <summary>产品套餐 ID。</summary>
        public string package_id { get; set; }

        /// <summary>产品套餐中文名。</summary>
        public string package_name { get; set; }
    }

    #endregion

    #region 消息推送

    /// <summary>云开发消息推送配置。</summary>
    public class CloudBaseCallbackConfig
    {
        /// <summary>云函数消息推送配置；为空时不更新。</summary>
        public CloudBaseFunctionCallbackConfig function_config { get; set; }

        /// <summary>云托管消息推送配置；为空时不更新。</summary>
        public CloudBaseContainerCallbackConfig container_config { get; set; }
    }

    /// <summary>云函数消息推送配置。</summary>
    public class CloudBaseFunctionCallbackConfig
    {
        /// <summary>是否启用云函数消息推送。</summary>
        public bool enable { get; set; }

        /// <summary>消息类型与云函数的映射列表。</summary>
        public List<CloudBaseFunctionCallbackItem> callbacks { get; set; }
    }

    /// <summary>单条云函数消息推送配置。</summary>
    public class CloudBaseFunctionCallbackItem
    {
        /// <summary>消息类型；字段名沿用官方协议。</summary>
        public string msgType { get; set; }

        /// <summary>事件类型；非事件消息可传空字符串。</summary>
        public string @event { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>接收消息的云函数名称；字段名沿用官方协议。</summary>
        public string functionName { get; set; }

        /// <summary>是否启用该条配置。</summary>
        public bool enable { get; set; }
    }

    /// <summary>云托管消息推送配置。</summary>
    public class CloudBaseContainerCallbackConfig
    {
        /// <summary>是否启用云托管消息推送。</summary>
        public bool enable { get; set; }

        /// <summary>云托管环境 ID。</summary>
        public string qbase_env { get; set; }

        /// <summary>接收消息的云托管路径。</summary>
        public string qbase_container_path { get; set; }

        /// <summary>文本格式：1 表示 JSON，2 表示 XML。</summary>
        public int text_mode { get; set; }
    }

    /// <summary>云开发消息推送配置查询结果。</summary>
    public class CloudBaseCallbackConfigJsonResult : WxJsonResult
    {
        /// <summary>云函数和云托管消息推送配置。</summary>
        public CloudBaseCallbackConfig data { get; set; }
    }

    #endregion

    #region 云函数

    /// <summary>云函数代码保护密钥结果。</summary>
    public class CloudBaseCodeSecretJsonResult : WxJsonResult
    {
        /// <summary>代码保护密钥。</summary>
        public string codesecret { get; set; }
    }

    /// <summary>云函数上传签名结果。</summary>
    public class CloudBaseUploadSignatureJsonResult : WxJsonResult
    {
        /// <summary>调用腾讯云 SCF 上传接口时使用的带签名请求头。</summary>
        public string headers { get; set; }
    }

    /// <summary>云函数列表查询请求。</summary>
    public class CloudBaseFunctionListRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>本次获取数量限制。</summary>
        public int limit { get; set; }

        /// <summary>分页偏移量。</summary>
        public int offset { get; set; }
    }

    /// <summary>云函数标识请求。</summary>
    public class CloudBaseFunctionIdentityRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云函数名称。</summary>
        public string function_name { get; set; }
    }

    /// <summary>云函数代码下载地址结果。</summary>
    public class CloudBaseFunctionLinkJsonResult : WxJsonResult
    {
        /// <summary>云函数代码下载地址。</summary>
        public string url { get; set; }

        /// <summary>云函数代码包的 SHA-256 摘要。</summary>
        public string checksum { get; set; }
    }

    /// <summary>上传云函数配置请求。</summary>
    public class CloudBaseUploadFunctionConfigRequest
    {
        /// <summary>配置类型：1 云调用，2 定时触发器，3 环境配置。</summary>
        public int type { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云函数名称。</summary>
        public string function_name { get; set; }

        /// <summary>配置 JSON 字符串。</summary>
        public string config { get; set; }
    }

    /// <summary>获取云函数配置请求。</summary>
    public class CloudBaseFunctionConfigRequest
    {
        /// <summary>配置类型：1 云调用，2 定时触发器，3 环境配置。</summary>
        public int type { get; set; }

        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>云函数名称。</summary>
        public string function_name { get; set; }
    }

    /// <summary>云函数配置查询结果。</summary>
    public class CloudBaseFunctionConfigJsonResult : WxJsonResult
    {
        /// <summary>配置 JSON 字符串。</summary>
        public string config { get; set; }
    }

    #endregion

    #region 数据库管理

    /// <summary>数据库更新记录结果。</summary>
    public class CloudBaseDatabaseUpdateJsonResult : WxJsonResult
    {
        /// <summary>匹配到的记录数。</summary>
        public long matched { get; set; }

        /// <summary>实际修改的记录数；通过 set 新增的记录不计入。</summary>
        public long modified { get; set; }

        /// <summary>通过 set 新增记录时返回的记录 ID。</summary>
        public string id { get; set; }
    }

    /// <summary>数据库集合批量管理请求。</summary>
    public class CloudBaseCollectionManageRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>返回数据长度；action=get 时可选。</summary>
        public int? limit { get; set; }

        /// <summary>数据偏移量；action=get 时可选。</summary>
        public int? offset { get; set; }

        /// <summary>操作类型：get、add 或 del。</summary>
        public string action { get; set; }

        /// <summary>集合名称；action=add 或 del 时必填。</summary>
        public string collection_name { get; set; }
    }

    /// <summary>数据库集合批量管理结果。</summary>
    public class CloudBaseCollectionManageJsonResult : WxJsonResult
    {
        /// <summary>集合信息；action=get 时返回。</summary>
        public List<CloudBaseCollectionInfo> collections { get; set; }

        /// <summary>集合总数；action=get 时返回。</summary>
        public long total { get; set; }
    }

    /// <summary>数据库插入记录结果。</summary>
    public class CloudBaseDatabaseAddJsonResult : WxJsonResult
    {
        /// <summary>插入成功的记录 ID 列表。</summary>
        public List<string> id_list { get; set; }
    }

    /// <summary>数据库集合列表查询请求。</summary>
    public class CloudBaseCollectionListRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>可选获取数量限制。</summary>
        public int? limit { get; set; }

        /// <summary>可选分页偏移量。</summary>
        public int? offset { get; set; }
    }

    /// <summary>数据库集合列表结果。</summary>
    public class CloudBaseCollectionListJsonResult : WxJsonResult
    {
        /// <summary>数据库集合信息。</summary>
        public List<CloudBaseCollectionInfo> collections { get; set; }

        /// <summary>分页信息。</summary>
        public CloudBasePager pager { get; set; }
    }

    /// <summary>数据库集合信息。</summary>
    public class CloudBaseCollectionInfo
    {
        /// <summary>集合名称。</summary>
        public string name { get; set; }

        /// <summary>集合中文档数量。</summary>
        public long count { get; set; }

        /// <summary>集合中文档总大小，单位字节。</summary>
        public long size { get; set; }

        /// <summary>索引数量。</summary>
        public long index_count { get; set; }

        /// <summary>索引占用大小，单位字节。</summary>
        public long index_size { get; set; }
    }

    /// <summary>数据库查询分页信息。</summary>
    public class CloudBasePager
    {
        /// <summary>分页偏移量；字段名沿用官方协议。</summary>
        public int Offset { get; set; }

        /// <summary>单次查询限制；字段名沿用官方协议。</summary>
        public int Limit { get; set; }

        /// <summary>符合条件的记录总数；字段名沿用官方协议。</summary>
        public long Total { get; set; }
    }

    /// <summary>数据库记录数统计结果。</summary>
    public class CloudBaseDatabaseCountJsonResult : WxJsonResult
    {
        /// <summary>符合条件的记录数量。</summary>
        public long count { get; set; }
    }

    /// <summary>数据库删除记录结果。</summary>
    public class CloudBaseDatabaseDeleteJsonResult : WxJsonResult
    {
        /// <summary>删除的记录数量。</summary>
        public long deleted { get; set; }
    }

    /// <summary>数据库查询记录结果。</summary>
    public class CloudBaseDatabaseQueryJsonResult : WxJsonResult
    {
        /// <summary>分页信息。</summary>
        public CloudBasePager pager { get; set; }

        /// <summary>JSON 字符串形式的记录列表。</summary>
        public List<string> data { get; set; }
    }

    /// <summary>更新数据库索引请求。</summary>
    public class CloudBaseUpdateIndexRequest
    {
        /// <summary>云开发环境 ID。</summary>
        public string env { get; set; }

        /// <summary>集合名称。</summary>
        public string collection_name { get; set; }

        /// <summary>需要新增的索引列表。</summary>
        public List<CloudBaseIndexDefinition> create_indexes { get; set; }

        /// <summary>需要删除的索引列表。</summary>
        public List<CloudBaseIndexDefinition> drop_indexes { get; set; }
    }

    /// <summary>数据库索引定义。</summary>
    public class CloudBaseIndexDefinition
    {
        /// <summary>索引名称。</summary>
        public string name { get; set; }

        /// <summary>是否为唯一索引；删除索引时可省略。</summary>
        public bool? unique { get; set; }

        /// <summary>索引字段；删除索引时可省略。</summary>
        public List<CloudBaseIndexKey> keys { get; set; }
    }

    /// <summary>数据库索引字段。</summary>
    public class CloudBaseIndexKey
    {
        /// <summary>字段名称。</summary>
        public string name { get; set; }

        /// <summary>字段排序方向，例如 asc、desc 或 2dsphere。</summary>
        public string direction { get; set; }
    }

    #endregion

    #region 微信支付授权

    /// <summary>云开发微信支付商户号列表结果。</summary>
    public class CloudBaseWechatPayListJsonResult : WxJsonResult
    {
        /// <summary>授权绑定的商户号列表。</summary>
        public List<CloudBaseWechatPayInfo> list { get; set; }
    }

    /// <summary>云开发微信支付商户号信息。</summary>
    public class CloudBaseWechatPayInfo
    {
        /// <summary>微信支付商户号。</summary>
        public string merchant_code { get; set; }

        /// <summary>商户简称。</summary>
        public string merchant_name { get; set; }

        /// <summary>商户主体名称。</summary>
        public string company_name { get; set; }

        /// <summary>商户号绑定关系状态；官方参数表写为 number，实际示例返回字符串枚举。</summary>
        public string mch_relation_state { get; set; }

        /// <summary>JSAPI 授权状态；官方参数表写为 number，实际示例返回字符串枚举。</summary>
        public string jsapi_auth_state { get; set; }

        /// <summary>退款授权状态；官方参数表写为 number，实际示例返回字符串枚举。</summary>
        public string refund_auth_state { get; set; }
    }

    /// <summary>申请微信支付商户号授权请求。</summary>
    public class CloudBaseWechatPayAuthRequest
    {
        /// <summary>操作类型：bind、openjsapi 或 openrefund。</summary>
        public string action { get; set; }

        /// <summary>微信支付商户号。</summary>
        public string merchant_code { get; set; }
    }

    #endregion
}
