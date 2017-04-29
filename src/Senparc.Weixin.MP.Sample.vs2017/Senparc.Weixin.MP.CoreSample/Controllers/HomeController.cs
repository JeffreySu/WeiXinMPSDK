using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Senparc.Weixin.MP.CoreSample.CommonService.Utilities;
using Senparc.Weixin.MP.CoreSample.CommonService.Download;

#if NET45
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
#endif


namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public class HomeController : BaseController
    {
#if NET45
        public HomeController()
        {
        }
#else
        IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
#endif

        public IActionResult Index()
        {
            Func<string, FileVersionInfo> getFileVersionInfo = dllFileName =>
#if NET45
                FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/" + dllFileName));
#else
            {
                var dllPath = Server.GetMapPath("~/bin/netcoreapp1.1/" + dllFileName);
                return FileVersionInfo.GetVersionInfo(dllPath);
            };
#endif

            Func<FileVersionInfo, string> getDisplayVersion = fileVersionInfo =>
                 Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+\.\d+").Value;

            ViewData["WeixinVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.dll"));
            ViewData["MpVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.dll"));

#if NET45
            ViewData["ExtensionVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.MvcExtension.dll"));
#else
            ViewData["ExtensionVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.CoreMvcExtension.dll"));
#endif

            ViewData["OpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Open.dll"));
            ViewData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));
            ViewData["RedisCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Redis.dll"));
            ViewData["MemcachedCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Memcached.dll"));
            ViewData["WxOpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.WxOpen.dll"));
            ViewData["WebSocketVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.WebSocket.dll"));

            //缓存
            //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            ViewData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            //文档下载版本
            var configHelper = new ConfigHelper(this.HttpContext);
            var config = configHelper.GetConfig();
            ViewData["NewestDocumentVersion"] = config.Versions.First();

#if NET45
                        Weixin.WeixinTrace.SendCustomLog("首页被访问", string.Format("Url：{0}\r\nIP：{1}", Request.Url, Request.UserHostName));
#else
            Weixin.WeixinTrace.SendCustomLog("首页被访问", string.Format("Url：{0}\r\nIP：{1}", Request.Host, HttpContext.Connection.RemoteIpAddress));//or use Header: REMOTE_ADDR
#endif


            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
