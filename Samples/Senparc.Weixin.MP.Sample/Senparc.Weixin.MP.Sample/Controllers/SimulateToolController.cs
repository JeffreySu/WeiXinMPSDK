/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SimulateToolController.cs
    文件功能描述：消息模拟工具
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.CO2NET.Helpers;
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
        private XDocument GetrequestMessaageDoc(string url, string token, RequestMsgType requestType, Event? eventType)
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
                                    EventKey = Request.Form["Event.EventKey"],
                                    Ticket = Request.Form["Event.Ticket"]
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
                                    SentCount = int.Parse(Request.Form["Event.SentCount"]),
                                    Status = Request.Form["Event.Status"],
                                    TotalCount = int.Parse(Request.Form["Event.TotalCount"])
                                }; break;
                            case Event.TEMPLATESENDJOBFINISH:
                                requestMessageEvent = new RequestMessageEvent_TemplateSendJobFinish()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    MsgID = long.Parse(Request.Form["Event.MsgID"]),
                                    Status = Request.Form["Event.Status"],
                                }; break;
                            case Event.scancode_push:
                                requestMessageEvent = new RequestMessageEvent_Scancode_Push()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    EventKey = Request.Form["Event.EventKey"],
                                    ScanCodeInfo = new ScanCodeInfo()
                                        {
                                            ScanResult = Request.Form["Event.ScanResult"],
                                            ScanType = Request.Form["Event.ScanType"],
                                        }
                                }; break;
                            case Event.scancode_waitmsg:
                                requestMessageEvent = new RequestMessageEvent_Scancode_Waitmsg()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    EventKey = Request.Form["Event.EventKey"],
                                    ScanCodeInfo = new ScanCodeInfo()
                                    {
                                        ScanResult = Request.Form["Event.ScanResult"],
                                        ScanType = Request.Form["Event.ScanType"],
                                    }
                                }; break;
                            case Event.pic_sysphoto:
                                var sysphotoPicMd5Sum = Request.Form["Event.PicMd5Sum"];
                                PicItem sysphotoPicItem = new PicItem()
                                    {
                                        item = new Md5Sum()
                                            {
                                                PicMd5Sum = sysphotoPicMd5Sum
                                            }
                                    };
                                List<PicItem> sysphotoPicItems = new List<PicItem>();
                                sysphotoPicItems.Add(sysphotoPicItem);
                                requestMessageEvent = new RequestMessageEvent_Pic_Sysphoto()
                            {
                                FromUserName = "mphelper",//系统指定
                                EventKey = Request.Form["Event.EventKey"],
                                SendPicsInfo = new SendPicsInfo()
                                {
                                    Count = Request.Form["Event.Count"],
                                    PicList = sysphotoPicItems
                                }
                            }; break;
                            case Event.pic_photo_or_album:
                                var photoOrAlbumPicMd5Sum = Request.Form["Event.PicMd5Sum"];
                                PicItem photoOrAlbumPicItem = new PicItem()
                                {
                                    item = new Md5Sum()
                                    {
                                        PicMd5Sum = photoOrAlbumPicMd5Sum
                                    }
                                };
                                List<PicItem> photoOrAlbumPicItems = new List<PicItem>();
                                photoOrAlbumPicItems.Add(photoOrAlbumPicItem);
                                requestMessageEvent = new RequestMessageEvent_Pic_Sysphoto()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    EventKey = Request.Form["Event.EventKey"],
                                    SendPicsInfo = new SendPicsInfo()
                                    {
                                        Count = Request.Form["Event.Count"],
                                        PicList = photoOrAlbumPicItems
                                    }
                                }; break;
                            case Event.pic_weixin:
                                var weixinPicMd5Sum = Request.Form["Event.PicMd5Sum"];
                                PicItem weixinPicItem = new PicItem()
                                {
                                    item = new Md5Sum()
                                    {
                                        PicMd5Sum = weixinPicMd5Sum
                                    }
                                };
                                List<PicItem> weixinPicItems = new List<PicItem>();
                                weixinPicItems.Add(weixinPicItem);
                                requestMessageEvent = new RequestMessageEvent_Pic_Sysphoto()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    EventKey = Request.Form["Event.EventKey"],
                                    SendPicsInfo = new SendPicsInfo()
                                    {
                                        Count = Request.Form["Event.Count"],
                                        PicList = weixinPicItems
                                    }
                                }; break;
                            case Event.location_select:
                                requestMessageEvent = new RequestMessageEvent_Location_Select()
                                {
                                    FromUserName = "mphelper",//系统指定
                                    EventKey = Request.Form["Event.EventKey"],
                                    SendLocationInfo = new SendLocationInfo()
                                        {
                                            Label = Request.Form["Event.Label"],
                                            Location_X = Request.Form["Event.Location_X"],
                                            Location_Y = Request.Form["Event.Location_Y"],
                                            Poiname = Request.Form["Event.Poiname"],
                                            Scale = Request.Form["Event.Scale"],
                                        }
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
        /// 模拟并发请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="requestMessaageDoc"></param>
        /// <returns></returns>
        private string TestAsyncTask(string url, string token, XDocument requestMessaageDoc)
        {
            //修改MsgId，防止被去重
            if (requestMessaageDoc.Root.Element("MsgId") != null)
            {
                requestMessaageDoc.Root.Element("MsgId").Value =
                    DateTimeHelper.GetWeixinDateTime(DateTime.Now.AddSeconds(Thread.CurrentThread.GetHashCode())).ToString();
            }

            var responseMessageXml = MessageAgent.RequestXml(null, url, token, requestMessaageDoc.ToString(), 1000 * 20);
            Thread.Sleep(100); //模拟服务器响应时间
            return responseMessageXml;
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
        public ActionResult Index(string url, string token, RequestMsgType requestType, Event? eventType, bool testConcurrence, int testConcurrenceCount)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType, eventType);
                requestMessaageDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var responseMessageXml = MessageAgent.RequestXml(null, url, token, requestMessaageDoc.ToString());

                if (string.IsNullOrEmpty(responseMessageXml))
                {
                    responseMessageXml = "返回消息为空，可能已经被去重。\r\nMsgId相同的连续消息将被自动去重。";
                }

                try
                {
                    DateTime dt1 = DateTime.Now;
                    if (testConcurrence)
                    {
                        testConcurrenceCount = testConcurrenceCount > 30 ? 30 : testConcurrenceCount;//设定最高限额

                        //模拟并发请求
                        List<Task<string>> taskList = new List<Task<string>>();
                        for (int i = 0; i < testConcurrenceCount; i++)
                        {
                            var task = Task.Factory.StartNew(() => TestAsyncTask(url, token, requestMessaageDoc));
                            taskList.Add(task);
                        }
                        Task.WaitAll(taskList.ToArray(), 1000 * 10);
                    }
                    DateTime dt2 = DateTime.Now;

                    return Json(new { Success = true, LoadTime = (dt2 - dt1).TotalMilliseconds.ToString("##.####"), Result = responseMessageXml });
                }
                catch (Exception ex)
                {
                    var msg = string.Format("{0}\r\n{1}\r\n{2}", ex.Message, null, ex.InnerException != null ? ex.InnerException.Message : null);
                    return Json(new { Success = false, Result = msg });

                }
            }
        }

        /// <summary>
        /// 返回模拟发送的XML
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRequestMessageXml(string url, string token, RequestMsgType requestType, Event? eventType)
        {
            var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType, eventType);
            return Content(requestMessaageDoc.ToString());
        }
    }
}
