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
//using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Open.CommonAPIs;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
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
