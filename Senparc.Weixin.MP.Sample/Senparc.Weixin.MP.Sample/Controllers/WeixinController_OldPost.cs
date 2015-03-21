/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinController_OldPost.cs
    文件功能描述：用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;
    //using Senparc.Weixin.MP.Sample.Service;
    //using Senparc.Weixin.MP.Sample.CustomerMessageHandler;

    public partial class WeixinController : Controller
    {
        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
        /// PS：此方法为常规switch判断方法，从v0.3.3版本起，此Demo不再更新
        /// </summary>
        [HttpPost]
        [ActionName("OldIndex")]
        public ActionResult OldPost(string signature, string timestamp, string nonce, string echostr)
        {
            LocationService locationService=new LocationService();
            EventService eventService= new EventService();

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
                            responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
                            break;
                        }
                    case RequestMsgType.Image://图片
                        {
                            //TODO:交给Service处理具体信息
                            var strongRequestMessage = requestMessage as RequestMessageImage;
                            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.News) as
                                ResponseMessageNews;
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
                            responseMessage = eventService.GetResponseMessage(requestMessage as RequestMessageEventBase);
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
