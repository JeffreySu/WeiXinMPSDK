using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
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

            using (var sr = new StreamReader(Request.InputStream))
            {
                data = sr.ReadToEnd();
            }

            var isAjax = Request.IsAjaxRequest();

            Response.SetCookie(new HttpCookie("TestCookie", SystemTime.Now.ToString()));

            return Content(data + " Ajax:" + isAjax + " Server Time:" + SystemTime.Now);
        }
    }
}
