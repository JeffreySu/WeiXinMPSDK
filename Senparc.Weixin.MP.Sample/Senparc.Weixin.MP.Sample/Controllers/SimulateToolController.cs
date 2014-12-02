using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class SimulateToolController : BaseController
    {
        /// <summary>
        /// 获取请求XML
        /// </summary>
        /// <returns></returns>
        private XDocument GetrequestMessaageDoc(string url, string token, RequestMsgType requestType, Event? eventType, string omitRepeat)
        {
            RequestMessageBase requestMessaage = null;
            switch (requestType)
            {
                case RequestMsgType.Text:
                    requestMessaage = new RequestMessageText()
                    {
                        Content = Request.Form["Content"],
                    };
                    break;
                case RequestMsgType.Location:
                    requestMessaage = new RequestMessageLocation()
                    {
                        Label = Request.Form["Label"],
                        Location_X = double.Parse(Request.Form["Location_X"]),
                        Location_Y = double.Parse(Request.Form["Location_Y"]),
                        Scale = int.Parse(Request.Form["Scale"])
                    };
                    break;
                case RequestMsgType.Image:
                    requestMessaage = new RequestMessageImage()
                    {
                        PicUrl = Request.Form["PicUrl"],
                    };
                    break;
                case RequestMsgType.Voice:
                    requestMessaage = new RequestMessageVoice()
                    {
                        Format = Request.Form["Format"],
                        Recognition = Request.Form["Recognition"],
                    };
                    break;
                case RequestMsgType.Video:
                    requestMessaage = new RequestMessageVideo()
                    {
                        MsgId = long.Parse(Request.Form["MsgId"]),
                        ThumbMediaId = Request.Form["ThumbMediaId"],
                    };
                    break;
                //case RequestMsgType.Link:
                //    break;
                case RequestMsgType.Event:
                    if (eventType.HasValue)
                    {
                        RequestMessageEventBase requestMessageEvent = null;
                        switch (eventType.Value)
                        {
                            //case Event.ENTER:
                            //    break;
                            case Event.LOCATION:
                                requestMessageEvent = new RequestMessageEvent_Location()
                                {
                                    Latitude = long.Parse(Request.Form["Event.Latitude"]),
                                    Longitude = long.Parse(Request.Form["Event.Longitude"]),
                                    Precision = double.Parse(Request.Form["Event.Precision"])
                                };
                                break;
                            case Event.subscribe:
                                requestMessageEvent = new RequestMessageEvent_Subscribe()
                                {
                                    EventKey = Request.Form["Event.EventKey"]
                                };
                                break;
                            case Event.unsubscribe:
                                requestMessageEvent = new RequestMessageEvent_Unsubscribe();
                                break;
                            case Event.CLICK:
                                requestMessageEvent = new RequestMessageEvent_Click()
                                   {
                                       EventKey = Request.Form["Event.EventKey"]
                                   };
                                break;
                            case Event.scan:
                                requestMessageEvent = new RequestMessageEvent_Scan()
                                 {
                                     EventKey = Request.Form["Event.EventKey"],
                                     Ticket = Request.Form["Event.Ticket"]
                                 }; break;
                            case Event.VIEW:
                                requestMessageEvent = new RequestMessageEvent_View()
                                 {
                                     EventKey = Request.Form["Event.EventKey"]
                                 }; break;
                            case Event.MASSSENDJOBFINISH:
                                requestMessageEvent = new RequestMessageEvent_MassSendJobFinish()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    ErrorCount = int.Parse(Request.Form["Event.ErrorCount"]),
                                    FilterCount = int.Parse(Request.Form["Event.FilterCount"]),
                                    SendCount = int.Parse(Request.Form["Event.SendCount"]),
                                    Status = Request.Form["Event.Status"],
                                    TotalCount = int.Parse(Request.Form["Event.TotalCount"])
                                }; break;
                            default:
                                throw new ArgumentOutOfRangeException("eventType");
                        }
                        requestMessaage = requestMessageEvent;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("eventType");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("requestType");
            }
            requestMessaage.MsgId = long.Parse(Request.Form["MsgId"]);
            requestMessaage.CreateTime = DateTime.Now;
            requestMessaage.FromUserName = requestMessaage.FromUserName ?? "FromUserName（OpenId）";//用于区别不同的请求用户
            requestMessaage.ToUserName = "ToUserName";

            return requestMessaage.ConvertEntityToXml();
        }

        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData["Token"] = WeixinController.Token;
            return View();
        }

        /// <summary>
        /// 模拟发送并返回结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string url, string token, RequestMsgType requestType, Event? eventType, string omitRepeat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType, eventType, omitRepeat);
                requestMessaageDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var responseMessageXml = MessageAgent.RequestXml(null, url, token, requestMessaageDoc.ToString());

                if (string.IsNullOrEmpty(responseMessageXml))
                {
                    responseMessageXml = "返回消息为空，可能已经被去重";
                }

                return Content(responseMessageXml);
            }
        }

        /// <summary>
        /// 返回模拟发送的XML
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRequestMessageXml(string url, string token, RequestMsgType requestType, Event? eventType, string omitRepeat)
        {
            var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType, eventType, omitRepeat);
            return Content(requestMessaageDoc.ToString());
        }
    }
}
