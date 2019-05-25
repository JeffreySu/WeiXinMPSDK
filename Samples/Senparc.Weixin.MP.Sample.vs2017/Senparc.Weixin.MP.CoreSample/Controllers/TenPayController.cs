/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：TenPayController.cs
    文件功能描述：微信支付Controller
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE TenPay
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.TenPay.V2;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 根据官方的Webforms Demo改写，所以可以看到直接result +=)之类的用法，实际项目中不提倡这么做。
    /// 注意：此Controller中的Demo为早期的微信支付，最新的V3微信支付请见TenPayV3Controller
    /// </summary>
    public class TenPayController : Controller
    {
        private static TenPayInfo _tenPayInfo;

        public static TenPayInfo TenPayInfo
        {
            get
            {
                if (_tenPayInfo == null)
                {
                    _tenPayInfo = 
                        TenPayInfoCollection.Data[Config.SenparcWeixinSetting.WeixinPay_PartnerId];
                }
                return _tenPayInfo;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedBack()
        {
            //创建支付应答对象
            ResponseHandler resHandler = new ResponseHandler(null);
            resHandler.Init();
            resHandler.SetKey(TenPayInfo.Key, TenPayInfo.AppKey);

            string result = String.Empty;

            //判断签名
            if (resHandler.IsWXsignfeedback())
            {
                //回复服务器处理成功
                result+="OK";
                result+= "OK:" + resHandler.GetDebugInfo();
            }
            else
            {
                //sha1签名失败
                result+= "fail";
                result+= "fail:" + resHandler.GetDebugInfo();
            }
            return Content(result);
        }

        public ActionResult JsApi()
        {
            //string appId = TenPayInfo.AppId;
            string timeStamp = "";
            string nonceStr = "";
            string packageValue = "";
            string paySign = "";

            string sp_billno = Request.Form["order_no"];
            //当前时间 yyyyMMdd
            string date = SystemTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request.Form["order_no"].ToString();
            }

            sp_billno = TenPayInfo.PartnerId + sp_billno;

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();
            packageReqHandler.SetKey(TenPayInfo.Key);

            //设置package订单参数
            packageReqHandler.SetParameter("partner", TenPayInfo.PartnerId);		  //商户号
            packageReqHandler.SetParameter("fee_type", "1");                    //币种，1人民币
            packageReqHandler.SetParameter("input_charset", "GBK");
            packageReqHandler.SetParameter("bank_type", "WX");
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("notify_url", TenPayInfo.TenPayNotify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("body", "JSAPIdemo");	                    //商品描述
            packageReqHandler.SetParameter("spbill_create_ip", HttpContext.UserHostAddress()?.ToString());   //用户的公网ip，不是商户服务器IP

            //获取package包
            packageValue = packageReqHandler.GetRequestURL();

            //调起微信支付签名
            timeStamp = TenPayUtil.GetTimestamp();
            nonceStr = TenPayUtil.GetNoncestr();

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appid", TenPayInfo.AppId);
            paySignReqHandler.SetParameter("appkey", TenPayInfo.AppKey);
            paySignReqHandler.SetParameter("noncestr", nonceStr);
            paySignReqHandler.SetParameter("timestamp", timeStamp);
            paySignReqHandler.SetParameter("package", packageValue);
            paySign = paySignReqHandler.CreateSHA1Sign();
            //TenPay.Delivernotify(TenPayInfo.AppId, "oX99MDgNcgwnz3zFN3DNmo8uwa-w", "111112222233333", sp_billno,
            //                     timeStamp, "1", "ok", "53cca9d47b883bd4a5c85a9300df3da0cb48565c", "sha1");


            //获取debug信息,建议把请求和debug信息写入日志，方便定位问题
            //string pakcageDebuginfo = packageReqHandler.getDebugInfo();
            //result +="<br/>pakcageDebuginfo:" + pakcageDebuginfo + "<br/>");
            //string paySignDebuginfo = paySignReqHandler.getDebugInfo();
            //result +="<br/>paySignDebuginfo:" + paySignDebuginfo + "<br/>");

            //TODO：和JSSDK一样整合信息包
            ViewData["appId"] = TenPayInfo.AppId;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["packageValue"] = packageValue;
            ViewData["paySign"] = paySign;

            return View();
        }


        public ActionResult Native()
        {
            string sp_billno = Request.Form["order_no"];
            //当前时间 yyyyMMdd
            string date = SystemTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request.Form["order_no"].ToString();
            }

            sp_billno = TenPayInfo.PartnerId + sp_billno;


            RequestHandler outParams = new RequestHandler(null);

            outParams.Init();
            string productid = sp_billno;
            string timeStamp = TenPayUtil.GetTimestamp();
            string nonceStr = TenPayUtil.GetNoncestr();

            RequestHandler Params = new RequestHandler(null);
            Params.SetParameter("appid", TenPayInfo.AppId);
            Params.SetParameter("appkey", TenPayInfo.AppKey);
            Params.SetParameter("noncestr", nonceStr);
            Params.SetParameter("timestamp", timeStamp);
            Params.SetParameter("productid", productid);
            string sign = Params.CreateSHA1Sign();
            Params.SetParameter("sign", sign);

            var parm = TenPay.V2.TenPay.NativePay(TenPayInfo.AppId, timeStamp, nonceStr, productid, sign);
            parm = QRCode.QRfromGoogle(parm);
            ViewData["parm"] = parm;
            return View();
        }

        public ActionResult NativeCall()
        {
            string sp_billno = Request.Form["order_no"];
            //当前时间 yyyyMMdd
            string date = SystemTime.Now.ToString("yyyyMMdd");
            //订单号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            string out_trade_no = "" + SystemTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request.Form["order_no"].ToString();
            }

            sp_billno = TenPayInfo.PartnerId + sp_billno;



            //创建RequestHandler实例
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();
            packageReqHandler.SetKey(TenPayInfo.Key);

            //设置package订单参数
            packageReqHandler.SetParameter("partner", TenPayInfo.PartnerId);		  //商户号
            packageReqHandler.SetParameter("bank_type", "WX");		                      //银行类型
            packageReqHandler.SetParameter("fee_type", "1");                    //币种，1人民币
            packageReqHandler.SetParameter("input_charset", "GBK");
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("notify_url", TenPayInfo.TenPayNotify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("body", "nativecall");	                    //商品描述
            packageReqHandler.SetParameter("spbill_create_ip", "8.8.8.8"/*Page.Request.UserHostAddress*/);   //用户的公网ip，不是商户服务器IP

            //获取package包
            string packageValue = packageReqHandler.GetRequestURL();

            //调起微信支付签名
            string timeStamp = TenPayUtil.GetTimestamp();
            string nonceStr = TenPayUtil.GetNoncestr();

            //设置支付参数
            RequestHandler payHandler = new RequestHandler(null);
            payHandler.SetParameter("appid", TenPayInfo.AppId);
            payHandler.SetParameter("noncestr", nonceStr);
            payHandler.SetParameter("timestamp", timeStamp);
            payHandler.SetParameter("package", packageValue);
            payHandler.SetParameter("RetCode", "0");
            payHandler.SetParameter("RetErrMsg", "成功");
            string paySign = payHandler.CreateSHA1Sign();
            payHandler.SetParameter("app_signature", paySign);
            payHandler.SetParameter("sign_method", "SHA1");


            Response.ContentType = "text/xml";
            Response.Clear();
            ViewData["payHandler"] = payHandler.ParseXML();

            return View();
        }

        public ActionResult Alert()
        {
            return Content("success");
        }

        public ActionResult PayNotifyUrl()
        {

            ResponseHandler resHandler = new ResponseHandler(null);
            resHandler.Init();
            resHandler.SetKey(TenPayInfo.Key, TenPayInfo.AppKey);

            string message;
            
            //判断签名
            if (resHandler.IsTenpaySign())
            {
                if (resHandler.IsWXsign())
                {
                    //商户在收到后台通知后根据通知ID向财付通发起验证确认，采用后台系统调用交互模式
                    string notify_id = resHandler.GetParameter("notify_id");
                    //取结果参数做业务处理
                    string out_trade_no = resHandler.GetParameter("out_trade_no");
                    //财付通订单号
                    string transaction_id = resHandler.GetParameter("transaction_id");
                    //金额,以分为单位
                    string total_fee = resHandler.GetParameter("total_fee");
                    //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                    string discount = resHandler.GetParameter("discount");
                    //支付结果
                    string trade_state = resHandler.GetParameter("trade_state");

                    string payMessage = null;

                    //即时到账
                    if ("0".Equals(trade_state))
                    {
                        //------------------------------
                        //处理业务开始
                        //------------------------------

                        //处理数据库逻辑
                        //注意交易单不要重复处理
                        //注意判断返回金额

                        //------------------------------
                        //处理业务完毕
                        //------------------------------

                        //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                        payMessage = "success 后台通知成功";
                    }
                    else
                    {
                        payMessage = "支付失败";
                    }
                    ViewData["payMessage"] = payMessage;
                    //回复服务器处理成功
                    message = "success";
                }

                else
                {//SHA1签名失败
                    message = "SHA1签名失败" + resHandler.GetDebugInfo();
                }
            }

            else
            {//md5签名失败
                message = "md5签名失败" + resHandler.GetDebugInfo();
            }
            ViewData["message"] = message;

            return Content("Success");
        }

        protected ActionResult Refund()
        {
            //创建请求对象
            RefundRequestHandler reqHandler = new RefundRequestHandler(null);

            //通信对象
            TenPayHttpClient httpClient = new TenPayHttpClient();

            //应答对象
            ClientResponseHandler resHandler = new ClientResponseHandler();

            //-----------------------------
            //设置请求参数
            //-----------------------------
            reqHandler.Init();
            reqHandler.SetKey(TenPayInfo.Key);

            reqHandler.SetParameter("partner", TenPayInfo.PartnerId);
            //out_trade_no和transaction_id至少一个必填，同时存在时transaction_id优先
            //reqHandler.setParameter("out_trade_no", "1458268681");
            reqHandler.SetParameter("transaction_id", "1900000109201103020030626316");
            reqHandler.SetParameter("out_refund_no", "2011030201");
            reqHandler.SetParameter("total_fee", "1");
            reqHandler.SetParameter("refund_fee", "1");
            reqHandler.SetParameter("refund_fee", "1");
            reqHandler.SetParameter("op_user_id", "1900000109");
            reqHandler.SetParameter("op_user_passwd", EncryptHelper.GetMD5("111111", "GBK"));
            reqHandler.SetParameter("service_version", "1.1");

            string requestUrl = reqHandler.GetRequestURL();
            httpClient.SetCertInfo("c:\\key\\1900000109.pfx", "1900000109");
            //设置请求内容
            httpClient.SetReqContent(requestUrl);
            //设置超时
            httpClient.SetTimeOut(10);

            string rescontent = "";

            string result = String.Empty;

            //后台调用
            if (httpClient.Call())
            {
                //获取结果
                rescontent = httpClient.GetResContent();

                resHandler.SetKey(TenPayInfo.Key);
                //设置结果参数
                resHandler.SetContent(rescontent);

                //判断签名及结果
                if (resHandler.IsTenpaySign() && resHandler.GetParameter("retcode") == "0")
                {
                    //商户订单号
                    string out_trade_no = resHandler.GetParameter("out_trade_no");
                    //财付通订单号
                    string transaction_id = resHandler.GetParameter("transaction_id");

                    //业务处理
                    result +="OK,transaction_id=" + resHandler.GetParameter("transaction_id") + "<br>";
                }
                else
                {
                    //错误时，返回结果未签名。
                    //如包格式错误或未确认结果的，请使用原来订单号重新发起，确认结果，避免多次操作
                    result +="业务错误信息或签名错误:" + resHandler.GetParameter("retcode") + "," + resHandler.GetParameter("retmsg") + "<br>";
                }

            }
            else
            {
                //后台调用通信失败
                result +="call err:" + httpClient.GetErrInfo() + "<br>" + httpClient.GetResponseCode() + "<br>";
                //有可能因为网络原因，请求已经处理，但未收到应答。
            }


            //获取debug信息,建议把请求、应答内容、debug信息，通信返回码写入日志，方便定位问题

            result +="http res:" + httpClient.GetResponseCode() + "," + httpClient.GetErrInfo() + "<br>";
            result +="req url:" + requestUrl + "<br/>";
            result +="req debug:" + reqHandler.GetDebugInfo() + "<br/>";
            result +="res content:" + rescontent.HtmlEncode() + "<br/>";
            result +="res debug:" + resHandler.GetDebugInfo().HtmlEncode() + "<br/>";

            return Content(result);
        }

        public ActionResult Delivernotify()
        {
            string timeStamp = "";
            string appSignature = "";
            //string appId, string openId, string transId, string out_Trade_No, string deliver_TimesTamp, string deliver_Status, string deliver_Msg, string app_Signature, 
            string sp_billno = Request.Form["order_no"];
            //当前时间 yyyyMMdd
            string date = SystemTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request.Form["order_no"].ToString();
            }

            sp_billno = TenPayInfo.PartnerId + sp_billno;

            //调起微信支付签名
            timeStamp = TenPayUtil.GetTimestamp();

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appid", TenPayInfo.AppId);
            paySignReqHandler.SetParameter("openId", TenPayInfo.AppKey);
            paySignReqHandler.SetParameter("transId", "111112222233333");
            paySignReqHandler.SetParameter("deliver_TimesTamp", timeStamp);
            paySignReqHandler.SetParameter("out_Trade_No", sp_billno);
            paySignReqHandler.SetParameter("deliver_Status", "1");
            paySignReqHandler.SetParameter("deliver_Msg", "ok");
            appSignature = paySignReqHandler.CreateSHA1Sign();
            var result = TenPay.V2.TenPay.Delivernotify(TenPayInfo.AppId, "oX99MDgNcgwnz3zFN3DNmo8uwa-w", "111112222233333", sp_billno,
                                 timeStamp, "1", "ok", appSignature, "sha1");

            ViewData["message"] = result.errcode;
            return View();
        }

        public ActionResult SharedAddress()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(TenPayInfo.AppId, "49b71198b776e18521659a32a97501a6");

            string timeStamp = TenPayUtil.GetTimestamp();
            string nonceStr = TenPayUtil.GetNoncestr();

            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("accessToken", accessToken);
            paySignReqHandler.SetParameter("appid", TenPayInfo.AppId);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("url", TenPayInfo.TenPayNotify);
            var addrSign = paySignReqHandler.CreateSHA1Sign();

            ViewData["appId"] = TenPayInfo.AppId;
            ViewData["addrSign"] = addrSign;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;

            return View();
        }
    }
}
