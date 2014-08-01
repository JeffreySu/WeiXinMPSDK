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
    public class SimulateToolController : Controller
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
                    break;
                case RequestMsgType.Image:
                    break;
                case RequestMsgType.Voice:
                    break;
                case RequestMsgType.Video:
                    break;
                //case RequestMsgType.Link:
                //    break;
                //case RequestMsgType.Event:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("requestType");
            }

            requestMessaage.CreateTime = DateTime.Now;
            requestMessaage.FromUserName = "FromUserName（OpenId）";
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
                
                var messageHandler = new CustomMessageHandler(ms);

                var responseMessageXml = messageHandler.RequestXml(url, token, requestMessaageDoc.ToString());

                return Content(responseMessageXml);
            }
        }
    }
}
