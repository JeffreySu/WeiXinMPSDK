#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
    
    文件名：XPayApi.cs
    文件功能描述：小程序虚拟支付
    
    
    创建标识：Yaofeng - 20231130
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.SecOrder;
using Senparc.Weixin.Open.XPayAPIs.XPayJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.XPayAPIs
{
    /// <summary>
    /// 小程序虚拟支付
    /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/industry/virtual-payment.html#_2-3-%E6%9C%8D%E5%8A%A1%E5%99%A8API
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class XPayApi
    {
        #region 同步方法
        /// <summary>
        /// 查询用户代币余额
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="openid">用户的openid</param>
        /// <param name="env">0-正式环境 1-沙箱环境</param>
        /// <param name="user_ip">用户ip，例如:1.1.1.1</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryUserBalanceJsonResult QueryUserBalance(string accessToken, string pay_sig, QueryUserBalanceRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/query_user_balance?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<QueryUserBalanceJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 扣减代币（一般用于代币支付）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="signature">签名</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CurrencyPayJsonResult CurrencyPay(string accessToken, string pay_sig, string signature, CurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/currency_pay?access_token={0}&pay_sig={1}&signature={2}", accessToken.AsUrlData(), pay_sig.AsUrlData(), signature.AsUrlData());
            return CommonJsonSend.Send<CurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询创建的订单（现金单，非代币单）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryOrderJsonResult QueryOrder(string accessToken, string pay_sig, QueryOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/query_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<QueryOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 代币支付退款(currency_pay接口的逆操作)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CancelCurrencyPayJsonResult CancelCurrencyPay(string accessToken, string pay_sig, CancelCurrencyPayRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/cancel_currency_pay?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<CancelCurrencyPayJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 通知已经发货完成（只能通知现金单）,正常通过xpay_goods_deliver_notify消息推送返回成功就不需要调用这个api接口。这个接口用于异常情况推送不成功时手动将单改成已发货状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult NotifyProvideGoods(string accessToken, string pay_sig, NotifyProvideGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/notify_provide_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 用于下载小程序账单，第一次调用触发生成下载url，可以间隔轮训来获取最终生成的下载url
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="begin_ds">起始时间（如20230801）</param>
        /// <param name="end_ds">截止时间（如20230810）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DownloadBillJsonResult DownloadBill(string accessToken, string pay_sig, long begin_ds, long end_ds, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/download_bill?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            var data = new
            {
                begin_ds,
                end_ds
            };
            return CommonJsonSend.Send<DownloadBillJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 对使用jsapi接口下的单进行退款，此接口只是启动退款任务成功，启动后需要调用query_order接口来查询退款单状态，等状态变成退款完成后即为最终成功
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RefundOrderJsonResult RefundOrder(string accessToken, string pay_sig, RefundOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/refund_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<RefundOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 创建提现单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateWithdrawOrderJsonResult CreateWithdrawOrder(string accessToken, string pay_sig, CreateWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/create_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<CreateWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询提现单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryWithdrawOrderJsonResult QueryWithdrawOrder(string accessToken, string pay_sig, QueryWithdrawOrderRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/query_withdraw_order?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<QueryWithdrawOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 启动批量上传道具任务
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_sig"></param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult StartUploadGoods(string accessToken, string pay_sig, StartUploadGoodsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/xpay/start_upload_goods?access_token={0}&pay_sig={1}", accessToken.AsUrlData(), pay_sig.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion

        #region 异步方法

        #endregion
    }
}