/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeChatCustomerServiceJson.cs
    文件功能描述：WeChatCustomerServiceJson 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService
{
    /// <summary>
    /// KfAccountAdd 接口请求参数。
    /// </summary>
    public class KfAccountAddRequest
    {
        public string name { get; set; }
        public string media_id { get; set; }
    }

    /// <summary>
    /// KfAccountAdd 接口返回结果。
    /// </summary>
    public class KfAccountAddResult : WorkJsonResult
    {
        public string open_kfid { get; set; }
    }

    /// <summary>
    /// KfAccountDelete 接口请求参数。
    /// </summary>
    public class KfAccountDeleteRequest
    {
        public string open_kfid { get; set; }
    }

    /// <summary>
    /// KfAccountUpdate 接口请求参数。
    /// </summary>
    public class KfAccountUpdateRequest : KfAccountDeleteRequest
    {
        public string name { get; set; }
        public string media_id { get; set; }
    }

    /// <summary>
    /// KfAccountList 接口请求参数。
    /// </summary>
    public class KfAccountListRequest
    {
        public int? offset { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// KfAccountList 接口返回结果。
    /// </summary>
    public class KfAccountListResult : WorkJsonResult
    {
        public IList<KfAccount> account_list { get; set; }
    }

    /// <summary>
    /// KfAccount 微信接口数据模型。
    /// </summary>
    public class KfAccount
    {
        public string open_kfid { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public bool manage_privilege { get; set; }
    }

    /// <summary>
    /// KfContactWay 接口请求参数。
    /// </summary>
    public class KfContactWayRequest
    {
        public string open_kfid { get; set; }
        public string scene { get; set; }
    }

    /// <summary>
    /// KfContactWay 接口返回结果。
    /// </summary>
    public class KfContactWayResult : WorkJsonResult
    {
        public string url { get; set; }
    }

    /// <summary>
    /// KfServicerChange 接口请求参数。
    /// </summary>
    public class KfServicerChangeRequest
    {
        public string open_kfid { get; set; }
        public IList<string> userid_list { get; set; }
        public IList<long> department_id_list { get; set; }
    }

    /// <summary>
    /// KfServicerChange 接口返回结果。
    /// </summary>
    public class KfServicerChangeResult : WorkJsonResult
    {
        public IList<KfServicerChangeItem> result_list { get; set; }
    }

    /// <summary>
    /// KfServicerChange 数据项。
    /// </summary>
    public class KfServicerChangeItem
    {
        public string userid { get; set; }
        public long? department_id { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    /// <summary>
    /// KfServicerList 接口返回结果。
    /// </summary>
    public class KfServicerListResult : WorkJsonResult
    {
        public IList<KfServicer> servicer_list { get; set; }
    }

    /// <summary>
    /// KfServicer 微信接口数据模型。
    /// </summary>
    public class KfServicer
    {
        public string userid { get; set; }
        public int status { get; set; }
        public int stop_type { get; set; }
        public long? department_id { get; set; }
    }

    /// <summary>
    /// KfServiceState 接口请求参数。
    /// </summary>
    public class KfServiceStateRequest
    {
        public string open_kfid { get; set; }
        public string external_userid { get; set; }
    }

    /// <summary>
    /// KfServiceState 接口返回结果。
    /// </summary>
    public class KfServiceStateResult : WorkJsonResult
    {
        public int service_state { get; set; }
        public string servicer_userid { get; set; }
    }

    /// <summary>
    /// KfServiceStateTransfer 接口请求参数。
    /// </summary>
    public class KfServiceStateTransferRequest : KfServiceStateRequest
    {
        public int service_state { get; set; }
        public string servicer_userid { get; set; }
    }

    /// <summary>
    /// KfServiceStateTransfer 接口返回结果。
    /// </summary>
    public class KfServiceStateTransferResult : WorkJsonResult
    {
        public string msg_code { get; set; }
    }

    /// <summary>
    /// KfSyncMessage 接口请求参数。
    /// </summary>
    public class KfSyncMessageRequest
    {
        public string cursor { get; set; }
        public string token { get; set; }
        public int? limit { get; set; }
        public int? voice_format { get; set; }
        public string open_kfid { get; set; }
    }

    /// <summary>
    /// KfSyncMessage 接口返回结果。
    /// </summary>
    public class KfSyncMessageResult : WorkJsonResult
    {
        public string next_cursor { get; set; }
        public int has_more { get; set; }
        public IList<KfMessage> msg_list { get; set; }
    }

    /// <summary>
    /// KfMessage 微信接口数据模型。
    /// </summary>
    public class KfMessage
    {
        public string msgid { get; set; }
        public string open_kfid { get; set; }
        public string external_userid { get; set; }
        public long send_time { get; set; }
        public int origin { get; set; }
        public string servicer_userid { get; set; }
        public string msgtype { get; set; }
        public KfTextMessage text { get; set; }
        public KfMediaMessage image { get; set; }
        public KfMediaMessage voice { get; set; }
        public KfMediaMessage video { get; set; }
        public KfMediaMessage file { get; set; }
        public KfLocationMessage location { get; set; }
        public KfLinkMessage link { get; set; }
        public KfMiniProgramMessage miniprogram { get; set; }
        public KfMessageMenu msgmenu { get; set; }
        public KfEventMessage @event { get; set; }
    }

    /// <summary>
    /// KfTextMessage 微信接口数据模型。
    /// </summary>
    public class KfTextMessage
    {
        public string content { get; set; }
        public string menu_id { get; set; }
    }

    /// <summary>
    /// KfMediaMessage 微信接口数据模型。
    /// </summary>
    public class KfMediaMessage
    {
        public string media_id { get; set; }
    }

    /// <summary>
    /// KfLocationMessage 微信接口数据模型。
    /// </summary>
    public class KfLocationMessage
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    /// <summary>
    /// KfLinkMessage 微信接口数据模型。
    /// </summary>
    public class KfLinkMessage
    {
        public string title { get; set; }
        public string desc { get; set; }
        public string url { get; set; }
        public string thumb_media_id { get; set; }
    }

    /// <summary>
    /// KfMiniProgramMessage 微信接口数据模型。
    /// </summary>
    public class KfMiniProgramMessage
    {
        public string appid { get; set; }
        public string title { get; set; }
        public string pagepath { get; set; }
        public string thumb_media_id { get; set; }
    }

    /// <summary>
    /// KfEventMessage 微信接口数据模型。
    /// </summary>
    public class KfEventMessage
    {
        public string event_type { get; set; }
        public string open_kfid { get; set; }
        public string external_userid { get; set; }
        public string scene { get; set; }
        public string scene_param { get; set; }
        public string welcome_code { get; set; }
        public string msg_code { get; set; }
        public string servicer_userid { get; set; }
        public int? service_state { get; set; }
        public int? change_type { get; set; }
        public int? fail_type { get; set; }
        public string fail_msgid { get; set; }
        public KfWeChatChannels wechat_channels { get; set; }
    }

    /// <summary>
    /// KfWeChatChannels 微信接口数据模型。
    /// </summary>
    public class KfWeChatChannels
    {
        public string nickname { get; set; }
        public string shop_nickname { get; set; }
        public int scene { get; set; }
    }

    /// <summary>
    /// KfSendMessage 接口请求参数。
    /// </summary>
    public class KfSendMessageRequest
    {
        public string touser { get; set; }
        public string open_kfid { get; set; }
        public string msgid { get; set; }
        public string msgtype { get; set; }
        public KfTextMessage text { get; set; }
        public KfMediaMessage image { get; set; }
        public KfMediaMessage voice { get; set; }
        public KfMediaMessage video { get; set; }
        public KfMediaMessage file { get; set; }
        public KfLinkMessage link { get; set; }
        public KfMiniProgramMessage miniprogram { get; set; }
        public KfMessageMenu msgmenu { get; set; }
    }

    /// <summary>
    /// KfSendEventMessage 接口请求参数。
    /// </summary>
    public class KfSendEventMessageRequest
    {
        public string code { get; set; }
        public string msgid { get; set; }
        public string msgtype { get; set; }
        public KfTextMessage text { get; set; }
        public KfMessageMenu msgmenu { get; set; }
    }

    /// <summary>
    /// KfSendMessage 接口返回结果。
    /// </summary>
    public class KfSendMessageResult : WorkJsonResult
    {
        public string msgid { get; set; }
    }

    /// <summary>
    /// KfMessageMenu 微信接口数据模型。
    /// </summary>
    public class KfMessageMenu
    {
        public string head_content { get; set; }
        public IList<KfMessageMenuItem> list { get; set; }
        public string tail_content { get; set; }
    }

    /// <summary>
    /// KfMessageMenu 数据项。
    /// </summary>
    public class KfMessageMenuItem
    {
        public string type { get; set; }
        public KfMessageMenuClick click { get; set; }
        public KfMessageMenuView view { get; set; }
        public KfMessageMenuMiniProgram miniprogram { get; set; }
        public KfMessageMenuText text { get; set; }
    }

    /// <summary>
    /// KfMessageMenuClick 微信接口数据模型。
    /// </summary>
    public class KfMessageMenuClick
    {
        public string id { get; set; }
        public string content { get; set; }
    }

    /// <summary>
    /// KfMessageMenuView 微信接口数据模型。
    /// </summary>
    public class KfMessageMenuView
    {
        public string url { get; set; }
        public string content { get; set; }
    }

    /// <summary>
    /// KfMessageMenuMiniProgram 微信接口数据模型。
    /// </summary>
    public class KfMessageMenuMiniProgram : KfMessageMenuView
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    /// <summary>
    /// KfMessageMenuText 微信接口数据模型。
    /// </summary>
    public class KfMessageMenuText
    {
        public string content { get; set; }
        public int? no_newline { get; set; }
    }

    /// <summary>
    /// KfUpgradeServiceConfig 接口返回结果。
    /// </summary>
    public class KfUpgradeServiceConfigResult : WorkJsonResult
    {
        public KfMemberRange member_range { get; set; }
        public KfGroupChatRange groupchat_range { get; set; }
    }

    /// <summary>
    /// KfMemberRange 微信接口数据模型。
    /// </summary>
    public class KfMemberRange
    {
        public IList<string> userid_list { get; set; }
        public IList<long> department_id_list { get; set; }
    }

    /// <summary>
    /// KfGroupChatRange 微信接口数据模型。
    /// </summary>
    public class KfGroupChatRange
    {
        public IList<string> chat_id_list { get; set; }
    }

    /// <summary>
    /// KfUpgradeService 接口请求参数。
    /// </summary>
    public class KfUpgradeServiceRequest : KfServiceStateRequest
    {
        public int type { get; set; }
        public KfUpgradeMember member { get; set; }
        public KfUpgradeGroupChat groupchat { get; set; }
    }

    /// <summary>
    /// KfUpgradeMember 微信接口数据模型。
    /// </summary>
    public class KfUpgradeMember
    {
        public string userid { get; set; }
        public string wording { get; set; }
    }

    /// <summary>
    /// KfUpgradeGroupChat 微信接口数据模型。
    /// </summary>
    public class KfUpgradeGroupChat
    {
        public string chat_id { get; set; }
        public string wording { get; set; }
    }

    /// <summary>
    /// KfBatchCustomer 接口请求参数。
    /// </summary>
    public class KfBatchCustomerRequest
    {
        public IList<string> external_userid_list { get; set; }
        public int? need_enter_session_context { get; set; }
    }

    /// <summary>
    /// KfBatchCustomer 接口返回结果。
    /// </summary>
    public class KfBatchCustomerResult : WorkJsonResult
    {
        public IList<KfCustomer> customer_list { get; set; }
        public IList<string> invalid_external_userid { get; set; }
    }

    /// <summary>
    /// KfCustomer 微信接口数据模型。
    /// </summary>
    public class KfCustomer
    {
        public string external_userid { get; set; }
        public string nickname { get; set; }
        public string avatar { get; set; }
        public int gender { get; set; }
        public string unionid { get; set; }
        public KfEnterSessionContext enter_session_context { get; set; }
    }

    /// <summary>
    /// KfEnterSessionContext 微信接口数据模型。
    /// </summary>
    public class KfEnterSessionContext
    {
        public string scene { get; set; }
        public string scene_param { get; set; }
        public KfWeChatChannels wechat_channels { get; set; }
    }

    /// <summary>
    /// KfStatistic 接口请求参数。
    /// </summary>
    public class KfStatisticRequest
    {
        public string open_kfid { get; set; }
        public long start_time { get; set; }
        public long end_time { get; set; }
    }

    /// <summary>
    /// KfServicerStatistic 接口请求参数。
    /// </summary>
    public class KfServicerStatisticRequest : KfStatisticRequest
    {
        public string servicer_userid { get; set; }
    }

    /// <summary>
    /// KfStatistic 接口返回结果。
    /// </summary>
    public class KfStatisticResult : WorkJsonResult
    {
        public IList<KfStatisticItem> statistic_list { get; set; }
    }

    /// <summary>
    /// KfStatistic 数据项。
    /// </summary>
    public class KfStatisticItem
    {
        public long stat_time { get; set; }
        public KfStatistic statistic { get; set; }
    }

    /// <summary>
    /// KfStatistic 微信接口数据模型。
    /// </summary>
    public class KfStatistic
    {
        public long? session_cnt { get; set; }
        public long? customer_cnt { get; set; }
        public long? customer_msg_cnt { get; set; }
        public long? upgrade_service_customer_cnt { get; set; }
        public long? ai_session_reply_cnt { get; set; }
        public double? ai_transfer_rate { get; set; }
        public double? ai_knowledge_hit_rate { get; set; }
        public long? msg_rejected_customer_cnt { get; set; }
        public double? reply_rate { get; set; }
        public double? first_reply_average_sec { get; set; }
        public long? satisfaction_investgate_cnt { get; set; }
        public double? satisfaction_participation_rate { get; set; }
        public double? satisfied_rate { get; set; }
        public double? middling_rate { get; set; }
        public double? dissatisfied_rate { get; set; }
        public long? upgrade_service_member_invite_cnt { get; set; }
        public long? upgrade_service_member_customer_cnt { get; set; }
        public long? upgrade_service_groupchat_invite_cnt { get; set; }
        public long? upgrade_service_groupchat_customer_cnt { get; set; }
    }

    /// <summary>
    /// KfKnowledgeGroup 接口请求参数。
    /// </summary>
    public class KfKnowledgeGroupRequest
    {
        public string group_id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// KfKnowledgeGroup 接口返回结果。
    /// </summary>
    public class KfKnowledgeGroupResult : WorkJsonResult
    {
        public string group_id { get; set; }
    }

    /// <summary>
    /// KfKnowledgeGroupList 接口请求参数。
    /// </summary>
    public class KfKnowledgeGroupListRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
        public string group_id { get; set; }
    }

    /// <summary>
    /// KfKnowledgeGroupList 接口返回结果。
    /// </summary>
    public class KfKnowledgeGroupListResult : WorkJsonResult
    {
        public string next_cursor { get; set; }
        public int has_more { get; set; }
        public IList<KfKnowledgeGroup> group_list { get; set; }
    }

    /// <summary>
    /// KfKnowledgeGroup 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeGroup
    {
        public string group_id { get; set; }
        public string name { get; set; }
        public int is_default { get; set; }
    }

    /// <summary>
    /// KfKnowledgeText 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeText
    {
        public string content { get; set; }
    }

    /// <summary>
    /// KfKnowledgeQuestion 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeQuestion
    {
        public KfKnowledgeText text { get; set; }
    }

    /// <summary>
    /// KfKnowledgeSimilarQuestions 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeSimilarQuestions
    {
        public IList<KfKnowledgeQuestion> items { get; set; }
    }

    /// <summary>
    /// KfKnowledgeAttachment 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeAttachment
    {
        public string msgtype { get; set; }
        public KfMediaMessage image { get; set; }
        public KfMediaMessage video { get; set; }
        public KfMediaMessage file { get; set; }
        public KfLinkMessage link { get; set; }
        public KfMiniProgramMessage miniprogram { get; set; }
    }

    /// <summary>
    /// KfKnowledgeAnswer 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeAnswer
    {
        public KfKnowledgeText text { get; set; }
        public IList<KfKnowledgeAttachment> attachments { get; set; }
    }

    /// <summary>
    /// KfKnowledgeIntent 接口请求参数。
    /// </summary>
    public class KfKnowledgeIntentRequest
    {
        public string intent_id { get; set; }
        public string group_id { get; set; }
        public KfKnowledgeQuestion question { get; set; }
        public KfKnowledgeSimilarQuestions similar_questions { get; set; }
        public IList<KfKnowledgeAnswer> answers { get; set; }
    }

    /// <summary>
    /// KfKnowledgeIntent 接口返回结果。
    /// </summary>
    public class KfKnowledgeIntentResult : WorkJsonResult
    {
        public string intent_id { get; set; }
    }

    /// <summary>
    /// KfKnowledgeIntentList 接口请求参数。
    /// </summary>
    public class KfKnowledgeIntentListRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
        public string group_id { get; set; }
        public string intent_id { get; set; }
    }

    /// <summary>
    /// KfKnowledgeIntentList 接口返回结果。
    /// </summary>
    public class KfKnowledgeIntentListResult : WorkJsonResult
    {
        public string next_cursor { get; set; }
        public int has_more { get; set; }
        public IList<KfKnowledgeIntent> intent_list { get; set; }
    }

    /// <summary>
    /// KfKnowledgeIntent 微信接口数据模型。
    /// </summary>
    public class KfKnowledgeIntent : KfKnowledgeIntentRequest
    {
        public int status { get; set; }
    }
}
