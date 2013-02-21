using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
    using System.Xml.Linq;
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Helpers;

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
        public static IRequestMessageBase GetRequestEntity(XDocument doc)
        {
            var msgType = MsgTypeHelper.GetMsgType(doc);
            RequestMessageBase requestMessage = null;
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
                case RequestMsgType.Event:

                    //判断类型
                    switch (doc.Root.Element("Event").Value.ToUpper())
                    {
                        case "ENTER"://进入会话
                            requestMessage = new RequestMessageEvent_Entry();
                            break;
                        case "LOCATION"://地理位置
                            requestMessage = new RequestMessageEvent_Location();
                            break;
                        default://其他意外类型（也可以选择抛出异常）
                            requestMessage = new RequestMessageEventBase();
                            break;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            EntityHelper.FillEntityWithXml(requestMessage, doc);
            return requestMessage;
        }
    }
}
