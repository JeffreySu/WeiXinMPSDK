/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRealControlJson.cs
    文件功能描述：企业微信会议实时会控请求与结果强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议实时会控和成员操作模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 会议实时会控请求基础字段。
    /// </summary>
    public class MeetingRealControlRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 会议实时会控中的被操作成员。
    /// </summary>
    public class MeetingRealControlParticipant
    {
        /// <summary>获取或设置参会成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置参会成员的终端设备类型。</summary>
        public int instance_id { get; set; }
    }

    /// <summary>
    /// 会议实时会控中的成员昵称设置。
    /// </summary>
    public class MeetingRealControlNicknameParticipant : MeetingRealControlParticipant
    {
        /// <summary>获取或设置成员在会议中的昵称。</summary>
        public string nickname { get; set; }
    }

    /// <summary>
    /// 设置会议实时会控参数请求。
    /// </summary>
    public class SetMeetingRealControlRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置是否全体静音。</summary>
        public bool? mute_all { get; set; }

        /// <summary>获取或设置成员入会静音模式。</summary>
        public int? enable_enter_mute { get; set; }

        /// <summary>获取或设置是否允许参会者自行解除静音。</summary>
        public bool? allow_unmute_self { get; set; }

        /// <summary>获取或设置是否锁定会议。</summary>
        public bool? meeting_locked { get; set; }

        /// <summary>获取或设置是否隐藏会议号和密码。</summary>
        public bool? hide_meeting_code_password { get; set; }

        /// <summary>获取或设置参会者聊天模式。</summary>
        public int? allow_chat { get; set; }

        /// <summary>获取或设置是否允许参会者发起屏幕共享。</summary>
        public bool? allow_share_screen { get; set; }

        /// <summary>获取或设置是否允许外部成员入会。</summary>
        public bool? allow_external_user { get; set; }

        /// <summary>获取或设置成员入会时是否播放提示音。</summary>
        public bool? play_ivr_on_join { get; set; }

        /// <summary>获取或设置是否开启等候室。</summary>
        public bool? enable_waiting_room { get; set; }
    }

    /// <summary>
    /// 设置会议实时会控参数结果。
    /// </summary>
    public class SetMeetingRealControlResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 设置或取消会议联席主持人请求。
    /// </summary>
    public class SetMeetingCoHostRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置操作动作；true 表示设置，false 表示取消。</summary>
        public bool action { get; set; }

        /// <summary>获取或设置被操作成员。</summary>
        public MeetingRealControlParticipant operated_user { get; set; }
    }

    /// <summary>
    /// 设置或取消会议联席主持人结果。
    /// </summary>
    public class SetMeetingCoHostResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 静音或解除静音会议成员请求。
    /// </summary>
    public class MuteMeetingUserRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置静音动作；true 表示静音，false 表示解除静音。</summary>
        public bool option { get; set; }

        /// <summary>获取或设置被操作成员。</summary>
        public MeetingRealControlParticipant operated_user { get; set; }
    }

    /// <summary>
    /// 静音或解除静音会议成员结果。
    /// </summary>
    public class MuteMeetingUserResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 开启或关闭会议成员视频请求。
    /// </summary>
    public class SwitchMeetingUserVideoRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置视频动作；true 表示开启，false 表示关闭。</summary>
        public bool video { get; set; }

        /// <summary>获取或设置被操作成员。</summary>
        public MeetingRealControlParticipant operated_user { get; set; }
    }

    /// <summary>
    /// 开启或关闭会议成员视频结果。
    /// </summary>
    public class SwitchMeetingUserVideoResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 关闭会议成员屏幕共享请求。
    /// </summary>
    public class CloseMeetingScreenShareRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置正在共享屏幕的成员。</summary>
        public MeetingRealControlParticipant operated_user { get; set; }
    }

    /// <summary>
    /// 关闭会议成员屏幕共享结果。
    /// </summary>
    public class CloseMeetingScreenShareResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 批量设置会议成员昵称请求。
    /// </summary>
    public class SetMeetingNicknamesRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置成员昵称列表。</summary>
        public IList<MeetingRealControlNicknameParticipant> operated_users { get; set; }
    }

    /// <summary>
    /// 批量设置会议成员昵称结果。
    /// </summary>
    public class SetMeetingNicknamesResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 管理会议等候室成员请求。
    /// </summary>
    public class ManageMeetingWaitingRoomUsersRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置等候室操作类型。</summary>
        public int operate_type { get; set; }

        /// <summary>获取或设置被移出时是否允许成员重新加入会议。</summary>
        public bool? allow_rejoin { get; set; }

        /// <summary>获取或设置被操作成员列表。</summary>
        public IList<MeetingRealControlParticipant> operated_users { get; set; }
    }

    /// <summary>
    /// 管理会议等候室成员结果。
    /// </summary>
    public class ManageMeetingWaitingRoomUsersResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 批量移出会议成员请求。
    /// </summary>
    public class KickoutMeetingUsersRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置是否允许被移出的成员重新加入会议。</summary>
        public bool? allow_rejoin { get; set; }

        /// <summary>获取或设置被移出的成员列表。</summary>
        public IList<MeetingRealControlParticipant> operated_users { get; set; }
    }

    /// <summary>
    /// 批量移出会议成员结果。
    /// </summary>
    public class KickoutMeetingUsersResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 结束会议请求。
    /// </summary>
    public class DismissMeetingRequest : MeetingRealControlRequest
    {
        /// <summary>获取或设置是否强制结束会议，0 表示否，1 表示是。</summary>
        public int? force_dismiss { get; set; }

        /// <summary>获取或设置是否回收会议号，0 表示否，1 表示是。</summary>
        public int? retrieve_code { get; set; }
    }

    /// <summary>
    /// 结束会议结果。
    /// </summary>
    public class DismissMeetingResult : WorkJsonResult
    {
    }
}
