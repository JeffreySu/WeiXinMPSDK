/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MessageAgent.cs
    文件功能描述：代理请求
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml.Linq;
using Senparc.Weixin.HttpUtility;
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
        /// 默认代理请求超时时间（毫秒）
        /// </summary>
        private const int AGENT_TIME_OUT = 2500;

        /// <summary>
        /// 获取Xml结果。
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="stream"></param>
        /// <param name="useWeiWeiHiKey">是否使用WeiWeiHiKey，如果使用，则token为WeiWeiHiKey</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static string RequestXml(this IMessageHandler messageHandler, string url, string token, Stream stream, bool useWeiWeiHiKey = false, int timeOut = AGENT_TIME_OUT)
        {
            if (messageHandler != null)
            {
                messageHandler.UsedMessageAgent = true;
            }
            string timestamp = DateTime.Now.Ticks.ToString();
            string nonce = "GodBlessYou";
            string signature = CheckSignature.GetSignature(timestamp, nonce, token);
            url += string.Format("{0}signature={1}&timestamp={2}&nonce={3}",
                    url.Contains("?") ? "&" : "?", signature.AsUrlData(), timestamp.AsUrlData(), nonce.AsUrlData());
            var responseXml = RequestUtility.HttpPost(url, null, stream, timeOut: timeOut);
            return responseXml;
        }

        /// <summary>
        /// 获取Xml结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="xml"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static string RequestXml(this IMessageHandler messageHandler, string url, string token, string xml, int timeOut = AGENT_TIME_OUT)
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
                    return messageHandler.RequestXml(url, token, sw.BaseStream, timeOut: timeOut);
                }
            }
        }

        /// <summary>
        /// 对接Souidea（P2P）平台，获取Xml结果，使用WeiWeiHiKey对接
        /// WeiWeiHiKey的获取方式请看：
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="weiweihiKey"></param>
        /// <param name="xml"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static string RequestWeiWeiHiXml(this IMessageHandler messageHandler, string weiweihiKey, string xml, string weiweihiDomainName = "www.weiweihi.com", int timeOut = AGENT_TIME_OUT)
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
                    return messageHandler.RequestXml(url, weiweihiKey, sw.BaseStream, timeOut: timeOut);
                }
            }
        }

        /// <summary>
        /// 获取ResponseMessge结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="stream"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IResponseMessageBase RequestResponseMessage(this IMessageHandler messageHandler, string url, string token, Stream stream, int timeOut = AGENT_TIME_OUT)
        {
            return messageHandler.RequestXml(url, token, stream, timeOut: timeOut).CreateResponseMessage();
        }

        /// <summary>
        /// 获取ResponseMessge结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="xml"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IResponseMessageBase RequestResponseMessage(this IMessageHandler messageHandler, string url, string token, string xml, int timeOut = AGENT_TIME_OUT)
        {
            return messageHandler.RequestXml(url, token, xml, timeOut).CreateResponseMessage();
        }

        /// <summary>
        /// 获取微微嗨（前Souidea）开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="weiweihiKey"></param>
        /// <param name="xml"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, string xml, string weiweihiDomainName = "www.weiweihi.com", int timeOut = AGENT_TIME_OUT)
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, xml, weiweihiDomainName, timeOut).CreateResponseMessage();
        }

        /// <summary>
        /// 获取Souidea开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="weiweihiKey"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <param name="document"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, XDocument document, string weiweihiDomainName = "www.weiweihi.com", int timeOut = AGENT_TIME_OUT)
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, document.ToString(), weiweihiDomainName, timeOut).CreateResponseMessage();
        }

        /// <summary>
        /// 获取Souidea开放平台的ResponseMessge结果
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="weiweihiKey"></param>
        /// <param name="requestMessage"></param>
        /// <param name="weiweihiDomainName"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static IResponseMessageBase RequestWeiweihiResponseMessage(this IMessageHandler messageHandler, string weiweihiKey, RequestMessageBase requestMessage, string weiweihiDomainName = "www.weiweihi.com", int timeOut = AGENT_TIME_OUT)
        {
            return messageHandler.RequestWeiWeiHiXml(weiweihiKey, requestMessage.ConvertEntityToXmlString(), weiweihiDomainName, timeOut).CreateResponseMessage();
        }

        /// <summary>
        /// 使用GET请求测试URL和TOKEN是否可用
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static bool CheckUrlAndToken(string url, string token, int timeOut = 2000)
        {
            try
            {
                string timestamp = DateTime.Now.Ticks.ToString();
                string nonce = "GodBlessYou";
                string echostr = Guid.NewGuid().ToString("n");
                string signature = CheckSignature.GetSignature(timestamp, nonce, token);
                url += string.Format("{0}signature={1}&timestamp={2}&nonce={3}&echostr={4}",
                        url.Contains("?") ? "&" : "?", signature.AsUrlData(), timestamp.AsUrlData(), nonce.AsUrlData(), echostr.AsUrlData());

                var responseStr = RequestUtility.HttpGet(url, encoding: null, timeOut: timeOut);
                return echostr == responseStr;
            }
            catch
            {
                return false;
            }
        }
    }
}
