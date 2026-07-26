/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocFormJson.cs
    文件功能描述：企业微信收集表强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐收集表创建、编辑、统计及答案模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    /// <summary>创建收集表请求。</summary>
    public class WeDocFormCreateRequest
    {
        /// <summary>微盘空间 SpaceID。</summary>
        public string spaceid { get; set; }

        /// <summary>父目录 FileID。</summary>
        public string fatherid { get; set; }

        /// <summary>收集表定义。</summary>
        public WeDocFormInfo form_info { get; set; }
    }

    /// <summary>创建收集表结果。</summary>
    public class WeDocFormCreateResult : WorkJsonResult
    {
        /// <summary>新建收集表 FormID。</summary>
        public string formid { get; set; }
    }

    /// <summary>编辑收集表请求。</summary>
    public class WeDocFormModifyRequest
    {
        /// <summary>编辑操作类型。</summary>
        public int oper { get; set; }

        /// <summary>收集表 FormID。</summary>
        public string formid { get; set; }

        /// <summary>需要更新的收集表定义。</summary>
        public WeDocFormInfo form_info { get; set; }
    }

    /// <summary>收集表 FormID 请求。</summary>
    public class WeDocFormIdRequest
    {
        /// <summary>收集表 FormID。</summary>
        public string formid { get; set; }
    }

    /// <summary>收集表信息结果。</summary>
    public class WeDocFormInfoResult : WorkJsonResult
    {
        /// <summary>收集表定义。</summary>
        public WeDocFormInfo form_info { get; set; }
    }

    /// <summary>收集表定义。</summary>
    public class WeDocFormInfo
    {
        /// <summary>收集表 FormID。</summary>
        public string formid { get; set; }

        /// <summary>收集表标题。</summary>
        public string form_title { get; set; }

        /// <summary>收集表说明。</summary>
        public string form_desc { get; set; }

        /// <summary>收集表头图。</summary>
        public string form_header { get; set; }

        /// <summary>问题列表。</summary>
        public WeDocFormQuestion form_question { get; set; }

        /// <summary>填写和周期设置。</summary>
        public WeDocFormSetting form_setting { get; set; }

        /// <summary>已生成的收集周期 ID 列表。</summary>
        public IList<string> repeated_id { get; set; }
    }

    /// <summary>收集表问题列表。</summary>
    public class WeDocFormQuestion
    {
        /// <summary>问题项。</summary>
        public IList<WeDocFormQuestionItem> items { get; set; }
    }

    /// <summary>收集表问题项。</summary>
    public class WeDocFormQuestionItem
    {
        /// <summary>问题 ID。</summary>
        public long? question_id { get; set; }

        /// <summary>问题标题。</summary>
        public string title { get; set; }

        /// <summary>显示位置。</summary>
        public int? pos { get; set; }

        /// <summary>问题状态。</summary>
        public int? status { get; set; }

        /// <summary>回复类型。</summary>
        public int? reply_type { get; set; }

        /// <summary>是否必填。</summary>
        public bool? must_reply { get; set; }

        /// <summary>问题说明。</summary>
        public string note { get; set; }

        /// <summary>选择题选项。</summary>
        public IList<WeDocFormOptionItem> option_item { get; set; }

        /// <summary>输入框占位提示。</summary>
        public string placeholder { get; set; }

        /// <summary>题型扩展设置。</summary>
        public WeDocFormQuestionExtendSetting question_extend_setting { get; set; }
    }

    /// <summary>收集表问题扩展设置。</summary>
    public class WeDocFormQuestionExtendSetting
    {
        /// <summary>图片题是否只允许直接拍照上传。</summary>
        public bool? camera_only { get; set; }
    }

    /// <summary>收集表选择题选项。</summary>
    public class WeDocFormOptionItem
    {
        /// <summary>选项编号。</summary>
        public int? key { get; set; }

        /// <summary>选项文本。</summary>
        public string value { get; set; }

        /// <summary>选项状态。</summary>
        public int? status { get; set; }
    }

    /// <summary>收集表填写设置。</summary>
    public class WeDocFormSetting
    {
        /// <summary>填写权限类型。</summary>
        public int? fill_out_auth { get; set; }

        /// <summary>允许填写的成员和部门范围。</summary>
        public WeDocFormFillInRange fill_in_range { get; set; }

        /// <summary>设置管理员范围。</summary>
        public WeDocFormManagerRange setting_manager_range { get; set; }

        /// <summary>定时重复收集设置。</summary>
        public WeDocFormTimedRepeatInfo timed_repeat_info { get; set; }

        /// <summary>是否允许同一成员多次填写。</summary>
        public bool? allow_multi_fill { get; set; }

        /// <summary>每个成员最大填写次数。</summary>
        public int? max_fill_cnt { get; set; }

        /// <summary>停止收集 Unix 时间戳（秒）。</summary>
        public long? timed_finish { get; set; }

        /// <summary>是否允许匿名填写。</summary>
        public bool? can_anonymous { get; set; }

        /// <summary>提交后是否通知管理员。</summary>
        public bool? can_notify_submit { get; set; }
    }

    /// <summary>收集表填写范围。</summary>
    public class WeDocFormFillInRange
    {
        /// <summary>成员 UserID 列表。</summary>
        public IList<string> userids { get; set; }

        /// <summary>部门 ID 列表，使用 64 位整数保存。</summary>
        public IList<long> departmentids { get; set; }
    }

    /// <summary>收集表设置管理员范围。</summary>
    public class WeDocFormManagerRange
    {
        /// <summary>管理员 UserID 列表。</summary>
        public IList<string> userids { get; set; }
    }

    /// <summary>收集表定时重复设置。</summary>
    public class WeDocFormTimedRepeatInfo
    {
        /// <summary>是否启用重复收集。</summary>
        public bool? enable { get; set; }

        /// <summary>按周重复标记。</summary>
        public int? week_flag { get; set; }

        /// <summary>提醒 Unix 时间戳（秒）。</summary>
        public long? remind_time { get; set; }

        /// <summary>重复类型。</summary>
        public int? repeat_type { get; set; }

        /// <summary>是否跳过节假日。</summary>
        public bool? skip_holiday { get; set; }

        /// <summary>每月收集日。</summary>
        public int? day_of_month { get; set; }

        /// <summary>当前周期结束方式。</summary>
        public int? fork_finish_type { get; set; }

        /// <summary>规则创建 Unix 时间戳（秒）。</summary>
        public long? rule_ctime { get; set; }

        /// <summary>规则修改 Unix 时间戳（秒）。</summary>
        public long? rule_mtime { get; set; }
    }

    /// <summary>单个收集周期的统计查询。</summary>
    public class WeDocFormStatisticRequest
    {
        /// <summary>收集周期 ID。</summary>
        public string repeated_id { get; set; }

        /// <summary>请求类型。</summary>
        public int? req_type { get; set; }

        /// <summary>统计开始 Unix 时间戳（秒）。</summary>
        public long? start_time { get; set; }

        /// <summary>统计结束 Unix 时间戳（秒）。</summary>
        public long? end_time { get; set; }

        /// <summary>分页大小。</summary>
        public long? limit { get; set; }

        /// <summary>分页游标。</summary>
        public long? cursor { get; set; }
    }

    /// <summary>收集表统计结果。</summary>
    public class WeDocFormStatisticResult : WorkJsonResult
    {
        /// <summary>各收集周期统计列表。</summary>
        public IList<WeDocFormStatistic> statistic_list { get; set; }
    }

    /// <summary>单个收集周期统计。</summary>
    public class WeDocFormStatistic
    {
        /// <summary>填写次数。</summary>
        public long? fill_cnt { get; set; }

        /// <summary>收集周期 ID。</summary>
        public string repeated_id { get; set; }

        /// <summary>收集周期名称。</summary>
        public string repeated_name { get; set; }

        /// <summary>已填写成员数。</summary>
        public long? fill_user_cnt { get; set; }

        /// <summary>未填写成员数。</summary>
        public long? unfill_user_cnt { get; set; }

        /// <summary>已提交成员列表。</summary>
        public IList<WeDocFormSubmitUser> submit_users { get; set; }

        /// <summary>未填写成员列表。</summary>
        public IList<WeDocFormUnfillUser> unfill_users { get; set; }

        /// <summary>是否还有下一页。</summary>
        public bool? has_more { get; set; }

        /// <summary>下一页游标。</summary>
        public long? cursor { get; set; }
    }

    /// <summary>收集表已提交成员。</summary>
    public class WeDocFormSubmitUser
    {
        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>临时外部联系人 UserID。</summary>
        public string tmp_external_userid { get; set; }

        /// <summary>提交 Unix 时间戳（秒）。</summary>
        public long? submit_time { get; set; }

        /// <summary>答案 ID。</summary>
        public long? answer_id { get; set; }

        /// <summary>填写人名称。</summary>
        public string user_name { get; set; }
    }

    /// <summary>收集表未填写成员。</summary>
    public class WeDocFormUnfillUser
    {
        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }

        /// <summary>成员名称。</summary>
        public string user_name { get; set; }
    }

    /// <summary>收集表答案请求。</summary>
    public class WeDocFormAnswerRequest
    {
        /// <summary>收集周期 ID。</summary>
        public string repeated_id { get; set; }

        /// <summary>答案 ID 列表。</summary>
        public IList<long> answer_ids { get; set; }
    }

    /// <summary>收集表答案结果。</summary>
    public class WeDocFormAnswerResult : WorkJsonResult
    {
        /// <summary>答案集合。</summary>
        public WeDocFormAnswer answer { get; set; }
    }

    /// <summary>收集表答案集合。</summary>
    public class WeDocFormAnswer
    {
        /// <summary>答案列表。</summary>
        public IList<WeDocFormAnswerItem> answer_list { get; set; }
    }

    /// <summary>一份收集表答案。</summary>
    public class WeDocFormAnswerItem
    {
        /// <summary>答案 ID。</summary>
        public long? answer_id { get; set; }

        /// <summary>填写人名称。</summary>
        public string user_name { get; set; }

        /// <summary>创建 Unix 时间戳（秒）。</summary>
        public long? ctime { get; set; }

        /// <summary>修改 Unix 时间戳（秒）。</summary>
        public long? mtime { get; set; }

        /// <summary>逐题回复。</summary>
        public WeDocFormReply reply { get; set; }

        /// <summary>答案状态。</summary>
        public int? answer_status { get; set; }

        /// <summary>临时外部联系人 UserID。</summary>
        public string tmp_external_userid { get; set; }

        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }
    }

    /// <summary>收集表逐题回复。</summary>
    public class WeDocFormReply
    {
        /// <summary>回复项列表。</summary>
        public IList<WeDocFormReplyItem> items { get; set; }
    }

    /// <summary>收集表单题回复。</summary>
    public class WeDocFormReplyItem
    {
        /// <summary>问题 ID。</summary>
        public long? question_id { get; set; }

        /// <summary>文本回复。</summary>
        public string text_reply { get; set; }

        /// <summary>选项编号回复。</summary>
        public IList<int> option_reply { get; set; }

        /// <summary>带补充文字的选项回复。</summary>
        public IList<WeDocFormOptionExtendReply> option_extend_reply { get; set; }

        /// <summary>文件回复。</summary>
        public IList<WeDocFormFileReply> file_extend_reply { get; set; }

        /// <summary>部门选择回复。</summary>
        public WeDocFormDepartmentReply department_reply { get; set; }

        /// <summary>成员选择回复。</summary>
        public WeDocFormMemberReply member_reply { get; set; }

        /// <summary>时长回复。</summary>
        public WeDocFormDurationReply duration_reply { get; set; }
    }

    /// <summary>收集表选项扩展回复。</summary>
    public class WeDocFormOptionExtendReply
    {
        /// <summary>选项编号。</summary>
        public int? option_reply { get; set; }

        /// <summary>选项补充文字。</summary>
        public string extend_text { get; set; }
    }

    /// <summary>收集表文件回复。</summary>
    public class WeDocFormFileReply
    {
        /// <summary>文件名称。</summary>
        public string name { get; set; }

        /// <summary>文件 FileID。</summary>
        public string fileid { get; set; }
    }

    /// <summary>收集表部门回复。</summary>
    public class WeDocFormDepartmentReply
    {
        /// <summary>部门列表。</summary>
        public IList<WeDocFormDepartmentItem> list { get; set; }
    }

    /// <summary>收集表部门回复项。</summary>
    public class WeDocFormDepartmentItem
    {
        /// <summary>部门 ID，使用 64 位整数保存。</summary>
        public long department_id { get; set; }
    }

    /// <summary>收集表成员回复。</summary>
    public class WeDocFormMemberReply
    {
        /// <summary>成员列表。</summary>
        public IList<WeDocFormMemberItem> list { get; set; }
    }

    /// <summary>收集表成员回复项。</summary>
    public class WeDocFormMemberItem
    {
        /// <summary>企业成员 UserID。</summary>
        public string userid { get; set; }
    }

    /// <summary>收集表时长回复。</summary>
    public class WeDocFormDurationReply
    {
        /// <summary>开始 Unix 时间戳（秒）。</summary>
        public long? begin_time { get; set; }

        /// <summary>结束 Unix 时间戳（秒）。</summary>
        public long? end_time { get; set; }

        /// <summary>时间刻度。</summary>
        public int? time_scale { get; set; }

        /// <summary>自然日范围。</summary>
        public int? day_range { get; set; }

        /// <summary>折算天数。</summary>
        public double? days { get; set; }

        /// <summary>折算小时数。</summary>
        public double? hours { get; set; }
    }
}
