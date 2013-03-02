using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.MP
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;

    public static class ResponseMessageFactory
    {
        //<?xml version="1.0" encoding="utf-8"?>
        //<xml>
        //  <FuncFlag>0</FuncFlag>
        //  <ToUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></ToUserName>
        //  <FromUserName><![CDATA[gh_a96a4a619366]]></FromUserName>
        //  <CreateTime>63497751173</CreateTime>
        //  <Content><![CDATA[文字信息]]></Content>
        //  <MsgType>text</MsgType>
        //</xml>

        /// <summary>
        /// 获取XDocument转换后的IResponseMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IResponseMessageBase GetResponseEntity(XDocument doc)
        {
            ResponseMessageBase responseMessage = null;
            ResponseMsgType msgType;
            try
            {
                msgType = MsgTypeHelper.GetResponseMsgType(doc);
                switch (msgType)
                {
                    case ResponseMsgType.Text:
                        responseMessage=new ResponseMessageText();
                        break;
                    case ResponseMsgType.News:
                        responseMessage=new ResponseMessageNews();
                        break;
                    case ResponseMsgType.Music:
                        responseMessage=new ResponseMessageMusic();
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在ResponseMessageFactory中没有对应的处理程序！", msgType), new ArgumentOutOfRangeException());
                }
                EntityHelper.FillEntityWithXml(responseMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("ResponseMessage转换出错！可能是MsgType不存在！，XML：{0}", doc.ToString()), ex);
            }
            return responseMessage;
        }


        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IResponseMessageBase GetResponseEntity(string xml)
        {
            return GetResponseEntity(XDocument.Parse(xml));
        }
    }
}
