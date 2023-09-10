using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取对接规则id列表 响应参数
    /// </summary>
    public class ListIdsResult : WorkJsonResult
    {
        /// <summary>
        /// 上下游关系规则的id
        /// </summary>
        public List<int> rule_ids { get; set; }
    }
}
