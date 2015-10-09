using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// JSSDK的演示
    /// </summary>
    public class WeixinJSSDKController : Controller
    {
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];

        //
        // GET: /JSSDK/

        public ActionResult Index()
        {
            //获取时间戳
            var timestamp = JSSDKHelper.GetTimestamp();
            //获取随机码
            var nonceStr = JSSDKHelper.GetNoncestr();
            string ticket = AccessTokenContainer.TryGetJsApiTicket(appId, secret);
            //获取签名
            var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, Request.Url.AbsoluteUri);

            ViewData["AppId"] = appId;
            ViewData["Timestamp"] = timestamp;
            ViewData["NonceStr"] = nonceStr;
            ViewData["Signature"] = signature;
            return View();
        }
    }
}
