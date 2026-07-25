/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MemberAuthorizationJson.cs
    文件功能描述：企业微信成员授权与二次验证强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐成员授权、选人结果和二次验证模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList
{
    /// <summary>
    /// 获取成员授权列表请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/94513">企业微信官方文档</see></para>
    /// </summary>
    public class GetMemberAuthListRequest
    {
        /// <summary>上一次调用返回的分页游标；首次调用可不填写。</summary>
        public string cursor { get; set; }

        /// <summary>分页大小。</summary>
        public int? limit { get; set; }
    }

    /// <summary>成员授权信息。</summary>
    public class MemberAuthInfo
    {
        /// <summary>第三方应用中的成员唯一标识。</summary>
        public string open_userid { get; set; }
    }

    /// <summary>获取成员授权列表结果。</summary>
    public class GetMemberAuthListResult : WorkJsonResult
    {
        /// <summary>下一页游标；为空表示没有更多数据。</summary>
        public string next_cursor { get; set; }

        /// <summary>成员授权列表。</summary>
        public IList<MemberAuthInfo> member_auth_list { get; set; }
    }

    /// <summary>
    /// 查询成员授权状态请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/94514">企业微信官方文档</see></para>
    /// </summary>
    public class CheckMemberAuthRequest
    {
        /// <summary>第三方应用中的成员唯一标识。</summary>
        public string open_userid { get; set; }
    }

    /// <summary>查询成员授权状态结果。</summary>
    public class CheckMemberAuthResult : WorkJsonResult
    {
        /// <summary>成员是否已授权当前应用。</summary>
        public bool is_member_auth { get; set; }
    }

    /// <summary>
    /// 获取 SelectedTicket 对应成员请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/94894">企业微信官方文档</see></para>
    /// </summary>
    public class GetSelectedTicketUsersRequest
    {
        /// <summary>选人 JSAPI 返回的 SelectedTicket。</summary>
        public string selected_ticket { get; set; }
    }

    /// <summary>获取 SelectedTicket 对应成员结果。</summary>
    public class GetSelectedTicketUsersResult : WorkJsonResult
    {
        /// <summary>执行选人操作的成员 OpenUserId。</summary>
        public string operator_open_userid { get; set; }

        /// <summary>当前应用可见范围内的已选成员 OpenUserId 列表。</summary>
        public IList<string> open_userid_list { get; set; }

        /// <summary>未授权当前应用的已选成员 OpenUserId 列表。</summary>
        public IList<string> unauth_open_userid_list { get; set; }

        /// <summary>本次选择的成员总数。</summary>
        public int total { get; set; }
    }

    /// <summary>
    /// 二次验证完成请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/99500">企业微信官方文档</see></para>
    /// </summary>
    public class TfaSuccessRequest
    {
        /// <summary>完成二次验证的成员账号。</summary>
        public string userid { get; set; }

        /// <summary>企业微信下发的二次验证授权码。</summary>
        public string tfa_code { get; set; }
    }
}
