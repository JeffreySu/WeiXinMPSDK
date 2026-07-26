/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocJson.cs
    文件功能描述：企业微信文档基础管理与高级账号强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐文档基础管理、权限及高级账号模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>新建文档或电子表格请求。</summary>
    public class WeDocCreateRequest
    {
        /// <summary>微盘空间 SpaceID；指定时需同时填写 <see cref="fatherid"/>。</summary>
        public string spaceid { get; set; }

        /// <summary>父目录 FileID；在空间根目录时填写 SpaceID。</summary>
        public string fatherid { get; set; }

        /// <summary>文档类型：3 文档，4 电子表格。</summary>
        public int doc_type { get; set; }

        /// <summary>文档名称，最长 255 个字符。</summary>
        public string doc_name { get; set; }

        /// <summary>文档管理员 UserID 列表。</summary>
        public IList<string> admin_users { get; set; }
    }

    /// <summary>新建文档结果。</summary>
    public class WeDocCreateResult : WorkJsonResult
    {
        /// <summary>新建文档访问地址。</summary>
        public string url { get; set; }

        /// <summary>新建文档 DocID。</summary>
        public string docid { get; set; }
    }

    /// <summary>重命名文档或收集表请求。</summary>
    public class WeDocRenameRequest
    {
        /// <summary>文档 DocID；与 <see cref="formid"/> 二选一。</summary>
        public string docid { get; set; }

        /// <summary>收集表 FormID；与 <see cref="docid"/> 二选一。</summary>
        public string formid { get; set; }

        /// <summary>新名称，最长 255 个字符。</summary>
        public string new_name { get; set; }
    }

    /// <summary>文档或收集表资源标识请求。</summary>
    public class WeDocResourceIdRequest
    {
        /// <summary>文档 DocID；与 <see cref="formid"/> 二选一。</summary>
        public string docid { get; set; }

        /// <summary>收集表 FormID；与 <see cref="docid"/> 二选一。</summary>
        public string formid { get; set; }
    }

    /// <summary>仅包含文档 DocID 的请求。</summary>
    public class WeDocIdRequest
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }
    }

    /// <summary>文档基础信息结果。</summary>
    public class WeDocBaseInfoResult : WorkJsonResult
    {
        /// <summary>文档基础信息。</summary>
        public WeDocBaseInfo doc_base_info { get; set; }
    }

    /// <summary>文档基础信息。</summary>
    public class WeDocBaseInfo
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>文档名称。</summary>
        public string doc_name { get; set; }

        /// <summary>文档创建 Unix 时间戳（秒）。</summary>
        public long create_time { get; set; }

        /// <summary>文档最后修改 Unix 时间戳（秒）。</summary>
        public long modify_time { get; set; }

        /// <summary>文档类型：3 文档，4 电子表格。</summary>
        public int doc_type { get; set; }
    }

    /// <summary>文档或收集表分享链接结果。</summary>
    public class WeDocShareResult : WorkJsonResult
    {
        /// <summary>分享链接。</summary>
        public string share_url { get; set; }
    }

    /// <summary>文档图片上传结果。</summary>
    public class WeDocImageUploadResult : WorkJsonResult
    {
        /// <summary>图片访问地址。</summary>
        public string url { get; set; }

        /// <summary>兼容协议返回的图片访问地址。</summary>
        public string image_url { get; set; }

        /// <summary>上传文件 ID。</summary>
        public string fileid { get; set; }

        /// <summary>图片 ID。</summary>
        public string imageid { get; set; }

        /// <summary>媒体 ID。</summary>
        public string media_id { get; set; }

        /// <summary>图片 MD5 摘要。</summary>
        public string md5 { get; set; }
    }

    /// <summary>文档查看规则。</summary>
    public class WeDocAccessRule
    {
        /// <summary>是否允许企业内成员访问。</summary>
        public bool? enable_corp_internal { get; set; }

        /// <summary>企业内默认权限。</summary>
        public int? corp_internal_auth { get; set; }

        /// <summary>是否允许企业外成员访问。</summary>
        public bool? enable_corp_external { get; set; }

        /// <summary>企业外默认权限。</summary>
        public int? corp_external_auth { get; set; }

        /// <summary>企业内访问申请是否仅管理员可审批。</summary>
        public bool? corp_internal_approve_only_by_admin { get; set; }

        /// <summary>企业外访问申请是否仅管理员可审批。</summary>
        public bool? corp_external_approve_only_by_admin { get; set; }

        /// <summary>是否禁止分享到企业外。</summary>
        public bool? ban_share_external { get; set; }
    }

    /// <summary>文档水印设置。</summary>
    public class WeDocWatermark
    {
        /// <summary>水印边距类型。</summary>
        public int? margin_type { get; set; }

        /// <summary>是否显示访问者名称。</summary>
        public bool? show_visitor_name { get; set; }

        /// <summary>是否显示自定义文字。</summary>
        public bool? show_text { get; set; }

        /// <summary>自定义水印文字。</summary>
        public string text { get; set; }
    }

    /// <summary>文档安全设置。</summary>
    public class WeDocSecureSetting
    {
        /// <summary>只读成员是否可复制内容。</summary>
        public bool? enable_readonly_copy { get; set; }

        /// <summary>只读成员是否可评论。</summary>
        public bool? enable_readonly_comment { get; set; }

        /// <summary>水印设置。</summary>
        public WeDocWatermark watermark { get; set; }
    }

    /// <summary>文档成员权限。</summary>
    public class WeDocMember
    {
        /// <summary>成员类型。</summary>
        public int type { get; set; }

        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>临时外部联系人 UserID。</summary>
        public string tmp_external_userid { get; set; }

        /// <summary>部门 ID；支持超过 32 位的企业微信部门编号。</summary>
        public long? departmentid { get; set; }

        /// <summary>文档权限。</summary>
        public int? auth { get; set; }
    }

    /// <summary>企业内协作范围权限。</summary>
    public class WeDocCoAuthInfo
    {
        /// <summary>协作对象类型。</summary>
        public int type { get; set; }

        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>临时外部联系人 UserID。</summary>
        public string tmp_external_userid { get; set; }

        /// <summary>部门 ID；支持超过 32 位的企业微信部门编号。</summary>
        public long? departmentid { get; set; }

        /// <summary>文档权限。</summary>
        public int? auth { get; set; }
    }

    /// <summary>文档权限信息结果。</summary>
    public class WeDocAuthResult : WorkJsonResult
    {
        /// <summary>企业内外查看规则。</summary>
        public WeDocAccessRule access_rule { get; set; }

        /// <summary>文档安全设置。</summary>
        public WeDocSecureSetting secure_setting { get; set; }

        /// <summary>文档成员及权限列表。</summary>
        public IList<WeDocMember> doc_member_list { get; set; }

        /// <summary>企业协作范围列表。</summary>
        public IList<WeDocCoAuthInfo> co_auth_list { get; set; }
    }

    /// <summary>修改文档查看规则请求。</summary>
    public class WeDocModifyJoinRuleRequest : WeDocAccessRule
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>是否同时更新企业协作范围列表。</summary>
        public bool? update_co_auth_list { get; set; }

        /// <summary>新的企业协作范围列表。</summary>
        public IList<WeDocCoAuthInfo> co_auth_list { get; set; }
    }

    /// <summary>修改文档成员及权限请求。</summary>
    public class WeDocModifyMemberRequest : WeDocIdRequest
    {
        /// <summary>待新增或更新的成员权限列表。</summary>
        public IList<WeDocMember> update_file_member_list { get; set; }

        /// <summary>待删除的成员列表。</summary>
        public IList<WeDocMember> del_file_member_list { get; set; }
    }

    /// <summary>修改文档安全设置请求。</summary>
    public class WeDocModifySafetySettingRequest : WeDocIdRequest
    {
        /// <summary>只读成员是否可复制内容；不修改时不传。</summary>
        public bool? enable_readonly_copy { get; set; }

        /// <summary>只读成员是否可评论；不修改时不传。</summary>
        public bool? enable_readonly_comment { get; set; }

        /// <summary>水印设置；不修改时不传。</summary>
        public WeDocWatermark watermark { get; set; }
    }

    /// <summary>文档高级功能账号批量分配或撤销请求。</summary>
    public class WeDocVipBatchRequest
    {
        /// <summary>企业成员 UserID 列表。</summary>
        public IList<string> userid_list { get; set; }
    }

    /// <summary>文档高级功能账号批量操作结果。</summary>
    public class WeDocVipBatchResult : WorkJsonResult
    {
        /// <summary>操作成功的成员 UserID 列表。</summary>
        public IList<string> succ_userid_list { get; set; }

        /// <summary>操作失败的成员 UserID 列表。</summary>
        public IList<string> fail_userid_list { get; set; }
    }

    /// <summary>分页获取文档高级功能账号请求。</summary>
    public class WeDocVipListRequest
    {
        /// <summary>分页游标；首次请求可不传。</summary>
        public string cursor { get; set; }

        /// <summary>每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>文档高级功能账号列表结果。</summary>
    public class WeDocVipListResult : WorkJsonResult
    {
        /// <summary>已分配高级功能账号的成员 UserID 列表。</summary>
        public IList<string> userid_list { get; set; }

        /// <summary>是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>文档高级功能账号请求。</summary>
    public class WeDocAdminRequest : WeDocIdRequest
    {
        /// <summary>企业成员 UserID；与 <see cref="open_userid"/> 按接口场景选择。</summary>
        public string userid { get; set; }

        /// <summary>OpenUserID；与 <see cref="userid"/> 按接口场景选择。</summary>
        public string open_userid { get; set; }

        /// <summary>高级功能账号类型。</summary>
        public int? type { get; set; }
    }

    /// <summary>文档高级功能账号信息。</summary>
    public class WeDocAdmin
    {
        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>OpenUserID。</summary>
        public string open_userid { get; set; }

        /// <summary>高级功能账号类型。</summary>
        public int? type { get; set; }
    }

    /// <summary>文档高级功能账号列表结果。</summary>
    public class WeDocAdminListResult : WeDocVipListResult
    {
        /// <summary>文档 DocID。</summary>
        public string docid { get; set; }

        /// <summary>高级功能账号列表。</summary>
        public IList<WeDocAdmin> admin_list { get; set; }
    }
}
