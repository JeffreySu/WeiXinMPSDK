using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取上下游列表 响应参数
    /// </summary>
    public class GetChainListResult : WorkJsonResult
    {
        /// <summary>
        /// 企业上下游列表
        /// </summary>
        public List<GetChainListResult_Chain> chains { get; set; }
    }

    public class GetChainListResult_Chain
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
