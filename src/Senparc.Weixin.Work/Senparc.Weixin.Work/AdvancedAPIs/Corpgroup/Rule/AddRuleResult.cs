using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 新增对接规则 响应参数
    /// </summary>
    public class AddRuleResult : WorkJsonResult
    {
        /// <summary>
        /// 上下游关系规则的id
        /// </summary>
        public int rule_id { get; set; }
    }
}
