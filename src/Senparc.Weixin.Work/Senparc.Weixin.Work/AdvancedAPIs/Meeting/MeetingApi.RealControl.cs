/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.RealControl.cs
    文件功能描述：企业微信会议实时会控接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议实时会控、成员管理和结束会议接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string SetMeetingRealControlPath = "/cgi-bin/meeting/realcontrol/set";
        private const string SetMeetingCoHostPath = "/cgi-bin/meeting/realcontrol/set_cohost";
        private const string MuteMeetingUserPath = "/cgi-bin/meeting/realcontrol/mute_user";
        private const string SwitchMeetingUserVideoPath = "/cgi-bin/meeting/realcontrol/switch_user_video";
        private const string CloseMeetingScreenSharePath = "/cgi-bin/meeting/realcontrol/close_screen_share";
        private const string SetMeetingNicknamesPath = "/cgi-bin/meeting/realcontrol/set_nicknames";
        private const string ManageMeetingWaitingRoomUsersPath = "/cgi-bin/meeting/realcontrol/manage_waiting_room_users";
        private const string KickoutMeetingUsersPath = "/cgi-bin/meeting/realcontrol/kickout_users";
        private const string DismissMeetingPath = "/cgi-bin/meeting/realcontrol/dismiss";

        /// <summary>
        /// 设置企业微信会议的实时会控参数。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98175"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及需要变更的静音、锁定、聊天、共享、外部成员或等候室设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议实时会控设置结果。</returns>
        public static SetMeetingRealControlResult SetMeetingRealControl(string accessTokenOrAppKey,
            SetMeetingRealControlRequest request, int timeOut = Config.TIME_OUT)
            => Post<SetMeetingRealControlResult>(accessTokenOrAppKey,
                SetMeetingRealControlPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议的实时会控参数。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98175"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及需要变更的静音、锁定、聊天、共享、外部成员或等候室设置。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议实时会控设置结果。</returns>
        public static Task<SetMeetingRealControlResult> SetMeetingRealControlAsync(
            string accessTokenOrAppKey, SetMeetingRealControlRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingRealControlResult>(accessTokenOrAppKey,
                SetMeetingRealControlPath, request, timeOut);

        /// <summary>
        /// 设置或取消企业微信会议联席主持人。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98180"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、设置动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>联席主持人设置结果。</returns>
        public static SetMeetingCoHostResult SetMeetingCoHost(string accessTokenOrAppKey,
            SetMeetingCoHostRequest request, int timeOut = Config.TIME_OUT)
            => Post<SetMeetingCoHostResult>(accessTokenOrAppKey,
                SetMeetingCoHostPath, request, timeOut);

        /// <summary>
        /// 异步设置或取消企业微信会议联席主持人。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98180"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、设置动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>联席主持人设置结果。</returns>
        public static Task<SetMeetingCoHostResult> SetMeetingCoHostAsync(string accessTokenOrAppKey,
            SetMeetingCoHostRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingCoHostResult>(accessTokenOrAppKey,
                SetMeetingCoHostPath, request, timeOut);

        /// <summary>
        /// 静音或解除静音企业微信会议成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98184"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、静音动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员静音操作结果。</returns>
        public static MuteMeetingUserResult MuteMeetingUser(string accessTokenOrAppKey,
            MuteMeetingUserRequest request, int timeOut = Config.TIME_OUT)
            => Post<MuteMeetingUserResult>(accessTokenOrAppKey,
                MuteMeetingUserPath, request, timeOut);

        /// <summary>
        /// 异步静音或解除静音企业微信会议成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98184"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、静音动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员静音操作结果。</returns>
        public static Task<MuteMeetingUserResult> MuteMeetingUserAsync(string accessTokenOrAppKey,
            MuteMeetingUserRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<MuteMeetingUserResult>(accessTokenOrAppKey,
                MuteMeetingUserPath, request, timeOut);

        /// <summary>
        /// 开启或关闭企业微信会议成员的视频画面。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98189"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、视频动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员视频开关操作结果。</returns>
        public static SwitchMeetingUserVideoResult SwitchMeetingUserVideo(string accessTokenOrAppKey,
            SwitchMeetingUserVideoRequest request, int timeOut = Config.TIME_OUT)
            => Post<SwitchMeetingUserVideoResult>(accessTokenOrAppKey,
                SwitchMeetingUserVideoPath, request, timeOut);

        /// <summary>
        /// 异步开启或关闭企业微信会议成员的视频画面。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98189"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、视频动作和被操作成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员视频开关操作结果。</returns>
        public static Task<SwitchMeetingUserVideoResult> SwitchMeetingUserVideoAsync(
            string accessTokenOrAppKey, SwitchMeetingUserVideoRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SwitchMeetingUserVideoResult>(accessTokenOrAppKey,
                SwitchMeetingUserVideoPath, request, timeOut);

        /// <summary>
        /// 关闭企业微信会议成员的屏幕共享。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98185"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和正在共享屏幕的成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>关闭屏幕共享结果。</returns>
        public static CloseMeetingScreenShareResult CloseMeetingScreenShare(
            string accessTokenOrAppKey, CloseMeetingScreenShareRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<CloseMeetingScreenShareResult>(accessTokenOrAppKey,
                CloseMeetingScreenSharePath, request, timeOut);

        /// <summary>
        /// 异步关闭企业微信会议成员的屏幕共享。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98185"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和正在共享屏幕的成员。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>关闭屏幕共享结果。</returns>
        public static Task<CloseMeetingScreenShareResult> CloseMeetingScreenShareAsync(
            string accessTokenOrAppKey, CloseMeetingScreenShareRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<CloseMeetingScreenShareResult>(accessTokenOrAppKey,
                CloseMeetingScreenSharePath, request, timeOut);

        /// <summary>
        /// 批量设置企业微信会议成员昵称。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98188"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和成员昵称列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员昵称设置结果。</returns>
        public static SetMeetingNicknamesResult SetMeetingNicknames(string accessTokenOrAppKey,
            SetMeetingNicknamesRequest request, int timeOut = Config.TIME_OUT)
            => Post<SetMeetingNicknamesResult>(accessTokenOrAppKey,
                SetMeetingNicknamesPath, request, timeOut);

        /// <summary>
        /// 异步批量设置企业微信会议成员昵称。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98188"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和成员昵称列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成员昵称设置结果。</returns>
        public static Task<SetMeetingNicknamesResult> SetMeetingNicknamesAsync(
            string accessTokenOrAppKey, SetMeetingNicknamesRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingNicknamesResult>(accessTokenOrAppKey,
                SetMeetingNicknamesPath, request, timeOut);

        /// <summary>
        /// 管理企业微信会议等候室成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98186"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、操作类型、重新入会选项和成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>等候室成员管理结果。</returns>
        public static ManageMeetingWaitingRoomUsersResult ManageMeetingWaitingRoomUsers(
            string accessTokenOrAppKey, ManageMeetingWaitingRoomUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ManageMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                ManageMeetingWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 异步管理企业微信会议等候室成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98186"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、操作类型、重新入会选项和成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>等候室成员管理结果。</returns>
        public static Task<ManageMeetingWaitingRoomUsersResult> ManageMeetingWaitingRoomUsersAsync(
            string accessTokenOrAppKey, ManageMeetingWaitingRoomUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ManageMeetingWaitingRoomUsersResult>(accessTokenOrAppKey,
                ManageMeetingWaitingRoomUsersPath, request, timeOut);

        /// <summary>
        /// 批量移出企业微信会议成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98181"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、重新入会选项和被移出的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>批量移出成员结果。</returns>
        public static KickoutMeetingUsersResult KickoutMeetingUsers(string accessTokenOrAppKey,
            KickoutMeetingUsersRequest request, int timeOut = Config.TIME_OUT)
            => Post<KickoutMeetingUsersResult>(accessTokenOrAppKey,
                KickoutMeetingUsersPath, request, timeOut);

        /// <summary>
        /// 异步批量移出企业微信会议成员。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98181"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、重新入会选项和被移出的成员列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>批量移出成员结果。</returns>
        public static Task<KickoutMeetingUsersResult> KickoutMeetingUsersAsync(
            string accessTokenOrAppKey, KickoutMeetingUsersRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<KickoutMeetingUsersResult>(accessTokenOrAppKey,
                KickoutMeetingUsersPath, request, timeOut);

        /// <summary>
        /// 结束企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98187"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、强制结束选项和会议号回收选项。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>结束会议结果。</returns>
        public static DismissMeetingResult DismissMeeting(string accessTokenOrAppKey,
            DismissMeetingRequest request, int timeOut = Config.TIME_OUT)
            => Post<DismissMeetingResult>(accessTokenOrAppKey,
                DismissMeetingPath, request, timeOut);

        /// <summary>
        /// 异步结束企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98187"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、强制结束选项和会议号回收选项。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>结束会议结果。</returns>
        public static Task<DismissMeetingResult> DismissMeetingAsync(string accessTokenOrAppKey,
            DismissMeetingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<DismissMeetingResult>(accessTokenOrAppKey,
                DismissMeetingPath, request, timeOut);
    }
}
