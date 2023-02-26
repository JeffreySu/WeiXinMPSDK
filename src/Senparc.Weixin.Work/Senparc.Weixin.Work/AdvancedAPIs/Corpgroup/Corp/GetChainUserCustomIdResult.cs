using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 查询成员自定义id 响应参数
    /// </summary>
    public class GetChainUserCustomIdResult : WorkJsonResult
    {
        /// <summary>
        /// 成员自定义 id
        /// </summary>
        public string user_custom_id { get; set; }
    }
}
