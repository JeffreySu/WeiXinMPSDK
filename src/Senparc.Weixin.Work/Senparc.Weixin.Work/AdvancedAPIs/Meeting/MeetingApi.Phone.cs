/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Phone.cs
    文件功能描述：企业微信会议电话外呼与临时 OpenId 接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议电话外呼、状态查询和临时 OpenId 接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string CalloutMeetingPhonesPath = "/cgi-bin/meeting/phone/callout";
        private const string GetMeetingPhoneCalloutStatusPath =
            "/cgi-bin/meeting/phone/get_callout_status";
        private const string GetMeetingPhoneTempOpenIdsPath = "/cgi-bin/meeting/phone/get_tmp_openid";

        /// <summary>
        /// 邀请电话号码通过电话加入企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98823"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要外呼的电话号码列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功与不合法的外呼号码列表。</returns>
        public static CalloutMeetingPhonesResult CalloutMeetingPhones(string accessTokenOrAppKey,
            CalloutMeetingPhonesRequest request, int timeOut = Config.TIME_OUT)
            => Post<CalloutMeetingPhonesResult>(accessTokenOrAppKey,
                CalloutMeetingPhonesPath, request, timeOut);

        /// <summary>
        /// 异步邀请电话号码通过电话加入企业微信会议。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98823"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和需要外呼的电话号码列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>成功与不合法的外呼号码列表。</returns>
        public static Task<CalloutMeetingPhonesResult> CalloutMeetingPhonesAsync(string accessTokenOrAppKey,
            CalloutMeetingPhonesRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<CalloutMeetingPhonesResult>(accessTokenOrAppKey,
                CalloutMeetingPhonesPath, request, timeOut);

        /// <summary>
        /// 分页获取企业微信会议电话外呼状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98824"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>电话号码、外呼状态及对应临时 OpenId。</returns>
        public static GetMeetingPhoneCalloutStatusResult GetMeetingPhoneCalloutStatus(
            string accessTokenOrAppKey, GetMeetingPhoneCalloutStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingPhoneCalloutStatusResult>(accessTokenOrAppKey,
                GetMeetingPhoneCalloutStatusPath, request, timeOut);

        /// <summary>
        /// 异步分页获取企业微信会议电话外呼状态。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98824"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和分页参数。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>电话号码、外呼状态及对应临时 OpenId。</returns>
        public static Task<GetMeetingPhoneCalloutStatusResult> GetMeetingPhoneCalloutStatusAsync(
            string accessTokenOrAppKey, GetMeetingPhoneCalloutStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingPhoneCalloutStatusResult>(accessTokenOrAppKey,
                GetMeetingPhoneCalloutStatusPath, request, timeOut);

        /// <summary>
        /// 根据电话号码批量获取企业微信会议成员临时 OpenId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98825"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和电话号码列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>电话号码与会议成员临时 OpenId 的对应关系。</returns>
        public static GetMeetingPhoneTempOpenIdsResult GetMeetingPhoneTempOpenIds(
            string accessTokenOrAppKey, GetMeetingPhoneTempOpenIdsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetMeetingPhoneTempOpenIdsResult>(accessTokenOrAppKey,
                GetMeetingPhoneTempOpenIdsPath, request, timeOut);

        /// <summary>
        /// 异步根据电话号码批量获取企业微信会议成员临时 OpenId。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98825"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID 和电话号码列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>电话号码与会议成员临时 OpenId 的对应关系。</returns>
        public static Task<GetMeetingPhoneTempOpenIdsResult> GetMeetingPhoneTempOpenIdsAsync(
            string accessTokenOrAppKey, GetMeetingPhoneTempOpenIdsRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingPhoneTempOpenIdsResult>(accessTokenOrAppKey,
                GetMeetingPhoneTempOpenIdsPath, request, timeOut);
    }
}
