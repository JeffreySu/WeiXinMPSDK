/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalMomentJson.cs
    文件功能描述：企业客户朋友圈发表、互动与规则组强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户朋友圈发表、互动与规则组模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>创建企业客户朋友圈发表任务请求。</summary>
    public class CreateMomentTaskRequest
    {
        public MomentTaskText text { get; set; }
        public IList<MomentTaskAttachment> attachments { get; set; }
        public MomentTaskVisibleRange visible_range { get; set; }
    }

    /// <summary>企业客户朋友圈文本。</summary>
    public class MomentTaskText
    {
        public string content { get; set; }
    }

    /// <summary>企业客户朋友圈附件。</summary>
    public class MomentTaskAttachment
    {
        public string msgtype { get; set; }
        public MomentTaskImage image { get; set; }
        public MomentTaskVideo video { get; set; }
        public MomentTaskLink link { get; set; }
    }

    /// <summary>企业客户朋友圈图片附件。</summary>
    public class MomentTaskImage
    {
        public string media_id { get; set; }
    }

    /// <summary>企业客户朋友圈视频附件。</summary>
    public class MomentTaskVideo
    {
        public string media_id { get; set; }
    }

    /// <summary>企业客户朋友圈链接附件。</summary>
    public class MomentTaskLink
    {
        public string title { get; set; }
        public string url { get; set; }
        public string media_id { get; set; }
    }

    /// <summary>企业客户朋友圈可见范围。</summary>
    public class MomentTaskVisibleRange
    {
        public MomentTaskSenderList sender_list { get; set; }
        public MomentTaskExternalContactList external_contact_list { get; set; }
    }

    /// <summary>企业客户朋友圈执行者范围。</summary>
    public class MomentTaskSenderList
    {
        public IList<string> user_list { get; set; }
        public IList<long> department_list { get; set; }
    }

    /// <summary>企业客户朋友圈目标客户范围。</summary>
    public class MomentTaskExternalContactList
    {
        public IList<string> tag_list { get; set; }
    }

    /// <summary>创建企业客户朋友圈发表任务结果。</summary>
    public class CreateMomentTaskResult : WorkJsonResult
    {
        public string jobid { get; set; }
    }

    /// <summary>企业客户朋友圈发表任务创建状态结果。</summary>
    public class MomentTaskCreateStatusResult : WorkJsonResult
    {
        public int status { get; set; }
        public string type { get; set; }
        public MomentTaskCreateDetail result { get; set; }
    }

    /// <summary>企业客户朋友圈发表任务创建详情。</summary>
    public class MomentTaskCreateDetail
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string moment_id { get; set; }
        public MomentTaskSenderList invalid_sender_list { get; set; }
        public MomentTaskExternalContactList invalid_external_contact_list { get; set; }
    }

    /// <summary>朋友圈客户列表请求。</summary>
    public class MomentCustomerListRequest
    {
        public string moment_id { get; set; }
        public string userid { get; set; }
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>朋友圈客户列表结果。</summary>
    public class MomentCustomerListResult : WorkJsonResult
    {
        public IList<MomentCustomerInfo> customer_list { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>朋友圈可见客户。</summary>
    public class MomentCustomerInfo
    {
        public string userid { get; set; }
        public string external_userid { get; set; }
    }

    /// <summary>朋友圈互动数据请求。</summary>
    public class MomentCommentsRequest
    {
        public string moment_id { get; set; }
        public string userid { get; set; }
    }

    /// <summary>朋友圈互动数据结果。</summary>
    public class MomentCommentsResult : WorkJsonResult
    {
        public IList<MomentInteractionInfo> comment_list { get; set; }
        public IList<MomentInteractionInfo> like_list { get; set; }
    }

    /// <summary>朋友圈评论或点赞信息。</summary>
    public class MomentInteractionInfo
    {
        public string external_userid { get; set; }
        public string userid { get; set; }
        public long create_time { get; set; }
    }

    /// <summary>客户朋友圈规则组列表请求。</summary>
    public class MomentStrategyListRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>客户朋友圈规则组列表结果。</summary>
    public class MomentStrategyListResult : WorkJsonResult
    {
        public IList<MomentStrategyIdInfo> strategy { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>客户朋友圈规则组 ID。</summary>
    public class MomentStrategyIdInfo
    {
        public long strategy_id { get; set; }
    }

    /// <summary>指定客户朋友圈规则组请求。</summary>
    public class MomentStrategyIdRequest
    {
        public long strategy_id { get; set; }
    }

    /// <summary>客户朋友圈规则组详情结果。</summary>
    public class MomentStrategyResult : WorkJsonResult
    {
        public MomentStrategyInfo strategy { get; set; }
    }

    /// <summary>客户朋友圈规则组详情。</summary>
    public class MomentStrategyInfo
    {
        public long strategy_id { get; set; }
        public long parent_id { get; set; }
        public string strategy_name { get; set; }
        public long create_time { get; set; }
        public IList<string> admin_list { get; set; }
        public MomentStrategyPrivilege privilege { get; set; }
    }

    /// <summary>客户朋友圈规则组权限。</summary>
    public class MomentStrategyPrivilege
    {
        public bool? view_moment_list { get; set; }
        public bool? send_moment { get; set; }
        public bool? manage_moment_cover_and_sign { get; set; }
    }

    /// <summary>客户朋友圈规则组管理范围请求。</summary>
    public class MomentStrategyRangeRequest : MomentStrategyIdRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>客户朋友圈规则组管理范围结果。</summary>
    public class MomentStrategyRangeResult : WorkJsonResult
    {
        public IList<CustomerStrategyRangeNode> range { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>创建客户朋友圈规则组请求。</summary>
    public class MomentStrategyCreateRequest
    {
        public long? parent_id { get; set; }
        public string strategy_name { get; set; }
        public IList<string> admin_list { get; set; }
        public MomentStrategyPrivilege privilege { get; set; }
        public IList<CustomerStrategyRangeNode> range { get; set; }
    }

    /// <summary>创建客户朋友圈规则组结果。</summary>
    public class MomentStrategyCreateResult : WorkJsonResult
    {
        public long strategy_id { get; set; }
    }

    /// <summary>编辑客户朋友圈规则组请求。</summary>
    public class MomentStrategyEditRequest : MomentStrategyIdRequest
    {
        public string strategy_name { get; set; }
        public IList<string> admin_list { get; set; }
        public MomentStrategyPrivilege privilege { get; set; }
        public IList<CustomerStrategyRangeNode> range_add { get; set; }
        public IList<CustomerStrategyRangeNode> range_del { get; set; }
    }
}
