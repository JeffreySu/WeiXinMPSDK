using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Rule
{
    /// <summary>
    /// 新增对接规则 请求参数
    /// </summary>
    public class AddRuleRequest
    {
        /// <summary>
        /// 上下游id
        /// 必填
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 上下游关系规则的详情
        /// </summary>
        public AddRuleRequest_RuleInfo rule_info { get; set; }

    }
    public class AddRuleRequest_RuleInfo
    {
        /// <summary>
        /// 上游企业的对接人规则（下游企业可以看到并联系的成员或部门）
        /// </summary>
        public AddRuleRequest_RuleInfo_OwnerCorpRange owner_corp_range { get; set; }

        /// <summary>
        /// 下游企业规则范围
        /// </summary>
        public AddRuleRequest_RuleInfo_MemberCorpRange member_corp_range { get; set; }

    }

    public class AddRuleRequest_RuleInfo_OwnerCorpRange
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
    public class AddRuleRequest_RuleInfo_MemberCorpRange
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
