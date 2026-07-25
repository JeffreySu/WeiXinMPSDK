/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TcbIncrementApi.cs
    文件功能描述：TcbIncrementApi 微信接口封装


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// SmsSendStatus 微信接口数据模型。
    /// </summary>
    public class SmsSendStatus
    {
        public string serial_no { get; set; }
        public string phone_number { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string iso_code { get; set; }
    }

    /// <summary>
    /// SendCloudBaseSms 接口返回结果。
    /// </summary>
    public class SendCloudBaseSmsJsonResult : WxJsonResult
    {
        public List<SmsSendStatus> send_status_list { get; set; }
    }

    /// <summary>
    /// SendCloudBaseSms 接口请求参数。
    /// </summary>
    public class SendCloudBaseSmsRequest
    {
        public string env { get; set; }
        public IList<string> phone_number_list { get; set; }
        public string sms_type { get; set; }
        public string template_id { get; set; }
        public string content { get; set; }
        public string path { get; set; }
        public IList<string> template_param_list { get; set; }
        public bool use_short_name { get; set; }
        public string resource_appid { get; set; }
    }

    /// <summary>
    /// CreateSendSmsTask 接口返回结果。
    /// </summary>
    public class CreateSendSmsTaskJsonResult : WxJsonResult
    {
        public string query_id { get; set; }
    }

    /// <summary>
    /// CloudBaseReport 接口请求参数。
    /// </summary>
    public class CloudBaseReportRequest
    {
        public string report_action { get; set; }
        public string env_id { get; set; }
        public string activity_id { get; set; }
        public string task_id { get; set; }
        public string phone_count { get; set; }
        public string channel_id { get; set; }
        public string session_id { get; set; }
    }

    /// <summary>
    /// DescribeSmsRecordsPost 数据。
    /// </summary>
    public class DescribeSmsRecordsPostData
    {
        public string EnvId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Mobile { get; set; }
        public string QueryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// SmsRecord 微信接口数据模型。
    /// </summary>
    public class SmsRecord
    {
        public string Mobile { get; set; }
        public string Content { get; set; }
        public int ContentSize { get; set; }
        public int Fee { get; set; }
        public string CreateTime { get; set; }
        public string ReceivedTime { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }

    /// <summary>
    /// DescribeSmsRecordsResponse 微信接口数据模型。
    /// </summary>
    public class DescribeSmsRecordsResponse
    {
        public List<SmsRecord> SmsRecords { get; set; }
        public long TotalCount { get; set; }
        public string RequestId { get; set; }
    }

    /// <summary>
    /// DescribeSmsRecords 接口返回结果。
    /// </summary>
    public class DescribeSmsRecordsJsonResult : WxJsonResult
    {
        public List<SmsRecord> SmsRecords { get; set; }
        public long TotalCount { get; set; }
        public string RequestId { get; set; }
        public DescribeSmsRecordsResponse Response { get; set; }
    }

    /// <summary>
    /// ExtensionFile 微信接口数据模型。
    /// </summary>
    public class ExtensionFile
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
    }

    /// <summary>
    /// ExtensionUploadFile 数据。
    /// </summary>
    public class ExtensionUploadFileData
    {
        public string CodeUri { get; set; }
        public string UploadUrl { get; set; }
        public string CustomKey { get; set; }
        public long MaxSize { get; set; }
    }

    /// <summary>
    /// ExtensionUploadResponse 微信接口数据模型。
    /// </summary>
    public class ExtensionUploadResponse
    {
        public List<ExtensionUploadFileData> FilesData { get; set; }
        public string RequestId { get; set; }
    }

    /// <summary>
    /// DescribeExtensionUploadInfo 接口返回结果。
    /// </summary>
    public class DescribeExtensionUploadInfoJsonResult : WxJsonResult
    {
        public List<ExtensionUploadFileData> FilesData { get; set; }
        public string RequestId { get; set; }
        public ExtensionUploadResponse Response { get; set; }
    }

    /// <summary>
    /// CloudBaseStatisticsCondition 微信接口数据模型。
    /// </summary>
    public class CloudBaseStatisticsCondition
    {
        public string env_id { get; set; }
        public string activity_id { get; set; }
        public string by_channel_id { get; set; }
    }

    /// <summary>
    /// CloudBaseStatisticsColumn 微信接口数据模型。
    /// </summary>
    public class CloudBaseStatisticsColumn
    {
        public string col_id { get; set; }
        public string col_name { get; set; }
        public string col_data_type { get; set; }
    }

    /// <summary>
    /// CloudBaseStatisticsRow 微信接口数据模型。
    /// </summary>
    public class CloudBaseStatisticsRow
    {
        public List<string> data_value { get; set; }
    }

    /// <summary>
    /// GetCloudBaseStatistics 接口返回结果。
    /// </summary>
    public class GetCloudBaseStatisticsJsonResult : WxJsonResult
    {
        public List<CloudBaseStatisticsColumn> data_column { get; set; }
        public List<CloudBaseStatisticsRow> data_value { get; set; }
        public long total_num { get; set; }
    }

    /// <summary>
    /// OpenData 数据项。
    /// </summary>
    public class OpenDataItem
    {
        public string cloud_id { get; set; }
        public object json { get; set; }
    }

    /// <summary>
    /// GetOpenData 接口返回结果。
    /// </summary>
    public class GetOpenDataJsonResult : WxJsonResult
    {
        public List<OpenDataItem> data_list { get; set; }
    }

    /// <summary>
    /// GetVoipSign 接口返回结果。
    /// </summary>
    public class GetVoipSignJsonResult : WxJsonResult
    {
        public string signature { get; set; }
    }

    /// <summary>
    /// 云开发增量接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class TcbIncrementApi
    {
        private static T Post<T>(string accessTokenOrAppId, string path, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId);
        }

        private static Task<T> PostAsync<T>(string accessTokenOrAppId, string path, object data, int timeOut)
            where T : WxJsonResult, new()
        {
            return WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<T>(accessToken, Config.ApiMpHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId);
        }

        /// <summary>
        /// 添加云函数延迟任务。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="delayTime">延迟执行秒数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult AddDelayedFunctionTask(string accessTokenOrAppId, string env, string functionName,
            string data, long delayTime, int timeOut = Config.TIME_OUT)
            => Post<WxJsonResult>(accessTokenOrAppId, "/tcb/adddelayedfunctiontask",
                new { env, function_name = functionName, data, delay_time = delayTime }, timeOut);

        /// <summary>
        /// 发送云开发短信（V2）。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="urlLink">短信跳转的 URL Link。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="templateParameters">短信模板参数。</param>
        /// <param name="phoneNumbers">接收短信的手机号列表。</param>
        /// <param name="useShortName">是否使用短信短签名。</param>
        /// <param name="resourceAppId">短信资源所属小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SendCloudBaseSmsJsonResult SendSmsV2(string accessTokenOrAppId, string env, string urlLink,
            string templateId, IList<string> templateParameters, IList<string> phoneNumbers, bool useShortName = false,
            string resourceAppId = null, int timeOut = Config.TIME_OUT)
            => Post<SendCloudBaseSmsJsonResult>(accessTokenOrAppId, "/tcb/sendsmsv2", new
            {
                env,
                url_link = urlLink,
                template_id = templateId,
                template_param_list = templateParameters,
                phone_number_list = phoneNumbers,
                use_short_name = useShortName,
                resource_appid = resourceAppId
            }, timeOut);

        /// <summary>
        /// 发送云开发短信。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SendCloudBaseSmsJsonResult SendSms(string accessTokenOrAppId, SendCloudBaseSmsRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SendCloudBaseSmsJsonResult>(accessTokenOrAppId, "/tcb/sendsms", request, timeOut);

        /// <summary>
        /// 创建云开发短信发送任务。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="fileUrl">待发送文件的 URL。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static CreateSendSmsTaskJsonResult CreateSendSmsTask(string accessTokenOrAppId, string env,
            string fileUrl, string templateId = "844110", int timeOut = Config.TIME_OUT)
            => Post<CreateSendSmsTaskJsonResult>(accessTokenOrAppId, "/tcb/createsendsmstask",
                new { env, file_url = fileUrl, template_id = templateId }, timeOut);

        /// <summary>
        /// 上报云开发数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Report(string accessTokenOrAppId, CloudBaseReportRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WxJsonResult>(accessTokenOrAppId, "/tcb/cloudbasereport", request, timeOut);

        /// <summary>
        /// 查询云开发短信发送记录。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="postData">上报数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static DescribeSmsRecordsJsonResult DescribeSmsRecords(string accessTokenOrAppId,
            DescribeSmsRecordsPostData postData, int timeOut = Config.TIME_OUT)
            => Post<DescribeSmsRecordsJsonResult>(accessTokenOrAppId, "/tcb/describesmsrecords",
                new { PostData = postData }, timeOut);

        /// <summary>
        /// 获取云开发扩展上传信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="files">待上传文件集合。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static DescribeExtensionUploadInfoJsonResult DescribeExtensionUploadInfo(string accessTokenOrAppId,
            IList<ExtensionFile> files, int timeOut = Config.TIME_OUT)
            => Post<DescribeExtensionUploadInfoJsonResult>(accessTokenOrAppId, "/tcb/describeextensionuploadinfo",
                new { PostData = new { ExtensionFiles = files } }, timeOut);

        /// <summary>
        /// 获取云开发统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="beginDate">统计开始日期。</param>
        /// <param name="endDate">统计结束日期。</param>
        /// <param name="pageOffset">分页偏移量。</param>
        /// <param name="pageLimit">每页记录数。</param>
        /// <param name="condition">统计筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetCloudBaseStatisticsJsonResult GetStatistics(string accessTokenOrAppId, string action,
            long beginDate, long endDate, int pageOffset, int pageLimit, CloudBaseStatisticsCondition condition = null,
            int timeOut = Config.TIME_OUT)
            => Post<GetCloudBaseStatisticsJsonResult>(accessTokenOrAppId, "/tcb/getstatistics", new
            {
                action,
                begin_date = beginDate,
                end_date = endDate,
                page_offset = pageOffset,
                page_limit = pageLimit,
                condition
            }, timeOut);

        /// <summary>
        /// 获取云调用开放数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cloudIds">待换取开放数据的 cloud_id 列表。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetOpenDataJsonResult GetOpenData(string accessTokenOrAppId, IList<string> cloudIds,
            string openId = null, int timeOut = Config.TIME_OUT)
        {
            var path = "/wxa/getopendata";
            if (!string.IsNullOrEmpty(openId)) path += "?openid=" + openId.AsUrlData() + "&access_token={0}";
            else path += "?access_token={0}";
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetOpenDataJsonResult>(accessToken, Config.ApiMpHost + path,
                    new { cloudid_list = cloudIds }, CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 获取实时音视频签名。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="timestamp">签名时间戳。</param>
        /// <param name="nonce">签名随机字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetVoipSignJsonResult GetVoipSign(string accessTokenOrAppId, string groupId, long timestamp,
            string nonce, int timeOut = Config.TIME_OUT)
            => Post<GetVoipSignJsonResult>(accessTokenOrAppId, "/wxa/getvoipsign",
                new { group_id = groupId, timestamp, nonce }, timeOut);

        /// <summary>
        /// 异步添加云函数延迟任务。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="functionName">云函数名称。</param>
        /// <param name="data">接口业务数据。</param>
        /// <param name="delayTime">延迟执行秒数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> AddDelayedFunctionTaskAsync(string accessTokenOrAppId, string env,
            string functionName, string data, long delayTime, int timeOut = Config.TIME_OUT)
            => PostAsync<WxJsonResult>(accessTokenOrAppId, "/tcb/adddelayedfunctiontask",
                new { env, function_name = functionName, data, delay_time = delayTime }, timeOut);

        /// <summary>
        /// 异步发送云开发短信（V2）。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="urlLink">短信跳转的 URL Link。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="templateParameters">短信模板参数。</param>
        /// <param name="phoneNumbers">接收短信的手机号列表。</param>
        /// <param name="useShortName">是否使用短信短签名。</param>
        /// <param name="resourceAppId">短信资源所属小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SendCloudBaseSmsJsonResult> SendSmsV2Async(string accessTokenOrAppId, string env,
            string urlLink, string templateId, IList<string> templateParameters, IList<string> phoneNumbers,
            bool useShortName = false, string resourceAppId = null, int timeOut = Config.TIME_OUT)
            => PostAsync<SendCloudBaseSmsJsonResult>(accessTokenOrAppId, "/tcb/sendsmsv2", new
            {
                env,
                url_link = urlLink,
                template_id = templateId,
                template_param_list = templateParameters,
                phone_number_list = phoneNumbers,
                use_short_name = useShortName,
                resource_appid = resourceAppId
            }, timeOut);

        /// <summary>
        /// 异步发送云开发短信。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SendCloudBaseSmsJsonResult> SendSmsAsync(string accessTokenOrAppId,
            SendCloudBaseSmsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SendCloudBaseSmsJsonResult>(accessTokenOrAppId, "/tcb/sendsms", request, timeOut);

        /// <summary>
        /// 异步创建云开发短信发送任务。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="env">云开发环境 ID。</param>
        /// <param name="fileUrl">待发送文件的 URL。</param>
        /// <param name="templateId">消息或短信模板 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<CreateSendSmsTaskJsonResult> CreateSendSmsTaskAsync(string accessTokenOrAppId, string env,
            string fileUrl, string templateId = "844110", int timeOut = Config.TIME_OUT)
            => PostAsync<CreateSendSmsTaskJsonResult>(accessTokenOrAppId, "/tcb/createsendsmstask",
                new { env, file_url = fileUrl, template_id = templateId }, timeOut);

        /// <summary>
        /// 异步上报云开发数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WxJsonResult> ReportAsync(string accessTokenOrAppId, CloudBaseReportRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WxJsonResult>(accessTokenOrAppId, "/tcb/cloudbasereport", request, timeOut);

        /// <summary>
        /// 异步查询云开发短信发送记录。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="postData">上报数据。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<DescribeSmsRecordsJsonResult> DescribeSmsRecordsAsync(string accessTokenOrAppId,
            DescribeSmsRecordsPostData postData, int timeOut = Config.TIME_OUT)
            => PostAsync<DescribeSmsRecordsJsonResult>(accessTokenOrAppId, "/tcb/describesmsrecords",
                new { PostData = postData }, timeOut);

        /// <summary>
        /// 异步获取云开发扩展上传信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="files">待上传文件集合。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<DescribeExtensionUploadInfoJsonResult> DescribeExtensionUploadInfoAsync(string accessTokenOrAppId,
            IList<ExtensionFile> files, int timeOut = Config.TIME_OUT)
            => PostAsync<DescribeExtensionUploadInfoJsonResult>(accessTokenOrAppId, "/tcb/describeextensionuploadinfo",
                new { PostData = new { ExtensionFiles = files } }, timeOut);

        /// <summary>
        /// 异步获取云开发统计数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="action">操作类型或待查询的统计指标。</param>
        /// <param name="beginDate">统计开始日期。</param>
        /// <param name="endDate">统计结束日期。</param>
        /// <param name="pageOffset">分页偏移量。</param>
        /// <param name="pageLimit">每页记录数。</param>
        /// <param name="condition">统计筛选条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetCloudBaseStatisticsJsonResult> GetStatisticsAsync(string accessTokenOrAppId, string action,
            long beginDate, long endDate, int pageOffset, int pageLimit, CloudBaseStatisticsCondition condition = null,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetCloudBaseStatisticsJsonResult>(accessTokenOrAppId, "/tcb/getstatistics", new
            {
                action,
                begin_date = beginDate,
                end_date = endDate,
                page_offset = pageOffset,
                page_limit = pageLimit,
                condition
            }, timeOut);

        /// <summary>
        /// 异步获取云调用开放数据。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="cloudIds">待换取开放数据的 cloud_id 列表。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetOpenDataJsonResult> GetOpenDataAsync(string accessTokenOrAppId, IList<string> cloudIds,
            string openId = null, int timeOut = Config.TIME_OUT)
        {
            var path = "/wxa/getopendata";
            if (!string.IsNullOrEmpty(openId)) path += "?openid=" + openId.AsUrlData() + "&access_token={0}";
            else path += "?access_token={0}";
            return WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetOpenDataJsonResult>(accessToken, Config.ApiMpHost + path,
                    new { cloudid_list = cloudIds }, CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppId);
        }

        /// <summary>
        /// 异步获取实时音视频签名。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="groupId">群聊、设备组或音视频房间 ID。</param>
        /// <param name="timestamp">签名时间戳。</param>
        /// <param name="nonce">签名随机字符串。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetVoipSignJsonResult> GetVoipSignAsync(string accessTokenOrAppId, string groupId,
            long timestamp, string nonce, int timeOut = Config.TIME_OUT)
            => PostAsync<GetVoipSignJsonResult>(accessTokenOrAppId, "/wxa/getvoipsign",
                new { group_id = groupId, timestamp, nonce }, timeOut);
    }
}
