/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：BaseController.cs
    文件功能描述：Controller基类
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class BaseController : Controller
    {
        protected string AppId
        {
            get
            {
                return Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
            }
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewData["CacheType"] = CO2NET.Cache.CacheStrategyFactory.GetObjectCacheStrategyInstance().GetType().Name;
            base.OnResultExecuting(filterContext);
        }
    }
}
