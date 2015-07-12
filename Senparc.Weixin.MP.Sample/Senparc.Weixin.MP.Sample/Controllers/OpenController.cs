using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.ThirdPartyMessageHandlers;
using Senparc.Weixin.Open.Entities.Request;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// 第三方开放平台演示
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
        public ActionResult Notice(PostModel postModel)
        {
            var logPath = Server.MapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            try
            {
                var messageHandler = new CustomThirdPartyMessageHandler(Request.InputStream, postModel);//初始化

                //记录RequestMessage日志（可选）
                messageHandler.RequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.AppId)));

                messageHandler.Excute();//执行

                //记录ResponseMessage日志（可选）
                using (TextWriter tw = new StreamWriter(Path.Combine(logPath, string.Format("{0}_Response_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.AppId))))
                {
                    tw.WriteLine(messageHandler.ResponseMessageText);
                    tw.Flush();
                    tw.Close();
                }

                return Content(messageHandler.ResponseMessageText);
            }
            catch (Exception ex)
            {
                return Content("error：" + ex.Message);
            }
        }

        /// <summary>
        /// 微信服务器会不间断推送最新的Ticket（10分钟一次），需要在此方法中更新缓存
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Callback(string appId, Entities.Request.PostModel postModel)
        {
            //此处的URL格式类型为：http://weixin.senparc.com/Open/Callback/$APPID$， 在RouteConfig中进行了配置，你也可以用自己的格式，只要和开放平台设置的一致。

            //处理微信普通消息，可以直接使用公众号的MessageHandler

            var messageHandler = new CommonService.CustomMessageHandler.CustomMessageHandler(Request.InputStream,
                postModel, 10);

            messageHandler.Execute();

            return new FixWeixinBugWeixinResult(messageHandler);
        }

    }
}
