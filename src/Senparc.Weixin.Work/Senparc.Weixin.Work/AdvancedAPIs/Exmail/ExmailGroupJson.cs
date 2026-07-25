/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailGroupJson.cs
    文件功能描述：企业微信邮件群组接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件群组管理模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 字符串列表包装对象。
    /// </summary>
    public class ExmailStringList
    {
        /// <summary>
        /// 字符串列表。
        /// </summary>
        public IList<string> list { get; set; }
    }

    /// <summary>
    /// 整数列表包装对象。
    /// </summary>
    public class ExmailIntList
    {
        /// <summary>
        /// 整数列表。
        /// </summary>
        public IList<int> list { get; set; }
    }

    /// <summary>
    /// 64 位整数列表包装对象。
    /// </summary>
    public class ExmailLongList
    {
        /// <summary>
        /// 64 位整数列表。
        /// </summary>
        public IList<long> list { get; set; }
    }

    /// <summary>
    /// 创建或更新邮件群组请求。
    /// </summary>
    public class ExmailGroupRequest
    {
        /// <summary>
        /// 邮件群组地址。
        /// </summary>
        public string groupid { get; set; }

        /// <summary>
        /// 邮件群组名称；更新时不修改名称可不传。
        /// </summary>
        public string groupname { get; set; }

        /// <summary>
        /// 群组包含的邮箱地址。
        /// </summary>
        public ExmailStringList email_list { get; set; }

        /// <summary>
        /// 群组包含的其他邮件群组地址。
        /// </summary>
        public ExmailStringList group_list { get; set; }

        /// <summary>
        /// 群组包含的通讯录标签 ID。
        /// </summary>
        public ExmailIntList tag_list { get; set; }

        /// <summary>
        /// 群组包含的部门 ID。
        /// </summary>
        public ExmailLongList department_list { get; set; }

        /// <summary>
        /// 群组使用权限类型。
        /// </summary>
        public int? allow_type { get; set; }

        /// <summary>
        /// 允许使用该群组的邮箱地址。
        /// </summary>
        public ExmailStringList allow_emaillist { get; set; }

        /// <summary>
        /// 允许使用该群组的标签 ID。
        /// </summary>
        public ExmailIntList allow_taglist { get; set; }

        /// <summary>
        /// 允许使用该群组的部门 ID。
        /// </summary>
        public ExmailLongList allow_departmentlist { get; set; }
    }

    /// <summary>
    /// 删除邮件群组请求。
    /// </summary>
    public class ExmailGroupIdRequest
    {
        /// <summary>
        /// 邮件群组地址。
        /// </summary>
        public string groupid { get; set; }
    }

    /// <summary>
    /// 获取邮件群组详情结果。
    /// </summary>
    public class ExmailGroupResult : WorkJsonResult
    {
        /// <summary>
        /// 邮件群组地址。
        /// </summary>
        public string groupid { get; set; }

        /// <summary>
        /// 邮件群组名称。
        /// </summary>
        public string groupname { get; set; }

        /// <summary>
        /// 群组包含的邮箱地址。
        /// </summary>
        public ExmailStringList email_list { get; set; }

        /// <summary>
        /// 群组包含的其他邮件群组地址。
        /// </summary>
        public ExmailStringList group_list { get; set; }

        /// <summary>
        /// 群组包含的通讯录标签 ID。
        /// </summary>
        public ExmailIntList tag_list { get; set; }

        /// <summary>
        /// 群组包含的部门 ID。
        /// </summary>
        public ExmailLongList department_list { get; set; }

        /// <summary>
        /// 群组使用权限类型。
        /// </summary>
        public int allow_type { get; set; }

        /// <summary>
        /// 允许使用该群组的邮箱地址。
        /// </summary>
        public ExmailStringList allow_emaillist { get; set; }

        /// <summary>
        /// 允许使用该群组的标签 ID。
        /// </summary>
        public ExmailIntList allow_taglist { get; set; }

        /// <summary>
        /// 允许使用该群组的部门 ID。
        /// </summary>
        public ExmailLongList allow_departmentlist { get; set; }
    }

    /// <summary>
    /// 邮件群组搜索结果条目。
    /// </summary>
    public class ExmailGroupSearchItem
    {
        /// <summary>
        /// 邮件群组地址。
        /// </summary>
        public string groupid { get; set; }

        /// <summary>
        /// 邮件群组名称。
        /// </summary>
        public string groupname { get; set; }
    }

    /// <summary>
    /// 搜索邮件群组结果。
    /// </summary>
    public class ExmailGroupSearchResult : WorkJsonResult
    {
        /// <summary>
        /// 匹配的邮件群组列表。
        /// </summary>
        public IList<ExmailGroupSearchItem> groups { get; set; }
    }
}
