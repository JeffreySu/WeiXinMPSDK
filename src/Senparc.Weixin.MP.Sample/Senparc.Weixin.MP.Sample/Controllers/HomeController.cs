using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class HomeController : Controller
    {
        protected SenparcWeixinSetting SenparcWeixinSetting { get; set; }

        //public HomeController(IOptions<SenparcWeixinSetting> settings)
        //{
        //    SenparcWeixinSetting = settings.Value;
        //}

        public IActionResult Index()
        {
            //ViewData["AppId"] = SenparcWeixinSetting.WeixinAppId;
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
