using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 提供给 Senparc.WeixinTests/Utilities/HttpUtility/PostTests.cs使用
    /// </summary>
    public class ForTestController : Controller
    {
        [HttpPost]
        public IActionResult PostTest()
        {
            string data;

            using (var sr = new StreamReader(Request.Body))
            {
                data = sr.ReadToEnd();
            }

            var isAjax = Request.Headers["X-Requested-With"];


            return Content(data + " Ajax:" + isAjax + " Server Time:" + DateTime.Now);
        }
    }
}
