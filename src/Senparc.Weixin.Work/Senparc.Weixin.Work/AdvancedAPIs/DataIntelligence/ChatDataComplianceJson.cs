/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataComplianceJson.cs
    文件功能描述：数据与智能专区同意状态、分析、导出和敏感信息模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐数据与智能专区同意状态、分析、导出和敏感信息模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>查询单聊会话存档同意状态的成员或外部联系人。</summary>
    public class ChatDataAgreeStatusUser
    {
        /// <summary>成员在数据与智能专区中的 OpenUserID。</summary>
        public string open_userid { get; set; }

        /// <summary>外部联系人 UserID。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>批量查询单聊会话存档同意状态请求。</summary>
    public class ChatDataSingleAgreeStatusRequest
    {
        /// <summary>待查询的成员或外部联系人列表。</summary>
        public ChatDataAgreeStatusUser[] item { get; set; }
    }

    /// <summary>单聊会话存档同意状态。</summary>
    public class ChatDataSingleAgreeStatusInfo : ChatDataAgreeStatusUser
    {
        /// <summary>同意状态，如 Agree 或 Disagree。</summary>
        public string agree_status { get; set; }

        /// <summary>状态变更时间，Unix 时间戳。</summary>
        public long status_change_time { get; set; }
    }

    /// <summary>单聊会话存档同意状态结果。</summary>
    public class ChatDataSingleAgreeStatusResult : WorkJsonResult
    {
        /// <summary>同意状态列表。</summary>
        public ChatDataSingleAgreeStatusInfo[] agreeinfo { get; set; }
    }

    /// <summary>查询群聊会话存档同意状态请求。</summary>
    public class ChatDataRoomAgreeStatusRequest
    {
        /// <summary>群聊 ID。</summary>
        public string chatid { get; set; }
    }

    /// <summary>群聊中外部联系人的会话存档同意状态。</summary>
    public class ChatDataRoomAgreeStatusInfo
    {
        /// <summary>外部联系人 UserID。</summary>
        public string external_userid { get; set; }

        /// <summary>同意状态，如 Agree 或 Disagree。</summary>
        public string agree_status { get; set; }

        /// <summary>状态变更时间，Unix 时间戳。</summary>
        public long status_change_time { get; set; }
    }

    /// <summary>群聊会话存档同意状态结果。</summary>
    public class ChatDataRoomAgreeStatusResult : WorkJsonResult
    {
        /// <summary>群聊中外部联系人的同意状态列表。</summary>
        public ChatDataRoomAgreeStatusInfo[] agreeinfo { get; set; }
    }

    /// <summary>会话内容分析消息密钥。</summary>
    public class ChatDataAnalyzeEncryptInfo
    {
        /// <summary>消息明文解密密钥。</summary>
        public string secret_key { get; set; }
    }

    /// <summary>待分析的会话消息。</summary>
    public class ChatDataAnalyzeMessage
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }

        /// <summary>消息密钥信息。</summary>
        public ChatDataAnalyzeEncryptInfo encrypt_info { get; set; }
    }

    /// <summary>添加会话内容分析任务请求。</summary>
    public class ChatDataAnalyzeTaskAddRequest
    {
        /// <summary>分析任务类型。</summary>
        public int analyze_task { get; set; }

        /// <summary>复用已有任务时提供的任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>待分析消息列表。</summary>
        public ChatDataAnalyzeMessage[] msg_list { get; set; }
    }

    /// <summary>添加分析任务时失败的消息。</summary>
    public class ChatDataAnalyzeTaskFailure
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }

        /// <summary>错误码。</summary>
        public int errcode { get; set; }

        /// <summary>错误说明。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>添加会话内容分析任务结果。</summary>
    public class ChatDataAnalyzeTaskAddResult : WorkJsonResult
    {
        /// <summary>分析任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>添加失败的消息列表。</summary>
        public ChatDataAnalyzeTaskFailure[] fail_list { get; set; }
    }

    /// <summary>通用专区任务查询或提交请求。</summary>
    public class ChatDataJobRequest
    {
        /// <summary>任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>批量会话内容分析结果。</summary>
    public class ChatDataAnalyzeBatchResult
    {
        /// <summary>分析结果 ID。</summary>
        public string result_id { get; set; }

        /// <summary>结果数据的业务密钥加密信息。</summary>
        public ChatDataServiceEncryptInfo service_encrypt_info { get; set; }
    }

    /// <summary>单条会话消息分析结果。</summary>
    public class ChatDataAnalyzeMessageResult
    {
        /// <summary>消息 ID。</summary>
        public string msgid { get; set; }

        /// <summary>错误码。</summary>
        public int errcode { get; set; }

        /// <summary>错误说明。</summary>
        public string errmsg { get; set; }

        /// <summary>情感分析结果。</summary>
        public int? sentiment_result { get; set; }

        /// <summary>垃圾内容分析结果。</summary>
        public int? spam_result { get; set; }
    }

    /// <summary>会话内容分析任务结果。</summary>
    public class ChatDataAnalyzeTaskResult : WorkJsonResult
    {
        /// <summary>任务状态。</summary>
        public int status { get; set; }

        /// <summary>批量分析结果。</summary>
        public ChatDataAnalyzeBatchResult analyze_result { get; set; }

        /// <summary>逐条消息分析结果。</summary>
        public ChatDataAnalyzeMessageResult[] analyze_result_list { get; set; }
    }

    /// <summary>创建专区数据导出任务请求。</summary>
    public class ChatDataExportCreateJobRequest
    {
        /// <summary>导出任务类型代码。</summary>
        public string code { get; set; }

        /// <summary>包含导出参数的临时文件 MediaID。</summary>
        public string media_id { get; set; }
    }

    /// <summary>创建专区任务结果。</summary>
    public class ChatDataJobResult : WorkJsonResult
    {
        /// <summary>任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>专区数据导出任务状态结果。</summary>
    public class ChatDataExportJobStatusResult : WorkJsonResult
    {
        /// <summary>任务状态。</summary>
        public int status { get; set; }

        /// <summary>任务成功后的导出结果 ID。</summary>
        public string result_id { get; set; }

        /// <summary>任务失败时的结果错误码。</summary>
        public int? result_errcode { get; set; }

        /// <summary>任务失败时的结果错误说明。</summary>
        public string result_errmsg { get; set; }
    }

    /// <summary>会话内容中的敏感信息隐藏配置。</summary>
    public class ChatDataSensitiveInfoConfig
    {
        /// <summary>是否隐藏手机号码。</summary>
        public bool? hide_mobile { get; set; }

        /// <summary>是否隐藏身份证号码。</summary>
        public bool? hide_idcard { get; set; }

        /// <summary>是否隐藏银行卡号。</summary>
        public bool? hide_bankno { get; set; }
    }

    /// <summary>查询成员敏感信息隐藏配置请求。</summary>
    public class ChatDataSensitiveInfoConfigRequest
    {
        /// <summary>成员在数据与智能专区中的 OpenUserID。</summary>
        public string open_userid { get; set; }
    }

    /// <summary>设置成员敏感信息隐藏配置请求。</summary>
    public class ChatDataSetSensitiveInfoConfigRequest : ChatDataSensitiveInfoConfigRequest
    {
        /// <summary>需要更新的敏感信息隐藏配置。</summary>
        public ChatDataSensitiveInfoConfig config { get; set; }
    }

    /// <summary>成员敏感信息隐藏配置结果。</summary>
    public class ChatDataSensitiveInfoConfigResult : WorkJsonResult
    {
        /// <summary>敏感信息隐藏配置。</summary>
        public ChatDataSensitiveInfoConfig config { get; set; }
    }
}
