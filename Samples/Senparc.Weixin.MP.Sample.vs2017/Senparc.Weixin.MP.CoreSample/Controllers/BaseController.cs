/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Senparc.Weixin.MP.CoreSample.Controllers
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["CacheType"] = CO2NET.Cache.CacheStrategyFactory.GetObjectCacheStrategyInstance().GetType().Name;
            base.OnActionExecuting(context);
        }
    }
}
