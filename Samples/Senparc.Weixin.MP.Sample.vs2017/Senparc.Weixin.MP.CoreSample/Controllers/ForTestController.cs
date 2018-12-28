using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.HttpUtility;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 提供给 Senparc.WeixinTests/Utilities/HttpUtility/PostTests.cs使用
    /// </summary>
    public class ForTestController : Controller
    {
        [HttpPost]
        public ActionResult PostTest()
        {
            string data;

            using (var sr = new StreamReader(Request.GetRequestMemoryStream()))
            {
                data = sr.ReadToEnd();
            }

            var isAjax = Request.IsAjaxRequest();

            Response.Cookies.Append("TestCookie", SystemTime.Now.ToString());

            return Content(data + " Ajax:" + isAjax + " Server Time:" + DateTime.Now);
        }
    }
}
