/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DataIntelligenceApi.Order.cs
    文件功能描述：企业微信数据与智能专区订单管理接口


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐高级接口下单、支付和查询接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 数据与智能专区订单管理接口。
    /// </summary>
    public static partial class DataIntelligenceApi
    {
        private static readonly JsonSetting AdvancedApiOrderIgnoreNullJsonSetting =
            new JsonSetting(true);

        private const string CreateAdvancedApiOrderPath =
            "/cgi-bin/advanced_api/create_order";
        private const string CancelAdvancedApiOrderPath =
            "/cgi-bin/advanced_api/cancel_order";
        private const string SubmitAdvancedApiOrderPaymentPath =
            "/cgi-bin/advanced_api/submit_pay";
        private const string GetAdvancedApiOrderListPath =
            "/cgi-bin/advanced_api/list_order";
        private const string GetAdvancedApiOrderPath =
            "/cgi-bin/advanced_api/get_order";
        private const string GetAdvancedApiCorpPurchaseInfoPath =
            "/cgi-bin/advanced_api/get_corp_buy_info";

        /// <summary>
        /// 为客户企业创建数据与智能专区高级接口订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100257"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业、下单人、订单类型和购买版本信息。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新创建的订单号。</returns>
        public static AdvancedApiCreateOrderResult CreateAdvancedApiOrder(
            string providerAccessToken, AdvancedApiCreateOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<AdvancedApiCreateOrderResult>(providerAccessToken,
                CreateAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 异步为客户企业创建数据与智能专区高级接口订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100257"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业、下单人、订单类型和购买版本信息。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新创建的订单号。</returns>
        public static Task<AdvancedApiCreateOrderResult> CreateAdvancedApiOrderAsync(
            string providerAccessToken, AdvancedApiCreateOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<AdvancedApiCreateOrderResult>(providerAccessToken,
                CreateAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 取消尚未完成的数据与智能专区高级接口订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100258"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待取消的订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult CancelAdvancedApiOrder(string providerAccessToken,
            AdvancedApiOrderIdRequest data, int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<WorkJsonResult>(providerAccessToken,
                CancelAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 异步取消尚未完成的数据与智能专区高级接口订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100258"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待取消的订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> CancelAdvancedApiOrderAsync(
            string providerAccessToken, AdvancedApiOrderIdRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<WorkJsonResult>(providerAccessToken,
                CancelAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 使用服务商充值账户余额支付数据与智能专区订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100259"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">支付人和待支付订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult SubmitAdvancedApiOrderPayment(
            string providerAccessToken, AdvancedApiSubmitPayRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<WorkJsonResult>(providerAccessToken,
                SubmitAdvancedApiOrderPaymentPath, data, timeOut);

        /// <summary>
        /// 异步使用服务商充值账户余额支付数据与智能专区订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100259"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">支付人和待支付订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> SubmitAdvancedApiOrderPaymentAsync(
            string providerAccessToken, AdvancedApiSubmitPayRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<WorkJsonResult>(providerAccessToken,
                SubmitAdvancedApiOrderPaymentPath, data, timeOut);

        /// <summary>
        /// 分页获取数据与智能专区高级接口订单列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100260"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业、下单时间范围和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单摘要列表、下一页游标和是否还有更多记录。</returns>
        public static AdvancedApiOrderListResult GetAdvancedApiOrderList(
            string providerAccessToken, AdvancedApiOrderListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<AdvancedApiOrderListResult>(providerAccessToken,
                GetAdvancedApiOrderListPath, data, timeOut);

        /// <summary>
        /// 异步分页获取数据与智能专区高级接口订单列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100260"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业、下单时间范围和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单摘要列表、下一页游标和是否还有更多记录。</returns>
        public static Task<AdvancedApiOrderListResult> GetAdvancedApiOrderListAsync(
            string providerAccessToken, AdvancedApiOrderListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<AdvancedApiOrderListResult>(providerAccessToken,
                GetAdvancedApiOrderListPath, data, timeOut);

        /// <summary>
        /// 获取指定数据与智能专区高级接口订单的详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100261"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待查询的订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单状态、价格、客户企业和购买版本详情。</returns>
        public static AdvancedApiOrderDetailResult GetAdvancedApiOrder(
            string providerAccessToken, AdvancedApiOrderIdRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<AdvancedApiOrderDetailResult>(providerAccessToken,
                GetAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 异步获取指定数据与智能专区高级接口订单的详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100261"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待查询的订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单状态、价格、客户企业和购买版本详情。</returns>
        public static Task<AdvancedApiOrderDetailResult> GetAdvancedApiOrderAsync(
            string providerAccessToken, AdvancedApiOrderIdRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<AdvancedApiOrderDetailResult>(providerAccessToken,
                GetAdvancedApiOrderPath, data, timeOut);

        /// <summary>
        /// 获取客户企业已购的数据与智能专区高级接口版本信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100271"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业和高级接口类型。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>客户企业已购买的版本、人数及有效期。</returns>
        public static AdvancedApiCorpPurchaseInfoResult GetAdvancedApiCorpPurchaseInfo(
            string providerAccessToken, AdvancedApiCorpPurchaseInfoRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrder<AdvancedApiCorpPurchaseInfoResult>(providerAccessToken,
                GetAdvancedApiCorpPurchaseInfoPath, data, timeOut);

        /// <summary>
        /// 异步获取客户企业已购的数据与智能专区高级接口版本信息。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/100271"/></para>
        /// </summary>
        /// <param name="providerAccessToken">应用服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">客户企业和高级接口类型。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>客户企业已购买的版本、人数及有效期。</returns>
        public static Task<AdvancedApiCorpPurchaseInfoResult>
            GetAdvancedApiCorpPurchaseInfoAsync(string providerAccessToken,
                AdvancedApiCorpPurchaseInfoRequest data, int timeOut = Config.TIME_OUT)
            => PostAdvancedApiOrderAsync<AdvancedApiCorpPurchaseInfoResult>(
                providerAccessToken, GetAdvancedApiCorpPurchaseInfoPath, data, timeOut);

        private static T PostAdvancedApiOrder<T>(string providerAccessToken, string path,
            object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: AdvancedApiOrderIgnoreNullJsonSetting);

        private static Task<T> PostAdvancedApiOrderAsync<T>(string providerAccessToken,
            string path, object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: AdvancedApiOrderIgnoreNullJsonSetting);
    }
}
