/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AdvancedFeatureJson.cs
    文件功能描述：企业微信高级功能成员申请强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐高级功能成员申请强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.AdvancedFeature
{
    /// <summary>高级功能申请的审批节点。</summary>
    public class AdvancedFeatureApprovalNode
    {
        /// <summary>当前待处理人 UserId 列表，最多 100 个。</summary>
        public IList<string> current_approvers { get; set; }

        /// <summary>已处理人 UserId 列表，最多 100 个。</summary>
        public IList<string> completed_approvers { get; set; }

        /// <summary>节点状态：1 审批中、2 已驳回、3 已同意、101 已撤销、102 未到流程。</summary>
        public uint node_apv_status { get; set; }

        /// <summary>多人审批方式：1 会签、2 或签、3 依次审批。</summary>
        public uint node_apv_rel { get; set; }

        /// <summary>节点更新时间的 Unix 时间戳。</summary>
        public ulong? apv_update_time { get; set; }
    }

    /// <summary>高级功能申请的审批流程。</summary>
    public class AdvancedFeatureApprovalProcess
    {
        /// <summary>全量审批节点列表。</summary>
        public IList<AdvancedFeatureApprovalNode> node_list { get; set; }
    }

    /// <summary>设置高级功能申请单审批信息请求。</summary>
    public class SetAdvancedFeatureApprovalDetailRequest
    {
        /// <summary>自建应用生成的审批 ID，与申请 ID 一一对应且不可变更。</summary>
        public string approval_id { get; set; }

        /// <summary>审批单状态：1 审批中、2 已驳回、3 已同意、101 已撤销。</summary>
        public uint approval_status { get; set; }

        /// <summary>企业微信高级功能申请 ID。</summary>
        public string apply_id { get; set; }

        /// <summary>审批单跳转地址，须以 http:// 或 https:// 开头。</summary>
        public string approval_url { get; set; }

        /// <summary>审批流程；变更节点时须传入全量节点。</summary>
        public AdvancedFeatureApprovalProcess process_list { get; set; }
    }

    /// <summary>批量获取高级功能申请单 ID 请求。</summary>
    public class GetAdvancedFeatureApplyIdListRequest
    {
        /// <summary>高级账号类型：1 邮件、2 文档、3 微盘、4 会议。</summary>
        public uint business_type { get; set; }

        /// <summary>申请人的 UserId。</summary>
        public string userid { get; set; }

        /// <summary>分页数量，默认 100，最大 200。</summary>
        public uint? limit { get; set; }

        /// <summary>分页游标；首次请求可不填写。</summary>
        public string cursor { get; set; }

        /// <summary>申请单类型：0 全部、1 仅 API 申请单、2 非 API 申请单。</summary>
        public uint? req_type { get; set; }
    }

    /// <summary>批量获取高级功能申请单 ID 结果。</summary>
    public class GetAdvancedFeatureApplyIdListResult : WorkJsonResult
    {
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>申请 ID 列表。</summary>
        public IList<string> apply_id_list { get; set; }

        /// <summary>是否还有下一页数据。</summary>
        public bool has_more { get; set; }
    }

    /// <summary>获取高级功能申请单详情请求。</summary>
    public class GetAdvancedFeatureApprovalInfoRequest
    {
        /// <summary>企业微信高级功能申请 ID。</summary>
        public string apply_id { get; set; }
    }

    /// <summary>高级功能申请单详情。</summary>
    public class AdvancedFeatureApprovalInfo
    {
        /// <summary>申请人的 UserId。</summary>
        public string applicant { get; set; }

        /// <summary>申请创建时间的 Unix 时间戳。</summary>
        public ulong create_time { get; set; }

        /// <summary>高级账号类型：1 邮件、2 文档、3 微盘、4 会议。</summary>
        public uint business_type { get; set; }

        /// <summary>自建应用中的审批 ID。</summary>
        public string approval_id { get; set; }

        /// <summary>企业微信高级功能申请 ID。</summary>
        public string apply_id { get; set; }

        /// <summary>自建应用中的审批单跳转地址。</summary>
        public string approval_url { get; set; }

        /// <summary>审批状态：0 无流程、1 审批中、2 驳回、3 同意、4 管理员驳回、5 已分配。</summary>
        public uint approval_status { get; set; }

        /// <summary>审批类型：0 默认申请、1 自定义审批、2 企业 OpenAPI。</summary>
        public uint approval_type { get; set; }

        /// <summary>申请原因。</summary>
        public string request_reason { get; set; }
    }

    /// <summary>获取高级功能申请单详情结果。</summary>
    public class GetAdvancedFeatureApprovalInfoResult : WorkJsonResult
    {
        /// <summary>申请单详情。</summary>
        public AdvancedFeatureApprovalInfo approval_info { get; set; }
    }
}
