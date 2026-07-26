/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeChatCustomerServiceApi.cs
    文件功能描述：WeChatCustomerServiceApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口；增加企业微信客服企业资质查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService
{
    /// <summary>
    /// 新版微信客服 API。
    /// </summary>
    public static partial class WeChatCustomerServiceApi
    {
        /// <summary>
        /// 添加微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfAccountAddResult AddAccount(string token, KfAccountAddRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfAccountAddResult>(token, "/cgi-bin/kf/account/add", request, timeOut);
        /// <summary>
        /// 异步添加微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfAccountAddResult> AddAccountAsync(string token, KfAccountAddRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfAccountAddResult>(token, "/cgi-bin/kf/account/add", request, timeOut);

        /// <summary>
        /// 删除微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult DeleteAccount(string token, KfAccountDeleteRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/account/del", request, timeOut);
        /// <summary>
        /// 异步删除微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> DeleteAccountAsync(string token, KfAccountDeleteRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/account/del", request, timeOut);

        /// <summary>
        /// 更新微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpdateAccount(string token, KfAccountUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/account/update", request, timeOut);
        /// <summary>
        /// 异步更新微信客服账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpdateAccountAsync(string token, KfAccountUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/account/update", request, timeOut);

        /// <summary>
        /// 获取微信客服账号列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfAccountListResult GetAccountList(string token, KfAccountListRequest request = null, int timeOut = Config.TIME_OUT)
            => Post<KfAccountListResult>(token, "/cgi-bin/kf/account/list", request ?? new KfAccountListRequest(), timeOut);
        /// <summary>
        /// 异步获取微信客服账号列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfAccountListResult> GetAccountListAsync(string token, KfAccountListRequest request = null, int timeOut = Config.TIME_OUT)
            => PostAsync<KfAccountListResult>(token, "/cgi-bin/kf/account/list", request ?? new KfAccountListRequest(), timeOut);

        /// <summary>
        /// 配置客户联系「联系我」方式。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfContactWayResult AddContactWay(string token, KfContactWayRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfContactWayResult>(token, "/cgi-bin/kf/add_contact_way", request, timeOut);
        /// <summary>
        /// 异步配置客户联系「联系我」方式。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfContactWayResult> AddContactWayAsync(string token, KfContactWayRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfContactWayResult>(token, "/cgi-bin/kf/add_contact_way", request, timeOut);

        /// <summary>
        /// 添加接待人员。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfServicerChangeResult AddServicers(string token, KfServicerChangeRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfServicerChangeResult>(token, "/cgi-bin/kf/servicer/add", request, timeOut);
        /// <summary>
        /// 异步添加接待人员。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfServicerChangeResult> AddServicersAsync(string token, KfServicerChangeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfServicerChangeResult>(token, "/cgi-bin/kf/servicer/add", request, timeOut);

        /// <summary>
        /// 删除接待人员。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfServicerChangeResult DeleteServicers(string token, KfServicerChangeRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfServicerChangeResult>(token, "/cgi-bin/kf/servicer/del", request, timeOut);
        /// <summary>
        /// 异步删除接待人员。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfServicerChangeResult> DeleteServicersAsync(string token, KfServicerChangeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfServicerChangeResult>(token, "/cgi-bin/kf/servicer/del", request, timeOut);

        /// <summary>
        /// 获取接待人员列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="openKfId">微信客服账号 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfServicerListResult GetServicerList(string token, string openKfId, int timeOut = Config.TIME_OUT)
            => Get<KfServicerListResult>(token, "/cgi-bin/kf/servicer/list", "&open_kfid=" + openKfId.AsUrlData(), timeOut);
        /// <summary>
        /// 异步获取接待人员列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="openKfId">微信客服账号 ID。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfServicerListResult> GetServicerListAsync(string token, string openKfId, int timeOut = Config.TIME_OUT)
            => GetAsync<KfServicerListResult>(token, "/cgi-bin/kf/servicer/list", "&open_kfid=" + openKfId.AsUrlData(), timeOut);

        /// <summary>
        /// 获取微信客服会话状态。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfServiceStateResult GetServiceState(string token, KfServiceStateRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfServiceStateResult>(token, "/cgi-bin/kf/service_state/get", request, timeOut);
        /// <summary>
        /// 异步获取微信客服会话状态。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfServiceStateResult> GetServiceStateAsync(string token, KfServiceStateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfServiceStateResult>(token, "/cgi-bin/kf/service_state/get", request, timeOut);

        /// <summary>
        /// 变更微信客服会话状态。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfServiceStateTransferResult TransferServiceState(string token, KfServiceStateTransferRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfServiceStateTransferResult>(token, "/cgi-bin/kf/service_state/trans", request, timeOut);
        /// <summary>
        /// 异步变更微信客服会话状态。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfServiceStateTransferResult> TransferServiceStateAsync(string token, KfServiceStateTransferRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfServiceStateTransferResult>(token, "/cgi-bin/kf/service_state/trans", request, timeOut);

        /// <summary>
        /// 同步微信客服消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfSyncMessageResult SyncMessages(string token, KfSyncMessageRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfSyncMessageResult>(token, "/cgi-bin/kf/sync_msg", request, timeOut);
        /// <summary>
        /// 异步同步微信客服消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfSyncMessageResult> SyncMessagesAsync(string token, KfSyncMessageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfSyncMessageResult>(token, "/cgi-bin/kf/sync_msg", request, timeOut);

        /// <summary>
        /// 发送智能机器人主动消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfSendMessageResult SendMessage(string token, KfSendMessageRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfSendMessageResult>(token, "/cgi-bin/kf/send_msg", request, timeOut);
        /// <summary>
        /// 异步发送智能机器人主动消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfSendMessageResult> SendMessageAsync(string token, KfSendMessageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfSendMessageResult>(token, "/cgi-bin/kf/send_msg", request, timeOut);

        /// <summary>
        /// 发送微信客服事件响应消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfSendMessageResult SendMessageOnEvent(string token, KfSendEventMessageRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfSendMessageResult>(token, "/cgi-bin/kf/send_msg_on_event", request, timeOut);
        /// <summary>
        /// 异步发送微信客服事件响应消息。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfSendMessageResult> SendMessageOnEventAsync(string token, KfSendEventMessageRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfSendMessageResult>(token, "/cgi-bin/kf/send_msg_on_event", request, timeOut);

        /// <summary>
        /// 获取微信客服升级服务配置。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfUpgradeServiceConfigResult GetUpgradeServiceConfig(string token, int timeOut = Config.TIME_OUT)
            => Get<KfUpgradeServiceConfigResult>(token, "/cgi-bin/kf/customer/get_upgrade_service_config", null, timeOut);
        /// <summary>
        /// 异步获取微信客服升级服务配置。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfUpgradeServiceConfigResult> GetUpgradeServiceConfigAsync(string token, int timeOut = Config.TIME_OUT)
            => GetAsync<KfUpgradeServiceConfigResult>(token, "/cgi-bin/kf/customer/get_upgrade_service_config", null, timeOut);

        /// <summary>
        /// 为微信客服客户升级服务。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpgradeService(string token, KfUpgradeServiceRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/customer/upgrade_service", request, timeOut);
        /// <summary>
        /// 异步为微信客服客户升级服务。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpgradeServiceAsync(string token, KfUpgradeServiceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/customer/upgrade_service", request, timeOut);

        /// <summary>
        /// 取消微信客服升级服务。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult CancelUpgradeService(string token, KfServiceStateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/customer/cancel_upgrade_service", request, timeOut);
        /// <summary>
        /// 异步取消微信客服升级服务。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> CancelUpgradeServiceAsync(string token, KfServiceStateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/customer/cancel_upgrade_service", request, timeOut);

        /// <summary>
        /// 批量获取获客助手客户详情。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfBatchCustomerResult BatchGetCustomers(string token, KfBatchCustomerRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfBatchCustomerResult>(token, "/cgi-bin/kf/customer/batchget", request, timeOut);
        /// <summary>
        /// 异步批量获取获客助手客户详情。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfBatchCustomerResult> BatchGetCustomersAsync(string token, KfBatchCustomerRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfBatchCustomerResult>(token, "/cgi-bin/kf/customer/batchget", request, timeOut);

        /// <summary>
        /// 获取微信客服企业统计数据。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfStatisticResult GetCorpStatistic(string token, KfStatisticRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfStatisticResult>(token, "/cgi-bin/kf/get_corp_statistic", request, timeOut);
        /// <summary>
        /// 异步获取微信客服企业统计数据。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfStatisticResult> GetCorpStatisticAsync(string token, KfStatisticRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfStatisticResult>(token, "/cgi-bin/kf/get_corp_statistic", request, timeOut);

        /// <summary>
        /// 获取微信客服接待人员统计数据。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfStatisticResult GetServicerStatistic(string token, KfServicerStatisticRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfStatisticResult>(token, "/cgi-bin/kf/get_servicer_statistic", request, timeOut);
        /// <summary>
        /// 异步获取微信客服接待人员统计数据。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfStatisticResult> GetServicerStatisticAsync(string token, KfServicerStatisticRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfStatisticResult>(token, "/cgi-bin/kf/get_servicer_statistic", request, timeOut);

        /// <summary>
        /// 添加微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfKnowledgeGroupResult AddKnowledgeGroup(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfKnowledgeGroupResult>(token, "/cgi-bin/kf/knowledge/add_group", request, timeOut);
        /// <summary>
        /// 异步添加微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfKnowledgeGroupResult> AddKnowledgeGroupAsync(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfKnowledgeGroupResult>(token, "/cgi-bin/kf/knowledge/add_group", request, timeOut);

        /// <summary>
        /// 删除微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult DeleteKnowledgeGroup(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/del_group", request, timeOut);
        /// <summary>
        /// 异步删除微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> DeleteKnowledgeGroupAsync(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/del_group", request, timeOut);

        /// <summary>
        /// 更新微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpdateKnowledgeGroup(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/mod_group", request, timeOut);
        /// <summary>
        /// 异步更新微信客服知识库分组。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpdateKnowledgeGroupAsync(string token, KfKnowledgeGroupRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/mod_group", request, timeOut);

        /// <summary>
        /// 获取微信客服知识库分组列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfKnowledgeGroupListResult GetKnowledgeGroupList(string token, KfKnowledgeGroupListRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfKnowledgeGroupListResult>(token, "/cgi-bin/kf/knowledge/list_group", request, timeOut);
        /// <summary>
        /// 异步获取微信客服知识库分组列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfKnowledgeGroupListResult> GetKnowledgeGroupListAsync(string token, KfKnowledgeGroupListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfKnowledgeGroupListResult>(token, "/cgi-bin/kf/knowledge/list_group", request, timeOut);

        /// <summary>
        /// 添加微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfKnowledgeIntentResult AddKnowledgeIntent(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfKnowledgeIntentResult>(token, "/cgi-bin/kf/knowledge/add_intent", request, timeOut);
        /// <summary>
        /// 异步添加微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfKnowledgeIntentResult> AddKnowledgeIntentAsync(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfKnowledgeIntentResult>(token, "/cgi-bin/kf/knowledge/add_intent", request, timeOut);

        /// <summary>
        /// 删除微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult DeleteKnowledgeIntent(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/del_intent", request, timeOut);
        /// <summary>
        /// 异步删除微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> DeleteKnowledgeIntentAsync(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/del_intent", request, timeOut);

        /// <summary>
        /// 更新微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult UpdateKnowledgeIntent(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/mod_intent", request, timeOut);
        /// <summary>
        /// 异步更新微信客服知识库问法。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> UpdateKnowledgeIntentAsync(string token, KfKnowledgeIntentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/kf/knowledge/mod_intent", request, timeOut);

        /// <summary>
        /// 获取微信客服知识库问法列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static KfKnowledgeIntentListResult GetKnowledgeIntentList(string token, KfKnowledgeIntentListRequest request, int timeOut = Config.TIME_OUT)
            => Post<KfKnowledgeIntentListResult>(token, "/cgi-bin/kf/knowledge/list_intent", request, timeOut);
        /// <summary>
        /// 异步获取微信客服知识库问法列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<KfKnowledgeIntentListResult> GetKnowledgeIntentListAsync(string token, KfKnowledgeIntentListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<KfKnowledgeIntentListResult>(token, "/cgi-bin/kf/knowledge/list_intent", request, timeOut);

        private static T Post<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);

        private static Task<T> PostAsync<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);

        private static T Get<T>(string token, string path, string query, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(null,
                Config.ApiWorkHost + path + "?access_token=" + accessToken.AsUrlData() + query,
                null, CommonJsonSendType.GET, timeOut), token);

        private static Task<T> GetAsync<T>(string token, string path, string query, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(null,
                Config.ApiWorkHost + path + "?access_token=" + accessToken.AsUrlData() + query,
                null, CommonJsonSendType.GET, timeOut), token);
    }
}
