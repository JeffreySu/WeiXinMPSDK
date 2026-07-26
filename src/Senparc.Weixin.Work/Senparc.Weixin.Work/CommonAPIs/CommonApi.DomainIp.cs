/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CommonApi.DomainIp.cs
    文件功能描述：企业微信接口域名 IP 段接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐获取企业微信接口 IP 段接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.CommonAPIs
{
    /// <summary>
    /// 企业微信通用基础接口。
    /// </summary>
    public partial class CommonApi
    {
        private const string GetApiDomainIpPath = "/cgi-bin/get_api_domain_ip";

        /// <summary>
        /// 获取企业微信 API 域名当前解析到的服务器 IP 段。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/92520"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信 API 服务器 IP 段列表。</returns>
        public static GetApiDomainIpResult GetApiDomainIp(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetApiDomainIpResult>(accessToken,
                    Config.ApiWorkHost + GetApiDomainIpPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取企业微信 API 域名当前解析到的服务器 IP 段。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/92520"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信 API 服务器 IP 段列表。</returns>
        public static Task<GetApiDomainIpResult> GetApiDomainIpAsync(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetApiDomainIpResult>(accessToken,
                    Config.ApiWorkHost + GetApiDomainIpPath + "?access_token={0}", null,
                    CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);
    }
}
