using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[统一下单]
    /// </summary>
    public class TenPayV3RequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商品金额,以分为单位(money * 100).ToString()
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// 用户的公网ip，不是商户服务器IP
        /// </summary>
        public string SpbillCreateIP { get; set; }
        /// <summary>
        /// 接收财付通通知的URL
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// 用户的openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="body"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="totalFee"></param>
        /// <param name="spbillCreateIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="tradeType"></param>
        /// <param name="openid"></param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        public TenPayV3RequestData(string appId, string mchId, string body, string outTradeNo, decimal totalFee, string spbillCreateIp, string notifyUrl, TenPayV3Type tradeType, string openid, string key, string nonceStr)
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Body = body ?? "test";
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIP = spbillCreateIp;
            NotifyUrl = notifyUrl;
            TradeType = tradeType;
            OpenId = openid;
            Key = key;
        }

        /// <summary>
        /// 获取拼接好的XML以及签名
        /// </summary>
        /// <returns></returns>
        public RequestHandler GetRequestHandler()
        {
            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

            var dataInfo = this;

            //设置package订单参数
            packageReqHandler.SetParameter("appid", dataInfo.AppId);          //公众账号ID
            packageReqHandler.SetParameter("mch_id", dataInfo.MchId);         //商户号
            packageReqHandler.SetParameter("nonce_str", dataInfo.NonceStr);                    //随机字符串
            packageReqHandler.SetParameter("body", dataInfo.NonceStr);    //商品信息
            packageReqHandler.SetParameter("out_trade_no", dataInfo.OutTradeNo);      //商家订单号
            packageReqHandler.SetParameter("total_fee", dataInfo.TotalFee.ToString());                    //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("spbill_create_ip", dataInfo.SpbillCreateIP);   //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", dataInfo.NotifyUrl);          //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", dataInfo.TradeType.ToString());                        //交易类型
            packageReqHandler.SetParameter("openid", dataInfo.OpenId);                      //用户的openId
            var sign = packageReqHandler.CreateMd5Sign("key", dataInfo.Key);
            packageReqHandler.SetParameter("sign", sign);                       //签名
            //string data = packageReqHandler.ParseXML();
            return packageReqHandler;
        }
    }
}
