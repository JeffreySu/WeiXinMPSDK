using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 获取下级企业加入的上下游 请求参数
    /// </summary>
    public class GetCorpSharedChainListRequest
    {
        /// <summary>
        /// 已加入企业id
        /// 必填
        /// </summary>
        public string corpid { get; set; }

    }
}
