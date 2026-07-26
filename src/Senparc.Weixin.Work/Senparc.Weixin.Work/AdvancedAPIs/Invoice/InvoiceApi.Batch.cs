/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：InvoiceApi.Batch.cs
    文件功能描述：企业微信批量查询电子发票接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐当前批量查询电子发票接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业微信电子发票接口。
    /// </summary>
    public static partial class InvoiceApi
    {
        private const string GetInvoiceInfoBatchPath =
            "/cgi-bin/card/invoice/reimburse/getinvoiceinfobatch";

        /// <summary>
        /// 批量查询用户选择的电子发票结构化信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/90287"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="data">待查询的发票 CardId 与加密 Code 列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>与请求顺序对应的电子发票结构化信息列表。</returns>
        public static GetInvoiceInfoBatchResultJson GetInvoiceInfoBatch(
            string accessTokenOrAppKey, GetInvoiceInfoBatchRequest data,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetInvoiceInfoBatchResultJson>(accessToken,
                    Config.ApiWorkHost + GetInvoiceInfoBatchPath +
                    "?access_token={0}", data, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        /// <summary>
        /// 异步批量查询用户选择的电子发票结构化信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/90287"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">企业微信 AccessToken，或已注册的应用 AppKey。</param>
        /// <param name="data">待查询的发票 CardId 与加密 Code 列表。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>与请求顺序对应的电子发票结构化信息列表。</returns>
        public static Task<GetInvoiceInfoBatchResultJson> GetInvoiceInfoBatchAsync(
            string accessTokenOrAppKey, GetInvoiceInfoBatchRequest data,
            int timeOut = Config.TIME_OUT)
            => ApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetInvoiceInfoBatchResultJson>(accessToken,
                    Config.ApiWorkHost + GetInvoiceInfoBatchPath +
                    "?access_token={0}", data, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);
    }
}
