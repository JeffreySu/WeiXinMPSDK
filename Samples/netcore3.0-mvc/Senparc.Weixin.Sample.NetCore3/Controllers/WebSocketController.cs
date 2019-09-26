using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
    public class WebSocketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
