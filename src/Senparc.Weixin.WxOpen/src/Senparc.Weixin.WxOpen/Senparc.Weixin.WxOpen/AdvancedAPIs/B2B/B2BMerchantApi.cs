#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License
is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：B2BMerchantApi.cs
    文件功能描述：B2BMerchantApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.B2B
{
    /// <summary>
    /// 小程序 B2B 门店助手商户进件与费率接口。
    /// </summary>
    public static partial class B2BApi
    {
        #region 商户进件

        /// <summary>
        /// 提交 B2B 商户号进件申请。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">主体、证件、结算账户、超级管理员和开通方式等进件资料。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>进件申请单号。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_retailregistermch"/>。</remarks>
        public static B2BRegisterMerchantJsonResult RegisterMerchant(string accessTokenOrAppId, B2BRegisterMerchantRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BRegisterMerchantJsonResult>(accessTokenOrAppId, "/retail/B2b/retailregistermch", request, timeOut);
        }

        /// <summary>异步提交 B2B 商户号进件申请。</summary>
        /// <inheritdoc cref="RegisterMerchant"/>
        public static Task<B2BRegisterMerchantJsonResult> RegisterMerchantAsync(string accessTokenOrAppId, B2BRegisterMerchantRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BRegisterMerchantJsonResult>(accessTokenOrAppId, "/retail/B2b/retailregistermch", request, timeOut);
        }

        /// <summary>
        /// 上传 B2B 商户进件图片。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">文件名及文件内容的 Base64 编码。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可用于进件资料的文件 ID。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_retailuploadmchfile"/>。</remarks>
        public static B2BUploadMerchantFileJsonResult UploadMerchantFile(string accessTokenOrAppId, B2BUploadMerchantFileRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BUploadMerchantFileJsonResult>(accessTokenOrAppId, "/retail/B2b/retailuploadmchfile", request, timeOut);
        }

        /// <summary>异步上传 B2B 商户进件图片。</summary>
        /// <inheritdoc cref="UploadMerchantFile"/>
        public static Task<B2BUploadMerchantFileJsonResult> UploadMerchantFileAsync(string accessTokenOrAppId, B2BUploadMerchantFileRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BUploadMerchantFileJsonResult>(accessTokenOrAppId, "/retail/B2b/retailuploadmchfile", request, timeOut);
        }

        /// <summary>
        /// 查询 B2B 商户号开通状态。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">可选的申请单号和分页参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信支付、银行转账、费率和小程序关联状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_retailgetmchorder"/>。</remarks>
        public static B2BGetMerchantApplicationJsonResult GetMerchantApplication(string accessTokenOrAppId, B2BGetMerchantApplicationRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetMerchantApplicationJsonResult>(accessTokenOrAppId, "/retail/B2b/retailgetmchorder", request, timeOut);
        }

        /// <summary>异步查询 B2B 商户号开通状态。</summary>
        /// <inheritdoc cref="GetMerchantApplication"/>
        public static Task<B2BGetMerchantApplicationJsonResult> GetMerchantApplicationAsync(string accessTokenOrAppId, B2BGetMerchantApplicationRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetMerchantApplicationJsonResult>(accessTokenOrAppId, "/retail/B2b/retailgetmchorder", request, timeOut);
        }

        /// <summary>
        /// 为已有进件申请开通银行转账能力。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">原进件申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>申请提交结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_registeronlywqf"/>。</remarks>
        public static WxJsonResult ApplyBankTransfer(string accessTokenOrAppId, B2BOutRegistrationIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/registeronlywqf", request, timeOut);
        }

        /// <summary>异步为已有进件申请开通银行转账能力。</summary>
        /// <inheritdoc cref="ApplyBankTransfer"/>
        public static Task<WxJsonResult> ApplyBankTransferAsync(string accessTokenOrAppId, B2BOutRegistrationIdRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/registeronlywqf", request, timeOut);
        }

        /// <summary>
        /// 创建银行转账业务页面链接。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">银行转账开通申请单号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>页面链接及过期时间。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_createwqflink"/>。</remarks>
        public static B2BCreateBankTransferLinkJsonResult CreateBankTransferLink(string accessTokenOrAppId, B2BCreateBankTransferLinkRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BCreateBankTransferLinkJsonResult>(accessTokenOrAppId, "/retail/B2b/createwqflink", request, timeOut);
        }

        /// <summary>异步创建银行转账业务页面链接。</summary>
        /// <inheritdoc cref="CreateBankTransferLink"/>
        public static Task<B2BCreateBankTransferLinkJsonResult> CreateBankTransferLinkAsync(string accessTokenOrAppId, B2BCreateBankTransferLinkRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BCreateBankTransferLinkJsonResult>(accessTokenOrAppId, "/retail/B2b/createwqflink", request, timeOut);
        }

        /// <summary>
        /// 获取当前小程序下全部 B2B 商户信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>商户号、企业名称、脱敏账户及支付开通状态。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getmchinfo"/>。</remarks>
        public static B2BGetMerchantInfoJsonResult GetMerchantInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BGetMerchantInfoJsonResult>(accessTokenOrAppId, "/retail/B2b/getmchinfo", new { }, timeOut);
        }

        /// <summary>异步获取当前小程序下全部 B2B 商户信息。</summary>
        /// <inheritdoc cref="GetMerchantInfo"/>
        public static Task<B2BGetMerchantInfoJsonResult> GetMerchantInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BGetMerchantInfoJsonResult>(accessTokenOrAppId, "/retail/B2b/getmchinfo", new { }, timeOut);
        }

        /// <summary>
        /// 报名微信支付技术服务费优惠活动。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">子商户号和万分比费率。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>报名结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_setmchprofitrate"/>。</remarks>
        public static WxJsonResult SetMerchantProfitRate(string accessTokenOrAppId, B2BSetMerchantProfitRateRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/setmchprofitrate", request, timeOut);
        }

        /// <summary>异步报名微信支付技术服务费优惠活动。</summary>
        /// <inheritdoc cref="SetMerchantProfitRate"/>
        public static Task<WxJsonResult> SetMerchantProfitRateAsync(string accessTokenOrAppId, B2BSetMerchantProfitRateRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<WxJsonResult>(accessTokenOrAppId, "/retail/B2b/setmchprofitrate", request, timeOut);
        }

        /// <summary>
        /// 报名银行转账技术服务费优惠活动。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">子商户号和认证门店费率分子。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>认证与未认证门店费率及有效期。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_updatewqfchargefee"/>。</remarks>
        public static B2BBankTransferFeeJsonResult UpdateBankTransferFee(string accessTokenOrAppId, B2BUpdateBankTransferFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BBankTransferFeeJsonResult>(accessTokenOrAppId, "/retail/B2b/updatewqfchargefee", request, timeOut);
        }

        /// <summary>异步报名银行转账技术服务费优惠活动。</summary>
        /// <inheritdoc cref="UpdateBankTransferFee"/>
        public static Task<B2BBankTransferFeeJsonResult> UpdateBankTransferFeeAsync(string accessTokenOrAppId, B2BUpdateBankTransferFeeRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BBankTransferFeeJsonResult>(accessTokenOrAppId, "/retail/B2b/updatewqfchargefee", request, timeOut);
        }

        /// <summary>
        /// 查询银行转账技术服务费率。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="request">微信支付子商户号。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>认证与未认证门店费率及有效期。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/retail_business/bill/api_getwqfchargefee"/>。</remarks>
        public static B2BBankTransferFeeJsonResult GetBankTransferFee(string accessTokenOrAppId, B2BSubMerchantRequest request, int timeOut = Config.TIME_OUT)
        {
            return Send<B2BBankTransferFeeJsonResult>(accessTokenOrAppId, "/retail/B2b/getwqfchargefee", request, timeOut);
        }

        /// <summary>异步查询银行转账技术服务费率。</summary>
        /// <inheritdoc cref="GetBankTransferFee"/>
        public static Task<B2BBankTransferFeeJsonResult> GetBankTransferFeeAsync(string accessTokenOrAppId, B2BSubMerchantRequest request, int timeOut = Config.TIME_OUT)
        {
            return SendAsync<B2BBankTransferFeeJsonResult>(accessTokenOrAppId, "/retail/B2b/getwqfchargefee", request, timeOut);
        }

        #endregion
    }
}
