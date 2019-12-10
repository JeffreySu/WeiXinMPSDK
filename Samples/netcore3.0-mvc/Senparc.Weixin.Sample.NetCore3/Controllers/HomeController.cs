using System;
using System.Collections.Generic;
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：HomeController.cs
    文件功能描述：首页Controller
    
    
    创建标识：Senparc - 20190926
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.Cache;
//DPBMARK MP
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;//DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.Download;
//DPBMARK Open
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.ComponentAPIs;//DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.Sample.NetCore3.Models;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            Func<Version, string> getDisplayVersion = version =>
                 Regex.Match(version.ToString(), @"\d+\.\d+\.\d+").Value;

            Func<Type, string> getTypeVersionInfo = type =>
            {
                var version = System.Reflection.Assembly.GetAssembly(type).GetName().Version;
                return getDisplayVersion(version);
            };

            TempData["SampleVersion"] = getTypeVersionInfo(this.GetType());//当前Demo的版本号
            TempData["CO2NETVersion"] = getTypeVersionInfo(typeof(CO2NET.Config));//CO2NET版本号
            TempData["NeuCharVersion"] = getTypeVersionInfo(typeof(Senparc.NeuChar.ApiBindInfo));//NeuChar版本号

            TempData["WeixinVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Config));
            TempData["TenPayVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.TenPay.Register));//DPBMARK TenPay DPBMARK_END
            TempData["MpVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.MP.Register));//DPBMARK MP DPBMARK_END
            TempData["ExtensionVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.MP.MvcExtension.FixWeixinBugWeixinResult));//DPBMARK MP DPBMARK_END
            TempData["OpenVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Open.Register));//DPBMARK Open DPBMARK_END
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            TempData["WorkVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Work.Register));//DPBMARK Work DPBMARK_END
            TempData["RedisCacheVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Cache.Redis.Register));//DPBMARK Redis DPBMARK_END
            TempData["MemcachedCacheVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Cache.Memcached.Register));//DPBMARK Memcached DPBMARK_END
            TempData["WxOpenVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.WxOpen.Register));//DPBMARK MiniProgram DPBMARK_END
            TempData["WebSocketVersion"] = getTypeVersionInfo(typeof(Senparc.WebSocket.WebSocketConfig));//DPBMARK WebSocket DPBMARK_END

            //缓存
            //var containerCacheStrategy  = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            TempData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            //文档下载版本
            var configHelper = new ConfigHelper();
            var config = configHelper.GetConfig();
            TempData["NewestDocumentVersion"] = config.Versions.First();

            Weixin.WeixinTrace.SendCustomLog("首页被访问",
                    string.Format("Url：{0}\r\nIP：{1}", Request.Host, HttpContext.Connection.RemoteIpAddress));
            //or use Header: REMOTE_ADDR

            //获取编译时间
            TempData["BuildTime"] = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyyMMdd.HH.mm");

            return View();
        }

        public ActionResult WeChatSampleBuilder()
        {
            return View();
        }

        public ActionResult Book()
        {
            return Redirect("https://book.weixin.senparc.com");//《微信开发深度解析》图书对应的线上辅助阅读系统
        }

        public ActionResult DebugOpen()
        {
            Senparc.Weixin.Config.IsDebug = true;
            return Content("Debug状态已打开。");
        }

        public ActionResult DebugClose()
        {
            Senparc.Weixin.Config.IsDebug = false;
            return Content("Debug状态已关闭。");
        }

        public ActionResult GetAccessTokenBags()
        {
            if (!Request.IsLocal())
            {
                return new UnauthorizedResult();//只允许本地访问
            }
            var accessTokenBags = AccessTokenContainer.GetAllItems();
            return Json(accessTokenBags);
        }


        public ActionResult TestPath()
        {
            return Content(HttpContext.Request.PathBase);
        }

        /// <summary>
        /// 测试未经注册的TryGetAccessToken同步方法
        /// </summary>
        /// <returns></returns>
        public ActionResult TryGetAccessTokenTest()
        {
            Senparc.Weixin.Config.ThrownWhenJsonResultFaild = false;//如果错误，不抛出异常
            var appId = Config.SenparcWeixinSetting.WeixinAppId;
            var appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;
            var result = AccessTokenContainer.TryGetAccessToken(appId, appSecret, true);
            Senparc.Weixin.Config.ThrownWhenJsonResultFaild = true;

            return Content($"AccessToken: {result?.Substring(0, 10) }...");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
