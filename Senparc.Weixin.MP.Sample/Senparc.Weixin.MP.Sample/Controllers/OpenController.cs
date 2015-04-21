using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.MessageHandlers;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// 第三方开放平台
    /// </summary>
    public class OpenController : Controller
    {
        /// <summary>
        /// 发起授权页的体验URL
        /// </summary>
        /// <returns></returns>
        public ActionResult OAuth()
        {
            return View();
        }

        /// <summary>
        /// 授权事件接收URL
        /// </summary>
        /// <returns></returns>
        public ActionResult Notice()
        {
            var messageHandler = new ThirdPartyMessageHandler(Request.InputStream);

            return Content("success");
        }

        public ActionResult Callback(string appId)
        {
            //处理微信普通消息，可以使用MessageHandler

            

            return View();
        }

    }
}
