/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：HumanResourcesApi.cs
    文件功能描述：企业微信人事助手花名册接口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐人事助手花名册接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.HumanResources
{
    /// <summary>企业微信人事助手花名册接口。</summary>
    public static class HumanResourcesApi
    {
        private const string GetFieldsPath = "/cgi-bin/hr/get_fields";
        private const string GetStaffInfoPath = "/cgi-bin/hr/get_staff_info";
        private const string UpdateStaffInfoPath = "/cgi-bin/hr/update_staff_info";

        /// <summary>获取员工花名册字段配置。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>字段组、字段及选项配置。</returns>
        public static GetStaffFieldsResult GetFields(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Get<GetStaffFieldsResult>(accessTokenOrAppKey, GetFieldsPath, timeOut);

        /// <summary>异步获取员工花名册字段配置。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>字段组、字段及选项配置。</returns>
        public static Task<GetStaffFieldsResult> GetFieldsAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetAsync<GetStaffFieldsResult>(accessTokenOrAppKey, GetFieldsPath, timeOut);

        /// <summary>获取指定员工的花名册信息。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">员工 UserId、全部字段标志或指定字段列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>员工花名册字段值。</returns>
        public static GetStaffInfoResult GetStaffInfo(string accessTokenOrAppKey,
            GetStaffInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<GetStaffInfoResult>(accessTokenOrAppKey, GetStaffInfoPath, request, timeOut);

        /// <summary>异步获取指定员工的花名册信息。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">员工 UserId、全部字段标志或指定字段列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>员工花名册字段值。</returns>
        public static Task<GetStaffInfoResult> GetStaffInfoAsync(string accessTokenOrAppKey,
            GetStaffInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<GetStaffInfoResult>(accessTokenOrAppKey, GetStaffInfoPath, request, timeOut);

        /// <summary>更新指定员工的花名册字段、删除重复字段组或插入重复字段组。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">员工 UserId 和至少一项更新、删除或插入操作。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各项更新结果。</returns>
        public static UpdateStaffInfoResult UpdateStaffInfo(string accessTokenOrAppKey,
            UpdateStaffInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<UpdateStaffInfoResult>(accessTokenOrAppKey, UpdateStaffInfoPath, request, timeOut);

        /// <summary>异步更新指定员工的花名册字段、删除重复字段组或插入重复字段组。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">员工 UserId 和至少一项更新、删除或插入操作。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各项更新结果。</returns>
        public static Task<UpdateStaffInfoResult> UpdateStaffInfoAsync(string accessTokenOrAppKey,
            UpdateStaffInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<UpdateStaffInfoResult>(accessTokenOrAppKey, UpdateStaffInfoPath, request, timeOut);

        private static T Get<T>(string accessTokenOrAppKey, string path, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

        private static Task<T> GetAsync<T>(string accessTokenOrAppKey, string path, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", null, CommonJsonSendType.GET, timeOut),
                accessTokenOrAppKey);

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
