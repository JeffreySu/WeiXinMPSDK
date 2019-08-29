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
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.Cache;
//DPBMARK MP
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;//DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.Download;
//DPBMARK Open
using Senparc.Weixin.Open.ComponentAPIs;//DPBMARK_END

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
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

            Weixin.WeixinTrace.SendCustomLog("首页被访问", string.Format("Url：{0}\r\nIP：{1}", Request.Url, Request.UserHostName));

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
            if (!Request.IsLocal)
            {
                return new HttpUnauthorizedResult();//只允许本地访问
            }
            var accessTokenBags = AccessTokenContainer.GetAllItems();
            return Json(accessTokenBags, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 测试未经注册的TryGetAccessToken同步方法
        /// </summary>
        /// <returns></returns>
        public ActionResult TryGetAccessTokenTest()
        {
            Stopwatch sp = new Stopwatch();
            var appId = "YourAppId";//非config里的注册过的appid
            var appSecret = "YourSecret";//非config里的注册过的appSecret
            sp.Start();
            string message = string.Empty;
            try
            {
                 message = AccessTokenContainer.TryGetAccessToken(appId, appSecret, true);//
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            sp.Stop();
            return Content($"消息：{message.Substring(0, 10)}...\r\n耗时 {sp.ElapsedMilliseconds}ms");
        }
    }
}
