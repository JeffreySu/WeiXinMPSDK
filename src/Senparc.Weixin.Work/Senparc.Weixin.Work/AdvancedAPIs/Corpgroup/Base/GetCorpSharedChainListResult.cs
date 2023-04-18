using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取下级企业加入的上下游 响应参数
    /// </summary>
    public class GetCorpSharedChainListResult : WorkJsonResult
    {
        /// <summary>
        /// 成员自定义 id
        /// </summary>
        public string user_custom_id { get; set; }

        /// <summary>
        /// 上下游列表
        /// </summary>
        public List<GetCorpSharedChainListResult_Chains> chains { get; set; }
    }


    public class GetCorpSharedChainListResult_Chains
    {
        /// <summary>
        /// 上下游id
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 上下游名称
        /// </summary>
        public string chain_name { get; set; }
    }
}
