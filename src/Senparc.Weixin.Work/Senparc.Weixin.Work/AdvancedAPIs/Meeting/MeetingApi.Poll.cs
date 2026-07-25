/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingApi.Poll.cs
    文件功能描述：企业微信会议投票管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议投票主题、投票过程和结果查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    public static partial class MeetingApi
    {
        private const string CreateMeetingPollThemePath = "/cgi-bin/meeting/poll/create_theme";
        private const string UpdateMeetingPollThemePath = "/cgi-bin/meeting/poll/update_theme";
        private const string GetMeetingPollListPath = "/cgi-bin/meeting/poll/get_poll_list";
        private const string GetMeetingPollThemeInfoPath = "/cgi-bin/meeting/poll/get_theme_info";
        private const string GetMeetingPollDetailPath = "/cgi-bin/meeting/poll/get_poll_detail";
        private const string DeleteMeetingPollPath = "/cgi-bin/meeting/poll/delete";
        private const string StartMeetingPollPath = "/cgi-bin/meeting/poll/start";
        private const string FinishMeetingPollPath = "/cgi-bin/meeting/poll/finish";

        /// <summary>
        /// 创建企业微信会议投票主题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98834"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、操作者、投票主题和问题列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新建的投票主题 ID。</returns>
        public static CreateMeetingPollThemeResult CreateMeetingPollTheme(string accessTokenOrAppKey,
            CreateMeetingPollThemeRequest request, int timeOut = Config.TIME_OUT)
            => Post<CreateMeetingPollThemeResult>(accessTokenOrAppKey,
                CreateMeetingPollThemePath, request, timeOut);

        /// <summary>
        /// 异步创建企业微信会议投票主题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98834"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议、操作者、投票主题和问题列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>新建的投票主题 ID。</returns>
        public static Task<CreateMeetingPollThemeResult> CreateMeetingPollThemeAsync(
            string accessTokenOrAppKey, CreateMeetingPollThemeRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<CreateMeetingPollThemeResult>(accessTokenOrAppKey,
                CreateMeetingPollThemePath, request, timeOut);

        /// <summary>
        /// 更新企业微信会议投票主题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98835"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">投票主题 ID 及更新后的主题和问题列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票主题更新结果。</returns>
        public static UpdateMeetingPollThemeResult UpdateMeetingPollTheme(string accessTokenOrAppKey,
            UpdateMeetingPollThemeRequest request, int timeOut = Config.TIME_OUT)
            => Post<UpdateMeetingPollThemeResult>(accessTokenOrAppKey,
                UpdateMeetingPollThemePath, request, timeOut);

        /// <summary>
        /// 异步更新企业微信会议投票主题。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98835"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">投票主题 ID 及更新后的主题和问题列表。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票主题更新结果。</returns>
        public static Task<UpdateMeetingPollThemeResult> UpdateMeetingPollThemeAsync(
            string accessTokenOrAppKey, UpdateMeetingPollThemeRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateMeetingPollThemeResult>(accessTokenOrAppKey,
                UpdateMeetingPollThemePath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议的投票列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98836"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>按投票主题分组的投票摘要列表。</returns>
        public static GetMeetingPollListResult GetMeetingPollList(string accessTokenOrAppKey,
            GetMeetingPollListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingPollListResult>(accessTokenOrAppKey,
                GetMeetingPollListPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议的投票列表。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98836"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>按投票主题分组的投票摘要列表。</returns>
        public static Task<GetMeetingPollListResult> GetMeetingPollListAsync(string accessTokenOrAppKey,
            GetMeetingPollListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingPollListResult>(accessTokenOrAppKey,
                GetMeetingPollListPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议投票主题详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98837"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票主题、匿名设置、问题和选项定义。</returns>
        public static GetMeetingPollThemeInfoResult GetMeetingPollThemeInfo(string accessTokenOrAppKey,
            GetMeetingPollThemeInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingPollThemeInfoResult>(accessTokenOrAppKey,
                GetMeetingPollThemeInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议投票主题详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98837"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票主题、匿名设置、问题和选项定义。</returns>
        public static Task<GetMeetingPollThemeInfoResult> GetMeetingPollThemeInfoAsync(
            string accessTokenOrAppKey, GetMeetingPollThemeInfoRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingPollThemeInfoResult>(accessTokenOrAppKey,
                GetMeetingPollThemeInfoPath, request, timeOut);

        /// <summary>
        /// 获取企业微信会议单次投票详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98838"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票状态、参与人数、问题、选项及投票成员。</returns>
        public static GetMeetingPollDetailResult GetMeetingPollDetail(string accessTokenOrAppKey,
            GetMeetingPollDetailRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetMeetingPollDetailResult>(accessTokenOrAppKey,
                GetMeetingPollDetailPath, request, timeOut);

        /// <summary>
        /// 异步获取企业微信会议单次投票详情。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98838"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票状态、参与人数、问题、选项及投票成员。</returns>
        public static Task<GetMeetingPollDetailResult> GetMeetingPollDetailAsync(
            string accessTokenOrAppKey, GetMeetingPollDetailRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetMeetingPollDetailResult>(accessTokenOrAppKey,
                GetMeetingPollDetailPath, request, timeOut);

        /// <summary>
        /// 删除企业微信会议投票主题或单次投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98839"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及二选一的投票主题 ID 或投票 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票删除结果。</returns>
        public static DeleteMeetingPollResult DeleteMeetingPoll(string accessTokenOrAppKey,
            DeleteMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => Post<DeleteMeetingPollResult>(accessTokenOrAppKey,
                DeleteMeetingPollPath, request, timeOut);

        /// <summary>
        /// 异步删除企业微信会议投票主题或单次投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98839"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID，以及二选一的投票主题 ID 或投票 ID。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票删除结果。</returns>
        public static Task<DeleteMeetingPollResult> DeleteMeetingPollAsync(string accessTokenOrAppKey,
            DeleteMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<DeleteMeetingPollResult>(accessTokenOrAppKey,
                DeleteMeetingPollPath, request, timeOut);

        /// <summary>
        /// 开始企业微信会议投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98840"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>本次投票 ID。</returns>
        public static StartMeetingPollResult StartMeetingPoll(string accessTokenOrAppKey,
            StartMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => Post<StartMeetingPollResult>(accessTokenOrAppKey,
                StartMeetingPollPath, request, timeOut);

        /// <summary>
        /// 异步开始企业微信会议投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98840"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>本次投票 ID。</returns>
        public static Task<StartMeetingPollResult> StartMeetingPollAsync(string accessTokenOrAppKey,
            StartMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<StartMeetingPollResult>(accessTokenOrAppKey,
                StartMeetingPollPath, request, timeOut);

        /// <summary>
        /// 结束企业微信会议投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98841"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、投票 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票结束结果。</returns>
        public static FinishMeetingPollResult FinishMeetingPoll(string accessTokenOrAppKey,
            FinishMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => Post<FinishMeetingPollResult>(accessTokenOrAppKey,
                FinishMeetingPollPath, request, timeOut);

        /// <summary>
        /// 异步结束企业微信会议投票。
        /// <see href="https://developer.work.weixin.qq.com/document/path/98841"/>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="request">会议 ID、投票主题 ID、投票 ID、操作者和终端类型。</param>
        /// <param name="timeOut">请求超时时间，单位为毫秒。</param>
        /// <returns>投票结束结果。</returns>
        public static Task<FinishMeetingPollResult> FinishMeetingPollAsync(string accessTokenOrAppKey,
            FinishMeetingPollRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<FinishMeetingPollResult>(accessTokenOrAppKey,
                FinishMeetingPollPath, request, timeOut);
    }
}
