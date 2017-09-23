using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.MP.Sample.CommonService.Download;

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
#elif NETCOREAPP2_0 || NETSTANDARD2_0
            {
                var dllPath = Server.GetMapPath("~/bin/netcoreapp2.0/" + dllFileName);
                return FileVersionInfo.GetVersionInfo(dllPath);
            };
#else
            {
                var dllPath = Server.GetMapPath("~/bin/netcoreapp1.1/" + dllFileName);
                return FileVersionInfo.GetVersionInfo(dllPath);
            };
#endif

            Func < FileVersionInfo, string> getDisplayVersion = fileVersionInfo =>
                 Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+\.\d+").Value;

            TempData["WeixinVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.dll"));
            TempData["MpVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.dll"));
            TempData["ExtensionVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.MvcExtension.dll"));
            TempData["OpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Open.dll"));
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            TempData["WorkVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Work.dll"));
            TempData["RedisCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Redis.dll"));
            TempData["MemcachedCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Memcached.dll"));
            TempData["WxOpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.WxOpen.dll"));
            TempData["WebSocketVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.WebSocket.dll"));


            //缓存
            //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            TempData["CacheStrategy"] = containerCacheStrategy.GetType().Name.Replace("ContainerCacheStrategy", "");

            //文档下载版本
            var configHelper = new ConfigHelper(this.HttpContext);
            var config = configHelper.GetConfig();
            TempData["NewestDocumentVersion"] = config.Versions.First();

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
