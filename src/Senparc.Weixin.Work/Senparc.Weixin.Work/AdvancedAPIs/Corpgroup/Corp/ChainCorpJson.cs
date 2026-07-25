/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChainCorpJson.cs
    文件功能描述：企业微信上下游分组及企业信息接口请求和响应模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增上下游分组、企业列表和企业详情强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取上下游通讯录分组请求。
    /// </summary>
    public class GetChainGroupRequest
    {
        /// <summary>
        /// 上下游 ID。
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 分组 ID；不填写时返回全部分组，填写时返回该分组的下级分组。
        /// </summary>
        public int? groupid { get; set; }
    }

    /// <summary>
    /// 获取上下游通讯录分组结果。
    /// </summary>
    public class GetChainGroupResult : WorkJsonResult
    {
        /// <summary>
        /// 企业上下游通讯录分组列表。
        /// </summary>
        public List<GetChainGroupItem> groups { get; set; }
    }

    /// <summary>
    /// 上下游通讯录分组。
    /// </summary>
    public class GetChainGroupItem
    {
        /// <summary>
        /// 分组 ID。
        /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 分组名称。
        /// </summary>
        public string group_name { get; set; }

        /// <summary>
        /// 上级分组 ID。
        /// </summary>
        public int parentid { get; set; }

        /// <summary>
        /// 在上级分组中的排序值。
        /// </summary>
        public long order { get; set; }
    }

    /// <summary>
    /// 获取上下游企业列表请求。
    /// </summary>
    public class GetChainCorpInfoListRequest
    {
        /// <summary>
        /// 上下游 ID。
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 分组 ID；不填写时查询全部分组中的企业。
        /// </summary>
        public int? groupid { get; set; }

        /// <summary>
        /// 是否返回尚未加入上下游的企业。
        /// </summary>
        public bool? need_pending { get; set; }

        /// <summary>
        /// 翻页游标；首次请求可不填写。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取上下游企业列表结果。
    /// </summary>
    public class GetChainCorpInfoListResult : WorkJsonResult
    {
        /// <summary>
        /// 分组中的企业列表。
        /// </summary>
        public List<GetChainCorpInfoListItem> group_corps { get; set; }

        /// <summary>
        /// 是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 上下游企业列表项。
    /// </summary>
    public class GetChainCorpInfoListItem
    {
        /// <summary>
        /// 企业所在的分组 ID。
        /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 已加入上下游企业的 CorpId。
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 尚未加入上下游企业的临时 CorpId。
        /// </summary>
        public string pending_corpid { get; set; }

        /// <summary>
        /// 企业名称。
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 企业在该上下游中的自定义 ID。
        /// </summary>
        public string custom_id { get; set; }

        /// <summary>
        /// 邀请该企业加入上下游的成员 UserId。
        /// </summary>
        public string invite_userid { get; set; }

        /// <summary>
        /// 企业是否已经加入上下游。
        /// </summary>
        public bool is_joined { get; set; }
    }

    /// <summary>
    /// 获取上下游企业详情请求。
    /// </summary>
    public class GetChainCorpInfoRequest
    {
        /// <summary>
        /// 上下游 ID。
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 已加入上下游企业的 CorpId；与 <see cref="pending_corpid"/> 至少填写一个。
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 尚未加入上下游企业的临时 CorpId；与 <see cref="corpid"/> 至少填写一个。
        /// </summary>
        public string pending_corpid { get; set; }
    }

    /// <summary>
    /// 获取上下游企业详情结果。
    /// </summary>
    public class GetChainCorpInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 企业名称。
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 企业所在的分组 ID。
        /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 企业在该上下游中的自定义 ID。
        /// </summary>
        public string custom_id { get; set; }

        /// <summary>
        /// 企业验证认证状态，具体取值以官方文档为准。
        /// </summary>
        public int? qualification_status { get; set; }

        /// <summary>
        /// 企业是否已经加入上下游。
        /// </summary>
        public bool is_joined { get; set; }
    }
}
