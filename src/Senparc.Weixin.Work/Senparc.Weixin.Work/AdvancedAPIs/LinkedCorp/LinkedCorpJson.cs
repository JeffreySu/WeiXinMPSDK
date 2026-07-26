/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LinkedCorpJson.cs
    文件功能描述：企业微信互联企业通讯录请求和响应模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增互联企业权限、成员、部门及自定义属性强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.LinkedCorp
{
    /// <summary>
    /// 获取互联企业应用可见范围请求。
    /// </summary>
    public class LinkedCorpAgentPermissionListRequest
    {
    }

    /// <summary>
    /// 获取互联企业应用可见范围结果。
    /// </summary>
    public class LinkedCorpAgentPermissionListResult : WorkJsonResult
    {
        /// <summary>
        /// 可见成员账号列表，格式为“CorpId/UserId”。
        /// </summary>
        public string[] userids { get; set; }

        /// <summary>
        /// 可见部门 ID 列表，格式为“LinkedId/DepartmentId”。
        /// </summary>
        public string[] department_ids { get; set; }
    }

    /// <summary>
    /// 获取互联企业部门列表请求。
    /// </summary>
    public class LinkedCorpDepartmentListRequest
    {
        /// <summary>
        /// 互联企业部门 ID，格式为“LinkedId/DepartmentId”。
        /// </summary>
        public string department_id { get; set; }
    }

    /// <summary>
    /// 获取互联企业部门列表结果。
    /// </summary>
    public class LinkedCorpDepartmentListResult : WorkJsonResult
    {
        /// <summary>
        /// 互联企业部门列表。
        /// </summary>
        public List<LinkedCorpDepartment> department_list { get; set; }
    }

    /// <summary>
    /// 互联企业部门。
    /// </summary>
    public class LinkedCorpDepartment
    {
        /// <summary>
        /// 部门 ID。
        /// </summary>
        public long department_id { get; set; }

        /// <summary>
        /// 部门名称。
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// 上级部门 ID。
        /// </summary>
        public long parentid { get; set; }

        /// <summary>
        /// 在上级部门中的排序值。
        /// </summary>
        public long order { get; set; }
    }

    /// <summary>
    /// 获取互联企业成员详情请求。
    /// </summary>
    public class LinkedCorpUserGetRequest
    {
        /// <summary>
        /// 互联企业成员账号，格式为“CorpId/UserId”。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取互联企业部门成员列表请求。
    /// </summary>
    public class LinkedCorpUserListRequest
    {
        /// <summary>
        /// 互联企业部门 ID，格式为“LinkedId/DepartmentId”。
        /// </summary>
        public string department_id { get; set; }

        /// <summary>
        /// 是否递归获取子部门成员。
        /// </summary>
        public bool? fetch_child { get; set; }
    }

    /// <summary>
    /// 获取互联企业部门成员简要列表结果。
    /// </summary>
    public class LinkedCorpSimpleUserListResult : WorkJsonResult
    {
        /// <summary>
        /// 互联企业成员简要信息列表。
        /// </summary>
        public List<LinkedCorpSimpleUser> userlist { get; set; }
    }

    /// <summary>
    /// 互联企业成员简要信息。
    /// </summary>
    public class LinkedCorpSimpleUser
    {
        /// <summary>
        /// 成员所属企业 CorpId。
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 成员账号。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 成员所属的互联企业部门 ID 列表，格式为“LinkedId/DepartmentId”。
        /// </summary>
        public string[] department { get; set; }
    }

    /// <summary>
    /// 获取互联企业部门成员详情列表结果。
    /// </summary>
    public class LinkedCorpUserListResult : WorkJsonResult
    {
        /// <summary>
        /// 互联企业成员详情列表。
        /// </summary>
        public List<LinkedCorpUser> userlist { get; set; }
    }

    /// <summary>
    /// 获取互联企业成员详情结果。
    /// </summary>
    public class LinkedCorpUserGetResult : WorkJsonResult
    {
        /// <summary>
        /// 互联企业成员详情。
        /// </summary>
        public LinkedCorpUser user_info { get; set; }
    }

    /// <summary>
    /// 互联企业成员详情。
    /// </summary>
    public class LinkedCorpUser : LinkedCorpSimpleUser
    {
        /// <summary>
        /// 手机号码。
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 座机号码。
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 邮箱地址。
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 职务。
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// 自定义属性。
        /// </summary>
        public LinkedCorpExtendedAttributes extattr { get; set; }
    }

    /// <summary>
    /// 互联企业成员自定义属性集合。
    /// </summary>
    public class LinkedCorpExtendedAttributes
    {
        /// <summary>
        /// 自定义属性列表。
        /// </summary>
        public List<LinkedCorpExtendedAttribute> attrs { get; set; }
    }

    /// <summary>
    /// 互联企业成员自定义属性。
    /// </summary>
    public class LinkedCorpExtendedAttribute
    {
        /// <summary>
        /// 属性名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 属性类型：0 表示文本，1 表示网页。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 文本属性值。
        /// </summary>
        public LinkedCorpTextAttribute text { get; set; }

        /// <summary>
        /// 网页属性值。
        /// </summary>
        public LinkedCorpWebAttribute web { get; set; }
    }

    /// <summary>
    /// 互联企业成员文本属性。
    /// </summary>
    public class LinkedCorpTextAttribute
    {
        /// <summary>
        /// 文本内容。
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 互联企业成员网页属性。
    /// </summary>
    public class LinkedCorpWebAttribute
    {
        /// <summary>
        /// 网页 URL。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 网页标题。
        /// </summary>
        public string title { get; set; }
    }
}
