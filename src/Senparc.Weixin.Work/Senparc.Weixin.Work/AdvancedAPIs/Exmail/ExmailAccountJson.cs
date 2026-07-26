/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailAccountJson.cs
    文件功能描述：企业微信邮件账号、功能设置和新邮件接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件账号激活、功能设置和新邮件数量模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 激活或注销成员邮箱、业务邮箱账号请求。
    /// </summary>
    public class ExmailActivateAccountRequest
    {
        /// <summary>
        /// 成员 UserID；操作成员邮箱时填写。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 业务邮箱 ID；操作业务邮箱时填写。
        /// </summary>
        public int? publicemail_id { get; set; }

        /// <summary>
        /// 操作类型，按官方账号激活协议填写。
        /// </summary>
        public int? type { get; set; }
    }

    /// <summary>
    /// 获取成员邮箱功能设置请求。
    /// </summary>
    public class ExmailGetUserOptionsRequest
    {
        /// <summary>
        /// 成员 UserID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 需要查询的功能设置类型列表。
        /// </summary>
        public IList<int> type { get; set; }
    }

    /// <summary>
    /// 邮箱功能设置条目。
    /// </summary>
    public class ExmailUserOptionItem
    {
        /// <summary>
        /// 功能设置类型。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 功能设置值。
        /// </summary>
        public string value { get; set; }
    }

    /// <summary>
    /// 邮箱功能设置列表包装对象。
    /// </summary>
    public class ExmailUserOptionList
    {
        /// <summary>
        /// 功能设置列表。
        /// </summary>
        public IList<ExmailUserOptionItem> list { get; set; }
    }

    /// <summary>
    /// 更新成员邮箱功能设置请求。
    /// </summary>
    public class ExmailUpdateUserOptionsRequest
    {
        /// <summary>
        /// 成员 UserID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 待更新的功能设置。
        /// </summary>
        public ExmailUserOptionList option { get; set; }
    }

    /// <summary>
    /// 获取成员邮箱功能设置结果。
    /// </summary>
    public class ExmailUserOptionsResult : WorkJsonResult
    {
        /// <summary>
        /// 功能设置列表。
        /// </summary>
        public ExmailUserOptionList option { get; set; }
    }

    /// <summary>
    /// 获取成员新邮件数量请求。
    /// </summary>
    public class ExmailNewMailCountRequest
    {
        /// <summary>
        /// 成员 UserID。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取成员新邮件数量结果。
    /// </summary>
    public class ExmailNewMailCountResult : WorkJsonResult
    {
        /// <summary>
        /// 新邮件数量。
        /// </summary>
        public int count { get; set; }
    }
}
