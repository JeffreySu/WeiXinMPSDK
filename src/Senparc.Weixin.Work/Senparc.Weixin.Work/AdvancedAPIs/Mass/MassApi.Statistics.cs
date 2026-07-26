/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MassApi.Statistics.cs
    文件功能描述：企业微信应用消息发送统计接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加应用消息发送统计同步与异步入口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 应用消息统计接口扩展。
    /// </summary>
    public static partial class MassApi
    {
        private const string GetMessageStatisticsPath = "/cgi-bin/message/get_statistics";

        /// <summary>
        /// 获取企业应用消息发送成功人次统计。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92369">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">查询时间类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各应用的消息发送成功人次。</returns>
        public static MessageStatisticsResult GetMessageStatistics(string accessTokenOrAppKey,
            MessageStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<MessageStatisticsResult>(
                accessToken, Config.ApiWorkHost + GetMessageStatisticsPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取企业应用消息发送成功人次统计。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92369">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="request">查询时间类型。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>各应用的消息发送成功人次。</returns>
        public static Task<MessageStatisticsResult> GetMessageStatisticsAsync(string accessTokenOrAppKey,
            MessageStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<MessageStatisticsResult>(
                accessToken, Config.ApiWorkHost + GetMessageStatisticsPath + "?access_token={0}", request,
                CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true)), accessTokenOrAppKey);
    }
}
