/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：RequestMessageFactory.cs
    文件功能描述：获取XDocument转换后的IRequestMessageBase实例
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150327
    修改描述：添加小视频类型

    修改标识：Senparc - 20160813
    修改描述：v2.3.0 添加authorized和updateauthorized两种通知类型的处理

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.Entities.Request;
using Senparc.Weixin.Open.Helpers;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// RequestMessage工厂
    /// </summary>
    public static class RequestMessageFactory
    {
        //<?xml version="1.0" encoding="utf-8"?>
        //<xml>
        //  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
        //  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
        //  <CreateTime>1357986928</CreateTime>
        //  <MsgType><![CDATA[text]]></MsgType>
        //  <Content><![CDATA[中文]]></Content>
        //  <MsgId>5832509444155992350</MsgId>
        //</xml>

        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity(XDocument doc, PostModel postModel = null)
        {
            RequestMessageBase requestMessage = null;
            RequestInfoType infoType;

            try
            {
                infoType = InfoTypeHelper.GetRequestInfoType(doc);
                switch (infoType)
                {
                    case RequestInfoType.component_verify_ticket:
                        requestMessage = new RequestMessageComponentVerifyTicket();
                        break;
                    case RequestInfoType.unauthorized:
                        /*
                         * <xml>
                        <AppId>第三方平台appid</AppId>
                        <CreateTime>1413192760</CreateTime>
                        <InfoType>unauthorized</InfoType>
                        <AuthorizerAppid>公众号appid</AuthorizerAppid>
                        </xml>
                        */
                        requestMessage = new RequestMessageUnauthorized();
                        break;
                    case RequestInfoType.authorized:
                        /*
                        <xml>
                        <AppId>第三方平台appid</AppId>
                        <CreateTime>1413192760</CreateTime>
                        <InfoType>authorized</InfoType>
                        <AuthorizerAppid>公众号appid</AuthorizerAppid>
                        <AuthorizationCode>授权码（code）</AuthorizationCode>
                        <AuthorizationCodeExpiredTime>过期时间</AuthorizationCodeExpiredTime>
                        </xml>
                        */
                        requestMessage = new RequestMessageAuthorized();
                        break;
                    case RequestInfoType.updateauthorized:
                        requestMessage = new RequestMessageUpdateAuthorized();
                        break;
                    case RequestInfoType.notify_third_fasteregister:
                        requestMessage = new RequestMessageThirdFasteRegister();
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException(string.Format("InfoType：{0} 在RequestMessageFactory中没有对应的处理程序！", infoType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                }
                EntityHelper.FillEntityWithXml(requestMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！可能是InfoType不存在！，XML：{0}", doc.ToString()), ex);
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
    }
}
