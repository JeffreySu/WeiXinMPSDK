/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalCustomerManagementApi.cs
    文件功能描述：客户联系规则组与客户继承接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户联系规则组与客户继承接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>企业微信客户联系规则组与客户继承接口。</summary>
    public static partial class ExternalApi
    {
        /// <summary>获取客户联系规则组列表。</summary>
        public static CustomerStrategyListResult ListCustomerStrategies(string accessTokenOrAppKey,
            CustomerStrategyListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerStrategyListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/list", request, timeOut);

        /// <summary>异步获取客户联系规则组列表。</summary>
        public static Task<CustomerStrategyListResult> ListCustomerStrategiesAsync(string accessTokenOrAppKey,
            CustomerStrategyListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerStrategyListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/list", request, timeOut);

        /// <summary>获取客户联系规则组详情。</summary>
        public static CustomerStrategyResult GetCustomerStrategy(string accessTokenOrAppKey,
            CustomerStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerStrategyResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/get", request, timeOut);

        /// <summary>异步获取客户联系规则组详情。</summary>
        public static Task<CustomerStrategyResult> GetCustomerStrategyAsync(string accessTokenOrAppKey,
            CustomerStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerStrategyResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/get", request, timeOut);

        /// <summary>获取客户联系规则组管理范围。</summary>
        public static CustomerStrategyRangeResult GetCustomerStrategyRange(string accessTokenOrAppKey,
            CustomerStrategyRangeRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerStrategyRangeResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/get_range", request, timeOut);

        /// <summary>异步获取客户联系规则组管理范围。</summary>
        public static Task<CustomerStrategyRangeResult> GetCustomerStrategyRangeAsync(string accessTokenOrAppKey,
            CustomerStrategyRangeRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerStrategyRangeResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/get_range", request, timeOut);

        /// <summary>创建客户联系规则组。</summary>
        public static CustomerStrategyCreateResult CreateCustomerStrategy(string accessTokenOrAppKey,
            CustomerStrategyCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerStrategyCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/create", request, timeOut);

        /// <summary>异步创建客户联系规则组。</summary>
        public static Task<CustomerStrategyCreateResult> CreateCustomerStrategyAsync(string accessTokenOrAppKey,
            CustomerStrategyCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerStrategyCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/create", request, timeOut);

        /// <summary>编辑客户联系规则组及其管理范围。</summary>
        public static WorkJsonResult EditCustomerStrategy(string accessTokenOrAppKey,
            CustomerStrategyEditRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/edit", request, timeOut);

        /// <summary>异步编辑客户联系规则组及其管理范围。</summary>
        public static Task<WorkJsonResult> EditCustomerStrategyAsync(string accessTokenOrAppKey,
            CustomerStrategyEditRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/edit", request, timeOut);

        /// <summary>删除客户联系规则组。</summary>
        public static WorkJsonResult DeleteCustomerStrategy(string accessTokenOrAppKey,
            CustomerStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/del", request, timeOut);

        /// <summary>异步删除客户联系规则组。</summary>
        public static Task<WorkJsonResult> DeleteCustomerStrategyAsync(string accessTokenOrAppKey,
            CustomerStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/customer_strategy/del", request, timeOut);

        /// <summary>分配在职成员的客户。</summary>
        public static CustomerTransferResult TransferOnJobCustomers(string accessTokenOrAppKey,
            OnJobCustomerTransferRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerTransferResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/transfer_customer", request, timeOut);

        /// <summary>异步分配在职成员的客户。</summary>
        public static Task<CustomerTransferResult> TransferOnJobCustomersAsync(string accessTokenOrAppKey,
            OnJobCustomerTransferRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerTransferResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/transfer_customer", request, timeOut);

        /// <summary>查询在职成员客户接替状态。</summary>
        public static CustomerTransferQueryResult GetOnJobCustomerTransferResult(string accessTokenOrAppKey,
            CustomerTransferQueryRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerTransferQueryResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/transfer_result", request, timeOut);

        /// <summary>异步查询在职成员客户接替状态。</summary>
        public static Task<CustomerTransferQueryResult> GetOnJobCustomerTransferResultAsync(string accessTokenOrAppKey,
            CustomerTransferQueryRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerTransferQueryResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/transfer_result", request, timeOut);

        /// <summary>获取待分配的离职成员客户列表。</summary>
        public static UnassignedCustomerListResult GetUnassignedCustomers(string accessTokenOrAppKey,
            UnassignedCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<UnassignedCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_unassigned_list", request, timeOut);

        /// <summary>异步获取待分配的离职成员客户列表。</summary>
        public static Task<UnassignedCustomerListResult> GetUnassignedCustomersAsync(string accessTokenOrAppKey,
            UnassignedCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<UnassignedCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_unassigned_list", request, timeOut);

        /// <summary>分配离职成员的客户。</summary>
        public static CustomerTransferResult TransferResignedCustomers(string accessTokenOrAppKey,
            ResignedCustomerTransferRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerTransferResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/resigned/transfer_customer", request, timeOut);

        /// <summary>异步分配离职成员的客户。</summary>
        public static Task<CustomerTransferResult> TransferResignedCustomersAsync(string accessTokenOrAppKey,
            ResignedCustomerTransferRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerTransferResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/resigned/transfer_customer", request, timeOut);

        /// <summary>查询离职成员客户接替状态。</summary>
        public static CustomerTransferQueryResult GetResignedCustomerTransferResult(string accessTokenOrAppKey,
            CustomerTransferQueryRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CustomerTransferQueryResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/resigned/transfer_result", request, timeOut);

        /// <summary>异步查询离职成员客户接替状态。</summary>
        public static Task<CustomerTransferQueryResult> GetResignedCustomerTransferResultAsync(
            string accessTokenOrAppKey, CustomerTransferQueryRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CustomerTransferQueryResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/resigned/transfer_result", request, timeOut);

        /// <summary>分配离职成员的客户群。</summary>
        public static OnJobTransferGroupChatResult TransferResignedGroupChats(string accessTokenOrAppKey,
            OnJobTransferGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<OnJobTransferGroupChatResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/groupchat/transfer", request, timeOut);

        /// <summary>异步分配离职成员的客户群。</summary>
        public static Task<OnJobTransferGroupChatResult> TransferResignedGroupChatsAsync(string accessTokenOrAppKey,
            OnJobTransferGroupChatRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<OnJobTransferGroupChatResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/groupchat/transfer", request, timeOut);
    }
}
