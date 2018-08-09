using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Containers;

namespace Senparc.Weixin.MP.Sample.Controllers
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
            var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            return View(jssdkUiPackage);
        }
    }
}