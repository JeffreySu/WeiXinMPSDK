/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：QuerySchemeJsonResult.cs
    文件功能描述：QuerySchemeJsonResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme
{
    /// <summary>
    /// 查询小程序 URL Scheme 返回结果
    /// </summary>
    public class QuerySchemeJsonResult : WxJsonResult
    {
        public QuerySchemeInfo scheme_info { get; set; }
        public QuerySchemeQuotaInfo quota_info { get; set; }
    }

    /// <summary>
    /// QueryScheme 信息。
    /// </summary>
    public class QuerySchemeInfo
    {
        public string appid { get; set; }
        public string path { get; set; }
        public string query { get; set; }
        public long create_time { get; set; }
        public long expire_time { get; set; }
        public string env_version { get; set; }
    }

    /// <summary>
    /// QuerySchemeQuota 信息。
    /// </summary>
    public class QuerySchemeQuotaInfo
    {
        public long remain_visit_quota { get; set; }
    }
}
