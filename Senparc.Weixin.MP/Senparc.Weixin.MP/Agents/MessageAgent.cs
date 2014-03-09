using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Agent
{
    /// <summary>
    /// 代理请求
    /// 注意！使用代理必然导致网络访问节点增加，会加重响应延时，
    ///       因此建议准备至少2-3秒的延迟时间的准备，
    ///       如果增加2-3秒后远远超过5秒的微信服务器等待时间，
    ///       需要慎重使用，否则可能导致用户无法收到消息。
    /// 
    /// 此外这个类中的方法也可以用于模拟服务器发送消息到自己的服务器进行测试。
    /// </summary>
    public static class MessageAgent
    {
        /// <summary>
        /// 获取Xml结果。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="stream"></param>
        /// <param name="useWeiWeiHiKey">是否使用WeiWeiHiKey，如果使用，则token为WeiWeiHiKey</param>
        /// <returns></returns>
        public static string RequestXml(this IMessageHandler messageHandler, string url, string token, Stream stream, bool useWeiWeiHiKey = false)
        {
            if (messageHandler != null)
            {
                messageHandler.UsedMessageAgent = true;
            }
            string timestamp = DateTime.Now.Ticks.ToString();
            string nonce = "GodBlessYou";
            string echostr = Guid.NewGuid().ToString("n");
            string signature = CheckSignature.GetSignature(timestamp, nonce, token);
            url += string.Format("{0}signature={1}&timestamp={2}&nonce={3}&echostr={4}",
                    url.Contains("?") ? "&" : "?", signature, timestamp, nonce, echostr);
            var responseXml = HttpUtility.RequestUtility.HttpPost(url, null, stream);
            return responseXml;
        }

        /// <summary>
        /// 获取Xml结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string RequestXml(this IMessageHandler messageHandler, string url, string token, string xml)
        {
            if (messageHandler != null)
            {
                messageHandler.UsedMessageAgent = true;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                //这里用ms模拟Request.InputStream
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(xml);
                    sw.Flush();
                    sw.BaseStream.Position = 0;
                    return messageHandler.RequestXml(url, token, sw.BaseStream);
                }
            }
        }

        /// <summary>
        /// 对接Souidea（P2P）平台，获取Xml结果，使用WeiWeiHiKey对接
        /// WeiWeiHiKey的获取方式请看：
        /// </summary>
        /// <param name="weiweihiKey"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string RequestWeiWeiHiXml(this IMessageHandler messageHandler, string weiweihiKey, string xml, string weiweihiDomainName = "www.weiweihi.com")
        {
            if (messageHandler != null)
            {
                messageHandler.UsedMessageAgent = true;
            }
            var url = "http://" + weiweihiDomainName + "/App/Weixin?weiweihiKey=" + weiweihiKey;//官方地址
            using (MemoryStream ms = new MemoryStream())
            {
                //这里用ms模拟Request.InputStream
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(xml);
                    sw.Flush();
                    sw.BaseStream.Position = 0;
                    return messageHandler.RequestXml(url, weiweihiKey, sw.BaseStream);
                }
            }
        }

        /// <summary>
        /// 获取ResponseMessge结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IResponseMessageBase RequestResponseMessage(this IMessageHandler messageHandler, string url, string token, Stream stream)
        {
            return messageHandler.RequestXml(url, token, stream).CreateResponseMessage();
        }

        /// <summary>
        /// 获取ResponseMessge结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IResponseMessageBase RequestResponseMessage(this IMessageHandler messageHandler, string url, string token, string xml)
        {
            return messageHandler.RequestXml(url, token, xml).CreateResponseMessage();
        }

        /// <summary>
        /// 获取Souidea开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="weiweihiKey"></param>
        /// <param name="xml"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, string xml, string weiweihiDomainName = "www.weiweihi.com")
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, xml, weiweihiDomainName).CreateResponseMessage();
        }

        /// <summary>
        /// 获取Souidea开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="weiweihiKey"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, XDocument document, string weiweihiDomainName = "www.weiweihi.com")
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, document.ToString(), weiweihiDomainName).CreateResponseMessage();
        }

        /// <summary>
        /// 获取Souidea开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="weiweihiKey"></param>
        /// <param name="requestMessage"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, RequestMessageBase requestMessage, string weiweihiDomainName = "www.weiweihi.com")
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, requestMessage.ConvertEntityToXmlString(), weiweihiDomainName).CreateResponseMessage();
        }

        /// <summary>
        /// 使用GET请求测试URL和TOKEN是否可用
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckUrlAndToken(string url, string token)
        {
            try
            {
                string timestamp = DateTime.Now.Ticks.ToString();
                string nonce = "GodBlessYou";
                string echostr = Guid.NewGuid().ToString("n");
                string signature = CheckSignature.GetSignature(timestamp, nonce, token);
                url += string.Format("{0}signature={1}&timestamp={2}&nonce={3}&echostr={4}",
                        url.Contains("?") ? "&" : "?", signature, timestamp, nonce, echostr);

                var responseStr = HttpUtility.RequestUtility.HttpGet(url, null);
                return echostr == responseStr;
            }
            catch
            {
                return false;
            }
        }
    }
}
