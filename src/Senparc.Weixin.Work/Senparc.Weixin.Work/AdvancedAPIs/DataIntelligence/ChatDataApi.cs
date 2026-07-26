/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChatDataApi.cs
    文件功能描述：企业微信数据与智能专区现行 ChatData 接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐数据与智能专区现行接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence
{
    /// <summary>
    /// 企业微信数据与智能专区现行接口。
    /// <para>所有接口均使用数据与智能专区应用 Secret 获取的 access_token。</para>
    /// </summary>
    public static class ChatDataApi
    {
        private const string GetAuthorizedUserListPath = "/cgi-bin/chatdata/get_auth_user_list";
        private const string GetCorpAuthorizationPath = "/cgi-bin/chatdata/get_corp_auth_info";
        private const string SetPublicKeyPath = "/cgi-bin/chatdata/set_public_key";
        private const string SetReceiveCallbackPath = "/cgi-bin/chatdata/set_receive_callback";
        private const string SetLogLevelPath = "/cgi-bin/chatdata/set_log_level";
        private const string UploadMediaPath = "/cgi-bin/chatdata/upload_media";
        private const string SyncMessagesPath = "/cgi-bin/chatdata/sync_msg";
        private const string GetGroupChatPath = "/cgi-bin/chatdata/groupchat/get";
        private const string GetSingleAgreeStatusPath = "/cgi-bin/chatdata/getagreestatus/single";
        private const string GetRoomAgreeStatusPath = "/cgi-bin/chatdata/getagreestatus/room";
        private const string AddAnalyzeTaskPath = "/cgi-bin/chatdata/analyze_task_add";
        private const string SubmitAnalyzeTaskPath = "/cgi-bin/chatdata/analyze_task_submit";
        private const string GetAnalyzeTaskResultPath = "/cgi-bin/chatdata/analyze_task_result";
        private const string OpenDebugModePath = "/cgi-bin/chatdata/open_debug_mode";
        private const string CloseDebugModePath = "/cgi-bin/chatdata/close_debug_mode";
        private const string GetDebugModePath = "/cgi-bin/chatdata/check_debug_mode";
        private const string CreateExportJobPath = "/cgi-bin/chatdata/export/create_job";
        private const string GetExportJobStatusPath = "/cgi-bin/chatdata/export/get_job_status";
        private const string SetSensitiveInfoConfigPath = "/cgi-bin/chatdata/set_hide_sensitiveinfo_config";
        private const string GetSensitiveInfoConfigPath = "/cgi-bin/chatdata/get_hide_sensitiveinfo_config";
        private const string CreateKeywordRulePath = "/cgi-bin/chatdata/keyword/create_rule";
        private const string UpdateKeywordRulePath = "/cgi-bin/chatdata/keyword/update_rule";
        private const string DeleteKeywordRulePath = "/cgi-bin/chatdata/keyword/delete_rule";
        private const string GetKeywordRuleListPath = "/cgi-bin/chatdata/keyword/get_rule_list";
        private const string GetKeywordRuleDetailPath = "/cgi-bin/chatdata/keyword/get_rule_detail";
        private const string GetKeywordHitMessageListPath = "/cgi-bin/chatdata/keyword/get_hit_msg_list";
        private const string SyncCallProgramPath = "/cgi-bin/chatdata/sync_call_program";
        private const string CreateAsyncProgramTaskPath = "/cgi-bin/chatdata/async_program_task";
        private const string GetAsyncProgramResultPath = "/cgi-bin/chatdata/async_program_result";
        private const string SearchChatPath = "/cgi-bin/chatdata/search_chat";
        private const string SearchMessagePath = "/cgi-bin/chatdata/search_msg";

        /// <summary>获取企业已授权存档的生效成员列表。</summary>
        public static ChatDataAuthorizedUserListResult GetAuthorizedUserList(string accessTokenOrAppKey,
            ChatDataAuthorizedUserListRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataAuthorizedUserListResult>(accessTokenOrAppKey,
                GetAuthorizedUserListPath, request, timeOut);

        /// <summary>异步获取企业已授权存档的生效成员列表。</summary>
        public static Task<ChatDataAuthorizedUserListResult> GetAuthorizedUserListAsync(
            string accessTokenOrAppKey, ChatDataAuthorizedUserListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataAuthorizedUserListResult>(accessTokenOrAppKey,
                GetAuthorizedUserListPath, request, timeOut);

        /// <summary>获取企业开通数据与智能专区的授权版本、范围和有效期。</summary>
        public static ChatDataCorpAuthorizationResult GetCorpAuthorization(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Post<ChatDataCorpAuthorizationResult>(accessTokenOrAppKey,
                GetCorpAuthorizationPath, new { }, timeOut);

        /// <summary>异步获取企业开通数据与智能专区的授权版本、范围和有效期。</summary>
        public static Task<ChatDataCorpAuthorizationResult> GetCorpAuthorizationAsync(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataCorpAuthorizationResult>(accessTokenOrAppKey,
                GetCorpAuthorizationPath, new { }, timeOut);

        /// <summary>设置用于会话内容密钥加密的 RSA 公钥。</summary>
        public static WorkJsonResult SetPublicKey(string accessTokenOrAppKey,
            ChatDataSetPublicKeyRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetPublicKeyPath, request, timeOut);

        /// <summary>异步设置用于会话内容密钥加密的 RSA 公钥。</summary>
        public static Task<WorkJsonResult> SetPublicKeyAsync(string accessTokenOrAppKey,
            ChatDataSetPublicKeyRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetPublicKeyPath, request, timeOut);

        /// <summary>设置接收专区回调事件的程序。</summary>
        public static WorkJsonResult SetReceiveCallback(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetReceiveCallbackPath, request, timeOut);

        /// <summary>异步设置接收专区回调事件的程序。</summary>
        public static Task<WorkJsonResult> SetReceiveCallbackAsync(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetReceiveCallbackPath, request, timeOut);

        /// <summary>设置专区程序的日志打印级别。</summary>
        public static WorkJsonResult SetLogLevel(string accessTokenOrAppKey,
            ChatDataSetLogLevelRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetLogLevelPath, request, timeOut);

        /// <summary>异步设置专区程序的日志打印级别。</summary>
        public static Task<WorkJsonResult> SetLogLevelAsync(string accessTokenOrAppKey,
            ChatDataSetLogLevelRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetLogLevelPath, request, timeOut);

        /// <summary>上传数据与智能专区临时文件，当前仅支持 file 类型。</summary>
        public static ChatDataUploadMediaResult UploadMedia(string accessTokenOrAppKey,
            string filePath, string type = "file", int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}{UploadMediaPath}?access_token={accessToken.AsUrlData()}" +
                          $"&type={type.AsUrlData()}";
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = filePath
                };
                return CO2NET.HttpUtility.Post.PostFileGetJson<ChatDataUploadMediaResult>(
                    CommonDI.CommonSP, url, null, files, null, timeOut: timeOut);
            }, accessTokenOrAppKey);

        /// <summary>异步上传数据与智能专区临时文件，当前仅支持 file 类型。</summary>
        public static Task<ChatDataUploadMediaResult> UploadMediaAsync(string accessTokenOrAppKey,
            string filePath, string type = "file", int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}{UploadMediaPath}?access_token={accessToken.AsUrlData()}" +
                          $"&type={type.AsUrlData()}";
                var files = new Dictionary<string, string>
                {
                    ["name"] = "media",
                    ["filename"] = filePath
                };
                return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<ChatDataUploadMediaResult>(
                    CommonDI.CommonSP, url, null, files, null, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);

        /// <summary>同步拉取专区中的加密会话消息。</summary>
        public static ChatDataSyncMessagesResult SyncMessages(string accessTokenOrAppKey,
            ChatDataSyncMessagesRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataSyncMessagesResult>(accessTokenOrAppKey, SyncMessagesPath, request, timeOut);

        /// <summary>异步拉取专区中的加密会话消息。</summary>
        public static Task<ChatDataSyncMessagesResult> SyncMessagesAsync(string accessTokenOrAppKey,
            ChatDataSyncMessagesRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataSyncMessagesResult>(accessTokenOrAppKey, SyncMessagesPath, request, timeOut);

        /// <summary>获取专区群聊的成员和创建信息。</summary>
        public static ChatDataGroupChatResult GetGroupChat(string accessTokenOrAppKey,
            ChatDataGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataGroupChatResult>(accessTokenOrAppKey, GetGroupChatPath, request, timeOut);

        /// <summary>异步获取专区群聊的成员和创建信息。</summary>
        public static Task<ChatDataGroupChatResult> GetGroupChatAsync(string accessTokenOrAppKey,
            ChatDataGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataGroupChatResult>(accessTokenOrAppKey, GetGroupChatPath, request, timeOut);

        /// <summary>批量获取成员与外部联系人的会话存档同意状态。</summary>
        public static ChatDataSingleAgreeStatusResult GetSingleAgreeStatus(string accessTokenOrAppKey,
            ChatDataSingleAgreeStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataSingleAgreeStatusResult>(accessTokenOrAppKey,
                GetSingleAgreeStatusPath, request, timeOut);

        /// <summary>异步批量获取成员与外部联系人的会话存档同意状态。</summary>
        public static Task<ChatDataSingleAgreeStatusResult> GetSingleAgreeStatusAsync(
            string accessTokenOrAppKey, ChatDataSingleAgreeStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataSingleAgreeStatusResult>(accessTokenOrAppKey,
                GetSingleAgreeStatusPath, request, timeOut);

        /// <summary>获取群聊中外部联系人的会话存档同意状态。</summary>
        public static ChatDataRoomAgreeStatusResult GetRoomAgreeStatus(string accessTokenOrAppKey,
            ChatDataRoomAgreeStatusRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataRoomAgreeStatusResult>(accessTokenOrAppKey,
                GetRoomAgreeStatusPath, request, timeOut);

        /// <summary>异步获取群聊中外部联系人的会话存档同意状态。</summary>
        public static Task<ChatDataRoomAgreeStatusResult> GetRoomAgreeStatusAsync(
            string accessTokenOrAppKey, ChatDataRoomAgreeStatusRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataRoomAgreeStatusResult>(accessTokenOrAppKey,
                GetRoomAgreeStatusPath, request, timeOut);

        /// <summary>添加会话内容分析任务。</summary>
        public static ChatDataAnalyzeTaskAddResult AddAnalyzeTask(string accessTokenOrAppKey,
            ChatDataAnalyzeTaskAddRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataAnalyzeTaskAddResult>(accessTokenOrAppKey,
                AddAnalyzeTaskPath, request, timeOut);

        /// <summary>异步添加会话内容分析任务。</summary>
        public static Task<ChatDataAnalyzeTaskAddResult> AddAnalyzeTaskAsync(
            string accessTokenOrAppKey, ChatDataAnalyzeTaskAddRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataAnalyzeTaskAddResult>(accessTokenOrAppKey,
                AddAnalyzeTaskPath, request, timeOut);

        /// <summary>提交会话内容分析任务。</summary>
        public static WorkJsonResult SubmitAnalyzeTask(string accessTokenOrAppKey,
            ChatDataJobRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SubmitAnalyzeTaskPath, request, timeOut);

        /// <summary>异步提交会话内容分析任务。</summary>
        public static Task<WorkJsonResult> SubmitAnalyzeTaskAsync(string accessTokenOrAppKey,
            ChatDataJobRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey,
                SubmitAnalyzeTaskPath, request, timeOut);

        /// <summary>获取会话内容分析任务结果。</summary>
        public static ChatDataAnalyzeTaskResult GetAnalyzeTaskResult(string accessTokenOrAppKey,
            ChatDataJobRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataAnalyzeTaskResult>(accessTokenOrAppKey,
                GetAnalyzeTaskResultPath, request, timeOut);

        /// <summary>异步获取会话内容分析任务结果。</summary>
        public static Task<ChatDataAnalyzeTaskResult> GetAnalyzeTaskResultAsync(
            string accessTokenOrAppKey, ChatDataJobRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataAnalyzeTaskResult>(accessTokenOrAppKey,
                GetAnalyzeTaskResultPath, request, timeOut);

        /// <summary>为专区程序开启调试模式。</summary>
        public static WorkJsonResult OpenDebugMode(string accessTokenOrAppKey,
            ChatDataOpenDebugModeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, OpenDebugModePath, request, timeOut);

        /// <summary>异步为专区程序开启调试模式。</summary>
        public static Task<WorkJsonResult> OpenDebugModeAsync(string accessTokenOrAppKey,
            ChatDataOpenDebugModeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, OpenDebugModePath, request, timeOut);

        /// <summary>关闭专区程序调试模式。</summary>
        public static WorkJsonResult CloseDebugMode(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CloseDebugModePath, request, timeOut);

        /// <summary>异步关闭专区程序调试模式。</summary>
        public static Task<WorkJsonResult> CloseDebugModeAsync(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CloseDebugModePath, request, timeOut);

        /// <summary>查询专区程序调试模式状态。</summary>
        public static ChatDataDebugModeResult GetDebugMode(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataDebugModeResult>(accessTokenOrAppKey, GetDebugModePath, request, timeOut);

        /// <summary>异步查询专区程序调试模式状态。</summary>
        public static Task<ChatDataDebugModeResult> GetDebugModeAsync(string accessTokenOrAppKey,
            ChatDataProgramIdentity request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataDebugModeResult>(accessTokenOrAppKey, GetDebugModePath, request, timeOut);

        /// <summary>创建专区数据导出任务。</summary>
        public static ChatDataJobResult CreateExportJob(string accessTokenOrAppKey,
            ChatDataExportCreateJobRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataJobResult>(accessTokenOrAppKey, CreateExportJobPath, request, timeOut);

        /// <summary>异步创建专区数据导出任务。</summary>
        public static Task<ChatDataJobResult> CreateExportJobAsync(string accessTokenOrAppKey,
            ChatDataExportCreateJobRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataJobResult>(accessTokenOrAppKey,
                CreateExportJobPath, request, timeOut);

        /// <summary>获取专区数据导出任务状态。</summary>
        public static ChatDataExportJobStatusResult GetExportJobStatus(string accessTokenOrAppKey,
            ChatDataJobRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataExportJobStatusResult>(accessTokenOrAppKey,
                GetExportJobStatusPath, request, timeOut);

        /// <summary>异步获取专区数据导出任务状态。</summary>
        public static Task<ChatDataExportJobStatusResult> GetExportJobStatusAsync(
            string accessTokenOrAppKey, ChatDataJobRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataExportJobStatusResult>(accessTokenOrAppKey,
                GetExportJobStatusPath, request, timeOut);

        /// <summary>设置成员敏感信息隐藏配置。</summary>
        public static WorkJsonResult SetSensitiveInfoConfig(string accessTokenOrAppKey,
            ChatDataSetSensitiveInfoConfigRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey,
                SetSensitiveInfoConfigPath, request, timeOut);

        /// <summary>异步设置成员敏感信息隐藏配置。</summary>
        public static Task<WorkJsonResult> SetSensitiveInfoConfigAsync(string accessTokenOrAppKey,
            ChatDataSetSensitiveInfoConfigRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey,
                SetSensitiveInfoConfigPath, request, timeOut);

        /// <summary>获取成员敏感信息隐藏配置。</summary>
        public static ChatDataSensitiveInfoConfigResult GetSensitiveInfoConfig(
            string accessTokenOrAppKey, ChatDataSensitiveInfoConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ChatDataSensitiveInfoConfigResult>(accessTokenOrAppKey,
                GetSensitiveInfoConfigPath, request, timeOut);

        /// <summary>异步获取成员敏感信息隐藏配置。</summary>
        public static Task<ChatDataSensitiveInfoConfigResult> GetSensitiveInfoConfigAsync(
            string accessTokenOrAppKey, ChatDataSensitiveInfoConfigRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataSensitiveInfoConfigResult>(accessTokenOrAppKey,
                GetSensitiveInfoConfigPath, request, timeOut);

        /// <summary>创建敏感关键词规则。</summary>
        public static ChatDataKeywordRuleCreatedResult CreateKeywordRule(string accessTokenOrAppKey,
            ChatDataKeywordRuleCreateRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataKeywordRuleCreatedResult>(accessTokenOrAppKey,
                CreateKeywordRulePath, request, timeOut);

        /// <summary>异步创建敏感关键词规则。</summary>
        public static Task<ChatDataKeywordRuleCreatedResult> CreateKeywordRuleAsync(
            string accessTokenOrAppKey, ChatDataKeywordRuleCreateRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataKeywordRuleCreatedResult>(accessTokenOrAppKey,
                CreateKeywordRulePath, request, timeOut);

        /// <summary>更新敏感关键词规则。</summary>
        public static WorkJsonResult UpdateKeywordRule(string accessTokenOrAppKey,
            ChatDataKeywordRuleUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateKeywordRulePath, request, timeOut);

        /// <summary>异步更新敏感关键词规则。</summary>
        public static Task<WorkJsonResult> UpdateKeywordRuleAsync(string accessTokenOrAppKey,
            ChatDataKeywordRuleUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey,
                UpdateKeywordRulePath, request, timeOut);

        /// <summary>删除敏感关键词规则。</summary>
        public static WorkJsonResult DeleteKeywordRule(string accessTokenOrAppKey,
            ChatDataKeywordRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteKeywordRulePath, request, timeOut);

        /// <summary>异步删除敏感关键词规则。</summary>
        public static Task<WorkJsonResult> DeleteKeywordRuleAsync(string accessTokenOrAppKey,
            ChatDataKeywordRuleIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey,
                DeleteKeywordRulePath, request, timeOut);

        /// <summary>分页获取敏感关键词规则列表。</summary>
        public static ChatDataKeywordRuleListResult GetKeywordRuleList(string accessTokenOrAppKey,
            ChatDataKeywordRuleListRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataKeywordRuleListResult>(accessTokenOrAppKey,
                GetKeywordRuleListPath, request, timeOut);

        /// <summary>异步分页获取敏感关键词规则列表。</summary>
        public static Task<ChatDataKeywordRuleListResult> GetKeywordRuleListAsync(
            string accessTokenOrAppKey, ChatDataKeywordRuleListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataKeywordRuleListResult>(accessTokenOrAppKey,
                GetKeywordRuleListPath, request, timeOut);

        /// <summary>获取敏感关键词规则详情。</summary>
        public static ChatDataKeywordRuleDetailResult GetKeywordRuleDetail(
            string accessTokenOrAppKey, ChatDataKeywordRuleIdRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ChatDataKeywordRuleDetailResult>(accessTokenOrAppKey,
                GetKeywordRuleDetailPath, request, timeOut);

        /// <summary>异步获取敏感关键词规则详情。</summary>
        public static Task<ChatDataKeywordRuleDetailResult> GetKeywordRuleDetailAsync(
            string accessTokenOrAppKey, ChatDataKeywordRuleIdRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataKeywordRuleDetailResult>(accessTokenOrAppKey,
                GetKeywordRuleDetailPath, request, timeOut);

        /// <summary>分页获取命中敏感关键词规则的消息。</summary>
        public static ChatDataKeywordHitMessageListResult GetKeywordHitMessageList(
            string accessTokenOrAppKey, ChatDataKeywordHitMessageListRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<ChatDataKeywordHitMessageListResult>(accessTokenOrAppKey,
                GetKeywordHitMessageListPath, request, timeOut);

        /// <summary>异步分页获取命中敏感关键词规则的消息。</summary>
        public static Task<ChatDataKeywordHitMessageListResult> GetKeywordHitMessageListAsync(
            string accessTokenOrAppKey, ChatDataKeywordHitMessageListRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataKeywordHitMessageListResult>(accessTokenOrAppKey,
                GetKeywordHitMessageListPath, request, timeOut);

        /// <summary>同步调用专区程序能力。</summary>
        public static ChatDataProgramCallResult SyncCallProgram(string accessTokenOrAppKey,
            ChatDataProgramCallRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataProgramCallResult>(accessTokenOrAppKey, SyncCallProgramPath, request, timeOut);

        /// <summary>异步执行“同步调用专区程序能力”请求。</summary>
        public static Task<ChatDataProgramCallResult> SyncCallProgramAsync(string accessTokenOrAppKey,
            ChatDataProgramCallRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataProgramCallResult>(accessTokenOrAppKey,
                SyncCallProgramPath, request, timeOut);

        /// <summary>创建异步调用专区程序的任务。</summary>
        public static ChatDataProgramTaskResult CreateAsyncProgramTask(string accessTokenOrAppKey,
            ChatDataProgramTaskRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataProgramTaskResult>(accessTokenOrAppKey,
                CreateAsyncProgramTaskPath, request, timeOut);

        /// <summary>异步创建调用专区程序的任务。</summary>
        public static Task<ChatDataProgramTaskResult> CreateAsyncProgramTaskAsync(
            string accessTokenOrAppKey, ChatDataProgramTaskRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataProgramTaskResult>(accessTokenOrAppKey,
                CreateAsyncProgramTaskPath, request, timeOut);

        /// <summary>获取异步专区程序任务结果。</summary>
        public static ChatDataProgramResult GetAsyncProgramResult(string accessTokenOrAppKey,
            ChatDataProgramResultRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataProgramResult>(accessTokenOrAppKey,
                GetAsyncProgramResultPath, request, timeOut);

        /// <summary>异步获取专区程序任务结果。</summary>
        public static Task<ChatDataProgramResult> GetAsyncProgramResultAsync(
            string accessTokenOrAppKey, ChatDataProgramResultRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataProgramResult>(accessTokenOrAppKey,
                GetAsyncProgramResultPath, request, timeOut);

        /// <summary>按关键词搜索群聊。</summary>
        public static ChatDataSearchChatResult SearchChat(string accessTokenOrAppKey,
            ChatDataSearchChatRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataSearchChatResult>(accessTokenOrAppKey, SearchChatPath, request, timeOut);

        /// <summary>异步按关键词搜索群聊。</summary>
        public static Task<ChatDataSearchChatResult> SearchChatAsync(string accessTokenOrAppKey,
            ChatDataSearchChatRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataSearchChatResult>(accessTokenOrAppKey,
                SearchChatPath, request, timeOut);

        /// <summary>按关键词和会话范围搜索消息。</summary>
        public static ChatDataSearchMessageResult SearchMessage(string accessTokenOrAppKey,
            ChatDataSearchMessageRequest request, int timeOut = Config.TIME_OUT)
            => Post<ChatDataSearchMessageResult>(accessTokenOrAppKey,
                SearchMessagePath, request, timeOut);

        /// <summary>异步按关键词和会话范围搜索消息。</summary>
        public static Task<ChatDataSearchMessageResult> SearchMessageAsync(
            string accessTokenOrAppKey, ChatDataSearchMessageRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ChatDataSearchMessageResult>(accessTokenOrAppKey,
                SearchMessagePath, request, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
    }
}
