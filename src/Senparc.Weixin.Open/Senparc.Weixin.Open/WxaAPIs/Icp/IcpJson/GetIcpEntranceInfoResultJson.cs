using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    public class GetIcpEntranceInfoResultJson : WxJsonResult
    {
        public GetIcpEntranceInfoModel info { get; set; }
    }

    /// <summary>
    /// 备案状态信息
    /// </summary>
    public class GetIcpEntranceInfoModel
    {
        /// <summary>
        /// 备案状态，取值参考备案状态枚举，示例值：`1024`
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 是否正在注销备案
        /// </summary>
        public bool is_canceling { get; set; }

        /// <summary>
        /// 驳回原因，备案不通过时返回
        /// </summary>
        public List<AuditDataModel> audit_data { get; set; }
    }


    /// <summary>
    /// 驳回原因，备案不通过时返回
    /// </summary>
    public class AuditDataModel
    {
        /// <summary>
        /// 审核不通过的字段中文名
        /// </summary>
        public string key_name { get; set; }

        /// <summary>
        /// 字段不通过的原因
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// 修改建议
        /// </summary>
        public string suggest { get; set; }
    }
}
