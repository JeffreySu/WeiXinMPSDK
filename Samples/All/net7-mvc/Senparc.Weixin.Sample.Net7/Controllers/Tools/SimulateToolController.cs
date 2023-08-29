/*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：SimulateToolController.cs
    文件功能描述：消息模拟工具
    
    
    创建标识：Senparc - 20150312

    
    修改标识：Senparc - 20191004
    修改描述：优化消息模拟功能 
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Agents;
using Senparc.Weixin.MP;
using Senparc.Weixin.Tencent;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.Cache;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.Sample.Net6.Controllers
{
    /// <summary>
    /// 消息模拟器
    /// </summary>
    public class SimulateToolController : BaseController
    {
        #region 私有方法


        private string _responseMessageXml = null;

        /// <summary>
        /// 获取请求XML
        /// </summary>
        /// <returns></returns>
        private XDocument GetrequestMessaageDoc(/*string url, string token, */RequestMsgType requestType, Event? eventType)
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
            requestMessaage.CreateTime = SystemTime.Now;
            requestMessaage.FromUserName = requestMessaage.FromUserName ?? "FromUserName(OpenId)";//用于区别不同的请求用户
            requestMessaage.ToUserName = "ToUserNameValue";

            return requestMessaage.ConvertEntityToXml();
        }

        private Random _random = new Random();

        /// <summary>
        /// 模拟并发请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="requestMessaageDoc"></param>
        /// <param name="autoFillUrlParameters">是否自动填充Url中缺少的参数（signature、timestamp、nonce），默认为 true</param>
        /// <returns></returns>
        private async Task<string> TestAsyncTask(string url, string token, XDocument requestMessaageDoc, bool autoFillUrlParameters, int index, int sleepMillionSeconds = 0)
        {
            //修改MsgId，防止被去重
            var msgId = DateTimeHelper.GetUnixDateTime(SystemTime.Now.AddSeconds(_random.Next(0, 9999999))).ToString();
            if (requestMessaageDoc.Root.Element("MsgId") != null)
            {
                requestMessaageDoc.Root.Element("MsgId").Value = msgId;
            }

            //修改文字内容
            if (requestMessaageDoc.Root.Element("MsgType") != null && requestMessaageDoc.Root.Element("MsgType").Value.ToUpper() == "TEXT")
            {
                var values = requestMessaageDoc.Root.Element("Content").Value.Split('|').Select(z => z.Trim()).ToList();
                if (values.Count == 1)
                {
                    values.Add(msgId);
                    values.Add(index.ToString());
                }
                else
                {
                    values[1] = msgId;
                    values[2] = index.ToString();
                }
                requestMessaageDoc.Root.Element("Content").Value = string.Join(" | ", values);
            }

            var responseMessageXml = await MessageAgent.RequestXmlAsync(null, _serviceProvider, url, token, requestMessaageDoc.ToString(), autoFillUrlParameters, 1000 * 20);
            Thread.Sleep(sleepMillionSeconds); //模拟服务器响应时间

            _responseMessageXml = responseMessageXml;
            return responseMessageXml;
        }

        #endregion

        IServiceProvider _serviceProvider;

        public SimulateToolController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData["Token"] = Request.IsLocal() ? WeixinController.Token : "<请输入您自己的 URL 和 Token>";
            return View();
        }

        /// <summary>
        /// 模拟发送并返回结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string url, string token, RequestMsgType requestType, Event? eventType,
            bool testConcurrence, int testConcurrenceCount,
            bool testEncrypt, string encodingAESKey, string appId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var requestMessaageDoc = GetrequestMessaageDoc(/*url, token,*/ requestType, eventType);
                requestMessaageDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                string msgSigature = null;
                var timeStamp = SystemTime.NowTicks.ToString();
                var nonce = (SystemTime.NowTicks * 2).ToString();
                string encryptTypeAll = null;
                string openIdAll = null;

                //对请求消息进行加密
                if (testEncrypt)
                {
                    try
                    {
                        var openId = requestMessaageDoc.Root.Element("FromUserName").Value;
                        var toUserName = requestMessaageDoc.Root.Element("ToUserName").Value;

                        WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(token, encodingAESKey, appId);
                        string finalResponseXml = null;
                        var ret = msgCrype.EncryptRequestMsg(requestMessaageDoc.ToString(), timeStamp, nonce, toUserName, ref finalResponseXml, ref msgSigature);

                        if (ret == 0)
                        {
                            requestMessaageDoc = XDocument.Parse(finalResponseXml);//赋值最新的加密信息
                            openIdAll = $"openid={openId}";
                            encryptTypeAll = "&encrypt_type=aes";
                        }
                    }
                    catch (Exception ex)
                    {
                        var data = new { Success = false, LoadTime = "N/A", Result = "发生错误：" + ex.ToString() };
                        return Json(data, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
                        throw;
                    }

                    //Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog("模拟测试-加密消息：", requestMessaageDoc?.ToString());
                }

                var sigature = CheckSignature.GetSignature(timeStamp, nonce, token);

                url += url.Contains("?") ? "&" : "?";
                url += $"signature={sigature}&timeStamp={timeStamp}&nonce={nonce}&msg_signature={msgSigature}{encryptTypeAll}{openIdAll}";
                //参数如：signature=330ed3b64e363dc876f35e54a79e59b48739f567&timestamp=1570075722&nonce=863153744&openid=olPjZjsXuQPJoV0HlruZkNzKc91E&encrypt_type=aes&msg_signature=71dc359205a4660bc3b3046b643452c994b5897d

                var dt1 = SystemTime.Now;

                try
                {
                    dt1 = SystemTime.Now;
                    if (testConcurrence)
                    {
                        //异步方法
                        testConcurrenceCount = testConcurrenceCount > 30 ? 30 : testConcurrenceCount;//设定最高限额

                        //模拟并发请求
                        List<Task<string>> taskList = new List<Task<string>>();
                        for (int i = 0; i < testConcurrenceCount; i++)
                        {
                            var task = TestAsyncTask(url, token, requestMessaageDoc, autoFillUrlParameters: false, index: i, sleepMillionSeconds: 0);
                            taskList.Add(task);
                        }
                        Task.WaitAll(taskList.ToArray(), 1500 * 10);
                    }
                    else
                    {
                        //同步方法，立即发送
                        _responseMessageXml = MessageAgent.RequestXml(null, _serviceProvider, url, token, requestMessaageDoc.ToString(), autoFillUrlParameters: false);
                    }


                    if (string.IsNullOrEmpty(_responseMessageXml))
                    {
                        _responseMessageXml = "返回消息为空，可能已经被去重。\r\nMsgId相同的连续消息将被自动去重。";
                    }

                    var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                    var data = new
                    {
                        Success = true,
                        LoadTime = SystemTime.DiffTotalMS(dt1, "f4"),
                        Result = _responseMessageXml,
                        CacheType = cache.GetType().Name,
                        ConcurrenceCount = testConcurrenceCount
                    };
                    return Json(data, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
                }
                catch (Exception ex)
                {
                    var msg = string.Format("{0}\r\n{1}\r\n{2}", ex.Message, null, ex.InnerException != null ? ex.InnerException.Message : null);
                    return Json(new { Success = false, Result = msg }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });

                }
            }
        }

        /// <summary>
        /// 返回模拟发送的XML
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRequestMessageXml(/*string url, string token,*/ RequestMsgType requestType, Event? eventType)
        {
            var requestMessaageDoc = GetrequestMessaageDoc(/*url, token,*/ requestType, eventType);
            return Content(requestMessaageDoc.ToString());
        }
    }
}
