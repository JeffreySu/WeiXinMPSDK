/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AsynchronousApi.UserIdConversion.cs
    文件功能描述：企业微信成员与智能机器人 OpenUserId 批量转换接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加成员与智能机器人 OpenUserId 批量转换入口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Asynchronous;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 异步任务接口的成员 ID 转换扩展。
    /// </summary>
    public static partial class AsynchronousApi
    {
        private static readonly JsonSetting UserIdConversionJsonSetting = new JsonSetting(true);

        private const string BatchUserIdToOpenUserIdPath = "/cgi-bin/batch/userid_to_openuserid";
        private const string BatchOpenUserIdToUserIdPath = "/cgi-bin/batch/openuserid_to_userid";
        private const string ServiceBatchUserIdToOpenUserIdPath =
            "/cgi-bin/service/batch/userid_to_openuserid";

        /// <summary>
        /// 批量将企业内部成员 ID 转换为 OpenUserId。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/95435">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待转换的企业内部成员 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功转换的成员映射和无效成员 ID。</returns>
        public static BatchUserIdToOpenUserIdResult BatchUserIdToOpenUserId(string accessTokenOrAppKey,
            BatchUserIdToOpenUserIdRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<BatchUserIdToOpenUserIdResult>(
                accessToken, Config.ApiWorkHost + BatchUserIdToOpenUserIdPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: UserIdConversionJsonSetting),
                accessTokenOrAppKey);

        /// <summary>
        /// 异步批量将企业内部成员 ID 转换为 OpenUserId。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/95435">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待转换的企业内部成员 ID 列表。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功转换的成员映射和无效成员 ID。</returns>
        public static Task<BatchUserIdToOpenUserIdResult> BatchUserIdToOpenUserIdAsync(string accessTokenOrAppKey,
            BatchUserIdToOpenUserIdRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<BatchUserIdToOpenUserIdResult>(
                accessToken, Config.ApiWorkHost + BatchUserIdToOpenUserIdPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: UserIdConversionJsonSetting),
                accessTokenOrAppKey);

        /// <summary>
        /// 批量将企业智能机器人返回的密文 OpenUserId 转换为企业内部成员 ID。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/101521">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业自建应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待转换的机器人密文 OpenUserId 列表，最多 1000 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功转换的 OpenUserId/成员 ID 对应关系及无效 OpenUserId。</returns>
        public static BatchOpenUserIdToUserIdResult BatchOpenUserIdToUserId(
            string accessTokenOrAppKey, BatchOpenUserIdToUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<BatchOpenUserIdToUserIdResult>(accessToken,
                    Config.ApiWorkHost + BatchOpenUserIdToUserIdPath + "?access_token={0}",
                    request, CommonJsonSendType.POST, timeOut,
                    jsonSetting: UserIdConversionJsonSetting), accessTokenOrAppKey);

        /// <summary>
        /// 异步批量将企业智能机器人返回的密文 OpenUserId 转换为企业内部成员 ID。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/101521">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业自建应用调用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">待转换的机器人密文 OpenUserId 列表，最多 1000 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>成功转换的 OpenUserId/成员 ID 对应关系及无效 OpenUserId。</returns>
        public static Task<BatchOpenUserIdToUserIdResult> BatchOpenUserIdToUserIdAsync(
            string accessTokenOrAppKey, BatchOpenUserIdToUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<BatchOpenUserIdToUserIdResult>(accessToken,
                    Config.ApiWorkHost + BatchOpenUserIdToUserIdPath + "?access_token={0}",
                    request, CommonJsonSendType.POST, timeOut,
                    jsonSetting: UserIdConversionJsonSetting), accessTokenOrAppKey);

        /// <summary>
        /// 使用服务商凭证将企业智能机器人范围内的加密成员 ID 转换为服务商范围的 OpenUserId。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97062">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="request">企业侧加密成员 ID 列表及来源机器人 ID，列表最多 1000 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业侧成员 ID、服务商 OpenUserId、机器人所在企业 ID 及无效 ID。</returns>
        public static ServiceBatchUserIdToOpenUserIdResult ServiceBatchUserIdToOpenUserId(
            string providerAccessToken, ServiceBatchUserIdToOpenUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => CommonJsonSend.Send<ServiceBatchUserIdToOpenUserIdResult>(providerAccessToken,
                Config.ApiWorkHost + ServiceBatchUserIdToOpenUserIdPath +
                "?provider_access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: UserIdConversionJsonSetting);

        /// <summary>
        /// 异步使用服务商凭证将企业智能机器人范围内的加密成员 ID 转换为服务商范围的 OpenUserId。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/97062">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="request">企业侧加密成员 ID 列表及来源机器人 ID，列表最多 1000 个。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业侧成员 ID、服务商 OpenUserId、机器人所在企业 ID 及无效 ID。</returns>
        public static Task<ServiceBatchUserIdToOpenUserIdResult> ServiceBatchUserIdToOpenUserIdAsync(
            string providerAccessToken, ServiceBatchUserIdToOpenUserIdRequest request,
            int timeOut = Config.TIME_OUT)
            => CommonJsonSend.SendAsync<ServiceBatchUserIdToOpenUserIdResult>(providerAccessToken,
                Config.ApiWorkHost + ServiceBatchUserIdToOpenUserIdPath +
                "?provider_access_token={0}", request, CommonJsonSendType.POST, timeOut,
                jsonSetting: UserIdConversionJsonSetting);
    }
}
