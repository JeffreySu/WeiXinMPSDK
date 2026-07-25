/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalCustomerManagementJson.cs
    文件功能描述：客户联系规则组与客户继承强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户联系规则组与客户继承模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>客户联系规则组分页请求。</summary>
    public class CustomerStrategyListRequest
    {
        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>分页大小，最大 1000。</summary>
        public int? limit { get; set; }
    }

    /// <summary>客户联系规则组列表结果。</summary>
    public class CustomerStrategyListResult : WorkJsonResult
    {
        /// <summary>规则组列表。</summary>
        public IList<CustomerStrategyIdInfo> strategy { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>客户联系规则组 ID。</summary>
    public class CustomerStrategyIdInfo
    {
        /// <summary>规则组 ID。</summary>
        public long strategy_id { get; set; }
    }

    /// <summary>指定客户联系规则组请求。</summary>
    public class CustomerStrategyIdRequest
    {
        /// <summary>规则组 ID。</summary>
        public long strategy_id { get; set; }
    }

    /// <summary>客户联系规则组详情结果。</summary>
    public class CustomerStrategyResult : WorkJsonResult
    {
        /// <summary>规则组详情。</summary>
        public CustomerStrategyInfo strategy { get; set; }
    }

    /// <summary>客户联系规则组详情。</summary>
    public class CustomerStrategyInfo
    {
        /// <summary>规则组 ID。</summary>
        public long strategy_id { get; set; }

        /// <summary>父规则组 ID，没有父规则组时为 0。</summary>
        public long parent_id { get; set; }

        /// <summary>规则组名称。</summary>
        public string strategy_name { get; set; }

        /// <summary>规则组创建时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>规则组管理员 UserID 列表。</summary>
        public IList<string> admin_list { get; set; }

        /// <summary>规则组权限。</summary>
        public CustomerStrategyPrivilege privilege { get; set; }
    }

    /// <summary>客户联系规则组权限。</summary>
    public class CustomerStrategyPrivilege
    {
        public bool? view_customer_list { get; set; }
        public bool? view_customer_data { get; set; }
        public bool? view_room_list { get; set; }
        public bool? contact_me { get; set; }
        public bool? join_room { get; set; }
        public bool? share_customer { get; set; }
        public bool? oper_resign_customer { get; set; }
        public bool? oper_resign_group { get; set; }
        public bool? send_customer_msg { get; set; }
        public bool? edit_welcome_msg { get; set; }
        public bool? view_behavior_data { get; set; }
        public bool? view_room_data { get; set; }
        public bool? send_group_msg { get; set; }
        public bool? room_deduplication { get; set; }
        public bool? rapid_reply { get; set; }
        public bool? onjob_customer_transfer { get; set; }
        public bool? edit_anti_spam_rule { get; set; }
        public bool? export_customer_list { get; set; }
        public bool? export_customer_data { get; set; }
        public bool? export_customer_group_list { get; set; }
        public bool? manage_customer_tag { get; set; }
    }

    /// <summary>客户联系规则组管理范围请求。</summary>
    public class CustomerStrategyRangeRequest : CustomerStrategyIdRequest
    {
        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>分页大小，最大 1000。</summary>
        public int? limit { get; set; }
    }

    /// <summary>客户联系规则组管理范围结果。</summary>
    public class CustomerStrategyRangeResult : WorkJsonResult
    {
        /// <summary>成员或部门范围节点。</summary>
        public IList<CustomerStrategyRangeNode> range { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>客户联系规则组管理范围节点。</summary>
    public class CustomerStrategyRangeNode
    {
        /// <summary>节点类型：1 成员，2 部门。</summary>
        public int type { get; set; }

        /// <summary>成员 UserID，仅成员节点有效。</summary>
        public string userid { get; set; }

        /// <summary>部门 ID，仅部门节点有效。</summary>
        public long? partyid { get; set; }
    }

    /// <summary>创建客户联系规则组请求。</summary>
    public class CustomerStrategyCreateRequest
    {
        /// <summary>父规则组 ID。</summary>
        public long? parent_id { get; set; }

        /// <summary>规则组名称。</summary>
        public string strategy_name { get; set; }

        /// <summary>规则组管理员 UserID 列表，最多 20 个。</summary>
        public IList<string> admin_list { get; set; }

        /// <summary>规则组权限。</summary>
        public CustomerStrategyPrivilege privilege { get; set; }

        /// <summary>规则组管理范围，单次最多 100 个节点。</summary>
        public IList<CustomerStrategyRangeNode> range { get; set; }
    }

    /// <summary>创建客户联系规则组结果。</summary>
    public class CustomerStrategyCreateResult : WorkJsonResult
    {
        /// <summary>新规则组 ID。</summary>
        public long strategy_id { get; set; }
    }

    /// <summary>编辑客户联系规则组请求。</summary>
    public class CustomerStrategyEditRequest : CustomerStrategyIdRequest
    {
        /// <summary>规则组名称。</summary>
        public string strategy_name { get; set; }

        /// <summary>覆盖后的管理员 UserID 列表。</summary>
        public IList<string> admin_list { get; set; }

        /// <summary>覆盖后的规则组权限。</summary>
        public CustomerStrategyPrivilege privilege { get; set; }

        /// <summary>新增的管理范围节点。</summary>
        public IList<CustomerStrategyRangeNode> range_add { get; set; }

        /// <summary>移除的管理范围节点。</summary>
        public IList<CustomerStrategyRangeNode> range_del { get; set; }
    }

    /// <summary>分配在职成员客户请求。</summary>
    public class OnJobCustomerTransferRequest
    {
        /// <summary>原跟进成员 UserID。</summary>
        public string handover_userid { get; set; }

        /// <summary>接替成员 UserID。</summary>
        public string takeover_userid { get; set; }

        /// <summary>客户 ExternalUserID 列表，最多 100 个。</summary>
        public IList<string> external_userid { get; set; }

        /// <summary>转移成功后发送给客户的消息。</summary>
        public string transfer_success_msg { get; set; }
    }

    /// <summary>分配离职成员客户请求。</summary>
    public class ResignedCustomerTransferRequest
    {
        /// <summary>离职成员 UserID。</summary>
        public string handover_userid { get; set; }

        /// <summary>接替成员 UserID。</summary>
        public string takeover_userid { get; set; }

        /// <summary>客户 ExternalUserID 列表，最多 100 个。</summary>
        public IList<string> external_userid { get; set; }
    }

    /// <summary>客户分配发起结果。</summary>
    public class CustomerTransferResult : WorkJsonResult
    {
        /// <summary>逐客户发起结果。</summary>
        public IList<CustomerTransferItem> customer { get; set; }
    }

    /// <summary>逐客户分配发起结果。</summary>
    public class CustomerTransferItem
    {
        /// <summary>客户 ExternalUserID。</summary>
        public string external_userid { get; set; }

        /// <summary>该客户的发起结果错误码。</summary>
        public int errcode { get; set; }
    }

    /// <summary>查询客户接替状态请求。</summary>
    public class CustomerTransferQueryRequest
    {
        /// <summary>原跟进成员 UserID。</summary>
        public string handover_userid { get; set; }

        /// <summary>接替成员 UserID。</summary>
        public string takeover_userid { get; set; }

        /// <summary>分页游标。</summary>
        public string cursor { get; set; }
    }

    /// <summary>客户接替状态查询结果。</summary>
    public class CustomerTransferQueryResult : WorkJsonResult
    {
        /// <summary>逐客户接替状态。</summary>
        public IList<CustomerTransferQueryItem> customer { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>逐客户接替状态。</summary>
    public class CustomerTransferQueryItem
    {
        /// <summary>客户 ExternalUserID。</summary>
        public string external_userid { get; set; }

        /// <summary>接替状态：1 完成，2 等待，3 拒绝，4 接替成员客户数达上限。</summary>
        public int status { get; set; }

        /// <summary>接替时间或预计自动接替时间戳。</summary>
        public long takeover_time { get; set; }
    }

    /// <summary>待分配离职成员客户列表请求。</summary>
    public class UnassignedCustomerListRequest
    {
        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>分页大小，最大 1000。</summary>
        public int? page_size { get; set; }
    }

    /// <summary>待分配离职成员客户列表结果。</summary>
    public class UnassignedCustomerListResult : WorkJsonResult
    {
        /// <summary>待分配客户信息。</summary>
        public IList<UnassignedCustomerInfo> info { get; set; }

        /// <summary>是否为最后一页。</summary>
        public bool is_last { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>待分配的离职成员客户。</summary>
    public class UnassignedCustomerInfo
    {
        /// <summary>离职成员 UserID。</summary>
        public string handover_userid { get; set; }

        /// <summary>客户 ExternalUserID。</summary>
        public string external_userid { get; set; }

        /// <summary>成员离职时间戳。</summary>
        public long dimission_time { get; set; }
    }
}
