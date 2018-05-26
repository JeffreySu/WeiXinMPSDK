using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Containers;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 设备能力测试
    /// </summary>
    public class DeviceController : BaseController
    {
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];


        public ActionResult Index()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            TempData["AccessToken"] = accessToken;
            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            return View(jssdkUiPackage);
        }
    }
}