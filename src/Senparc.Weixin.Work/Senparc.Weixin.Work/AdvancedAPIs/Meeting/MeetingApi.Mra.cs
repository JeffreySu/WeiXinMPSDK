/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Mra.cs
    文件功能描述：企业微信会议连接器 MRA 会控接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议连接器状态、分屏、举手和挂断接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string GetMeetingMraStatusPath = "/cgi-bin/meeting/mra/query_status";
        private const string SetMeetingMraDefaultLayoutPath = "/cgi-bin/meeting/mra/set_default_layout";
        private const string SetMeetingMraRaiseHandPath = "/cgi-bin/meeting/mra/set_raise_hand";
        private const string HangupMeetingMraPath = "/cgi-bin/meeting/mra/hangup";

        /// <summary>
        /// 查询企业微信会议连接器 MRA 的实时状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98786"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和连接器成员临时 OpenId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>连接器终端、角色、音视频、共享、分屏和举手状态。</returns>
        public static GetMeetingMraStatusResult GetMeetingMraStatus(string accessTokenOrAppKey,
            GetMeetingMraStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingMraStatusResult>(accessTokenOrAppKey,
                GetMeetingMraStatusPath, request, timeOut);

        /// <summary>
        /// 异步查询企业微信会议连接器 MRA 的实时状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98786"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和连接器成员临时 OpenId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>连接器终端、角色、音视频、共享、分屏和举手状态。</returns>
        public static Task<GetMeetingMraStatusResult> GetMeetingMraStatusAsync(
            string accessTokenOrAppKey, GetMeetingMraStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingMraStatusResult>(accessTokenOrAppKey,
                GetMeetingMraStatusPath, request, timeOut);

        /// <summary>
        /// 设置企业微信会议连接器 MRA 的默认分屏布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98787"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、连接器、默认分屏和无视频成员显示方式。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认分屏设置结果。</returns>
        public static SetMeetingMraDefaultLayoutResult SetMeetingMraDefaultLayout(
            string accessTokenOrAppKey, SetMeetingMraDefaultLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetMeetingMraDefaultLayoutResult>(accessTokenOrAppKey,
                SetMeetingMraDefaultLayoutPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议连接器 MRA 的默认分屏布局。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98787"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、连接器、默认分屏和无视频成员显示方式。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>默认分屏设置结果。</returns>
        public static Task<SetMeetingMraDefaultLayoutResult> SetMeetingMraDefaultLayoutAsync(
            string accessTokenOrAppKey, SetMeetingMraDefaultLayoutRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingMraDefaultLayoutResult>(accessTokenOrAppKey,
                SetMeetingMraDefaultLayoutPath, request, timeOut);

        /// <summary>
        /// 设置企业微信会议连接器 MRA 的举手状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98788"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、连接器和目标举手状态。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>举手状态设置结果。</returns>
        public static SetMeetingMraRaiseHandResult SetMeetingMraRaiseHand(
            string accessTokenOrAppKey, SetMeetingMraRaiseHandRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SetMeetingMraRaiseHandResult>(accessTokenOrAppKey,
                SetMeetingMraRaiseHandPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议连接器 MRA 的举手状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98788"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、连接器和目标举手状态。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>举手状态设置结果。</returns>
        public static Task<SetMeetingMraRaiseHandResult> SetMeetingMraRaiseHandAsync(
            string accessTokenOrAppKey, SetMeetingMraRaiseHandRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingMraRaiseHandResult>(accessTokenOrAppKey,
                SetMeetingMraRaiseHandPath, request, timeOut);

        /// <summary>
        /// 挂断企业微信会议中的指定连接器 MRA。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98789"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要挂断的连接器成员临时 OpenId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>连接器挂断结果。</returns>
        public static HangupMeetingMraResult HangupMeetingMra(string accessTokenOrAppKey,
            HangupMeetingMraRequest request, int timeOut = Config.TIME_OUT)
            => Post<HangupMeetingMraResult>(accessTokenOrAppKey,
                HangupMeetingMraPath, request, timeOut);

        /// <summary>
        /// 异步挂断企业微信会议中的指定连接器 MRA。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98789"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要挂断的连接器成员临时 OpenId。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>连接器挂断结果。</returns>
        public static Task<HangupMeetingMraResult> HangupMeetingMraAsync(
            string accessTokenOrAppKey, HangupMeetingMraRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<HangupMeetingMraResult>(accessTokenOrAppKey,
                HangupMeetingMraPath, request, timeOut);
    }
}
