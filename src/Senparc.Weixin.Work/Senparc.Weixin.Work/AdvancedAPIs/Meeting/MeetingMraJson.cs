/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingMraJson.cs
    文件功能描述：企业微信会议连接器 MRA 请求及结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议连接器状态、分屏、举手和挂断强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 企业微信会议连接器 MRA 目标。
    /// </summary>
    public class MeetingMraTarget
    {
        /// <summary>获取或设置连接器参会成员的临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 查询企业微信会议连接器 MRA 状态请求。
    /// </summary>
    public class GetMeetingMraStatusRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置连接器参会成员的临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 查询企业微信会议连接器 MRA 状态结果。
    /// </summary>
    public class GetMeetingMraStatusResult : WorkJsonResult
    {
        /// <summary>获取或设置连接器参会成员的临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置连接器终端设备类型。</summary>
        public int instance_id { get; set; }

        /// <summary>获取或设置连接器成员角色。</summary>
        public int user_role { get; set; }

        /// <summary>获取或设置网络研讨会中的成员角色。</summary>
        public int? webinar_member_role { get; set; }

        /// <summary>获取或设置连接器 IP 地址。</summary>
        public string ip { get; set; }

        /// <summary>获取或设置连接器显示名称。</summary>
        public string name { get; set; }

        /// <summary>获取或设置麦克风是否开启。</summary>
        public bool audio_state { get; set; }

        /// <summary>获取或设置摄像头是否开启。</summary>
        public bool video_state { get; set; }

        /// <summary>获取或设置是否正在共享屏幕。</summary>
        public bool screen_shared_state { get; set; }

        /// <summary>获取或设置默认分屏布局。</summary>
        public int default_layout { get; set; }

        /// <summary>获取或设置连接器是否处于举手状态。</summary>
        public bool raise_hands_state { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议连接器 MRA 默认分屏请求。
    /// </summary>
    public class SetMeetingMraDefaultLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置默认分屏布局。</summary>
        public int default_layout { get; set; }

        /// <summary>获取或设置无视频成员在默认分屏中的显示方式。</summary>
        public int default_novideo_user { get; set; }

        /// <summary>获取或设置需要控制的会议连接器。</summary>
        public MeetingMraTarget mra { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议连接器 MRA 默认分屏结果。
    /// </summary>
    public class SetMeetingMraDefaultLayoutResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 设置企业微信会议连接器 MRA 举手状态请求。
    /// </summary>
    public class SetMeetingMraRaiseHandRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置目标举手状态。</summary>
        public bool raise_hand { get; set; }

        /// <summary>获取或设置需要控制的会议连接器。</summary>
        public MeetingMraTarget mra { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议连接器 MRA 举手状态结果。
    /// </summary>
    public class SetMeetingMraRaiseHandResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 挂断企业微信会议连接器 MRA 请求。
    /// </summary>
    public class HangupMeetingMraRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要挂断的会议连接器。</summary>
        public MeetingMraTarget mra { get; set; }
    }

    /// <summary>
    /// 挂断企业微信会议连接器 MRA 结果。
    /// </summary>
    public class HangupMeetingMraResult : WorkJsonResult
    {
    }
}
