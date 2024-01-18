/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：HomeController.cs
    文件功能描述：首页Controller
    
    
    创建标识：Senparc - 20190926
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Cache;
using Senparc.Weixin.MP.Containers;//DPBMARK MP DPBMARK_END
using Senparc.Weixin.Sample.CommonService.Download;//DPBMARK MP DPBMARK_END
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.Sample.Net6.Models;
using Senparc.Weixin.Sample.Net6.Models.VD;
using System.Reflection;

namespace Senparc.Weixin.Sample.Net6.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            #region 程序集信息

            var vd = new Home_IndexVD()
            {
                AssemblyModelCollection = new Dictionary<Home_IndexVD_GroupInfo, List<Home_IndexVD_AssemblyModel>>()
            };

            //Senparc.Weixin SDK
            var sdkGitHubUrl = "https://github.com/JeffreySu/WeiXinMPSDK";
            var sdkGroup = new Home_IndexVD_GroupInfo()
            {
                Title = "Senparc.Weixin SDK",
                Description = "对应于每一个微信平台的基础 SDK，包含了目前微信平台的绝大部分 API，进行微信开发重点是对这些库的使用。"
            };
            var sdkList = new List<Home_IndexVD_AssemblyModel>();
            sdkList.Add(new Home_IndexVD_AssemblyModel("SDK 公共基础库", "Senparc.Weixin", typeof(Senparc.Weixin.WeixinRegister), gitHubUrl: sdkGitHubUrl));
            sdkList.Add(new Home_IndexVD_AssemblyModel("公众号<br />JSSDK<br />摇一摇周边", "Senparc.Weixin.MP", typeof(Senparc.Weixin.MP.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK MP DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("公众号MvcExtension", "Senparc.Weixin.MP.MvcExtension", typeof(Senparc.Weixin.MP.MvcExtension.SenparcOAuthAttribute), "Senparc.Weixin.MP.Mvc", gitHubUrl: sdkGitHubUrl));//DPBMARK MP DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("小程序", "Senparc.Weixin.WxOpen", typeof(Senparc.Weixin.WxOpen.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK MiniProgram DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("微信支付", "Senparc.Weixin.TenPay", typeof(Senparc.Weixin.TenPay.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK TenPay DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("微信支付V3（新）", "Senparc.Weixin.TenPayV3", typeof(Senparc.Weixin.TenPayV3.Register), supportNet45: false, gitHubUrl: sdkGitHubUrl));//DPBMARK TenPay DPBMARK_END
            sdkList.Add(new Home_IndexVD_AssemblyModel("开放平台", "Senparc.Weixin.Open", typeof(Senparc.Weixin.Open.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK Open DPBMARK_END
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            sdkList.Add(new Home_IndexVD_AssemblyModel("企业微信", "Senparc.Weixin.Work", typeof(Senparc.Weixin.Work.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK Work DPBMARK_END
            vd.AssemblyModelCollection[sdkGroup] = sdkList;


            var aspnetGroup = new Home_IndexVD_GroupInfo()
            {
                Title = "Senparc.Weixin SDK 的 ASP.NET 运行时基础库",
                Description = "这些库基于 ASP.NET 运行时，依赖 ASP.NET 一些特性完成一系列基于 ASP.NET 及 ASP.NET Core 的操作。<br />" +
                "分离出这些库的另外一个原因，是为了使 Senparc.Weixin SDK 核心库可以不依赖于 ASP.NET 运行时，<br />" +
                "以便部署在轻量级的容器（如 Docker）、命令行（Console）、桌面（Desktop / WinForm / WPF / Blazor / MAUI / UWP），甚至手机应用（App）等特殊环境中。"
            };
            var aspnetList = new List<Home_IndexVD_AssemblyModel>();
            aspnetList.Add(new Home_IndexVD_AssemblyModel("ASP.NET<br />运行时基础库", "Senparc.Weixin.AspNet", typeof(Senparc.Weixin.AspNet.WeixinRegister), gitHubUrl: sdkGitHubUrl));//AspNet 运行时基础库
            aspnetList.Add(new Home_IndexVD_AssemblyModel("公众号消息中间件", "Senparc.Weixin.MP.Middleware", typeof(Senparc.Weixin.MP.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension), gitHubUrl: sdkGitHubUrl));//DPBMARK MP DPBMARK_END
            aspnetList.Add(new Home_IndexVD_AssemblyModel("小程序消息中间件", "Senparc.Weixin.WxOpen.Middleware", typeof(Senparc.Weixin.WxOpen.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension), gitHubUrl: sdkGitHubUrl));//DPBMARK MiniProgram DPBMARK_END
            aspnetList.Add(new Home_IndexVD_AssemblyModel("企业微信消息中间件", "Senparc.Weixin.Work.Middleware", typeof(Senparc.Weixin.Work.MessageHandlers.Middleware.MessageHandlerMiddlewareExtension), gitHubUrl: sdkGitHubUrl));//DPBMARK Work DPBMARK_END
            vd.AssemblyModelCollection[aspnetGroup] = aspnetList;

            var cacheAndExtensionGroup = new Home_IndexVD_GroupInfo()
            {
                Title = "Senparc.Weixin SDK 扩展组件",
                Description = "Senparc.Weixin SDK 扩展组件用于提供缓存、WebSocket 等一系列扩展模块，<br />" +
                "这些模块是盛派官方的一个实现，几乎所有的扩展模块都是严格面向接口开发的，<br />" +
                "因此，您也可以自行扩展，并对接到微信 SDK 或其他系统中。<br />"
            };
            var cacheAndExtensionList = new List<Home_IndexVD_AssemblyModel>();
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Redis 缓存<br />（StackExchange.Redis）", "Senparc.Weixin.Cache.Redis", typeof(Senparc.Weixin.Cache.Redis.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK Redis DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Redis 缓存<br />（CsRedis）", "Senparc.Weixin.Cache.CsRedis", typeof(Senparc.Weixin.Cache.CsRedis.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK CsRedis DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("Memcached 缓存", "Senparc.Weixin.Cache.Memcached", typeof(Senparc.Weixin.Cache.Memcached.Register), gitHubUrl: sdkGitHubUrl));//DPBMARK Memcached DPBMARK_END
            cacheAndExtensionList.Add(new Home_IndexVD_AssemblyModel("WebSocket 模块", "Senparc.WebSocket", typeof(Senparc.WebSocket.WebSocketConfig), gitHubUrl: sdkGitHubUrl));//DPBMARK WebSocket DPBMARK_END
            vd.AssemblyModelCollection[cacheAndExtensionGroup] = cacheAndExtensionList;

            var neucharGitHubUrl = "https://github.com/Senparc/NeuChar";
            var neucharGroup = new Home_IndexVD_GroupInfo()
            {
                Title = "跨平台支持库：Senparc.NeuChar",
                Description = "NeuChar 是盛派提供的一套跨平台服务的标准（例如跨微信公众号、微信小程序、钉钉、QQ小程序、百度小程序，等等），<br />" +
                "使用一套代码，同时服务多平台。目前 Senparc.Weixin SDK 就是基于 NeuChar 标准在微信领域内的一个实现分支，<br />" +
                "您也可以使用 NeuChar 扩展到更多的平台。<br />" +
                "<a href=\"https://www.neuchar.com\" target=\"_blank\">https://www.neuchar.com</a> 是盛派官方提供的一个基于 NeuChar 标准实现的可视化跨平台配置操作平台。"
            };
            var neucharList = new List<Home_IndexVD_AssemblyModel>();
            neucharList.Add(new Home_IndexVD_AssemblyModel("NeuChar 跨平台支持库", "Senparc.NeuChar", typeof(Senparc.NeuChar.Register), gitHubUrl: neucharGitHubUrl));// NeuChar 基础库
            neucharList.Add(new Home_IndexVD_AssemblyModel("NeuChar APP 以及<br />NeuChar Ending<br />的对接 SDK", "Senparc.NeuChar.App", typeof(Senparc.NeuChar.App.HttpRequestType), gitHubUrl: neucharGitHubUrl));// NeuChar 基础库
            neucharList.Add(new Home_IndexVD_AssemblyModel("NeuChar 的 ASP.NET<br />运行时支持库", "Senparc.NeuChar.AspNet", typeof(Senparc.NeuChar.Middlewares.MessageHandlerMiddlewareExtension), gitHubUrl: neucharGitHubUrl));// NeuChar 基础库
            vd.AssemblyModelCollection[neucharGroup] = neucharList;

            var co2netGitHubUrl = "https://github.com/Senparc/Senparc.CO2NET";
            var co2netGroup = new Home_IndexVD_GroupInfo()
            {
                Title = "底层公共基础库：Senparc.CO2NET",
                Description = "Senparc.CO2NET 是一个支持 .NET Framework 和 .NET Core 的公共基础扩展库，包含常规开发所需要的基础帮助类。<br />" +
                "开发者可以直接使用 CO2NET 为项目提供公共基础方法，免去重复准备和维护公共代码的痛苦。<br />" +
                "您可以在几乎任何项目中使用 CO2NET。<a href=\"https://github.com/Senparc/Senparc.CO2NET\" target=\"_blank\">查看源码</a>"
            };
            var co2netList = new List<Home_IndexVD_AssemblyModel>();
            co2netList.Add(new Home_IndexVD_AssemblyModel("CO2NET 基础库", "Senparc.CO2NET", typeof(CO2NET.Config), gitHubUrl: co2netGitHubUrl));//CO2NET 基础库版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("APM 库", "Senparc.CO2NET.APM", typeof(CO2NET.APM.Config), gitHubUrl: co2netGitHubUrl));//CO2NET.APM 版本信息
            co2netList.Add(new Home_IndexVD_AssemblyModel("Redis 库<br />（StackExchange.Redis）", "Senparc.CO2NET.Cache.Redis", typeof(Senparc.CO2NET.Cache.Redis.Register), gitHubUrl: co2netGitHubUrl));//CO2NET.Cache.Redis 版本信息  -- DPBMARK CsRedis DPBMARK_END
            co2netList.Add(new Home_IndexVD_AssemblyModel("Redis 库<br />（CSRedis）", "Senparc.CO2NET.Cache.CsRedis", typeof(Senparc.CO2NET.Cache.CsRedis.Register), gitHubUrl: co2netGitHubUrl));//CO2NET.Cache.CsRedis 版本信息        -- DPBMARK CsRedis DPBMARK_END
            co2netList.Add(new Home_IndexVD_AssemblyModel("Memcached 库", "Senparc.CO2NET.Cache.Memcached", typeof(Senparc.CO2NET.Cache.Memcached.Register), gitHubUrl: co2netGitHubUrl));//CO2NET.Cache.Memcached 版本信息               -- DPBMARK Memcached DPBMARK_END
            co2netList.Add(new Home_IndexVD_AssemblyModel("CO2NET 的 ASP.NET<br />运行时支持库", "Senparc.CO2NET.AspNet", typeof(Senparc.CO2NET.AspNet.Register), gitHubUrl: co2netGitHubUrl));//CO2NET.AspNet 版本信息
            vd.AssemblyModelCollection[co2netGroup] = co2netList;
            co2netList.Add(new Home_IndexVD_AssemblyModel("WebApi 引擎库（新）", "Senparc.CO2NET.WebApi", typeof(Senparc.CO2NET.WebApi.Register), supportNet45: false, gitHubUrl: co2netGitHubUrl));//CO2NET.AspNet 版本信息
            vd.AssemblyModelCollection[co2netGroup] = co2netList;

            #endregion

            //当前 Sample 版本

            TempData["SampleVersion"] = Home_IndexVD_AssemblyModel.GetDisplayVersion(Assembly.GetExecutingAssembly().GetName().Version);

            //缓存
            //var containerCacheStrategy  = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            TempData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            #region DPBMARK MP
            try
            {
                //文档下载版本
                var configHelper = new ConfigHelper();
                var config = configHelper.GetConfig();
                TempData["NewestDocumentVersion"] = config.Versions.First();
            }
            catch (Exception)
            {
                TempData["NewestDocumentVersion"] = new CommonService.Download.Config();
            }
            #endregion  DPBMARK_END

            Weixin.WeixinTrace.SendCustomLog("首页被访问",
                                string.Format("Url：{0}\r\nIP：{1}", Request.Host, HttpContext.Connection.RemoteIpAddress));
            //or use Header: REMOTE_ADDR

            //获取编译时间
            TempData["BuildTime"] = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyyMMdd.HH.mm");

            return View(vd);
        }

        #region MP 测试 -- DPBMARK MP
        public ActionResult GetAccessTokenBags()
        {
            if (!Request.IsLocal())
            {
                return new UnauthorizedResult();//只允许本地访问
            }
            var accessTokenBags = AccessTokenContainer.GetAllItems();
            return Json(accessTokenBags);
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
        #endregion  //DPBMARK_END


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


        public ActionResult TestPath()
        {
            return Content(HttpContext.Request.PathBase);
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
