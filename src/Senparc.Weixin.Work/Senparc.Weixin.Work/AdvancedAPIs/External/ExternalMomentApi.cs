/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalMomentApi.cs
    文件功能描述：企业客户朋友圈发表、互动与规则组接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增客户朋友圈发表、互动与规则组接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>企业客户朋友圈补充接口。</summary>
    public static partial class ExternalApi
    {
        /// <summary>创建企业客户朋友圈发表任务。</summary>
        public static CreateMomentTaskResult CreateMomentTask(string accessTokenOrAppKey,
            CreateMomentTaskRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<CreateMomentTaskResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_moment_task", request, timeOut);

        /// <summary>异步创建企业客户朋友圈发表任务。</summary>
        public static Task<CreateMomentTaskResult> CreateMomentTaskAsync(string accessTokenOrAppKey,
            CreateMomentTaskRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<CreateMomentTaskResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/add_moment_task", request, timeOut);

        /// <summary>获取企业客户朋友圈发表任务创建结果。</summary>
        public static MomentTaskCreateStatusResult GetMomentTaskCreateResult(string accessTokenOrAppKey,
            string jobId, int timeOut = Config.TIME_OUT)
            => GetP1<MomentTaskCreateStatusResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_task_result", "&jobid=" + jobId.AsUrlData(), timeOut);

        /// <summary>异步获取企业客户朋友圈发表任务创建结果。</summary>
        public static Task<MomentTaskCreateStatusResult> GetMomentTaskCreateResultAsync(string accessTokenOrAppKey,
            string jobId, int timeOut = Config.TIME_OUT)
            => GetP1Async<MomentTaskCreateStatusResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_task_result", "&jobid=" + jobId.AsUrlData(), timeOut);

        /// <summary>获取客户朋友圈创建时选择的可见客户范围。</summary>
        public static MomentCustomerListResult GetMomentCustomerList(string accessTokenOrAppKey,
            MomentCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_customer_list", request, timeOut);

        /// <summary>异步获取客户朋友圈创建时选择的可见客户范围。</summary>
        public static Task<MomentCustomerListResult> GetMomentCustomerListAsync(string accessTokenOrAppKey,
            MomentCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_customer_list", request, timeOut);

        /// <summary>获取客户朋友圈发表后的可见客户列表。</summary>
        public static MomentCustomerListResult GetMomentSendResult(string accessTokenOrAppKey,
            MomentCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_send_result", request, timeOut);

        /// <summary>异步获取客户朋友圈发表后的可见客户列表。</summary>
        public static Task<MomentCustomerListResult> GetMomentSendResultAsync(string accessTokenOrAppKey,
            MomentCustomerListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentCustomerListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_send_result", request, timeOut);

        /// <summary>获取客户朋友圈评论和点赞数据。</summary>
        public static MomentCommentsResult GetMomentComments(string accessTokenOrAppKey,
            MomentCommentsRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentCommentsResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_comments", request, timeOut);

        /// <summary>异步获取客户朋友圈评论和点赞数据。</summary>
        public static Task<MomentCommentsResult> GetMomentCommentsAsync(string accessTokenOrAppKey,
            MomentCommentsRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentCommentsResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/get_moment_comments", request, timeOut);

        /// <summary>获取客户朋友圈规则组列表。</summary>
        public static MomentStrategyListResult ListMomentStrategies(string accessTokenOrAppKey,
            MomentStrategyListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentStrategyListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/list", request, timeOut);

        /// <summary>异步获取客户朋友圈规则组列表。</summary>
        public static Task<MomentStrategyListResult> ListMomentStrategiesAsync(string accessTokenOrAppKey,
            MomentStrategyListRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentStrategyListResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/list", request, timeOut);

        /// <summary>获取客户朋友圈规则组详情。</summary>
        public static MomentStrategyResult GetMomentStrategy(string accessTokenOrAppKey,
            MomentStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentStrategyResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/get", request, timeOut);

        /// <summary>异步获取客户朋友圈规则组详情。</summary>
        public static Task<MomentStrategyResult> GetMomentStrategyAsync(string accessTokenOrAppKey,
            MomentStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentStrategyResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/get", request, timeOut);

        /// <summary>获取客户朋友圈规则组管理范围。</summary>
        public static MomentStrategyRangeResult GetMomentStrategyRange(string accessTokenOrAppKey,
            MomentStrategyRangeRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentStrategyRangeResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/get_range", request, timeOut);

        /// <summary>异步获取客户朋友圈规则组管理范围。</summary>
        public static Task<MomentStrategyRangeResult> GetMomentStrategyRangeAsync(string accessTokenOrAppKey,
            MomentStrategyRangeRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentStrategyRangeResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/get_range", request, timeOut);

        /// <summary>创建客户朋友圈规则组。</summary>
        public static MomentStrategyCreateResult CreateMomentStrategy(string accessTokenOrAppKey,
            MomentStrategyCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<MomentStrategyCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/create", request, timeOut);

        /// <summary>异步创建客户朋友圈规则组。</summary>
        public static Task<MomentStrategyCreateResult> CreateMomentStrategyAsync(string accessTokenOrAppKey,
            MomentStrategyCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<MomentStrategyCreateResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/create", request, timeOut);

        /// <summary>编辑客户朋友圈规则组及其管理范围。</summary>
        public static WorkJsonResult EditMomentStrategy(string accessTokenOrAppKey,
            MomentStrategyEditRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/edit", request, timeOut);

        /// <summary>异步编辑客户朋友圈规则组及其管理范围。</summary>
        public static Task<WorkJsonResult> EditMomentStrategyAsync(string accessTokenOrAppKey,
            MomentStrategyEditRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/edit", request, timeOut);

        /// <summary>删除客户朋友圈规则组。</summary>
        public static WorkJsonResult DeleteMomentStrategy(string accessTokenOrAppKey,
            MomentStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/del", request, timeOut);

        /// <summary>异步删除客户朋友圈规则组。</summary>
        public static Task<WorkJsonResult> DeleteMomentStrategyAsync(string accessTokenOrAppKey,
            MomentStrategyIdRequest request, int timeOut = Config.TIME_OUT)
            => PostP1Async<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/externalcontact/moment_strategy/del", request, timeOut);

        private static T GetP1<T>(string accessTokenOrAppKey, string path, string query, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(null,
                Config.ApiWorkHost + path + "?access_token=" + accessToken.AsUrlData() + query,
                null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        private static Task<T> GetP1Async<T>(string accessTokenOrAppKey, string path, string query, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(null,
                Config.ApiWorkHost + path + "?access_token=" + accessToken.AsUrlData() + query,
                null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);
    }
}
