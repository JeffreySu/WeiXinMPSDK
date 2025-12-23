
/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：GetApprovalDetailResult.cs
    文件功能描述：获取审批申请详情 返回结果
    
    
    创建标识：Senparc - 20251202
    
    修改标识：Senparc - 20251218
    修改描述：添加对所有控件类型的支持，包括明细控件（Table）的 children 属性
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class GetApprovalDetailResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info info { get; set; }
    }

    public class GetApprovalDetailResult_Info
    {
        /// <summary>
        /// 
        /// </summary>
        public string sp_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sp_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sp_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long apply_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_Applyer applyer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_SpRecord> sp_record { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_Notifyer> notifyer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_ApplyData apply_data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_Comments> comments { get; set; }
    }

    public class GetApprovalDetailResult_TextLang
    {
        /// <summary>
        /// 摘要行显示文字，用于记录列表和消息通知的显示，不要超过20个字符
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 摘要行显示语言，中文：zh_CN（注意不是zh-CN），英文：en。
        /// </summary>
        public string lang { get; set; }
    }

    public class GetApprovalDetailResult_Info_Applyer
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string partyid { get; set; }
    }

    public class GetApprovalDetailResult_Info_SpRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int sp_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int approverattr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_SpRecord_Details> details { get; set; }
    }

    public class GetApprovalDetailResult_Info_SpRecord_Details
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_SpRecord_Details_Approver approver { get; set; }

        public string speech { get; set; }

        public int sp_status { get; set; }

        public long sptime { get; set; }

        public List<string> media_id { get; set; }

    }

    public class GetApprovalDetailResult_Info_SpRecord_Details_Approver
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

    }


    public class GetApprovalDetailResult_Info_Notifyer
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

    }

    public class GetApprovalDetailResult_Info_ApplyData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_Info_ApplyData_Contents> contents { get; set; }

    }

    public class GetApprovalDetailResult_Info_ApplyData_Contents
    {
        /// <summary>
        /// 
        /// </summary>
        public string control { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetApprovalDetailResult_TextLang> title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_ApplyData_Contents_Value value { get; set; }
    }

    /// <summary>
    /// 控件值，不同控件类型对应不同的参数
    /// 参考文档：https://developer.work.weixin.qq.com/document/path/91983
    /// </summary>
    public class GetApprovalDetailResult_Info_ApplyData_Contents_Value
    {
        /// <summary>
        /// 文本/多行文本控件（control为Text或Textarea）的值
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 说明文字控件（control为Tips）的值
        /// </summary>
        public List<string> tips { get; set; }

        /// <summary>
        /// 数字控件（control为Number）的值
        /// </summary>
        public string new_number { get; set; }

        /// <summary>
        /// 金额控件（control为Money）的值
        /// </summary>
        public string new_money { get; set; }

        /// <summary>
        /// 日期/日期+时间控件（control为Date）的值
        /// </summary>
        public GetApprovalDetailResult_Date date { get; set; }

        /// <summary>
        /// 单选/多选控件（control为Selector）的值
        /// </summary>
        public GetApprovalDetailResult_Selector selector { get; set; }

        /// <summary>
        /// 成员控件（control为Contact，mode为user）的值
        /// </summary>
        public List<GetApprovalDetailResult_Member> members { get; set; }

        /// <summary>
        /// 部门控件（control为Contact，mode为department）的值
        /// </summary>
        public List<GetApprovalDetailResult_Department> departments { get; set; }

        /// <summary>
        /// 附件控件（control为File）的值
        /// </summary>
        public List<GetApprovalDetailResult_File> files { get; set; }

        /// <summary>
        /// 明细控件（control为Table）的值，包含多行子控件数据
        /// </summary>
        public List<GetApprovalDetailResult_TableChildren> children { get; set; }

        /// <summary>
        /// 位置控件（control为Location）的值
        /// </summary>
        public GetApprovalDetailResult_Location location { get; set; }

        /// <summary>
        /// 关联审批单控件（control为RelatedApproval）的值
        /// </summary>
        public List<GetApprovalDetailResult_RelatedApproval> related_approval { get; set; }

        /// <summary>
        /// 时长控件（control为DateRange）的值
        /// </summary>
        public GetApprovalDetailResult_DateRange date_range { get; set; }

        /// <summary>
        /// 公式控件（control为Formula）的值
        /// </summary>
        public string formula { get; set; }

        /// <summary>
        /// 假勤控件-请假（control为Vacation）的值
        /// </summary>
        public GetApprovalDetailResult_Vacation vacation { get; set; }

        /// <summary>
        /// 假勤控件-出差/外出/加班（control为Attendance）的值
        /// </summary>
        public GetApprovalDetailResult_Attendance attendance { get; set; }
    }

    #region 各控件类型的值类

    /// <summary>
    /// 日期控件的值
    /// </summary>
    public class GetApprovalDetailResult_Date
    {
        /// <summary>
        /// 时间展示类型：day-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long s_timestamp { get; set; }
    }

    /// <summary>
    /// 单选/多选控件的值
    /// </summary>
    public class GetApprovalDetailResult_Selector
    {
        /// <summary>
        /// 选择方式：single-单选；multi-多选
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 选中的选项
        /// </summary>
        public List<GetApprovalDetailResult_SelectorOption> options { get; set; }
    }

    /// <summary>
    /// 单选/多选控件的选项
    /// </summary>
    public class GetApprovalDetailResult_SelectorOption
    {
        /// <summary>
        /// 选项key
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 选项的值
        /// </summary>
        public List<GetApprovalDetailResult_TextLang> value { get; set; }
    }

    /// <summary>
    /// 成员控件的成员值
    /// </summary>
    public class GetApprovalDetailResult_Member
    {
        /// <summary>
        /// 成员userid
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 部门控件的部门值
    /// </summary>
    public class GetApprovalDetailResult_Department
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public string openapi_id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 附件控件的附件值
    /// </summary>
    public class GetApprovalDetailResult_File
    {
        /// <summary>
        /// 文件id
        /// </summary>
        public string file_id { get; set; }
    }

    /// <summary>
    /// 明细控件的子控件行数据（一行）
    /// </summary>
    public class GetApprovalDetailResult_TableChildren
    {
        /// <summary>
        /// 明细行中每个子控件的值
        /// </summary>
        public List<GetApprovalDetailResult_Info_ApplyData_Contents> list { get; set; }
    }

    /// <summary>
    /// 位置控件的值
    /// </summary>
    public class GetApprovalDetailResult_Location
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 地点标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 地点详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 选择的打卡时间（秒）
        /// </summary>
        public long time { get; set; }
    }

    /// <summary>
    /// 关联审批单控件的值
    /// </summary>
    public class GetApprovalDetailResult_RelatedApproval
    {
        /// <summary>
        /// 关联审批单号
        /// </summary>
        public string sp_no { get; set; }
    }

    /// <summary>
    /// 时长控件的值
    /// </summary>
    public class GetApprovalDetailResult_DateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public GetApprovalDetailResult_DateRangeData new_begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public GetApprovalDetailResult_DateRangeData new_end { get; set; }

        /// <summary>
        /// 时长秒数
        /// </summary>
        public long new_duration { get; set; }
    }

    /// <summary>
    /// 时长控件的时间范围数据
    /// </summary>
    public class GetApprovalDetailResult_DateRangeData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 类型：0-上午；1-下午；仅当type为halfday时有效
        /// </summary>
        public int time_type { get; set; }
    }

    /// <summary>
    /// 假勤控件-请假的值
    /// </summary>
    public class GetApprovalDetailResult_Vacation
    {
        /// <summary>
        /// 请假类型
        /// </summary>
        public GetApprovalDetailResult_VacationSelector selector { get; set; }

        /// <summary>
        /// 请假的日期范围
        /// </summary>
        public GetApprovalDetailResult_VacationAttendance attendance { get; set; }
    }

    /// <summary>
    /// 请假类型选择器
    /// </summary>
    public class GetApprovalDetailResult_VacationSelector
    {
        /// <summary>
        /// 选择方式
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 选择的请假类型
        /// </summary>
        public List<GetApprovalDetailResult_SelectorOption> options { get; set; }
    }

    /// <summary>
    /// 请假日期范围信息
    /// </summary>
    public class GetApprovalDetailResult_VacationAttendance
    {
        /// <summary>
        /// 时间范围
        /// </summary>
        public GetApprovalDetailResult_AttendanceDateRange date_range { get; set; }
    }

    /// <summary>
    /// 假勤控件-出差/外出/加班的值
    /// </summary>
    public class GetApprovalDetailResult_Attendance
    {
        /// <summary>
        /// 时长控件
        /// </summary>
        public GetApprovalDetailResult_AttendanceDateRange date_range { get; set; }
    }

    /// <summary>
    /// 假勤控件的日期范围
    /// </summary>
    public class GetApprovalDetailResult_AttendanceDateRange
    {
        /// <summary>
        /// 时间展示类型：halfday-日期；hour-日期+时间
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public GetApprovalDetailResult_AttendanceDateRangeData new_begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public GetApprovalDetailResult_AttendanceDateRangeData new_end { get; set; }

        /// <summary>
        /// 时长秒数
        /// </summary>
        public long new_duration { get; set; }
    }

    /// <summary>
    /// 假勤日期范围数据
    /// </summary>
    public class GetApprovalDetailResult_AttendanceDateRangeData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 类型：0-上午；1-下午；仅当type为halfday时有效
        /// </summary>
        public int time_type { get; set; }
    }

    #endregion

    public class GetApprovalDetailResult_Info_Comments
    {
        /// <summary>
        /// 
        /// </summary>
        public GetApprovalDetailResult_Info_Comments_CommentUserInfo commentUserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long commenttime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string commentcontent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string commentid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> media_id { get; set; }
    }

    public class GetApprovalDetailResult_Info_Comments_CommentUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }
    }
}
