using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信支付接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static class WeixinPay
    {
        /// <summary>
        /// Native
        /// </summary>
        /// <param name="appid">开放平台账户的唯一标识</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="noncestr">32 位内的随机串，防重发</param>
        /// <param name="productid">商品唯一id</param>
        /// <param name="sign">签名</param>
        public static string NativePay(string appid, string timestamp, string noncestr, string productid, string sign)
        {
            var urlFormat = "weixin://wxpay/bizpayurl?sign={0}&appid={1}&productid={2}&timestamp={3}&noncestr={4}";
            var url = string.Format(urlFormat, appid, timestamp, noncestr, productid, sign);

            return url;
        }

        /// <summary>
        /// 发货通知
        /// </summary>
        /// <param name="appid">公众平台账户的AppId</param>
        /// <param name="openid">购买用户的OpenId</param>
        /// <param name="transid">交易单号</param>
        /// <param name="out_trade_no">第三方订单号</param>
        /// <param name="deliver_timestamp">发货时间戳</param>
        /// <param name="deliver_status">发货状态，1 表明成功，0 表明失败，失败时需要在deliver_msg 填上失败原因</param>
        /// <param name="deliver_msg">发货状态信息，失败时可以填上UTF8 编码的错误提示信息，比如“该商品已退款</param>
        /// <param name="app_signature">签名</param>
        /// <param name="sign_method">签名方法</param>
        public static DelivernotifyResult Delivernotify(string appid, string openid, string transid, string out_trade_no, string deliver_timestamp, string deliver_status, string deliver_msg, string app_signature, string sign_method = "sha1")
        {
            var accessToken = AccessTokenContainer.GetToken(appid);

            var urlFormat = "https://api.weixin.qq.com/pay/delivernotify?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appid,
                openid = openid,
                transid = transid,
                out_trade_no = out_trade_no,
                deliver_timestamp = deliver_timestamp,
                deliver_status = deliver_status,
                deliver_msg = deliver_msg,
                app_signature = app_signature,
                sign_method = sign_method
            };

            return CommonJsonSend.Send<DelivernotifyResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="appid">公众平台账户的AppId</param>
        /// <param name="package">查询订单的关键信息数据</param>
        /// <param name="timestamp">linux 时间戳</param>
        /// <param name="app_signature">签名</param>
        /// <param name="sign_method">签名方法</param>
        public static OrderqueryResult Orderquery(string appid, string package, string timestamp, string app_signature, string sign_method)
        {
            var accessToken = AccessTokenContainer.GetToken(appid);

            var urlFormat = "https://api.weixin.qq.com/pay/orderquery?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appid,
                package = package,
                timestamp = timestamp,
                app_signature = app_signature,
                sign_method = sign_method
            };

            return CommonJsonSend.Send<OrderqueryResult>(accessToken, urlFormat, data);
        }
    }
}
