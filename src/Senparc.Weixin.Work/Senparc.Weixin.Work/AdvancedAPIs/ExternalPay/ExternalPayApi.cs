/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalPayApi.cs
    文件功能描述：企业微信对外收款接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐对外收款商户、账单和付款信息接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.ExternalPay
{
    /// <summary>
    /// 企业微信对外收款接口。
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class ExternalPayApi
    {
        private const string AddMerchantPath = "/cgi-bin/externalpay/addmerchant";
        private const string GetMerchantPath = "/cgi-bin/externalpay/getmerchant";
        private const string DeleteMerchantPath = "/cgi-bin/externalpay/delmerchant";
        private const string SetMerchantUseScopePath = "/cgi-bin/externalpay/set_mch_use_scope";
        private const string GetBillListPath = "/cgi-bin/externalpay/get_bill_list";
        private const string GetPaymentInfoPath = "/cgi-bin/externalpay/get_payment_info";

        /// <summary>
        /// 添加对外收款商户号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号及商户全称。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult AddMerchant(string accessTokenOrAppKey,
            ExternalPayAddMerchantRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, AddMerchantPath, data, timeOut);

        /// <summary>
        /// 异步添加对外收款商户号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号及商户全称。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> AddMerchantAsync(string accessTokenOrAppKey,
            ExternalPayAddMerchantRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, AddMerchantPath, data, timeOut);

        /// <summary>
        /// 查询对外收款商户号及使用范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>商户号、绑定状态和使用范围。</returns>
        public static ExternalPayGetMerchantResult GetMerchant(string accessTokenOrAppKey,
            ExternalPayMerchantRequest data, int timeOut = Config.TIME_OUT)
            => Post<ExternalPayGetMerchantResult>(accessTokenOrAppKey, GetMerchantPath, data, timeOut);

        /// <summary>
        /// 异步查询对外收款商户号及使用范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号查询参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>商户号、绑定状态和使用范围。</returns>
        public static Task<ExternalPayGetMerchantResult> GetMerchantAsync(string accessTokenOrAppKey,
            ExternalPayMerchantRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<ExternalPayGetMerchantResult>(accessTokenOrAppKey, GetMerchantPath, data, timeOut);

        /// <summary>
        /// 删除对外收款商户号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">待删除的商户号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult DeleteMerchant(string accessTokenOrAppKey,
            ExternalPayMerchantRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, DeleteMerchantPath, data, timeOut);

        /// <summary>
        /// 异步删除对外收款商户号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">待删除的商户号。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> DeleteMerchantAsync(string accessTokenOrAppKey,
            ExternalPayMerchantRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteMerchantPath, data, timeOut);

        /// <summary>
        /// 设置商户号的使用范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号及允许使用的成员、部门和标签。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static WorkJsonResult SetMerchantUseScope(string accessTokenOrAppKey,
            ExternalPaySetMerchantUseScopeRequest data, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetMerchantUseScopePath, data, timeOut);

        /// <summary>
        /// 异步设置商户号的使用范围。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93666"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">商户号及允许使用的成员、部门和标签。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>企业微信通用结果。</returns>
        public static Task<WorkJsonResult> SetMerchantUseScopeAsync(string accessTokenOrAppKey,
            ExternalPaySetMerchantUseScopeRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetMerchantUseScopePath, data, timeOut);

        /// <summary>
        /// 分页查询对外收款交易记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93667"/></para>
        /// <para>退款字段参考：<see href="https://developer.work.weixin.qq.com/document/path/93727"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、收款成员和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>交易记录及下一页游标。</returns>
        public static ExternalPayGetBillListResult GetBillList(string accessTokenOrAppKey,
            ExternalPayGetBillListRequest data, int timeOut = Config.TIME_OUT)
            => Post<ExternalPayGetBillListResult>(accessTokenOrAppKey, GetBillListPath, data, timeOut);

        /// <summary>
        /// 异步分页查询对外收款交易记录。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/93667"/></para>
        /// <para>退款字段参考：<see href="https://developer.work.weixin.qq.com/document/path/93727"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">时间范围、收款成员和分页参数。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>交易记录及下一页游标。</returns>
        public static Task<ExternalPayGetBillListResult> GetBillListAsync(string accessTokenOrAppKey,
            ExternalPayGetBillListRequest data, int timeOut = Config.TIME_OUT)
            => PostAsync<ExternalPayGetBillListResult>(accessTokenOrAppKey, GetBillListPath, data, timeOut);

        /// <summary>
        /// 按收款项目 ID 查询关联的商户订单号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95944"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">收款项目 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>关联的商户订单号列表。</returns>
        public static ExternalPayGetPaymentInfoResult GetPaymentInfo(string accessTokenOrAppKey,
            ExternalPayGetPaymentInfoRequest data, int timeOut = Config.TIME_OUT)
            => Post<ExternalPayGetPaymentInfoResult>(accessTokenOrAppKey, GetPaymentInfoPath, data, timeOut);

        /// <summary>
        /// 异步按收款项目 ID 查询关联的商户订单号。
        /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95944"/></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">对外收款应用凭证，或由 AccessTokenContainer.BuildingKey 生成的 AppKey。</param>
        /// <param name="data">收款项目 ID。</param>
        /// <param name="timeOut">请求超时时间。</param>
        /// <returns>关联的商户订单号列表。</returns>
        public static Task<ExternalPayGetPaymentInfoResult> GetPaymentInfoAsync(
            string accessTokenOrAppKey, ExternalPayGetPaymentInfoRequest data,
            int timeOut = Config.TIME_OUT)
            => PostAsync<ExternalPayGetPaymentInfoResult>(accessTokenOrAppKey,
                GetPaymentInfoPath, data, timeOut);

        private static T Post<T>(string accessTokenOrAppKey, string path, object data, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object data,
            int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", data,
                CommonJsonSendType.POST, timeOut: timeOut), accessTokenOrAppKey);
    }
}
