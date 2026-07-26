/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExmailVipJson.cs
    文件功能描述：企业微信邮件高级功能账号强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐邮件高级功能账号请求、结果和分页模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Exmail
{
    /// <summary>
    /// 批量分配或撤销邮件高级功能账号请求。
    /// </summary>
    public class ExmailVipBatchRequest
    {
        /// <summary>
        /// 获取或设置待操作的成员 UserID 列表；单次最多 100 个。
        /// </summary>
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// 批量分配或撤销邮件高级功能账号结果。
    /// </summary>
    public class ExmailVipBatchResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置操作成功的成员 UserID 列表。
        /// </summary>
        public IList<string> succ_userid_list { get; set; }

        /// <summary>
        /// 获取或设置操作失败的成员 UserID 列表。
        /// </summary>
        public IList<string> fail_userid_list { get; set; }
    }

    /// <summary>
    /// 分页获取邮件高级功能账号请求。
    /// </summary>
    public class ExmailVipListRequest
    {
        /// <summary>
        /// 获取或设置上一页返回的分页游标；首次请求可不填。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 获取或设置每页数量；默认 100，最大 200。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 分页获取邮件高级功能账号结果。
    /// </summary>
    public class ExmailVipListResult : WorkJsonResult
    {
        /// <summary>
        /// 获取或设置是否还有更多数据未获取。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 获取或设置下一页请求应使用的游标。
        /// </summary>
        public string next_cursor { get; set; }

        /// <summary>
        /// 获取或设置已分配邮件高级功能的成员 UserID 列表。
        /// </summary>
        public IList<string> userid_list { get; set; }
    }
}
