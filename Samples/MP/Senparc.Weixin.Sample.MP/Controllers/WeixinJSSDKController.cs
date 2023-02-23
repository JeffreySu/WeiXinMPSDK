//DPBMARK_FILE MP
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.MP.Controllers
{
    /// <summary>
    /// JSSDK 的演示
    /// </summary>
    public class WeixinJSSDKController : BaseController
    {
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        public async Task<ActionResult> Index()
        {
            var jssdkUiPackage = await JSSDKHelper.GetJsSdkUiPackageAsync(appId, appSecret, Request.AbsoluteUri());
            return View(jssdkUiPackage);
        }
    }
}
