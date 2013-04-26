using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;
    //using Senparc.Weixin.MP.Sample.Service;
    //using Senparc.Weixin.MP.Sample.CustomerMessageHandler;
    using Senparc.Weixin.MP.Sample.CommonService;
    using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;

    public partial class WeixinController : Controller
    {
        public readonly string Token = "weixin";//与微信公众账号后台的Token设置保持一致，区分大小写。

        public WeixinController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + MP.CheckSignature.GetSignature(timestamp, nonce, Token));
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
        /// PS：此方法为简化方法，效果与OldPost一致
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.InputStream);

            try
            {
                //测试时可开启此记录，帮助跟踪数据
                messageHandler.RequestDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据
                messageHandler.ResponseDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
                return Content(messageHandler.ResponseDocument.ToString());
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace); 
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }
        }

        /// <summary>
        /// 最简化的处理流程
        /// </summary>
        [HttpPost]
        [ActionName("MiniPost")]
        public ActionResult MiniPost(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }

            var messageHandler = new CustomMessageHandler(Request.InputStream);

            messageHandler.Execute();//执行微信处理过程

            return Content(messageHandler.ResponseDocument.ToString());
        }

        /*
         * v0.3.0之前的原始Post方法见：WeixinController_OldPost.cs
         * 
         * 注意：虽然这里提倡使用CustomerMessageHandler的方法，但是MessageHandler基类最终还是基于OldPost的判断逻辑，
         * 因此如果需要深入了解Senparc.Weixin.MP内部处理消息的机制，可以查看WeixinController_OldPost.cs中的OldPost方法。
         * 目前为止OldPost依然有效，依然可用于生产。
         */
    }
}
