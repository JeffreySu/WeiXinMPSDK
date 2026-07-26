/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SmartRobotJson.cs
    文件功能描述：SmartRobotJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.SmartRobot
{
    /// <summary>
    /// SmartRobotCallback 微信接口数据模型。
    /// </summary>
    public class SmartRobotCallback
    {
        public string msgid { get; set; }
        public long? create_time { get; set; }
        public string aibotid { get; set; }
        public string chatid { get; set; }
        public string chattype { get; set; }
        public SmartRobotFrom from { get; set; }
        public string response_url { get; set; }
        public string msgtype { get; set; }
        public SmartRobotText text { get; set; }
        public SmartRobotMedia image { get; set; }
        public SmartRobotMixed mixed { get; set; }
        public SmartRobotText voice { get; set; }
        public SmartRobotMedia file { get; set; }
        public SmartRobotMedia video { get; set; }
        public SmartRobotQuote quote { get; set; }
        public SmartRobotEvent @event { get; set; }
    }

    /// <summary>
    /// SmartRobotFrom 微信接口数据模型。
    /// </summary>
    public class SmartRobotFrom
    {
        public string corpid { get; set; }
        public string userid { get; set; }
    }

    /// <summary>
    /// SmartRobotText 微信接口数据模型。
    /// </summary>
    public class SmartRobotText
    {
        public string content { get; set; }
    }

    /// <summary>
    /// SmartRobotMedia 微信接口数据模型。
    /// </summary>
    public class SmartRobotMedia
    {
        public string url { get; set; }
        public string aeskey { get; set; }
        public string base64 { get; set; }
        public string md5 { get; set; }
    }

    /// <summary>
    /// SmartRobotMixed 微信接口数据模型。
    /// </summary>
    public class SmartRobotMixed
    {
        public IList<SmartRobotMessageItem> msg_item { get; set; }
    }

    /// <summary>
    /// SmartRobotMessage 数据项。
    /// </summary>
    public class SmartRobotMessageItem
    {
        public string msgtype { get; set; }
        public SmartRobotText text { get; set; }
        public SmartRobotMedia image { get; set; }
    }

    /// <summary>
    /// SmartRobotQuote 微信接口数据模型。
    /// </summary>
    public class SmartRobotQuote
    {
        public string msgtype { get; set; }
        public SmartRobotText text { get; set; }
        public SmartRobotMedia image { get; set; }
        public SmartRobotMedia file { get; set; }
        public SmartRobotMedia video { get; set; }
    }

    /// <summary>
    /// SmartRobotEvent 微信接口数据模型。
    /// </summary>
    public class SmartRobotEvent
    {
        public string eventtype { get; set; }
        public SmartRobotTemplateCardEvent template_card_event { get; set; }
        public SmartRobotFeedbackEvent feedback_event { get; set; }
        public object disconnected_event { get; set; }
    }

    /// <summary>
    /// SmartRobotTemplateCardEvent 微信接口数据模型。
    /// </summary>
    public class SmartRobotTemplateCardEvent
    {
        public string card_type { get; set; }
        public string event_key { get; set; }
        public string task_id { get; set; }
        public SmartRobotSelectedItems selected_items { get; set; }
    }

    /// <summary>
    /// SmartRobotSelectedItems 微信接口数据模型。
    /// </summary>
    public class SmartRobotSelectedItems
    {
        public IList<SmartRobotSelectedItem> selected_item { get; set; }
    }

    /// <summary>
    /// SmartRobotSelected 数据项。
    /// </summary>
    public class SmartRobotSelectedItem
    {
        public string question_key { get; set; }
        public SmartRobotOptionIds option_ids { get; set; }
    }

    /// <summary>
    /// SmartRobotOptionIds 微信接口数据模型。
    /// </summary>
    public class SmartRobotOptionIds
    {
        public IList<string> option_id { get; set; }
    }

    /// <summary>
    /// SmartRobotFeedbackEvent 微信接口数据模型。
    /// </summary>
    public class SmartRobotFeedbackEvent
    {
        public string id { get; set; }
        public int? feedback_type { get; set; }
    }

    /// <summary>
    /// SmartRobotReply 微信接口数据模型。
    /// </summary>
    public class SmartRobotReply
    {
        public string msgtype { get; set; }
        public SmartRobotText text { get; set; }
        public SmartRobotMarkdown markdown { get; set; }
        public SmartRobotStream stream { get; set; }
        public object template_card { get; set; }
        public string response_type { get; set; }
        public IList<string> userids { get; set; }
    }

    /// <summary>
    /// SmartRobotMarkdown 微信接口数据模型。
    /// </summary>
    public class SmartRobotMarkdown
    {
        public string content { get; set; }
        public SmartRobotFeedback feedback { get; set; }
    }

    /// <summary>
    /// SmartRobotStream 微信接口数据模型。
    /// </summary>
    public class SmartRobotStream
    {
        public string id { get; set; }
        public bool? finish { get; set; }
        public string content { get; set; }
        public IList<SmartRobotMessageItem> msg_item { get; set; }
        public SmartRobotFeedback feedback { get; set; }
    }

    /// <summary>
    /// SmartRobotFeedback 微信接口数据模型。
    /// </summary>
    public class SmartRobotFeedback
    {
        public string id { get; set; }
    }

    /// <summary>
    /// SmartSheetGroupChatList 接口请求参数。
    /// </summary>
    public class SmartSheetGroupChatListRequest
    {
        public string docid { get; set; }
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// SmartSheetGroupChatList 接口返回结果。
    /// </summary>
    public class SmartSheetGroupChatListResult : WorkJsonResult
    {
        public bool has_more { get; set; }
        public string next_cursor { get; set; }
        public IList<string> chat_id_list { get; set; }
    }

    /// <summary>
    /// SmartSheetGroupChat 接口请求参数。
    /// </summary>
    public class SmartSheetGroupChatRequest
    {
        public string docid { get; set; }
        public string chat_id { get; set; }
    }

    /// <summary>
    /// SmartSheetGroupChat 接口返回结果。
    /// </summary>
    public class SmartSheetGroupChatResult : WorkJsonResult
    {
        public string name { get; set; }
        public string owner { get; set; }
        public IList<string> user_list { get; set; }
    }

    /// <summary>
    /// UpdateSmartSheetGroupChat 接口请求参数。
    /// </summary>
    public class UpdateSmartSheetGroupChatRequest : SmartSheetGroupChatRequest
    {
        public string owner { get; set; }
        public IList<string> add_user_list { get; set; }
        public IList<string> del_user_list { get; set; }
    }

    /// <summary>
    /// SmartRobotSocketHeaders 微信接口数据模型。
    /// </summary>
    public class SmartRobotSocketHeaders
    {
        public string req_id { get; set; }
    }

    /// <summary>
    /// SmartRobotSocketEnvelope 微信接口数据模型。
    /// </summary>
    public class SmartRobotSocketEnvelope
    {
        public string cmd { get; set; }
        public SmartRobotSocketHeaders headers { get; set; }
        public object body { get; set; }
        public int? errcode { get; set; }
        public string errmsg { get; set; }
    }
}
