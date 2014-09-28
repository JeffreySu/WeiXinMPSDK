using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信支付接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static class TenPayV3
    {
        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <returns></returns>
        public static string TenPay(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            return CommonJsonSend.Send<string>(null, urlFormat, data);
        }

        /// <summary>
        /// 发货通知
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="openId">购买用户的OpenId</param>
        /// <param name="transId">交易单号</param>
        /// <param name="out_Trade_No">第三方订单号</param>
        /// <param name="deliver_TimesTamp">发货时间戳</param>
        /// <param name="deliver_Status">发货状态，1 表明成功，0 表明失败，失败时需要在deliver_msg 填上失败原因</param>
        /// <param name="deliver_Msg">发货状态信息，失败时可以填上UTF8 编码的错误提示信息，比如“该商品已退款</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static WxJsonResult Delivernotify(string appId, string openId, string transId, string out_Trade_No, string deliver_TimesTamp, string deliver_Status, string deliver_Msg, string app_Signature, string sign_Method = "sha1")
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/pay/delivernotify?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                openid = openId,
                transid = transId,
                out_trade_no = out_Trade_No,
                deliver_timestamp = deliver_TimesTamp,
                deliver_status = deliver_Status,
                deliver_msg = deliver_Msg,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="package">查询订单的关键信息数据</param>
        /// <param name="timesTamp">linux 时间戳</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static OrderqueryResult Orderquery(string appId, string package, string timesTamp, string app_Signature, string sign_Method)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/pay/orderquery?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                package = package,
                timestamp = timesTamp,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return CommonJsonSend.Send<OrderqueryResult>(accessToken, urlFormat, data);
        }
    }
}
