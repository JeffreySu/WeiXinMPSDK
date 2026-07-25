/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AdvancedFeatureApi.cs
    文件功能描述：企业微信高级功能成员申请接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐高级功能成员申请接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.AdvancedFeature
{
    /// <summary>
    /// 企业微信高级功能成员申请接口。
    /// </summary>
    public static class AdvancedFeatureApi
    {
        private const string SetApprovalDetailPath = "/cgi-bin/advanced_feature/set_approval_detail";
        private const string GetApplyIdListPath = "/cgi-bin/advanced_feature/get_apply_id_list";
        private const string GetApprovalInfoPath = "/cgi-bin/advanced_feature/get_approval_info";

        /// <summary>
        /// 设置高级功能申请单的审批信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">审批单状态、跳转地址及全量审批节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult SetApprovalDetail(string accessTokenOrAppKey,
            SetAdvancedFeatureApprovalDetailRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetApprovalDetailPath, request, timeOut);

        /// <summary>
        /// 异步设置高级功能申请单的审批信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">审批单状态、跳转地址及全量审批节点。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> SetApprovalDetailAsync(string accessTokenOrAppKey,
            SetAdvancedFeatureApprovalDetailRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetApprovalDetailPath, request, timeOut);

        /// <summary>
        /// 批量获取高级功能申请单 ID。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员、业务类型和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单 ID 列表及分页信息。</returns>
        public static GetAdvancedFeatureApplyIdListResult GetApplyIdList(string accessTokenOrAppKey,
            GetAdvancedFeatureApplyIdListRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetAdvancedFeatureApplyIdListResult>(accessTokenOrAppKey, GetApplyIdListPath, request, timeOut);

        /// <summary>
        /// 异步批量获取高级功能申请单 ID。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">成员、业务类型和分页条件。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请单 ID 列表及分页信息。</returns>
        public static Task<GetAdvancedFeatureApplyIdListResult> GetApplyIdListAsync(string accessTokenOrAppKey,
            GetAdvancedFeatureApplyIdListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetAdvancedFeatureApplyIdListResult>(accessTokenOrAppKey, GetApplyIdListPath, request, timeOut);

        /// <summary>
        /// 获取高级功能申请单详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含申请 ID 的请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高级功能申请详情。</returns>
        public static GetAdvancedFeatureApprovalInfoResult GetApprovalInfo(string accessTokenOrAppKey,
            GetAdvancedFeatureApprovalInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetAdvancedFeatureApprovalInfoResult>(accessTokenOrAppKey, GetApprovalInfoPath, request, timeOut);

        /// <summary>
        /// 异步获取高级功能申请单详细信息。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">包含申请 ID 的请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>高级功能申请详情。</returns>
        public static Task<GetAdvancedFeatureApprovalInfoResult> GetApprovalInfoAsync(string accessTokenOrAppKey,
            GetAdvancedFeatureApprovalInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetAdvancedFeatureApprovalInfoResult>(accessTokenOrAppKey, GetApprovalInfoPath, request, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
