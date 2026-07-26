/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：QueryUrlLinkJsonResult.cs
    文件功能描述：QueryUrlLinkJsonResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.UrlLinkJson
{
    /// <summary>
    /// 查询小程序 URL Link 返回结果
    /// </summary>
    public class QueryUrlLinkJsonResult : WxJsonResult
    {
        public QueryUrlLinkInfo url_link_info { get; set; }
        public QueryUrlLinkQuotaInfo quota_info { get; set; }
    }

    /// <summary>
    /// QueryUrlLink 信息。
    /// </summary>
    public class QueryUrlLinkInfo
    {
        public string appid { get; set; }
        public string path { get; set; }
        public string query { get; set; }
        public long create_time { get; set; }
        public long expire_time { get; set; }
        public string env_version { get; set; }
        public QueryUrlLinkCloudBase cloud_base { get; set; }
    }

    /// <summary>
    /// QueryUrlLinkCloudBase 微信接口数据模型。
    /// </summary>
    public class QueryUrlLinkCloudBase
    {
        public string env { get; set; }

        /// <summary>
        /// 微信官方返回字段当前拼写为 doamin。
        /// </summary>
        public string doamin { get; set; }

        public string path { get; set; }
        public string query { get; set; }
        public string resource_appid { get; set; }
    }

    /// <summary>
    /// QueryUrlLinkQuota 信息。
    /// </summary>
    public class QueryUrlLinkQuotaInfo
    {
        public long remain_visit_quota { get; set; }
    }
}
