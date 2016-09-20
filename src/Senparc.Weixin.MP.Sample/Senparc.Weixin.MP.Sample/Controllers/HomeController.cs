/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Open.CommonAPIs;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Func<string, FileVersionInfo> getFileVersionInfo = dllFileName => 
                FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/" + dllFileName));

            Func<FileVersionInfo, string> getDisplayVersion = fileVersionInfo =>
                 Regex.Match(fileVersionInfo.FileVersion, @"\d+\.\d+\.\d+").Value;

            TempData["WeixinVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.dll"));
            TempData["MpVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.dll"));
            TempData["ExtensionVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.MP.MvcExtension.dll"));
            TempData["OpenVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Open.dll"));
            TempData["QYVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.QY.dll"));
            TempData["RedisCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Redis.dll"));
            TempData["MemcachedCacheVersion"] = getDisplayVersion(getFileVersionInfo("Senparc.Weixin.Cache.Memcached.dll"));
            return View();
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
            var accessToken = Senparc.Weixin.MP.CommonAPIs.AccessTokenContainer.GetAccessToken(appId);
            //使用AccessToken请求接口
            var apiResult = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu("你的AppId");


            throw new Exception("出错测试，使用Elmah保存错误结果(2)");
            return View();
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
    }
}
