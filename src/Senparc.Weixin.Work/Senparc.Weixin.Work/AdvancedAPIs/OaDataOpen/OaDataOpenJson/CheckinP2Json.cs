/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CheckinP2Json.cs
    文件功能描述：企业微信打卡增量接口请求与响应模型

    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信打卡统计、排班及规则管理强类型模型
----------------------------------------------------------------*/

using Newtonsoft.Json;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson
{
    /// <summary>获取企业全部打卡规则结果。</summary>
    public class GetCorpCheckinOptionJsonResult : WorkJsonResult
    {
        /// <summary>企业打卡规则列表。</summary>
        public Group[] group { get; set; }
    }

    /// <summary>按时间范围和成员查询打卡统计的请求。</summary>
    public class CheckinStatisticsRequest
    {
        /// <summary>查询开始时间（Unix 时间戳）。</summary>
        public long starttime { get; set; }

        /// <summary>查询结束时间（Unix 时间戳）。</summary>
        public long endtime { get; set; }

        /// <summary>成员 UserId 列表。</summary>
        public string[] useridlist { get; set; }
    }

    /// <summary>获取打卡月报结果。</summary>
    public class GetCheckinMonthDataJsonResult : WorkJsonResult
    {
        /// <summary>成员月报列表。</summary>
        public CheckinMonthData[] datas { get; set; }
    }

    /// <summary>单个成员的打卡月报。</summary>
    public class CheckinMonthData
    {
        /// <summary>成员及规则基础信息。</summary>
        public CheckinMonthBaseInfo base_info { get; set; }

        /// <summary>月度汇总信息。</summary>
        public CheckinMonthSummaryInfo summary_info { get; set; }

        /// <summary>异常统计。</summary>
        public CheckinMonthExceptionInfo[] exception_infos { get; set; }

        /// <summary>审批统计。</summary>
        public CheckinMonthApprovalItem[] sp_items { get; set; }

        /// <summary>加班统计。</summary>
        public CheckinMonthOverworkInfo overwork_info { get; set; }
    }

    /// <summary>月报基础信息。</summary>
    public class CheckinMonthBaseInfo
    {
        /// <summary>记录类型。</summary>
        public int record_type { get; set; }

        /// <summary>成员姓名。</summary>
        public string name { get; set; }

        /// <summary>成员姓名补充信息。</summary>
        public string name_ex { get; set; }

        /// <summary>部门名称。</summary>
        public string departs_name { get; set; }

        /// <summary>打卡规则信息。</summary>
        public CheckinMonthRuleInfo rule_info { get; set; }

        /// <summary>成员账号。</summary>
        public string acctid { get; set; }
    }

    /// <summary>月报中的打卡规则信息。</summary>
    public class CheckinMonthRuleInfo
    {
        /// <summary>打卡规则 ID。</summary>
        public int groupid { get; set; }

        /// <summary>打卡规则名称。</summary>
        public string groupname { get; set; }
    }

    /// <summary>月报汇总信息。</summary>
    public class CheckinMonthSummaryInfo
    {
        /// <summary>应出勤天数。</summary>
        public int work_days { get; set; }

        /// <summary>正常天数。</summary>
        public int regular_days { get; set; }

        /// <summary>休息天数。</summary>
        public int rest_days { get; set; }

        /// <summary>异常天数。</summary>
        public int except_days { get; set; }

        /// <summary>实际工作时长（秒）。</summary>
        public int regular_work_sec { get; set; }

        /// <summary>标准工作时长（秒）。</summary>
        public int standard_work_sec { get; set; }
    }

    /// <summary>月报异常统计项。</summary>
    public class CheckinMonthExceptionInfo
    {
        /// <summary>异常类型。</summary>
        public int exception { get; set; }

        /// <summary>异常次数。</summary>
        public int count { get; set; }

        /// <summary>异常时长（秒）。</summary>
        public int duration { get; set; }
    }

    /// <summary>月报审批统计项。</summary>
    public class CheckinMonthApprovalItem
    {
        /// <summary>审批类型。</summary>
        public int type { get; set; }

        /// <summary>假期 ID。</summary>
        public int vacation_id { get; set; }

        /// <summary>审批次数。</summary>
        public int count { get; set; }

        /// <summary>审批时长。</summary>
        public int duration { get; set; }

        /// <summary>时间单位类型。</summary>
        public int time_type { get; set; }

        /// <summary>审批类型名称。</summary>
        public string name { get; set; }
    }

    /// <summary>月报加班统计。</summary>
    public class CheckinMonthOverworkInfo
    {
        /// <summary>工作日加班时长（秒）。</summary>
        public int workday_over_sec { get; set; }

        /// <summary>节假日加班时长（秒）。</summary>
        public int holidays_over_sec { get; set; }

        /// <summary>休息日加班时长（秒）。</summary>
        public int restdays_over_sec { get; set; }

        /// <summary>工作日转调休时长。</summary>
        public int workdays_over_as_vacation { get; set; }

        /// <summary>工作日转加班费时长。</summary>
        public int workdays_over_as_money { get; set; }

        /// <summary>休息日转调休时长。</summary>
        public int restdays_over_as_vacation { get; set; }

        /// <summary>休息日转加班费时长。</summary>
        public int restdays_over_as_money { get; set; }

        /// <summary>节假日转调休时长。</summary>
        public int holidays_over_as_vacation { get; set; }

        /// <summary>节假日转加班费时长。</summary>
        public int holidays_over_as_money { get; set; }
    }

    /// <summary>获取成员排班结果。</summary>
    public class GetCheckinScheduleListJsonResult : WorkJsonResult
    {
        /// <summary>成员排班列表。</summary>
        public CheckinUserSchedule[] schedule_list { get; set; }
    }

    /// <summary>成员月度排班。</summary>
    public class CheckinUserSchedule
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>排班月份，格式为 yyyyMM。</summary>
        public int yearmonth { get; set; }

        /// <summary>打卡规则 ID。</summary>
        public int groupid { get; set; }

        /// <summary>打卡规则名称。</summary>
        public string groupname { get; set; }

        /// <summary>排班详情。</summary>
        public CheckinScheduleContainer schedule { get; set; }
    }

    /// <summary>排班详情容器。</summary>
    public class CheckinScheduleContainer
    {
        /// <summary>每日排班列表。</summary>
        public CheckinScheduleDay[] scheduleList { get; set; }
    }

    /// <summary>单日排班。</summary>
    public class CheckinScheduleDay
    {
        /// <summary>日期中的日。</summary>
        public int day { get; set; }

        /// <summary>班次信息。</summary>
        public CheckinScheduleInfo schedule_info { get; set; }
    }

    /// <summary>班次信息。</summary>
    public class CheckinScheduleInfo
    {
        /// <summary>班次 ID。</summary>
        public int schedule_id { get; set; }

        /// <summary>班次名称。</summary>
        public string schedule_name { get; set; }

        /// <summary>上下班时段。</summary>
        public CheckinScheduleTimeSection[] time_section { get; set; }
    }

    /// <summary>班次上下班时段。</summary>
    public class CheckinScheduleTimeSection
    {
        /// <summary>时段 ID。</summary>
        public int id { get; set; }

        /// <summary>上班时间，距零点秒数。</summary>
        public int work_sec { get; set; }

        /// <summary>下班时间，距零点秒数。</summary>
        public int off_work_sec { get; set; }

        /// <summary>上班提醒时间。</summary>
        public int remind_work_sec { get; set; }

        /// <summary>下班提醒时间。</summary>
        public int remind_off_work_sec { get; set; }
    }

    /// <summary>设置成员排班请求。</summary>
    public class SetCheckinScheduleListRequest
    {
        /// <summary>打卡规则 ID。</summary>
        public int groupid { get; set; }

        /// <summary>成员排班项。</summary>
        public SetCheckinScheduleItem[] items { get; set; }

        /// <summary>排班月份，格式为 yyyyMM。</summary>
        public int yearmonth { get; set; }
    }

    /// <summary>设置成员单日排班项。</summary>
    public class SetCheckinScheduleItem
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>日期中的日。</summary>
        public int day { get; set; }

        /// <summary>班次 ID。</summary>
        public int schedule_id { get; set; }
    }

    /// <summary>补卡请求。</summary>
    public class PunchCorrectionRequest
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>应打卡日期的 Unix 时间戳。</summary>
        public long schedule_date_time { get; set; }

        /// <summary>应打卡时间，距离当天零点的秒数。</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? schedule_checkin_time { get; set; }

        /// <summary>实际补卡时间的 Unix 时间戳。</summary>
        public long checkin_time { get; set; }

        /// <summary>补卡备注。</summary>
        public string remark { get; set; }
    }

    /// <summary>录入成员人脸请求。</summary>
    public class AddCheckinUserFaceRequest
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>Base64 编码的人脸图片。</summary>
        public string userface { get; set; }
    }

    /// <summary>获取硬件打卡数据请求。</summary>
    public class GetHardwareCheckinDataRequest : CheckinStatisticsRequest
    {
        /// <summary>筛选类型：1 按打卡时间，2 按上传时间。</summary>
        public int filter_type { get; set; }
    }

    /// <summary>获取硬件打卡数据结果。</summary>
    public class GetHardwareCheckinDataJsonResult : WorkJsonResult
    {
        /// <summary>硬件打卡记录。</summary>
        public HardwareCheckinData[] checkindata { get; set; }
    }

    /// <summary>单条硬件打卡记录。</summary>
    public class HardwareCheckinData
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>打卡时间（Unix 时间戳）。</summary>
        public long checkin_time { get; set; }

        /// <summary>设备序列号。</summary>
        public string device_sn { get; set; }

        /// <summary>设备名称。</summary>
        public string device_name { get; set; }
    }

    /// <summary>新增或更新打卡规则请求。</summary>
    public class CheckinOptionRequest
    {
        /// <summary>是否立即生效；不填写时由服务端采用默认值。</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? effective_now { get; set; }

        /// <summary>打卡规则。</summary>
        public Group group { get; set; }
    }

    /// <summary>清空打卡规则数组字段请求。</summary>
    public class ClearCheckinOptionArrayFieldRequest
    {
        /// <summary>打卡规则 ID。</summary>
        public int groupid { get; set; }

        /// <summary>需要清空的字段编号列表。</summary>
        public int[] clear_field { get; set; }

        /// <summary>是否立即生效。</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? effective_now { get; set; }
    }

    /// <summary>删除打卡规则请求。</summary>
    public class DeleteCheckinOptionRequest
    {
        /// <summary>打卡规则 ID。</summary>
        public int groupid { get; set; }

        /// <summary>是否立即生效。</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? effective_now { get; set; }
    }

    /// <summary>打卡范围。</summary>
    public class CheckinRange
    {
        /// <summary>部门 ID 列表。</summary>
        public long[] party_id { get; set; }

        /// <summary>成员 UserId 列表。</summary>
        public string[] userid { get; set; }

        /// <summary>标签 ID 列表。</summary>
        public long[] tagid { get; set; }
    }

    /// <summary>打卡汇报对象信息。</summary>
    public class CheckinReporterInfo
    {
        /// <summary>汇报对象。</summary>
        public CheckinReporter[] reporters { get; set; }

        /// <summary>更新时间（Unix 时间戳）。</summary>
        public long updatetime { get; set; }
    }

    /// <summary>打卡汇报对象。</summary>
    public class CheckinReporter
    {
        /// <summary>成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>标签 ID。</summary>
        public long tagid { get; set; }
    }

    /// <summary>大小周配置。</summary>
    public class CheckinBiweekly
    {
        /// <summary>是否启用大小周。</summary>
        public bool enable_weekday_recurrence { get; set; }

        /// <summary>单周工作日。</summary>
        public int[] odd_workdays { get; set; }

        /// <summary>双周工作日。</summary>
        public int[] even_workdays { get; set; }
    }

    /// <summary>迟到规则。</summary>
    public class CheckinLateRule
    {
        /// <summary>超过下班时间的秒数。</summary>
        public int offwork_after_time { get; set; }

        /// <summary>上班弹性时间。</summary>
        public int onwork_flex_time { get; set; }

        /// <summary>是否允许晚下班抵扣。</summary>
        public bool allow_offwork_after_time { get; set; }

        /// <summary>分段迟到规则。</summary>
        public CheckinLateTimeRule[] timerules { get; set; }
    }

    /// <summary>分段迟到规则。</summary>
    public class CheckinLateTimeRule
    {
        /// <summary>晚下班时长。</summary>
        public int offwork_after_time { get; set; }

        /// <summary>可抵扣的上班弹性时长。</summary>
        public int onwork_flex_time { get; set; }
    }

    /// <summary>休息时段。</summary>
    public class CheckinRestTime
    {
        /// <summary>休息开始时间。</summary>
        public int rest_begin_time { get; set; }

        /// <summary>休息结束时间。</summary>
        public int rest_end_time { get; set; }
    }

    /// <summary>打卡规则班次。</summary>
    public class CheckinRuleSchedule
    {
        /// <summary>班次 ID。</summary>
        public int schedule_id { get; set; }

        /// <summary>班次名称。</summary>
        public string schedule_name { get; set; }

        /// <summary>打卡时段。</summary>
        public Checkintime[] time_section { get; set; }

        /// <summary>提前打卡限制。</summary>
        public int limit_aheadtime { get; set; }

        /// <summary>延后打卡限制。</summary>
        public int limit_offtime { get; set; }

        /// <summary>下班是否无需打卡。</summary>
        public bool noneed_offwork { get; set; }

        /// <summary>是否允许弹性打卡。</summary>
        public bool allow_flex { get; set; }

        /// <summary>上班弹性时间。</summary>
        public int flex_on_duty_time { get; set; }

        /// <summary>下班弹性时间。</summary>
        public int flex_off_duty_time { get; set; }

        /// <summary>迟到规则。</summary>
        public CheckinLateRule late_rule { get; set; }

        /// <summary>允许最早到达时长。</summary>
        public int max_allow_arrive_early { get; set; }

        /// <summary>允许最晚到达时长。</summary>
        public int max_allow_arrive_late { get; set; }
    }

    /// <summary>旧版加班配置。</summary>
    public class CheckinOvertimeInfo
    {
        /// <summary>加班核算类型。</summary>
        public int type { get; set; }

        /// <summary>是否允许工作日加班。</summary>
        public bool allow_ot_workingday { get; set; }

        /// <summary>是否允许非工作日加班。</summary>
        public bool allow_ot_nonworkingday { get; set; }

        /// <summary>按打卡时间核算配置。</summary>
        public CheckinOvertimeCalculation otcheckinfo { get; set; }

        /// <summary>按加班申请核算配置。</summary>
        public CheckinOvertimeCalculation otapplyinfo { get; set; }
    }

    /// <summary>旧版加班时长核算配置。</summary>
    public class CheckinOvertimeCalculation
    {
        /// <summary>工作日加班开始偏移。</summary>
        public int ot_workingday_time_start { get; set; }

        /// <summary>工作日最短加班时长。</summary>
        public int ot_workingday_time_min { get; set; }

        /// <summary>工作日最长加班时长。</summary>
        public int ot_workingday_time_max { get; set; }

        /// <summary>非工作日最短加班时长。</summary>
        public int ot_nonworkingday_time_min { get; set; }

        /// <summary>非工作日最长加班时长。</summary>
        public int ot_nonworkingday_time_max { get; set; }

        /// <summary>非工作日跨天时间。</summary>
        public int ot_nonworkingday_spanday_time { get; set; }

        /// <summary>工作日休息扣除配置。</summary>
        public CheckinOvertimeRestInfo ot_workingday_restinfo { get; set; }

        /// <summary>非工作日休息扣除配置。</summary>
        public CheckinOvertimeRestInfo ot_nonworkingday_restinfo { get; set; }
    }

    /// <summary>加班休息扣除配置。</summary>
    public class CheckinOvertimeRestInfo
    {
        /// <summary>扣除类型。</summary>
        public int type { get; set; }

        /// <summary>单个指定休息时段。</summary>
        public CheckinOvertimeFixedTimeRule fix_time_rule { get; set; }

        /// <summary>多个指定休息时段。</summary>
        public CheckinOvertimeFixedTimeRule[] fix_time_rule_list { get; set; }

        /// <summary>按加班时长扣除规则。</summary>
        public CheckinOvertimeDeductionRule cal_ottime_rule { get; set; }
    }

    /// <summary>指定休息时段。</summary>
    public class CheckinOvertimeFixedTimeRule
    {
        /// <summary>开始时间。</summary>
        public int fix_time_begin_sec { get; set; }

        /// <summary>结束时间。</summary>
        public int fix_time_end_sec { get; set; }
    }

    /// <summary>按加班时长扣除规则。</summary>
    public class CheckinOvertimeDeductionRule
    {
        /// <summary>扣除条件。</summary>
        public CheckinOvertimeDeductionItem[] items { get; set; }
    }

    /// <summary>加班休息扣除条件。</summary>
    public class CheckinOvertimeDeductionItem
    {
        /// <summary>加班时长。</summary>
        public int ot_time { get; set; }

        /// <summary>扣除时长。</summary>
        public int rest_time { get; set; }
    }

    /// <summary>新版加班配置。</summary>
    public class CheckinOvertimeInfoV2
    {
        /// <summary>工作日加班配置。</summary>
        public CheckinOvertimeDayConfig workdayconf { get; set; }

        /// <summary>休息日加班配置。</summary>
        public CheckinOvertimeDayConfig restdayconf { get; set; }

        /// <summary>节假日加班配置。</summary>
        public CheckinOvertimeDayConfig holidayconf { get; set; }

        /// <summary>加班单位配置。</summary>
        public CheckinOvertimeUnitConfig time_unit_config { get; set; }
    }

    /// <summary>单类日期的加班配置。</summary>
    public class CheckinOvertimeDayConfig
    {
        /// <summary>是否允许加班。</summary>
        public bool allow_ot { get; set; }

        /// <summary>核算类型。</summary>
        public int type { get; set; }

        /// <summary>仅按审批核算配置。</summary>
        public CheckinOvertimeModeConfig apply { get; set; }

        /// <summary>仅按打卡核算配置。</summary>
        public CheckinOvertimeModeConfig checkin { get; set; }

        /// <summary>审批和打卡取交集配置。</summary>
        public CheckinOvertimeModeConfig applycheckin { get; set; }

        /// <summary>是否允许转换为调休或加班费。</summary>
        public bool ot_trans_enable { get; set; }

        /// <summary>转换类型。</summary>
        public int ot_trans_type { get; set; }

        /// <summary>调休配置。</summary>
        public CheckinOvertimeVacation vacation { get; set; }

        /// <summary>加班时段范围。</summary>
        public int ot_time_range { get; set; }
    }

    /// <summary>加班核算模式配置。</summary>
    public class CheckinOvertimeModeConfig
    {
        /// <summary>下班后开始计算加班的偏移秒数。</summary>
        public int ot_time_start { get; set; }

        /// <summary>最短加班时长。</summary>
        public int ot_time_min { get; set; }

        /// <summary>最长加班时长。</summary>
        public int ot_time_max { get; set; }

        /// <summary>休息扣除配置。</summary>
        public CheckinOvertimeRestInfo restinfo { get; set; }
    }

    /// <summary>加班转调休配置。</summary>
    public class CheckinOvertimeVacation
    {
        /// <summary>调休换算比例。</summary>
        public int trans_ratio { get; set; }

        /// <summary>是否自动关联假勤。</summary>
        public bool sync_vacation { get; set; }
    }

    /// <summary>加班单位和取整配置。</summary>
    public class CheckinOvertimeUnitConfig
    {
        /// <summary>加班单位。</summary>
        public int ot_time_unit { get; set; }

        /// <summary>每天对应的秒数。</summary>
        public int perday_duration_secs { get; set; }

        /// <summary>取整方式。</summary>
        public int rounding_method { get; set; }

        /// <summary>保留小数位。</summary>
        public int rounding_precision { get; set; }

        /// <summary>取整步长。</summary>
        public int step_size { get; set; }
    }

    /// <summary>补卡提醒配置。</summary>
    public class CheckinCorrectionReminder
    {
        /// <summary>是否开启提醒。</summary>
        public bool open_remind { get; set; }

        /// <summary>提醒日期。</summary>
        public int buka_remind_day { get; set; }

        /// <summary>提醒月份：0 当月，1 次月。</summary>
        public int buka_remind_month { get; set; }
    }
}
