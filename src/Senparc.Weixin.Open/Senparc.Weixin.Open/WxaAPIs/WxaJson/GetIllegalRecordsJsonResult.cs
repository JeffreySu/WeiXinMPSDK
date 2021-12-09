using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetIllegalRecordsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 违规处罚记录id
        /// </summary>
        public string illegal_record_id { get; set; }

        /// <summary>
        /// 违规处罚时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 违规原因
        /// </summary>
        public string illegal_reason { get; set; }

        /// <summary>
        /// 违规内容
        /// </summary>
        public string illegal_content { get; set; }

        /// <summary>
        /// 规则链接
        /// </summary>
        public string rule_url { get; set; }

        /// <summary>
        /// 违反的规则名称
        /// </summary>
        public string rule_name { get; set; }
    }
}
