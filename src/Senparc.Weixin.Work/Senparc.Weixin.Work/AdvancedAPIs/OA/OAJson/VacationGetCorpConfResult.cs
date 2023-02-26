

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class VacationGetCorpConfResult : WorkJsonResult
    {
        /// <summary>
        /// 假期列表
        /// </summary>
        public List<VacationGetCorpConfResult_Lists> lists { get; set; }
    }

    public class VacationGetCorpConfResult_Lists
    {
        /// <summary>
        /// 假期id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 假期名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 假期时间刻度：0-按天请假；1-按小时请假
        /// </summary>
        public int time_attr { get; set; }

        /// <summary>
        /// 时长计算类型：0-自然日；1-工作日
        /// </summary>
        public int duration_type { get; set; }

        /// <summary>
        /// 假期发放相关配置
        /// </summary>
        public VacationGetCorpConfResult_Lists_QuotaAttr quota_attr { get; set; }

        /// <summary>
        /// 单位换算值，即1天对应的秒数，可将此值除以3600得到一天对应的小时。
        /// </summary>
        public int perday_duration { get; set; }

        /// <summary>
        /// 是否关联加班调休，0-不关联，1-关联，关联后改假期类型变为调休假
        /// </summary>
        public int is_newovertime { get; set; }

        /// <summary>
        /// 入职时间大于n个月可用该假期，单位为月
        /// </summary>
        public int enter_comp_time_limit { get; set; }

        /// <summary>
        /// 假期过期规则
        /// </summary>
        public VacationGetCorpConfResult_Lists_ExpireRule expire_rule { get; set; }
    }

    public class VacationGetCorpConfResult_Lists_QuotaAttr
    {
        /// <summary>
        /// 假期发放类型：0-不限额；1-自动按年发放；2-手动发放；3-自动按月发放
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 自动发放时间戳，若假期发放为自动发放，此参数代表自动发放日期。注：返回时间戳的年份是无意义的，请只使用返回时间的月和日；若at_entry_date为true，该字段则无效，假期发放时间为员工入职时间
        /// </summary>
        public long autoreset_time { get; set; }

        /// <summary>
        /// 自动发放时长，单位为秒。注：只有自动按年发放和自动按月发放时有效，若选择了按照工龄和司龄发放，该字段无效，发放时长请使用区间中的quota
        /// </summary>
        public long autoreset_duration { get; set; }

        /// <summary>
        /// 额度计算类型，自动按年发放时有效，0-固定额度；1-按工龄计算；2-按司龄计算
        /// </summary>
        public int quota_rule_type { get; set; }

        /// <summary>
        /// 额度计算规则，自动按年发放时有效
        /// </summary>
        public VacationGetCorpConfResult_Lists_QuotaAttr_QuotaRules quota_rules { get; set; }

        /// <summary>
        /// 是否按照入职日期发放假期，只有在自动按年发放类型有效，选择后发放假期的时间会成为员工入职的日期
        /// </summary>
        public bool at_entry_date { get; set; }

        /// <summary>
        /// 自动按月发放的发放时间，只有自动按月发放类型有效
        /// </summary>
        public int auto_reset_month_day { get; set; }
    }

    public class VacationGetCorpConfResult_Lists_QuotaAttr_QuotaRules
    {
        /// <summary>
        /// 额度计算规则区间，只有在选择了按照工龄计算或者按照司龄计算时有效
        /// </summary>
        public List<VacationGetCorpConfResult_Lists_QuotaAttr_QuotaRules_List> list { get; set; }

        /// <summary>
        /// 是否根据实际入职时间计算假期，选择后会根据员工在今年的实际工作时间发放假期
        /// </summary>
        public bool based_on_actual_work_time { get; set; }
    }

    public class VacationGetCorpConfResult_Lists_QuotaAttr_QuotaRules_List
    {
        /// <summary>
        /// 区间发放时长，单位为s
        /// </summary>
        public int quota { get; set; }

        /// <summary>
        /// 区间开始点，单位为年
        /// </summary>
        public int begin { get; set; }

        /// <summary>
        /// 区间结束点，无穷大则为0，单位为年
        /// </summary>
        public int end { get; set; }
    }

    public class VacationGetCorpConfResult_Lists_ExpireRule
    {
        /// <summary>
        /// 过期规则类型，1-按固定时间过期，2-从发放日按年过期，3-从发放日按月过期，4-不过期
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 有效期，按年过期为年，按月过期为月，只有在以上两种情况时有效
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 失效日期，只有按固定时间过期时有效
        /// </summary>
        public VacationGetCorpConfResult_Lists_ExpireRule_Date date { get; set; }

        /// <summary>
        /// 是否允许延长有效期
        /// </summary>
        public bool extern_duration_enable { get; set; }

        /// <summary>
        /// 延长有效期的具体时间，只有在extern_duration_enable为true时有效
        /// </summary>
        public VacationGetCorpConfResult_Lists_ExpireRule_Date extern_duration { get; set; }
    }


    public class VacationGetCorpConfResult_Lists_ExpireRule_Date
    {
        /// <summary>
        /// 
        /// </summary>
        public int month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int day { get; set; }
    }
}
