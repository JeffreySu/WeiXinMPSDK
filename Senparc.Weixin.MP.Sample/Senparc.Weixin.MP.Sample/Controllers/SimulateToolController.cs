using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class SimulateToolController : BaseController
    {
        /// <summary>
        /// 获取请求XML
        /// </summary>
        /// <returns></returns>
        private XDocument GetrequestMessaageDoc(string url, string token, RequestMsgType requestType)
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
                //case RequestMsgType.Event:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("requestType");
            }

            requestMessaage.CreateTime = DateTime.Now;
            requestMessaage.FromUserName = "FromUserName（OpenId）" ;//用于区别不同的请求用户
            requestMessaage.ToUserName = "ToUserName";

            return requestMessaage.ConvertEntityToXml();
        }

        public ActionResult Index()
        {
            ViewData["Token"] = WeixinController.Token;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string url, string token, RequestMsgType requestType)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType);
                requestMessaageDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var responseMessageXml = MessageAgent.RequestXml(null, url, token, requestMessaageDoc.ToString());

                return Content(responseMessageXml);
            }
        }

        [HttpPost]
        public ActionResult GetRequestMessageXml(string url, string token, RequestMsgType requestType)
        {
            var requestMessaageDoc = GetrequestMessaageDoc(url, token, requestType);
            return Content(requestMessaageDoc.ToString());
        }
    }
}
