﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
  
    文件名：RequestMessageFactory.cs
    文件功能描述：获取XDocument转换后的IRequestMessageBase实例
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150327
    修改描述：添加小视频类型
    
    修改标识：Senparc - 20180829
    修改描述：v15.4.0 支持NeuChar，添加 RequestMessageNeuChar() 方法

    修改标识：Senparc - 20180829
    修改描述：v15.4.0 支持NeuChar，添加 RequestMessageNeuChar() 方法

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// RequestMessage 消息处理方法工厂类
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
        /// <param name="messageContext">MessageContext 消息上下文对象</param>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, XDocument doc, PostModel postModel = null)
            where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            RequestMessageBase requestMessage = null;
            RequestMsgType msgType;

            try
            {
                msgType = MsgTypeHelper.GetRequestMsgType(doc);

                requestMessage = messageContext.GetRequestEntityMappingResult(msgType, doc) as RequestMessageBase;

                Senparc.NeuChar.Helpers.EntityHelper.FillEntityWithXml(requestMessage, doc);
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType不存在！，XML：{0}", doc.ToString()), ex);
            }
            return requestMessage;
        }


        /// <summary>
        /// 获取XML转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <param name="messageContext">MessageContext 消息上下文对象</param>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, string xml)
            where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            return GetRequestEntity(messageContext, XDocument.Parse(xml));
        }


        /// <summary>
        /// 获取内容为XML的Stream转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <param name="messageContext">MessageContext 消息上下文对象</param>
        /// <param name="stream">如Request.InputStream</param>
        /// <returns></returns>
        public static IRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, Stream stream)
            where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
        {
            using (XmlReader xr = XmlReader.Create(stream))
            {
                var doc = XDocument.Load(xr);
                return GetRequestEntity(messageContext, doc);
            }
        }
    }
}
