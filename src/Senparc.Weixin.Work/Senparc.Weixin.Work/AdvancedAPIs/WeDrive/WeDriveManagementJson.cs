/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveManagementJson.cs
    文件功能描述：企业微信微盘专业版、容量和高级账号接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘专业版、容量与高级账号管理模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 获取微盘专业版信息请求。
    /// </summary>
    public class WeDriveProfessionalInfoRequest
    {
        /// <summary>
        /// 操作者 UserID；不需要指定操作者时可不传。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取微盘专业版信息结果。
    /// </summary>
    public class WeDriveProfessionalInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 企业是否已开通微盘专业版。
        /// </summary>
        public bool is_pro { get; set; }

        /// <summary>
        /// 高级功能账号总数。
        /// </summary>
        public int total_vip_acct_num { get; set; }

        /// <summary>
        /// 已使用的高级功能账号数。
        /// </summary>
        public int use_vip_acct_num { get; set; }

        /// <summary>
        /// 专业版到期时间戳（秒）。
        /// </summary>
        public long? pro_expire_time { get; set; }
    }

    /// <summary>
    /// 获取微盘容量结果。
    /// </summary>
    public class WeDriveCapacityResult : WorkJsonResult
    {
        /// <summary>
        /// 全员总容量（字节）。
        /// </summary>
        public long total_capacity_for_all { get; set; }

        /// <summary>
        /// 高级账号总容量（字节）。
        /// </summary>
        public long total_capacity_for_vip { get; set; }

        /// <summary>
        /// 全员剩余容量（字节）。
        /// </summary>
        public long rest_capacity_for_all { get; set; }

        /// <summary>
        /// 高级账号剩余容量（字节）。
        /// </summary>
        public long rest_capacity_for_vip { get; set; }
    }

    /// <summary>
    /// 批量分配或撤销微盘高级功能账号请求。
    /// </summary>
    public class WeDriveVipBatchRequest
    {
        /// <summary>
        /// 企业成员 UserID 列表，单次最多 100 个。
        /// </summary>
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// 批量分配或撤销微盘高级功能账号结果。
    /// </summary>
    public class WeDriveVipBatchResult : WorkJsonResult
    {
        /// <summary>
        /// 操作成功的成员 UserID 列表。
        /// </summary>
        public IList<string> succ_userid_list { get; set; }

        /// <summary>
        /// 操作失败的成员 UserID 列表。
        /// </summary>
        public IList<string> fail_userid_list { get; set; }
    }

    /// <summary>
    /// 获取微盘高级功能账号列表请求。
    /// </summary>
    public class WeDriveVipListRequest
    {
        /// <summary>
        /// 分页游标；首次调用可不传。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 每页数量，默认 100，最大 200。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 获取微盘高级功能账号列表结果。
    /// </summary>
    public class WeDriveVipListResult : WorkJsonResult
    {
        /// <summary>
        /// 已分配高级功能且在应用可见范围内的成员 UserID 列表。
        /// </summary>
        public IList<string> userid_list { get; set; }

        /// <summary>
        /// 是否还有更多数据。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 下一页游标。
        /// </summary>
        public string next_cursor { get; set; }
    }
}
