/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：TenPayV3.cs
    文件功能描述：微信支付V3接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160707
    修改描述：增加撤销订单接口：Reverse

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
              
    修改标识：Senparc - 20161202
    修改描述：v14.3.109 命名空间由Senparc.Weixin.MP.AdvancedAPIs改为Senparc.Weixin.MP.TenPayLibV3

    修改标识：Senparc - 20161203
    修改描述：v14.3.110 增加Unifiedorder方法重载和TenPayV3XmlDataInfo类

    修改标识：Senparc - 20161204
    修改描述：v14.3.111 增加AnalyUnifiedorderXmlData用于解析统一下单返回Xml数据
----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
 */

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付XMLData数据[统一下单]
    /// </summary>
    public class TenPayV3XmlDataInfo
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
        public TenPayV3XmlDataInfo(string appId, string mchId, string body, string outTradeNo, decimal totalFee, string spbillCreateIp, string notifyUrl, TenPayV3Type tradeType, string openid, string key, string nonceStr)
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
    }

    /// <summary>
    ///  微信支付ReturnData[统一下单]
    /// </summary>
    public class TenPayV3ReturnData
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public TenPayV3CodeState ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnMsg { get; set; }

        public TenPayV3ResultCode ResultData { get; set; }

        public TenPayV3ReturnData(TenPayV3CodeState returnCode, string returnMsg = null, TenPayV3ResultCode resultData = null)
        {
            ReturnCode = returnCode;
            ReturnMsg = returnMsg;
            ResultData = resultData;
        }
    }
    /// <summary>
    ///  微信支付ResultData[统一下单]
    /// </summary>
    public class TenPayV3ResultCode
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
        /// 终端设备号[非必填]
        /// </summary>
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 业务结果[SUCCESS/FAIL]
        /// </summary>
        public TenPayV3CodeState ResultCode { get; set; }
        /// <summary>
        /// 错误代码[非必填]
        /// </summary>
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误代码描述[非必填]
        /// </summary>
        public string ErrCodeDes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TenPayV3SuccessData SuccessData { get; set; }
        public TenPayV3ResultCode(string appId, string mchId, string nonceStr, string sign, TenPayV3CodeState resultCode, string errCode = null, string errCodeDes = null, string deviceInfo = null, TenPayV3SuccessData successData = null)
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            DeviceInfo = deviceInfo;
            Sign = sign;
            ResultCode = resultCode;
            ErrCode = errCode;
            ErrCodeDes = errCodeDes;
            SuccessData = successData;
        }
    }
    /// <summary>
    /// 微信支付Success数据[统一下单]
    /// </summary>
    public class TenPayV3SuccessData
    {
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// 微信生成的预支付回话标识
        /// </summary>
        public string PrepayId { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string CodeUrl { get; set; }

        public TenPayV3SuccessData(TenPayV3Type tradeType, string prepayId, string codeUrl = null)
        {
            TradeType = tradeType;
            PrepayId = prepayId;
            CodeUrl = codeUrl;
        }
    }

    /// <summary>
    /// 微信支付接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static class TenPayV3
    {
        #region 同步请求


        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期建议使用 Unifiedorder(TenPayV3XmlDataInfo dataInfo, int timeOut = Config.TIME_OUT)")]
        public static string Unifiedorder(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static TenPayV3ReturnData Unifiedorder(TenPayV3XmlDataInfo dataInfo, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

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
            string sign = packageReqHandler.CreateMd5Sign("key", dataInfo.Key);
            packageReqHandler.SetParameter("sign", sign);                       //签名
            string data = packageReqHandler.ParseXML();

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
            return AnalyUnifiedorderXmlData(resultXml);
        }

        /// <summary>
        /// 解析统一支付接口返回的Xml数据
        /// </summary>
        /// <param name="resultXml">统一订单接口返回的Xml数据</param>
        /// <returns></returns>
        public static TenPayV3ReturnData AnalyUnifiedorderXmlData(string resultXml)
        {
            try
            {
                var res = XDocument.Parse(resultXml);
                var returnCode =
                   (TenPayV3CodeState)Enum.Parse(typeof(TenPayV3CodeState), res.Element("xml").Element("return_code").Value);
                var data = new TenPayV3ReturnData(returnCode);
                if (returnCode == TenPayV3CodeState.FAIL)
                {
                    data.ReturnMsg = res.Element("xml").Element("return_msg").Value;
                    return data;
                }
                var appId = res.Element("xml").Element("appid").Value;
                var mchId = res.Element("xml").Element("mch_id").Value;
                var deviceInfo = res.Element("xml").Element("device_info").Value ?? "";
                var nonceStr = res.Element("xml").Element("nonce_str").Value;
                var sign = res.Element("xml").Element("sign").Value;
                var errCode = res.Element("xml").Element("err_code").Value ?? "";
                var errCodeDes = res.Element("xml").Element("err_code_des").Value ?? "";

                var resultCode = (TenPayV3CodeState)Enum.Parse(typeof(TenPayV3CodeState), res.Element("xml").Element("result_code").Value);
                var result = new TenPayV3ResultCode(appId, mchId, nonceStr, sign, resultCode, errCode, errCodeDes, deviceInfo);
                if (returnCode == TenPayV3CodeState.SUCCESS)
                {
                    var tradetype =
               (TenPayV3Type)Enum.Parse(typeof(TenPayV3Type), res.Element("xml").Element("trade_type").Value);
                    var prepayId = res.Element("xml").Element("prepay_id").Value;
                    var codeurl = tradetype == TenPayV3Type.NATIVE ? res.Element("xml").Element("code_url").Value : "";
                    result.SuccessData = new TenPayV3SuccessData(tradetype, prepayId, codeurl);
                }
                data.ResultData = result;
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(resultXml);
            }
        }



        /// <summary>
        /// Native
        /// </summary>
        /// <param name="appId">开放平台账户的唯一标识</param>
        /// <param name="timesTamp">时间戳</param>
        /// <param name="mchId">商户Id</param>
        /// <param name="nonceStr">32 位内的随机串，防重发</param>
        /// <param name="productId">商品唯一id</param>
        /// <param name="sign">签名</param>
        public static string NativePay(string appId, string timesTamp, string mchId, string nonceStr, string productId, string sign)
        {
            var urlFormat = "weixin://wxpay/bizpayurl?sign={0}&appid={1}&mch_id={2}&product_id={3}&time_stamp={4}&nonce_str={5}";
            var url = string.Format(urlFormat, sign, appId, mchId, productId, timesTamp, nonceStr);

            return url;
        }

        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string OrderQuery(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/orderquery";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="data">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        public static string CloseOrder(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/closeorder";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        /// <summary>
        /// 撤销订单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Reverse(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        //退款申请请直接参考Senparc.Weixin.MP.Sample中的退款demo
        ///// <summary>
        ///// 退款申请接口
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static string Refund(string data)
        //{
        //    var urlFormat = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        //    var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
        //    MemoryStream ms = new MemoryStream();
        //    ms.Write(formDataBytes, 0, formDataBytes.Length);
        //    ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        //    return Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(urlFormat, null, ms);
        //}

        /// <summary>
        /// 退款查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RefundQuery(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/refundquery";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        /// <summary>
        /// 对账单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DownloadBill(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/downloadbill";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        /// <summary>
        /// 短链接转换接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ShortUrl(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }

        /// <summary>
        /// 刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MicroPay(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/micropay";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms);
        }
        #endregion

        #region 异步请求
        /// <summary>
        /// 【异步方法】统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> UnifiedorderAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】订单查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> OrderQueryAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/orderquery";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        /// <summary>
        /// 【异步方法】关闭订单接口
        /// </summary>
        /// <param name="data">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        public static async Task<string> CloseOrderAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/closeorder";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        /// <summary>
        /// 【异步方法】撤销订单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> ReverseAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        //退款申请请直接参考Senparc.Weixin.MP.Sample中的退款demo
        ///// <summary>
        ///// 退款申请接口
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static string Refund(string data)
        //{
        //    var urlFormat = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        //    var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
        //    MemoryStream ms = new MemoryStream();
        //    ms.Write(formDataBytes, 0, formDataBytes.Length);
        //    ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        //    return Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(urlFormat, null, ms);
        //}

        /// <summary>
        /// 【异步方法】退款查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> RefundQueryAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/refundquery";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        /// <summary>
        /// 【异步方法】对账单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> DownloadBillAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/downloadbill";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        /// <summary>
        /// 【异步方法】短链接转换接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> ShortUrlAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }

        /// <summary>
        /// 【异步方法】刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> MicroPayAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/micropay";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms);
        }
        #endregion
    }
}
