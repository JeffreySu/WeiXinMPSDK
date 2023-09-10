using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取下级/下游企业的access_token 请求参数
    /// </summary>
    public class GetTokenRequest
    {
        /// <summary>
        /// 上级/上游企业应用agentid
        /// 必填
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 填0则为企业互联/局校互联，填1则表示上下游企业
        /// 非必填
        /// </summary>
        public int business_type { get; set; }

        /// <summary>
        /// 下级/下游企业corpid，若指定该参数则表示拉取该下级/下游企业的应用共享信息
        /// 必填
        /// </summary>
        public string corpid { get; set; }
    }
}
