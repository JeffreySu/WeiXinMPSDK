using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Containers;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 设备能力测试
    /// </summary>
    public class DeviceController : BaseController
    {
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string secret = Config.SenparcWeixinSetting.WeixinAppSecret;


        public ActionResult Index()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            TempData["AccessToken"] = accessToken;
            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.AbsoluteUri());
            return View(jssdkUiPackage);
        }
    }
}