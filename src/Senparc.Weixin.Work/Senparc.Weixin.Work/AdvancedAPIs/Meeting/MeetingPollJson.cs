/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingPollJson.cs
    文件功能描述：企业微信会议投票请求与结果强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议投票主题、投票过程和结果详情模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 会议投票操作者请求基础字段。
    /// </summary>
    public class MeetingPollOperatorRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置执行操作的企业成员 UserId。</summary>
        public string operator_userid { get; set; }

        /// <summary>获取或设置操作者终端设备类型。</summary>
        public int instance_id { get; set; }
    }

    /// <summary>
    /// 会议投票问题定义。
    /// </summary>
    public class MeetingPollQuestion
    {
        /// <summary>获取或设置问题类型。</summary>
        public int question_type { get; set; }

        /// <summary>获取或设置问题描述。</summary>
        public string question_desc { get; set; }

        /// <summary>获取或设置投票选项文本列表。</summary>
        public IList<string> poll_option { get; set; }
    }

    /// <summary>
    /// 创建会议投票主题请求。
    /// </summary>
    public class CreateMeetingPollThemeRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题。</summary>
        public string poll_topic { get; set; }

        /// <summary>获取或设置投票主题描述。</summary>
        public string poll_desc { get; set; }

        /// <summary>获取或设置是否匿名，0 表示否，1 表示是。</summary>
        public int? is_anony { get; set; }

        /// <summary>获取或设置投票问题列表。</summary>
        public IList<MeetingPollQuestion> poll_questions { get; set; }
    }

    /// <summary>
    /// 创建会议投票主题结果。
    /// </summary>
    public class CreateMeetingPollThemeResult : WorkJsonResult
    {
        /// <summary>获取或设置新建的投票主题 ID。</summary>
        public string poll_theme_id { get; set; }
    }

    /// <summary>
    /// 更新会议投票主题请求。
    /// </summary>
    public class UpdateMeetingPollThemeRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置投票主题。</summary>
        public string poll_topic { get; set; }

        /// <summary>获取或设置投票主题描述。</summary>
        public string poll_desc { get; set; }

        /// <summary>获取或设置是否匿名，0 表示否，1 表示是。</summary>
        public int? is_anony { get; set; }

        /// <summary>获取或设置投票问题列表。</summary>
        public IList<MeetingPollQuestion> poll_questions { get; set; }
    }

    /// <summary>
    /// 更新会议投票主题结果。
    /// </summary>
    public class UpdateMeetingPollThemeResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取会议投票列表请求。
    /// </summary>
    public class GetMeetingPollListRequest : MeetingPollOperatorRequest
    {
    }

    /// <summary>
    /// 单次会议投票摘要。
    /// </summary>
    public class MeetingPollSummary
    {
        /// <summary>获取或设置投票 ID。</summary>
        public string poll_id { get; set; }

        /// <summary>获取或设置投票主题。</summary>
        public string poll_topic { get; set; }

        /// <summary>获取或设置投票状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置是否共享，0 表示否，1 表示是。</summary>
        public int is_shared { get; set; }

        /// <summary>获取或设置是否匿名，0 表示否，1 表示是。</summary>
        public int is_anony { get; set; }
    }

    /// <summary>
    /// 会议投票主题及其投票摘要。
    /// </summary>
    public class MeetingPollThemeSummary
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置主题下的投票摘要列表。</summary>
        public IList<MeetingPollSummary> polls_info { get; set; }
    }

    /// <summary>
    /// 获取会议投票列表结果。
    /// </summary>
    public class GetMeetingPollListResult : WorkJsonResult
    {
        /// <summary>获取或设置按主题分组的投票列表。</summary>
        public IList<MeetingPollThemeSummary> polls_theme_info { get; set; }
    }

    /// <summary>
    /// 获取会议投票主题详情请求。
    /// </summary>
    public class GetMeetingPollThemeInfoRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }
    }

    /// <summary>
    /// 会议投票主题中的选项定义。
    /// </summary>
    public class MeetingPollThemeOption
    {
        /// <summary>获取或设置选项描述。</summary>
        public string option_desc { get; set; }
    }

    /// <summary>
    /// 会议投票主题中的问题详情。
    /// </summary>
    public class MeetingPollThemeQuestion
    {
        /// <summary>获取或设置问题类型。</summary>
        public int question_type { get; set; }

        /// <summary>获取或设置问题描述。</summary>
        public string question_desc { get; set; }

        /// <summary>获取或设置选项定义列表。</summary>
        public IList<MeetingPollThemeOption> option_info { get; set; }
    }

    /// <summary>
    /// 获取会议投票主题详情结果。
    /// </summary>
    public class GetMeetingPollThemeInfoResult : WorkJsonResult
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置投票主题。</summary>
        public string poll_topic { get; set; }

        /// <summary>获取或设置投票主题描述。</summary>
        public string poll_desc { get; set; }

        /// <summary>获取或设置是否匿名，0 表示否，1 表示是。</summary>
        public int is_anony { get; set; }

        /// <summary>获取或设置投票问题定义列表。</summary>
        public IList<MeetingPollThemeQuestion> poll_question_data { get; set; }
    }

    /// <summary>
    /// 获取会议单次投票详情请求。
    /// </summary>
    public class GetMeetingPollDetailRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票 ID。</summary>
        public string poll_id { get; set; }
    }

    /// <summary>
    /// 会议投票成员标识。
    /// </summary>
    public class MeetingPollVoter
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置参会成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 会议投票结果选项。
    /// </summary>
    public class MeetingPollDetailOption
    {
        /// <summary>获取或设置选项 ID；兼容数字或字符串响应。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string option_id { get; set; }

        /// <summary>获取或设置选项描述。</summary>
        public string option_desc { get; set; }

        /// <summary>获取或设置该选项的投票数。</summary>
        public int option_num { get; set; }

        /// <summary>获取或设置投票率，范围为 0 至 100。</summary>
        public int rate { get; set; }

        /// <summary>获取或设置选择该选项的成员列表。</summary>
        public IList<MeetingPollVoter> option_user { get; set; }
    }

    /// <summary>
    /// 会议投票结果问题。
    /// </summary>
    public class MeetingPollDetailQuestion
    {
        /// <summary>获取或设置问题 ID；兼容数字或字符串响应。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string question_id { get; set; }

        /// <summary>获取或设置问题类型。</summary>
        public int question_type { get; set; }

        /// <summary>获取或设置问题描述。</summary>
        public string question_desc { get; set; }

        /// <summary>获取或设置投票结果选项列表。</summary>
        public IList<MeetingPollDetailOption> option_info { get; set; }
    }

    /// <summary>
    /// 获取会议单次投票详情结果。
    /// </summary>
    public class GetMeetingPollDetailResult : WorkJsonResult
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置投票主题。</summary>
        public string poll_topic { get; set; }

        /// <summary>获取或设置投票主题描述。</summary>
        public string poll_desc { get; set; }

        /// <summary>获取或设置投票状态。</summary>
        public int status { get; set; }

        /// <summary>获取或设置是否共享，0 表示否，1 表示是。</summary>
        public int is_shared { get; set; }

        /// <summary>获取或设置是否匿名，0 表示否，1 表示是。</summary>
        public int is_anony { get; set; }

        /// <summary>获取或设置参加投票的总人数。</summary>
        public int vote_total_num { get; set; }

        /// <summary>获取或设置投票问题结果列表。</summary>
        public IList<MeetingPollDetailQuestion> poll_question_data { get; set; }
    }

    /// <summary>
    /// 删除会议投票请求。
    /// </summary>
    public class DeleteMeetingPollRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题 ID；与投票 ID 二选一。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置投票 ID；与投票主题 ID 二选一。</summary>
        public string poll_id { get; set; }
    }

    /// <summary>
    /// 删除会议投票结果。
    /// </summary>
    public class DeleteMeetingPollResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 开始会议投票请求。
    /// </summary>
    public class StartMeetingPollRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }
    }

    /// <summary>
    /// 开始会议投票结果。
    /// </summary>
    public class StartMeetingPollResult : WorkJsonResult
    {
        /// <summary>获取或设置本次投票 ID。</summary>
        public string poll_id { get; set; }
    }

    /// <summary>
    /// 结束会议投票请求。
    /// </summary>
    public class FinishMeetingPollRequest : MeetingPollOperatorRequest
    {
        /// <summary>获取或设置投票主题 ID。</summary>
        public string poll_theme_id { get; set; }

        /// <summary>获取或设置投票 ID。</summary>
        public string poll_id { get; set; }
    }

    /// <summary>
    /// 结束会议投票结果。
    /// </summary>
    public class FinishMeetingPollResult : WorkJsonResult
    {
    }
}
