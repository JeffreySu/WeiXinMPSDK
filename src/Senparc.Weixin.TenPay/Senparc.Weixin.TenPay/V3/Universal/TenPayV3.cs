﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
 
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

    修改标识：Senparc - 20161226
    修改描述：v14.3.111 增加OrderQuery,CloseOrder方法重载

    修改标识：Senparc - 20161226
    修改描述：v14.3.112 增加Reverse,RefundQuery,ShortUrl,MicroPay方法重载

    修改标识：Ritazh - 20161207
    修改描述：v14.3.112 迁移企业支付方法

    修改标识：Ritazh - 20161207
    修改描述：v14.3.112 迁移企业支付方法

    修改标识：Senparc - 20170215
    修改描述：v14.3.126 增加 Transfers和TransfersAsync方法重载

    修改标识：Senparc - 20170215
    修改描述：v14.3.126 增加 GetTransferInfo和GetTransferInfoAsync方法重载

    修改标识：Senparc - 20170215
    修改描述：v14.3.126 增加 DownloadBill和DownloadBillAsync方法重载
    
    修改标识：Senparc - 20170508
    修改描述：v14.4.6 1、修复企业付款接口无法指定证书的问题（TenpayV3.Transfers）
                      2、添加CertPost()及配套异步方法
    
    修改标识：Senparc - 20170815
    修改描述：v14.4.6 增加 ReverseAsync 方法重载
    
    修改标识：Senparc - 20170815
    修改描述：v14.6.1 撤销订单接口（TenPayV3.Reverse()）添加证书设置
        
    修改标识：Senparc - 20170916
    修改描述：v14.7.0 TenPayV3的接口添加对 UseSandBoxPay 的判断，可以自动使用沙箱

    修改标识：Senparc - 20180331
    修改描述：v14.10.12 添加TenpayV3的GetSignKey()接口，用于获取模拟支付环境下的签名。

    修改标识：Senparc - 20180416
    修改描述：v14.11.1 为 TenPayV3.GetTransferInfo() 及对应异步方法添加证书参数。

    修改标识：Senparc - 20190521
    修改描述：v1.4.0 .NET Core 添加多证书注册功能
 
    修改标识：hesi815 - 20200318
    修改描述：v1.5.401 实现分账接口，添加方法：MultiProfitSharing()、ProfitSharingFinish()、ProfitSharingAddReceiver()、ProfitSharingRemoveReceiver()、ProfitSharingQuery()

----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
 */

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.TenPay;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static partial class TenPayV3
    {
        #region 私有方法

#if NET45
        /// <summary>
        /// 带证书提交
        /// </summary>
        /// <param name="cert">证书绝对路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="data">数据</param>
        /// <param name="url">Url</param>
        /// <param name="timeOut">超时时间（毫秒）</param>
        /// <returns></returns>
        private static string CertPost(string cert, string certPassword, string data, string url, int timeOut = Config.TIME_OUT)
        {
            string password = certPassword;
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(dataBytes))
            {
                //调用证书
                X509Certificate2 cer = new X509Certificate2(cert, certPassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                string responseContent = RequestUtility.HttpPost(
                    CommonDI.CommonSP, 
                    url,
                    postStream: ms,
                    cer: cer,
                    timeOut: timeOut);

                return responseContent;
            }
        }


        /// <summary>
        /// 【异步方法】带证书提交
        /// </summary>
        /// <param name="cert">证书绝对路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="data">数据</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        private static async Task<string> CertPostAsync(string cert, string certPassword, string data, string url, int timeOut = Config.TIME_OUT)
        {
            string password = certPassword;
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(dataBytes))
            {
                //调用证书
                X509Certificate2 cer = new X509Certificate2(cert, certPassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                string responseContent = await RequestUtility.HttpPostAsync(
                    CommonDI.CommonSP,
                    url,
                    postStream: ms,
                    cer: cer,
                    timeOut: timeOut).ConfigureAwait(false);

                return responseContent;
            }
        }
#else
        /// <summary>
        /// 带证书提交
        /// </summary>
        /// <param name="mchId">商户号</param>
        /// <param name="subMchId">子商户号，如果没有则填 null 或空字符串</param>
        /// <param name="data">数据</param>
        /// <param name="url">Url</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private static string CertPost_NetCore(IServiceProvider serviceProvider, string mchId, string subMchId, string data, string url, int timeOut = Config.TIME_OUT)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(dataBytes))
            {
                var certName = TenPayHelper.GetRegisterKey(mchId, subMchId);
                string responseContent = RequestUtility.HttpPost(
                    serviceProvider,
                    url,
                    postStream: ms,
                    certName: certName,
                    timeOut: timeOut);

                return responseContent;
            }
        }


        /// <summary>
        /// 【异步方法】带证书提交
        /// </summary>
        /// <param name="mchId">商户号</param>
        /// <param name="subMchId">子商户号，如果没有则填 null 或空字符串</param>
        /// <param name="data">数据</param>
        /// <param name="url">Url</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private static async Task<string> CertPost_NetCoreAsync(IServiceProvider serviceProvider, string mchId, string subMchId, string data, string url, int timeOut = Config.TIME_OUT)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(dataBytes))
            {
                var certName = TenPayHelper.GetRegisterKey(mchId, subMchId);
                string responseContent = await RequestUtility.HttpPostAsync(
                    serviceProvider,
                    url,
                    postStream: ms,
                    certName: certName,
                    timeOut: timeOut).ConfigureAwait(false);

                return responseContent;
            }
        }
#endif
        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        #endregion

        #region 同步方法

        /// <summary>
        /// 获取验签秘钥API
        /// </summary>
        /// <param name="mchId">商户号</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static TenpayV3GetSignKeyResult GetSignKey(TenPayV3GetSignKeyRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var url = "https://api.mch.weixin.qq.com/sandboxnew/pay/getsignkey";

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, url, null, ms, timeOut: timeOut);
            return new TenpayV3GetSignKeyResult(resultXml);
        }

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
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UnifiedorderResult Unifiedorder(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);
        }

        /// <summary>
        /// H5支付接口（和“统一支付接口”近似）
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的Data数据，TradeType将会强制赋值为TenPayV3Type.MWEB</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UnifiedorderResult Html5Order(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            dataInfo.TradeType = TenPayV3Type.MWEB;
            return Unifiedorder(dataInfo, timeOut);
            /*var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");
            dataInfo.TradeType = TenPayV3Type.MWEB;

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);*/
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
            paySignReqHandler.SetParameter("signType", signType);
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
        [Obsolete("此方法已过期，建议使用 OrderQuery(TenPayV3OrderQueryData dataInfo)")]
        public static string OrderQuery(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/orderquery");
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }


        /// <summary>
        /// 订单查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static OrderQueryResult OrderQuery(TenPayV3OrderQueryRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/orderquery");
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new OrderQueryResult(resultXml);
        }

        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="data">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 CloseOrder(TenPayV3CloseOrderData dataInfo)")]
        public static string CloseOrder(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/closeorder");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }

        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <param name="dataInfo">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        public static CloseOrderResult CloseOrder(TenPayV3CloseOrderRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/closeorder");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new CloseOrderResult(resultXml);
        }

        /// <summary>
        /// 单次分账接口
        /// 服务商(单次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_1&index=1
        /// 境内普通商户(单次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_1&index=1
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param> 
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static ProfitSharingResult ProfitSharing
            (
            IServiceProvider serviceProvider, TenpayV3ProtfitSharingRequestData dataInfo,
#if NET45
           string cert, string certPassword,
#endif
            int timeOut = Config.TIME_OUT
            )
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/secapi/{0}pay/profitsharing");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            //var dataBytes = Encoding.UTF8.GetBytes(data);
            //using (MemoryStream ms = new MemoryStream(dataBytes))
            //{
            //调用证书
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new ProfitSharingResult(responseContent);
        }


        /// <summary>
        /// 多次分账接口
        /// 服务商(多次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_6&index=2
        /// 境内普通商户(多次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_6&index=2
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param> 
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static ProfitSharingResult MultiProfitSharing(
            IServiceProvider serviceProvider, TenpayV3ProtfitSharingRequestData dataInfo,
#if NET45
            string cert, string certPassword,
#endif
            int timeOut = Config.TIME_OUT
        )
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/secapi/{0}pay/multiprofitsharing");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            //var dataBytes = Encoding.UTF8.GetBytes(data);
            //using (MemoryStream ms = new MemoryStream(dataBytes))
            //{
            //调用证书
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new ProfitSharingResult(responseContent);
        }


        /// <summary>
        /// 完结分账
        /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_5&index=6
        /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_5&index=6
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProfitSharingResult ProfitSharingFinish(
            IServiceProvider serviceProvider, TenpayV3ProtfitSharingFinishRequestData dataInfo,
#if NET45
            string cert, string certPassword,
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/secapi/{0}pay/profitsharingfinish");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            //var dataBytes = Encoding.UTF8.GetBytes(data);
            //using (MemoryStream ms = new MemoryStream(dataBytes))
            //{
            //调用证书
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new ProfitSharingResult(responseContent);
        }

        /// <summary>
        /// 服务商代子商户发起添加分账接收方请求，
        /// 后续可通过发起分账请求将结算后的钱分到该分账接收方
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProfitSharingAddReceiverResult ProfitSharingAddReceiver(TenpayV3ProfitShareingAddReceiverRequestData dataInfo,
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/profitsharingaddreceiver");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
           // var resultXml = RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
            return new ProfitSharingAddReceiverResult(resultXml);
        }


        /// <summary>
        ///  删除分账接收方
        /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_4&index=5
        /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_4&index=5
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProfitSharingRemoveReceiverResult ProfitSharingRemoveReceiver(TenpayV3ProfitShareingRemoveReceiverRequestData dataInfo,
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/profitsharingremovereceiver");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            // var resultXml = RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
            return new ProfitSharingRemoveReceiverResult(resultXml);
        }



        /// <summary>
        /// 分账查询
        /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_2&index=3
        /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_2&index=3
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProfitSharingQueryResult ProfitSharingQuery(TenpayV3ProtfitSharingQueryRequestData dataInfo,
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/profitsharingquery");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            // var resultXml = RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
            return new ProfitSharingQueryResult(resultXml);
        }

        /// <summary>
        /// 撤销订单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 Reverse(TenPayV3ReverseRequestData dataInfo)")]
        public static string Reverse(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }


        /// <summary>
        /// 撤销订单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期 建议使用Reverse(TenPayV3ReverseRequestData dataInfo, string cert, string certPassword)")]
        public static ReverseResult Reverse(TenPayV3ReverseRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new ReverseResult(resultXml);
        }

        /// <summary>
        /// 撤销订单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param> 
        /// <param name="certPassword">证书密码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ReverseResult Reverse(
            IServiceProvider serviceProvider,
            TenPayV3ReverseRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            //var dataBytes = Encoding.UTF8.GetBytes(data);
            //using (MemoryStream ms = new MemoryStream(dataBytes))
            //{
            //调用证书
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new ReverseResult(responseContent);
            //  }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        //退款申请请可参考Senparc.Weixin.MP.Sample中的退款demo
        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static RefundResult Refund(
            IServiceProvider serviceProvider,
            TenPayV3RefundRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            //退款结果通知：https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=9_16&index=11

            //退款接口地址
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/refund");

            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            //string cert = cert;// @"F:\apiclient_cert.p12";
            //私钥（在安装证书时设置）

            var data = dataInfo.PackageRequestHandler.ParseXML();
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(dataBytes))
            {
                //调用证书
#if NET45
                string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
                string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
                return new RefundResult(responseContent);
            }
        }

        /// <summary>
        /// 退款查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 RefundQuery(TenPayV3RefundQueryRequestData dataInfo)")]
        public static string RefundQuery(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/refundquery");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }

        /// <summary>
        /// 退款查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static RefundQueryResult RefundQuery(TenPayV3RefundQueryRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/refundquery");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new RefundQueryResult(resultXml);
        }


        /// <summary>
        /// 对账单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 DownloadBill(TenPayV3DownloadBillRequestData dataInfo)")]
        public static string DownloadBill(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/downloadbill");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }

        /// <summary>
        /// 对账单接口
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_6
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static string DownloadBill(TenPayV3DownloadBillRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/downloadbill");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var useGzip = dataInfo.TarType == "GZIP";//使用GZIP压缩
            //TODO:根据GZIP参数判断是否压缩：https://github.com/JeffreySu/WeiXinMPSDK/issues/670


            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, encoding: Encoding.UTF8);
        }

        /// <summary>
        /// 短链接转换接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 ShortUrl(TenPayV3ShortUrlRequestData dataInfo)")]
        public static string ShortUrl(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }

        /// <summary>
        /// 短链接转换接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static ShortUrlResult ShortUrl(TenPayV3ShortUrlRequestData dataInfo)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new ShortUrlResult(resultXml);
        }

        /// <summary>
        /// 刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 MicroPay(TenPayV3MicroPayRequestData dataInfo)")]
        public static string MicroPay(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/micropay");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        }

        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 Transfers(TenPayV3TransfersRequestData dataInfo, int timeOut = Config.TIME_OUT)")]
        public static string Transfers(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/promotion/transfers");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
        }


        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static TransfersResult Transfers(
            IServiceProvider serviceProvider,
            TenPayV3TransfersRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/promotion/transfers");
            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new TransfersResult(responseContent);
        }

        /// <summary>
        /// 用于企业向向员工付款
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert"></param>
        /// <param name="certPassword"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static TransfersResult PayToWorker(
            IServiceProvider serviceProvider,
            TenPayV3PayToWorkerRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/paywwsptrans2pocket");
            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new TransfersResult(responseContent);
        }


        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 GetTransferInfo(TenPayV3GetTransferInfoRequestData dataInfo, int timeOut = Config.TIME_OUT)")]
        public static string GetTransferInfo(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/gettransferinfo");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
        }


        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。【请求需要双向证书】
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTransferInfoResult GetTransferInfo(
            IServiceProvider serviceProvider,
            TenPayV3GetTransferInfoRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            //文档：https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=14_3
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/gettransferinfo");

            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new GetTransferInfoResult(responseContent);
        }

        /// <summary>
        /// 用于查询付款记录
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert"></param>
        /// <param name="certPassword"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTransferInfoResult QueryPayLog(
            IServiceProvider serviceProvider,
            TenPayV3GetTransferInfoRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/querywwsptrans2pocket");

            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = CertPost_NetCore(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut);
#endif
            return new GetTransferInfoResult(responseContent);
        }


        /// <summary>
        /// 刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static MicropayResult MicroPay(TenPayV3MicroPayRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/micropay");

            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
            return new MicropayResult(resultXml);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 获取验签秘钥API
        /// </summary>
        /// <param name="mchId">商户号</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static async Task<TenpayV3GetSignKeyResult> GetSignKeyAsync(TenPayV3GetSignKeyRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var url = "https://api.mch.weixin.qq.com/sandboxnew/pay/getsignkey";

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, url, null, ms, timeOut: timeOut).ConfigureAwait(false);
            return new TenpayV3GetSignKeyResult(resultXml);
        }

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
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UnifiedorderResult> UnifiedorderAsync(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            await ms.WriteAsync(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut).ConfigureAwait(false);
            return new UnifiedorderResult(resultXml);
        }

        /// <summary>
        /// 【异步方法】H5支付接口（和“统一支付接口”近似）
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的Data数据，TradeType将会强制赋值为TenPayV3Type.MWEB</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UnifiedorderResult> Html5OrderAsync(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            dataInfo.TradeType = TenPayV3Type.MWEB;
            return await UnifiedorderAsync(dataInfo, timeOut).ConfigureAwait(false);
            /*var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");
            dataInfo.TradeType = TenPayV3Type.MWEB;

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            await ms.WriteAsync(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);*/
        }

        //退款申请请可参考Senparc.Weixin.MP.Sample中的退款demo
        /// <summary>
        /// 【异步方法】退款申请接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static async Task<RefundResult> RefundAsync(
            IServiceProvider serviceProvider,
            TenPayV3RefundRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var data = dataInfo.PackageRequestHandler.ParseXML();

            //var urlFormat = "https://api.mch.weixin.qq.com/secapi/pay/refund";

            //退款接口地址
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/refund");
            //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
            //string cert = cert;// @"F:\apiclient_cert.p12";
            //私钥（在安装证书时设置）
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = await CertPost_NetCoreAsync(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut).ConfigureAwait(false);
#endif
            return new RefundResult(responseContent);
        }

        /// <summary>
        /// 【异步方法】订单查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 OrderQueryAsync(TenPayV3OrderQueryData dataInfo)")]
        public static async Task<string> OrderQueryAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/orderquery");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】订单查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>

        public static async Task<OrderQueryResult> OrderQueryAsync(TenPayV3OrderQueryRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/orderquery");
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new OrderQueryResult(resultXml);
        }

        /// <summary>
        /// 【异步方法】关闭订单接口
        /// </summary>
        /// <param name="data">关闭订单需要post的xml数据</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 CloseOrderAsync(TenPayV3CloseOrderData dataInfo)")]
        public static async Task<string> CloseOrderAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/closeorder");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】关闭订单接口
        /// </summary>
        /// <param name="dataInfo">关闭订单需要post的xml数据</param>
        /// <returns></returns>

        public static async Task<CloseOrderResult> CloseOrderAsync(TenPayV3CloseOrderRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/closeorder");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new CloseOrderResult(resultXml);
        }


        /// <summary>
        /// 【异步方法】撤销订单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用  ReverseAsync(TenPayV3ReverseRequestData dataInfo)")]
        public static async Task<string> ReverseAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】撤销订单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期 建议使用 ReverseAsync(TenPayV3ReverseRequestData dataInfo, string cert, string certPassword)")]
        public static async Task<ReverseResult> ReverseAsync(TenPayV3ReverseRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resutlXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new ReverseResult(resutlXml);
        }

        /// <summary>
        /// 【异步方法】撤销订单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书绝对路径，如@"F:\apiclient_cert.p12"</param> 
        /// <param name="certPassword">证书密码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ReverseResult> ReverseAsync(
            IServiceProvider serviceProvider,
            TenPayV3ReverseRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}secapi/pay/reverse");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            //var dataBytes = Encoding.UTF8.GetBytes(data);
            //using (MemoryStream ms = new MemoryStream(dataBytes))
            //{
            //调用证书
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = await CertPost_NetCoreAsync(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut).ConfigureAwait(false);
#endif
            return new ReverseResult(responseContent);
            //}
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
        //    return Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms);
        //}

        /// <summary>
        /// 【异步方法】退款查询接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用  RefundQueryAsync(TenPayV3RefundQueryRequestData dataInfo)")]
        public static async Task<string> RefundQueryAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/refundquery");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】退款查询接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static async Task<RefundQueryResult> RefundQueryAsync(TenPayV3RefundQueryRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/refundquery");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new RefundQueryResult(resultXml);
        }

        /// <summary>
        /// 【异步方法】对账单接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用 DownloadBillAsync(TenPayV3DownloadBillRequestData dataInfo)")]
        public static async Task<string> DownloadBillAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/downloadbill");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】对账单接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static async Task<string> DownloadBillAsync(TenPayV3DownloadBillRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/downloadbill");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】短链接转换接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用  ShortUrlAsync(TenPayV3ShortUrlRequestData dataInfo)")]
        public static async Task<string> ShortUrlAsync(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】短链接转换接口
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static async Task<ShortUrlResult> ShortUrlAsync(TenPayV3ShortUrlRequestData dataInfo)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/tools/shorturl";
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new ShortUrlResult(resultXml);
        }

        /// <summary>
        /// 【异步方法】刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用  MicroPayAsync(TenPayV3MicroPayRequestData dataInfo)")]
        public static async Task<string> MicroPayAsync(string data)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/micropay");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用  TransfersAsync(TenPayV3TransfersRequestData dataInfo, int timeOut = Config.TIME_OUT)")]
        public static async Task<string> TransfersAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/promotion/transfers");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<TransfersResult> TransfersAsync(
            IServiceProvider serviceProvider,
            TenPayV3TransfersRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/promotion/transfers");
            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = await CertPost_NetCoreAsync(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut).ConfigureAwait(false);
#endif
            return new TransfersResult(responseContent);
        }

        /// <summary>
        /// 【异步方法】用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("此方法已过期，建议使用GetTransferInfoAsync(TenPayV3GetTransferInfoRequestData dataInfo, int timeOut = Config.TIME_OUT)")]
        public static async Task<string> GetTransferInfoAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/gettransferinfo");

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTransferInfoResult> GetTransferInfoAsync(
            IServiceProvider serviceProvider,
            TenPayV3GetTransferInfoRequestData dataInfo,
#if NET45
            string cert, string certPassword, 
#endif
            int timeOut = Config.TIME_OUT)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaymkttransfers/gettransferinfo");
            var data = dataInfo.PackageRequestHandler.ParseXML();
#if NET45
            string responseContent = CertPost(cert, certPassword, data, urlFormat, timeOut);
#else
            string responseContent = await CertPost_NetCoreAsync(serviceProvider, dataInfo.MchId, dataInfo.SubMchId, data, urlFormat, timeOut).ConfigureAwait(false);
#endif
            return new GetTransferInfoResult(responseContent);
        }


        /// <summary>
        /// 【异步方法】刷卡支付
        /// 提交被扫支付
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public static async Task<MicropayResult> MicroPayAsync(TenPayV3MicroPayRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/micropay");
            var data = dataInfo.PackageRequestHandler.ParseXML();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = await RequestUtility.HttpPostAsync(CommonDI.CommonSP, urlFormat, null, ms).ConfigureAwait(false);
            return new MicropayResult(resultXml);
        }

        #endregion
    }
}
