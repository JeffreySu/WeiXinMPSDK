using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取对接规则详情 响应参数
    /// </summary>
    public class GetRuleInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 上下游关系规则的详情
        /// </summary>
        public GetRuleInfoResult_RuleInfo rule_info { get; set; }
    }

    public class GetRuleInfoResult_RuleInfo
    {
        /// <summary>
        /// 上游企业的对接人规则（下游企业可以看到并联系的成员或部门）
        /// </summary>
        public GetRuleInfoResult_RuleInfo_OwnerCorpRange owner_corp_range { get; set; }

        /// <summary>
        /// 下游企业规则范围
        /// </summary>
        public GetRuleInfoResult_RuleInfo_MemberCorpRange member_corp_range { get; set; }

    }

    public class GetRuleInfoResult_RuleInfo_OwnerCorpRange
    {
        /// <summary>
        /// 	部门id
        /// </summary>
        public List<string> departmentids { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public List<string> userids { get; set; }

    }

    /// <summary>
    /// 下游企业规则范围
    /// </summary>
    public class GetRuleInfoResult_RuleInfo_MemberCorpRange
    {
        /// <summary>
        /// 分组id
        /// </summary>
        public List<string> groupids { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        public List<string> corpids { get; set; }
    }
}
