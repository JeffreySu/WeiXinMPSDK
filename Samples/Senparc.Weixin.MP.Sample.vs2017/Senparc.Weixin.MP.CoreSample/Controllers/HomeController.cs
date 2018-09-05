/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：HomeController.cs
    文件功能描述：首页Controller
    
    
    创建标识：Senparc - 20150312
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
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public class HomeController : BaseController
    {
        IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        public ActionResult Index()
        {
            Func<Version, string> getDisplayVersion = version =>
                 Regex.Match(version.ToString(), @"\d+\.\d+\.\d+").Value;

            Func<Type, string> getTypeVersionInfo = type =>
            {
                var version = System.Reflection.Assembly.GetAssembly(type).GetName().Version;
                return getDisplayVersion(version);
            };

            TempData["CO2NETVersion"] = getTypeVersionInfo(typeof(CO2NET.Config));
            TempData["WeixinVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Config));
            TempData["TenPayVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.TenPay.Register));
            TempData["MpVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.MP.Register));
            TempData["ExtensionVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.MP.MvcExtension.FixWeixinBugWeixinResult));
            TempData["OpenVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Open.Register));
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            TempData["WorkVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Work.Register));
            TempData["RedisCacheVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Cache.Redis.Register));
            TempData["MemcachedCacheVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.Cache.Memcached.Register));
            TempData["WxOpenVersion"] = getTypeVersionInfo(typeof(Senparc.Weixin.WxOpen.Register));
            TempData["WebSocketVersion"] = getTypeVersionInfo(typeof(Senparc.WebSocket.WebSocketConfig));

            //缓存
            //var containerCacheStrategy  = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            TempData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            //文档下载版本
            var configHelper = new ConfigHelper(this.HttpContext);
            var config = configHelper.GetConfig();
            TempData["NewestDocumentVersion"] = config.Versions.First();

            Weixin.WeixinTrace.SendCustomLog("首页被访问",
                    string.Format("Url：{0}\r\nIP：{1}", Request.Host, HttpContext.Connection.RemoteIpAddress));
            //or use Header: REMOTE_ADDR

            return View();
        }

        public ActionResult Book()
        {
            return Redirect("https://book.weixin.senparc.com");//《微信开发深度解析》图书对应的线上辅助阅读系统
        }

        public ActionResult TestElmah()
        {
            try
            {
                throw new Exception("出错测试，使用Elmah保存错误结果(1)");
            }
            catch (Exception)
            {

            }

            var appId = "你的AppId";
            //获取AccessToken
            var accessToken = Senparc.Weixin.MP.Containers.AccessTokenContainer.GetAccessToken(appId);
            //使用AccessToken请求接口
            var apiResult = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu("你的AppId");

            throw new Exception("出错测试，使用Elmah保存错误结果(2)");
            //return View();
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
    }
}
