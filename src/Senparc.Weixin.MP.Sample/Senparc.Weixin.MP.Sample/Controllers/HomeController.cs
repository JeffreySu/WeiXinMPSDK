using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Entities;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.Containers;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : Controller
    {
        SenparcWeixinSetting setting;
        public HomeController(IOptionsSnapshot<SenparcWeixinSetting> senparcWeixinSetting)
        {
            setting = senparcWeixinSetting.Value;
        }
        public IActionResult Index()
        {
            ViewData["AccessToken"] = AccessTokenContainer.TryGetAccessToken(setting.WeixinAppId, setting.WeixinAppSecret);
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
