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
    修改描述：v14.3.110 增加Unifiedorder方法重载和TenPayV3RequestData类

    修改标识：Senparc - 20161204
    修改描述：v14.3.111 启用TenPayV3Result用于解析统一下单返回Xml数据

    修改标识：Senparc - 20161205
    修改描述：v14.3.110 增加UnifiedorderAsync方法重载

    修改标识：Ritazh - 20161207
    修改描述：v14.3.112 迁移企业支付方法

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
        [Obsolete("此方法已过期，建议使用 Unifiedorder(TenPayV3XmlDataInfo dataInfo, int timeOut = Config.TIME_OUT)")]
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
        public static UnifiedorderResult Unifiedorder(TenPayV3RequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML

            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);
        }

        /// <summary>
        /// 获取UI使用的JS支付签名
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="timeStamp"></param>
        /// <param name="nonceStr"></param>
        /// <param name="package">格式：prepay_id={0}</param>
        /// <param name="signType"></param>
        /// <returns></returns>
        public static string GetJsPaySign(string appId, string timeStamp, string nonceStr, string package, string key,
            string signType = "MD5")
        {
            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appId", appId);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("package", package);
            paySignReqHandler.SetParameter("signType", "MD5");
            var paySign = paySignReqHandler.CreateMd5Sign("key", key);
            return paySign;
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

        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Transfers(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string GetTransferInfo(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
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
        [Obsolete("此方法已过期，建议使用 Unifiedorder(TenPayV3XmlDataInfo dataInfo, int timeOut = Config.TIME_OUT)")]
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
        /// 【异步方法】统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UnifiedorderResult> UnifiedorderAsync(TenPayV3RequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            await ms.WriteAsync(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);
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

        /// <summary>
        ///【异步方法】 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> TransfersAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> GetTransferInfoAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(urlFormat, null, ms, timeOut: timeOut);
        }
        #endregion
    }
}
