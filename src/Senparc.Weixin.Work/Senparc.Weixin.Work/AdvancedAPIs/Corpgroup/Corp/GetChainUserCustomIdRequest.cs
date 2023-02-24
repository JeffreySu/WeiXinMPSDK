using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 查询成员自定义id 请求参数
    /// </summary>
    public class GetChainUserCustomIdRequest
    {
        /// <summary>
        /// 上下游id
        /// 必填
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 已加入企业id
        /// 必填
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 企业内的成员
        /// 必填
        /// </summary>
        public string userid { get; set; }

    }
}
