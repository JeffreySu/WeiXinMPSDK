using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.OpenMessageHandler;
using Senparc.Weixin.MP.Sample.CommonService.OpenTicket;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.ThirdPartyMessageHandlers;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 第三方开放平台演示
    /// </summary>
    public class OpenController : Controller
    {
        private string component_AppId = Senparc.Weixin.Config.DefaultSenparcWeixinSetting.Component_Appid;
        private string component_Secret = Senparc.Weixin.Config.DefaultSenparcWeixinSetting.Component_Secret;
        private string component_Token = Senparc.Weixin.Config.DefaultSenparcWeixinSetting.Component_Token;
        private string component_EncodingAESKey = Senparc.Weixin.Config.DefaultSenparcWeixinSetting.Component_EncodingAESKey;

        /// <summary>
        /// 发起授权页的体验URL
        /// </summary>
        /// <returns></returns>
        public ActionResult OAuth()
        {
            //获取预授权码
            var preAuthCode = ComponentContainer.TryGetPreAuthCode(component_AppId, component_Secret, true);

            var callbackUrl = "http://sdk.weixin.senparc.com/OpenOAuth/OpenOAuthCallback";//成功回调地址
            var url = ComponentApi.GetComponentLoginPageUrl(component_AppId, preAuthCode, callbackUrl);
            return Redirect(url);
        }

        /// <summary>
        /// 微信服务器会不间断推送最新的Ticket（10分钟一次），需要在此方法中更新缓存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Notice(PostModel postModel)
        {
            var logPath = Server.GetMapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //using (TextWriter tw = new StreamWriter(Path.Combine(logPath, string.Format("{0}_RequestStream.txt", DateTime.Now.Ticks))))
            //{
            //    using (var sr = new StreamReader(Request.InputStream))
            //    {
            //        tw.WriteLine(sr.ReadToEnd());
            //        tw.Flush();
            //    }
            //}

            //Request.InputStream.Seek(0, SeekOrigin.Begin);

            try
            {
                postModel.Token = component_Token;
                postModel.EncodingAESKey = component_EncodingAESKey;//根据自己后台的设置保持一致
                postModel.AppId = component_AppId;//根据自己后台的设置保持一致



                var messageHandler = new CustomThirdPartyMessageHandler(Request.GetRequestMemoryStream(), postModel);//初始化
                //注意：再进行“全网发布”时使用上面的CustomThirdPartyMessageHandler，发布完成之后使用正常的自定义的MessageHandler，例如下面一行。
                //var messageHandler = new CommonService.CustomMessageHandler.CustomMessageHandler(Request.GetRequestMemoryStream(),
                //    postModel, 10);

                //记录RequestMessage日志（可选）
                //messageHandler.EcryptRequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request.txt", DateTime.Now.Ticks)));
                messageHandler.RequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.AppId)));

                messageHandler.Execute();//执行

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
                throw;
                return Content("error：" + ex.Message);
            }
        }

        /// <summary>
        /// 授权事件接收URL
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Callback(Entities.Request.PostModel postModel)
        {
            //此处的URL格式类型为：http://sdk.weixin.senparc.com/Open/Callback/$APPID$， 在RouteConfig中进行了配置，你也可以用自己的格式，只要和开放平台设置的一致。

            //处理微信普通消息，可以直接使用公众号的MessageHandler。此处的URL也可以直接填写公众号普通的URL，如本Demo中的/Weixin访问地址。

            var logPath = Server.GetMapPath(string.Format("~/App_Data/Open/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            postModel.Token = component_Token;
            postModel.EncodingAESKey = component_EncodingAESKey; //根据自己后台的设置保持一致
            postModel.AppId = component_AppId; //根据自己后台的设置保持一致

            var maxRecordCount = 10;
            MessageHandler<CustomMessageContext> messageHandler = null;

            try
            {
                var checkPublish = false; //是否在“全网发布”阶段
                if (checkPublish)
                {
                    messageHandler = new OpenCheckMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);
                }
                else
                {
                    messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);
                }

                messageHandler.RequestDocument.Save(Path.Combine(logPath,
                    string.Format("{0}_Request_{1}.txt", DateTime.Now.Ticks, messageHandler.RequestMessage.FromUserName)));

                messageHandler.Execute(); //执行

                if (messageHandler.ResponseDocument != null)
                {
                    var ticks = DateTime.Now.Ticks;
                    messageHandler.ResponseDocument.Save(Path.Combine(logPath,
                        string.Format("{0}_Response_{1}.txt", ticks,
                            messageHandler.RequestMessage.FromUserName)));

                    //记录加密后的日志
                    //if (messageHandler.UsingEcryptMessage)
                    //{
                    //    messageHandler.FinalResponseDocument.Save(Path.Combine(logPath,
                    // string.Format("{0}_Response_Final_{1}.txt", ticks,
                    //     messageHandler.RequestMessage.FromUserName)));
                    //}
                }
                return new FixWeixinBugWeixinResult(messageHandler);
            }
            catch (Exception ex)
            {
                using (
                    TextWriter tw =
                        new StreamWriter(Server.GetMapPath("~/App_Data/Open/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }

                    if (ex.InnerException != null)
                    {
                        tw.WriteLine("========= InnerException =========");
                        tw.WriteLine(ex.InnerException.Message);
                        tw.WriteLine(ex.InnerException.Source);
                        tw.WriteLine(ex.InnerException.StackTrace);
                    }

                    tw.Flush();
                    tw.Close();
                    return Content("");
                }
            }
        }
    }
}
