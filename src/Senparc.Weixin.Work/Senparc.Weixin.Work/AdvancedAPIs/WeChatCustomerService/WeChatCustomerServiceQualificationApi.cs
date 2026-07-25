/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeChatCustomerServiceQualificationApi.cs
    文件功能描述：企业微信客服企业资质查询接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加企业资质查询同步与异步入口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService
{
    /// <summary>
    /// 新版微信客服企业资质接口扩展。
    /// </summary>
    public static partial class WeChatCustomerServiceApi
    {
        private const string GetCorpQualificationPath = "/cgi-bin/kf/get_corp_qualification";

        /// <summary>
        /// 获取企业的微信客服资质信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/95153">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业是否已绑定视频号。</returns>
        public static KfCorpQualificationResult GetCorpQualification(string token, int timeOut = Config.TIME_OUT)
            => Get<KfCorpQualificationResult>(token, GetCorpQualificationPath, string.Empty, timeOut);

        /// <summary>
        /// 异步获取企业的微信客服资质信息。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/95153">企业微信官方文档</see></para>
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>企业是否已绑定视频号。</returns>
        public static Task<KfCorpQualificationResult> GetCorpQualificationAsync(string token,
            int timeOut = Config.TIME_OUT)
            => GetAsync<KfCorpQualificationResult>(token, GetCorpQualificationPath, string.Empty, timeOut);
    }
}
