/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：IdConvertApi.MassCallTicket.cs
    文件功能描述：企业微信接口高频调用凭据接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐获取接口高频调用凭据接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs.IdConvert
{
    /// <summary>
    /// 企业微信账号 ID 转换及高频调用凭据接口。
    /// </summary>
    public static partial class IdConvertApi
    {
        private const string ApplyMassCallTicketPath =
            "/cgi-bin/corp/apply_mass_call_ticket";

        /// <summary>
        /// 获取用于企业授权后大规模初始化的接口高频调用凭据。
        /// <para>该凭据仅能在授权后三个月内申请一次，申请成功后有效期为七天。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96168"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">第三方、代开发或上下游应用 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>用于受支持 ID 转换接口的高频调用凭据。</returns>
        public static ApplyMassCallTicketResult ApplyMassCallTicket(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<ApplyMassCallTicketResult>(accessToken,
                    Config.ApiWorkHost + ApplyMassCallTicketPath + "?access_token={0}",
                    null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        /// <summary>
        /// 异步获取用于企业授权后大规模初始化的接口高频调用凭据。
        /// <para>该凭据仅能在授权后三个月内申请一次，申请成功后有效期为七天。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96168"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">第三方、代开发或上下游应用 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>用于受支持 ID 转换接口的高频调用凭据。</returns>
        public static Task<ApplyMassCallTicketResult> ApplyMassCallTicketAsync(
            string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<ApplyMassCallTicketResult>(accessToken,
                    Config.ApiWorkHost + ApplyMassCallTicketPath + "?access_token={0}",
                    null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);
    }
}
