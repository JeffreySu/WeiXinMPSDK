/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：DefaultWorkMessageContext.cs
    文件功能描述：企业微信上下文的默认实现
    
    
    创建标识：Senparc - 20190915
    
    修改标识：OrchesAdam - 2019119
    修改描述：v3.7.104.2 添加“上报企业客户变更事件”
    
    修改标识：OrchesAdam - 20200430
    修改描述：v3.7.502 添加企业内部开发外部联系人

    修改标识：jiehanlin - 20200430
    修改描述：v3.7.502 添加客户群变更事件推送（CHANGE_EXTERNAL_CHAT）

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加 CHANGE_EXTERNAL_CHAT 对 ChangeType 的判断

    修改标识：Senparc - 20210324
    修改描述：v3.14.6 添加：审批申请状态变化回调通知： "SYS_APPROVAL_CHANGE"

    修改标识：XiaoPoTian - 20231119
    修改描述：v3.18.1 添加“企业客户标签变更事件回调通知”（CHANGE_EXTERNAL_Tag）
    
    修改标识：IcedMango - 20240229
    修改描述：添加: 企业微信会话存档-产生会话回调事件（MSGAUDIT_NOTIFY）

    修改标识：LofyLiu - 20240315
    修改描述：添加: 模板卡片回调事件
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.Weixin.Work.Entities.Request.Event;

namespace Senparc.Weixin.Work.MessageContexts
{
    /// <summary>
    /// 企业号上下文消息的默认实现
    /// </summary>
    public class DefaultWorkMessageContext
        : MessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>
    {

        /// <summary>
        /// 获取请求消息和实体之间的映射结果
        /// </summary>
        /// <param name="requestMsgType"></param>
        /// <returns></returns>
        public override IWorkRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
        {
            IWorkRequestMessageBase requestMessage;
            switch (requestMsgType)
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
                case RequestMsgType.ShortVideo:
                    requestMessage = new RequestMessageShortVideo();
                    break;
                case RequestMsgType.File:
                    requestMessage = new RequestMessageFile();
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
                        case "SCANCODE_PUSH"://扫码推事件(scancode_push)
                            requestMessage = new RequestMessageEvent_Scancode_Push();
                            break;
                        case "SCANCODE_WAITMSG"://扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
                            requestMessage = new RequestMessageEvent_Scancode_Waitmsg();
                            break;
                        case "PIC_SYSPHOTO"://弹出系统拍照发图(pic_sysphoto)
                            requestMessage = new RequestMessageEvent_Pic_Sysphoto();
                            break;
                        case "PIC_PHOTO_OR_ALBUM"://弹出拍照或者相册发图（pic_photo_or_album）
                            requestMessage = new RequestMessageEvent_Pic_Photo_Or_Album();
                            break;
                        case "PIC_WEIXIN"://弹出微信相册发图器(pic_weixin)
                            requestMessage = new RequestMessageEvent_Pic_Weixin();
                            break;
                        case "LOCATION_SELECT"://弹出地理位置选择器（location_select）
                            requestMessage = new RequestMessageEvent_Location_Select();
                            break;
                        case "LOCATION"://上报地理位置事件（location）
                            requestMessage = new RequestMessageEvent_Location();
                            break;
                        case "ENTER_AGENT"://用户进入应用的事件推送（enter_agent）
                            requestMessage = new RequestMessageEvent_Enter_Agent();
                            break;
                        case "BATCH_JOB_RESULT"://异步任务完成事件推送(batch_job_result)
                            requestMessage = new RequestMessageEvent_Batch_Job_Result();
                            break;
                        case "CHANGE_CONTACT"://通讯录更改推送(change_contact)
                            switch (doc.Root.Element("ChangeType").Value.ToUpper())
                            {
                                case "CREATE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Create();
                                    break;
                                case "UPDATE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Update();
                                    break;
                                case "DELETE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Base();
                                    break;
                                case "CREATE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Create();
                                    break;
                                case "UPDATE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Update();
                                    break;
                                case "DELETE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Base();
                                    break;
                                case "UPDATE_TAG":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Tag_Update();
                                    break;
                                default://其他意外类型（也可以选择抛出异常）
                                    requestMessage = new RequestMessageEventBase();
                                    break;
                            }
                            break;
                        case "CHANGE_EXTERNAL_CONTACT"://客户变更回调
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
                                case "MSG_AUDIT_APPROVED":
                                    requestMessage = new RequestMessageEvent_Change_ExternalContact_MsgAudit();
                                    break;
                                default:
                                    requestMessage = new RequestMessageEventBase();
                                    break;
                            }
                            break;
                        case "CHANGE_EXTERNAL_CHAT"://客户群变更事件推送
                            switch (doc.Root.Element("ChangeType").Value.ToUpper())
                            {
                                case "CREATE":
                                    requestMessage = new RequestMessageEvent_Change_External_Chat_Create();
                                    break;
                                case "UPDATE":
                                    requestMessage = new RequestMessageEvent_Change_External_Chat_Update(doc.Root.Element("MemChangeList"));
                                    break;
                                case "DISMISS":
                                    requestMessage = new RequestMessageEvent_Change_External_Chat_Dismiss();
                                    break;
                                default://其他意外类型（也可以选择抛出异常）
                                    requestMessage = new RequestMessageEventBase();
                                    break;
                            }
                            break;
                        case "CHANGE_EXTERNAL_TAG"://企业客户标签变更事件推送
                            //创建标签时，此项为tag，创建标签组时，此项为tag_group
                            //文档：https://developer.work.weixin.qq.com/document/path/92130#%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE%E5%88%9B%E5%BB%BA%E4%BA%8B%E4%BB%B6
                            var tagType = doc.Root.Element("TagType").Value;
                            switch (doc.Root.Element("ChangeType").Value.ToUpper())
                            {
                                case "CREATE":
                                    requestMessage = new RequestMessageEvent_Change_External_Tag_Create(tagType);
                                    break;
                                case "UPDATE":
                                    requestMessage = new RequestMessageEvent_Change_External_Tag_Update(tagType);
                                    break;
                                case "DELETE":
                                    requestMessage = new RequestMessageEvent_Change_External_Tag_Delete(tagType);
                                    break;
                                case "SHUFFLE":
                                    requestMessage = new RequestMessageEvent_Change_External_Tag_Shuffle();
                                    break;
                                default://其他意外类型（也可以选择抛出异常）
                                    requestMessage = new RequestMessageEventBase();
                                    break;
                            }
                            break;
                        case "LIVING_STATUS_CHANGE"://直播回调事件(living_status_change)
                            requestMessage = new RequestMessageEvent_Living_Status_Change_Base();
                            break;
                        case "SYS_APPROVAL_CHANGE":
                            requestMessage = new RequestMessageEvent_SysApprovalChange();
                            break;
                        case "OPEN_APPROVAL_CHANGE":
                            requestMessage = new RequestMessageEvent_OpenApprovalChange();
                            break;
                        // 企业微信会话存档-产生会话回调事件(msgaudit_notify)
                        // 文档: https://developer.work.weixin.qq.com/document/path/95039
                        case "MSGAUDIT_NOTIFY":
                            requestMessage = new RequestMessageEvent_MsgAuditNotify();
                            break;
                        case "TEMPLATE_CARD_EVENT": //模板卡片回调事件
                            requestMessage = new RequestMessageEvent_TemplateCardClick();
                            break;
                        default://其他意外类型（也可以选择抛出异常）
                            requestMessage = new RequestMessageEventBase();
                            break;
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", requestMsgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
            }
            return requestMessage;
        }

        /// <summary>
        /// 获取响应消息和实体之间的映射结果
        /// </summary>
        /// <param name="responseMsgType"></param>
        /// <returns></returns>
        public override IWorkResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
        {
            IWorkResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Text:
                    responseMessage = new ResponseMessageText();
                    break;
                case ResponseMsgType.News:
                    responseMessage = new ResponseMessageNews();
                    break;
                case ResponseMsgType.Image:
                    responseMessage = new ResponseMessageImage();
                    break;
                case ResponseMsgType.Voice:
                    responseMessage = new ResponseMessageVoice();
                    break;
                case ResponseMsgType.Video:
                    responseMessage = new ResponseMessageVideo();
                    break;
                case ResponseMsgType.NoResponse:
                    responseMessage = new WorkResponseMessageNoResponse();
                    break;
                case ResponseMsgType.SuccessResponse:
                    responseMessage = new WorkSuccessResponseMessage();
                    break;

                #region 不支持
                case ResponseMsgType.Transfer_Customer_Service:
                case ResponseMsgType.Music:
                #endregion

                #region 扩展类型
                case ResponseMsgType.MultipleNews:
                case ResponseMsgType.LocationMessage:
                case ResponseMsgType.UseApi:
                #endregion

                case ResponseMsgType.Other:
                case ResponseMsgType.Unknown:
                default:
                    responseMessage = new WorkResponseMessageUnknownType()
                    {
                        ResponseDocument = doc
                    };
                    break;
            }
            return responseMessage;

        }
    }
}
