using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.CoreSample.Models;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
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


        public async Task<IActionResult> Index()
        {
            //测试Get方法
            var html = await RequestUtility.HttpGetAsync("https://www.baidu.com", refererUrl: "https://sdk.weixin.senparc.com");


            #region 获取版本信息


            Func<string, FileVersionInfo> getFileVersionInfo = dllFileName =>
#if NET45
                FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/" + dllFileName));
#elif NETCOREAPP2_0 || NETSTANDARD2_0
            {
                var dllPath =
                    Senparc.Weixin.MP.Sample.CommonService.Utilities.Server.GetMapPath(
                        System.IO.Path.Combine(AppContext.BaseDirectory, dllFileName)); //dll所在目录
                return FileVersionInfo.GetVersionInfo(dllPath);
            };
#else
            {
                var dllPath = Server.GetMapPath("~/bin/netcoreapp1.1/" + dllFileName);
                return FileVersionInfo.GetVersionInfo(dllPath);
            };
#endif

            Func<FileVersionInfo, string> getDisplayVersion = fileVersionInfo =>
                Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+\.\d+").Value;

            TempData["WeixinVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.dll"));
            TempData["MpVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.dll"));
            TempData["ExtensionVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.MvcExtension.dll"));
            TempData["OpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Open.dll"));
            //TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));//已经停止更新
            TempData["WorkVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Work.dll"));
            TempData["RedisCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Redis.dll"));
            TempData["MemcachedCacheVersion"] =
                getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Memcached.dll"));
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
            Weixin.WeixinTrace.SendCustomLog("首页被访问",
                    string.Format("Url：{0}\r\nIP：{1}", Request.Host, HttpContext.Connection.RemoteIpAddress));
            //or use Header: REMOTE_ADDR
#endif

            #endregion

            if (html.Length == 0)
            {
                throw new Exception("RequestUtility.HttpGet()方法测试失败");
            }

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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
