using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.CoreSample.Controllers.NeuChar
{
    /// <summary>
    /// NeuChar 处理程序
    /// </summary>
    public class NeuCharController : Controller
    {
        [HttpPost]
        public IActionResult Sync(string json)
        {
            //TODO:json可加密

            //TODO：进行保存

            return Json(new { msg = "ok" });
        }
    }
}
