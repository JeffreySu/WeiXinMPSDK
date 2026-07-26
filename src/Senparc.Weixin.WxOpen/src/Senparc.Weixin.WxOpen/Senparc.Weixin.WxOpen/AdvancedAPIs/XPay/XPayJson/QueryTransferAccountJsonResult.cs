/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：QueryTransferAccountJsonResult.cs
    文件功能描述：QueryTransferAccountJsonResult 强类型数据模型


    创建标识：Senparc - 20231130

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 查询广告金充值账户结果。
    /// </summary>
    public class QueryTransferAccountJsonResult : WxJsonResult
    {
        /// <summary>
        /// 广告金充值账户列表。
        /// </summary>
        public List<QueryTransferAccountItem> acct_list { get; set; }
    }

    /// <summary>
    /// 广告金充值账户信息。
    /// </summary>
    public class QueryTransferAccountItem
    {
        /// <summary>
        /// 充值账户名称
        /// </summary>
        public string transfer_account_name { get; set; }

        /// <summary>
        /// 充值账户 uid
        /// </summary>
        public long transfer_account_uid { get; set; }

        /// <summary>
        /// 充值账户服务商账号 id
        /// </summary>
        public long transfer_account_agency_id { get; set; }

        /// <summary>
        /// 充值账户服务商账号名称
        /// </summary>
        public string transfer_account_agency_name { get; set; }

        /// <summary>
        /// 审核状态：0 待审核，1 审核通过，2 审核驳回。
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 绑定结果：1 绑定成功，2 绑定失败。
        /// </summary>
        public int bind_result { get; set; }

        /// <summary>
        /// 审核或绑定失败的错误信息。
        /// </summary>
        public string error_msg { get; set; }
    }
}
