/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
  
    文件名：RequestMessageFactory.cs
    文件功能描述：获取XDocument转换后的IRequestMessageBase实例
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送

    修改标识：Senparc - 20180909
    修改描述：v3.1.2 RequestMessageInfo_Contact_Sync 改名为 RequestMessageInfo_Change_Contact

    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口

    修改标识：Senparc - 20210324
    修改描述：v3.8.202 解决且有微信消息时间返回为 null 的问题

    修改标识：Senparc - 20230914
    修改描述：v3.16.4 企业微信三方代开发处理事件: 修复 Async 方法循环调用的 Bug

    修改标识：Senparc - 20231026
    修改描述：v3.17.0 成员对外联系 > 客户消息通知处理
    
   修改标识：IcedMango - 20240229
   修改描述：添加: 企业微信会话存档-产生会话回调事件（MSGAUDIT_NOTIFY）

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Entities.Request.Event;
using Senparc.NeuChar;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Context;

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
                    //获取消息类型
                    msgType = MsgTypeHelper.GetRequestMsgType(doc);

                    //自动获取对应类型的 RequestMessage
                    requestMessage = messageContext.GetRequestEntityMappingResult(msgType, doc) as WorkRequestMessageBase;

                    ////特殊对象处理（不使用底层 EntityHelper）
                    //if (requestMessage is RequestMessageEvent_SysApprovalChange)
                    //{
                    //    var requestXml = doc.Root.ToString();
                    //    XmlUtility.Deserialize<RequestMessageEvent_SysApprovalChange>(requestXml);
                    //}

                    //将 XML 内容填充到 requestMessage 中
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
                        case ThirdPartyInfo.RESET_PERMANENT_CODE:
                            requestMessage = new RequestMessageInfo_Reset_Permanent_Code();
                            break;
                        case ThirdPartyInfo.CHANGE_EXTERNAL_CONTACT:
                            switch (doc.Root.Element("ChangeType").Value.ToUpper())
                            {
                                case "ADD_EXTERNAL_CONTACT":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Add();
                                    break;
                                case "ADD_HALF_EXTERNAL_CONTACT":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Add_Half();
                                    break;
                                case "EDIT_EXTERNAL_CONTACT":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Modified();
                                    break;
                                case "DEL_EXTERNAL_CONTACT":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Del();
                                    break;
                                case "DEL_FOLLOW_USER":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Del_FollowUser();
                                    break;
                                case "TRANSFER_FAIL":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Transfer_Fail();
                                    break;
                                case "MSG_AUDIT_APPROVED":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_MsgAudit();
                                    break;
                                case "MSGAUDIT_NOTIFY":
                                    requestMessage = new RequestMessageEvent_MsgAuditNotify();
                                    break;
                                case "CREATE":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Create();
                                    break;
                                case "UPDATE":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Update();
                                    break;
                                case "DISMISS":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Dismiss();
                                    break;
                                default:
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_Base();
                                    break;
                            }
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException(string.Format("InfoType：{0} 在RequestMessageFactory中没有对应的处理程序！", infoType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                    }
                    EntityHelper.FillEntityWithXml(requestMessage, doc);
                }
                catch (ArgumentException ex)
                {
                    throw new WeixinException(string.Format("RequestMessage转换异常！InfoType类型存在的情况下无法处理！，XML：{0}", doc.ToString()), ex);
                }
            }
            else
            {
                throw new WeixinException(string.Format("RequestMessage转换出错！MsgType和InfoType都不存在！，XML：{0}", doc.ToString()));
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
            return GetRequestEntity(messageContext, XDocument.Parse(xml));
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
                return GetRequestEntity(messageContext, doc);
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
