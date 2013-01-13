using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Sample.Service;
    
    public class WeixinController : Controller
    {
        private LocationService locationService;
        public WeixinController()
        {
            locationService=new LocationService();
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
                return Content(echostr);//返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + MP.CheckSignature.GetSignature(timestamp, nonce, Token));
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }
            try
            {
                var doc = XDocument.Load(Request.InputStream);
                var requestMessage = RequestMessageFactory.GetRequestEntity(doc);
                doc.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" + requestMessage.FromUserName +".txt"));//测试时可开启，帮助跟踪数据
                ResponseMessageBase responseMessage = null;
                switch (requestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        {
                            //TODO:交给Service处理具体信息
                            var strongRequestMessage = requestMessage as RequestMessageText;
                            var strongresponseMessage =
                                ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text) as
                                ResponseMessageText;
                            strongresponseMessage.Content =
                                string.Format(
                                    "您刚才发送了文字信息：{0}\r\n您还可以发送位置或图片信息，查看不同的回复。\r\nSDK官方地址：http://weixin.senparc.com",
                                    strongRequestMessage.Content);
                            responseMessage = strongresponseMessage;
                            break;
                        }
                    case RequestMsgType.Location:
                        {
                            //var strongRequestMessage = requestMessage as RequestMessageLocation;
                            //var strongresponseMessage =
                            //    ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text) as
                            //    ResponseMessageText;
                            //strongresponseMessage.Content =
                            //    string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                            //                  strongRequestMessage.Location_X, strongRequestMessage.Location_Y,
                            //                  strongRequestMessage.Scale,strongRequestMessage.Label);
                            responseMessage =
                                locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
                            break;
                        }
                    case RequestMsgType.Image:
                        {
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var responseDoc = MP.Helpers.EntityHelper.ConvertEntityToXml(responseMessage);
                responseDoc.Save(Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" + responseMessage.ToUserName + ".txt"));//测试时可开启，帮助跟踪数据

                return Content(responseDoc.ToString());
            }
            catch (Exception ex)
            {
                TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt"));
                tw.WriteLine(ex.Message);
                tw.WriteLine(ex.InnerException.Message);
                tw.Flush();
                tw.Close();
                return Content("");
            }
        }
    }
}
