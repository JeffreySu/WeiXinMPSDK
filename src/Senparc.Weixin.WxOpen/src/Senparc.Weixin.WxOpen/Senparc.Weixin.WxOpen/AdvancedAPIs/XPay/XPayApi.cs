#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：XPayApi.cs
    文件功能描述：小程序虚拟支付


    创建标识：Yaofeng - 20231130

    修改标识：Senparc - 20260617
    修改描述：添加 iOS 会员订阅签名辅助方法 GenerateAppleSubscribePaySign

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Newtonsoft.Json;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 小程序虚拟支付
    /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/industry/virtual-payment.html#_2-3-%E6%9C%8D%E5%8A%A1%E5%99%A8API
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public partial class XPayApi
    {
        #region 同步方法
        /// <summary>
        /// 查询用户代币余额
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="openid">用户的openid</param>
        /// <param name="env">0-正式环境 1-沙箱环境</param>
        /// <param name="user_ip">用户ip，例如:1.1.1.1</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryUserBalanceJsonResult QueryUserBalance(string accessTokenOrAppId, string pay_sig, string signature, QueryUserBalanceRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_user_balance?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return CommonJsonSend.Send<QueryUserBalanceJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 扣减代币（一般用于代币支付）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CurrencyPayJsonResult CurrencyPay(string accessTokenOrAppId, string pay_sig, string signature, CurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/currency_pay?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return CommonJsonSend.Send<CurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询创建的订单（现金单，非代币单）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryOrderJsonResult QueryOrder(string accessTokenOrAppId, string pay_sig, QueryOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<QueryOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 代币支付退款(currency_pay接口的逆操作)
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CancelCurrencyPayJsonResult CancelCurrencyPay(string accessTokenOrAppId, string pay_sig, string signature, CancelCurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/cancel_currency_pay?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return CommonJsonSend.Send<CancelCurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 通知已经发货完成（只能通知现金单）,正常通过xpay_goods_deliver_notify消息推送返回成功就不需要调用这个api接口。这个接口用于异常情况推送不成功时手动将单改成已发货状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult NotifyProvideGoods(string accessTokenOrAppId, string pay_sig, NotifyProvideGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/notify_provide_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 代币赠送
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PresentCurrencyJsonResult PresentCurrency(string accessTokenOrAppId, PresentCurrencyRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/present_currency?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<PresentCurrencyJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 用于下载小程序账单，第一次调用触发生成下载url，可以间隔轮训来获取最终生成的下载url
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="begin_ds">起始时间（如20230801）</param>
        /// <param name="end_ds">截止时间（如20230810）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DownloadBillJsonResult DownloadBill(string accessTokenOrAppId, string pay_sig, long begin_ds, long end_ds, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/download_bill?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                var data = new
                {
                    begin_ds,
                    end_ds
                };
                return CommonJsonSend.Send<DownloadBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 对使用jsapi接口下的单进行退款，此接口只是启动退款任务成功，启动后需要调用query_order接口来查询退款单状态，等状态变成退款完成后即为最终成功
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RefundOrderJsonResult RefundOrder(string accessTokenOrAppId, string pay_sig, RefundOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/refund_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<RefundOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建提现单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateWithdrawOrderJsonResult CreateWithdrawOrder(string accessTokenOrAppId, string pay_sig, CreateWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/create_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<CreateWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询提现单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryWithdrawOrderJsonResult QueryWithdrawOrder(string accessTokenOrAppId, string pay_sig, QueryWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<QueryWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 启动批量上传道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult StartUploadGoods(string accessTokenOrAppId, string pay_sig, StartUploadGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_upload_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询批量上传道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryUploadGoodsJsonResult QueryUploadGoods(string accessTokenOrAppId, string pay_sig, QueryUploadGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_upload_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<QueryUploadGoodsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 启动批量发布道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult StartPublishGoods(string accessTokenOrAppId, string pay_sig, StartPublishGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_publish_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询批量发布道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryPublishGoodsJsonResult QueryPublishGoods(string accessTokenOrAppId, string pay_sig, QueryPublishGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_publish_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<QueryPublishGoodsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询商家账户里的可提现余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="env">0-正式环境 1-沙箱环境（仅作为签名校验，查询的结果都是正式环境的）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可提现余额及币种。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_query_biz_balance"/>。本接口支持第三方平台代商家调用。</remarks>
        public static QueryBizBalanceJsonResult QueryBizBalance(string accessTokenOrAppId, string pay_sig, int env, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_biz_balance?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                var data = new
                {
                    env
                };
                return CommonJsonSend.Send<QueryBizBalanceJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金充值账户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="env">0-正式环境 1-沙箱环境（仅作为签名校验，查询的结果都是正式环境的）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryTransferAccountJsonResult QueryTransferAccount(string accessTokenOrAppId, int env, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_transfer_account?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    env
                };
                return CommonJsonSend.Send<QueryTransferAccountJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金发放记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryAdverFundsJsonResult QueryAdverFunds(string accessTokenOrAppId, QueryAdverFundsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_adver_funds?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<QueryAdverFundsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 充值广告金
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateFundsBillJsonResult CreateFundsBill(string accessTokenOrAppId, CreateFundsBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/create_funds_bill?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CreateFundsBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 绑定广告金充值账户。此方法名保留历史拼写，建议新代码使用 <see cref="BindTransferAccount"/>。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="data">充值账户及小程序环境信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_bind_transfer_accout"/>。本接口支持第三方平台代商家调用。</remarks>
        public static WxJsonResult BindTransferAccout(string accessTokenOrAppId, BindTransferAccoutRequestData data, int timeOut = Config.TIME_OUT)
        {
            return BindTransferAccount(accessTokenOrAppId, data, timeOut);
        }

        /// <summary>
        /// 查询广告金充值记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryFundsBillJsonResult QueryFundsBill(string accessTokenOrAppId, QueryFundsBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_funds_bill?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<QueryFundsBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金回收记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryRecoverBillJsonResult QueryRecoverBill(string accessTokenOrAppId, QueryRecoverBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_recover_bill?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<QueryRecoverBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetComplaintListJsonResult GetComplaintList(string accessTokenOrAppId, string pay_sig, GetComplaintListRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_complaint_list?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<GetComplaintListJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取投诉详情
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetComplaintDetailJsonResult GetComplaintDetail(string accessTokenOrAppId, string pay_sig, GetComplaintDetailRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_complaint_detail?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<GetComplaintDetailJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取协商历史
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetNegotiationHistoryJsonResult GetNegotiationHistory(string accessTokenOrAppId, string pay_sig, GetNegotiationHistoryRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_negotiation_history?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<GetNegotiationHistoryJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 回复用户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ResponseComplaint(string accessTokenOrAppId, string pay_sig, ResponseComplaintRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/response_complaint?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 完成投诉处理
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CompleteComplaint(string accessTokenOrAppId, string pay_sig, CompleteComplaintRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/complete_complaint?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传媒体文件（如图片，凭证等）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadVpFileJsonResult UploadVpFile(string accessTokenOrAppId, string pay_sig, UploadVpFileRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/upload_vp_file?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<UploadVpFileJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取微信支付反馈投诉图片的签名头部
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUploadFileSignJsonResult GetUploadFileSign(string accessTokenOrAppId, string pay_sig, GetUploadFileSignRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_upload_file_sign?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<GetUploadFileSignJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载广告金对应的商户订单信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DownloadAdverfundsOrderJsonResult DownloadAdverfundsOrder(string accessTokenOrAppId, DownloadAdverfundsOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/download_adverfunds_order?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<DownloadAdverfundsOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询签约关系
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QuerySubscribeContractJsonResult QuerySubscribeContract(string accessTokenOrAppId, QuerySubscribeContractRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_subscribe_contract?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<QuerySubscribeContractJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预通知扣款
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SendSubscribePrePaymentJsonResult SendSubscribePrePayment(string accessTokenOrAppId, SendSubscribePrePaymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/send_subscribe_pre_payment?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<SendSubscribePrePaymentJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发起订阅扣款
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SubmitSubscribePayOrderJsonResult SubmitSubscribePayOrder(string accessTokenOrAppId, SubmitSubscribePayOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/submit_subscribe_pay_order?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<SubmitSubscribePayOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 商家解约
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CancelSubscribeContractJsonResult CancelSubscribeContract(string accessTokenOrAppId, CancelSubscribeContractRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/cancel_subscribe_contract?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CancelSubscribeContractJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载支付订单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static StartDownloadOrderJsonResult StartDownloadOrder(string accessTokenOrAppId, string pay_sig, StartDownloadOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_download_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<StartDownloadOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询下载订单任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryDownloadOrderJsonResult QueryDownloadOrder(string accessTokenOrAppId, string pay_sig, QueryDownloadOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_download_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return CommonJsonSend.Send<QueryDownloadOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 查询用户代币余额
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="openid">用户的openid</param>
        /// <param name="env">0-正式环境 1-沙箱环境</param>
        /// <param name="user_ip">用户ip，例如:1.1.1.1</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryUserBalanceJsonResult> QueryUserBalanceAsync(string accessTokenOrAppId, string pay_sig, string signature, QueryUserBalanceRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_user_balance?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryUserBalanceJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 扣减代币（一般用于代币支付）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CurrencyPayJsonResult> CurrencyPayAsync(string accessTokenOrAppId, string pay_sig, string signature, CurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/currency_pay?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return await CommonJsonSend.SendAsync<CurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询创建的订单（现金单，非代币单）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryOrderJsonResult> QueryOrderAsync(string accessTokenOrAppId, string pay_sig, QueryOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 代币支付退款(currency_pay接口的逆操作)
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig">支付签名</param>
        /// <param name="signature">用户态签名</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CancelCurrencyPayJsonResult> CancelCurrencyPayAsync(string accessTokenOrAppId, string pay_sig, string signature, CancelCurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/cancel_currency_pay?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
                return await CommonJsonSend.SendAsync<CancelCurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 通知已经发货完成（只能通知现金单）,正常通过xpay_goods_deliver_notify消息推送返回成功就不需要调用这个api接口。这个接口用于异常情况推送不成功时手动将单改成已发货状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> NotifyProvideGoodsAsync(string accessTokenOrAppId, string pay_sig, NotifyProvideGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/notify_provide_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 代币赠送
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PresentCurrencyJsonResult> PresentCurrencyAsync(string accessTokenOrAppId, PresentCurrencyRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/present_currency?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<PresentCurrencyJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 用于下载小程序账单，第一次调用触发生成下载url，可以间隔轮训来获取最终生成的下载url
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="begin_ds">起始时间（如20230801）</param>
        /// <param name="end_ds">截止时间（如20230810）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DownloadBillJsonResult> DownloadBillAsync(string accessTokenOrAppId, string pay_sig, long begin_ds, long end_ds, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/download_bill?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                var data = new
                {
                    begin_ds,
                    end_ds
                };
                return await CommonJsonSend.SendAsync<DownloadBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 对使用jsapi接口下的单进行退款，此接口只是启动退款任务成功，启动后需要调用query_order接口来查询退款单状态，等状态变成退款完成后即为最终成功
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RefundOrderJsonResult> RefundOrderAsync(string accessTokenOrAppId, string pay_sig, RefundOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/refund_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<RefundOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建提现单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CreateWithdrawOrderJsonResult> CreateWithdrawOrderAsync(string accessTokenOrAppId, string pay_sig, CreateWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/create_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<CreateWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询提现单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryWithdrawOrderJsonResult> QueryWithdrawOrderAsync(string accessTokenOrAppId, string pay_sig, QueryWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 启动批量上传道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> StartUploadGoodsAsync(string accessTokenOrAppId, string pay_sig, StartUploadGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_upload_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询批量上传道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryUploadGoodsJsonResult> QueryUploadGoodsAsync(string accessTokenOrAppId, string pay_sig, QueryUploadGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_upload_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryUploadGoodsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 启动批量发布道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> StartPublishGoodsAsync(string accessTokenOrAppId, string pay_sig, StartPublishGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_publish_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询批量发布道具任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryPublishGoodsJsonResult> QueryPublishGoodsAsync(string accessTokenOrAppId, string pay_sig, QueryPublishGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_publish_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryPublishGoodsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 异步查询商家账户里的可提现余额。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="pay_sig">支付签名。</param>
        /// <param name="env">0-正式环境 1-沙箱环境（仅作为签名校验，查询的结果都是正式环境的）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>可提现余额及币种。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_query_biz_balance"/>。本接口支持第三方平台代商家调用。</remarks>
        public static async Task<QueryBizBalanceJsonResult> QueryBizBalanceAsync(string accessTokenOrAppId, string pay_sig, int env, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_biz_balance?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                var data = new
                {
                    env
                };
                return await CommonJsonSend.SendAsync<QueryBizBalanceJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金充值账户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="env">0-正式环境 1-沙箱环境（仅作为签名校验，查询的结果都是正式环境的）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryTransferAccountJsonResult> QueryTransferAccountAsync(string accessTokenOrAppId, int env, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_transfer_account?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    env
                };
                return await CommonJsonSend.SendAsync<QueryTransferAccountJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金发放记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryAdverFundsJsonResult> QueryAdverFundsAsync(string accessTokenOrAppId, QueryAdverFundsRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_adver_funds?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryAdverFundsJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 充值广告金
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CreateFundsBillJsonResult> CreateFundsBillAsync(string accessTokenOrAppId, CreateFundsBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/create_funds_bill?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<CreateFundsBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 异步绑定广告金充值账户。此方法名保留历史拼写，建议新代码使用 <see cref="BindTransferAccountAsync"/>。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken、authorizer_access_token 或已注册的小程序 AppId。</param>
        /// <param name="data">充值账户及小程序环境信息。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>绑定结果。</returns>
        /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/API/VirtualPayment/api_bind_transfer_accout"/>。本接口支持第三方平台代商家调用。</remarks>
        public static async Task<WxJsonResult> BindTransferAccoutAsync(string accessTokenOrAppId, BindTransferAccoutRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await BindTransferAccountAsync(accessTokenOrAppId, data, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询广告金充值记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryFundsBillJsonResult> QueryFundsBillAsync(string accessTokenOrAppId, QueryFundsBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_funds_bill?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryFundsBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询广告金回收记录
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryRecoverBillJsonResult> QueryRecoverBillAsync(string accessTokenOrAppId, QueryRecoverBillRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_recover_bill?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryRecoverBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetComplaintListJsonResult> GetComplaintListAsync(string accessTokenOrAppId, string pay_sig, GetComplaintListRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_complaint_list?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<GetComplaintListJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取投诉详情
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetComplaintDetailJsonResult> GetComplaintDetailAsync(string accessTokenOrAppId, string pay_sig, GetComplaintDetailRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_complaint_detail?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<GetComplaintDetailJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取协商历史
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetNegotiationHistoryJsonResult> GetNegotiationHistoryAsync(string accessTokenOrAppId, string pay_sig, GetNegotiationHistoryRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_negotiation_history?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<GetNegotiationHistoryJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 回复用户
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ResponseComplaintAsync(string accessTokenOrAppId, string pay_sig, ResponseComplaintRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/response_complaint?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 完成投诉处理
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CompleteComplaintAsync(string accessTokenOrAppId, string pay_sig, CompleteComplaintRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/complete_complaint?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 上传媒体文件（如图片，凭证等）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadVpFileJsonResult> UploadVpFileAsync(string accessTokenOrAppId, string pay_sig, UploadVpFileRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/upload_vp_file?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<UploadVpFileJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取微信支付反馈投诉图片的签名头部
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUploadFileSignJsonResult> GetUploadFileSignAsync(string accessTokenOrAppId, string pay_sig, GetUploadFileSignRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/get_upload_file_sign?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<GetUploadFileSignJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载广告金对应的商户订单信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DownloadAdverfundsOrderJsonResult> DownloadAdverfundsOrderAsync(string accessTokenOrAppId, DownloadAdverfundsOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/download_adverfunds_order?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<DownloadAdverfundsOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询签约关系
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QuerySubscribeContractJsonResult> QuerySubscribeContractAsync(string accessTokenOrAppId, QuerySubscribeContractRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_subscribe_contract?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<QuerySubscribeContractJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 预通知扣款
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SendSubscribePrePaymentJsonResult> SendSubscribePrePaymentAsync(string accessTokenOrAppId, SendSubscribePrePaymentRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/send_subscribe_pre_payment?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<SendSubscribePrePaymentJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发起订阅扣款
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SubmitSubscribePayOrderJsonResult> SubmitSubscribePayOrderAsync(string accessTokenOrAppId, SubmitSubscribePayOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/submit_subscribe_pay_order?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<SubmitSubscribePayOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 商家解约
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CancelSubscribeContractJsonResult> CancelSubscribeContractAsync(string accessTokenOrAppId, CancelSubscribeContractRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/cancel_subscribe_contract?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<CancelSubscribeContractJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 下载支付订单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<StartDownloadOrderJsonResult> StartDownloadOrderAsync(string accessTokenOrAppId, string pay_sig, StartDownloadOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/start_download_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<StartDownloadOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询下载订单任务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryDownloadOrderJsonResult> QueryDownloadOrderAsync(string accessTokenOrAppId, string pay_sig, QueryDownloadOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/xpay/query_download_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
                return await CommonJsonSend.SendAsync<QueryDownloadOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 生成pay_sig
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appKey"></param>
        /// <param name="uri">例：/xpay/query_user_balance</param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string GeneratePaySign<T>(string appKey, string uri, T data)
        {
            if (string.IsNullOrWhiteSpace(appKey))
            {
                throw new ArgumentNullException("appKey");
            }
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException("uri");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var signData = JsonConvert.SerializeObject(data);
            var rawData = $"{uri}&{signData}";

            var keyBytes = Encoding.UTF8.GetBytes(appKey);
            var dataBytes = Encoding.UTF8.GetBytes(rawData);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 生成signature
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string GenerateSignature<T>(string sessionKey, T data)
        {
            if (string.IsNullOrWhiteSpace(sessionKey))
            {
                throw new ArgumentNullException("sessionKey");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var signData = JsonConvert.SerializeObject(data);
            var keyBytes = Encoding.UTF8.GetBytes(sessionKey);
            var dataBytes = Encoding.UTF8.GetBytes(signData);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        /// <summary>
        /// 生成 iOS 会员订阅（wx.requestAppleSubscribeSign）所需的 pay_sig
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appKey">小程序虚拟支付 AppKey</param>
        /// <param name="data">iOS 订阅签名数据（<see cref="RequestAppleSubscribeSignData"/>）</param>
        /// <returns>pay_sig 字符串</returns>
        public static string GenerateAppleSubscribePaySign<T>(string appKey, T data)
        {
            return GeneratePaySign(appKey, "requestAppleSubscribeSign", data);
        }
        #endregion
    }
}
