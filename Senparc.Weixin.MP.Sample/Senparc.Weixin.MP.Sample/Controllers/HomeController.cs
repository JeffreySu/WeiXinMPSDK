/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
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
            var verifyTicket =
                ComponentContainer.TryGetComponentVerifyTicket(ConfigurationManager.AppSettings["Component_Appid"], true);

            var accessToken =
    ComponentContainer.TryGetComponentAccessToken(ConfigurationManager.AppSettings["Component_Appid"],
        ConfigurationManager.AppSettings["Component_Secret"], verifyTicket);

            var ticket =
                ComponentContainer.TryGetComponentVerifyTicket(ConfigurationManager.AppSettings["Component_Appid"]);

            var precode = ComponentContainer.GetPreAuthCode(ConfigurationManager.AppSettings["Component_Appid"]);
            throw new Exception("At：" + accessToken + "\r\n" + ticket + "\r\n" + precode);

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

            throw new Exception("出错测试，使用Elmah保存错误结果(2)");
            return View();
        }
    }
}
