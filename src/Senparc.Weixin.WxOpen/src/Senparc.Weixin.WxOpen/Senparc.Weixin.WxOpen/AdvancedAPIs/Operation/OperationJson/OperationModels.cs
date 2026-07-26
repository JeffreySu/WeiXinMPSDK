/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OperationModels.cs
    文件功能描述：OperationModels 相关功能


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Operation
{
    /// <summary>
    /// GetDomainInfo 接口返回结果。
    /// </summary>
    public class GetDomainInfoJsonResult : WxJsonResult
    {
        public List<string> requestdomain { get; set; }
        public List<string> wsrequestdomain { get; set; }
        public List<string> uploaddomain { get; set; }
        public List<string> downloaddomain { get; set; }
        public List<string> udpdomain { get; set; }
        public List<string> bizdomain { get; set; }
    }

    /// <summary>
    /// GetOperationPerformance 接口返回结果。
    /// </summary>
    public class GetOperationPerformanceJsonResult : WxJsonResult
    {
        public string default_time_data { get; set; }
        public string compare_time_data { get; set; }
    }

    /// <summary>
    /// GetSceneList 接口返回结果。
    /// </summary>
    public class GetSceneListJsonResult : WxJsonResult
    {
        public List<OperationScene> scene { get; set; }
    }

    /// <summary>
    /// OperationScene 微信接口数据模型。
    /// </summary>
    public class OperationScene
    {
        public string name { get; set; }
        public object value { get; set; }
    }

    /// <summary>
    /// GetVersionList 接口返回结果。
    /// </summary>
    public class GetVersionListJsonResult : WxJsonResult
    {
        public List<ClientVersionGroup> cvlist { get; set; }
    }

    /// <summary>
    /// ClientVersionGroup 微信接口数据模型。
    /// </summary>
    public class ClientVersionGroup
    {
        public int type { get; set; }
        public List<string> client_version_list { get; set; }
    }

    /// <summary>
    /// RealTimeLogSearch 接口返回结果。
    /// </summary>
    public class RealTimeLogSearchJsonResult : WxJsonResult
    {
        public RealTimeLogData data { get; set; }
    }

    /// <summary>
    /// RealTimeLog 数据。
    /// </summary>
    public class RealTimeLogData
    {
        public List<RealTimeLogItem> list { get; set; }
        public long total { get; set; }
    }

    /// <summary>
    /// RealTimeLog 数据项。
    /// </summary>
    public class RealTimeLogItem
    {
        public int level { get; set; }
        public string libraryVersion { get; set; }
        public string clientVersion { get; set; }
        public string id { get; set; }
        public long timestamp { get; set; }
        public int platform { get; set; }
        public string url { get; set; }
        public List<RealTimeLogMessage> msg { get; set; }
        public string traceid { get; set; }
        public string filterMsg { get; set; }
    }

    /// <summary>
    /// RealTimeLogMessage 微信接口数据模型。
    /// </summary>
    public class RealTimeLogMessage
    {
        public long time { get; set; }
        public List<object> msg { get; set; }
        public int level { get; set; }
    }

    /// <summary>
    /// GetFeedback 接口返回结果。
    /// </summary>
    public class GetFeedbackJsonResult : WxJsonResult
    {
        public List<FeedbackItem> list { get; set; }
        public long total_num { get; set; }
    }

    /// <summary>
    /// Feedback 数据项。
    /// </summary>
    public class FeedbackItem
    {
        public long record_id { get; set; }
        public long create_time { get; set; }
        public string content { get; set; }
        public object phone { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string head_url { get; set; }
        public int type { get; set; }
        public List<string> mediaIds { get; set; }
        public string systemInfo { get; set; }
    }

    /// <summary>
    /// JsErrList 接口请求参数。
    /// </summary>
    public class JsErrListRequest
    {
        public string appVersion { get; set; }
        public string errType { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string keyword { get; set; }
        public string openid { get; set; }
        public string orderby { get; set; }
        public string desc { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
    }

    /// <summary>
    /// GetJsErrList 接口返回结果。
    /// </summary>
    public class GetJsErrListJsonResult : WxJsonResult
    {
        public bool success { get; set; }
        public string openid { get; set; }
        public List<JsErrListItem> data { get; set; }
        public long totalCount { get; set; }
    }

    /// <summary>
    /// JsErrList 数据项。
    /// </summary>
    public class JsErrListItem
    {
        public string errorMsgMd5 { get; set; }
        public string errorMsg { get; set; }
        public long uv { get; set; }
        public long pv { get; set; }
        public string errorStackMd5 { get; set; }
        public string errorStack { get; set; }
        public string pvPercent { get; set; }
        public string uvPercent { get; set; }
    }

    /// <summary>
    /// JsErrDetail 接口请求参数。
    /// </summary>
    public class JsErrDetailRequest
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string errorMsgMd5 { get; set; }
        public string errorStackMd5 { get; set; }
        public string appVersion { get; set; }
        public string sdkVersion { get; set; }
        public string osName { get; set; }
        public string clientVersion { get; set; }
        public string openid { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public string desc { get; set; }
    }

    /// <summary>
    /// GetJsErrDetail 接口返回结果。
    /// </summary>
    public class GetJsErrDetailJsonResult : WxJsonResult
    {
        public bool success { get; set; }
        public string openid { get; set; }
        public List<JsErrDetailItem> data { get; set; }
        public long totalCount { get; set; }
    }

    /// <summary>
    /// JsErrDetail 数据项。
    /// </summary>
    public class JsErrDetailItem
    {
        public string Count { get; set; }
        public string sdkVersion { get; set; }
        public string ClientVersion { get; set; }
        public string errorStackMd5 { get; set; }
        public string TimeStamp { get; set; }
        public string appVersion { get; set; }
        public string errorMsgMd5 { get; set; }
        public string errorMsg { get; set; }
        public string errorStack { get; set; }
        public string Ds { get; set; }
        public string OsName { get; set; }
        public string openId { get; set; }
        public string pluginversion { get; set; }
        public string appId { get; set; }
        public string DeviceModel { get; set; }
        public string source { get; set; }
        public string route { get; set; }
        public string nickname { get; set; }
    }
}
