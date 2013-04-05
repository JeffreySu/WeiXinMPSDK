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
    using Senparc.Weixin.MP.Sample.Service;
    using Senparc.Weixin.MP.Sample.CustomerMessageHandler;

    public class WeixinController : Controller
    {
        private LocationService _locationService;
        private EventService _eventService;

        public WeixinController()
        {
            _locationService = new LocationService();
            _eventService = new EventService();
        }

        public readonly string Token = "weixin";

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
            var messageHandler = new CustomerMessageHandler(Request.InputStream);

            try
            {
                //测试时可开启此记录，帮助跟踪数据
                messageHandler.RequestDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据
                messageHandler.ResponseDocument.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine(ex.Message);
                    tw.WriteLine(ex.InnerException.Message);
                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }

            return Content(messageHandler.ResponseDocument.ToString());
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
        /// PS：此方法为常规switch判断方法，从v0.3.3版本起，此Demo不再更新
        /// </summary>
        [HttpPost]
        [ActionName("OldIndex")]
        public ActionResult OldPost(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }
            XDocument requestDoc = null;
            try
            {
                requestDoc = XDocument.Load(Request.InputStream);

                var requestMessage = RequestMessageFactory.GetRequestEntity(requestDoc);
                //如果不需要记录requestDoc，只需要：
                //var requestMessage = RequestMessageFactory.GetRequestEntity(Request.InputStream);

                requestDoc.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" + requestMessage.FromUserName + ".txt"));//测试时可开启，帮助跟踪数据
                ResponseMessageBase responseMessage = null;
                switch (requestMessage.MsgType)
                {
                    case RequestMsgType.Text://文字
                        {
                            //TODO:交给Service处理具体信息，参考/Service/EventSercice.cs 及 /Service/LocationSercice.cs
                            var strongRequestMessage = requestMessage as RequestMessageText;
                            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text) as
                                ResponseMessageText;
                            strongresponseMessage.Content =
                                string.Format(
                                    "您刚才发送了文字信息：{0}\r\n您还可以发送【位置】【图片】【语音】等类型的信息，查看不同格式的回复。\r\nSDK官方地址：http://weixin.senparc.com",
                                    strongRequestMessage.Content);
                            responseMessage = strongresponseMessage;
                            break;
                        }
                    case RequestMsgType.Location://位置
                        {
                            responseMessage = _locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
                            break;
                        }
                    case RequestMsgType.Image://图片
                        {
                            //TODO:交给Service处理具体信息
                            var strongRequestMessage = requestMessage as RequestMessageImage;
                            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.News) as
                                ResponseMessageNews;
                            strongresponseMessage.Content = "这里是正文内容，一共将发2条Article。";
                            strongresponseMessage.Articles.Add(new Article()
                                                                   {
                                                                       Title = "您刚才发送了图片信息",
                                                                       Description = "您发送的图片将会显示在边上",
                                                                       PicUrl = strongRequestMessage.PicUrl,
                                                                       Url = "http://weixin.senparc.com"
                                                                   });
                            strongresponseMessage.Articles.Add(new Article()
                                                                   {
                                                                       Title = "第二条",
                                                                       Description = "第二条带连接的内容",
                                                                       PicUrl = strongRequestMessage.PicUrl,
                                                                       Url = "http://weixin.senparc.com"
                                                                   });
                            responseMessage = strongresponseMessage;
                            break;
                        }
                    case RequestMsgType.Voice://语音
                        {
                            //TODO:交给Service处理具体信息
                            var strongRequestMessage = requestMessage as RequestMessageVoice;
                            var strongresponseMessage =
                               ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Music) as
                               ResponseMessageMusic;
                            strongresponseMessage.Music.MusicUrl = "http://weixin.senparc.com/Content/music1.mp3";
                            responseMessage = strongresponseMessage;
                            break;
                        }
                    case RequestMsgType.Event://事件
                        {
                            responseMessage = _eventService.GetResponseMessage(requestMessage as RequestMessageEventBase);
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var responseDoc = MP.Helpers.EntityHelper.ConvertEntityToXml(responseMessage);
                responseDoc.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" + responseMessage.ToUserName + ".txt"));//测试时可开启，帮助跟踪数据

                return Content(responseDoc.ToString());
                //如果不需要记录responseDoc，只需要：
                //return Content(responseMessage.ConvertEntityToXmlString());
            }
            catch (Exception ex)
            {
                using (
                    TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine(ex.Message);
                    tw.WriteLine(ex.InnerException.Message);
                    if (requestDoc != null)
                    {
                        tw.WriteLine(requestDoc.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }
        }
    }
}
