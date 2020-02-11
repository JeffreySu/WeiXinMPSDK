using System;
using System.Collections.Generic;
/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
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
using Senparc.Weixin.Sample.NetCore3.Models.VD;

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

           
            var vd = new Home_IndexVD()
            {
                AssemblyModelCollection = new Dictionary<string, List<Home_IndexVD_AssemblyModel>>()
            };

            //Senparc.Weixin SDK
            var sdkList = new List<Home_IndexVD_AssemblyModel>();
            sdkList.Add(new Home_IndexVD_AssemblyModel("SDK 公共基础库", "Senparc.Weixin", this.GetType()));
            sdkList.Add(new Home_IndexVD_AssemblyModel("微信支付", "Senparc.Weixin.TenPay", typeof(Senparc.Weixin.Config)));
            sdkList.Add(new Home_IndexVD_AssemblyModel("公众号<br />JSSDK<br />摇一摇周边", "Senparc.Weixin.MP", typeof(Senparc.Weixin.Config)));//DPBMARK TenPay DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("公众号MvcExtension", "Senparc.Weixin.MP.MvcExtension", typeof(Senparc.Weixin.MP.Register), "Senparc.Weixin.MP.Mvc"));//DPBMARK MP DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("小程序", "Senparc.Weixin.WxOpen", typeof(Senparc.Weixin.WxOpen.Register)));//DPBMARK MiniProgram DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("微信支付", "Senparc.Weixin.TenPay", typeof(Senparc.Weixin.MP.MvcExtension.FixWeixinBugWeixinResult)));//DPBMARK MP DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("开放平台", "Senparc.Weixin.Open", typeof(Senparc.Weixin.Open.Register)));//DPBMARK Open DPBMARK_END
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            sdkList.Add(new Home_IndexVD_AssemblyModel("企业微信", "Senparc.Weixin.Work", typeof(Senparc.Weixin.Work.Register)));//DPBMARK Work DPBMARK_END
            vd.AssemblyModelCollection["Senparc.Weixin SDK"] = sdkList;

            var aspnetList = new List<Home_IndexVD_AssemblyModel>();
            aspnetList.Add(new Home_IndexVD_AssemblyModel("ASP.NET 运行时基础库", "Senparc.Weixin.AspNet",typeof(Senparc.Weixin.AspNet.WeixinRegister)));//AspNet 运行时基础库
            aspnetList.Add(new Home_IndexVD_AssemblyModel("公众号消息中间件", "Senparc.Weixin.MP.Middleware",typeof(Senparc.Weixin.MP.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension)));//DPBMARK MP DPBMARK_END
            aspnetList.Add(new Home_IndexVD_AssemblyModel("小程序消息中间件", "Senparc.Weixin.WxOpen.Middleware",typeof(Senparc.Weixin.WxOpen.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension)));//DPBMARK MiniProgram DPBMARK_END
            aspnetList.Add(new Home_IndexVD_AssemblyModel("企业微信消息中间件", "Senparc.Weixin.Work.Middleware",typeof(Senparc.Weixin.Work.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension)));//DPBMARK Work DPBMARK_END
            vd.AssemblyModelCollection["Senparc.Weixin SDK 的 ASP.NET 运行时基础库"] = aspnetList;

            var cacheAndExtensionList = new List<Home_IndexVD_AssemblyModel>();
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Redis 缓存<br />（StackExchange.Redis）", "Senparc.Weixin.Cache.Redis", typeof(Senparc.Weixin.Cache.Redis.Register)));//DPBMARK Redis DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Redis 缓存<br />（CsRedis）", "Senparc.Weixin.Cache.CsRedis", typeof(Senparc.Weixin.Cache.CsRedis.Register)));//DPBMARK CsRedis DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Memcached 缓存", "Senparc.Weixin.Cache.Memcached", typeof(Senparc.Weixin.Cache.Memcached.Register)));//DPBMARK Memcached DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("WebSocket 模块", "Senparc.WebSocket.WebSocketConfig", typeof(Senparc.WebSocket.WebSocketConfig)));//DPBMARK WebSocket DPBMARK_END
            vd.AssemblyModelCollection["Senparc.Weixin SDK 扩展组件"] = cacheAndExtensionList;

            var neucharList = new List<Home_IndexVD_AssemblyModel>();
            neucharList.Add(new Home_IndexVD_AssemblyModel("NeuChar 跨平台支持库", "Senparc.NeuChar", typeof(Senparc.NeuChar.ApiBindInfo)));// NeuChar 基础库
            neucharList.Add(new Home_IndexVD_AssemblyModel("NeuChar APP 以及<br />NeuChar Ending<br />的对接 SDK", "Senparc.NeuChar.App", typeof(Senparc.NeuChar.App.HttpRequestType)));// NeuChar 基础库
            vd.AssemblyModelCollection["跨平台支持库：Senparc.NeuChar"] = neucharList;

            var co2netList = new List<Home_IndexVD_AssemblyModel>();
            co2netList.Add(new Home_IndexVD_AssemblyModel("CO2NET 基础库","Senparc.CO2NET", typeof(CO2NET.Config)));//CO2NET 基础库版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("APM 库", "Senparc.CO2NET.APM", typeof(CO2NET.APM.Config)));//CO2NET.APM 版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("Redis 库<br />（StackExchange.Redis）", "Senparc.CO2NET.Cache.Redis", typeof(Senparc.CO2NET.Cache.Redis.Register)));//CO2NET.Cache.Redis 版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("Redis 库<br />（CSRedis）", "Senparc.CO2NET.Cache.CsRedis", typeof(Senparc.CO2NET.Cache.CsRedis.Register)));//CO2NET.Cache.CsRedis 版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("Memcached 库", "Senparc.CO2NET.Cache.Memcached", typeof(Senparc.CO2NET.Cache.Memcached.Register)));//CO2NET.Cache.Memcached 版本信息
            vd.AssemblyModelCollection["底层公共基础库：Senparc.CO2NET"] = cacheAndExtensionList;



            TempData["CO2NETVersion"] = getTypeVersionInfo();
            TempData["NeuCharVersion"] = getTypeVersionInfo(typeof(Senparc.NeuChar.ApiBindInfo));//NeuChar版本号


            //缓存
            //var containerCacheStrategy  = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            TempData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            try
            {
                //文档下载版本
                var configHelper = new ConfigHelper();
                var config = configHelper.GetConfig();
                TempData["NewestDocumentVersion"] = config.Versions.First();
            }
            catch (Exception)
            {
                TempData["NewestDocumentVersion"] = new Senparc.Weixin.MP.Sample.CommonService.Download.Config();
            }

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
