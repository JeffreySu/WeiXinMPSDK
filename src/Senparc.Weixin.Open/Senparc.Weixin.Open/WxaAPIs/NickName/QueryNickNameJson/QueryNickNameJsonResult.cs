using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.NickName.QueryNickNameJson
{

    [Serializable]
    public class QueryNickNameJsonResult : WxJsonResult
    {
        /// <summary>
        /// 审核昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 审核状态，1：审核中，2：审核失败，3：审核成功
        /// </summary>
        public AuditStat audit_stat { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 审核提交时间
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 审核完成时间
        /// </summary>
        public long audit_time { get; set; }
    }
}
