//DPBMARK_FILE MP
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.Net6.Controllers
{
    public class WebSocketController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
