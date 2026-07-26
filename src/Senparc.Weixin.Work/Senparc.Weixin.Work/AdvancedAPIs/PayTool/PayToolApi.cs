/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：PayToolApi.cs
    文件功能描述：企业微信服务商收银台收款工具接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐收款订单和发票管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.PayTool
{
    /// <summary>
    /// 企业微信服务商收银台收款工具接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class PayToolApi
    {
        private static readonly JsonSetting IgnoreNullJsonSetting = new JsonSetting(true);

        private const string GetInvoiceListPath = "/cgi-bin/paytool/get_invoice_list";
        private const string MarkInvoiceStatusPath = "/cgi-bin/paytool/mark_invoice_status";
        private const string OpenOrderPath = "/cgi-bin/paytool/open_order";
        private const string CloseOrderPath = "/cgi-bin/paytool/close_order";
        private const string GetOrderListPath = "/cgi-bin/paytool/get_order_list";
        private const string GetOrderDetailPath = "/cgi-bin/paytool/get_order_detail";

        /// <summary>
        /// 分页获取应用订单发票列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99436"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">申请时间和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>发票列表和下一页游标。</returns>
        public static PayToolGetInvoiceListResult GetInvoiceList(string providerAccessToken,
            PayToolGetInvoiceListRequest data, int timeOut = Config.TIME_OUT)
            => Post<PayToolGetInvoiceListResult>(providerAccessToken, GetInvoiceListPath,
                data, timeOut);

        /// <summary>
        /// 异步分页获取应用订单发票列表。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99436"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">申请时间和分页条件。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>发票列表和下一页游标。</returns>
        public static Task<PayToolGetInvoiceListResult> GetInvoiceListAsync(
            string providerAccessToken, PayToolGetInvoiceListRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<PayToolGetInvoiceListResult>(providerAccessToken,
                GetInvoiceListPath, data, timeOut);

        /// <summary>
        /// 标记指定订单的开票状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99437"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">订单、操作人、开票状态和客户可见备注。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult MarkInvoiceStatus(string providerAccessToken,
            PayToolMarkInvoiceStatusRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(providerAccessToken, MarkInvoiceStatusPath, data, timeOut);

        /// <summary>
        /// 异步标记指定订单的开票状态。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/99437"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">订单、操作人、开票状态和客户可见备注。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> MarkInvoiceStatusAsync(string providerAccessToken,
            PayToolMarkInvoiceStatusRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(providerAccessToken, MarkInvoiceStatusPath,
                data, timeOut);

        /// <summary>
        /// 创建普通第三方应用、代开发应用或行业解决方案收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98045"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">业务类型、支付方式和购买商品。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单号、客户支付或确认链接以及可确定的价格。</returns>
        public static PayToolOpenOrderResult OpenOrder(string providerAccessToken,
            PayToolOpenOrderRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSigned<PayToolOpenOrderResult>(providerAccessToken, OpenOrderPath,
                data, payToolApiSecret, timeOut);

        /// <summary>
        /// 异步创建普通第三方应用、代开发应用或行业解决方案收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98045"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">业务类型、支付方式和购买商品。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单号、客户支付或确认链接以及可确定的价格。</returns>
        public static Task<PayToolOpenOrderResult> OpenOrderAsync(string providerAccessToken,
            PayToolOpenOrderRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSignedAsync<PayToolOpenOrderResult>(providerAccessToken, OpenOrderPath,
                data, payToolApiSecret, timeOut);

        /// <summary>
        /// 取消指定的收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98046"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待取消的收款订单。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static WorkJsonResult CloseOrder(string providerAccessToken,
            PayToolCloseOrderRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSigned<WorkJsonResult>(providerAccessToken, CloseOrderPath,
                data, payToolApiSecret, timeOut);

        /// <summary>
        /// 异步取消指定的收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98046"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待取消的收款订单。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信执行结果。</returns>
        public static Task<WorkJsonResult> CloseOrderAsync(string providerAccessToken,
            PayToolCloseOrderRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSignedAsync<WorkJsonResult>(providerAccessToken, CloseOrderPath,
                data, payToolApiSecret, timeOut);

        /// <summary>
        /// 分页获取指定时间段内创建的收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98053"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">业务类型、创建时间范围和分页条件。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>收款订单摘要和下一页游标。</returns>
        public static PayToolGetOrderListResult GetOrderList(string providerAccessToken,
            PayToolGetOrderListRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSigned<PayToolGetOrderListResult>(providerAccessToken,
                GetOrderListPath, data, payToolApiSecret, timeOut);

        /// <summary>
        /// 异步分页获取指定时间段内创建的收款订单。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98053"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">业务类型、创建时间范围和分页条件。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>收款订单摘要和下一页游标。</returns>
        public static Task<PayToolGetOrderListResult> GetOrderListAsync(
            string providerAccessToken, PayToolGetOrderListRequest data,
            string payToolApiSecret = null, int timeOut = Config.TIME_OUT)
            => PostSignedAsync<PayToolGetOrderListResult>(providerAccessToken,
                GetOrderListPath, data, payToolApiSecret, timeOut);

        /// <summary>
        /// 获取指定收款订单的完整详情和商品明细。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98054"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待查询的收款订单。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单、支付、到账和商品明细。</returns>
        public static PayToolGetOrderDetailResult GetOrderDetail(string providerAccessToken,
            PayToolGetOrderDetailRequest data, string payToolApiSecret = null,
            int timeOut = Config.TIME_OUT)
            => PostSigned<PayToolGetOrderDetailResult>(providerAccessToken,
                GetOrderDetailPath, data, payToolApiSecret, timeOut);

        /// <summary>
        /// 异步获取指定收款订单的完整详情和商品明细。
        /// <para>当请求尚未填写签名时，会自动补齐随机串、时间戳并使用支付密钥签名。</para>
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/98054"/></para>
        /// </summary>
        /// <param name="providerAccessToken">服务商 ProviderAccessToken；本接口不使用普通应用 AccessToken。</param>
        /// <param name="data">待查询的收款订单。</param>
        /// <param name="payToolApiSecret">收银台 API 调用密钥；当 <paramref name="data"/> 未预先填写 sig 时必填。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>订单、支付、到账和商品明细。</returns>
        public static Task<PayToolGetOrderDetailResult> GetOrderDetailAsync(
            string providerAccessToken, PayToolGetOrderDetailRequest data,
            string payToolApiSecret = null, int timeOut = Config.TIME_OUT)
            => PostSignedAsync<PayToolGetOrderDetailResult>(providerAccessToken,
                GetOrderDetailPath, data, payToolApiSecret, timeOut);

        private static T Post<T>(string providerAccessToken, string path, object data,
            int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.Send<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static Task<T> PostAsync<T>(string providerAccessToken, string path,
            object data, int timeOut) where T : WorkJsonResult, new()
            => CommonJsonSend.SendAsync<T>(providerAccessToken,
                Config.ApiWorkHost + path + "?provider_access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut,
                jsonSetting: IgnoreNullJsonSetting);

        private static T PostSigned<T>(string providerAccessToken, string path,
            PayToolSignedRequestBase data, string payToolApiSecret, int timeOut)
            where T : WorkJsonResult, new()
        {
            PayToolSignatureHelper.PrepareRequest(data, payToolApiSecret);
            return Post<T>(providerAccessToken, path, data, timeOut);
        }

        private static Task<T> PostSignedAsync<T>(string providerAccessToken, string path,
            PayToolSignedRequestBase data, string payToolApiSecret, int timeOut)
            where T : WorkJsonResult, new()
        {
            PayToolSignatureHelper.PrepareRequest(data, payToolApiSecret);
            return PostAsync<T>(providerAccessToken, path, data, timeOut);
        }
    }
}
