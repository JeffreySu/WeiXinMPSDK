/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailPublicMailJson.cs
    文件功能描述：企业微信业务邮箱接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐业务邮箱及客户端专用密码模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 业务邮箱客户端专用密码创建信息。
    /// </summary>
    public class ExmailAuthCodeInfo
    {
        /// <summary>
        /// 客户端专用密码备注。
        /// </summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// 创建业务邮箱请求。
    /// </summary>
    public class ExmailPublicMailCreateRequest
    {
        /// <summary>
        /// 业务邮箱地址。
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 业务邮箱名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 允许使用该邮箱的成员 UserID。
        /// </summary>
        public ExmailStringList userid_list { get; set; }

        /// <summary>
        /// 允许使用该邮箱的标签 ID。
        /// </summary>
        public ExmailIntList tag_list { get; set; }

        /// <summary>
        /// 允许使用该邮箱的部门 ID。
        /// </summary>
        public ExmailLongList department_list { get; set; }

        /// <summary>
        /// 是否创建客户端专用密码：0 否，1 是。
        /// </summary>
        public int? create_auth_code { get; set; }

        /// <summary>
        /// 客户端专用密码信息。
        /// </summary>
        public ExmailAuthCodeInfo auth_code_info { get; set; }
    }

    /// <summary>
    /// 更新业务邮箱请求。
    /// </summary>
    public class ExmailPublicMailUpdateRequest
    {
        /// <summary>
        /// 业务邮箱 ID。
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 新的业务邮箱名称；不修改时可不传。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 允许使用该邮箱的成员 UserID。
        /// </summary>
        public ExmailStringList userid_list { get; set; }

        /// <summary>
        /// 允许使用该邮箱的标签 ID。
        /// </summary>
        public ExmailIntList tag_list { get; set; }

        /// <summary>
        /// 允许使用该邮箱的部门 ID。
        /// </summary>
        public ExmailLongList department_list { get; set; }

        /// <summary>
        /// 业务邮箱别名地址。
        /// </summary>
        public ExmailStringList alias_list { get; set; }

        /// <summary>
        /// 是否创建客户端专用密码：0 否，1 是。
        /// </summary>
        public int? create_auth_code { get; set; }

        /// <summary>
        /// 客户端专用密码信息。
        /// </summary>
        public ExmailAuthCodeInfo auth_code_info { get; set; }
    }

    /// <summary>
    /// 仅包含业务邮箱 ID 的请求。
    /// </summary>
    public class ExmailPublicMailIdRequest
    {
        /// <summary>
        /// 业务邮箱 ID。
        /// </summary>
        public int id { get; set; }
    }

    /// <summary>
    /// 批量获取业务邮箱请求。
    /// </summary>
    public class ExmailPublicMailIdListRequest
    {
        /// <summary>
        /// 业务邮箱 ID 列表。
        /// </summary>
        public IList<int> id_list { get; set; }
    }

    /// <summary>
    /// 删除业务邮箱客户端专用密码请求。
    /// </summary>
    public class ExmailDeleteAuthCodeRequest
    {
        /// <summary>
        /// 业务邮箱 ID。
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 客户端专用密码 ID。
        /// </summary>
        public int auth_code_id { get; set; }
    }

    /// <summary>
    /// 创建业务邮箱结果。
    /// </summary>
    public class ExmailPublicMailCreateResult : WorkJsonResult
    {
        /// <summary>
        /// 新建业务邮箱 ID。
        /// </summary>
        public int id { get; set; }
    }

    /// <summary>
    /// 业务邮箱详情。
    /// </summary>
    public class ExmailPublicMailItem
    {
        /// <summary>
        /// 业务邮箱 ID。
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 业务邮箱地址。
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 业务邮箱名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 可使用该邮箱的成员 UserID。
        /// </summary>
        public ExmailStringList userid_list { get; set; }

        /// <summary>
        /// 可使用该邮箱的标签 ID。
        /// </summary>
        public ExmailIntList tag_list { get; set; }

        /// <summary>
        /// 可使用该邮箱的部门 ID。
        /// </summary>
        public ExmailLongList department_list { get; set; }

        /// <summary>
        /// 业务邮箱别名地址。
        /// </summary>
        public ExmailStringList alias_list { get; set; }
    }

    /// <summary>
    /// 批量获取业务邮箱结果。
    /// </summary>
    public class ExmailPublicMailListResult : WorkJsonResult
    {
        /// <summary>
        /// 业务邮箱详情列表。
        /// </summary>
        public IList<ExmailPublicMailItem> list { get; set; }
    }

    /// <summary>
    /// 业务邮箱搜索结果条目。
    /// </summary>
    public class ExmailPublicMailSearchItem
    {
        /// <summary>
        /// 业务邮箱 ID。
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 业务邮箱地址。
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 业务邮箱名称。
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 搜索业务邮箱结果。
    /// </summary>
    public class ExmailPublicMailSearchResult : WorkJsonResult
    {
        /// <summary>
        /// 匹配的业务邮箱列表。
        /// </summary>
        public IList<ExmailPublicMailSearchItem> list { get; set; }
    }

    /// <summary>
    /// 业务邮箱客户端专用密码条目。
    /// </summary>
    public class ExmailAuthCodeItem
    {
        /// <summary>
        /// 客户端专用密码 ID。
        /// </summary>
        public int auth_code_id { get; set; }

        /// <summary>
        /// 客户端专用密码备注。
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 最后使用时间的 Unix 时间戳（秒）。
        /// </summary>
        public long last_use_time { get; set; }

        /// <summary>
        /// 创建时间的 Unix 时间戳（秒）。
        /// </summary>
        public long create_time { get; set; }
    }

    /// <summary>
    /// 获取业务邮箱客户端专用密码列表结果。
    /// </summary>
    public class ExmailAuthCodeListResult : WorkJsonResult
    {
        /// <summary>
        /// 客户端专用密码列表。
        /// </summary>
        public IList<ExmailAuthCodeItem> auth_code_list { get; set; }
    }
}
