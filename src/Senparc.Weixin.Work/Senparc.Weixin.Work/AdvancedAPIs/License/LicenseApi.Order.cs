/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseApi.Order.cs
    文件功能描述：企业微信服务商接口调用许可订单管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可单企业、跨企业和余额支付订单接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>
    /// 企业微信服务商接口调用许可订单管理接口。
    /// </summary>
    public static partial class LicenseApi
    {
        private const string CreateOrderPath = "/cgi-bin/license/create_new_order";
        private const string CreateRenewOrderJobPath =
            "/cgi-bin/license/create_renew_order_job";
        private const string SubmitRenewOrderJobPath =
            "/cgi-bin/license/submit_order_job";
        private const string ListOrderPath = "/cgi-bin/license/list_order";
        private const string GetOrderPath = "/cgi-bin/license/get_order";
        private const string ListOrderAccountPath =
            "/cgi-bin/license/list_order_account";
        private const string CancelOrderPath = "/cgi-bin/license/cancel_order";
        private const string CreateMultiCorpOrderJobPath =
            "/cgi-bin/license/create_new_order_job";
        private const string SubmitMultiCorpOrderJobPath =
            "/cgi-bin/license/submit_new_order_job";
        private const string GetMultiCorpOrderJobResultPath =
            "/cgi-bin/license/new_order_job_result";
        private const string GetUnionOrderPath = "/cgi-bin/license/get_union_order";
        private const string SubmitBalancePaymentJobPath =
            "/cgi-bin/license/submit_pay_job";
        private const string GetBalancePaymentJobResultPath =
            "/cgi-bin/license/pay_job_result";

        /// <summary>
        /// 为单个企业下单购买许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97182"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、购买人、账号数量和购买时长。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新订单号。</returns>
        public static LicenseCreateOrderResult CreateOrder(string providerAccessToken,
            LicenseCreateOrderRequest data, int timeOut = Config.TIME_OUT)
            => Post<LicenseCreateOrderResult>(providerAccessToken, CreateOrderPath, data,
                timeOut);

        /// <summary>
        /// 异步为单个企业下单购买许可账号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97182"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、购买人、账号数量和购买时长。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>新订单号。</returns>
        public static Task<LicenseCreateOrderResult> CreateOrderAsync(
            string providerAccessToken, LicenseCreateOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseCreateOrderResult>(providerAccessToken,
                CreateOrderPath, data, timeOut);

        /// <summary>
        /// 创建指定成员许可账号续期任务并校验账号有效性。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97183"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、成员账号列表及调用方幂等 JobId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>续期任务和无效账号列表。</returns>
        public static LicenseCreateRenewOrderJobResult CreateRenewOrderJob(
            string providerAccessToken, LicenseCreateRenewOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseCreateRenewOrderJobResult>(providerAccessToken,
                CreateRenewOrderJobPath, data, timeOut);

        /// <summary>
        /// 异步创建指定成员许可账号续期任务并校验账号有效性。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97183"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、成员账号列表及调用方幂等 JobId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>续期任务和无效账号列表。</returns>
        public static Task<LicenseCreateRenewOrderJobResult>
            CreateRenewOrderJobAsync(string providerAccessToken,
                LicenseCreateRenewOrderJobRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseCreateRenewOrderJobResult>(providerAccessToken,
                CreateRenewOrderJobPath, data, timeOut);

        /// <summary>
        /// 提交已经创建并校验完成的许可续期任务生成订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97183"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">任务、购买人和续期时长。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>续期订单号。</returns>
        public static LicenseCreateOrderResult SubmitRenewOrderJob(
            string providerAccessToken, LicenseSubmitRenewOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseCreateOrderResult>(providerAccessToken,
                SubmitRenewOrderJobPath, data, timeOut);

        /// <summary>
        /// 异步提交已经创建并校验完成的许可续期任务生成订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97183"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">任务、购买人和续期时长。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>续期订单号。</returns>
        public static Task<LicenseCreateOrderResult> SubmitRenewOrderJobAsync(
            string providerAccessToken, LicenseSubmitRenewOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseCreateOrderResult>(providerAccessToken,
                SubmitRenewOrderJobPath, data, timeOut);

        /// <summary>
        /// 分页获取指定企业的许可订单列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97184"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、订单时间范围和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单摘要列表和下一页游标。</returns>
        public static LicenseOrderListResult ListOrder(string providerAccessToken,
            LicenseOrderListRequest data, int timeOut = Config.TIME_OUT)
            => Post<LicenseOrderListResult>(providerAccessToken, ListOrderPath, data,
                timeOut);

        /// <summary>
        /// 异步分页获取指定企业的许可订单列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97184"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业、订单时间范围和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单摘要列表和下一页游标。</returns>
        public static Task<LicenseOrderListResult> ListOrderAsync(
            string providerAccessToken, LicenseOrderListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseOrderListResult>(providerAccessToken, ListOrderPath,
                data, timeOut);

        /// <summary>
        /// 获取单个许可订单的状态、价格、账号数量和购买时长。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97185"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>许可订单详情。</returns>
        public static LicenseGetOrderResult GetOrder(string providerAccessToken,
            LicenseOrderIdRequest data, int timeOut = Config.TIME_OUT)
            => Post<LicenseGetOrderResult>(providerAccessToken, GetOrderPath, data,
                timeOut);

        /// <summary>
        /// 异步获取单个许可订单的状态、价格、账号数量和购买时长。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97185"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>许可订单详情。</returns>
        public static Task<LicenseGetOrderResult> GetOrderAsync(
            string providerAccessToken, LicenseOrderIdRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseGetOrderResult>(providerAccessToken, GetOrderPath,
                data, timeOut);

        /// <summary>
        /// 分页获取许可订单中的成员账号和激活码列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97186"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">订单号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单账号列表和下一页游标。</returns>
        public static LicenseOrderAccountListResult ListOrderAccount(
            string providerAccessToken, LicenseOrderAccountListRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseOrderAccountListResult>(providerAccessToken,
                ListOrderAccountPath, data, timeOut);

        /// <summary>
        /// 异步分页获取许可订单中的成员账号和激活码列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97186"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">订单号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单账号列表和下一页游标。</returns>
        public static Task<LicenseOrderAccountListResult> ListOrderAccountAsync(
            string providerAccessToken, LicenseOrderAccountListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseOrderAccountListResult>(providerAccessToken,
                ListOrderAccountPath, data, timeOut);

        /// <summary>
        /// 取消尚未支付的指定企业许可订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97187"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">订单号和所属企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult CancelOrder(string providerAccessToken,
            LicenseCancelOrderRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(providerAccessToken, CancelOrderPath, data,
                timeOut);

        /// <summary>
        /// 异步取消尚未支付的指定企业许可订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97187"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">订单号和所属企业。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> CancelOrderAsync(
            string providerAccessToken, LicenseCancelOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(providerAccessToken, CancelOrderPath, data,
                timeOut);

        /// <summary>
        /// 创建一次为多个企业购买许可账号的异步任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业购买列表和调用方幂等 JobId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>购买任务和无效企业列表。</returns>
        public static LicenseCreateMultiCorpOrderJobResult CreateMultiCorpOrderJob(
            string providerAccessToken, LicenseCreateMultiCorpOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseCreateMultiCorpOrderJobResult>(providerAccessToken,
                CreateMultiCorpOrderJobPath, data, timeOut);

        /// <summary>
        /// 异步创建一次为多个企业购买许可账号的异步任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">企业购买列表和调用方幂等 JobId。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>购买任务和无效企业列表。</returns>
        public static Task<LicenseCreateMultiCorpOrderJobResult>
            CreateMultiCorpOrderJobAsync(string providerAccessToken,
                LicenseCreateMultiCorpOrderJobRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseCreateMultiCorpOrderJobResult>(providerAccessToken,
                CreateMultiCorpOrderJobPath, data, timeOut);

        /// <summary>
        /// 提交跨企业许可购买任务生成联合订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">任务和服务商企业内购买人。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult SubmitMultiCorpOrderJob(
            string providerAccessToken, LicenseSubmitMultiCorpOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(providerAccessToken,
                SubmitMultiCorpOrderJobPath, data, timeOut);

        /// <summary>
        /// 异步提交跨企业许可购买任务生成联合订单。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">任务和服务商企业内购买人。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> SubmitMultiCorpOrderJobAsync(
            string providerAccessToken, LicenseSubmitMultiCorpOrderJobRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(providerAccessToken,
                SubmitMultiCorpOrderJobPath, data, timeOut);

        /// <summary>
        /// 查询跨企业许可购买任务的处理状态和联合订单号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询任务。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务状态、联合订单号和失败企业。</returns>
        public static LicenseMultiCorpOrderJobResult GetMultiCorpOrderJobResult(
            string providerAccessToken, LicenseJobIdRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseMultiCorpOrderJobResult>(providerAccessToken,
                GetMultiCorpOrderJobResultPath, data, timeOut);

        /// <summary>
        /// 异步查询跨企业许可购买任务的处理状态和联合订单号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98887"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询任务。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>任务状态、联合订单号和失败企业。</returns>
        public static Task<LicenseMultiCorpOrderJobResult>
            GetMultiCorpOrderJobResultAsync(string providerAccessToken,
                LicenseJobIdRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseMultiCorpOrderJobResult>(providerAccessToken,
                GetMultiCorpOrderJobResultPath, data, timeOut);

        /// <summary>
        /// 分页获取联合订单及其各企业子订单详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98888"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">联合订单号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>联合订单和企业购买列表。</returns>
        public static LicenseUnionOrderResult GetUnionOrder(
            string providerAccessToken, LicenseUnionOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseUnionOrderResult>(providerAccessToken,
                GetUnionOrderPath, data, timeOut);

        /// <summary>
        /// 异步分页获取联合订单及其各企业子订单详情。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98888"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">联合订单号和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>联合订单和企业购买列表。</returns>
        public static Task<LicenseUnionOrderResult> GetUnionOrderAsync(
            string providerAccessToken, LicenseUnionOrderRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseUnionOrderResult>(providerAccessToken,
                GetUnionOrderPath, data, timeOut);

        /// <summary>
        /// 创建使用服务商充值账户余额支付许可订单的异步任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99420"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">付款人和待支付订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>支付任务号。</returns>
        public static LicenseSubmitPaymentJobResult SubmitBalancePaymentJob(
            string providerAccessToken, LicenseSubmitPaymentJobRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicenseSubmitPaymentJobResult>(providerAccessToken,
                SubmitBalancePaymentJobPath, data, timeOut);

        /// <summary>
        /// 异步创建使用服务商充值账户余额支付许可订单的异步任务。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99420"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">付款人和待支付订单号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>支付任务号。</returns>
        public static Task<LicenseSubmitPaymentJobResult>
            SubmitBalancePaymentJobAsync(string providerAccessToken,
                LicenseSubmitPaymentJobRequest data,
                int timeOut = Config.TIME_OUT)
            => PostAsync<LicenseSubmitPaymentJobResult>(providerAccessToken,
                SubmitBalancePaymentJobPath, data, timeOut);

        /// <summary>
        /// 查询服务商余额支付任务的处理状态和失败企业。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99420"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询支付任务。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>支付任务状态和失败详情。</returns>
        public static LicensePaymentJobResult GetBalancePaymentJobResult(
            string providerAccessToken, LicenseJobIdRequest data,
            int timeOut = Config.TIME_OUT)
            => Post<LicensePaymentJobResult>(providerAccessToken,
                GetBalancePaymentJobResultPath, data, timeOut);

        /// <summary>
        /// 异步查询服务商余额支付任务的处理状态和失败企业。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99420"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken。</param>
        /// <param name="data">待查询支付任务。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>支付任务状态和失败详情。</returns>
        public static Task<LicensePaymentJobResult>
            GetBalancePaymentJobResultAsync(string providerAccessToken,
                LicenseJobIdRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<LicensePaymentJobResult>(providerAccessToken,
                GetBalancePaymentJobResultPath, data, timeOut);
    }
}
