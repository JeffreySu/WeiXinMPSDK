/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Management.cs
    文件功能描述：企业微信会议取消、详情与嘉宾管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会议取消、详情及嘉宾查询与设置接口；补齐会议设备参会检查接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string CancelMeetingPath = "/cgi-bin/meeting/cancel";
        private const string GetMeetingInfoPath = "/cgi-bin/meeting/get_info";
        private const string CheckDeviceInMeetingPath =
            "/cgi-bin/meeting/check_device_in_meeting";
        private const string GetMeetingGuestsPath = "/cgi-bin/meeting/get_guests";
        private const string SetMeetingGuestsPath = "/cgi-bin/meeting/set_guests";

        /// <summary>
        /// 取消企业微信会议或指定子会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93709"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98153"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和可选子会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消会议结果。</returns>
        public static CancelMeetingResult CancelMeeting(string accessTokenOrAppKey,
            CancelMeetingRequest request, int timeOut = Config.TIME_OUT)
            => Post<CancelMeetingResult>(accessTokenOrAppKey, CancelMeetingPath, request, timeOut);

        /// <summary>
        /// 异步取消企业微信会议或指定子会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93709"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98153"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和可选子会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>取消会议结果。</returns>
        public static Task<CancelMeetingResult> CancelMeetingAsync(string accessTokenOrAppKey,
            CancelMeetingRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CancelMeetingResult>(accessTokenOrAppKey, CancelMeetingPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93708"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98149"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、会议号或子会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议、参会成员、嘉宾、设置和周期信息。</returns>
        public static GetMeetingInfoResult GetMeetingInfo(string accessTokenOrAppKey,
            GetMeetingInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingInfoResult>(accessTokenOrAppKey, GetMeetingInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/93708"/>
        /// <see href="https://developer.work.weixin.qq.com/document/path/98149"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、会议号或子会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议、参会成员、嘉宾、设置和周期信息。</returns>
        public static Task<GetMeetingInfoResult> GetMeetingInfoAsync(string accessTokenOrAppKey,
            GetMeetingInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingInfoResult>(accessTokenOrAppKey, GetMeetingInfoPath, request, timeOut);

        /// <summary>
        /// 检查指定成员的终端设备是否正在目标会议中。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98164，但企业微信公开站点当前与等候室历史成员文档冲突；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">成员 UserId、可选终端类型和会议 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>匹配到的会议 ID 和终端设备类型列表。</returns>
        public static CheckDeviceInMeetingResult CheckDeviceInMeeting(
            string accessTokenOrAppKey, CheckDeviceInMeetingRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<CheckDeviceInMeetingResult>(accessTokenOrAppKey,
                CheckDeviceInMeetingPath, request, timeOut);

        /// <summary>
        /// 异步检查指定成员的终端设备是否正在目标会议中。
        /// </summary>
        /// <remarks>
        /// 固定协议记录的参考文档编号为 98164，但企业微信公开站点当前与等候室历史成员文档冲突；
        /// 本接口的路径和字段依据固定协议模型及请求响应样例实现。
        /// </remarks>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">成员 UserId、可选终端类型和会议 ID 列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>匹配到的会议 ID 和终端设备类型列表。</returns>
        public static Task<CheckDeviceInMeetingResult> CheckDeviceInMeetingAsync(
            string accessTokenOrAppKey, CheckDeviceInMeetingRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<CheckDeviceInMeetingResult>(accessTokenOrAppKey,
                CheckDeviceInMeetingPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99039"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议标识、主题和嘉宾列表。</returns>
        public static GetMeetingGuestsResult GetMeetingGuests(string accessTokenOrAppKey,
            GetMeetingGuestsRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingGuestsResult>(accessTokenOrAppKey, GetMeetingGuestsPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99039"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>会议标识、主题和嘉宾列表。</returns>
        public static Task<GetMeetingGuestsResult> GetMeetingGuestsAsync(string accessTokenOrAppKey,
            GetMeetingGuestsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingGuestsResult>(accessTokenOrAppKey, GetMeetingGuestsPath, request, timeOut);

        /// <summary>
        /// 设置企业微信会议嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99040"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要替换的嘉宾列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>嘉宾设置结果。</returns>
        public static SetMeetingGuestsResult SetMeetingGuests(string accessTokenOrAppKey,
            SetMeetingGuestsRequest request, int timeOut = Config.TIME_OUT)
            => Post<SetMeetingGuestsResult>(accessTokenOrAppKey, SetMeetingGuestsPath, request, timeOut);

        /// <summary>
        /// 异步设置企业微信会议嘉宾列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/99040"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要替换的嘉宾列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>嘉宾设置结果。</returns>
        public static Task<SetMeetingGuestsResult> SetMeetingGuestsAsync(string accessTokenOrAppKey,
            SetMeetingGuestsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SetMeetingGuestsResult>(accessTokenOrAppKey, SetMeetingGuestsPath, request, timeOut);
    }
}
