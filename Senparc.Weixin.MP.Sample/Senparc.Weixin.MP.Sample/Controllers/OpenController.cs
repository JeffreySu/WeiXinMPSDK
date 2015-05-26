using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.ThirdPartyMessageHandlers;

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
            var messageHandler = new CustomThirdPartyMessageHandler(Request.InputStream);

            return Content("success");
        }

        /// <summary>
        /// 微信服务器会不间断推送最新的Ticket，需要在此方法中更新缓存
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult Callback(string appId)
        {
            //处理微信普通消息，可以使用MessageHandler

            

            
            return View();
        }

    }
}
