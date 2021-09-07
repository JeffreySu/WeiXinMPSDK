/*----------------------------------------------------------------
  
    文件名：GetCheckinDayDataJsonResult.cs
    文件功能描述：获取考勤日报数据接口返回结果
    
    
    创建标识：XbCore - 20210906

----------------------------------------------------------------*/
#region 引用

using System.Collections.Generic;

using Senparc.Weixin.Entities;

#endregion

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    /// <summary>
    /// 打卡日报数据
    /// </summary>
    public class GetCheckinDayDataJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 打卡日报数据
        /// </summary>
        public GetCheckinDayDataJsonResult_Result[] datas { get; set; }
    }

    public class GetCheckinDayDataJsonResult_Result
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public base_info base_info { get; set; }

        /// <summary>
        /// 汇总信息
        /// </summary>
        public summary_info summary_info { get; set; }

        /// <summary>
        /// 假勤相关信息
        /// </summary>
        public List<holiday_info> holiday_infos { get; set; }

        /// <summary>
        /// 校准状态信息
        /// </summary>
        public List<exception_info> exception_infos { get; set; }

        /// <summary>
        /// 加班信息
        /// </summary>
        public ot_info ot_info { get; set; }

        /// <summary>
        /// 假勤统计信息
        /// </summary>
        public List<sp_item> sp_items { get; set; }
    }

    /// <summary>
    /// 基础信息
    /// </summary>
    public class base_info
    {
        /// <summary>
        /// 日报日期
        /// </summary>
        public uint date { get; set; }

        /// <summary>
        /// 记录类型
        /// </summary>
        public RecordType record_type { get; set; }

        /// <summary>
        /// 打卡人员姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 打卡人员别名
        /// </summary>
        public string name_ex { get; set; }

        /// <summary>
        /// 打卡人员所在部门，会显示所有所在部门
        /// </summary>
        public string departs_name { get; set; }

        /// <summary>
        /// 打卡人员帐号，即userid
        /// </summary>
        public string acctid { get; set; }

        /// <summary>
        /// 打卡人员所属规则信息
        /// </summary>
        public rule_info rule_info { get; set; }

        /// <summary>
        /// 日报类型
        /// </summary>
        public DayType day_type { get; set; }
    }

    /// <summary>
    /// 打卡人员所属规则信息
    /// </summary>
    public class rule_info
    {
        /// <summary>
        /// 所属规则的id
        /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 打卡规则名
        /// </summary>
        public string groupname { get; set; }

        /// <summary>
        /// 当日所属班次id，仅按班次上下班才有值，显示在打卡日报-班次列
        /// </summary>
        public int scheduleid { get; set; }

        /// <summary>
        /// 当日所属班次名称，仅按班次上下班才有值，显示在打卡日报-班次列
        /// </summary>
        public string schedulename { get; set; }

        /// <summary>
        /// 当日打卡时间，仅固定上下班规则有值，显示在打卡日报-班次列
        /// </summary>
        public List<checkintime> checkintime { get; set; }
    }

    /// <summary>
    /// 当日打卡时间
    /// </summary>
    public class checkintime
    {
        /// <summary>
        /// 上班时间，为距离0点的时间差
        /// </summary>
        public uint work_sec { get; set; }

        /// <summary>
        /// 下班时间，为距离0点的时间差
        /// </summary>
        public uint off_work_sec { get; set; }
    }

    /// <summary>
    /// 	汇总信息
    /// </summary>
    public class summary_info
    {
        /// <summary>
        /// 当日打卡次数
        /// </summary>
        public uint checkin_count { get; set; }

        /// <summary>
        /// 当日实际工作时长，单位：秒
        /// </summary>
        public uint regular_work_sec { get; set; }

        /// <summary>
        /// 当日标准工作时长，单位：秒
        /// </summary>
        public uint standard_work_sec { get; set; }

        /// <summary>
        /// 当日最早打卡时间
        /// </summary>
        public uint earliest_time { get; set; }

        /// <summary>
        /// 当日最晚打卡时间
        /// </summary>
        public uint lastest_time { get; set; }
    }

    /// <summary>
    /// 假勤相关信息
    /// </summary>
    public class holiday_info
    {
        /// <summary>
        /// 假勤申请id，即当日关联的假勤审批单id
        /// </summary>
        public string sp_number { get; set; }

        /// <summary>
        /// 假勤信息摘要-标题信息
        /// </summary>
        public sp_title sp_title { get; set; }

        /// <summary>
        /// 假勤信息摘要-描述信息
        /// </summary>
        public sp_description sp_description { get; set; }
    }

    /// <summary>
    /// 假勤信息摘要
    /// </summary>
    public class sp_title
    {
        /// <summary>
        /// 多种语言描述，目前只有中文一种
        /// </summary>
        public List<data> data { get; set; }
    }

    /// <summary>
    /// 假勤信息摘要-描述信息
    /// </summary>
    public class sp_description
    {
        /// <summary>
        /// 多种语言描述，目前只有中文一种
        /// </summary>
        public List<data> data { get; set; }
    }

    public class data
    {
        /// <summary>
        /// 语言类型：”zh_CN”
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// 标题文本
        /// </summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 校准状态信息
    /// </summary>
    public class exception_info
    {
        /// <summary>
        /// 当日此异常的次数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 当日此异常的时长（迟到/早退/旷工才有值）
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 校准状态类型：1-迟到；2-早退；3-缺卡；4-旷工；5-地点异常；6-设备异常
        /// </summary>
        public ExceptionType exception { get; set; }
    }

    /// <summary>
    /// 加班信息
    /// </summary>
    public class ot_info
    {
        /// <summary>
        /// 状态：0-无加班；1-正常；2-缺时长
        /// </summary>
        public OtStatus ot_status { get; set; }

        /// <summary>
        /// 加班时长
        /// </summary>
        public uint ot_duration { get; set; }

        /// <summary>
        /// ot_status为2下，加班不足的时长
        /// </summary>
        public List<uint> exception_duration { get; set; }
    }

    /// <summary>
    /// 假勤统计信息
    /// </summary>
    public class sp_item
    {
        /// <summary>
        /// 类型：1-请假；2-补卡；3-出差；4-外出；100-外勤
        /// </summary>
        public LeaveType type { get; set; }

        /// <summary>
        /// 具体请假类型，当type为1请假时，具体的请假类型id，可通过审批相关接口获取假期详情
        /// </summary>
        public uint vacation_id { get; set; }

        /// <summary>
        /// 统计项名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 当日假勤次数
        /// </summary>
        public uint count { get; set; }

        /// <summary>
        /// 当日假勤时长秒数，时长单位为天直接除以86400即为天数，单位为小时直接除以3600即为小时数
        /// </summary>
        public long duration { get; set; }

        /// <summary>
        /// 时长单位：0-按天 1-按小时
        /// </summary>
        public TimeType time_type { get; set; }
    }

    /// <summary>
    /// 类型：1-请假；2-补卡；3-出差；4-外出；100-外勤
    /// </summary>
    public enum LeaveType
    {
        请假 = 1,
        补卡,
        出差,
        外出,
        外勤
    }

    /// <summary>
    /// 日报类型
    /// </summary>
    public enum DayType
    {
        工作日日报 = 0,
        休息日日报 = 1
    }

    /// <summary>
    /// 时长单位
    /// </summary>
    public enum TimeType
    {
        按天 = 0,
        按小时 = 1
    }

    /// <summary>
    /// 记录类型
    /// </summary>
    public enum RecordType
    {
        固定上下班 = 1,
        外出 = 2,
        按班次上下班 = 3,
        自由签到 = 4,
        加班 = 5,
        无规则 = 7
    }

    /// <summary>
    /// 加班状态
    /// </summary>
    public enum OtStatus
    {
        无加班 = 0,
        正常,
        缺时长
    }

    /// <summary>
    /// 校准状态类型：1-迟到；2-早退；3-缺卡；4-旷工；5-地点异常；6-设备异常
    /// </summary>
    public enum ExceptionType
    {
        迟到 = 1,
        早退,
        缺卡,
        旷工,
        地点异常,
        设备异常
    }
}
