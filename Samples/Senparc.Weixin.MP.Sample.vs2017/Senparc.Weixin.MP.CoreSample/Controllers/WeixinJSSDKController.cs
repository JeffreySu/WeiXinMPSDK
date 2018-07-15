using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// JSSDK的演示
    /// </summary>
    public class WeixinJSSDKController : Controller
    {
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        //
        // GET: /JSSDK/

        public ActionResult Index()
        {
            #region v13.6.4之前的写法
            ////获取时间戳
            //var timestamp = JSSDKHelper.GetTimestamp();
            ////获取随机码
            //var nonceStr = JSSDKHelper.GetNoncestr();
            //string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, secret);
            ////获取签名
            //var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, Request.Url.AbsoluteUri);

            //ViewData["AppId"] = appId;
            //ViewData["Timestamp"] = timestamp;
            //ViewData["NonceStr"] = nonceStr;
            //ViewData["Signature"] = signature;
            #endregion

            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, appSecret, Request.AbsoluteUri());
            //ViewData["JsSdkUiPackage"] = jssdkUiPackage;
            //ViewData["AppId"] = appId;
            //ViewData["Timestamp"] = jssdkUiPackage.Timestamp;
            //ViewData["NonceStr"] = jssdkUiPackage.NonceStr;
            //ViewData["Signature"] = jssdkUiPackage.Signature;

            return View(jssdkUiPackage);
        }
    }
}
