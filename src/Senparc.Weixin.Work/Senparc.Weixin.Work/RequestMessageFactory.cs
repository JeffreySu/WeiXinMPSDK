/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：RequestMessageFactory.cs
    文件功能描述：获取XDocument转换后的IRequestMessageBase实例
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送

    修改标识：Senparc - 20180909
    修改描述：v3.1.2 RequestMessageInfo_Contact_Sync 改名为 RequestMessageInfo_Change_Contact
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work
{
    public static class RequestMessageFactory
    {
        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IWorkRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, XDocument doc)
            where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
        {
            WorkRequestMessageBase requestMessage = null;
            RequestMsgType msgType;
            ThirdPartyInfo infoType;

            //区分普通消息与第三方应用授权推送消息，MsgType有值说明是普通消息，反之则是第三方应用授权推送消息
            if (doc.Root.Element("MsgType") != null)
            {
                //常规推送信息
                try
                {
                    msgType = MsgTypeHelper.GetRequestMsgType(doc);

                    messageContext.GetRequestEntityMappingResult(msgType, doc);
                    
                    EntityHelper.FillEntityWithXml(requestMessage, doc);
                }
                catch (ArgumentException ex)
                {
                    throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType不存在！，XML：{0}", doc.ToString()), ex);
                }
            }
            else if (doc.Root.Element("InfoType") != null)
            {
                //第三方回调
                try
                {
                    infoType = Work.Helpers.MsgTypeHelper.GetThirdPartyInfo(doc);
                    switch (infoType)
                    {
                        case ThirdPartyInfo.SUITE_TICKET://推送suite_ticket协议
                            requestMessage = new RequestMessageInfo_Suite_Ticket();
                            break;
                        case ThirdPartyInfo.CHANGE_AUTH://变更授权的通知
                            requestMessage = new RequestMessageInfo_Change_Auth();
                            break;
                        case ThirdPartyInfo.CANCEL_AUTH://取消授权的通知
                            requestMessage = new RequestMessageInfo_Cancel_Auth();
                            break;
                        case ThirdPartyInfo.CREATE_AUTH://授权成功推送auth_code事件
                            requestMessage = new RequestMessageInfo_Create_Auth();
                            break;
                        case ThirdPartyInfo.CHANGE_CONTACT://通讯录变更通知
                            requestMessage = new RequestMessageInfo_Change_Contact();
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException(string.Format("InfoType：{0} 在RequestMessageFactory中没有对应的处理程序！", infoType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                    }
                    EntityHelper.FillEntityWithXml(requestMessage, doc);
                }
                catch (ArgumentException ex)
                {
                    throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType和InfoType都不存在！，XML：{0}", doc.ToString()), ex);
                }
            }
            else
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！可能是MsgType和InfoType都不存在！，XML：{0}", doc.ToString()));
            }

            return requestMessage;
        }


        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <returns></returns>
        public static IWorkRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, string xml)
            where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
        {
            return GetRequestEntity(messageContext,XDocument.Parse(xml));
        }

        /// <summary>
        /// 获取XDocument转换后的IRequestMessageBase实例。
        /// 如果MsgType不存在，抛出UnknownRequestMsgTypeException异常
        /// </summary>
        /// <param name="stream">如Request.InputStream</param>
        /// <returns></returns>
        public static IWorkRequestMessageBase GetRequestEntity<TMC>(TMC messageContext, Stream stream)
            where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
        {
            using (XmlReader xr = XmlReader.Create(stream))
            {
                var doc = XDocument.Load(xr);
                return GetRequestEntity(messageContext,doc);
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
