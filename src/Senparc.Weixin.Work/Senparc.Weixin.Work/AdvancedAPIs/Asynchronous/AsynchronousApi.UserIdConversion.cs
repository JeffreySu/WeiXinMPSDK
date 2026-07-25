/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AsynchronousApi.UserIdConversion.cs
    文件功能描述：企业微信批量成员 ID 转 OpenUserId 接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加批量成员 ID 转 OpenUserId 同步与异步入口

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
        private const string BatchUserIdToOpenUserIdPath = "/cgi-bin/batch/userid_to_openuserid";

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
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

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
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);
    }
}
