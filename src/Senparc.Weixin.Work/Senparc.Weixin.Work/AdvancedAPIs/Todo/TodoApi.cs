/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TodoApi.cs
    文件功能描述：企业微信待办接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐待办详情与状态更新接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Todo
{
    /// <summary>
    /// 企业微信待办接口。
    /// </summary>
    public static class TodoApi
    {
        private const string GetTodoPath = "/cgi-bin/todo/get";
        private const string UpdateTodoPath = "/cgi-bin/todo/update";

        /// <summary>
        /// 获取指定待办的详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">获取待办详情请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>待办详情。</returns>
        public static GetTodoResult GetTodo(string accessTokenOrAppKey, GetTodoRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<GetTodoResult>(accessTokenOrAppKey, GetTodoPath, request, timeOut);

        /// <summary>
        /// 异步获取指定待办的详情。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">获取待办详情请求。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>待办详情。</returns>
        public static Task<GetTodoResult> GetTodoAsync(string accessTokenOrAppKey, GetTodoRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<GetTodoResult>(accessTokenOrAppKey, GetTodoPath, request, timeOut);

        /// <summary>
        /// 更新指定待办的整体状态、参与人或参与人状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">更新待办请求；参与人最多 20 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static WorkJsonResult UpdateTodo(string accessTokenOrAppKey, UpdateTodoRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateTodoPath, request, timeOut);

        /// <summary>
        /// 异步更新指定待办的整体状态、参与人或参与人状态。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">更新待办请求；参与人最多 20 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>接口执行结果。</returns>
        public static Task<WorkJsonResult> UpdateTodoAsync(string accessTokenOrAppKey, UpdateTodoRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateTodoPath, request, timeOut);

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
