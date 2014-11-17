using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.Helpers;

namespace Senparc.Weixin.QY
{
    using Senparc.Weixin.QY.Entities;

    public static class RequestMessageFactory
    {
        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity(XDocument doc)
        {
            RequestMessageBase requestMessage = null;
            RequestMsgType msgType;
            try
            {
                msgType = MsgTypeHelper.GetRequestMsgType(doc);
                switch (msgType)
                {
                    case RequestMsgType.Text:
                        requestMessage = new RequestMessageText();
                        break;
                    case RequestMsgType.Location:
                        requestMessage = new RequestMessageLocation();
                        break;
                    case RequestMsgType.Image:
                        requestMessage = new RequestMessageImage();
                        break;
                    case RequestMsgType.Voice:
                        requestMessage = new RequestMessageVoice();
                        break;
                    case RequestMsgType.Video:
                        requestMessage = new RequestMessageVideo();
                        break;
                    case RequestMsgType.Event:
                        //判断Event类型
                        switch (doc.Root.Element("Event").Value.ToUpper())
                        {
                            case "CLICK"://菜单点击
                                requestMessage = new RequestMessageEvent_Click();
                                break;
                            case "VIEW"://URL跳转
                                requestMessage = new RequestMessageEvent_View();
                                break;
                            case "SUBSCRIBE"://订阅（关注）
                                requestMessage = new RequestMessageEvent_Subscribe();
                                break;
                            case "UNSUBSCRIBE"://取消订阅（关注）
                                requestMessage = new RequestMessageEvent_UnSubscribe();
                                break;
                            default://其他意外类型（也可以选择抛出异常）
                                requestMessage = new RequestMessageEventBase();
                                break;
                        }
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", msgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                }
                EntityHelper.FillEntityWithXml(requestMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType不存在！，XML：{0}", doc.ToString()), ex);
            }
            return requestMessage;
        }


        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity(string xml)
        {
            return GetRequestEntity(XDocument.Parse(xml));
        }

        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <param name="stream">如Request.InputStream</param>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity(Stream stream)
        {
            using (XmlReader xr = XmlReader.Create(stream))
            {
                var doc = XDocument.Load(xr);
                return GetRequestEntity(doc);
            }
        }

        /// <summary>
        /// 获取微信服务器发送过来的加密xml信息
        /// </summary>
        /// <param name="xml"></param>
        public static EncryptPostData GetEncryptPostData(string xml)
        {
            if (xml == null)
            {
                return null;
            }
            var encryptPostData = new EncryptPostData();
            encryptPostData.FillEntityWithXml(XDocument.Parse(xml));
            return encryptPostData;
        }
    }
}
