/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataJson.cs
    文件功能描述：企业微信数据与智能专区现行接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐数据与智能专区现行接口强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>分页获取授权成员请求。</summary>
    public class ChatDataAuthorizedUserListRequest
    {
        /// <summary>上次请求返回的游标，首次请求可为空。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量，不超过 1000。</summary>
        public int? limit { get; set; }
    }

    /// <summary>已授权存档的成员。</summary>
    public class ChatDataAuthorizedUser
    {
        /// <summary>成员在数据与智能专区中的 OpenUserID。</summary>
        public string open_userid { get; set; }

        /// <summary>成员已授权的会话存档版本列表。</summary>
        public int[] edition_list { get; set; }
    }

    /// <summary>授权成员分页结果。</summary>
    public class ChatDataAuthorizedUserListResult : WorkJsonResult
    {
        /// <summary>生效成员列表。</summary>
        public ChatDataAuthorizedUser[] auth_user_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }

    /// <summary>专区授权范围。</summary>
    public class ChatDataAuthorizationScope
    {
        /// <summary>授权成员列表。</summary>
        public string[] userid_list { get; set; }

        /// <summary>授权部门 ID 列表。</summary>
        public long[] department_id_list { get; set; }

        /// <summary>授权标签 ID 列表。</summary>
        public long[] tag_id_list { get; set; }
    }

    /// <summary>企业开通的专区授权版本。</summary>
    public class ChatDataAuthorizationEdition
    {
        /// <summary>授权版本。</summary>
        public int edition { get; set; }

        /// <summary>授权范围。</summary>
        public ChatDataAuthorizationScope auth_scope { get; set; }

        /// <summary>授权状态。</summary>
        public int status { get; set; }

        /// <summary>授权开始时间，Unix 时间戳。</summary>
        public long begin_time { get; set; }

        /// <summary>授权结束时间，Unix 时间戳。</summary>
        public long end_time { get; set; }

        /// <summary>会话消息保留天数。</summary>
        public int msg_duration_days { get; set; }

        /// <summary>该版本已授权的成员数量。</summary>
        public int? auth_user_count { get; set; }
    }

    /// <summary>企业专区授权信息结果。</summary>
    public class ChatDataCorpAuthorizationResult : WorkJsonResult
    {
        /// <summary>企业开通的授权版本列表。</summary>
        public ChatDataAuthorizationEdition[] auth_edition_list { get; set; }
    }

    /// <summary>设置专区 RSA 公钥请求。</summary>
    public class ChatDataSetPublicKeyRequest
    {
        /// <summary>包含 BEGIN/END 标识和换行符的 RSA 公钥。</summary>
        public string public_key { get; set; }

        /// <summary>公钥版本；更换公钥时必须大于旧版本。</summary>
        public int public_key_ver { get; set; }
    }

    /// <summary>专区程序标识请求。</summary>
    public class ChatDataProgramIdentity
    {
        /// <summary>应用关联的专区程序 ID。</summary>
        public string program_id { get; set; }
    }

    /// <summary>设置专区程序日志级别请求。</summary>
    public class ChatDataSetLogLevelRequest : ChatDataProgramIdentity
    {
        /// <summary>日志级别：1-ERR、2-INFO、3-DBG。</summary>
        public int log_level { get; set; }
    }

    /// <summary>专区临时文件上传结果。</summary>
    public class ChatDataUploadMediaResult : WorkJsonResult
    {
        /// <summary>文件类型。</summary>
        public string type { get; set; }

        /// <summary>三天内有效的临时文件 MediaID。</summary>
        public string media_id { get; set; }

        /// <summary>文件上传时间，Unix 时间戳。</summary>
        public long created_at { get; set; }
    }

    /// <summary>同步拉取专区会话消息请求。</summary>
    public class ChatDataSyncMessagesRequest
    {
        /// <summary>首次拉取使用专区提供的 token，后续请求可为空。</summary>
        public string token { get; set; }

        /// <summary>分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回的最大数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>专区消息中的发送者或接收者。</summary>
    public class ChatDataMessageParticipant
    {
        /// <summary>参与者类型。</summary>
        public int type { get; set; }

        /// <summary>参与者 ID。</summary>
        public string id { get; set; }
    }

    /// <summary>专区消息的业务密钥加密信息。</summary>
    public class ChatDataServiceEncryptInfo
    {
        /// <summary>使用已设置 RSA 公钥加密的消息密钥。</summary>
        public string encrypted_secret_key { get; set; }

        /// <summary>加密该密钥使用的公钥版本。</summary>
        public int public_key_ver { get; set; }
    }

    /// <summary>专区中的一条加密会话消息。</summary>
    public class ChatDataMessage
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }

        /// <summary>消息类型编号。</summary>
        public int msgtype { get; set; }

        /// <summary>消息发送者。</summary>
        public ChatDataMessageParticipant sender { get; set; }

        /// <summary>消息接收者列表。</summary>
        public ChatDataMessageParticipant[] receiver_list { get; set; }

        /// <summary>群聊 ID，单聊时可为空。</summary>
        public string chatid { get; set; }

        /// <summary>消息发送时间，Unix 时间戳。</summary>
        public long send_time { get; set; }

        /// <summary>消息业务密钥加密信息。</summary>
        public ChatDataServiceEncryptInfo service_encrypt_info { get; set; }
    }

    /// <summary>专区会话消息分页结果。</summary>
    public class ChatDataSyncMessagesResult : WorkJsonResult
    {
        /// <summary>加密消息列表。</summary>
        public ChatDataMessage[] msg_list { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否仍有未返回的数据。</summary>
        public bool has_more { get; set; }
    }

    /// <summary>获取专区群聊请求。</summary>
    public class ChatDataGroupChatRequest
    {
        /// <summary>群聊 ID。</summary>
        public string chatid { get; set; }
    }

    /// <summary>专区群聊成员。</summary>
    public class ChatDataGroupChatMember
    {
        /// <summary>成员类型。</summary>
        public int type { get; set; }

        /// <summary>成员 ID。</summary>
        public string memberid { get; set; }

        /// <summary>入群时间，Unix 时间戳。</summary>
        public long jointime { get; set; }
    }

    /// <summary>专区群聊详情结果。</summary>
    public class ChatDataGroupChatResult : WorkJsonResult
    {
        /// <summary>群主的 OpenUserID。</summary>
        public string creator { get; set; }

        /// <summary>群聊创建时间，Unix 时间戳。</summary>
        public long room_create_time { get; set; }

        /// <summary>群聊成员列表。</summary>
        public ChatDataGroupChatMember[] members { get; set; }
    }

    /// <summary>开启专区程序调试模式请求。</summary>
    public class ChatDataOpenDebugModeRequest : ChatDataProgramIdentity
    {
        /// <summary>程序调试凭证。</summary>
        public string debug_token { get; set; }
    }

    /// <summary>专区程序调试模式状态结果。</summary>
    public class ChatDataDebugModeResult : WorkJsonResult
    {
        /// <summary>调试模式状态：1-关闭，2-开启。</summary>
        public int debug_mode_status { get; set; }
    }

    /// <summary>同步调用专区程序请求。</summary>
    public class ChatDataProgramCallRequest : ChatDataProgramTaskRequest
    {
        /// <summary>专区通知应用返回的通知 ID。</summary>
        public string notify_id { get; set; }
    }

    /// <summary>异步调用专区程序请求。</summary>
    public class ChatDataProgramTaskRequest : ChatDataProgramIdentity
    {
        /// <summary>程序关联的能力 ID。</summary>
        public string ability_id { get; set; }

        /// <summary>符合程序输入协议的 JSON 字符串。</summary>
        public string request_data { get; set; }
    }

    /// <summary>同步调用专区程序结果。</summary>
    public class ChatDataProgramCallResult : WorkJsonResult
    {
        /// <summary>程序输出协议对应的 JSON 字符串。</summary>
        public string response_data { get; set; }
    }

    /// <summary>异步专区程序任务创建结果。</summary>
    public class ChatDataProgramTaskResult : WorkJsonResult
    {
        /// <summary>异步任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>异步专区程序任务结果查询请求。</summary>
    public class ChatDataProgramResultRequest
    {
        /// <summary>异步任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>异步专区程序任务结果。</summary>
    public class ChatDataProgramResult : ChatDataProgramCallResult
    {
        /// <summary>专区程序自身返回的错误码。</summary>
        public int response_errcode { get; set; }
    }
}
