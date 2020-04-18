using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 插件项目相关：
    /// </summary>
    public class PluginsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}