/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OperationApi.cs
    文件功能描述：OperationApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Operation
{
    /// <summary>
    /// 小程序运维中心接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class OperationApi
    {
        private static string AppendQuery(string url, string name, object value)
        {
            return value == null ? url : url + "&" + name + "=" + value.ToString().AsUrlData();
        }

        #region 同步方法

        /// <summary>
        /// 查询小程序业务域名信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetDomainInfoJsonResult GetDomainInfo(string accessTokenOrAppId, string action = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetDomainInfoJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxa/getwxadevinfo?access_token={0}", new { action }, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序性能数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="costTimeType">耗时区间类型。</param>
        /// <param name="defaultStartTime">性能数据默认开始时间。</param>
        /// <param name="defaultEndTime">性能数据默认结束时间。</param>
        /// <param name="device">客户端设备类型。</param>
        /// <param name="isDownloadCode">是否为下载代码场景。</param>
        /// <param name="scene">小程序访问场景值。</param>
        /// <param name="networkType">客户端网络类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetOperationPerformanceJsonResult GetPerformance(string accessTokenOrAppId, int costTimeType,
            long defaultStartTime, long defaultEndTime, string device = "@_all", string isDownloadCode = "@_all",
            string scene = "@_all", string networkType = "@_all", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    cost_time_type = costTimeType,
                    default_start_time = defaultStartTime,
                    default_end_time = defaultEndTime,
                    device,
                    is_download_code = isDownloadCode,
                    scene,
                    networktype = networkType
                };
                return CommonJsonSend.Send<GetOperationPerformanceJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_performance?access_token={0}", data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序访问来源列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetSceneListJsonResult GetSceneList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetSceneListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_scene?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序客户端版本列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetVersionListJsonResult GetVersionList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetVersionListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_client_version?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 查询小程序实时日志。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="date">查询日期，格式为 yyyyMMdd。</param>
        /// <param name="beginTime">查询开始时间。</param>
        /// <param name="endTime">查询结束时间。</param>
        /// <param name="start">查询起始位置。</param>
        /// <param name="limit">单次查询条数。</param>
        /// <param name="traceId">日志 TraceId。</param>
        /// <param name="url">请求或日志页面 URL。</param>
        /// <param name="id">记录 ID。</param>
        /// <param name="filterMsg">日志内容过滤关键字。</param>
        /// <param name="level">日志级别。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static RealTimeLogSearchJsonResult RealTimeLogSearch(string accessTokenOrAppId, string date,
            long beginTime, long endTime, int start = 0, int limit = 20, string traceId = null, string url = null,
            string id = null, string filterMsg = null, int? level = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var requestUrl = Config.ApiMpHost + "/wxaapi/userlog/userlog_search?access_token=" + accessToken.AsUrlData();
                requestUrl = AppendQuery(requestUrl, "date", date);
                requestUrl = AppendQuery(requestUrl, "begintime", beginTime);
                requestUrl = AppendQuery(requestUrl, "endtime", endTime);
                requestUrl = AppendQuery(requestUrl, "start", start);
                requestUrl = AppendQuery(requestUrl, "limit", limit);
                requestUrl = AppendQuery(requestUrl, "traceId", traceId);
                requestUrl = AppendQuery(requestUrl, "url", url);
                requestUrl = AppendQuery(requestUrl, "id", id);
                requestUrl = AppendQuery(requestUrl, "filterMsg", filterMsg);
                requestUrl = AppendQuery(requestUrl, "level", level);
                return CommonJsonSend.Send<RealTimeLogSearchJsonResult>(accessToken, requestUrl, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序用户反馈列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="page">页码。</param>
        /// <param name="num">每页记录数。</param>
        /// <param name="type">查询或业务类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetFeedbackJsonResult GetFeedback(string accessTokenOrAppId, int page, int num,
            int? type = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var requestUrl = Config.ApiMpHost + "/wxaapi/feedback/list?access_token=" + accessToken.AsUrlData();
                requestUrl = AppendQuery(requestUrl, "type", type);
                requestUrl = AppendQuery(requestUrl, "page", page);
                requestUrl = AppendQuery(requestUrl, "num", num);
                return CommonJsonSend.Send<GetFeedbackJsonResult>(accessToken, requestUrl, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载小程序用户反馈媒体文件。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="recordId">反馈记录 ID。</param>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="stream">接收下载内容的可写流。</param>
        public static void GetFeedbackMedia(string accessTokenOrAppId, long recordId, string mediaId, Stream stream)
        {
            WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var requestUrl = string.Format(Config.ApiMpFileHost + "/cgi-bin/media/getfeedbackmedia?access_token={0}&record_id={1}&media_id={2}",
                    accessToken.AsUrlData(), recordId, mediaId.AsUrlData());
                CO2NET.HttpUtility.Get.Download(CommonDI.CommonSP, requestUrl, stream);
                return new WxJsonResult { errcode = ReturnCode.请求成功, errmsg = "ok" };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序 JavaScript 错误详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetJsErrDetailJsonResult GetJsErrDetail(string accessTokenOrAppId, JsErrDetailRequest request,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetJsErrDetailJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/jserr_detail?access_token={0}", request, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序 JavaScript 错误列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetJsErrListJsonResult GetJsErrList(string accessTokenOrAppId, JsErrListRequest request,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetJsErrListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/jserr_list?access_token={0}", request, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步查询小程序业务域名信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetDomainInfoJsonResult> GetDomainInfoAsync(string accessTokenOrAppId, string action = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetDomainInfoJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxa/getwxadevinfo?access_token={0}", new { action }, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序性能数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="costTimeType">耗时区间类型。</param>
        /// <param name="defaultStartTime">性能数据默认开始时间。</param>
        /// <param name="defaultEndTime">性能数据默认结束时间。</param>
        /// <param name="device">客户端设备类型。</param>
        /// <param name="isDownloadCode">是否为下载代码场景。</param>
        /// <param name="scene">小程序访问场景值。</param>
        /// <param name="networkType">客户端网络类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetOperationPerformanceJsonResult> GetPerformanceAsync(string accessTokenOrAppId, int costTimeType,
            long defaultStartTime, long defaultEndTime, string device = "@_all", string isDownloadCode = "@_all",
            string scene = "@_all", string networkType = "@_all", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                var data = new
                {
                    cost_time_type = costTimeType,
                    default_start_time = defaultStartTime,
                    default_end_time = defaultEndTime,
                    device,
                    is_download_code = isDownloadCode,
                    scene,
                    networktype = networkType
                };
                return CommonJsonSend.SendAsync<GetOperationPerformanceJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_performance?access_token={0}", data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序访问来源列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetSceneListJsonResult> GetSceneListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetSceneListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_scene?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序客户端版本列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetVersionListJsonResult> GetVersionListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetVersionListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/get_client_version?access_token={0}", null, CommonJsonSendType.GET, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步查询小程序实时日志。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="date">查询日期，格式为 yyyyMMdd。</param>
        /// <param name="beginTime">查询开始时间。</param>
        /// <param name="endTime">查询结束时间。</param>
        /// <param name="start">查询起始位置。</param>
        /// <param name="limit">单次查询条数。</param>
        /// <param name="traceId">日志 TraceId。</param>
        /// <param name="url">请求或日志页面 URL。</param>
        /// <param name="id">记录 ID。</param>
        /// <param name="filterMsg">日志内容过滤关键字。</param>
        /// <param name="level">日志级别。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<RealTimeLogSearchJsonResult> RealTimeLogSearchAsync(string accessTokenOrAppId, string date,
            long beginTime, long endTime, int start = 0, int limit = 20, string traceId = null, string url = null,
            string id = null, string filterMsg = null, int? level = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                var requestUrl = Config.ApiMpHost + "/wxaapi/userlog/userlog_search?access_token=" + accessToken.AsUrlData();
                requestUrl = AppendQuery(requestUrl, "date", date);
                requestUrl = AppendQuery(requestUrl, "begintime", beginTime);
                requestUrl = AppendQuery(requestUrl, "endtime", endTime);
                requestUrl = AppendQuery(requestUrl, "start", start);
                requestUrl = AppendQuery(requestUrl, "limit", limit);
                requestUrl = AppendQuery(requestUrl, "traceId", traceId);
                requestUrl = AppendQuery(requestUrl, "url", url);
                requestUrl = AppendQuery(requestUrl, "id", id);
                requestUrl = AppendQuery(requestUrl, "filterMsg", filterMsg);
                requestUrl = AppendQuery(requestUrl, "level", level);
                return CommonJsonSend.SendAsync<RealTimeLogSearchJsonResult>(accessToken, requestUrl, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序用户反馈列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="page">页码。</param>
        /// <param name="num">每页记录数。</param>
        /// <param name="type">查询或业务类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetFeedbackJsonResult> GetFeedbackAsync(string accessTokenOrAppId, int page, int num,
            int? type = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                var requestUrl = Config.ApiMpHost + "/wxaapi/feedback/list?access_token=" + accessToken.AsUrlData();
                requestUrl = AppendQuery(requestUrl, "type", type);
                requestUrl = AppendQuery(requestUrl, "page", page);
                requestUrl = AppendQuery(requestUrl, "num", num);
                return CommonJsonSend.SendAsync<GetFeedbackJsonResult>(accessToken, requestUrl, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步下载小程序用户反馈媒体文件。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="recordId">反馈记录 ID。</param>
        /// <param name="mediaId">媒体文件 MediaId。</param>
        /// <param name="stream">接收下载内容的可写流。</param>
        /// <returns>表示异步操作的任务。</returns>
        public static async Task GetFeedbackMediaAsync(string accessTokenOrAppId, long recordId, string mediaId, Stream stream)
        {
            await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var requestUrl = string.Format(Config.ApiMpFileHost + "/cgi-bin/media/getfeedbackmedia?access_token={0}&record_id={1}&media_id={2}",
                    accessToken.AsUrlData(), recordId, mediaId.AsUrlData());
                await CO2NET.HttpUtility.Get.DownloadAsync(CommonDI.CommonSP, requestUrl, stream).ConfigureAwait(false);
                return new WxJsonResult { errcode = ReturnCode.请求成功, errmsg = "ok" };
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序 JavaScript 错误详情。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetJsErrDetailJsonResult> GetJsErrDetailAsync(string accessTokenOrAppId, JsErrDetailRequest request,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetJsErrDetailJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/jserr_detail?access_token={0}", request, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取小程序 JavaScript 错误列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetJsErrListJsonResult> GetJsErrListAsync(string accessTokenOrAppId, JsErrListRequest request,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetJsErrListJsonResult>(accessToken,
                    Config.ApiMpHost + "/wxaapi/log/jserr_list?access_token={0}", request, CommonJsonSendType.POST, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}
