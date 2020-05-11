/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinController_OldPost.cs
    文件功能描述：用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    using Senparc.CO2NET.Utilities;
    using Senparc.NeuChar;
    using Senparc.NeuChar.Entities;
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;
    using Senparc.Weixin.MP.Sample.CommonService.Utilities;

    //using Senparc.Weixin.MP.CoreSample.Service;
    //using Senparc.Weixin.MP.CoreSample.CustomerMessageHandler;

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
            LocationService locationService = new LocationService();
            EventService eventService = new EventService();

            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }
            XDocument requestDoc = null;
            try
            {
                requestDoc = XDocument.Load(Request.Body);

                var requestMessage = RequestMessageFactory.GetRequestEntity(requestDoc);
                //如果不需要记录requestDoc，只需要：
                //var requestMessage = RequestMessageFactory.GetRequestEntity(Request.InputStream);

                requestDoc.Save(ServerUtility.ContentRootMapPath("~/App_Data/" + SystemTime.Now.Ticks + "_Request_" + requestMessage.FromUserName + ".txt"));//测试时可开启，帮助跟踪数据
                ResponseMessageBase responseMessage = null;
                switch (requestMessage.MsgType)
                {
                    case RequestMsgType.Text://文字
                        {
                            //TODO:交给Service处理具体信息，参考/Service/EventSercice.cs 及 /Service/LocationSercice.cs
                            var strongRequestMessage = requestMessage as RequestMessageText;
                            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                            strongresponseMessage.Content =
                                string.Format(
                                    "您刚才发送了文字信息：{0}\r\n您还可以发送【位置】【图片】【语音】等类型的信息，查看不同格式的回复。\r\nSDK官方地址：http://sdk.weixin.senparc.com",
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
                                ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
                            strongresponseMessage.Articles.Add(new Article()
                            {
                                Title = "您刚才发送了图片信息",
                                Description = "您发送的图片将会显示在边上",
                                PicUrl = strongRequestMessage.PicUrl,
                                Url = "http://sdk.weixin.senparc.com"
                            });
                            strongresponseMessage.Articles.Add(new Article()
                            {
                                Title = "第二条",
                                Description = "第二条带连接的内容",
                                PicUrl = strongRequestMessage.PicUrl,
                                Url = "http://sdk.weixin.senparc.com"
                            });
                            responseMessage = strongresponseMessage;
                            break;
                        }
                    case RequestMsgType.Voice://语音
                        {
                            //TODO:交给Service处理具体信息
                            var strongRequestMessage = requestMessage as RequestMessageVoice;
                            var strongresponseMessage =
                               ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
                            strongresponseMessage.Music.MusicUrl = "http://sdk.weixin.senparc.com/Content/music1.mp3";
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
                var responseDoc = Senparc.NeuChar.Helpers.EntityHelper.ConvertEntityToXml(responseMessage);
                responseDoc.Save(ServerUtility.ContentRootMapPath("~/App_Data/" + SystemTime.Now.Ticks + "_Response_" + responseMessage.ToUserName + ".txt"));//测试时可开启，帮助跟踪数据

                return Content(responseDoc.ToString());
                //如果不需要记录responseDoc，只需要：
                //return Content(responseMessage.ConvertEntityToXmlString());
            }
            catch (Exception ex)
            {
                using (
                    TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Error_" + SystemTime.Now.Ticks + ".txt")))
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

        [HttpPost]
        public ActionResult OriginalPost(string signature, string timestamp, string nonce, string echostr)
        {
            //消息安全验证代码开始
            //...
            //消息安全验证代码结束

            string requestXmlString = null;//请求XML字符串
            using (var sr = new StreamReader(HttpContext.Request.Body))
            {
                requestXmlString = sr.ReadToEnd();
            }

            //XML消息格式正确性验证代码开始
            //...
            //XML消息格式正确性验证代码结束

            /* XML消息范例
            <xml>
                <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
                <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
                <CreateTime>{{0}}</CreateTime>
                <MsgType><![CDATA[text]]></MsgType>
                <Content><![CDATA[{0}]]></Content>
                <MsgId>5832509444155992350</MsgId>
            </xml>
            */

            XDocument xmlDocument = XDocument.Parse(requestXmlString);//XML消息对象
            var xmlRoot = xmlDocument.Root;
            var msgType = xmlRoot.Element("MsgType").Value;//消息类型
            var toUserName = xmlRoot.Element("ToUserName").Value;
            var fromUserName = xmlRoot.Element("FromUserName").Value;
            var createTime = xmlRoot.Element("CreateTime").Value;
            var msgId = xmlRoot.Element("MsgId").Value;

            //根据MsgId去重开始
            //...
            //根据MsgId去重结束

            string responseXml = null;//响应消息XML
            var responseTime = (SystemTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000 - 8 * 60 * 60;

            switch (msgType)
            {
                case "text":
                    //处理文本消息
                    var content = xmlRoot.Element("Content").Value;//文本内容
                    if (content == "你好")
                    {
                        var title = "Title";
                        var description = "Description";
                        var picUrl = "PicUrl";
                        var url = "Url";
                        var articleCount = 1;
                        responseXml = @"<xml>
                        <ToUserName><![CDATA[" + fromUserName + @"]]></ToUserName>
                        <FromUserName><![CDATA[" + toUserName + @"]]></FromUserName>
                        <CreateTime>" + responseTime + @"</CreateTime>
                        <MsgType><![CDATA[news]]></MsgType>
                        <ArticleCount>" + articleCount + @"</ArticleCount>
                        <Articles>
                        <item>
                        <Title><![CDATA[" + title + @"]]></Title> 
                        <Description><![CDATA[" + description + @"]]></Description>
                        <PicUrl><![CDATA[" + picUrl + @"]]></PicUrl>
                        <Url><![CDATA[" + url + @"]]></Url>
                        </item>
                        </Articles>
                        </xml> ";
                    }
                    else if (content == "Senparc")
                    {
                        //相似处理逻辑
                    }
                    else
                    {
                        //...
                    }
                    break;
                case "image":
                    //处理图片消息
                    //...
                    break;
                case "event":
                    //这里会有更加复杂的事件类型处理
                    //...
                    break;
                //更多其他请求消息类型的判断...
                default:
                    //其他未知类型
                    break;
            }

            return Content(responseXml, "text/xml");//返回结果
        }
    }
}
