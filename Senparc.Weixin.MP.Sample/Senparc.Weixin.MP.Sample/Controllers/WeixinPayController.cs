using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// 根据官方的Webforms Demo改写，所以可以看到直接Response.Write()之类的用法，实际项目中不提倡这么做。
    /// </summary>
    public class WeixinPayController : Controller
    {
        private static TenPayInfo _weixinPayInfo;

        public static TenPayInfo WeixinPayInfo
        {
            get
            {
                if (_weixinPayInfo == null)
                {
                    _weixinPayInfo =
                        TenPayInfoCollection.Data[System.Configuration.ConfigurationManager.AppSettings["WeixinPay_PartnerId"]];
                }
                return _weixinPayInfo;
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
            resHandler.SetKey(WeixinPayInfo.Key, WeixinPayInfo.AppKey);

            //判断签名
            if (resHandler.IsWXsignfeedback())
            {
                //回复服务器处理成功
                Response.Write("OK");
                Response.Write("OK:" + resHandler.GetDebugInfo());
            }
            else
            {
                //sha1签名失败
                Response.Write("fail");
                Response.Write("fail:" + resHandler.GetDebugInfo());
            }
            return null;
        }

        public ActionResult JsApi()
        {
            String appId = WeixinPayInfo.AppId;
            String timeStamp = "";
            String nonceStr = "";
            String packageValue = "";
            String paySign = "";


            string sp_billno = Request["order_no"];
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
            }
            else
            {
                sp_billno = Request["order_no"].ToString();
            }

            sp_billno = WeixinPayInfo.PartnerId + sp_billno;

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();


            //设置package订单参数
            packageReqHandler.SetParameter("partner", WeixinPayInfo.PartnerId);		  //商户号
            packageReqHandler.SetParameter("fee_type", "1");                    //币种，1人民币
            packageReqHandler.SetParameter("input_charset", "GBK");
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("notify_url", WeixinPayInfo.TenPayNotify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("body", "JSAPIdemo");	                    //商品描述
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

            //获取package包
            packageValue = packageReqHandler.GetRequestURL();

            //调起微信支付签名
            timeStamp = TenPayUtil.GetTimestamp();
            nonceStr = TenPayUtil.GetNoncestr();

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appid", appId);
            paySignReqHandler.SetParameter("appkey", WeixinPayInfo.AppKey);
            paySignReqHandler.SetParameter("noncestr", nonceStr);
            paySignReqHandler.SetParameter("timestamp", timeStamp);
            paySignReqHandler.SetParameter("package", packageValue);
            //paySignReqHandler.SetParameter("appid", "wxd930ea5d5a258f4f");
            //paySignReqHandler.SetParameter("appkey", "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K");
            //paySignReqHandler.SetParameter("noncestr", "e7d161ac8d8a76529d39d9f5b4249ccb");
            //paySignReqHandler.SetParameter("timestamp", "1399514976");
            //paySignReqHandler.SetParameter("traceid", "test_1399514976");
            //paySignReqHandler.SetParameter("package", "bank_type=WX&body=%E6%94%AF%E4%BB%98%E6%B5%8B%E8%AF%95&fee_type=1&input_charset=UTF-8&notify_url=http%3A%2F%2Fweixin.qq.com&out_trade_no=7240b65810859cbf2a8d9f76a638c0a3&partner=1900000109&spbill_create_ip=196.168.1.1&total_fee=1&sign=7F77B507B755B3262884291517E380F8");
            paySign = paySignReqHandler.CreateSHA1Sign();



            //获取debug信息,建议把请求和debug信息写入日志，方便定位问题
            //string pakcageDebuginfo = packageReqHandler.getDebugInfo();
            //Response.Write("<br/>pakcageDebuginfo:" + pakcageDebuginfo + "<br/>");
            //string paySignDebuginfo = paySignReqHandler.getDebugInfo();
            //Response.Write("<br/>paySignDebuginfo:" + paySignDebuginfo + "<br/>");

            ViewData["appId"] = appId;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["packageValue"] = packageValue;
            ViewData["paySign"] = paySign;

            return View();
        }


    }
}
